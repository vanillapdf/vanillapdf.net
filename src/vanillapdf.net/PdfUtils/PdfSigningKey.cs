using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    public abstract class PdfSigningContext : IDisposable
    {
        public PdfSigningContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        internal GCHandle Handle { get; }

        public abstract UInt32 Initialize(PdfMessageDigestAlgorithmType digest);
        public abstract UInt32 Update(PdfBuffer buffer);
        public abstract UInt32 Final(out PdfBuffer buffer);
        public abstract void Cleanup();

        private void ReleaseUnmanagedResources()
        {
            if (Handle.IsAllocated) {
                Handle.Free();
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PdfSigningContext()
        {
            ReleaseUnmanagedResources();
        }
    }

    public class PdfSigningKey : PdfUnknown
    {
        internal PdfSigningKey(PdfSigningKeySafeHandle handle) : base(handle)
        {
        }

        static PdfSigningKey()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfSigningKey CreateCustom(PdfSigningContext context)
        {
            UInt32 result = NativeMethods.SigningKey_CreateCustom(Initialize, Update, Final, Cleanup, GCHandle.ToIntPtr(context.Handle), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSigningKey(data);
        }

        private static UInt32 Initialize(IntPtr userdata, PdfMessageDigestAlgorithmType digest)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningContext context = (handle.Target as PdfSigningContext);

                return context.Initialize(digest);
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 Update(IntPtr userdata, IntPtr buffer)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningContext context = (handle.Target as PdfSigningContext);

                // This is unfortunately neccessary, because of the error
                // "Cannot marshal 'parameter #2': SafeHandles cannot be marshaled from unmanaged to managed."
                // The reason for this, is the unmanaged code is unable to create SafeHandle,
                // when calling back to managed.

                PdfBufferSafeHandle bufferSafeHandle = new PdfBufferSafeHandle();
                bufferSafeHandle.DangerousSetHandle(buffer);

                using (var wrapper = new PdfBuffer(bufferSafeHandle)) {
                    return context.Update(wrapper);
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 Final(IntPtr userdata, out IntPtr buffer)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningContext context = (handle.Target as PdfSigningContext);

                UInt32 rv = context.Final(out PdfBuffer finalResult);
                if (rv != PdfReturnValues.ERROR_SUCCESS) {
                    buffer = IntPtr.Zero;
                    return rv;
                }

                PdfBufferSafeHandle bufferSafeHandle = finalResult.Handle;
                buffer = bufferSafeHandle.DangerousGetHandle();

                return PdfReturnValues.ERROR_SUCCESS;
            }
            catch {
                buffer = IntPtr.Zero;
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static void Cleanup(IntPtr userdata)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningContext context = (handle.Target as PdfSigningContext);

                context.Cleanup();
            }
            catch {
            }
        }

        private static class NativeMethods
        {
            public static CreateCustomDelgate SigningKey_CreateCustom = LibraryInstance.GetFunction<CreateCustomDelgate>("SigningKey_CreateCustom");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateCustomDelgate(InitializeDelgate initialize, UpdateDelgate update, FinalDelgate final, CleanupDelgate cleanup, IntPtr userdata, out PdfSigningKeySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InitializeDelgate(IntPtr userdata, PdfMessageDigestAlgorithmType digest);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 UpdateDelgate(IntPtr userdata, IntPtr buffer);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FinalDelgate(IntPtr userdata, out IntPtr buffer);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate void CleanupDelgate(IntPtr userdata);
        }
    }
}
