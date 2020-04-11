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
        //private GenericReleaseDelgate ReleaseMethod { get; }
        protected abstract GenericReleaseDelgate ReleaseDelegate { get; }

        public PdfSafeHandle() : base(IntPtr.Zero, true)
        {
        }

        //public PdfSafeHandle(IntPtr value) : base(IntPtr.Zero, true)
        //{
        //    if (value == IntPtr.Zero) {
        //        throw new ArgumentNullException(nameof(value));
        //    }

        //    SetHandle(value);
        //    ReleaseMethod = LibraryInstance.GetFunction<GenericReleaseDelgate>(ReleaseFunctionName);
        //}

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
        public delegate UInt32 GenericReleaseDelgate(IntPtr handle);
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
    }

    internal sealed class PdfInputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("InputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    #endregion

    #region Syntax

    internal sealed class PdfFileSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("File_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfXrefSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Xref_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Object_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        public delegate UInt32 ToUnknownDelgate(IntPtr handle, out PdfUnknownSafeHandle data);

        private static ConvertToUnknownDelegate<PdfObjectSafeHandle> Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate<PdfObjectSafeHandle>>("Object_ToUnknown");
        public static implicit operator PdfUnknownSafeHandle(PdfObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
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
    }

    internal sealed class PdfBooleanObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("BooleanObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfDictionaryObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("DictionaryObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfIndirectObjectReferenceSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("IndirectObjectReference_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfNameObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("NameObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfNullObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("NullObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfRealObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("RealObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfStreamObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("StreamObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfStringObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("StringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfLiteralStringObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("LiteralStringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfHexadecimalStringObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("HexadecimalStringObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    #endregion

    #region Semantics

    internal sealed class PdfAnnotationSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Annotation_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfCatalogSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Catalog_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfContentsSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Contents_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfDocumentSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Document_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfPageAnnotationsSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageAnnotations_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfPageObjectSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    internal sealed class PdfPageTreeSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PageTree_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;
    }

    #endregion
}
