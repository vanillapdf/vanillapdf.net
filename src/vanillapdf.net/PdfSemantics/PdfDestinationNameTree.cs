using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A name tree that maps string names to destination objects.
    /// This is the PDF 1.2+ method for storing named destinations,
    /// replacing the old-style /Dests dictionary in the catalog.
    /// </summary>
    public class PdfDestinationNameTree : IDisposable
    {
        internal PdfDestinationNameTreeSafeHandle Handle { get; }

        internal PdfDestinationNameTree(PdfDestinationNameTreeSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty destination name tree.
        /// </summary>
        /// <returns>A new empty destination name tree.</returns>
        public static PdfDestinationNameTree Create()
        {
            UInt32 result = NativeMethods.DestinationNameTree_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestinationNameTree(data);
        }

        /// <summary>
        /// Check if the name tree contains a destination with the given name.
        /// </summary>
        /// <param name="name">The string name to check.</param>
        /// <returns>True if the name exists in the tree.</returns>
        public bool Contains(PdfStringObject name)
        {
            UInt32 result = NativeMethods.DestinationNameTree_Contains(Handle, name.StringHandle, out bool contains);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return contains;
        }

        /// <summary>
        /// Find a destination by its string name.
        /// Use Contains() first to check if the name exists.
        /// </summary>
        /// <param name="name">The string name to look up.</param>
        /// <returns>The destination associated with the name.</returns>
        public PdfDestination Find(PdfStringObject name)
        {
            UInt32 result = NativeMethods.DestinationNameTree_Find(Handle, name.StringHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestination(data);
        }

        /// <summary>
        /// Try to find a destination by its string name.
        /// </summary>
        /// <param name="name">The string name to look up.</param>
        /// <param name="destination">The destination if found.</param>
        /// <returns>True if the name was found.</returns>
        public bool TryFind(PdfStringObject name, out PdfDestination destination)
        {
            UInt32 result = NativeMethods.DestinationNameTree_TryFind(Handle, name.StringHandle, out var data, out bool found);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            if (!found) {
                destination = null;
                return false;
            }

            destination = new PdfDestination(data);
            return true;
        }

        /// <summary>
        /// Insert a named destination into the tree.
        /// </summary>
        /// <param name="name">The string name for the destination.</param>
        /// <param name="destination">The destination to insert.</param>
        public void Insert(PdfStringObject name, PdfDestination destination)
        {
            UInt32 result = NativeMethods.DestinationNameTree_Insert(Handle, name.StringHandle, destination.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove a named destination from the tree.
        /// </summary>
        /// <param name="name">The string name to remove.</param>
        /// <returns>True if the destination was removed, false if not found.</returns>
        public bool Remove(PdfStringObject name)
        {
            UInt32 result = NativeMethods.DestinationNameTree_Remove(Handle, name.StringHandle, out bool removed);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return removed;
        }

        /// <summary>
        /// Get an iterator to traverse all named destinations.
        /// Modifying the collection may invalidate the iterator.
        /// </summary>
        /// <returns>An iterator for traversing the name tree.</returns>
        public PdfDestinationNameTreeIterator GetIterator()
        {
            UInt32 result = NativeMethods.DestinationNameTree_GetIterator(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestinationNameTreeIterator(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
