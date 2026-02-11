using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Observer capable of receiving events from \ref PdfFileWriter
    /// </summary>
    public class PdfFileWriterObserver : IDisposable
    {
        internal PdfFileWriterObserverSafeHandle Handle { get; }

        internal PdfFileWriterObserver(PdfFileWriterObserverSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfFileWriterObserver using specified context
        /// </summary>
        /// <param name="context">Handle to observer context to be used</param>
        /// <returns>New instance of \ref PdfFileWriterObserver on success, throws exception on failure</returns>
        public static PdfFileWriterObserver CreateCustom(PdfFileWriterObserverContext context)
        {
            UInt32 result = NativeMethods.FileWriterObserver_CreateCustom(
                Marshal.GetFunctionPointerForDelegate(onInitializingDelegate),
                Marshal.GetFunctionPointerForDelegate(onFinalizingDelegate),
                Marshal.GetFunctionPointerForDelegate(onBeforeObjectWriteDelegate),
                Marshal.GetFunctionPointerForDelegate(onAfterObjectWriteDelegate),
                Marshal.GetFunctionPointerForDelegate(onBeforeObjectOffsetRecalculationDelgate),
                Marshal.GetFunctionPointerForDelegate(onAfterObjectOffsetRecalculationDelgate),
                Marshal.GetFunctionPointerForDelegate(onBeforeEntryOffsetRecalculationDelgate),
                Marshal.GetFunctionPointerForDelegate(onAfterEntryOffsetRecalculationDelgate),
                Marshal.GetFunctionPointerForDelegate(onBeforeOutputFlushDelegate),
                Marshal.GetFunctionPointerForDelegate(onAfterOutputFlushDelegate),
                GCHandle.ToIntPtr(context.Handle), out var data);

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileWriterObserver(data);
        }

        public void Dispose()
        {
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
        private static NativeMethods.OnFinalizingDelegate onFinalizingDelegate = OnFinalizing;
        private static NativeMethods.OnBeforeObjectWriteDelegate onBeforeObjectWriteDelegate = OnBeforeObjectWrite;
        private static NativeMethods.OnAfterObjectWriteDelegate onAfterObjectWriteDelegate = OnAfterObjectWrite;
        private static NativeMethods.OnBeforeObjectOffsetRecalculationDelegate onBeforeObjectOffsetRecalculationDelgate = OnBeforeObjectOffsetRecalculation;
        private static NativeMethods.OnAfterObjectOffsetRecalculationDelegate onAfterObjectOffsetRecalculationDelgate = OnAfterObjectOffsetRecalculation;
        private static NativeMethods.OnBeforeEntryOffsetRecalculationDelegate onBeforeEntryOffsetRecalculationDelgate = OnBeforeEntryOffsetRecalculation;
        private static NativeMethods.OnAfterEntryOffsetRecalculationDelegate onAfterEntryOffsetRecalculationDelgate = OnAfterEntryOffsetRecalculation;
        private static NativeMethods.OnBeforeOutputFlushDelegate onBeforeOutputFlushDelegate = OnBeforeOutputFlush;
        private static NativeMethods.OnAfterOutputFlushDelegate onAfterOutputFlushDelegate = OnAfterOutputFlush;
    }
}
