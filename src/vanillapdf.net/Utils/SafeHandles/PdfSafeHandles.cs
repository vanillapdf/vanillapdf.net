using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using vanillapdf.net.Utils;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net.Utils.SafeHandles
{
    internal abstract class PdfSafeHandle : SafeHandle
    {
        protected abstract GenericReleaseDelgate ReleaseDelegate { get; }

        public PdfSafeHandle() : base(IntPtr.Zero, true)
        {
        }

        internal void DangerousSetHandle(IntPtr newHandle)
        {
            handle = newHandle;
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
}
