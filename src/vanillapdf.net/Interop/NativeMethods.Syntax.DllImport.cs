#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region File

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_ToUnknown(PdfFileSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_FromUnknown(PdfUnknownSafeHandle handle, out PdfFileSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_Open(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string filename,
            out PdfFileSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_OpenStream(
            PdfInputOutputStreamSafeHandle inputStream,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string name,
            out PdfFileSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_Create(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string filename,
            out PdfFileSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_CreateStream(
            PdfInputOutputStreamSafeHandle inputStream,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string name,
            out PdfFileSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_Initialize(PdfFileSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_GetVersion(PdfFileSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_GetFilename(PdfFileSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_IsEncrypted(PdfFileSafeHandle handle, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_SetEncryptionPassword(
            PdfFileSafeHandle handle,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string password);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_XrefChain(PdfFileSafeHandle handle, out PdfXrefChainSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 File_AllocateNewEntry(PdfFileSafeHandle handle, out PdfXrefUsedEntrySafeHandle data);

        #endregion

        #region FileWriter

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_ToUnknown(PdfFileWriterSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_FromUnknown(PdfUnknownSafeHandle handle, out PdfFileWriterSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_Create(out PdfFileWriterSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_Write(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_WriteIncremental(PdfFileWriterSafeHandle handle, PdfFileSafeHandle source, PdfFileSafeHandle destination);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_Subscribe(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriter_Unsubscribe(PdfFileWriterSafeHandle handle, PdfFileWriterObserverSafeHandle data);

        #endregion

        #region FileWriterObserver

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriterObserver_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriterObserver_ToUnknown(PdfFileWriterObserverSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriterObserver_FromUnknown(PdfUnknownSafeHandle handle, out PdfFileWriterObserverSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 FileWriterObserver_CreateCustom(
            OnInitializingDelegate on_initializing,
            OnFinalizingDelegate on_finalizing,
            OnBeforeObjectWriteDelegate on_before_object_write,
            OnAfterObjectWriteDelegate on_after_object_write,
            OnBeforeObjectOffsetRecalculationDelegate on_before_object_offset_recalculation,
            OnAfterObjectOffsetRecalculationDelegate on_after_object_offset_recalculation,
            OnBeforeEntryOffsetRecalculationDelegate on_before_entry_offset_recalculation,
            OnAfterEntryOffsetRecalculationDelegate on_after_entry_offset_recalculation,
            OnBeforeOutputFlushDelegate on_before_output_flush,
            OnAfterOutputFlushDelegate on_after_output_flush,
            IntPtr userdata,
            out PdfFileWriterObserverSafeHandle data);

        #endregion

        #region Object

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_ToUnknown(PdfObjectSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_FromUnknown(PdfUnknownSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_GetObjectType(PdfObjectSafeHandle handle, out PdfSyntax.PdfObjectType data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_GetOffset(PdfObjectSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_ToString(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_ToPdf(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Object_GetAttributeList(PdfObjectSafeHandle handle, out PdfObjectAttributeListSafeHandle data);

        #endregion

        #region ArrayObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_ToObject(PdfArrayObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_FromObject(PdfObjectSafeHandle handle, out PdfArrayObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Create(out PdfArrayObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_GetSize(PdfArrayObjectSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_GetValue(PdfArrayObjectSafeHandle handle, UIntPtr index, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_SetValue(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Append(PdfArrayObjectSafeHandle handle, PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Insert(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Remove(PdfArrayObjectSafeHandle handle, UIntPtr index);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ArrayObject_Clear(PdfArrayObjectSafeHandle handle);

        #endregion

        #region DictionaryObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_ToObject(PdfDictionaryObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_FromObject(PdfObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Create(out PdfDictionaryObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_GetSize(PdfDictionaryObjectSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Find(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_TryFind(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool contains, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Contains(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_GetIterator(PdfDictionaryObjectSafeHandle handle, out PdfDictionaryObjectIteratorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Remove(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Clear(PdfDictionaryObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObject_Insert(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, PdfObjectSafeHandle data, bool overwrite);

        #endregion

        #region DictionaryObjectIterator

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_ToUnknown(PdfDictionaryObjectIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfDictionaryObjectIteratorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_GetKey(PdfDictionaryObjectIteratorSafeHandle handle, out PdfNameObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_GetValue(PdfDictionaryObjectIteratorSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_Next(PdfDictionaryObjectIteratorSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 DictionaryObjectIterator_IsValid(PdfDictionaryObjectIteratorSafeHandle handle, out bool data);

        #endregion

        #region IntegerObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_ToObject(PdfIntegerObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_FromObject(PdfObjectSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_Create(out PdfIntegerObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_GetIntegerValue(PdfIntegerObjectSafeHandle handle, out Int64 value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_SetIntegerValue(PdfIntegerObjectSafeHandle handle, Int64 value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_GetUnsignedIntegerValue(PdfIntegerObjectSafeHandle handle, out UInt64 value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IntegerObject_SetUnsignedIntegerValue(PdfIntegerObjectSafeHandle handle, UInt64 value);

        #endregion

        #region RealObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_ToObject(PdfRealObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_FromObject(PdfObjectSafeHandle handle, out PdfRealObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_Create(out PdfRealObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_GetValue(PdfRealObjectSafeHandle handle, out double value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 RealObject_SetValue(PdfRealObjectSafeHandle handle, double value);

        #endregion

        #region BooleanObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_ToObject(PdfBooleanObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_FromObject(PdfObjectSafeHandle handle, out PdfBooleanObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_Create(out PdfBooleanObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_GetValue(PdfBooleanObjectSafeHandle handle, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BooleanObject_SetValue(PdfBooleanObjectSafeHandle handle, bool data);

        #endregion

        #region NullObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NullObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NullObject_ToObject(PdfNullObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NullObject_FromObject(PdfObjectSafeHandle handle, out PdfNullObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NullObject_Create(out PdfNullObjectSafeHandle handle);

        #endregion

        #region NameObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_ToObject(PdfNameObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_FromObject(PdfObjectSafeHandle handle, out PdfNameObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_Create(out PdfNameObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_CreateFromEncodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string data,
            out PdfNameObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_CreateFromDecodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string data,
            out PdfNameObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_GetValue(PdfNameObjectSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_SetValue(PdfNameObjectSafeHandle handle, PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_Equals(PdfNameObjectSafeHandle handle, PdfNameObjectSafeHandle other, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 NameObject_Hash(PdfNameObjectSafeHandle handle, out UIntPtr data);

        #endregion

        #region StringObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_ToObject(PdfStringObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_FromObject(PdfObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_GetStringType(PdfStringObjectSafeHandle handle, out PdfSyntax.PdfStringType data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_GetValue(PdfStringObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StringObject_SetValue(PdfStringObjectSafeHandle handle, PdfBufferSafeHandle value);

        #endregion

        #region LiteralStringObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_ToStringObject(PdfLiteralStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_FromStringObject(PdfStringObjectSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_Create(out PdfLiteralStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_CreateFromEncodedBuffer(PdfBufferSafeHandle value, out PdfLiteralStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_CreateFromEncodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string value,
            out PdfLiteralStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_CreateFromDecodedBuffer(PdfBufferSafeHandle value, out PdfLiteralStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LiteralStringObject_CreateFromDecodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string value,
            out PdfLiteralStringObjectSafeHandle handle);

        #endregion

        #region HexadecimalStringObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_ToStringObject(PdfHexadecimalStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_FromStringObject(PdfStringObjectSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_Create(out PdfHexadecimalStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_CreateFromEncodedBuffer(PdfBufferSafeHandle value, out PdfHexadecimalStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_CreateFromEncodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string value,
            out PdfHexadecimalStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_CreateFromDecodedBuffer(PdfBufferSafeHandle value, out PdfHexadecimalStringObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 HexadecimalStringObject_CreateFromDecodedString(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string value,
            out PdfHexadecimalStringObjectSafeHandle handle);

        #endregion

        #region StreamObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_ToObject(PdfStreamObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_FromObject(PdfObjectSafeHandle handle, out PdfStreamObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_Create(out PdfStreamObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_GetHeader(PdfStreamObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_SetHeader(PdfStreamObjectSafeHandle handle, PdfDictionaryObjectSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_GetBodyRaw(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_GetBody(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 StreamObject_SetBody(PdfStreamObjectSafeHandle handle, PdfBufferSafeHandle value);

        #endregion

        #region IndirectReferenceObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_ToObject(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_FromObject(PdfObjectSafeHandle handle, out PdfIndirectReferenceObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_Create(out PdfIndirectReferenceObjectSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_GetReferencedObjectNumber(PdfIndirectReferenceObjectSafeHandle handle, out UInt64 value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_GetReferencedGenerationNumber(PdfIndirectReferenceObjectSafeHandle handle, out UInt16 value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_GetReferencedObject(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle value);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IndirectReferenceObject_SetReferencedObject(PdfIndirectReferenceObjectSafeHandle handle, PdfObjectSafeHandle value);

        #endregion

        #region BaseObjectAttribute

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseObjectAttribute_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseObjectAttribute_ToUnknown(PdfBaseObjectAttributeSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseObjectAttribute_FromUnknown(PdfUnknownSafeHandle handle, out PdfBaseObjectAttributeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseObjectAttribute_GetAttributeType(PdfBaseObjectAttributeSafeHandle handle, out PdfSyntax.PdfObjectAttributeType data);

        #endregion

        #region ImageMetadataObjectAttribute

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_ToBaseAttribute(PdfImageMetadataObjectAttributeSafeHandle handle, out PdfBaseObjectAttributeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_FromBaseAttribute(PdfBaseObjectAttributeSafeHandle handle, out PdfImageMetadataObjectAttributeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_Create(out PdfImageMetadataObjectAttributeSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_GetColorComponents(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_SetColorComponents(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_GetColorSpace(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_SetColorSpace(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_GetWidth(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_SetWidth(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_GetHeight(PdfImageMetadataObjectAttributeSafeHandle handle, out int data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ImageMetadataObjectAttribute_SetHeight(PdfImageMetadataObjectAttributeSafeHandle handle, int data);

        #endregion

        #region ObjectAttributeList

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_ToUnknown(PdfObjectAttributeListSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_FromUnknown(PdfUnknownSafeHandle handle, out PdfObjectAttributeListSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Create(out PdfObjectAttributeListSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Add(PdfObjectAttributeListSafeHandle handle, PdfBaseObjectAttributeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Remove(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Contains(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key, out bool result);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Get(PdfObjectAttributeListSafeHandle handle, PdfSyntax.PdfObjectAttributeType key, out PdfBaseObjectAttributeSafeHandle result);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ObjectAttributeList_Clear(PdfObjectAttributeListSafeHandle handle);

        #endregion
    }
}

#endif
