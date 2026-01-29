using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfContents.Extensions;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for content layer auto-upgrade cost.
    /// Compares None, Single, and Full upgrade policies.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfContentUpgradeBenchmarks
    {
        private const string PdfFileName = "Resources/19005-1_FAQ.PDF";

        private PdfFile _file;
        private PdfDocument _document;
        private PdfContentInstructionCollection _instructions;
        private PdfContentObjectText _textObject;
        private ulong _instructionCount;
        private ulong _operationCount;

        [Params(UpgradePolicy.None, UpgradePolicy.ResolveOnly, UpgradePolicy.Single, UpgradePolicy.Full)]
        public UpgradePolicy Policy { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
            _file = PdfFile.Open(pdfPath);
            _file.Initialize();

            _document = PdfDocument.OpenFile(_file);

            // Temporarily set Full to find the text object during setup
            LibraryInstance.UpgradePolicy = UpgradePolicy.Full;

            using var catalog = _document.GetCatalog();
            using var tree = catalog.GetPages();
            using var page = tree.GetPage(1);
            using var contents = page.GetContents();
            _instructions = contents.GetInstructionCollection();
            _instructionCount = _instructions.GetInstructionsSize();

            // Find first text object for operation benchmarks
            for (ulong i = 0; i < _instructionCount; i++) {
                using var instruction = _instructions.At(i);
                if (instruction is PdfContentObject contentObject) {
                    if (contentObject.GetObjectType() == PdfContentObjectType.Text) {
                        _textObject = PdfContentObjectText.FromContentObject(contentObject);
                        _operationCount = _textObject.GetOperationsSize();
                        break;
                    }
                }
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _textObject?.Dispose();
            _instructions?.Dispose();
            _document?.Dispose();
            _file?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public ulong InstructionCollection_At()
        {
            LibraryInstance.UpgradePolicy = Policy;
            ulong count = 0;
            for (ulong i = 0; i < _instructionCount; i++) {
                using var instruction = _instructions.At(i);
                count++;
            }
            return count;
        }

        [Benchmark]
        public ulong TextObject_GetOperationAt()
        {
            LibraryInstance.UpgradePolicy = Policy;
            ulong count = 0;
            for (ulong i = 0; i < _operationCount; i++) {
                using var operation = _textObject.GetOperationAt(i);
                count++;
            }
            return count;
        }
    }
}
