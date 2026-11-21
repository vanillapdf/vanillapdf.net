using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The appearance characteristics dictionary (MK entry) of a widget annotation, describing
    /// how the widget shall be presented when its appearance streams are regenerated.
    /// </summary>
    /// <remarks>
    /// Only a subset of the entries is modelled - the remainder is reachable through <see cref="BaseObject"/>.
    /// </remarks>
    public class PdfAppearanceCharacteristics : IDisposable
    {
        internal PdfAppearanceCharacteristicsSafeHandle Handle { get; }

        internal PdfAppearanceCharacteristics(PdfAppearanceCharacteristicsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The colour of the widget's border (BC entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfColor BorderColor
        {
            get => GetBorderColor();
            set => SetBorderColor(value);
        }

        /// <summary>
        /// The colour of the widget's background (BG entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfColor BackgroundColor
        {
            get => GetBackgroundColor();
            set => SetBackgroundColor(value);
        }

        /// <summary>
        /// The caption displayed when the widget is not interacting with the user (CA entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfStringObject NormalCaption
        {
            get => GetNormalCaption();
            set => SetNormalCaption(value);
        }

        /// <summary>
        /// The caption displayed when the cursor enters the widget (RC entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfStringObject RolloverCaption
        {
            get => GetRolloverCaption();
            set => SetRolloverCaption(value);
        }

        /// <summary>
        /// The caption displayed when the mouse button is pressed within the widget (AC entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfStringObject DownCaption
        {
            get => GetDownCaption();
            set => SetDownCaption(value);
        }

        /// <summary>
        /// Underlying low-level dictionary object.
        /// </summary>
        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
        }

        /// <summary>
        /// Create an empty appearance characteristics dictionary.
        /// </summary>
        /// <remarks>
        /// The dictionary becomes part of the document once it is attached through
        /// <see cref="PdfWidgetAnnotation.AppearanceCharacteristics"/>.
        /// </remarks>
        /// <returns>A new <see cref="PdfAppearanceCharacteristics"/> instance.</returns>
        public static PdfAppearanceCharacteristics Create()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAppearanceCharacteristics(data);
        }

        /// <summary>
        /// Get the number of degrees by which the widget is rotated counterclockwise relative to the page (R entry).
        /// </summary>
        /// <returns>The value, or null if the entry is absent, in which case the widget is not rotated.</returns>
        public Int64? GetRotation()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetRotation(Handle, out Int64 data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set the number of degrees by which the widget is rotated counterclockwise relative to the page (R entry). The value shall be a multiple of 90.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetRotation(Int64 value)
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_SetRotation(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfColor GetBorderColor()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetBorderColor(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfColor(data);
        }

        private void SetBorderColor(PdfColor value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.AppearanceCharacteristics_SetBorderColor(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfColor GetBackgroundColor()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetBackgroundColor(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfColor(data);
        }

        private void SetBackgroundColor(PdfColor value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.AppearanceCharacteristics_SetBackgroundColor(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfStringObject GetNormalCaption()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetNormalCaption(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        private void SetNormalCaption(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.AppearanceCharacteristics_SetNormalCaption(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfStringObject GetRolloverCaption()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetRolloverCaption(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        private void SetRolloverCaption(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.AppearanceCharacteristics_SetRolloverCaption(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfStringObject GetDownCaption()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetDownCaption(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        private void SetDownCaption(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.AppearanceCharacteristics_SetDownCaption(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.AppearanceCharacteristics_GetBaseObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
