using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public abstract class PdfFileWriterObserverContext : IDisposable
    {
        public PdfFileWriterObserverContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        public GCHandle Handle { get; }

        public abstract UInt32 OnInitializing(PdfInputOutputStream data);
        public abstract UInt32 OnFinalizing(PdfInputOutputStream data);

        public abstract UInt32 OnBeforeObjectWrite(PdfObject data);
        public abstract UInt32 OnAfterObjectWrite(PdfObject data);

        public abstract UInt32 OnBeforeObjectOffsetRecalculation(PdfObject data);
        public abstract UInt32 OnAfterObjectOffsetRecalculation(PdfObject data);

        public abstract UInt32 OnBeforeEntryOffsetRecalculation(PdfXrefEntry data);
        public abstract UInt32 OnAfterEntryOffsetRecalculation(PdfXrefEntry data);

        public abstract UInt32 OnBeforeOutputFlush(PdfInputOutputStream data);
        public abstract UInt32 OnAfterOutputFlush(PdfInputOutputStream data);

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

        ~PdfFileWriterObserverContext()
        {
            ReleaseUnmanagedResources();
        }
    }

    public class PdfFileWriterObserver : PdfUnknown
    {
        internal PdfFileWriterObserver(PdfFileWriterObserverSafeHandle handle) : base(handle)
        {
        }

        static PdfFileWriterObserver()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfFileWriterObserver CreateCustom(PdfFileWriterObserverContext context)
        {
            UInt32 result = NativeMethods.FileWriterObserver_CreateCustom(
                OnInitializing, OnFinalizing,
                OnBeforeObjectWrite, OnAfterObjectWrite,
                OnBeforeObjectOffsetRecalculation, OnAfterObjectOffsetRecalculation,
                OnBeforeEntryOffsetRecalculation, OnAfterEntryOffsetRecalculation,
                OnBeforeOutputFlush, OnAfterOutputFlush,
                GCHandle.ToIntPtr(context.Handle), out var data);

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileWriterObserver(data);
        }

        private static UInt32 OnInitializing(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle();
                streamSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                    return context.OnInitializing(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnFinalizing(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle();
                streamSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                    return context.OnFinalizing(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeObjectWrite(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle();
                objectSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfObject(objectSafeHandle)) {
                    return context.OnBeforeObjectWrite(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterObjectWrite(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle();
                objectSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfObject(objectSafeHandle)) {
                    return context.OnAfterObjectWrite(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeObjectOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle();
                objectSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfObject(objectSafeHandle)) {
                    return context.OnBeforeObjectOffsetRecalculation(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterObjectOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle();
                objectSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfObject(objectSafeHandle)) {
                    return context.OnAfterObjectOffsetRecalculation(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeEntryOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfXrefEntrySafeHandle entrySafeHandle = new PdfXrefEntrySafeHandle();
                entrySafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfXrefEntry(entrySafeHandle)) {
                    return context.OnBeforeEntryOffsetRecalculation(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterEntryOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfXrefEntrySafeHandle entrySafeHandle = new PdfXrefEntrySafeHandle();
                entrySafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfXrefEntry(entrySafeHandle)) {
                    return context.OnAfterEntryOffsetRecalculation(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeOutputFlush(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle();
                streamSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                    return context.OnBeforeOutputFlush(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterOutputFlush(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle();
                streamSafeHandle.DangerousSetHandle(data);

                using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                    return context.OnAfterOutputFlush(wrapper);
                }
            }
            catch (Exception ex) {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static class NativeMethods
        {
            public static CreateCustomDelgate FileWriterObserver_CreateCustom = LibraryInstance.GetFunction<CreateCustomDelgate>("FileWriterObserver_CreateCustom");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateCustomDelgate(
                OnInitializingDelgate on_initializing,
                OnFinalizingDelgate on_finalizing,
                OnBeforeObjectWriteDelgate on_before_object_write,
                OnAfterObjectWriteDelgate on_after_object_write,
                OnBeforeObjectOffsetRecalculationDelgate on_before_object_offset_recalculation,
                OnAfterObjectOffsetRecalculationDelgate on_after_object_offset_recalculation,
                OnBeforeEntryOffsetRecalculationDelgate on_before_entry_offset_recalculation,
                OnAfterEntryOffsetRecalculationDelgate on_after_entry_offset_recalculation,
                OnBeforeOutputFlushDelegate on_before_output_flush,
                OnAfterOutputFlushDelegate on_after_output_flush,
                IntPtr userdata,
                out PdfFileWriterObserverSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnInitializingDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnFinalizingDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeObjectWriteDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterObjectWriteDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeObjectOffsetRecalculationDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterObjectOffsetRecalculationDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeEntryOffsetRecalculationDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterEntryOffsetRecalculationDelgate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeOutputFlushDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterOutputFlushDelegate(IntPtr userdata, IntPtr data);
        }
    }
}
