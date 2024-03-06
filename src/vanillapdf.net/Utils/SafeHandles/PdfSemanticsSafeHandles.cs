using System;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfAnnotationSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Annotation_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Annotation_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Annotation_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfAnnotationSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfAnnotationSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfAnnotationSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfCatalogSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Catalog_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Catalog_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Catalog_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfCatalogSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfCatalogSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfCatalogSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfCatalogSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfCatalogSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageContentsSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageContents_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageContents_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageContents_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageContentsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageContentsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageContentsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageContentsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageContentsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfDocumentSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Document_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Document_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Document_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfDocumentSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfDocumentSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDocumentSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfDocumentSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfDocumentSignatureSettingsSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("DocumentSignatureSettings_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("DocumentSignatureSettings_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("DocumentSignatureSettings_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfDocumentSignatureSettingsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentSignatureSettingsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDocumentSignatureSettingsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfDocumentSignatureSettingsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfDocumentEncryptionSettingsSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("DocumentEncryptionSettings_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("DocumentEncryptionSettings_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("DocumentEncryptionSettings_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfDocumentEncryptionSettingsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentEncryptionSettingsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDocumentEncryptionSettingsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfDocumentEncryptionSettingsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageAnnotationsSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageAnnotations_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageAnnotations_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageAnnotations_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageAnnotationsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageAnnotationsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageAnnotationsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageAnnotationsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfResourceDictionarySafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ResourceDictionary_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ResourceDictionary_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ResourceDictionary_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfResourceDictionarySafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfResourceDictionarySafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfResourceDictionarySafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfResourceDictionarySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfResourceDictionarySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfFontMapSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("FontMap_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("FontMap_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("FontMap_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfFontMapSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfFontMapSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfFontMapSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfFontMapSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfFontMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfFontSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Font_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Font_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Font_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfFontSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfFontSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfFontSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfFontSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfType0FontSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Type0Font_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToFontDelegate Convert_ToFont = LibraryInstance.GetFunction<ConvertToFontDelegate>("Type0Font_ToFont");
        private static readonly ConvertFromFontDelegate Convert_FromFont = LibraryInstance.GetFunction<ConvertFromFontDelegate>("Type0Font_FromFont");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToFontDelegate(PdfType0FontSafeHandle handle, out PdfFontSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromFontDelegate(PdfFontSafeHandle handle, out PdfType0FontSafeHandle data);

        public static implicit operator PdfFontSafeHandle(PdfType0FontSafeHandle handle)
        {
            UInt32 result = Convert_ToFont(handle, out PdfFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfType0FontSafeHandle(PdfFontSafeHandle handle)
        {
            UInt32 result = Convert_FromFont(handle, out PdfType0FontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfType0FontSafeHandle handle)
        {
            return (PdfFontSafeHandle)handle;
        }

        public static implicit operator PdfType0FontSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfFontSafeHandle)handle;
        }
    }

    internal sealed class PdfCharacterMapSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("CharacterMap_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("CharacterMap_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("CharacterMap_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfCharacterMapSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfCharacterMapSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfCharacterMapSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfCharacterMapSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfUnicodeCharacterMapSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("UnicodeCharacterMap_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToCharacterMapDelegate Convert_ToCharacterMap = LibraryInstance.GetFunction<ConvertToCharacterMapDelegate>("UnicodeCharacterMap_ToCharacterMap");
        private static readonly ConvertFromCharacterMapDelegate Convert_FromCharacterMap = LibraryInstance.GetFunction<ConvertFromCharacterMapDelegate>("UnicodeCharacterMap_FromCharacterMap");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToCharacterMapDelegate(PdfUnicodeCharacterMapSafeHandle handle, out PdfCharacterMapSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromCharacterMapDelegate(PdfCharacterMapSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        public static implicit operator PdfCharacterMapSafeHandle(PdfUnicodeCharacterMapSafeHandle handle)
        {
            UInt32 result = Convert_ToCharacterMap(handle, out PdfCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnicodeCharacterMapSafeHandle(PdfCharacterMapSafeHandle handle)
        {
            UInt32 result = Convert_FromCharacterMap(handle, out PdfUnicodeCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfUnicodeCharacterMapSafeHandle handle)
        {
            return (PdfCharacterMapSafeHandle)handle;
        }

        public static implicit operator PdfUnicodeCharacterMapSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfCharacterMapSafeHandle)handle;
        }
    }

    internal sealed class PdfPageObjectSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageObject_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageObject_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageObjectSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageObjectSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageTreeSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageTree_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageTree_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageTree_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageTreeSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageTreeSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageTreeSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageTreeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageTreeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfDateSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Date_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Date_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Date_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfDateSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfDateSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfDateSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDateSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfDateSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfRectangleSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Rectangle_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Rectangle_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Rectangle_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfRectangleSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfRectangleSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfRectangleSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfRectangleSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfRectangleSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfOutlineBaseSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("OutlineBase_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("OutlineBase_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("OutlineBase_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfOutlineBaseSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutlineBaseSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfOutlineSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Outline_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToOutlineBaseDelegate Convert_ToOutlineBase = LibraryInstance.GetFunction<ConvertToOutlineBaseDelegate>("Outline_ToOutlineBase");
        private static readonly ConvertFromOutlineBaseDelegate Convert_FromOutlineBase = LibraryInstance.GetFunction<ConvertFromOutlineBaseDelegate>("Outline_FromOutlineBase");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToOutlineBaseDelegate(PdfOutlineSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromOutlineBaseDelegate(PdfOutlineBaseSafeHandle handle, out PdfOutlineSafeHandle data);

        public static implicit operator PdfOutlineBaseSafeHandle(PdfOutlineSafeHandle handle)
        {
            UInt32 result = Convert_ToOutlineBase(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutlineSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = Convert_FromOutlineBase(handle, out PdfOutlineSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfOutlineSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }

        public static implicit operator PdfOutlineSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }
    }

    internal sealed class PdfOutlineItemSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("OutlineItem_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToOutlineBaseDelegate Convert_ToOutlineBase = LibraryInstance.GetFunction<ConvertToOutlineBaseDelegate>("OutlineItem_ToOutlineBase");
        private static readonly ConvertFromOutlineBaseDelegate Convert_FromOutlineBase = LibraryInstance.GetFunction<ConvertFromOutlineBaseDelegate>("OutlineItem_FromOutlineBase");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToOutlineBaseDelegate(PdfOutlineItemSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromOutlineBaseDelegate(PdfOutlineBaseSafeHandle handle, out PdfOutlineItemSafeHandle data);

        public static implicit operator PdfOutlineBaseSafeHandle(PdfOutlineItemSafeHandle handle)
        {
            UInt32 result = Convert_ToOutlineBase(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutlineItemSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = Convert_FromOutlineBase(handle, out PdfOutlineItemSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfOutlineItemSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }

        public static implicit operator PdfOutlineItemSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }
    }
}
