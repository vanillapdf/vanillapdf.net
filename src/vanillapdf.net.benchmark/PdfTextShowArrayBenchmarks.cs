using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for walking the elements of TJ (show text array) operations,
    /// the hottest per-element path when rendering text.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfTextShowArrayBenchmarks
    {
        private const string PdfFileName = "Resources/19005-1_FAQ.PDF";

        private PdfFile _file;
        private PdfDocument _document;
        private readonly List<PdfArrayObject> _showArrays = new List<PdfArrayObject>();
        private PdfObject _element;
        private PdfBuffer _buffer;

        [GlobalSetup]
        public void Setup()
        {
            var pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
            _file = PdfFile.Open(pdfPath);
            _file.Initialize();

            _document = PdfDocument.OpenFile(_file);

            // Full upgrade lets the setup dispatch on the concrete operation types
            LibraryInstance.UpgradePolicy = UpgradePolicy.Full;

            using var catalog = _document.GetCatalog();
            using var tree = catalog.GetPages();
            ulong pageCount = tree.GetPageCount();
            for (ulong i = 1; i <= pageCount; i++) {
                CollectShowArrays(tree, i);
            }

            if (_showArrays.Count == 0) {
                throw new InvalidOperationException($"No TJ operations found in {PdfFileName}");
            }

            LibraryInstance.UpgradePolicy = UpgradePolicy.Single;

            _element = FindFirstString();
            using var text = PdfStringObject.FromObject(_element);
            _buffer = text.Value;
        }

        private PdfObject FindFirstString()
        {
            foreach (var array in _showArrays) {
                ulong size = array.GetSize();
                for (ulong i = 0; i < size; i++) {
                    var item = array.GetValue(i);
                    if (item.GetObjectType() == PdfObjectType.String) {
                        return item;
                    }
                    item.Dispose();
                }
            }

            throw new InvalidOperationException($"No string element found in TJ operations of {PdfFileName}");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _buffer?.Dispose();
            _element?.Dispose();
            foreach (var array in _showArrays) {
                array.Dispose();
            }
            _document?.Dispose();
            _file?.Dispose();
        }

        /// <summary>
        /// Reads every TJ element the way a renderer does: type check, typed conversion, value.
        /// </summary>
        [Benchmark(Baseline = true)]
        public long WalkShowArrays()
        {
            long total = 0;
            foreach (var array in _showArrays) {
                ulong size = array.GetSize();
                for (ulong i = 0; i < size; i++) {
                    total += ReadElement(array, i);
                }
            }
            return total;
        }

        [Benchmark]
        public PdfObjectType Object_GetObjectType()
        {
            return _element.GetObjectType();
        }

        [Benchmark]
        public byte[] Buffer_GetData()
        {
            return _buffer.Data;
        }

        private void CollectShowArrays(PdfPageTree tree, ulong pageNumber)
        {
            using var page = tree.GetPage(pageNumber);
            using var contents = page.GetContents();
            using var instructions = contents.GetInstructionCollection();

            ulong instructionCount = instructions.GetInstructionsSize();
            for (ulong i = 0; i < instructionCount; i++) {
                using var instruction = instructions.At(i);
                if (instruction is PdfContentObjectText textObject) {
                    CollectShowArrays(textObject);
                }
            }
        }

        private void CollectShowArrays(PdfContentObjectText textObject)
        {
            ulong operationCount = textObject.GetOperationsSize();
            for (ulong i = 0; i < operationCount; i++) {
                using var operation = textObject.GetOperationAt(i);
                if (operation is PdfContentOperationTextShowArray showArray) {
                    _showArrays.Add(showArray.Value);
                }
            }
        }

        private static long ReadElement(PdfArrayObject array, ulong index)
        {
            using var item = array.GetValue(index);

            var objectType = item.GetObjectType();
            if (objectType == PdfObjectType.String) {
                using var text = PdfStringObject.FromObject(item);
                using var buffer = text.Value;
                return buffer.Data.Length;
            }

            if (objectType == PdfObjectType.Integer) {
                using var integer = PdfIntegerObject.FromObject(item);
                return integer.IntegerValue;
            }

            if (objectType == PdfObjectType.Real) {
                using var real = PdfRealObject.FromObject(item);
                return (long)real.Value;
            }

            return 0;
        }
    }
}
