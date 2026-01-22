using System;
using vanillapdf.net.Interop;
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

#if DEBUG_REFCOUNT
        internal void AddRef()
        {
            UInt32 result = NativeMethods.IUnknown_AddRef(UnknownHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        internal void Release()
        {
            UInt32 result = NativeMethods.IUnknown_Release(UnknownHandle.DangerousGetHandle());
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }
#endif

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

    }
}
