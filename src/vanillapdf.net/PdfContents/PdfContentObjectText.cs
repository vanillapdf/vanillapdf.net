using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfContents.Extensions;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// A PDF text object consists of operators that may show text strings,
    /// move the text position, and set text state and certain other parameters.
    /// </summary>
    public class PdfContentObjectText : PdfContentObject
    {
        internal PdfContentObjectTextSafeHandle Handle { get; }

        internal PdfContentObjectText(PdfContentObjectTextSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get number of operations within the current text object
        /// </summary>
        /// <returns>Number of operations within current text object on success, throws exception on failure</returns>
        public UInt64 GetOperationsSize()
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get content stream operation at index in the current text object
        /// </summary>
        /// <returns>Operation at <p>index</p> on success, throws exception on failure</returns>
        public PdfContentOperation GetOperationAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            if (LibraryInstance.UpgradePolicy == UpgradePolicy.None) {
                return new PdfContentOperation(data);
            }

            using (var operation = new PdfContentOperation(data)) {
                return operation.Upgrade();
            }
        }

        /// <summary>
        /// Convert content object to content text object
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentObject to be converted</param>
        /// <returns>A new instance of \ref PdfContentObjectText if the object can be converted, throws exception on failure</returns>
        public static PdfContentObjectText FromContentObject(PdfContentObject data)
        {
            return new PdfContentObjectText(data.ObjectHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
