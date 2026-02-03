using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Annotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.Annotation_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfAnnotationSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Annotation_FromUnknown(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfCatalogSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Catalog_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfCatalogSafeHandle handle)
        {
            UInt32 result = NativeMethods.Catalog_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfCatalogSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Catalog_FromUnknown(handle, out PdfCatalogSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfPageContentsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageContents_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfPageContentsSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageContents_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfPageContentsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageContents_FromUnknown(handle, out PdfPageContentsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDocumentSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Document_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentSafeHandle handle)
        {
            UInt32 result = NativeMethods.Document_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDocumentSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Document_FromUnknown(handle, out PdfDocumentSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDocumentSignatureSettingsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentSignatureSettings_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentSignatureSettingsSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDocumentSignatureSettingsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_FromUnknown(handle, out PdfDocumentSignatureSettingsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDocumentInfoSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentInfo_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentInfoSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentInfo_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDocumentInfoSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentInfo_FromUnknown(handle, out PdfDocumentInfoSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDocumentEncryptionSettingsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DocumentEncryptionSettings_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentEncryptionSettingsSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDocumentEncryptionSettingsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DocumentEncryptionSettings_FromUnknown(handle, out PdfDocumentEncryptionSettingsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfPageAnnotationsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageAnnotations_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfPageAnnotationsSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageAnnotations_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfPageAnnotationsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageAnnotations_FromUnknown(handle, out PdfPageAnnotationsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfResourceDictionarySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ResourceDictionary_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfResourceDictionarySafeHandle handle)
        {
            UInt32 result = NativeMethods.ResourceDictionary_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfResourceDictionarySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.ResourceDictionary_FromUnknown(handle, out PdfResourceDictionarySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfFontMapSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FontMap_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfFontMapSafeHandle handle)
        {
            UInt32 result = NativeMethods.FontMap_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFontMapSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.FontMap_FromUnknown(handle, out PdfFontMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfFontSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Font_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfFontSafeHandle handle)
        {
            UInt32 result = NativeMethods.Font_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFontSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Font_FromUnknown(handle, out PdfFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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
        protected override bool ReleaseHandle() => NativeMethods.CharacterMap_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfCharacterMapSafeHandle handle)
        {
            UInt32 result = NativeMethods.CharacterMap_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfCharacterMapSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.CharacterMap_FromUnknown(handle, out PdfCharacterMapSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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
        protected override bool ReleaseHandle() => NativeMethods.PageObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfPageObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageObject_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfPageObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageObject_FromUnknown(handle, out PdfPageObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfPageTreeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PageTree_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfPageTreeSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageTree_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfPageTreeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.PageTree_FromUnknown(handle, out PdfPageTreeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDateSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Date_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDateSafeHandle handle)
        {
            UInt32 result = NativeMethods.Date_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDateSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Date_FromUnknown(handle, out PdfDateSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfRectangleSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Rectangle_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfRectangleSafeHandle handle)
        {
            UInt32 result = NativeMethods.Rectangle_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfRectangleSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Rectangle_FromUnknown(handle, out PdfRectangleSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfOutlineBaseSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.OutlineBase_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfOutlineBaseSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutlineBase_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfOutlineBaseSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutlineBase_FromUnknown(handle, out PdfOutlineBaseSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfOutlineItemSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }

        public static implicit operator PdfOutlineItemSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfOutlineBaseSafeHandle)handle;
        }
    }

    internal sealed class PdfDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Destination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.Destination_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Destination_FromUnknown(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXYZDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XYZDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfXYZDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.XYZDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXYZDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.XYZDestination_FromDestination(handle, out PdfXYZDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfXYZDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfXYZDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitDestination_FromDestination(handle, out PdfFitDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitHorizontalDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitHorizontalDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitHorizontalDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitHorizontalDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitHorizontalDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitHorizontalDestination_FromDestination(handle, out PdfFitHorizontalDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitHorizontalDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitHorizontalDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitVerticalDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitVerticalDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitVerticalDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitVerticalDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitVerticalDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitVerticalDestination_FromDestination(handle, out PdfFitVerticalDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitVerticalDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitVerticalDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitRectangleDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitRectangleDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitRectangleDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitRectangleDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitRectangleDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitRectangleDestination_FromDestination(handle, out PdfFitRectangleDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitRectangleDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitRectangleDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitBoundingBoxDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitBoundingBoxDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitBoundingBoxDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitBoundingBoxDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxDestination_FromDestination(handle, out PdfFitBoundingBoxDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitBoundingBoxDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitBoundingBoxDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitBoundingBoxHorizontalDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitBoundingBoxHorizontalDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitBoundingBoxHorizontalDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxHorizontalDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitBoundingBoxHorizontalDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxHorizontalDestination_FromDestination(handle, out PdfFitBoundingBoxHorizontalDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitBoundingBoxHorizontalDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitBoundingBoxHorizontalDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfFitBoundingBoxVerticalDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FitBoundingBoxVerticalDestination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfDestinationSafeHandle(PdfFitBoundingBoxVerticalDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxVerticalDestination_ToDestination(handle, out PdfDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFitBoundingBoxVerticalDestinationSafeHandle(PdfDestinationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FitBoundingBoxVerticalDestination_FromDestination(handle, out PdfFitBoundingBoxVerticalDestinationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfFitBoundingBoxVerticalDestinationSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }

        public static implicit operator PdfFitBoundingBoxVerticalDestinationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfDestinationSafeHandle)handle;
        }
    }

    internal sealed class PdfNamedDestinationsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NamedDestinations_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfLinkAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.LinkAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfLinkAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.LinkAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfLinkAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.LinkAnnotation_FromBaseAnnotation(handle, out PdfLinkAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfLinkAnnotationSafeHandle handle)
        {
            return (PdfAnnotationSafeHandle)handle;
        }

        public static implicit operator PdfLinkAnnotationSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfAnnotationSafeHandle)handle;
        }
    }

    internal sealed class PdfDestinationNameTreeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DestinationNameTree_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDestinationNameTreeSafeHandle handle)
        {
            UInt32 result = NativeMethods.DestinationNameTree_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDestinationNameTreeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DestinationNameTree_FromUnknown(handle, out PdfDestinationNameTreeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDestinationNameTreeIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DestinationNameTreeIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDestinationNameTreeIteratorSafeHandle handle)
        {
            UInt32 result = NativeMethods.DestinationNameTreeIterator_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDestinationNameTreeIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DestinationNameTreeIterator_FromUnknown(handle, out PdfDestinationNameTreeIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfNameDictionarySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NameDictionary_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfNameDictionarySafeHandle handle)
        {
            UInt32 result = NativeMethods.NameDictionary_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfNameDictionarySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.NameDictionary_FromUnknown(handle, out PdfNameDictionarySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }
}
