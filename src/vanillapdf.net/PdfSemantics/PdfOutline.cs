using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The root of a document's outline hierarchy
    /// </summary>
    public class PdfOutline : PdfOutlineBase
    {
        internal PdfOutlineSafeHandle Handle { get; }

        internal PdfOutline(PdfOutlineSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// An outline item dictionary representing the first top-level item in the outline.
        /// </summary>
        public PdfOutlineItem First
        {
            get { return GetFirst(); }
        }

        /// <summary>
        /// An outline item dictionary representing the last top-level item in the outline.
        /// </summary>
        public PdfOutlineItem Last
        {
            get { return GetLast(); }
        }

        /// <summary>
        /// Total number of visible outline items at all levels of the outline.
        /// The value cannot be negative.
        /// This entry shall be omitted if there are no open outline items.
        /// </summary>
        public PdfIntegerObject Count
        {
            get { return GetCount(); }
        }

        private PdfOutlineItem GetFirst()
        {
            UInt32 result = NativeMethods.Outline_GetFirst(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutlineItem(data);
        }

        private PdfOutlineItem GetLast()
        {
            UInt32 result = NativeMethods.Outline_GetLast(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutlineItem(data);
        }

        private PdfIntegerObject GetCount()
        {
            UInt32 result = NativeMethods.Outline_GetCount(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
