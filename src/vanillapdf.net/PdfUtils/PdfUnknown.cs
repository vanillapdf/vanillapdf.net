using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Serves as a base class all interface objects
    /// </summary>
    public class PdfUnknown : IDisposable
    {
        internal PdfUnknownSafeHandle Handle { get; }

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Release all unmanaged resources
        /// </summary>
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
