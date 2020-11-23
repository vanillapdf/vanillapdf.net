using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Class for supporting complex file serialization features
    /// </summary>
    public class PdfFileWriter : PdfUnknown
    {
        internal PdfFileWriter(PdfFileWriterSafeHandle handle) : base(handle)
        {
        }

        static PdfFileWriter()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
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

        private static class NativeMethods
        {
            public static CreateDelgate FileWriter_Create = LibraryInstance.GetFunction<CreateDelgate>("FileWriter_Create");
            public static WriteDelgate FileWriter_Write = LibraryInstance.GetFunction<WriteDelgate>("FileWriter_Write");
            public static WriteIncrementalDelgate FileWriter_WriteIncremental = LibraryInstance.GetFunction<WriteIncrementalDelgate>("FileWriter_WriteIncremental");
            public static SubscribeDelgate FileWriter_Subscribe = LibraryInstance.GetFunction<SubscribeDelgate>("FileWriter_Subscribe");
            public static UnsubscribeDelgate FileWriter_Unsubscribe = LibraryInstance.GetFunction<UnsubscribeDelgate>("FileWriter_Unsubscribe");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfFileWriterSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteDelgate(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteIncrementalDelgate(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SubscribeDelgate(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 UnsubscribeDelgate(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);
        }
    }
}
