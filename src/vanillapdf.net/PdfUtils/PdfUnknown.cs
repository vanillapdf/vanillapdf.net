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
        internal static int Counter { get; private set; }

        internal PdfUnknownSafeHandle UnknownHandle { get; }

        protected bool _disposed = false;

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            UnknownHandle = handle;
            Counter++;
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
                UnknownHandle?.Dispose();
                DisposeCustomHandle();
            }

            _disposed = true;
            Counter--;
        }

        protected virtual void DisposeCustomHandle()
        {
            
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
