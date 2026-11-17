using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The group attributes dictionary (Group entry) of a page or a form XObject, describing
    /// how the group's contents are composited in the transparent imaging model.
    /// </summary>
    /// <remarks>
    /// The group colour space (CS entry) is not modelled yet - it is reachable through <see cref="BaseObject"/>.
    /// </remarks>
    public class PdfTransparencyGroup : IDisposable
    {
        internal PdfTransparencyGroupSafeHandle Handle { get; }

        internal PdfTransparencyGroup(PdfTransparencyGroupSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Underlying low-level dictionary object.
        /// </summary>
        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
        }

        /// <summary>
        /// Create an empty transparency group dictionary with the mandatory /S /Transparency subtype.
        /// </summary>
        /// <remarks>
        /// The dictionary becomes part of the document once it is attached through
        /// <see cref="PdfPageObject.Group"/> or <see cref="PdfFormXObject.Group"/>.
        /// </remarks>
        /// <returns>A new <see cref="PdfTransparencyGroup"/> instance.</returns>
        public static PdfTransparencyGroup Create()
        {
            UInt32 result = NativeMethods.TransparencyGroup_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfTransparencyGroup(data);
        }

        /// <summary>
        /// Get whether the group is isolated (I entry) - composited against a fully transparent backdrop.
        /// </summary>
        /// <returns>The value, or null if the entry is absent, in which case the group is not isolated.</returns>
        public bool? GetIsolated()
        {
            UInt32 result = NativeMethods.TransparencyGroup_GetIsolated(Handle, out bool data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set whether the group is isolated (I entry).
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetIsolated(bool value)
        {
            UInt32 result = NativeMethods.TransparencyGroup_SetIsolated(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get whether the group is a knockout group (K entry) - every element is composited with the group's initial backdrop.
        /// </summary>
        /// <returns>The value, or null if the entry is absent, in which case the group is not a knockout group.</returns>
        public bool? GetKnockout()
        {
            UInt32 result = NativeMethods.TransparencyGroup_GetKnockout(Handle, out bool data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set whether the group is a knockout group (K entry).
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetKnockout(bool value)
        {
            UInt32 result = NativeMethods.TransparencyGroup_SetKnockout(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.TransparencyGroup_GetBaseObject(Handle, out var data);
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
