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
        public static extern UInt32 Document_ToUnknown(PdfDocumentSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Document_FromUnknown(PdfUnknownSafeHandle handle, out PdfDocumentSafeHandle data);

        #endregion

        #region DocumentInfo

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_ToUnknown(PdfDocumentInfoSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentInfo_FromUnknown(PdfUnknownSafeHandle handle, out PdfDocumentInfoSafeHandle data);

        #endregion

        #region DocumentSignatureSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_ToUnknown(PdfDocumentSignatureSettingsSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentSignatureSettings_FromUnknown(PdfUnknownSafeHandle handle, out PdfDocumentSignatureSettingsSafeHandle data);

        #endregion

        #region DocumentEncryptionSettings

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_ToUnknown(PdfDocumentEncryptionSettingsSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DocumentEncryptionSettings_FromUnknown(PdfUnknownSafeHandle handle, out PdfDocumentEncryptionSettingsSafeHandle data);

        #endregion

        #region Catalog

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_ToUnknown(PdfCatalogSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Catalog_FromUnknown(PdfUnknownSafeHandle handle, out PdfCatalogSafeHandle data);

        #endregion

        #region PageTree

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_ToUnknown(PdfPageTreeSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageTree_FromUnknown(PdfUnknownSafeHandle handle, out PdfPageTreeSafeHandle data);

        #endregion

        #region PageObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_ToUnknown(PdfPageObjectSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageObject_FromUnknown(PdfUnknownSafeHandle handle, out PdfPageObjectSafeHandle data);

        #endregion

        #region PageContents

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageContents_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageContents_ToUnknown(PdfPageContentsSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageContents_FromUnknown(PdfUnknownSafeHandle handle, out PdfPageContentsSafeHandle data);

        #endregion

        #region PageAnnotations

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_ToUnknown(PdfPageAnnotationsSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PageAnnotations_FromUnknown(PdfUnknownSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

        #endregion

        #region Annotation

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Annotation_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Annotation_ToUnknown(PdfAnnotationSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Annotation_FromUnknown(PdfUnknownSafeHandle handle, out PdfAnnotationSafeHandle data);

        #endregion

        #region Rectangle

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_ToUnknown(PdfRectangleSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Rectangle_FromUnknown(PdfUnknownSafeHandle handle, out PdfRectangleSafeHandle data);

        #endregion

        #region Date

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_ToUnknown(PdfDateSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Date_FromUnknown(PdfUnknownSafeHandle handle, out PdfDateSafeHandle data);

        #endregion

        #region Font

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_ToUnknown(PdfFontSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Font_FromUnknown(PdfUnknownSafeHandle handle, out PdfFontSafeHandle data);

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
        public static extern UInt32 FontMap_ToUnknown(PdfFontMapSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FontMap_FromUnknown(PdfUnknownSafeHandle handle, out PdfFontMapSafeHandle data);

        #endregion

        #region ResourceDictionary

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ResourceDictionary_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ResourceDictionary_ToUnknown(PdfResourceDictionarySafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ResourceDictionary_FromUnknown(PdfUnknownSafeHandle handle, out PdfResourceDictionarySafeHandle data);

        #endregion

        #region CharacterMap

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 CharacterMap_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 CharacterMap_ToUnknown(PdfCharacterMapSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 CharacterMap_FromUnknown(PdfUnknownSafeHandle handle, out PdfCharacterMapSafeHandle data);

        #endregion

        #region UnicodeCharacterMap

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_ToCharacterMap(PdfUnicodeCharacterMapSafeHandle handle, out PdfCharacterMapSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 UnicodeCharacterMap_FromCharacterMap(PdfCharacterMapSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);

        #endregion

        #region OutlineBase

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineBase_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineBase_ToUnknown(PdfOutlineBaseSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineBase_FromUnknown(PdfUnknownSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        #endregion

        #region Outline

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_ToOutlineBase(PdfOutlineSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Outline_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineSafeHandle data);

        #endregion

        #region OutlineItem

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_ToOutlineBase(PdfOutlineItemSafeHandle handle, out PdfOutlineBaseSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutlineItem_FromOutlineBase(PdfOutlineBaseSafeHandle handle, out PdfOutlineItemSafeHandle data);

        #endregion
    }
}

#endif
