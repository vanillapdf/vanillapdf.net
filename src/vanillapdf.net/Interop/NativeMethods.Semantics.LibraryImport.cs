#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region Document

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_Release(IntPtr handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Document_Open(string filename, out PdfDocumentSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_OpenFile(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Document_Create(string filename, out PdfDocumentSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_CreateFile(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_AppendDocument(PdfDocumentSafeHandle handle, PdfDocumentSafeHandle source);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_GetCatalog(PdfDocumentSafeHandle handle, out PdfCatalogSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Document_Save(PdfDocumentSafeHandle handle, string filename);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_SaveFile(PdfDocumentSafeHandle handle, PdfFileSafeHandle file);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_Sign(PdfDocumentSafeHandle handle, PdfFileSafeHandle destination, PdfDocumentSignatureSettingsSafeHandle settings);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_AddEncryption(PdfDocumentSafeHandle handle, PdfDocumentEncryptionSettingsSafeHandle settings);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_RemoveEncryption(PdfDocumentSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Document_GetDocumentInfo(PdfDocumentSafeHandle handle, out PdfDocumentInfoSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Document_OpenWithStrategy(string filename, PdfUtils.IOStrategyType strategy, out PdfDocumentSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Document_CreateWithStrategy(string filename, PdfUtils.IOStrategyType strategy, out PdfDocumentSafeHandle data);

        #endregion

        #region DocumentInfo

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetTitle(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetAuthor(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetSubject(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetKeywords(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetCreator(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetProducer(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetCreationDate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentInfo_GetModificationDate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);

        #endregion

        #region DocumentSignatureSettings

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_Release(IntPtr handle);

        #endregion

        #region DocumentEncryptionSettings

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_Release(IntPtr handle);

        #endregion

        #region Catalog

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_GetPages(PdfCatalogSafeHandle handle, out PdfPageTreeSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_GetVersion(PdfCatalogSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_GetOutlines(PdfCatalogSafeHandle handle, out PdfOutlineSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_GetDestinations(PdfCatalogSafeHandle handle, out PdfNamedDestinationsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_GetNames(PdfCatalogSafeHandle handle, out PdfNameDictionarySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Catalog_SetNames(PdfCatalogSafeHandle handle, PdfNameDictionarySafeHandle names);

        #endregion

        #region PageTree

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_GetPage(PdfPageTreeSafeHandle handle, UIntPtr at, out PdfPageObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_GetPageCount(PdfPageTreeSafeHandle handle, out UIntPtr count);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_InsertPage(PdfPageTreeSafeHandle handle, UIntPtr at, PdfPageObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_AppendPage(PdfPageTreeSafeHandle handle, PdfPageObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_RemovePage(PdfPageTreeSafeHandle handle, UIntPtr at);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageTree_FindPageIndex(PdfPageTreeSafeHandle handle, PdfObjectSafeHandle pageRef, out UIntPtr result);

        #endregion

        #region PageObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_GetContents(PdfPageObjectSafeHandle handle, out PdfPageContentsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_GetAnnotations(PdfPageObjectSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_GetResources(PdfPageObjectSafeHandle handle, out PdfResourceDictionarySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_GetMediaBox(PdfPageObjectSafeHandle handle, out PdfRectangleSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_SetMediaBox(PdfPageObjectSafeHandle handle, PdfRectangleSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageObject_GetBaseObject(PdfPageObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        #endregion

        #region PageContents

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageContents_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageContents_GetInstructionCollection(PdfPageContentsSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageContents_RecalculateStreamData(PdfPageContentsSafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool data);

        #endregion

        #region PageAnnotations

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_Create(out PdfPageAnnotationsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_GetSize(PdfPageAnnotationsSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_At(PdfPageAnnotationsSafeHandle handle, UIntPtr index, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_Append(PdfPageAnnotationsSafeHandle handle, PdfAnnotationSafeHandle annotation);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_Remove(PdfPageAnnotationsSafeHandle handle, UIntPtr at);

        #endregion

        #region Annotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetAnnotationType(PdfAnnotationSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetRect(PdfAnnotationSafeHandle handle, out PdfRectangleSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_SetRect(PdfAnnotationSafeHandle handle, PdfRectangleSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetContents(PdfAnnotationSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_SetContents(PdfAnnotationSafeHandle handle, PdfLiteralStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetColor(PdfAnnotationSafeHandle handle, out PdfColorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_SetColor(PdfAnnotationSafeHandle handle, PdfColorSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetFlags(PdfAnnotationSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_SetFlags(PdfAnnotationSafeHandle handle, Int32 value);

        #endregion

        #region Color

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_CreateTransparent(out PdfColorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_CreateGray(double gray, out PdfColorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_CreateRGB(double red, double green, double blue, out PdfColorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_CreateCMYK(double cyan, double magenta, double yellow, double black, out PdfColorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetColorSpace(PdfColorSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetRed(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetGreen(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetBlue(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetGray(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetCyan(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetMagenta(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetYellow(PdfColorSafeHandle handle, out double data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Color_GetBlack(PdfColorSafeHandle handle, out double data);

        #endregion

        #region TextAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_Create(PdfRectangleSafeHandle rect, out PdfTextAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_CreateWithContents(PdfRectangleSafeHandle rect, PdfLiteralStringObjectSafeHandle contents, out PdfTextAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_GetAuthor(PdfTextAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_SetAuthor(PdfTextAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_GetModificationDate(PdfTextAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_SetModificationDate(PdfTextAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_GetCreationDate(PdfTextAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_SetCreationDate(PdfTextAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_ToBaseAnnotation(PdfTextAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TextAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfTextAnnotationSafeHandle data);

        #endregion

        #region HighlightAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_Create(out PdfHighlightAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_CreateFromRect(PdfRectangleSafeHandle rect, out PdfHighlightAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_GetQuadPoints(PdfHighlightAnnotationSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_SetQuadPoints(PdfHighlightAnnotationSafeHandle handle, PdfArrayObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_GetAuthor(PdfHighlightAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_SetAuthor(PdfHighlightAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_GetModificationDate(PdfHighlightAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_SetModificationDate(PdfHighlightAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_GetCreationDate(PdfHighlightAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_SetCreationDate(PdfHighlightAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_ToBaseAnnotation(PdfHighlightAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HighlightAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfHighlightAnnotationSafeHandle data);

        #endregion

        #region FreeTextAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_Create(PdfRectangleSafeHandle rect, PdfLiteralStringObjectSafeHandle contents, PdfLiteralStringObjectSafeHandle defaultAppearance, out PdfFreeTextAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_GetDefaultAppearance(PdfFreeTextAnnotationSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_SetDefaultAppearance(PdfFreeTextAnnotationSafeHandle handle, PdfLiteralStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_GetAuthor(PdfFreeTextAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_SetAuthor(PdfFreeTextAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_GetModificationDate(PdfFreeTextAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_SetModificationDate(PdfFreeTextAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_GetCreationDate(PdfFreeTextAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_SetCreationDate(PdfFreeTextAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_ToBaseAnnotation(PdfFreeTextAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FreeTextAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfFreeTextAnnotationSafeHandle data);

        #endregion

        #region UnderlineAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_Create(out PdfUnderlineAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_CreateFromRect(PdfRectangleSafeHandle rect, out PdfUnderlineAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_GetQuadPoints(PdfUnderlineAnnotationSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_SetQuadPoints(PdfUnderlineAnnotationSafeHandle handle, PdfArrayObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_GetAuthor(PdfUnderlineAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_SetAuthor(PdfUnderlineAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_GetModificationDate(PdfUnderlineAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_SetModificationDate(PdfUnderlineAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_GetCreationDate(PdfUnderlineAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_SetCreationDate(PdfUnderlineAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_ToBaseAnnotation(PdfUnderlineAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnderlineAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfUnderlineAnnotationSafeHandle data);

        #endregion

        #region StrikeOutAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_Create(out PdfStrikeOutAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_CreateFromRect(PdfRectangleSafeHandle rect, out PdfStrikeOutAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_GetQuadPoints(PdfStrikeOutAnnotationSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_SetQuadPoints(PdfStrikeOutAnnotationSafeHandle handle, PdfArrayObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_GetAuthor(PdfStrikeOutAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_SetAuthor(PdfStrikeOutAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_GetModificationDate(PdfStrikeOutAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_SetModificationDate(PdfStrikeOutAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_GetCreationDate(PdfStrikeOutAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_SetCreationDate(PdfStrikeOutAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_ToBaseAnnotation(PdfStrikeOutAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StrikeOutAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfStrikeOutAnnotationSafeHandle data);

        #endregion

        #region SquigglyAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_Create(out PdfSquigglyAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_CreateFromRect(PdfRectangleSafeHandle rect, out PdfSquigglyAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_GetQuadPoints(PdfSquigglyAnnotationSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_SetQuadPoints(PdfSquigglyAnnotationSafeHandle handle, PdfArrayObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_GetAuthor(PdfSquigglyAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_SetAuthor(PdfSquigglyAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_GetModificationDate(PdfSquigglyAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_SetModificationDate(PdfSquigglyAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_GetCreationDate(PdfSquigglyAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_SetCreationDate(PdfSquigglyAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_ToBaseAnnotation(PdfSquigglyAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SquigglyAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfSquigglyAnnotationSafeHandle data);

        #endregion

        #region InkAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_Create(out PdfInkAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_CreateFromRect(PdfRectangleSafeHandle rect, out PdfInkAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_GetInkList(PdfInkAnnotationSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_SetInkList(PdfInkAnnotationSafeHandle handle, PdfArrayObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_GetAuthor(PdfInkAnnotationSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_SetAuthor(PdfInkAnnotationSafeHandle handle, PdfStringObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_GetModificationDate(PdfInkAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_SetModificationDate(PdfInkAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_GetCreationDate(PdfInkAnnotationSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_SetCreationDate(PdfInkAnnotationSafeHandle handle, PdfDateSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_ToBaseAnnotation(PdfInkAnnotationSafeHandle handle, out PdfAnnotationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InkAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfInkAnnotationSafeHandle data);

        #endregion

        #region Rectangle

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_Create(out PdfRectangleSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_GetLowerLeftX(PdfRectangleSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_SetLowerLeftX(PdfRectangleSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_GetLowerLeftY(PdfRectangleSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_SetLowerLeftY(PdfRectangleSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_GetUpperRightX(PdfRectangleSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_SetUpperRightX(PdfRectangleSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_GetUpperRightY(PdfRectangleSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Rectangle_SetUpperRightY(PdfRectangleSafeHandle handle, Int64 data);

        #endregion

        #region Date

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_CreateEmpty(out PdfDateSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_CreateCurrent(out PdfDateSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetYear(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetYear(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetMonth(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetMonth(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetDay(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetDay(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetHour(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetHour(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetMinute(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetMinute(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetSecond(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetSecond(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetTimezone(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetTimezone(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetHourOffset(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetHourOffset(PdfDateSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_GetMinuteOffset(PdfDateSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Date_SetMinuteOffset(PdfDateSafeHandle handle, Int32 data);

        #endregion

        #region Font

        [LibraryImport(LibraryName)]
        public static partial UInt32 Font_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Font_CreateFromObject(PdfDictionaryObjectSafeHandle handle, out PdfFontSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Font_GetFontType(PdfFontSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Font_GetUnicodeMap(PdfFontSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        #endregion

        #region Type0Font

        [LibraryImport(LibraryName)]
        public static partial UInt32 Type0Font_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Type0Font_ToFont(PdfType0FontSafeHandle handle, out PdfFontSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Type0Font_FromFont(PdfFontSafeHandle handle, out PdfType0FontSafeHandle data);

        #endregion

        #region FontMap

        [LibraryImport(LibraryName)]
        public static partial UInt32 FontMap_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FontMap_Contains(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, [MarshalAs(UnmanagedType.I1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FontMap_Find(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, out PdfFontSafeHandle data);

        #endregion

        #region ResourceDictionary

        [LibraryImport(LibraryName)]
        public static partial UInt32 ResourceDictionary_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ResourceDictionary_GetFontMap(PdfResourceDictionarySafeHandle handle, out PdfFontMapSafeHandle data);

        #endregion

        #region CharacterMap

        [LibraryImport(LibraryName)]
        public static partial UInt32 CharacterMap_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 CharacterMap_GetCharacterMapType(PdfCharacterMapSafeHandle handle, out Int32 data);

        #endregion

        #region UnicodeCharacterMap

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnicodeCharacterMap_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnicodeCharacterMap_ToCharacterMap(PdfUnicodeCharacterMapSafeHandle handle, out PdfCharacterMapSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnicodeCharacterMap_FromCharacterMap(PdfCharacterMapSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 UnicodeCharacterMap_GetMappedValue(PdfUnicodeCharacterMapSafeHandle handle, PdfBufferSafeHandle key, out PdfBufferSafeHandle data);

        #endregion

        #region OutlineBase

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineBase_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineBase_GetOutlineType(PdfOutlineBaseSafeHandle handle, out Int32 data);

        #endregion

        #region Outline

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_ToOutlineBase(PdfOutlineSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_GetFirst(PdfOutlineSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_GetLast(PdfOutlineSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Outline_GetCount(PdfOutlineSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        #endregion

        #region OutlineItem

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_ToOutlineBase(PdfOutlineItemSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetTitle(PdfOutlineItemSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetParent(PdfOutlineItemSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetPrev(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetNext(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetFirst(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetLast(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetCount(PdfOutlineItemSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutlineItem_GetDestination(PdfOutlineItemSafeHandle handle, out PdfDestinationSafeHandle data);

        #endregion

        #region DocumentSignatureSettings

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_Create(out PdfDocumentSignatureSettingsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetDigest(PdfDocumentSignatureSettingsSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetDigest(PdfDocumentSignatureSettingsSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetSigningKey(PdfDocumentSignatureSettingsSafeHandle handle, out PdfSigningKeySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetSigningKey(PdfDocumentSignatureSettingsSafeHandle handle, PdfSigningKeySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetName(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetName(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetLocation(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetLocation(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetReason(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetReason(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetSigningTime(PdfDocumentSignatureSettingsSafeHandle handle, out PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetSigningTime(PdfDocumentSignatureSettingsSafeHandle handle, PdfDateSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_GetCertificate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentSignatureSettings_SetCertificate(PdfDocumentSignatureSettingsSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        #endregion

        #region DocumentEncryptionSettings

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_Create(out PdfDocumentEncryptionSettingsSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_GetAlgorithm(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_SetAlgorithm(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_GetKeyLength(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_SetKeyLength(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_GetUserAccessPermissions(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_SetUserAccessPermissions(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_GetUserPassword(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_SetUserPassword(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_GetOwnerPassword(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DocumentEncryptionSettings_SetOwnerPassword(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);

        #endregion

        #region Destination

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_CreateFromArray(PdfArrayObjectSafeHandle arrayHandle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_CreateFromDictionary(PdfDictionaryObjectSafeHandle dictHandle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_GetDestinationType(PdfDestinationSafeHandle handle, out Int32 result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_GetPageNumber(PdfDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Destination_Resolve(PdfObjectSafeHandle handle, out PdfDestinationSafeHandle result);

        #endregion

        #region XYZDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfXYZDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_ToDestination(PdfXYZDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_GetLeft(PdfXYZDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_GetTop(PdfXYZDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XYZDestination_GetZoom(PdfXYZDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region FitDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitDestination_ToDestination(PdfFitDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        #endregion

        #region FitHorizontalDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitHorizontalDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitHorizontalDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitHorizontalDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitHorizontalDestination_ToDestination(PdfFitHorizontalDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitHorizontalDestination_GetTop(PdfFitHorizontalDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region FitVerticalDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitVerticalDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitVerticalDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitVerticalDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitVerticalDestination_ToDestination(PdfFitVerticalDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitVerticalDestination_GetLeft(PdfFitVerticalDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region FitRectangleDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitRectangleDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_ToDestination(PdfFitRectangleDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_GetLeft(PdfFitRectangleDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_GetBottom(PdfFitRectangleDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_GetRight(PdfFitRectangleDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitRectangleDestination_GetTop(PdfFitRectangleDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region FitBoundingBoxDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitBoundingBoxDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxDestination_ToDestination(PdfFitBoundingBoxDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        #endregion

        #region FitBoundingBoxHorizontalDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxHorizontalDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxHorizontalDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitBoundingBoxHorizontalDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxHorizontalDestination_ToDestination(PdfFitBoundingBoxHorizontalDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxHorizontalDestination_GetTop(PdfFitBoundingBoxHorizontalDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region FitBoundingBoxVerticalDestination

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxVerticalDestination_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxVerticalDestination_FromDestination(PdfDestinationSafeHandle handle, out PdfFitBoundingBoxVerticalDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxVerticalDestination_ToDestination(PdfFitBoundingBoxVerticalDestinationSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FitBoundingBoxVerticalDestination_GetLeft(PdfFitBoundingBoxVerticalDestinationSafeHandle handle, out PdfObjectSafeHandle result);

        #endregion

        #region NamedDestinations

        [LibraryImport(LibraryName)]
        public static partial UInt32 NamedDestinations_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NamedDestinations_Contains(PdfNamedDestinationsSafeHandle handle, PdfNameObjectSafeHandle name, [MarshalAs(UnmanagedType.I1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NamedDestinations_Find(PdfNamedDestinationsSafeHandle handle, PdfNameObjectSafeHandle name, out PdfDestinationSafeHandle result);

        #endregion

        #region LinkAnnotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 LinkAnnotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LinkAnnotation_ToBaseAnnotation(PdfLinkAnnotationSafeHandle handle, out PdfAnnotationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LinkAnnotation_FromBaseAnnotation(PdfAnnotationSafeHandle handle, out PdfLinkAnnotationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LinkAnnotation_GetDestination(PdfLinkAnnotationSafeHandle handle, out PdfDestinationSafeHandle result);

        #endregion

        #region DestinationNameTree

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Create(out PdfDestinationNameTreeSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Contains(PdfDestinationNameTreeSafeHandle handle, PdfStringObjectSafeHandle name, [MarshalAs(UnmanagedType.I1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Find(PdfDestinationNameTreeSafeHandle handle, PdfStringObjectSafeHandle name, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_TryFind(PdfDestinationNameTreeSafeHandle handle, PdfStringObjectSafeHandle name, out PdfDestinationSafeHandle result, [MarshalAs(UnmanagedType.I1)] out bool found);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Insert(PdfDestinationNameTreeSafeHandle handle, PdfStringObjectSafeHandle name, PdfDestinationSafeHandle destination);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_Remove(PdfDestinationNameTreeSafeHandle handle, PdfStringObjectSafeHandle name, [MarshalAs(UnmanagedType.I1)] out bool removed);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTree_GetIterator(PdfDestinationNameTreeSafeHandle handle, out PdfDestinationNameTreeIteratorSafeHandle result);

        #endregion

        #region DestinationNameTreeIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTreeIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTreeIterator_GetKey(PdfDestinationNameTreeIteratorSafeHandle handle, out PdfStringObjectSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTreeIterator_GetValue(PdfDestinationNameTreeIteratorSafeHandle handle, out PdfDestinationSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTreeIterator_IsValid(PdfDestinationNameTreeIteratorSafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DestinationNameTreeIterator_Next(PdfDestinationNameTreeIteratorSafeHandle handle);

        #endregion

        #region NameDictionary

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameDictionary_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameDictionary_Create(out PdfNameDictionarySafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameDictionary_ContainsDestinations(PdfNameDictionarySafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameDictionary_GetDestinations(PdfNameDictionarySafeHandle handle, out PdfDestinationNameTreeSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameDictionary_SetDestinations(PdfNameDictionarySafeHandle handle, PdfDestinationNameTreeSafeHandle destinations);

        #endregion
    }
}

#endif
