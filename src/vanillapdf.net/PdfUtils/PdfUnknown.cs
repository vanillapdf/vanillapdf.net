﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfUnknown : IDisposable
    {
        internal PdfUnknownSafeHandle IUnknownHandle { get; }

        internal PdfUnknown(PdfUnknownSafeHandle handle)
        {
            IUnknownHandle = handle;
        }

        static PdfUnknown()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public void Dispose()
        {
            // Hook for derived classes
            ReleaseManagedResources();

            GC.SuppressFinalize(this);
        }

        protected virtual void ReleaseManagedResources()
        {
            IUnknownHandle.Dispose();
        }

        private static class NativeMethods
        {
            public static IUnknownAddRefDelgate IUnknown_AddRef = LibraryInstance.GetFunction<IUnknownAddRefDelgate>("IUnknown_AddRef");
            public static IUnknownReleaseDelgate IUnknown_Release = LibraryInstance.GetFunction<IUnknownReleaseDelgate>("IUnknown_Release");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IUnknownAddRefDelgate(IntPtr handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IUnknownReleaseDelgate(IntPtr handle);
        }
    }
}
