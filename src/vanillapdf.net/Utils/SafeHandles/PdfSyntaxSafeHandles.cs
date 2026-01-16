using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfFileSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.File_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfFileSafeHandle handle)
        {
            UInt32 result = NativeMethods.File_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFileSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.File_FromUnknown(handle, out PdfFileSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfFileWriterSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FileWriter_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfFileWriterSafeHandle handle)
        {
            UInt32 result = NativeMethods.FileWriter_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFileWriterSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.FileWriter_FromUnknown(handle, out PdfFileWriterSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfFileWriterObserverSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.FileWriterObserver_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfFileWriterObserverSafeHandle handle)
        {
            UInt32 result = NativeMethods.FileWriterObserver_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfFileWriterObserverSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.FileWriterObserver_FromUnknown(handle, out PdfFileWriterObserverSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Xref_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfXrefSafeHandle handle)
        {
            UInt32 result = NativeMethods.Xref_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Xref_FromUnknown(handle, out PdfXrefSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfXrefIteratorSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefIterator_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefIterator_FromUnknown(handle, out PdfXrefIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefChainSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefChain_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfXrefChainSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefChain_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefChainSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefChain_FromUnknown(handle, out PdfXrefChainSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefChainIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefChainIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfXrefChainIteratorSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefChainIterator_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefChainIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefChainIterator_FromUnknown(handle, out PdfXrefChainIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfXrefEntrySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.XrefEntry_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefEntry_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfXrefEntrySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.XrefEntry_FromUnknown(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfXrefFreeEntrySafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
        }

        public static implicit operator PdfXrefFreeEntrySafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfXrefUsedEntrySafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
        }

        public static implicit operator PdfXrefUsedEntrySafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfXrefCompressedEntrySafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
        }

        public static implicit operator PdfXrefCompressedEntrySafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfXrefEntrySafeHandle)handle;
        }
    }

    internal class PdfObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Object_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.Object_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Object_FromUnknown(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfIntegerObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfIntegerObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfArrayObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfArrayObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfBooleanObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfBooleanObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfDictionaryObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfDictionaryObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }
    }

    internal sealed class PdfDictionaryObjectIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.DictionaryObjectIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfDictionaryObjectIteratorSafeHandle handle)
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfDictionaryObjectIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_FromUnknown(handle, out PdfDictionaryObjectIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfIndirectReferenceObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfIndirectReferenceObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfNameObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfNameObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfNullObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfNullObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfRealObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfRealObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfStreamObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfStreamObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
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

        public static implicit operator PdfUnknownSafeHandle(PdfBaseObjectAttributeSafeHandle handle)
        {
            UInt32 result = NativeMethods.BaseObjectAttribute_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfBaseObjectAttributeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.BaseObjectAttribute_FromUnknown(handle, out PdfBaseObjectAttributeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfObjectAttributeListSafeHandle handle)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfObjectAttributeListSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_FromUnknown(handle, out PdfObjectAttributeListSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    #endregion
}
