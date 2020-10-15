using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfFile : PdfUnknown
    {
        internal PdfFile(PdfFileSafeHandle handle) : base(handle)
        {
        }

        static PdfFile()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfFile Open(string filename)
        {
            UInt32 result = NativeMethods.File_Open(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        public static PdfFile OpenStream(PdfInputOutputStream stream, string filename)
        {
            UInt32 result = NativeMethods.File_OpenStream(stream.Handle, filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        public static PdfFile Create(string filename)
        {
            UInt32 result = NativeMethods.File_Create(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        public static PdfFile CreateStream(PdfInputOutputStream stream, string name)
        {
            UInt32 result = NativeMethods.File_CreateStream(stream.Handle, name, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        public void Initialize()
        {
            UInt32 result = NativeMethods.File_Initialize(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool IsEncrypted()
        {
            UInt32 result = NativeMethods.File_IsEncrypted(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public bool SetEncryptionPassword(string password)
        {
            UInt32 result = NativeMethods.File_SetEncryptionPassword(Handle, password);
            if (result == PdfReturnValues.ERROR_INVALID_PASSWORD) {
                return false;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetErrorException(result);
            }

            return true;
        }

        public PdfXrefChain GetXrefChain()
        {
            UInt32 result = NativeMethods.File_XrefChain(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefChain(data);
        }

        private static class NativeMethods
        {
            public static FileOpenDelgate File_Open = LibraryInstance.GetFunction<FileOpenDelgate>("File_Open");
            public static FileOpenStreamDelgate File_OpenStream = LibraryInstance.GetFunction<FileOpenStreamDelgate>("File_OpenStream");
            public static FileCreateDelgate File_Create = LibraryInstance.GetFunction<FileCreateDelgate>("File_Create");
            public static FileCreateStreamDelgate File_CreateStream = LibraryInstance.GetFunction<FileCreateStreamDelgate>("File_CreateStream");
            public static FileInitializeDelgate File_Initialize = LibraryInstance.GetFunction<FileInitializeDelgate>("File_Initialize");
            public static FileIsEncryptedDelgate File_IsEncrypted = LibraryInstance.GetFunction<FileIsEncryptedDelgate>("File_IsEncrypted");
            public static FileSetEncryptionPasswordDelgate File_SetEncryptionPassword = LibraryInstance.GetFunction<FileSetEncryptionPasswordDelgate>("File_SetEncryptionPassword");

            public static FileXrefChainDelgate File_XrefChain = LibraryInstance.GetFunction<FileXrefChainDelgate>("File_XrefChain");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileOpenDelgate(string filename, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileOpenStreamDelgate(PdfInputOutputStreamSafeHandle input_stream, string name, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileCreateDelgate(string filename, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileCreateStreamDelgate(PdfInputOutputStreamSafeHandle input_stream, string name, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileInitializeDelgate(PdfFileSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileIsEncryptedDelgate(PdfFileSafeHandle handle, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileSetEncryptionPasswordDelgate(PdfFileSafeHandle handle, string password);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileXrefChainDelgate(PdfFileSafeHandle handle, out PdfXrefChainSafeHandle data);
        }
    }
}
