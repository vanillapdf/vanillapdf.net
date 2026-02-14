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

    internal sealed class PdfDestinationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Destination_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

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

    }

    internal sealed class PdfDestinationNameTreeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DestinationNameTree_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfDestinationNameTreeIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DestinationNameTreeIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfNameDictionarySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NameDictionary_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }
}
