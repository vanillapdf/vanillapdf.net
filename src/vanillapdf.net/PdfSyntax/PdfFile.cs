﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents low-level interface for manipulating file structure
    /// </summary>
    public class PdfFile : PdfUnknown
    {
        internal PdfFileSafeHandle Handle { get; }

        internal PdfFile(PdfFileSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfFile()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFileSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get version of PDF standard the document is conforming to
        /// </summary>
        public PdfVersion Version
        {
            get { return GetVersion(); }
        }

        /// <summary>
        /// Get the name of the current file on the physical filesystem
        /// </summary>
        public string Filename
        {
            get { return GetFilename(); }
        }

        /// <summary>
        /// Determines if the file is encrypted
        /// </summary>
        public bool Encrypted
        {
            get { return IsEncrypted(); }
        }

        /// <summary>
        /// Cross-reference chain contains list of all indirect objects
        /// </summary>
        public PdfXrefChain XrefChain
        {
            get { return GetXrefChain(); }
        }

        /// <summary>
        /// Opens an existing file from the specified location
        /// </summary>
        /// <param name="filename">path to existing file location</param>
        /// <returns>A new \ref PdfFile instance on success, throws exception on failure</returns>
        public static PdfFile Open(string filename)
        {
            UInt32 result = NativeMethods.File_Open(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        /// <summary>
        /// Open file from existing \ref PdfUtils.PdfInputOutputStream containing a valid PDF document structure
        /// </summary>
        /// <param name="stream">Stream containing a valid PDF document structure</param>
        /// <param name="name">Filename for logging purposes</param>
        /// <returns>A new \ref PdfFile instance on success, throws exception on failure</returns>
        public static PdfFile OpenStream(PdfInputOutputStream stream, string name)
        {
            UInt32 result = NativeMethods.File_OpenStream(stream.Handle, name, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        /// <summary>
        /// Creates a file for writing.
        /// Truncates the contents if it already exists.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static PdfFile Create(string filename)
        {
            UInt32 result = NativeMethods.File_Create(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        /// <summary>
        /// Creates a new file content in the specified \ref PdfUtils.PdfInputOutputStream
        /// </summary>
        /// <param name="stream">Stream to receive data when the file is saved</param>
        /// <param name="name">Filename for logging purposes</param>
        /// <returns>A new \ref PdfFile instance on success, throws exception on failure</returns>
        public static PdfFile CreateStream(PdfInputOutputStream stream, string name)
        {
            UInt32 result = NativeMethods.File_CreateStream(stream.Handle, name, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFile(data);
        }

        /// <summary>
        /// Reads the file cross-reference tables and initialized basic properties
        /// </summary>
        public void Initialize()
        {
            UInt32 result = NativeMethods.File_Initialize(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfVersion GetVersion()
        {
            UInt32 result = NativeMethods.File_GetVersion(Handle, out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfVersion>.CheckedCast(data);
        }

        private string GetFilename()
        {
            UInt32 result = NativeMethods.File_GetFilename(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            using (var buffer = new PdfBuffer(data)) {
                return Encoding.UTF8.GetString(buffer.Data);
            }
        }

        private bool IsEncrypted()
        {
            UInt32 result = NativeMethods.File_IsEncrypted(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set encryption password for files that require authentication
        /// </summary>
        /// <param name="password">Clear-text password to be used to decrypt the file</param>
        /// <returns>true if the password is correct, false if incorrect, throw exception on failure</returns>
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

        /// <summary>
        /// Allocate a new cross reference table entry for the file.
        /// </summary>
        /// <returns>The newly allocated <see cref="PdfXrefUsedEntry"/>.</returns>
        public PdfXrefUsedEntry AllocateNewEntry()
        {
            UInt32 result = NativeMethods.File_AllocateNewEntry(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetErrorException(result);
            }

            return new PdfXrefUsedEntry(data);
        }

        private PdfXrefChain GetXrefChain()
        {
            UInt32 result = NativeMethods.File_XrefChain(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefChain(data);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Filename;
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static FileOpenDelgate File_Open = LibraryInstance.GetFunction<FileOpenDelgate>("File_Open");
            public static FileOpenStreamDelgate File_OpenStream = LibraryInstance.GetFunction<FileOpenStreamDelgate>("File_OpenStream");
            public static FileCreateDelgate File_Create = LibraryInstance.GetFunction<FileCreateDelgate>("File_Create");
            public static FileCreateStreamDelgate File_CreateStream = LibraryInstance.GetFunction<FileCreateStreamDelgate>("File_CreateStream");
            public static FileInitializeDelgate File_Initialize = LibraryInstance.GetFunction<FileInitializeDelgate>("File_Initialize");
            public static FileGetVersionDelgate File_GetVersion = LibraryInstance.GetFunction<FileGetVersionDelgate>("File_GetVersion");
            public static FileGetFilenameDelgate File_GetFilename = LibraryInstance.GetFunction<FileGetFilenameDelgate>("File_GetFilename");
            public static FileIsEncryptedDelgate File_IsEncrypted = LibraryInstance.GetFunction<FileIsEncryptedDelgate>("File_IsEncrypted");
            public static FileSetEncryptionPasswordDelgate File_SetEncryptionPassword = LibraryInstance.GetFunction<FileSetEncryptionPasswordDelgate>("File_SetEncryptionPassword");
            public static FileAllocateNewEntryDelegate File_AllocateNewEntry = LibraryInstance.GetFunction<FileAllocateNewEntryDelegate>("File_AllocateNewEntry");

            public static FileXrefChainDelgate File_XrefChain = LibraryInstance.GetFunction<FileXrefChainDelgate>("File_XrefChain");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileOpenDelgate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string filename, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileOpenStreamDelgate(PdfInputOutputStreamSafeHandle input_stream, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string name, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileCreateDelgate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string filename, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileCreateStreamDelgate(PdfInputOutputStreamSafeHandle input_stream, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string name, out PdfFileSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileInitializeDelgate(PdfFileSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileGetVersionDelgate(PdfFileSafeHandle handle, out int data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileGetFilenameDelgate(PdfFileSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileIsEncryptedDelgate(PdfFileSafeHandle handle, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileSetEncryptionPasswordDelgate(PdfFileSafeHandle handle, string password);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileXrefChainDelgate(PdfFileSafeHandle handle, out PdfXrefChainSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FileAllocateNewEntryDelegate(PdfFileSafeHandle handle, out PdfXrefUsedEntrySafeHandle data);
        }
    }
}
