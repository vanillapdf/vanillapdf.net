using System;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// File writer observer context is used for transferring user data through file serialization process
    /// </summary>
    public abstract class PdfFileWriterObserverContext : IDisposable
    {
        /// <summary>
        /// Allocates a new handle to be used as a callback parameter
        /// </summary>
        public PdfFileWriterObserverContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        internal GCHandle Handle { get; }

        /// <summary>
        /// Callback function is used to initialize necessary user-specific data
        /// </summary>
        /// <param name="data">Handle to destination stream</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnInitializing(PdfInputOutputStream data);

        /// <summary>
        /// Callback function is used to cleanup necessary user-specific data
        /// </summary>
        /// <param name="data">Handle to destination stream</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnFinalizing(PdfInputOutputStream data);

        /// <summary>
        /// Callback function is called before writing every indirect object
        /// </summary>
        /// <param name="data">Handle to object currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnBeforeObjectWrite(PdfObject data);

        /// <summary>
        /// Callback function is called after writing every indirect object
        /// </summary>
        /// <param name="data">Handle to object currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnAfterObjectWrite(PdfObject data);

        /// <summary>
        /// Callback function is called before adjusting offset of indirect object
        /// </summary>
        /// <param name="data">Handle to object currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnBeforeObjectOffsetRecalculation(PdfObject data);

        /// <summary>
        /// Callback function is called after adjusting offset of indirect object
        /// </summary>
        /// <param name="data">Handle to object currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnAfterObjectOffsetRecalculation(PdfObject data);

        /// <summary>
        /// Callback function is called before adjusting offset of cross-reference entry
        /// </summary>
        /// <param name="data">Handle to cross-reference entry currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnBeforeEntryOffsetRecalculation(PdfXrefEntry data);

        /// <summary>
        /// Callback function is called after adjusting offset of cross-reference entry
        /// </summary>
        /// <param name="data">Handle to cross-reference entry currently being serialized</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnAfterEntryOffsetRecalculation(PdfXrefEntry data);

        /// <summary>
        /// Callback function is called before flushing data into the outputstream
        /// </summary>
        /// <param name="data">Handle to destination stream</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnBeforeOutputFlush(PdfInputOutputStream data);

        /// <summary>
        /// Callback function is called after flushing data into the outputstream
        /// </summary>
        /// <param name="data">Handle to destination stream</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the file serialization will be terminated</returns>
        public abstract UInt32 OnAfterOutputFlush(PdfInputOutputStream data);

        private void ReleaseUnmanagedResources()
        {
            if (Handle.IsAllocated) {
                Handle.Free();
            }
        }

        /// <summary>
        /// Release all managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor for <c>PdfFileWriterObserverContext</c>.
        /// Ensures that all unmanaged resources held by this context are properly released.
        /// </summary>
        /// <remarks>
        /// Invokes <see cref="ReleaseUnmanagedResources"/> to clean up native handles,
        /// memory allocations, or file streams before the object is reclaimed.
        /// </remarks>
        ~PdfFileWriterObserverContext()
        {
            ReleaseUnmanagedResources();
        }
    }
}
