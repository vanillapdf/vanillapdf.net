using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Class for supporting complex file serialization features
    /// </summary>
    public class PdfFileWriter : IDisposable
    {
        internal PdfFileWriterSafeHandle Handle { get; }

        internal PdfFileWriter(PdfFileWriterSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfFileWriter with default value
        /// </summary>
        /// <returns>New instance of \ref PdfFileWriter on success, throws exception on failure</returns>
        public static PdfFileWriter Create()
        {
            UInt32 result = NativeMethods.FileWriter_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileWriter(data);
        }

        /// <summary>
        /// Writes contents of a source file into destination file
        /// </summary>
        /// <param name="source">Handle to file to be serialized</param>
        /// <param name="destination">Handle to file which contents will be overwritten by the source file</param>
        public void Write(PdfFile source, PdfFile destination)
        {
            UInt32 result = NativeMethods.FileWriter_Write(Handle, source.Handle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Writes contents of a source file into destination file using incremental strategy
        /// </summary>
        /// <param name="source">Handle to file to be serialized</param>
        /// <param name="destination">Handle to file which contents will be overwritten by the source file</param>
        public void WriteIncremental(PdfFile source, PdfFile destination)
        {
            UInt32 result = NativeMethods.FileWriter_WriteIncremental(Handle, source.Handle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Register a write observer to receive write notifications
        /// </summary>
        /// <param name="observer">Handle to write observer</param>
        public void Subscribe(PdfFileWriterObserver observer)
        {
            UInt32 result = NativeMethods.FileWriter_Subscribe(Handle, observer.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Unregister a write observer to receive write notifications
        /// </summary>
        /// <param name="observer">Handle to write observer</param>
        public void Unsubscribe(PdfFileWriterObserver observer)
        {
            UInt32 result = NativeMethods.FileWriter_Unsubscribe(Handle, observer.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
