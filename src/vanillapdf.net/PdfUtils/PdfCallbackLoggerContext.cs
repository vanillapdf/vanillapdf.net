using System;
using System.Runtime.InteropServices;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PdfCallbackLoggerContext : IDisposable
    {
        internal GCHandle Handle { get; }

        /// <summary>
        /// Allocates a new handle to be used as a callback parameter
        /// </summary>
        public PdfCallbackLoggerContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="payload"></param>
        public abstract void SinkLog(PdfLoggingSeverity severity, string payload);

        /// <summary>
        /// 
        /// </summary>
        public abstract void SinkFlush();

        /// <summary>
        /// Release all managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            if (Handle.IsAllocated) {
                Handle.Free();
            }
        }

        /// <summary>
        /// Finalizer of the class, ensures the proper release of resources
        /// </summary>
        ~PdfCallbackLoggerContext()
        {
            ReleaseUnmanagedResources();
        }
    }
}
