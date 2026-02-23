using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a choice form field (list box or combo box) in a PDF AcroForm.
    /// </summary>
    public class PdfChoiceField : PdfField
    {
        internal PdfChoiceFieldSafeHandle ChoiceFieldHandle { get; }

        internal PdfChoiceField(PdfChoiceFieldSafeHandle handle) : base(handle)
        {
            ChoiceFieldHandle = handle;
        }

        /// <summary>
        /// Convert a generic field to a choice field.
        /// </summary>
        /// <param name="field">The field to convert. Must have type <see cref="PdfFieldType.Choice"/>.</param>
        /// <returns>A <see cref="PdfChoiceField"/> wrapping the same field.</returns>
        public static PdfChoiceField FromField(PdfField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            return new PdfChoiceField(field.Handle);
        }

        /// <summary>
        /// Get the current value of the choice field.
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the value, or null.</returns>
        public PdfStringObject GetValue()
        {
            UInt32 result = NativeMethods.ChoiceField_GetValue(ChoiceFieldHandle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Set the value of the choice field.
        /// </summary>
        /// <param name="value">The <see cref="PdfStringObject"/> value to set.</param>
        public void SetValue(PdfStringObject value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            UInt32 result = NativeMethods.ChoiceField_SetValue(ChoiceFieldHandle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the number of options in the choice field.
        /// </summary>
        /// <returns>The number of options.</returns>
        public UInt64 GetOptionCount()
        {
            UInt32 result = NativeMethods.ChoiceField_GetOptionCount(ChoiceFieldHandle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get an option at the given index.
        /// </summary>
        /// <param name="index">Zero-based index of the option.</param>
        /// <returns>The <see cref="PdfObject"/> representing the option.</returns>
        public PdfObject GetOptionAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ChoiceField_GetOptionAt(ChoiceFieldHandle, new UIntPtr(index), out PdfObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            ChoiceFieldHandle?.Dispose();
        }
    }
}
