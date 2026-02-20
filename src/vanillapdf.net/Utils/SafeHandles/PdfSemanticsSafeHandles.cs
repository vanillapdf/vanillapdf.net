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

    internal sealed class PdfColorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Color_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfTextAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.TextAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfTextAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.TextAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfTextAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.TextAnnotation_FromBaseAnnotation(handle, out PdfTextAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfHighlightAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.HighlightAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfHighlightAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.HighlightAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfHighlightAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.HighlightAnnotation_FromBaseAnnotation(handle, out PdfHighlightAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfFreeTextAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FreeTextAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfFreeTextAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FreeTextAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFreeTextAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.FreeTextAnnotation_FromBaseAnnotation(handle, out PdfFreeTextAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfUnderlineAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.UnderlineAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfUnderlineAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.UnderlineAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfUnderlineAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.UnderlineAnnotation_FromBaseAnnotation(handle, out PdfUnderlineAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfStrikeOutAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.StrikeOutAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfStrikeOutAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.StrikeOutAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfStrikeOutAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.StrikeOutAnnotation_FromBaseAnnotation(handle, out PdfStrikeOutAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfSquigglyAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.SquigglyAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfSquigglyAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.SquigglyAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfSquigglyAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.SquigglyAnnotation_FromBaseAnnotation(handle, out PdfSquigglyAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfInkAnnotationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.InkAnnotation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfAnnotationSafeHandle(PdfInkAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.InkAnnotation_ToBaseAnnotation(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfInkAnnotationSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = NativeMethods.InkAnnotation_FromBaseAnnotation(handle, out PdfInkAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfActionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Action_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfGoToActionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.GoToAction_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfActionSafeHandle(PdfGoToActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.GoToAction_ToAction(handle, out PdfActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfGoToActionSafeHandle(PdfActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.GoToAction_FromAction(handle, out PdfGoToActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfURIActionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.URIAction_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfActionSafeHandle(PdfURIActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.URIAction_ToAction(handle, out PdfActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfURIActionSafeHandle(PdfActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.URIAction_FromAction(handle, out PdfURIActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfGoToRemoteActionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.GoToRemoteAction_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfActionSafeHandle(PdfGoToRemoteActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.GoToRemoteAction_ToAction(handle, out PdfActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfGoToRemoteActionSafeHandle(PdfActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.GoToRemoteAction_FromAction(handle, out PdfGoToRemoteActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfNamedActionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NamedAction_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfActionSafeHandle(PdfNamedActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.NamedAction_ToAction(handle, out PdfActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfNamedActionSafeHandle(PdfActionSafeHandle handle)
        {
            UInt32 result = NativeMethods.NamedAction_FromAction(handle, out PdfNamedActionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }
}
