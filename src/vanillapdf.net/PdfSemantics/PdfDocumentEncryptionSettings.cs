using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Class for specifying document encryption parameters
    /// </summary>
    public class PdfDocumentEncryptionSettings : PdfUnknown
    {
        internal PdfDocumentEncryptionSettingsSafeHandle Handle { get; }

        internal PdfDocumentEncryptionSettings(PdfDocumentEncryptionSettingsSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Algorithm to be used for document encryption
        /// </summary>
        public PdfEncryptionAlgorithmType EncryptionAlgorithmType
        {
            get { return GetAlgorithm(); }
            set { SetAlgorithm(value); }
        }

        /// <summary>
        /// Length of the key for document encryption
        /// </summary>
        public Int32 KeyLength
        {
            get { return GetKeyLength(); }
            set { SetKeyLength(value); }
        }

        /// <summary>
        /// Permissions for user, when opening the document with user password
        /// </summary>
        public PdfUserAccessPermissionFlags UserAccessPermissions
        {
            get { return GetUserAccessPermissions(); }
            set { SetUserAccessPermissions(value); }
        }

        /// <summary>
        /// User password to be used for document encryption
        /// </summary>
        public PdfBuffer UserPassword
        {
            get { return GetUserPassword(); }
            set { SetUserPassword(value); }
        }

        /// <summary>
        /// Owner password to be used for document encryption
        /// </summary>
        public PdfBuffer OwnerPassword
        {
            get { return GetOwnerPassword(); }
            set { SetOwnerPassword(value); }
        }

        /// <summary>
        /// Creates a new blank instance of \ref PdfDocumentEncryptionSettings with default values
        /// </summary>
        /// <returns>A new \ref PdfDocumentEncryptionSettings instance on success, throws exception on failure</returns>
        public static PdfDocumentEncryptionSettings Create()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocumentEncryptionSettings(data);
        }

        #region private properties

        private PdfEncryptionAlgorithmType GetAlgorithm()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_GetAlgorithm(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfEncryptionAlgorithmType>.CheckedCast(data);
        }

        private void SetAlgorithm(PdfEncryptionAlgorithmType data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetAlgorithm(Handle, (Int32)data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetKeyLength()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_GetKeyLength(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetKeyLength(Int32 data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetKeyLength(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfUserAccessPermissionFlags GetUserAccessPermissions()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_GetUserAccessPermissions(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return (PdfUserAccessPermissionFlags)data;
        }

        private void SetUserAccessPermissions(PdfUserAccessPermissionFlags data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetUserAccessPermissions(Handle, (Int32)data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfBuffer GetUserPassword()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_GetUserPassword(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private void SetUserPassword(PdfBuffer data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetUserPassword(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfBuffer GetOwnerPassword()
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_GetOwnerPassword(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private void SetOwnerPassword(PdfBuffer data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetOwnerPassword(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        #endregion

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
