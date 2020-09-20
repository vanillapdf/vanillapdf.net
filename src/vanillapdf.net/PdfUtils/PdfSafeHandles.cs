using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using vanillapdf.net.Utils;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net
{
    internal abstract class PdfSafeHandle : SafeHandle
    {
        protected abstract GenericReleaseDelgate ReleaseDelegate { get; }

        public PdfSafeHandle() : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            [PrePrepareMethod]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            get { return (handle == IntPtr.Zero); }
        }

        [PrePrepareMethod]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            if (ReleaseDelegate == null) {
                return false;
            }

            return (ReleaseDelegate(handle) == PdfReturnValues.ERROR_SUCCESS);
        }

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention), SuppressUnmanagedCodeSecurity]
        protected delegate UInt32 GenericReleaseDelgate(IntPtr handle);
    }

    #region Utils

    internal sealed class PdfUnknownSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("IUnknown_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfBufferSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Buffer_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Buffer_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Buffer_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfBufferSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfBufferSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfBufferSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfBufferSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfBufferSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfInputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("InputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("InputStream_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("InputStream_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfInputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfInputStreamSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfInputStreamSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfOutputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("OutputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("OutputStream_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("OutputStream_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfOutputStreamSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #endregion

    #region Syntax

    internal sealed class PdfFileSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("File_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("File_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("File_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfFileSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfFileSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfFileSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfFileSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfFileSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Xref_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Xref_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Xref_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfXrefSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfXrefSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefIteratorSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefIterator_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefIterator_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefIterator_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefIteratorSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfXrefIteratorSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfXrefIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefChainSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefChain_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefChain_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefChain_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefChainSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefChainSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfXrefChainSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefChainSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfXrefChainSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefChainIteratorSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefChainIterator_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefChainIterator_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefChainIterator_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefChainIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfXrefChainIteratorSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefChainIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfXrefChainIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefEntrySafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefEntry_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefEntry_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefEntry_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefEntrySafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefEntrySafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefEntrySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfXrefFreeEntrySafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefFreeEntry_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToEntry = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefFreeEntry_ToEntry");
        private static ConvertFromUnknownDelegate Convert_FromEntry = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefFreeEntry_FromEntry");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefFreeEntrySafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefFreeEntrySafeHandle data);

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefFreeEntrySafeHandle handle)
        {
            UInt32 result = Convert_ToEntry(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefFreeEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = Convert_FromEntry(handle, out PdfXrefFreeEntrySafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefUsedEntry_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToEntryDelegate Convert_ToEntry = LibraryInstance.GetFunction<ConvertToEntryDelegate>("XrefUsedEntry_ToEntry");
        private static ConvertFromEntryDelegate Convert_FromEntry = LibraryInstance.GetFunction<ConvertFromEntryDelegate>("XrefUsedEntry_FromEntry");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToEntryDelegate(PdfXrefUsedEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromEntryDelegate(PdfXrefEntrySafeHandle handle, out PdfXrefUsedEntrySafeHandle data);

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefUsedEntrySafeHandle handle)
        {
            UInt32 result = Convert_ToEntry(handle, out PdfXrefEntrySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefUsedEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = Convert_FromEntry(handle, out PdfXrefUsedEntrySafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("XrefCompressedEntry_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToEntry = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("XrefCompressedEntry_ToEntry");
        private static ConvertFromUnknownDelegate Convert_FromEntry = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("XrefCompressedEntry_FromEntry");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfXrefCompressedEntrySafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfXrefCompressedEntrySafeHandle data);

        public static implicit operator PdfXrefEntrySafeHandle(PdfXrefCompressedEntrySafeHandle handle)
        {
            UInt32 result = Convert_ToEntry(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfXrefCompressedEntrySafeHandle(PdfXrefEntrySafeHandle handle)
        {
            UInt32 result = Convert_FromEntry(handle, out PdfXrefCompressedEntrySafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Object_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Object_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Object_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfObjectSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfObjectSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfIntegerObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("IntegerObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("IntegerObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("IntegerObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfIntegerObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfIntegerObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfIntegerObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfIntegerObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfIntegerObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ArrayObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("ArrayObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("ArrayObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfArrayObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfArrayObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfArrayObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfArrayObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfArrayObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("BooleanObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("BooleanObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("BooleanObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfBooleanObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfBooleanObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfBooleanObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfBooleanObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfBooleanObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("DictionaryObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("DictionaryObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("DictionaryObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfDictionaryObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfDictionaryObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDictionaryObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfDictionaryObjectSafeHandle data);
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

    internal sealed class PdfIndirectReferenceObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("IndirectReferenceObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("IndirectReferenceObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("IndirectReferenceObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfIndirectReferenceObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfIndirectReferenceObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfIndirectReferenceObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfIndirectReferenceObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("NameObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("NameObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("NameObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfNameObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfNameObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfNameObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfNameObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfNameObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("NullObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("NullObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("NullObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfNullObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfNullObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfNullObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfNullObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfNullObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("RealObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("RealObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("RealObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfRealObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfRealObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfRealObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfRealObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfRealObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("StreamObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("StreamObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("StreamObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfStreamObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfStreamObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfStreamObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfStreamObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfStreamObjectSafeHandle data);
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
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("StringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToObject = LibraryInstance.GetFunction<ConvertToObjectDelegate>("StringObject_ToObject");
        private static ConvertFromObjectDelegate Convert_FromObject = LibraryInstance.GetFunction<ConvertFromObjectDelegate>("StringObject_FromObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfStringObjectSafeHandle handle, out PdfObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromObjectDelegate(PdfObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        public static implicit operator PdfObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToObject(handle, out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfStringObjectSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromObject(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfStringObjectSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }

        public static implicit operator PdfStringObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfObjectSafeHandle)handle;
        }
    }

    internal sealed class PdfLiteralStringObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("LiteralStringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToStringDelegate Convert_ToString = LibraryInstance.GetFunction<ConvertToStringDelegate>("LiteralStringObject_ToStringObject");
        private static ConvertFromStringDelegate Convert_FromString = LibraryInstance.GetFunction<ConvertFromStringDelegate>("LiteralStringObject_FromStringObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToStringDelegate(PdfLiteralStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromStringDelegate(PdfStringObjectSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

        public static implicit operator PdfStringObjectSafeHandle(PdfLiteralStringObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToString(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfLiteralStringObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromString(handle, out PdfLiteralStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfLiteralStringObjectSafeHandle handle)
        {
            return (PdfStringObjectSafeHandle)handle;
        }

        public static implicit operator PdfLiteralStringObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfStringObjectSafeHandle)handle;
        }
    }

    internal sealed class PdfHexadecimalStringObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("HexadecimalStringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToObjectDelegate Convert_ToString = LibraryInstance.GetFunction<ConvertToObjectDelegate>("HexadecimalStringObject_ToStringObject");
        private static ConvertFromStringDelegate Convert_FromString = LibraryInstance.GetFunction<ConvertFromStringDelegate>("HexadecimalStringObject_FromStringObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToObjectDelegate(PdfHexadecimalStringObjectSafeHandle handle, out PdfStringObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromStringDelegate(PdfStringObjectSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        public static implicit operator PdfStringObjectSafeHandle(PdfHexadecimalStringObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToString(handle, out PdfStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfHexadecimalStringObjectSafeHandle(PdfStringObjectSafeHandle handle)
        {
            UInt32 result = Convert_FromString(handle, out PdfHexadecimalStringObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfHexadecimalStringObjectSafeHandle handle)
        {
            return (PdfStringObjectSafeHandle)handle;
        }

        public static implicit operator PdfHexadecimalStringObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfStringObjectSafeHandle)handle;
        }
    }

    #endregion

    #region Semantics

    internal sealed class PdfAnnotationSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Annotation_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Annotation_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Annotation_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfAnnotationSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfAnnotationSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfAnnotationSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfAnnotationSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfAnnotationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfCatalogSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Catalog_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Catalog_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Catalog_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfCatalogSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfCatalogSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfCatalogSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfCatalogSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfCatalogSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentsSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Contents_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Contents_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Contents_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfDocumentSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Document_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Document_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Document_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfDocumentSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfDocumentSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfDocumentSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfDocumentSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfDocumentSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageAnnotationsSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageAnnotations_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageAnnotations_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageAnnotations_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageAnnotationsSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageAnnotationsSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageAnnotationsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageAnnotationsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageObject_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageObject_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageObjectSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageObjectSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPageTreeSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageTree_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("PageTree_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("PageTree_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfPageTreeSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfPageTreeSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfPageTreeSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPageTreeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfPageTreeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #endregion
}
