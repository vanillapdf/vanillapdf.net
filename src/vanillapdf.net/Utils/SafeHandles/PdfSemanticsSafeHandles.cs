using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Annotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfCatalogSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Catalog_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfPageContentsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageContents_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDocumentSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Document_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDocumentSignatureSettingsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentSignatureSettings_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDocumentInfoSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentInfo_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDocumentEncryptionSettingsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentEncryptionSettings_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfPageAnnotationsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageAnnotations_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfResourceDictionarySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ResourceDictionary_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfFontMapSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FontMap_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfFontSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Font_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfType0FontSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Type0Font_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfFontSafeHandle(PdfType0FontSafeHandle handle)
        {
            UInt32 result = NativeMethods.Type0Font_ToFont(handle, out PdfFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfType0FontSafeHandle(PdfFontSafeHandle handle)
        {
            UInt32 result = NativeMethods.Type0Font_FromFont(handle, out PdfType0FontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfCharacterMapSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.CharacterMap_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfUnicodeCharacterMapSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.UnicodeCharacterMap_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfCharacterMapSafeHandle(PdfUnicodeCharacterMapSafeHandle handle)
        {
            UInt32 result = NativeMethods.UnicodeCharacterMap_ToCharacterMap(handle, out PdfCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnicodeCharacterMapSafeHandle(PdfCharacterMapSafeHandle handle)
        {
            UInt32 result = NativeMethods.UnicodeCharacterMap_FromCharacterMap(handle, out PdfUnicodeCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfPageObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfPageTreeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageTree_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDateSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Date_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfRectangleSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Rectangle_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfOutlineBaseSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.OutlineBase_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfOutlineSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Outline_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfOutlineBaseSafeHandle(PdfOutlineSafeHandle handle)
        {
            UInt32 result = NativeMethods.Outline_ToOutlineBase(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfOutlineSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = NativeMethods.Outline_FromOutlineBase(handle, out PdfOutlineSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfOutlineItemSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.OutlineItem_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfOutlineBaseSafeHandle(PdfOutlineItemSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutlineItem_ToOutlineBase(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfOutlineItemSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutlineItem_FromOutlineBase(handle, out PdfOutlineItemSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }
}
