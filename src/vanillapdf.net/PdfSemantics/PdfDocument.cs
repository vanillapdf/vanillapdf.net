using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// High-level counterpart of the \ref PdfSyntax.PdfFile
    /// </summary>
    public class PdfDocument : PdfUnknown
    {
        internal PdfDocumentSafeHandle Handle { get; }

        internal PdfDocument(PdfDocumentSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfDocument()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Open and existing document for reading and manupulation
        /// </summary>
        /// <param name="filename">path to existing document</param>
        /// <returns>A new \ref PdfDocument instance on success, throws exception on failure</returns>
        public static PdfDocument Open(string filename)
        {
            UInt32 result = NativeMethods.Document_Open(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocument(data);
        }

        /// <summary>
        /// Open and existing document for reading and manupulation
        /// </summary>
        /// <param name="file">Handle to \ref PdfFile instance</param>
        /// <returns>A new \ref PdfDocument instance on success, throws exception on failure</returns>
        public static PdfDocument OpenFile(PdfFile file)
        {
            UInt32 result = NativeMethods.Document_OpenFile(file.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocument(data);
        }

        /// <summary>
        /// Creates a new document at the specified location
        /// </summary>
        /// <param name="filename">path file to be created</param>
        /// <returns>A new \ref PdfDocument instance on success, throws exception on failure</returns>
        public static PdfDocument Create(string filename)
        {
            UInt32 result = NativeMethods.Document_Create(filename, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocument(data);
        }

        /// <summary>
        /// Creates a new document at the specified location
        /// </summary>
        /// <param name="file">Handle to \ref PdfFile instance</param>
        /// <returns>A new \ref PdfDocument instance on success, throws exception on failure</returns>
        public static PdfDocument CreateFile(PdfFile file)
        {
            UInt32 result = NativeMethods.Document_CreateFile(file.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocument(data);
        }

        /// <summary>
        /// Appends all pages from the specified document to the current instance
        /// </summary>
        /// <param name="source">Source document from which all pages will be extracted</param>
        public void AppendDocument(PdfDocument source)
        {
            UInt32 result = NativeMethods.Document_AppendDocument(Handle, source.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get catalog entry (the root of all nested items) from the current document
        /// </summary>
        /// <returns>Handle to existing \ref PdfCatalog instance on success, throws exception on failure</returns>
        public PdfCatalog GetCatalog()
        {
            UInt32 result = NativeMethods.Document_GetCatalog(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfCatalog(data);
        }

        /// <summary>
        /// Saves current document at the specified location
        /// </summary>
        /// <param name="filename">file path where the document should be saved</param>
        public void Save(string filename)
        {
            UInt32 result = NativeMethods.Document_Save(Handle, filename);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Saves current document at the specified location
        /// </summary>
        /// <param name="destination">File handle to the destination path</param>
        public void SaveFile(PdfFile destination)
        {
            UInt32 result = NativeMethods.Document_SaveFile(Handle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Sign current document using specified settings
        /// </summary>
        /// <param name="destination">Destination of the signed file</param>
        /// <param name="settings">Signature settings</param>
        public void Sign(PdfFile destination, PdfDocumentSignatureSettings settings)
        {
            UInt32 result = NativeMethods.Document_Sign(Handle, destination.Handle, settings.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Enable encryption on the current document using the specified settings.
        /// </summary>
        /// <param name="settings">Encryption parameters.</param>
        public void AddEncryption(PdfDocumentEncryptionSettings settings)
        {
            UInt32 result = NativeMethods.Document_AddEncryption(Handle, settings.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove any encryption from the current document.
        /// </summary>
        public void RemoveEncryption()
        {
            UInt32 result = NativeMethods.Document_RemoveEncryption(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static OpenDelgate Document_Open = LibraryInstance.GetFunction<OpenDelgate>("Document_Open");
            public static OpenFileDelgate Document_OpenFile = LibraryInstance.GetFunction<OpenFileDelgate>("Document_OpenFile");
            public static CreateDelgate Document_Create = LibraryInstance.GetFunction<CreateDelgate>("Document_Create");
            public static CreateFileDelgate Document_CreateFile = LibraryInstance.GetFunction<CreateFileDelgate>("Document_CreateFile");
            public static AppendDocumentDelgate Document_AppendDocument = LibraryInstance.GetFunction<AppendDocumentDelgate>("Document_AppendDocument");
            public static GetCatalogDelgate Document_GetCatalog = LibraryInstance.GetFunction<GetCatalogDelgate>("Document_GetCatalog");
            public static SaveDelgate Document_Save = LibraryInstance.GetFunction<SaveDelgate>("Document_Save");
            public static SaveFileDelgate Document_SaveFile = LibraryInstance.GetFunction<SaveFileDelgate>("Document_SaveFile");
            public static SignDelgate Document_Sign = LibraryInstance.GetFunction<SignDelgate>("Document_Sign");

            public static AddEncryptionDelgate Document_AddEncryption = LibraryInstance.GetFunction<AddEncryptionDelgate>("Document_AddEncryption");
            public static RemoveEncryptionDelgate Document_RemoveEncryption = LibraryInstance.GetFunction<RemoveEncryptionDelgate>("Document_RemoveEncryption");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OpenDelgate(string filename, out PdfDocumentSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OpenFileDelgate(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(string filename, out PdfDocumentSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFileDelgate(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AppendDocumentDelgate(PdfDocumentSafeHandle handle, PdfDocumentSafeHandle source);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCatalogDelgate(PdfDocumentSafeHandle handle, out PdfCatalogSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SaveDelgate(PdfDocumentSafeHandle handle, string filename);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SaveFileDelgate(PdfDocumentSafeHandle handle, PdfFileSafeHandle file);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SignDelgate(PdfDocumentSafeHandle handle, PdfFileSafeHandle destination, PdfDocumentSignatureSettingsSafeHandle settings);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AddEncryptionDelgate(PdfDocumentSafeHandle handle, PdfDocumentEncryptionSettingsSafeHandle settings);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveEncryptionDelgate(PdfDocumentSafeHandle handle);
        }
    }
}
