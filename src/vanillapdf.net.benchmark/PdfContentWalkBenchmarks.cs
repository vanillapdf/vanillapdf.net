using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for walking page content streams the way a renderer does:
    /// every instruction, and every operation inside text objects, dispatched by type.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfContentWalkBenchmarks
    {
        private const string PdfFileName = "Resources/19005-1_FAQ.PDF";

        private PdfFile _file;
        private PdfDocument _document;
        private readonly List<PdfContentInstructionCollection> _collections = new List<PdfContentInstructionCollection>();
        private PdfContentOperation _operation;

        [GlobalSetup]
        public void Setup()
        {
            var pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
            _file = PdfFile.Open(pdfPath);
            _file.Initialize();

            _document = PdfDocument.OpenFile(_file);

            LibraryInstance.UpgradePolicy = UpgradePolicy.Single;

            using var catalog = _document.GetCatalog();
            using var tree = catalog.GetPages();
            ulong pageCount = tree.GetPageCount();
            for (ulong i = 1; i <= pageCount; i++) {
                using var page = tree.GetPage(i);
                using var contents = page.GetContents();
                _collections.Add(contents.GetInstructionCollection());
            }

            _operation = FindFirstOperation();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _operation?.Dispose();
            foreach (var collection in _collections) {
                collection.Dispose();
            }
            _document?.Dispose();
            _file?.Dispose();
        }

        /// <summary>
        /// Visits every instruction and every operation inside text objects, reading their types.
        /// </summary>
        [Benchmark(Baseline = true)]
        public long WalkContentStreams()
        {
            long total = 0;
            foreach (var collection in _collections) {
                ulong count = collection.GetInstructionsSize();
                for (ulong i = 0; i < count; i++) {
                    total += VisitInstruction(collection, i);
                }
            }
            return total;
        }

        [Benchmark]
        public PdfContentOperationType ContentOperation_GetOperationType()
        {
            return _operation.GetOperationType();
        }

        private static long VisitInstruction(PdfContentInstructionCollection collection, ulong index)
        {
            using var instruction = collection.At(index);
            if (instruction is PdfContentOperation operation) {
                return (long)operation.GetOperationType();
            }

            if (instruction is PdfContentObject contentObject && contentObject.GetObjectType() == PdfContentObjectType.Text) {
                using var textObject = PdfContentObjectText.FromContentObject(contentObject);
                return VisitTextObject(textObject);
            }

            return 0;
        }

        private static long VisitTextObject(PdfContentObjectText textObject)
        {
            long total = 0;
            ulong count = textObject.GetOperationsSize();
            for (ulong i = 0; i < count; i++) {
                using var operation = textObject.GetOperationAt(i);
                total += (long)operation.GetOperationType();
            }
            return total;
        }

        private PdfContentOperation FindFirstOperation()
        {
            foreach (var collection in _collections) {
                ulong count = collection.GetInstructionsSize();
                for (ulong i = 0; i < count; i++) {
                    var instruction = collection.At(i);
                    if (instruction is PdfContentOperation operation) {
                        return operation;
                    }
                    instruction.Dispose();
                }
            }

            throw new InvalidOperationException($"No content operation found in {PdfFileName}");
        }
    }
}
