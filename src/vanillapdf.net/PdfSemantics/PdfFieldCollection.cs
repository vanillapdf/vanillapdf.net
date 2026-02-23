using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An ordered collection of interactive form fields from a PDF AcroForm.
    /// </summary>
    public class PdfFieldCollection : IDisposable
    {
        internal PdfFieldCollectionSafeHandle Handle { get; }

        internal PdfFieldCollection(PdfFieldCollectionSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the number of fields in the collection.
        /// </summary>
        /// <returns>The field count.</returns>
        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.FieldCollection_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the field at the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the field to retrieve.</param>
        /// <returns>The <see cref="PdfField"/> at <paramref name="index"/>.</returns>
        public PdfField GetValueAt(UInt64 index)
        {
            UInt32 result = NativeMethods.FieldCollection_At(Handle, new UIntPtr(index), out PdfFieldSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
