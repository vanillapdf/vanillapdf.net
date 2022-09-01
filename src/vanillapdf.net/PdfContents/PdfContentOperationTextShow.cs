using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Stream operation for showing a simple text string
    /// </summary>
    public class PdfContentOperationTextShow : PdfContentOperation
    {
        internal PdfContentOperationTextShowSafeHandle Handle { get; }

        internal PdfContentOperationTextShow(PdfContentOperationTextShowSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentOperationTextShow()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Text string to be displayed in the document
        /// </summary>
        public PdfStringObject Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        private PdfStringObject GetValue()
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(value);
        }

        private void SetValue(PdfStringObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_SetValue(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert content operation to text showing operation
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentOperation to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperationTextShow if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperationTextShow FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationTextShow(data.OperationHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetValueDelgate ContentOperationTextShow_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentOperationTextShow_GetValue");
            public static SetValueDelgate ContentOperationTextShow_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("ContentOperationTextShow_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfContentOperationTextShowSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfContentOperationTextShowSafeHandle handle, PdfStringObjectSafeHandle data);
        }
    }
}
