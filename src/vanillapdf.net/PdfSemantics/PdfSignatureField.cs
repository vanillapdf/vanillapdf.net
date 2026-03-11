using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a signature form field in a PDF AcroForm.
    /// </summary>
    public class PdfSignatureField : PdfField
    {
        internal PdfSignatureFieldSafeHandle SignatureFieldHandle { get; }

        internal PdfSignatureField(PdfSignatureFieldSafeHandle handle) : base(handle)
        {
            SignatureFieldHandle = handle;
        }

        /// <summary>
        /// Convert a generic field to a signature field.
        /// </summary>
        /// <param name="field">The field to convert. Must have type <see cref="PdfFieldType.Signature"/>.</param>
        /// <returns>A <see cref="PdfSignatureField"/> wrapping the same field.</returns>
        public static PdfSignatureField FromField(PdfField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            return new PdfSignatureField(field.Handle);
        }

        /// <summary>
        /// Get the digital signature value stored in this field.
        /// Returns null if no signature value is present.
        /// </summary>
        /// <returns>The <see cref="PdfDigitalSignature"/>, or null if the field is unsigned.</returns>
        public PdfDigitalSignature GetValue()
        {
            UInt32 result = NativeMethods.SignatureField_GetValue(SignatureFieldHandle, out PdfDigitalSignatureSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDigitalSignature(data);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            SignatureFieldHandle?.Dispose();
        }
    }
}
