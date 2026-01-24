using System;
using BenchmarkDotNet.Attributes;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.benchmark
{
    [MemoryDiagnoser]
    public class PdfBufferBenchmarks
    {
        private byte[] _smallData;
        private byte[] _mediumData;
        private byte[] _largeData;
        private PdfBuffer _smallBuffer;
        private PdfBuffer _mediumBuffer;
        private PdfBuffer _largeBuffer;

        private string _smallString;
        private string _mediumString;
        private PdfBuffer _smallStringBuffer;
        private PdfBuffer _mediumStringBuffer;

        [GlobalSetup]
        public void Setup()
        {

            // Create test data of various sizes
            _smallData = new byte[100];
            _mediumData = new byte[10_000];
            _largeData = new byte[1_000_000];

            var random = new Random(42);
            random.NextBytes(_smallData);
            random.NextBytes(_mediumData);
            random.NextBytes(_largeData);

            // Pre-create buffers for GetData benchmarks
            _smallBuffer = PdfBuffer.CreateFromData(_smallData);
            _mediumBuffer = PdfBuffer.CreateFromData(_mediumData);
            _largeBuffer = PdfBuffer.CreateFromData(_largeData);

            // String test data
            _smallString = new string('A', 100);
            _mediumString = new string('B', 10_000);

            _smallStringBuffer = PdfBuffer.Create();
            _smallStringBuffer.StringData = _smallString;

            _mediumStringBuffer = PdfBuffer.Create();
            _mediumStringBuffer.StringData = _mediumString;
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _smallBuffer?.Dispose();
            _mediumBuffer?.Dispose();
            _largeBuffer?.Dispose();
            _smallStringBuffer?.Dispose();
            _mediumStringBuffer?.Dispose();
        }

        // CreateFromData benchmarks
        [Benchmark]
        public PdfBuffer CreateFromData_Small()
        {
            var buffer = PdfBuffer.CreateFromData(_smallData);
            buffer.Dispose();
            return buffer;
        }

        [Benchmark]
        public PdfBuffer CreateFromData_Medium()
        {
            var buffer = PdfBuffer.CreateFromData(_mediumData);
            buffer.Dispose();
            return buffer;
        }

        [Benchmark]
        public PdfBuffer CreateFromData_Large()
        {
            var buffer = PdfBuffer.CreateFromData(_largeData);
            buffer.Dispose();
            return buffer;
        }

        // GetData benchmarks
        [Benchmark]
        public byte[] GetData_Small()
        {
            return _smallBuffer.Data;
        }

        [Benchmark]
        public byte[] GetData_Medium()
        {
            return _mediumBuffer.Data;
        }

        [Benchmark]
        public byte[] GetData_Large()
        {
            return _largeBuffer.Data;
        }

        // StringData benchmarks
        [Benchmark]
        public string GetStringData_Small()
        {
            return _smallStringBuffer.StringData;
        }

        [Benchmark]
        public string GetStringData_Medium()
        {
            return _mediumStringBuffer.StringData;
        }

        [Benchmark]
        public void SetStringData_Small()
        {
            _smallStringBuffer.StringData = _smallString;
        }

        [Benchmark]
        public void SetStringData_Medium()
        {
            _mediumStringBuffer.StringData = _mediumString;
        }
    }
}
