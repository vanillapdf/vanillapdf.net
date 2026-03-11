using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents the AcroForm (interactive form) of a PDF document.
    /// </summary>
    public class PdfAcroForm : IDisposable
    {
        internal PdfInteractiveFormSafeHandle Handle { get; }

        internal PdfAcroForm(PdfInteractiveFormSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the collection of all fields in this AcroForm.
        /// </summary>
        /// <returns>The <see cref="PdfFieldCollection"/>, or null if no fields entry exists.</returns>
        public PdfFieldCollection GetFields()
        {
            UInt32 result = NativeMethods.InteractiveForm_GetFields(Handle, out PdfFieldCollectionSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFieldCollection(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
