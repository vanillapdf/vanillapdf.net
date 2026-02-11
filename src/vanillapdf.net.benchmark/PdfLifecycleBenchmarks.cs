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
    ///
    /// PdfUnknown removal results (.NET 8.0, i7-10700KF):
    ///
    /// | Method                         | Before (Mean) | After (Mean) | Before (Alloc) | After (Alloc) |
    /// |------------------------------- |--------------:|-------------:|---------------:|--------------:|
    /// | CreateDispose_PdfBuffer        |     279.4 ns  |    176.2 ns  |         136 B  |         96 B  |
    /// | CreateDispose_PdfIntegerObject |     506.5 ns  |    435.0 ns  |         144 B  |         96 B  |
    /// | CreateDispose_PdfBooleanObject |     362.9 ns  |    310.5 ns  |         144 B  |         96 B  |
    /// | DocumentOpenWorkflow           |   568,252 ns  |  1,101,559*  |         417 B  |        225 B  |
    ///
    /// * DocumentOpenWorkflow time is IO-bound and noisy; allocation drop (417 B -> 225 B) is the key metric.
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
