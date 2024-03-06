using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        static PdfOutlineItem()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutlineItemSafeHandle).TypeHandle);
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

        private static class NativeMethods
        {
            public static GetTitleDelegate OutlineItem_GetTitle = LibraryInstance.GetFunction<GetTitleDelegate>("OutlineItem_GetTitle");
            public static GetCountDelegate OutlineItem_GetParent = LibraryInstance.GetFunction<GetCountDelegate>("OutlineItem_GetParent");
            public static GetPrevDelegate OutlineItem_GetPrev = LibraryInstance.GetFunction<GetPrevDelegate>("OutlineItem_GetPrev");
            public static GetNextDelegate OutlineItem_GetNext = LibraryInstance.GetFunction<GetNextDelegate>("OutlineItem_GetNext");
            public static GetFirstDelegate OutlineItem_GetFirst = LibraryInstance.GetFunction<GetFirstDelegate>("OutlineItem_GetFirst");
            public static GetLastDelegate OutlineItem_GetLast = LibraryInstance.GetFunction<GetLastDelegate>("OutlineItem_GetLast");
            public static GetCountDelegate OutlineItem_GetCount = LibraryInstance.GetFunction<GetCountDelegate>("OutlineItem_GetCount");
            //public static GetCountDelegate OutlineItem_GetColor = LibraryInstance.GetFunction<GetCountDelegate>("OutlineItem_GetColor");
            //public static GetCountDelegate OutlineItem_GetFlags = LibraryInstance.GetFunction<GetCountDelegate>("OutlineItem_GetFlags");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTitleDelegate(PdfOutlineItemSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCountDelegate(PdfOutlineItemSafeHandle handle, out PdfIntegerObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetFirstDelegate(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetLastDelegate(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetPrevDelegate(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetNextDelegate(PdfOutlineItemSafeHandle handle, out PdfOutlineItemSafeHandle data);
        }
    }
}
