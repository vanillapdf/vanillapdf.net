using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for object creation + disposal lifecycle costs.
    /// Measures SafeHandle allocations and P/Invoke overhead at each hierarchy level.
    /// </summary>
    [MemoryDiagnoser]
    public class PdfLifecycleBenchmarks
    {
        private const string PdfFileName = "Resources/minimalist.pdf";
        private string _pdfPath;

        [GlobalSetup]
        public void Setup()
        {
            _pdfPath = Path.Combine(AppContext.BaseDirectory, PdfFileName);
        }

        /// <summary>
        /// Level 1: PdfBuffer.Create() + Dispose.
        /// Single SafeHandle allocation.
        /// </summary>
        [Benchmark]
        public void CreateDispose_PdfBuffer()
        {
            using var buffer = PdfBuffer.Create();
        }

        /// <summary>
        /// Level 2: PdfIntegerObject.Create() + Dispose.
        /// One conversion: ToObject.
        /// </summary>
        [Benchmark]
        public void CreateDispose_PdfIntegerObject()
        {
            using var obj = PdfIntegerObject.Create();
        }

        /// <summary>
        /// Level 2: PdfBooleanObject.Create() + Dispose.
        /// One conversion: ToObject.
        /// </summary>
        [Benchmark]
        public void CreateDispose_PdfBooleanObject()
        {
            using var obj = PdfBooleanObject.Create();
        }

        /// <summary>
        /// Document open workflow: Open → Initialize → OpenDocument → GetCatalog → GetPages → Dispose all.
        /// Multiple objects created and disposed.
        /// </summary>
        [Benchmark]
        public void DocumentOpenWorkflow()
        {
            using var file = PdfFile.Open(_pdfPath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();
        }
    }
}
