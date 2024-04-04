﻿using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal abstract class PdfSafeHandle : SafeHandle
    {
        internal static int _counter;
        internal static int Counter { get => _counter; private set => _counter = value; }

        private protected bool _disposed = false;

        protected abstract GenericReleaseDelgate ReleaseDelegate { get; }

        public PdfSafeHandle() : base(IntPtr.Zero, true)
        {
            IncrementCounter();
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

            return (ReleaseDelegate(handle) == PdfReturnValues.ERROR_SUCCESS);
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (_disposed) {
                return;
            }

            base.Dispose(disposing);

            _disposed = true;
            DecrementCounter();
        }

        /// <summary>
        /// Finalizer of the class, ensures the proper release of resources
        /// </summary>
        ~PdfSafeHandle()
        {
            Dispose(false);
        }

        #endregion

        private void IncrementCounter()
        {
#if DEBUG || TRACE_SAFE_HANDLES
            System.Threading.Interlocked.Increment(ref _counter);
#endif
        }

        private void DecrementCounter()
        {
#if DEBUG || TRACE_SAFE_HANDLES
            System.Threading.Interlocked.Decrement(ref _counter);
#endif
        }

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention), SuppressUnmanagedCodeSecurity]
        protected delegate UInt32 GenericReleaseDelgate(IntPtr handle);
    }
}
