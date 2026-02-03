using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The name dictionary (PDF 1.2+) associates names with various
    /// document objects using name trees for efficient lookup.
    /// </summary>
    public class PdfNameDictionary : PdfUnknown
    {
        internal PdfNameDictionarySafeHandle Handle { get; }

        internal PdfNameDictionary(PdfNameDictionarySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty name dictionary.
        /// </summary>
        /// <returns>A new empty name dictionary.</returns>
        public static PdfNameDictionary Create()
        {
            UInt32 result = NativeMethods.NameDictionary_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameDictionary(data);
        }

        /// <summary>
        /// Check if the name dictionary contains a Dests name tree.
        /// </summary>
        /// <returns>True if the Dests name tree exists.</returns>
        public bool ContainsDestinations()
        {
            UInt32 result = NativeMethods.NameDictionary_ContainsDestinations(Handle, out bool contains);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return contains;
        }

        /// <summary>
        /// Get the Dests name tree from the name dictionary.
        /// Use ContainsDestinations() to check if it exists first.
        /// </summary>
        /// <returns>The destination name tree, or null if it does not exist.</returns>
        public PdfDestinationNameTree GetDestinations()
        {
            UInt32 result = NativeMethods.NameDictionary_GetDestinations(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestinationNameTree(data);
        }

        /// <summary>
        /// Set the Dests name tree in the name dictionary.
        /// This replaces any existing Dests tree.
        /// </summary>
        /// <param name="destinations">The destination name tree to set.</param>
        public void SetDestinations(PdfDestinationNameTree destinations)
        {
            UInt32 result = NativeMethods.NameDictionary_SetDestinations(Handle, destinations.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
