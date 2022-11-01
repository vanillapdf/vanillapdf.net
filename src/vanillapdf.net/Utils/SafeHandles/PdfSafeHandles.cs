using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal abstract class PdfSafeHandle : SafeHandle
    {
        internal static int _counter;
        internal static int Counter { get => _counter; private set => _counter = value; }

        protected abstract GenericReleaseDelgate ReleaseDelegate { get; }

        public PdfSafeHandle() : base(IntPtr.Zero, true)
        {
            Interlocked.Increment(ref _counter);
        }

        internal void DangerousSetHandle(IntPtr newHandle)
        {
            handle = newHandle;
        }

        public override bool IsInvalid
        {
#if NETSTANDARD2_0
            [PrePrepareMethod]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
#endif
            get { return (handle == IntPtr.Zero); }
        }

#if NETSTANDARD2_0
        [PrePrepareMethod]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        protected override bool ReleaseHandle()
        {
            if (ReleaseDelegate == null) {
                return false;
            }

            Interlocked.Decrement(ref _counter);

            return (ReleaseDelegate(handle) == PdfReturnValues.ERROR_SUCCESS);
        }

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention), SuppressUnmanagedCodeSecurity]
        protected delegate UInt32 GenericReleaseDelgate(IntPtr handle);
    }
}
