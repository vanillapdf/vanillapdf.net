#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region File

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_Release(IntPtr handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 File_Open(string filename, out PdfFileSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 File_OpenStream(PdfInputOutputStreamSafeHandle inputStream, string name, out PdfFileSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 File_Create(string filename, out PdfFileSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 File_CreateStream(PdfInputOutputStreamSafeHandle inputStream, string name, out PdfFileSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_Initialize(PdfFileSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_GetVersion(PdfFileSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_GetFilename(PdfFileSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_IsEncrypted(PdfFileSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 File_SetEncryptionPassword(PdfFileSafeHandle handle, string password);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_XrefChain(PdfFileSafeHandle handle, out PdfXrefChainSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 File_AllocateNewEntry(PdfFileSafeHandle handle, out PdfXrefUsedEntrySafeHandle data);

        #endregion

        #region FileWriter

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_Create(out PdfFileWriterSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_Write(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_WriteIncremental(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_Subscribe(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriter_Unsubscribe(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);

        #endregion

        #region FileWriterObserver

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriterObserver_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 FileWriterObserver_CreateCustom(
            IntPtr on_initializing,
            IntPtr on_finalizing,
            IntPtr on_before_object_write,
            IntPtr on_after_object_write,
            IntPtr on_before_object_offset_recalculation,
            IntPtr on_after_object_offset_recalculation,
            IntPtr on_before_entry_offset_recalculation,
            IntPtr on_after_entry_offset_recalculation,
            IntPtr on_before_output_flush,
            IntPtr on_after_output_flush,
            IntPtr userdata,
            out PdfFileWriterObserverSafeHandle data);

        #endregion

        #region Object

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_GetObjectType(PdfObjectSafeHandle handle, out PdfSyntax.PdfObjectType data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_GetOffset(PdfObjectSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_ToString(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_ToPdf(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Object_GetAttributeList(PdfObjectSafeHandle handle, out PdfObjectAttributeListSafeHandle data);

        #endregion

        #region ArrayObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_ToObject(PdfArrayObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_FromObject(PdfObjectSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Create(out PdfArrayObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_GetSize(PdfArrayObjectSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_GetValue(PdfArrayObjectSafeHandle handle, UIntPtr index, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_SetValue(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Append(PdfArrayObjectSafeHandle handle, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Insert(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Remove(PdfArrayObjectSafeHandle handle, UIntPtr index);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ArrayObject_Clear(PdfArrayObjectSafeHandle handle);

        #endregion

        #region DictionaryObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_ToObject(PdfDictionaryObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_FromObject(PdfObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Create(out PdfDictionaryObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_GetSize(PdfDictionaryObjectSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Find(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_TryFind(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, [MarshalAs(UnmanagedType.U1)] out bool contains, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Contains(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_GetIterator(PdfDictionaryObjectSafeHandle handle, out PdfDictionaryObjectIteratorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Remove(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Clear(PdfDictionaryObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObject_Insert(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, PdfObjectSafeHandle data, [MarshalAs(UnmanagedType.U1)] bool overwrite);

        #endregion

        #region DictionaryObjectIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObjectIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObjectIterator_GetKey(PdfDictionaryObjectIteratorSafeHandle handle, out PdfNameObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObjectIterator_GetValue(PdfDictionaryObjectIteratorSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObjectIterator_Next(PdfDictionaryObjectIteratorSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 DictionaryObjectIterator_IsValid(PdfDictionaryObjectIteratorSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        #endregion

        #region IntegerObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_ToObject(PdfIntegerObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_FromObject(PdfObjectSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_Create(out PdfIntegerObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_GetIntegerValue(PdfIntegerObjectSafeHandle handle, out Int64 value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_SetIntegerValue(PdfIntegerObjectSafeHandle handle, Int64 value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_GetUnsignedIntegerValue(PdfIntegerObjectSafeHandle handle, out UInt64 value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IntegerObject_SetUnsignedIntegerValue(PdfIntegerObjectSafeHandle handle, UInt64 value);

        #endregion

        #region RealObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_ToObject(PdfRealObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_FromObject(PdfObjectSafeHandle handle, out PdfRealObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_Create(out PdfRealObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_GetValue(PdfRealObjectSafeHandle handle, out double value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 RealObject_SetValue(PdfRealObjectSafeHandle handle, double value);

        #endregion

        #region BooleanObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_ToObject(PdfBooleanObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_FromObject(PdfObjectSafeHandle handle, out PdfBooleanObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_Create(out PdfBooleanObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_GetValue(PdfBooleanObjectSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BooleanObject_SetValue(PdfBooleanObjectSafeHandle handle, [MarshalAs(UnmanagedType.U1)] bool data);

        #endregion

        #region NullObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 NullObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NullObject_ToObject(PdfNullObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NullObject_FromObject(PdfObjectSafeHandle handle, out PdfNullObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NullObject_Create(out PdfNullObjectSafeHandle handle);

        #endregion

        #region NameObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_ToObject(PdfNameObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_FromObject(PdfObjectSafeHandle handle, out PdfNameObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_Create(out PdfNameObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 NameObject_CreateFromEncodedString(string data, out PdfNameObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 NameObject_CreateFromDecodedString(string data, out PdfNameObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_GetValue(PdfNameObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_SetValue(PdfNameObjectSafeHandle handle, PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_Equals(PdfNameObjectSafeHandle handle, PdfNameObjectSafeHandle other, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 NameObject_Hash(PdfNameObjectSafeHandle handle, out UIntPtr data);

        #endregion

        #region StringObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_ToObject(PdfStringObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_FromObject(PdfObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_GetStringType(PdfStringObjectSafeHandle handle, out PdfSyntax.PdfStringType data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_GetValue(PdfStringObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StringObject_SetValue(PdfStringObjectSafeHandle handle, PdfBufferSafeHandle value);

        #endregion

        #region LiteralStringObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_ToStringObject(PdfLiteralStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_FromStringObject(PdfStringObjectSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_Create(out PdfLiteralStringObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_CreateFromEncodedBuffer(PdfBufferSafeHandle value, out PdfLiteralStringObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 LiteralStringObject_CreateFromEncodedString(string value, out PdfLiteralStringObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LiteralStringObject_CreateFromDecodedBuffer(PdfBufferSafeHandle value, out PdfLiteralStringObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 LiteralStringObject_CreateFromDecodedString(string value, out PdfLiteralStringObjectSafeHandle handle);

        #endregion

        #region HexadecimalStringObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_ToStringObject(PdfHexadecimalStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_FromStringObject(PdfStringObjectSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_Create(out PdfHexadecimalStringObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_CreateFromEncodedBuffer(PdfBufferSafeHandle value, out PdfHexadecimalStringObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 HexadecimalStringObject_CreateFromEncodedString(string value, out PdfHexadecimalStringObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 HexadecimalStringObject_CreateFromDecodedBuffer(PdfBufferSafeHandle value, out PdfHexadecimalStringObjectSafeHandle handle);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 HexadecimalStringObject_CreateFromDecodedString(string value, out PdfHexadecimalStringObjectSafeHandle handle);

        #endregion

        #region StreamObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_ToObject(PdfStreamObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_FromObject(PdfObjectSafeHandle handle, out PdfStreamObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_Create(out PdfStreamObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_GetHeader(PdfStreamObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_SetHeader(PdfStreamObjectSafeHandle handle, PdfDictionaryObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_GetBodyRaw(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_GetBody(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 StreamObject_SetBody(PdfStreamObjectSafeHandle handle, PdfBufferSafeHandle value);

        #endregion

        #region IndirectReferenceObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_ToObject(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_FromObject(PdfObjectSafeHandle handle, out PdfIndirectReferenceObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_Create(out PdfIndirectReferenceObjectSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_GetReferencedObjectNumber(PdfIndirectReferenceObjectSafeHandle handle, out UInt64 value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_GetReferencedGenerationNumber(PdfIndirectReferenceObjectSafeHandle handle, out UInt16 value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_GetReferencedObject(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IndirectReferenceObject_SetReferencedObject(PdfIndirectReferenceObjectSafeHandle handle, PdfObjectSafeHandle value);

        #endregion

        #region BaseObjectAttribute

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseObjectAttribute_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseObjectAttribute_GetAttributeType(PdfBaseObjectAttributeSafeHandle handle, out PdfSyntax.PdfObjectAttributeType data);

        #endregion

        #region ImageMetadataObjectAttribute

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_ToBaseAttribute(PdfImageMetadataObjectAttributeSafeHandle handle, out PdfBaseObjectAttributeSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_FromBaseAttribute(PdfBaseObjectAttributeSafeHandle handle, out PdfImageMetadataObjectAttributeSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_Create(out PdfImageMetadataObjectAttributeSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_GetColorComponents(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_SetColorComponents(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_GetColorSpace(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_SetColorSpace(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_GetWidth(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_SetWidth(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_GetHeight(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ImageMetadataObjectAttribute_SetHeight(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        #endregion

        #region ObjectAttributeList

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Create(out PdfObjectAttributeListSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Add(PdfObjectAttributeListSafeHandle handle, PdfBaseObjectAttributeSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Remove(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Contains(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key, [MarshalAs(UnmanagedType.U1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Get(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key, out PdfBaseObjectAttributeSafeHandle result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectAttributeList_Clear(PdfObjectAttributeListSafeHandle handle);

        #endregion
    }
}

#endif
