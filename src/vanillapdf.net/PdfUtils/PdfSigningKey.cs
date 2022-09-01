using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Signing key is used for document signing
    /// @see \ref PdfSemantics.PdfDocument.Sign
    /// </summary>
    public class PdfSigningKey : PdfUnknown
    {
        internal PdfSigningKeySafeHandle Handle { get; }

        internal PdfSigningKey(PdfSigningKeySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfSigningKey()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfSigningKeySafeHandle).TypeHandle);
        }

        /// <summary>
        /// Create a new instance of \ref PdfSigningKey with associated \ref PdfSigningKeyContext callback functions
        /// </summary>
        /// <returns>New instance of \ref PdfSigningKey</returns>
        public static PdfSigningKey CreateCustom(PdfSigningKeyContext context)
        {
            UInt32 result = NativeMethods.SigningKey_CreateCustom(initializeDelgate, updateDelgate, finalDelgate, cleanupDelgate, GCHandle.ToIntPtr(context.Handle), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSigningKey(data);
        }

        private static UInt32 Initialize(IntPtr userdata, PdfMessageDigestAlgorithmType digest)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningKeyContext context = (handle.Target as PdfSigningKeyContext);

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
                PdfSigningKeyContext context = (handle.Target as PdfSigningKeyContext);

                // This is unfortunately neccessary, because of the error
                // "Cannot marshal 'parameter #2': SafeHandles cannot be marshaled from unmanaged to managed."
                // The reason for this, is the unmanaged code is unable to create SafeHandle,
                // when calling back to managed.

                using (PdfBufferSafeHandle bufferSafeHandle = new PdfBufferSafeHandle()) {
                    bool success = false;

                    bufferSafeHandle.DangerousSetHandle(buffer);
                    bufferSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfBuffer(bufferSafeHandle)) {
                        return context.Update(wrapper);
                    }
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
                PdfSigningKeyContext context = (handle.Target as PdfSigningKeyContext);

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

        private static UInt32 Cleanup(IntPtr userdata)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfSigningKeyContext context = (handle.Target as PdfSigningKeyContext);

                return context.Cleanup();
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

        // We need to have static delegates as the SigningKey_CreateCustom
        // would create a delegate when referencing the static function
        // which would be disposed during the garbage collection.
        // We prevent cleaning up the delegates by having static references.

        private static NativeMethods.InitializeDelgate initializeDelgate = Initialize;
        private static NativeMethods.UpdateDelgate updateDelgate = Update;
        private static NativeMethods.FinalDelgate finalDelgate = Final;
        private static NativeMethods.CleanupDelgate cleanupDelgate = Cleanup;

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
            public delegate UInt32 CleanupDelgate(IntPtr userdata);
        }
    }
}
