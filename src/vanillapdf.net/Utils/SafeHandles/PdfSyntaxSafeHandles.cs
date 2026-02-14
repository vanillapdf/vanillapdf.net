using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfFileSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.File_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfFileWriterSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FileWriter_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfFileWriterObserverSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FileWriterObserver_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Xref_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefChainSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefChain_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefChainIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefChainIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefEntrySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefEntry_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfXrefFreeEntrySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefFreeEntry_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefFreeEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefFreeEntry_ToEntry(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefFreeEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefFreeEntry_FromEntry(handle, out PdfXrefFreeEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefUsedEntrySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefUsedEntry_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefUsedEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefUsedEntry_ToEntry(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefUsedEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefUsedEntry_FromEntry(handle, out PdfXrefUsedEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefCompressedEntrySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefCompressedEntry_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefCompressedEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_ToEntry(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefCompressedEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_FromEntry(handle, out PdfXrefCompressedEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal class PdfObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Object_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfIntegerObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.IntegerObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfIntegerObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.IntegerObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfIntegerObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.IntegerObject_FromObject(handle, out PdfIntegerObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfArrayObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ArrayObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfArrayObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.ArrayObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfArrayObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.ArrayObject_FromObject(handle, out PdfArrayObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfBooleanObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.BooleanObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfBooleanObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.BooleanObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfBooleanObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.BooleanObject_FromObject(handle, out PdfBooleanObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDictionaryObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DictionaryObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfDictionaryObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.DictionaryObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDictionaryObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.DictionaryObject_FromObject(handle, out PdfDictionaryObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfDictionaryObjectIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DictionaryObjectIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfIndirectReferenceObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.IndirectReferenceObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfIndirectReferenceObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfIndirectReferenceObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_FromObject(handle, out PdfIndirectReferenceObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfNameObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NameObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfNameObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.NameObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfNameObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.NameObject_FromObject(handle, out PdfNameObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfNullObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.NullObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfNullObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.NullObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfNullObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.NullObject_FromObject(handle, out PdfNullObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfRealObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.RealObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfRealObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.RealObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfRealObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.RealObject_FromObject(handle, out PdfRealObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfStreamObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.StreamObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfStreamObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.StreamObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfStreamObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.StreamObject_FromObject(handle, out PdfStreamObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfStringObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.StringObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.StringObject_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfStringObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.StringObject_FromObject(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfLiteralStringObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.LiteralStringObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfStringObjectSafeHandle(PdfLiteralStringObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.LiteralStringObject_ToStringObject(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfLiteralStringObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.LiteralStringObject_FromStringObject(handle, out PdfLiteralStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfHexadecimalStringObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.HexadecimalStringObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfStringObjectSafeHandle(PdfHexadecimalStringObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_ToStringObject(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfHexadecimalStringObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_FromStringObject(handle, out PdfHexadecimalStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    #region Attributes

    internal sealed class PdfBaseObjectAttributeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.BaseObjectAttribute_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfImageMetadataObjectAttributeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ImageMetadataObjectAttribute_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfBaseObjectAttributeSafeHandle(PdfImageMetadataObjectAttributeSafeHandle handle)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_ToBaseAttribute(handle, out PdfBaseObjectAttributeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfImageMetadataObjectAttributeSafeHandle(PdfBaseObjectAttributeSafeHandle handle)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_FromBaseAttribute(handle, out PdfImageMetadataObjectAttributeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfObjectAttributeListSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ObjectAttributeList_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    #endregion
}
