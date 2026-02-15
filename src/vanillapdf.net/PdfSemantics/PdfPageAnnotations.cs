using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An array of annotation dictionaries that shall contain indirect
    /// references to all \ref PdfAnnotation associated with the page.
    /// </summary>
    public class PdfPageAnnotations : IDisposable
    {
        internal PdfPageAnnotationsSafeHandle Handle { get; }

        internal PdfPageAnnotations(PdfPageAnnotationsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get number of page annotations in the current \ref PdfPageObject
        /// </summary>
        /// <returns>Number of page annotations on success, throws exception on failure</returns>
        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.PageAnnotations_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Create a new empty annotation collection.
        /// </summary>
        public static PdfPageAnnotations Create()
        {
            UInt32 result = NativeMethods.PageAnnotations_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfPageAnnotations(data);
        }

        /// <summary>
        /// Get page annotation at index in the current \ref PdfPageObject
        /// </summary>
        /// <param name="index">Index of page annotation to be returned</param>
        /// <returns>Handle to page annotation object at <p>index</p> on success, throws exception on failure</returns>
        public PdfAnnotation At(UInt64 index)
        {
            UInt32 result = NativeMethods.PageAnnotations_At(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAnnotation(data);
        }

        /// <summary>
        /// Append an annotation to the collection.
        /// </summary>
        /// <param name="annotation">The annotation to append.</param>
        public void Append(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.PageAnnotations_Append(Handle, annotation.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove an annotation from the collection at the specified position.
        /// </summary>
        /// <param name="index">Position of the element to remove.</param>
        public void Remove(UInt64 index)
        {
            UInt32 result = NativeMethods.PageAnnotations_Remove(Handle, new UIntPtr(index));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
