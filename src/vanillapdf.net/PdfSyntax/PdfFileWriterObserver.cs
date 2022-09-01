using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Observer capable of receiving events from \ref PdfFileWriter
    /// </summary>
    public class PdfFileWriterObserver : PdfUnknown
    {
        internal PdfFileWriterObserverSafeHandle Handle { get; }

        internal PdfFileWriterObserver(PdfFileWriterObserverSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfFileWriterObserver()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFileWriterObserverSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Create a new instance of \ref PdfFileWriterObserver using specified context
        /// </summary>
        /// <param name="context">Handle to observer context to be used</param>
        /// <returns>New instance of \ref PdfFileWriterObserver on success, throws exception on failure</returns>
        public static PdfFileWriterObserver CreateCustom(PdfFileWriterObserverContext context)
        {
            UInt32 result = NativeMethods.FileWriterObserver_CreateCustom(
                onInitializingDelegate, onFinalizingDelegate,
                onBeforeObjectWriteDelegate, onAfterObjectWriteDelegate,
                onBeforeObjectOffsetRecalculationDelgate, onAfterObjectOffsetRecalculationDelgate,
                onBeforeEntryOffsetRecalculationDelgate, onAfterEntryOffsetRecalculationDelgate,
                onBeforeOutputFlushDelegate, onAfterOutputFlushDelegate,
                GCHandle.ToIntPtr(context.Handle), out var data);

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileWriterObserver(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
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
            catch {
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
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeObjectWrite(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle()) {
                    bool success = false;

                    objectSafeHandle.DangerousSetHandle(data);
                    objectSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfObject(objectSafeHandle)) {
                        return context.OnBeforeObjectWrite(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterObjectWrite(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle()) {
                    bool success = false;

                    objectSafeHandle.DangerousSetHandle(data);
                    objectSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfObject(objectSafeHandle)) {
                        return context.OnAfterObjectWrite(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeObjectOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle()) {
                    bool success = false;

                    objectSafeHandle.DangerousSetHandle(data);
                    objectSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfObject(objectSafeHandle)) {
                        return context.OnBeforeObjectOffsetRecalculation(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterObjectOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfObjectSafeHandle objectSafeHandle = new PdfObjectSafeHandle()) {
                    bool success = false;

                    objectSafeHandle.DangerousSetHandle(data);
                    objectSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfObject(objectSafeHandle)) {
                        return context.OnAfterObjectOffsetRecalculation(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeEntryOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfXrefEntrySafeHandle entrySafeHandle = new PdfXrefEntrySafeHandle()) {
                    bool success = false;

                    entrySafeHandle.DangerousSetHandle(data);
                    entrySafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfXrefEntry(entrySafeHandle)) {
                        return context.OnBeforeEntryOffsetRecalculation(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterEntryOffsetRecalculation(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfXrefEntrySafeHandle entrySafeHandle = new PdfXrefEntrySafeHandle()) {
                    bool success = false;

                    entrySafeHandle.DangerousSetHandle(data);
                    entrySafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfXrefEntry(entrySafeHandle)) {
                        return context.OnAfterEntryOffsetRecalculation(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnBeforeOutputFlush(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle()) {
                    bool success = false;

                    streamSafeHandle.DangerousSetHandle(data);
                    streamSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                        return context.OnBeforeOutputFlush(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        private static UInt32 OnAfterOutputFlush(IntPtr userdata, IntPtr data)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfFileWriterObserverContext context = (handle.Target as PdfFileWriterObserverContext);

                using (PdfInputOutputStreamSafeHandle streamSafeHandle = new PdfInputOutputStreamSafeHandle()) {
                    bool success = false;

                    streamSafeHandle.DangerousSetHandle(data);
                    streamSafeHandle.DangerousAddRef(ref success);

                    using (var wrapper = new PdfInputOutputStream(streamSafeHandle)) {
                        return context.OnAfterOutputFlush(wrapper);
                    }
                }
            }
            catch {
                return PdfReturnValues.ERROR_GENERAL;
            }
        }

        // We need to have static delegates as the FileWriterObserver_CreateCustom
        // would create a delegate when referencing the static function
        // which would be disposed during the garbage collection.
        // We prevent cleaning up the delegates by having static references.

        private static NativeMethods.OnInitializingDelegate onInitializingDelegate = OnInitializing;
        private static NativeMethods.OnFinalizingDelegate onFinalizingDelegate = OnInitializing;
        private static NativeMethods.OnBeforeObjectWriteDelegate onBeforeObjectWriteDelegate = OnBeforeObjectWrite;
        private static NativeMethods.OnAfterObjectWriteDelegate onAfterObjectWriteDelegate = OnAfterObjectWrite;
        private static NativeMethods.OnBeforeObjectOffsetRecalculationDelegate onBeforeObjectOffsetRecalculationDelgate = OnBeforeObjectOffsetRecalculation;
        private static NativeMethods.OnAfterObjectOffsetRecalculationDelegate onAfterObjectOffsetRecalculationDelgate = OnAfterObjectOffsetRecalculation;
        private static NativeMethods.OnBeforeEntryOffsetRecalculationDelegate onBeforeEntryOffsetRecalculationDelgate = OnBeforeEntryOffsetRecalculation;
        private static NativeMethods.OnAfterEntryOffsetRecalculationDelegate onAfterEntryOffsetRecalculationDelgate = OnAfterEntryOffsetRecalculation;
        private static NativeMethods.OnBeforeOutputFlushDelegate onBeforeOutputFlushDelegate = OnBeforeOutputFlush;
        private static NativeMethods.OnAfterOutputFlushDelegate onAfterOutputFlushDelegate = OnAfterOutputFlush;

        private static class NativeMethods
        {
            public static CreateCustomDelgate FileWriterObserver_CreateCustom = LibraryInstance.GetFunction<CreateCustomDelgate>("FileWriterObserver_CreateCustom");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateCustomDelgate(
                OnInitializingDelegate on_initializing,
                OnFinalizingDelegate on_finalizing,
                OnBeforeObjectWriteDelegate on_before_object_write,
                OnAfterObjectWriteDelegate on_after_object_write,
                OnBeforeObjectOffsetRecalculationDelegate on_before_object_offset_recalculation,
                OnAfterObjectOffsetRecalculationDelegate on_after_object_offset_recalculation,
                OnBeforeEntryOffsetRecalculationDelegate on_before_entry_offset_recalculation,
                OnAfterEntryOffsetRecalculationDelegate on_after_entry_offset_recalculation,
                OnBeforeOutputFlushDelegate on_before_output_flush,
                OnAfterOutputFlushDelegate on_after_output_flush,
                IntPtr userdata,
                out PdfFileWriterObserverSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnInitializingDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnFinalizingDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeObjectWriteDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterObjectWriteDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeObjectOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterObjectOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeEntryOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterEntryOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnBeforeOutputFlushDelegate(IntPtr userdata, IntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 OnAfterOutputFlushDelegate(IntPtr userdata, IntPtr data);
        }
    }
}
