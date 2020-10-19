using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfFileWriter : PdfUnknown
    {
        internal PdfFileWriter(PdfFileWriterSafeHandle handle) : base(handle)
        {
        }

        static PdfFileWriter()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfFileWriter Create()
        {
            UInt32 result = NativeMethods.FileWriter_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileWriter(data);
        }

        public void Write(PdfFile source, PdfFile destination)
        {
            UInt32 result = NativeMethods.FileWriter_Write(Handle, source.Handle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void WriteIncremental(PdfFile source, PdfFile destination)
        {
            UInt32 result = NativeMethods.FileWriter_WriteIncremental(Handle, source.Handle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Subscribe(PdfFileWriterObserver observer)
        {
            UInt32 result = NativeMethods.FileWriter_Subscribe(Handle, observer.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

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
