using System;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net
{
    public class PdfUnknown : IDisposable
    {
        internal PdfUnknownSafeHandle Handle { get; }

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            // Hook for derived classes
            ReleaseManagedResources();

            GC.SuppressFinalize(this);
        }

        protected virtual void ReleaseManagedResources()
        {
            Handle.Dispose();
        }
    }
}
