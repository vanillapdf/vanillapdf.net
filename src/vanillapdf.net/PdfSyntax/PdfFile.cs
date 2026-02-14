using System;
using System.Text;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents low-level interface for manipulating file structure
    /// </summary>
    public class PdfFile : IDisposable
    {
        internal PdfFileSafeHandle Handle { get; }

        internal PdfFile(PdfFileSafeHandle handle)
        {
            Handle = handle;
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

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
