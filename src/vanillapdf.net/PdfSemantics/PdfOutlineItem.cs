using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Child element within tree-structured hierarchy of outline items
    /// </summary>
    public class PdfOutlineItem : PdfOutlineBase
    {
        internal PdfOutlineItemSafeHandle Handle { get; }

        internal PdfOutlineItem(PdfOutlineItemSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base outline type.
        /// </summary>
        public static PdfOutlineItem FromOutlineBase(PdfOutlineBase data)
        {
            return new PdfOutlineItem(data.OutlineBaseHandle);
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
        /// The previous item at this outline level.
        /// </summary>
        public PdfOutlineItem Previous
        {
            get { return GetPrevious(); }
        }

        /// <summary>
        /// The next item at this outline level.
        /// </summary>
        public PdfOutlineItem Next
        {
            get { return GetNext(); }
        }

        /// <summary>
        /// The text that shall be displayed on the screen for this item.
        /// </summary>
        public PdfStringObject Title
        {
            get { return GetTitle(); }
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

        /// <summary>
        /// A destination to be displayed when this outline item is activated.
        /// Returns null if no destination is specified.
        /// </summary>
        public PdfDestination Destination
        {
            get { return GetDestination(); }
        }

        private PdfDestination GetDestination()
        {
            UInt32 result = NativeMethods.OutlineItem_GetDestination(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestination(data);
        }

        private PdfIntegerObject GetCount()
        {
            UInt32 result = NativeMethods.OutlineItem_GetCount(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        private PdfStringObject GetTitle()
        {
            UInt32 result = NativeMethods.OutlineItem_GetTitle(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        private PdfOutlineItem GetFirst()
        {
            UInt32 result = NativeMethods.OutlineItem_GetFirst(Handle, out var data);
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
            UInt32 result = NativeMethods.OutlineItem_GetLast(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutlineItem(data);
        }

        private PdfOutlineItem GetPrevious()
        {
            UInt32 result = NativeMethods.OutlineItem_GetPrev(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutlineItem(data);
        }

        private PdfOutlineItem GetNext()
        {
            UInt32 result = NativeMethods.OutlineItem_GetNext(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutlineItem(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
