using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A map of unique names mapped to their associated destinations.
    /// Named destinations allow destinations to be referred to by name
    /// rather than by explicit page and coordinates.
    /// </summary>
    public class PdfNamedDestinations : IDisposable
    {
        internal PdfNamedDestinationsSafeHandle Handle { get; }
        private bool _disposed;

        internal PdfNamedDestinations(PdfNamedDestinationsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Check if the named destinations map contains the specified name.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>True if the name exists in the map.</returns>
        public bool Contains(PdfNameObject name)
        {
            UInt32 result = NativeMethods.NamedDestinations_Contains(Handle, name.ObjectHandle, out bool contains);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return contains;
        }

        /// <summary>
        /// Find the destination associated with the specified name.
        /// Use Contains() first to check if the name exists.
        /// </summary>
        /// <param name="name">The name to look up.</param>
        /// <returns>The destination associated with the name.</returns>
        public PdfDestination Find(PdfNameObject name)
        {
            UInt32 result = NativeMethods.NamedDestinations_Find(Handle, name.ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestination(data);
        }

        /// <summary>
        /// Try to find the destination associated with the specified name.
        /// </summary>
        /// <param name="name">The name to look up.</param>
        /// <param name="destination">The destination if found.</param>
        /// <returns>True if the name was found.</returns>
        public bool TryFind(PdfNameObject name, out PdfDestination destination)
        {
            if (!Contains(name)) {
                destination = null;
                return false;
            }

            destination = Find(name);
            return true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose pattern implementation.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed) {
                if (disposing) {
                    Handle?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
