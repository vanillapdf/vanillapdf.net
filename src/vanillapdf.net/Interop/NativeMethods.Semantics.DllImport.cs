#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region Document

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_Open(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string filename,
            out PdfDocumentSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_OpenFile(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_Create(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string filename,
            out PdfDocumentSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_CreateFile(PdfFileSafeHandle file, out PdfDocumentSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_AppendDocument(PdfDocumentSafeHandle handle, PdfDocumentSafeHandle source);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_GetCatalog(PdfDocumentSafeHandle handle, out PdfCatalogSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_Save(PdfDocumentSafeHandle handle,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string filename);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_SaveFile(PdfDocumentSafeHandle handle, PdfFileSafeHandle file);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_Sign(PdfDocumentSafeHandle handle, PdfFileSafeHandle destination, PdfDocumentSignatureSettingsSafeHandle settings);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_AddEncryption(PdfDocumentSafeHandle handle, PdfDocumentEncryptionSettingsSafeHandle settings);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_RemoveEncryption(PdfDocumentSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_GetDocumentInfo(PdfDocumentSafeHandle handle, out PdfDocumentInfoSafeHandle data);

        #endregion

        #region DocumentInfo

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetTitle(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetAuthor(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetSubject(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetKeywords(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetCreator(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetProducer(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetCreationDate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_GetModificationDate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);

        #endregion

        #region DocumentSignatureSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_Release(IntPtr handle);

        #endregion

        #region DocumentEncryptionSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_Release(IntPtr handle);

        #endregion

        #region Catalog

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_GetPages(PdfCatalogSafeHandle handle, out PdfPageTreeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_GetVersion(PdfCatalogSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_GetOutlines(PdfCatalogSafeHandle handle, out PdfOutlineSafeHandle data);

        #endregion

        #region PageTree

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_GetPage(PdfPageTreeSafeHandle handle, UIntPtr at, out PdfPageObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_GetPageCount(PdfPageTreeSafeHandle handle, out UIntPtr count);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_InsertPage(PdfPageTreeSafeHandle handle, UIntPtr at, PdfPageObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_AppendPage(PdfPageTreeSafeHandle handle, PdfPageObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_RemovePage(PdfPageTreeSafeHandle handle, UIntPtr at);

        #endregion

        #region PageObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_GetContents(PdfPageObjectSafeHandle handle, out PdfPageContentsSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_GetAnnotations(PdfPageObjectSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_GetResources(PdfPageObjectSafeHandle handle, out PdfResourceDictionarySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_GetMediaBox(PdfPageObjectSafeHandle handle, out PdfRectangleSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_SetMediaBox(PdfPageObjectSafeHandle handle, PdfRectangleSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_GetBaseObject(PdfPageObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        #endregion

        #region PageContents

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageContents_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageContents_GetInstructionCollection(PdfPageContentsSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 PageContents_RecalculateStreamData(PdfPageContentsSafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool data);

        #endregion

        #region PageAnnotations

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_GetSize(PdfPageAnnotationsSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_At(PdfPageAnnotationsSafeHandle handle, UIntPtr index, out PdfAnnotationSafeHandle data);

        #endregion

        #region Annotation

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Annotation_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Annotation_GetAnnotationType(PdfAnnotationSafeHandle handle, out Int32 data);

        #endregion

        #region Rectangle

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_Create(out PdfRectangleSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_GetLowerLeftX(PdfRectangleSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_SetLowerLeftX(PdfRectangleSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_GetLowerLeftY(PdfRectangleSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_SetLowerLeftY(PdfRectangleSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_GetUpperRightX(PdfRectangleSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_SetUpperRightX(PdfRectangleSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_GetUpperRightY(PdfRectangleSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_SetUpperRightY(PdfRectangleSafeHandle handle, Int64 data);

        #endregion

        #region Date

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_CreateEmpty(out PdfDateSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_CreateCurrent(out PdfDateSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetYear(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetYear(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetMonth(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetMonth(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetDay(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetDay(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetHour(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetHour(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetMinute(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetMinute(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetSecond(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetSecond(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetTimezone(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetTimezone(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetHourOffset(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetHourOffset(PdfDateSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_GetMinuteOffset(PdfDateSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_SetMinuteOffset(PdfDateSafeHandle handle, Int32 data);

        #endregion

        #region Font

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_CreateFromObject(PdfDictionaryObjectSafeHandle handle, out PdfFontSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_GetFontType(PdfFontSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_GetUnicodeMap(PdfFontSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        #endregion

        #region Type0Font

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Type0Font_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Type0Font_ToFont(PdfType0FontSafeHandle handle, out PdfFontSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Type0Font_FromFont(PdfFontSafeHandle handle, out PdfType0FontSafeHandle data);

        #endregion

        #region FontMap

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FontMap_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 FontMap_Contains(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, [MarshalAs(UnmanagedType.I1)] out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FontMap_Find(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, out PdfFontSafeHandle data);

        #endregion

        #region ResourceDictionary

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ResourceDictionary_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ResourceDictionary_GetFontMap(PdfResourceDictionarySafeHandle handle, out PdfFontMapSafeHandle data);

        #endregion

        #region CharacterMap

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 CharacterMap_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 CharacterMap_GetCharacterMapType(PdfCharacterMapSafeHandle handle, out Int32 data);

        #endregion

        #region UnicodeCharacterMap

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_ToCharacterMap(PdfUnicodeCharacterMapSafeHandle handle, out PdfCharacterMapSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_FromCharacterMap(PdfCharacterMapSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_GetMappedValue(PdfUnicodeCharacterMapSafeHandle handle, PdfBufferSafeHandle key, out PdfBufferSafeHandle data);

        #endregion

        #region OutlineBase

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineBase_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineBase_GetOutlineType(PdfOutlineBaseSafeHandle handle, out Int32 data);

        #endregion

        #region Outline

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_ToOutlineBase(PdfOutlineSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_GetFirst(PdfOutlineSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_GetLast(PdfOutlineSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_GetCount(PdfOutlineSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        #endregion

        #region OutlineItem

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_ToOutlineBase(PdfOutlineItemSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetTitle(PdfOutlineItemSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetParent(PdfOutlineItemSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetPrev(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetNext(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetFirst(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetLast(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_GetCount(PdfOutlineItemSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        #endregion

        #region DocumentSignatureSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_Create(out PdfDocumentSignatureSettingsSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetDigest(PdfDocumentSignatureSettingsSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetDigest(PdfDocumentSignatureSettingsSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetSigningKey(PdfDocumentSignatureSettingsSafeHandle handle, out PdfSigningKeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetSigningKey(PdfDocumentSignatureSettingsSafeHandle handle, PdfSigningKeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetName(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetName(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetLocation(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetLocation(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetReason(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetReason(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetSigningTime(PdfDocumentSignatureSettingsSafeHandle handle, out PdfDateSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetSigningTime(PdfDocumentSignatureSettingsSafeHandle handle, PdfDateSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_GetCertificate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_SetCertificate(PdfDocumentSignatureSettingsSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        #endregion

        #region DocumentEncryptionSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_Create(out PdfDocumentEncryptionSettingsSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_GetAlgorithm(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_SetAlgorithm(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_GetKeyLength(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_SetKeyLength(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_GetUserAccessPermissions(PdfDocumentEncryptionSettingsSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_SetUserAccessPermissions(PdfDocumentEncryptionSettingsSafeHandle handle, Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_GetUserPassword(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_SetUserPassword(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_GetOwnerPassword(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_SetOwnerPassword(PdfDocumentEncryptionSettingsSafeHandle handle, PdfBufferSafeHandle data);

        #endregion
    }
}

#endif
