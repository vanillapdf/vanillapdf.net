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
        public static partial UInt32 PageAnnotations_GetSize(PdfPageAnnotationsSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PageAnnotations_At(PdfPageAnnotationsSafeHandle handle, UIntPtr index, out PdfAnnotationSafeHandle data);

        #endregion

        #region Annotation

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Annotation_GetAnnotationType(PdfAnnotationSafeHandle handle, out Int32 data);

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
