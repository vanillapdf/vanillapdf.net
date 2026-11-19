using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Benchmarks for public API calls backed by trivial native functions:
    /// type getters and plain value getters and setters.
    /// Each benchmark creates and releases only the object it uses.
    /// Run on main and on a branch to compare the cost of the native calls.
    /// </summary>
    /// <remarks>
    /// Results without and with [SuppressGCTransition] (2026-11-19, native 2.3.0,
    /// .NET 10.0.15, Release, Intel Core i7-10700KF, Windows 11 25H2):
    ///
    /// | Benchmark                             | Without  | With     | Change |
    /// |---------------------------------------|----------|----------|--------|
    /// | Object_GetObjectType                  | 18.3 ns  | 14.8 ns  | -19%   |
    /// | BooleanObject_GetValue                | 17.5 ns  | 13.4 ns  | -24%   |
    /// | Date_GetYear                          | 16.6 ns  | 13.0 ns  | -21%   |
    /// | ImageMetadataObjectAttribute_SetWidth | 16.7 ns  | 13.4 ns  | -19%   |
    /// </remarks>
    [MemoryDiagnoser]
    public class PdfTrivialCallBenchmarks
    {
        #region Object

        private PdfObject _object;

        [GlobalSetup(Target = nameof(Object_GetObjectType))]
        public void SetupObject()
        {
            _object = PdfLiteralStringObject.CreateFromDecodedString("benchmark");
        }

        [Benchmark(Baseline = true)]
        public PdfObjectType Object_GetObjectType()
        {
            return _object.GetObjectType();
        }

        [GlobalCleanup(Target = nameof(Object_GetObjectType))]
        public void CleanupObject()
        {
            _object?.Dispose();
        }

        #endregion

        #region Boolean object

        private PdfBooleanObject _booleanObject;

        [GlobalSetup(Target = nameof(BooleanObject_GetValue))]
        public void SetupBooleanObject()
        {
            _booleanObject = PdfBooleanObject.Create();
        }

        [Benchmark]
        public bool BooleanObject_GetValue()
        {
            return _booleanObject.Value;
        }

        [GlobalCleanup(Target = nameof(BooleanObject_GetValue))]
        public void CleanupBooleanObject()
        {
            _booleanObject?.Dispose();
        }

        #endregion

        #region Date

        private PdfDate _date;

        [GlobalSetup(Target = nameof(Date_GetYear))]
        public void SetupDate()
        {
            _date = PdfDate.CreateCurrent();
        }

        [Benchmark]
        public int Date_GetYear()
        {
            return _date.Year;
        }

        [GlobalCleanup(Target = nameof(Date_GetYear))]
        public void CleanupDate()
        {
            _date?.Dispose();
        }

        #endregion

        #region Image metadata

        private PdfImageMetadataObjectAttribute _imageMetadata;

        [GlobalSetup(Target = nameof(ImageMetadataObjectAttribute_SetWidth))]
        public void SetupImageMetadata()
        {
            _imageMetadata = PdfImageMetadataObjectAttribute.Create();
        }

        [Benchmark]
        public void ImageMetadataObjectAttribute_SetWidth()
        {
            _imageMetadata.Width = 100;
        }

        [GlobalCleanup(Target = nameof(ImageMetadataObjectAttribute_SetWidth))]
        public void CleanupImageMetadata()
        {
            _imageMetadata?.Dispose();
        }

        #endregion
    }
}
