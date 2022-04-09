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
        internal PdfUnknownSafeHandle Handle { get; }

        private bool _disposed = false;

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            Handle = handle;
        }

        ~PdfUnknown()
        {
            Dispose(false);
        }

        public void AddRef()
        {
            UInt32 result = NativeMethods.IUnknown_AddRef(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Release()
        {
            UInt32 result = NativeMethods.IUnknown_Release(Handle);
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
                Handle.Dispose();
            }

            _disposed = true;
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
