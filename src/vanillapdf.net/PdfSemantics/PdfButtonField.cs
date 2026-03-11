using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a button form field (check box, radio button, or push button) in a PDF AcroForm.
    /// </summary>
    public class PdfButtonField : PdfField
    {
        internal PdfButtonFieldSafeHandle ButtonFieldHandle { get; }

        internal PdfButtonField(PdfButtonFieldSafeHandle handle) : base(handle)
        {
            ButtonFieldHandle = handle;
        }

        /// <summary>
        /// Convert a generic field to a button field.
        /// </summary>
        /// <param name="field">The field to convert. Must have type <see cref="PdfFieldType.Button"/>.</param>
        /// <returns>A <see cref="PdfButtonField"/> wrapping the same field.</returns>
        public static PdfButtonField FromField(PdfField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            return new PdfButtonField(field.Handle);
        }

        /// <summary>
        /// Get the current value of the button field.
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfNameObject"/> holding the value, or null.</returns>
        public PdfNameObject GetValue()
        {
            UInt32 result = NativeMethods.ButtonField_GetValue(ButtonFieldHandle, out PdfNameObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        /// <summary>
        /// Set the value of the button field.
        /// </summary>
        /// <param name="value">The <see cref="PdfNameObject"/> value to set.</param>
        public void SetValue(PdfNameObject value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            UInt32 result = NativeMethods.ButtonField_SetValue(ButtonFieldHandle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            ButtonFieldHandle?.Dispose();
        }
    }
}
