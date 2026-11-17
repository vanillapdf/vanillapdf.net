using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.benchmark
{
    /// <summary>
    /// Measures what [SuppressGCTransition] saves on individual native calls.
    /// Each native function is declared twice, with and without the attribute, and called
    /// with raw pointers so SafeHandle allocation and marshalling do not mask the difference.
    /// Both variants run in the same process, which removes run-to-run drift.
    /// </summary>
    [MemoryDiagnoser]
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class PdfInteropTransitionBenchmarks
    {
        private PdfObject _element;
        private IntPtr _objectPtr;
        private IntPtr _stringPtr;

        [GlobalSetup]
        public void Setup()
        {
            // Any string object will do; the benchmarks only convert it
            _element = PdfLiteralStringObject.CreateFromDecodedString("benchmark");
            _objectPtr = _element.ObjectHandle.DangerousGetHandle();

            if (Native.StringObject_FromObject(_objectPtr, out _stringPtr) != 0) {
                throw new InvalidOperationException("StringObject_FromObject failed during setup");
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            Native.StringObject_Release(_stringPtr);
            _element?.Dispose();
        }

        #region Control: field getter

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("GetObjectType")]
        public UInt32 GetObjectType_Transition()
        {
            Native.Object_GetObjectType(_objectPtr, out var type);
            return type;
        }

        [Benchmark]
        [BenchmarkCategory("GetObjectType")]
        public UInt32 GetObjectType_Suppressed()
        {
            Native.Object_GetObjectTypeSuppressed(_objectPtr, out var type);
            return type;
        }

        #endregion

        #region Conversions (each followed by an unmarked release)

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("FromObject")]
        public IntPtr FromObject_Transition()
        {
            Native.StringObject_FromObject(_objectPtr, out var result);
            Native.StringObject_Release(result);
            return result;
        }

        [Benchmark]
        [BenchmarkCategory("FromObject")]
        public IntPtr FromObject_Suppressed()
        {
            Native.StringObject_FromObjectSuppressed(_objectPtr, out var result);
            Native.StringObject_Release(result);
            return result;
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("ToObject")]
        public IntPtr ToObject_Transition()
        {
            Native.StringObject_ToObject(_stringPtr, out var result);
            Native.Object_Release(result);
            return result;
        }

        [Benchmark]
        [BenchmarkCategory("ToObject")]
        public IntPtr ToObject_Suppressed()
        {
            Native.StringObject_ToObjectSuppressed(_stringPtr, out var result);
            Native.Object_Release(result);
            return result;
        }

        #endregion

        private static class Native
        {
            private const string LibraryName = "libvanillapdf";

            [DllImport(LibraryName, EntryPoint = "Object_GetObjectType")]
            public static extern UInt32 Object_GetObjectType(IntPtr handle, out UInt32 type);

            [DllImport(LibraryName, EntryPoint = "Object_GetObjectType")]
            [SuppressGCTransition]
            public static extern UInt32 Object_GetObjectTypeSuppressed(IntPtr handle, out UInt32 type);

            [DllImport(LibraryName, EntryPoint = "StringObject_FromObject")]
            public static extern UInt32 StringObject_FromObject(IntPtr handle, out IntPtr result);

            [DllImport(LibraryName, EntryPoint = "StringObject_FromObject")]
            [SuppressGCTransition]
            public static extern UInt32 StringObject_FromObjectSuppressed(IntPtr handle, out IntPtr result);

            [DllImport(LibraryName, EntryPoint = "StringObject_ToObject")]
            public static extern UInt32 StringObject_ToObject(IntPtr handle, out IntPtr result);

            [DllImport(LibraryName, EntryPoint = "StringObject_ToObject")]
            [SuppressGCTransition]
            public static extern UInt32 StringObject_ToObjectSuppressed(IntPtr handle, out IntPtr result);

            [DllImport(LibraryName)]
            public static extern UInt32 StringObject_Release(IntPtr handle);

            [DllImport(LibraryName)]
            public static extern UInt32 Object_Release(IntPtr handle);
        }
    }
}
