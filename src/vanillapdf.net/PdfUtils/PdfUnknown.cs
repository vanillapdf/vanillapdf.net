using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Serves as a base class all interface objects
    /// </summary>
    public class PdfUnknown : IDisposable
    {
        internal static int _counter;
        internal static int Counter { get => _counter; private set => _counter = value; }

        internal PdfUnknownSafeHandle UnknownHandle { get; }

        private protected bool _disposed = false;

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            UnknownHandle = handle;
            IncrementCounter();
        }

        internal void AddRef()
        {
            UInt32 result = NativeMethods.IUnknown_AddRef(UnknownHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        internal void Release()
        {
            UInt32 result = NativeMethods.IUnknown_Release(UnknownHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private protected virtual void Dispose(bool disposing)
        {
            if (_disposed) {
                return;
            }

            if (disposing) {
                DisposeCustomHandle();
            }

            _disposed = true;
            DecrementCounter();
        }

        private void IncrementCounter()
        {
#if DEBUG || TRACE_SAFE_HANDLES
            System.Threading.Interlocked.Increment(ref _counter);
#endif
        }

        private void DecrementCounter()
        {
#if DEBUG || TRACE_SAFE_HANDLES
            System.Threading.Interlocked.Decrement(ref _counter);
#endif
        }

        private protected virtual void DisposeCustomHandle()
        {
            UnknownHandle?.Dispose();
        }

        /// <summary>
        /// Finalizer of the class, ensures the proper release of resources
        /// </summary>
        ~PdfUnknown()
        {
            Dispose(false);
        }

        private static class NativeMethods
        {
            public static AddRefDelegate IUnknown_AddRef = LibraryInstance.GetFunction<AddRefDelegate>("IUnknown_AddRef");
            public static ReleaseRefDelegate IUnknown_Release = LibraryInstance.GetFunction<ReleaseRefDelegate>("IUnknown_Release");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AddRefDelegate(PdfUnknownSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ReleaseRefDelegate(PdfUnknownSafeHandle handle);
        }
    }
}
