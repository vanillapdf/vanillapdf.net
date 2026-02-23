using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a text form field in a PDF AcroForm.
    /// </summary>
    public class PdfTextField : PdfField
    {
        internal PdfTextFieldSafeHandle TextFieldHandle { get; }

        internal PdfTextField(PdfTextFieldSafeHandle handle) : base(handle)
        {
            TextFieldHandle = handle;
        }

        /// <summary>
        /// Convert a generic field to a text field.
        /// </summary>
        /// <param name="field">The field to convert. Must have type <see cref="PdfFieldType.Text"/>.</param>
        /// <returns>A <see cref="PdfTextField"/> wrapping the same field.</returns>
        public static PdfTextField FromField(PdfField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            return new PdfTextField(field.Handle);
        }

        /// <summary>
        /// Get the current value of the text field.
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the value, or null.</returns>
        public PdfStringObject GetValue()
        {
            UInt32 result = NativeMethods.TextField_GetValue(TextFieldHandle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Set the value of the text field.
        /// </summary>
        /// <param name="value">The <see cref="PdfStringObject"/> value to set.</param>
        public void SetValue(PdfStringObject value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            UInt32 result = NativeMethods.TextField_SetValue(TextFieldHandle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the default value of the text field (/DV entry).
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the default value, or null.</returns>
        public PdfStringObject GetDefaultValue()
        {
            UInt32 result = NativeMethods.TextField_GetDefaultValue(TextFieldHandle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the maximum length of the text field (/MaxLen entry).
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfIntegerObject"/> holding the max length, or null.</returns>
        public PdfIntegerObject GetMaxLength()
        {
            UInt32 result = NativeMethods.TextField_GetMaxLength(TextFieldHandle, out PdfIntegerObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            TextFieldHandle?.Dispose();
        }
    }
}
