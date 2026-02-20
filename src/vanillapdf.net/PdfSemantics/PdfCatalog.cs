using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The root of a document's object hierarchy.
    /// </summary>
    public class PdfCatalog : IDisposable
    {
        internal PdfCatalogSafeHandle Handle { get; }

        internal PdfCatalog(PdfCatalogSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The version of the PDF specification to which the document conforms
        /// if later than the version specified in the file's header.
        /// 
        /// If the header specifies a later version, or if this entry is absent,
        /// the document shall conform to the version specified in the header.
        /// </summary>
        public PdfVersion? Version
        {
            get { return GetVersion(); }
        }

        private PdfVersion? GetVersion()
        {
            UInt32 result = NativeMethods.Catalog_GetVersion(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfVersion>.CheckedCast(data);
        }

        /// <summary>
        /// The root of the document's page tree (see 7.7.3, "Page Tree").
        /// </summary>
        /// <returns>Handle to a \ref PdfPageTree on success, throws exception on failure</returns>
        public PdfPageTree GetPages()
        {
            UInt32 result = NativeMethods.Catalog_GetPages(Handle, out PdfPageTreeSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageTree(data);
        }

        /// <summary>
        /// Retrieve the outline hierarchy of the document.
        /// </summary>
        /// <returns>The root <see cref="PdfOutline"/> object or <c>null</c> if none exists.</returns>
        public PdfOutline GetOutlines()
        {
            UInt32 result = NativeMethods.Catalog_GetOutlines(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutline(data);
        }

        /// <summary>
        /// Retrieve the document's name dictionary (PDF 1.2+).
        /// The name dictionary contains name trees that map strings to various
        /// document objects, including destinations.
        /// </summary>
        /// <returns>The <see cref="PdfNameDictionary"/> object or <c>null</c> if none exists.</returns>
        public PdfNameDictionary GetNames()
        {
            UInt32 result = NativeMethods.Catalog_GetNames(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameDictionary(data);
        }

        /// <summary>
        /// Set the document's name dictionary.
        /// </summary>
        /// <param name="names">The name dictionary to set.</param>
        public void SetNames(PdfNameDictionary names)
        {
            UInt32 result = NativeMethods.Catalog_SetNames(Handle, names.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Retrieve the named destinations dictionary.
        /// Named destinations allow destinations to be referred to by name
        /// rather than by explicit page and coordinates.
        /// </summary>
        /// <returns>The <see cref="PdfNamedDestinations"/> object or <c>null</c> if none exists.</returns>
        public PdfNamedDestinations GetDestinations()
        {
            UInt32 result = NativeMethods.Catalog_GetDestinations(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNamedDestinations(data);
        }

        /// <summary>
        /// Get the open action for the document.
        ///
        /// The returned object can be either an array (destination) or a dictionary (action).
        /// Use <see cref="PdfObject.GetObjectType"/> to determine the actual type, then convert using
        /// <see cref="PdfDestination.CreateFromArray"/> or <see cref="PdfAction.CreateFromDictionary"/>.
        /// Returns null if no open action is specified.
        /// </summary>
        public PdfObject GetOpenAction()
        {
            UInt32 result = NativeMethods.Catalog_GetOpenAction(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
