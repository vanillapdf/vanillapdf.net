using System;
using System.Runtime.InteropServices;
using System.Threading;
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

        protected bool _disposed = false;

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            UnknownHandle = handle;

            Interlocked.Increment(ref _counter);
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

        /// <summary>
        /// Release all unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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

        protected virtual void DecrementCounter()
        {
            Interlocked.Decrement(ref _counter);
        }

        protected virtual void DisposeCustomHandle()
        {
            UnknownHandle?.Dispose();
        }

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
