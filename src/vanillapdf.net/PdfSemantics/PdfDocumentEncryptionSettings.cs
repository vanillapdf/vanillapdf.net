using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        static PdfDocumentEncryptionSettings()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentEncryptionSettingsSafeHandle).TypeHandle);
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
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetAlgorithm(Handle, data);
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

            return data;
        }

        private void SetUserAccessPermissions(PdfUserAccessPermissionFlags data)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_SetUserAccessPermissions(Handle, data);
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

        private static class NativeMethods
        {
            public static CreateDelgate DocumentEncryptionSettings_Create = LibraryInstance.GetFunction<CreateDelgate>("DocumentEncryptionSettings_Create");

            public static GetAlgorithmDelgate DocumentEncryptionSettings_GetAlgorithm = LibraryInstance.GetFunction<GetAlgorithmDelgate>("DocumentEncryptionSettings_GetAlgorithm");
            public static SetAlgorithmDelgate DocumentEncryptionSettings_SetAlgorithm = LibraryInstance.GetFunction<SetAlgorithmDelgate>("DocumentEncryptionSettings_SetAlgorithm");

            public static GetKeyLengthDelgate DocumentEncryptionSettings_GetKeyLength = LibraryInstance.GetFunction<GetKeyLengthDelgate>("DocumentEncryptionSettings_GetKeyLength");
            public static SetKeyLengthDelgate DocumentEncryptionSettings_SetKeyLength = LibraryInstance.GetFunction<SetKeyLengthDelgate>("DocumentEncryptionSettings_SetKeyLength");

            public static GetUserAccessPermissionsDelgate DocumentEncryptionSettings_GetUserAccessPermissions = LibraryInstance.GetFunction<GetUserAccessPermissionsDelgate>("DocumentEncryptionSettings_GetUserAccessPermissions");
            public static SetUserAccessPermissionsDelgate DocumentEncryptionSettings_SetUserAccessPermissions = LibraryInstance.GetFunction<SetUserAccessPermissionsDelgate>("DocumentEncryptionSettings_SetUserAccessPermissions");

            public static GetUserPasswordDelgate DocumentEncryptionSettings_GetUserPassword = LibraryInstance.GetFunction<GetUserPasswordDelgate>("DocumentEncryptionSettings_GetUserPassword");
            public static SetUserPasswordDelgate DocumentEncryptionSettings_SetUserPassword = LibraryInstance.GetFunction<SetUserPasswordDelgate>("DocumentEncryptionSettings_SetUserPassword");

            public static GetOwnerPasswordDelgate DocumentEncryptionSettings_GetOwnerPassword = LibraryInstance.GetFunction<GetOwnerPasswordDelgate>("DocumentEncryptionSettings_GetOwnerPassword");
            public static SetOwnerPasswordDelgate DocumentEncryptionSettings_SetOwnerPassword = LibraryInstance.GetFunction<SetOwnerPasswordDelgate>("DocumentEncryptionSettings_SetOwnerPassword");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfDocumentEncryptionSettingsSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetAlgorithmDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfEncryptionAlgorithmType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetAlgorithmDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, PdfEncryptionAlgorithmType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetKeyLengthDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetKeyLengthDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUserAccessPermissionsDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfUserAccessPermissionFlags data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetUserAccessPermissionsDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, PdfUserAccessPermissionFlags data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUserPasswordDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetUserPasswordDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOwnerPasswordDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetOwnerPasswordDelgate(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);
        }
    }
}
