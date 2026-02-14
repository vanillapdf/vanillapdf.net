using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A content stream is a PDF stream object whose data consists of
    /// a sequence of instructions describing the graphical elements to be painted on a page.
    /// </summary>
    public class PdfPageContents : IDisposable
    {
        internal PdfPageContentsSafeHandle Handle { get; }

        internal PdfPageContents(PdfPageContentsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get PdfContents.PdfContentInstructionCollection of all the instructions within the content stream
        /// </summary>
        /// <returns>Handle to \ref PdfContents.PdfContentInstructionCollection on success, throws exception on failure</returns>
        public PdfContentInstructionCollection GetInstructionCollection()
        {
            UInt32 result = NativeMethods.PageContents_GetInstructionCollection(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstructionCollection(data);
        }

        /// <summary>
        /// This function updates contents of the stream after any changes to the instruction
        /// collection has been done. It has to be called to apply any change,
        /// otherwise the changes will be LOST.
        /// </summary>
        /// <returns>True if the recalculation was successful, False in case no recalculation was needed, throws exception on failure</returns>
        public bool RecalculateStreamData()
        {
            UInt32 result = NativeMethods.PageContents_RecalculateStreamData(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
