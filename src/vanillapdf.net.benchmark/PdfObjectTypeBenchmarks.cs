using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks comparing auto-upgrade vs explicit upgrade costs.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfObjectTypeBenchmarks
    {
        private const string PdfFileName = "Resources/minimalist.pdf";
        private const int Iterations = 1000;

        private PdfFile _file;
        private PdfDocument _document;
        private PdfCatalog _catalog;
        private PdfPageTree _pageTree;
        private PdfDictionaryObject _trailerDictionary;

        [GlobalSetup]
        public void Setup()
        {
            LibraryInstance.Initialize();

            var pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
            _file = PdfFile.Open(pdfPath);
            _file.Initialize();

            _document = PdfDocument.OpenFile(_file);
            _catalog = _document.GetCatalog();
            _pageTree = _catalog.GetPages();

            // Get first trailer dictionary for dictionary iteration benchmark
            using var xrefChain = _file.XrefChain;
            foreach (var xref in xrefChain) {
                _trailerDictionary = xref.GetTrailerDictionary();
                xref.Dispose();
                break;
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _trailerDictionary?.Dispose();
            _pageTree?.Dispose();
            _catalog?.Dispose();
            _document?.Dispose();
            _file?.Dispose();
        }

        /// <summary>
        /// Baseline: Return base PdfObject without upgrading.
        /// </summary>
        [Benchmark(Baseline = true)]
        public int DictionaryAccess_NoUpgrade()
        {
            int count = 0;
            for (int i = 0; i < Iterations; i++) {
                using var obj = _trailerDictionary.Find("Size");
                count++;
            }
            return count;
        }

        /// <summary>
        /// Call Upgrade() explicitly after access.
        /// </summary>
        [Benchmark]
        public int DictionaryAccess_ExplicitUpgrade()
        {
            int count = 0;
            for (int i = 0; i < Iterations; i++) {
                using var obj = _trailerDictionary.Find("Size");
                using var upgraded = obj.Upgrade();
                count++;
            }
            return count;
        }

        /// <summary>
        /// Auto-upgrade on every access (old behavior).
        /// </summary>
        [Benchmark]
        public int DictionaryAccess_AutoUpgrade()
        {
            int count = 0;
            for (int i = 0; i < Iterations; i++) {
                using var obj = _trailerDictionary.Find("Size");
                using var upgraded = PdfObject.GetAsDerivedObject(obj);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Is/As pattern: check type, then convert if match.
        /// </summary>
        [Benchmark]
        public int IsAsPattern()
        {
            int count = 0;
            for (int i = 0; i < Iterations; i++) {
                using var obj = _trailerDictionary.Find("Size");
                if (obj.IsInteger()) {
                    using var intObj = obj.AsInteger();
                    count++;
                }
            }
            return count;
        }
    }
}
