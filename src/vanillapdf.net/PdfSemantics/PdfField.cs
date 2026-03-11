using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents an interactive form field in a PDF AcroForm.
    /// </summary>
    public class PdfField : IDisposable
    {
        internal PdfFieldSafeHandle Handle { get; }

        internal PdfField(PdfFieldSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the field type.
        /// </summary>
        /// <returns>The <see cref="PdfFieldType"/> of this field.</returns>
        public PdfFieldType GetFieldType()
        {
            UInt32 result = NativeMethods.Field_GetType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfFieldType>.CheckedCast(data);
        }

        /// <summary>
        /// Get the partial field name (/T entry).
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the field name, or null.</returns>
        public PdfStringObject GetName()
        {
            UInt32 result = NativeMethods.Field_GetName(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the alternate field name (/TU entry), used for display in user interfaces.
        /// Returns null if absent.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the alternate name, or null.</returns>
        public PdfStringObject GetAlternateName()
        {
            UInt32 result = NativeMethods.Field_GetAlternateName(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the field flags (/Ff entry).
        /// </summary>
        /// <returns>The <see cref="PdfFieldFlags"/> for this field.</returns>
        public PdfFieldFlags GetFieldFlags()
        {
            UInt32 result = NativeMethods.Field_GetFieldFlags(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfFieldFlags>.FlagsCast(data);
        }

        /// <summary>
        /// Set the field flags (/Ff entry).
        /// </summary>
        /// <param name="flags">The <see cref="PdfFieldFlags"/> to set.</param>
        public void SetFieldFlags(PdfFieldFlags flags)
        {
            UInt32 result = NativeMethods.Field_SetFieldFlags(Handle, (Int32)flags);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
