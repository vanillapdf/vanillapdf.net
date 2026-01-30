using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for PDF object access patterns: Find, TryFind, GetValue, GetValueAs.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfObjectTypeBenchmarks
    {
        private const string PdfFileName = "Resources/minimalist.pdf";

        private PdfFile _file;
        private PdfDocument _document;
        private PdfDictionaryObject _trailerDictionary;
        private PdfArrayObject _testArray;
        private PdfNameObject _sizeKey;

        [GlobalSetup]
        public void Setup()
        {
            var pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
            _file = PdfFile.Open(pdfPath);
            _file.Initialize();

            _document = PdfDocument.OpenFile(_file);

            // Get first trailer dictionary
            using var xrefChain = _file.XrefChain;
            foreach (var xref in xrefChain) {
                _trailerDictionary = xref.GetTrailerDictionary();
                xref.Dispose();
                break;
            }

            // Create key for repeated lookups
            _sizeKey = PdfNameObject.Create();
            _sizeKey.Value.StringData = "Size";

            // Create test array with integer value
            _testArray = PdfArrayObject.Create();
            using (var intObj = PdfIntegerObject.Create()) {
                intObj.IntegerValue = 100;
                _testArray.Append(intObj);
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _testArray?.Dispose();
            _sizeKey?.Dispose();
            _trailerDictionary?.Dispose();
            _document?.Dispose();
            _file?.Dispose();
        }

        #region Dictionary Operations

        [Benchmark(Baseline = true)]
        public PdfObject Dictionary_Find()
        {
            var obj = _trailerDictionary.Find(_sizeKey);
            obj.Dispose();
            return obj;
        }

        [Benchmark]
        public PdfObject Dictionary_TryFind()
        {
            _trailerDictionary.TryFind(_sizeKey, out var obj);
            obj?.Dispose();
            return obj;
        }

        [Benchmark]
        public PdfIntegerObject Dictionary_FindAs()
        {
            var obj = _trailerDictionary.FindAs<PdfIntegerObject>(_sizeKey);
            obj.Dispose();
            return obj;
        }

        [Benchmark]
        public PdfIntegerObject Dictionary_TryFindAs()
        {
            _trailerDictionary.TryFindAs<PdfIntegerObject>(_sizeKey, out var obj);
            obj?.Dispose();
            return obj;
        }

        #endregion

        #region Array Operations

        [Benchmark]
        public PdfObject Array_GetValue()
        {
            var obj = _testArray.GetValue(0);
            obj.Dispose();
            return obj;
        }

        [Benchmark]
        public PdfIntegerObject Array_GetValueAs()
        {
            var obj = _testArray.GetValueAs<PdfIntegerObject>(0);
            obj.Dispose();
            return obj;
        }

        #endregion
    }
}
