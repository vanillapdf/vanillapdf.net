using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for GetAsDerivedObject which is called on every dictionary/array access.
    /// This measures the impact of caching GetObjectType.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfObjectTypeBenchmarks
    {
        private const string PdfFileName = "Resources/minimalist.pdf";

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
        /// Traverse page tree - each GetPage calls GetAsDerivedObject internally.
        /// </summary>
        [Benchmark]
        public int TraversePages()
        {
            int count = 0;
            var pageCount = _pageTree.GetPageCount();
            for (ulong i = 1; i <= pageCount; i++) {
                using var page = _pageTree.GetPage(i);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Iterate dictionary entries - each access calls GetAsDerivedObject.
        /// </summary>
        [Benchmark]
        public int IterateDictionary()
        {
            int count = 0;
            foreach (var kvp in _trailerDictionary) {
                kvp.Key.Dispose();
                kvp.Value.Dispose();
                count++;
            }
            return count;
        }
    }
}
