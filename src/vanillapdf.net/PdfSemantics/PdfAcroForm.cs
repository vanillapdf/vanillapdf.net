using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents the AcroForm (interactive form) of a PDF document.
    /// </summary>
    /// <remarks>
    /// The form exposes its document-level attributes; the fields themselves
    /// live in the field hierarchy obtained from <see cref="GetFieldTree"/>.
    /// </remarks>
    public class PdfAcroForm : IDisposable
    {
        internal PdfInteractiveFormSafeHandle Handle { get; }

        internal PdfAcroForm(PdfInteractiveFormSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create an empty interactive form registered as an indirect object within the document.
        /// </summary>
        /// <remarks>
        /// The form does not take effect until it is attached with <see cref="PdfCatalog.SetAcroForm"/>.
        /// </remarks>
        /// <param name="document">The document that owns the new form.</param>
        /// <returns>A new <see cref="PdfAcroForm"/> instance.</returns>
        public static PdfAcroForm CreateFromDocument(PdfDocument document)
        {
            UInt32 result = NativeMethods.InteractiveForm_CreateFromDocument(document.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAcroForm(data);
        }

        /// <summary>
        /// Create an interactive form from an existing low-level dictionary object,
        /// for example an indirect object already registered within the document.
        /// </summary>
        /// <param name="dictionary">The interactive form dictionary.</param>
        /// <returns>A new <see cref="PdfAcroForm"/> instance.</returns>
        public static PdfAcroForm CreateFromDictionary(PdfDictionaryObject dictionary)
        {
            UInt32 result = NativeMethods.InteractiveForm_CreateFromDictionary(dictionary.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAcroForm(data);
        }

        /// <summary>
        /// Get the collection of all fields in this AcroForm.
        /// </summary>
        /// <returns>The <see cref="PdfFieldCollection"/>, or null if no fields entry exists.</returns>
        [Obsolete("The raw /Fields array models dictionary nodes rather than logical fields. Use GetFieldTree instead.")]
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

        /// <summary>
        /// Get the field hierarchy of the form - its /Fields entry.
        /// </summary>
        /// <remarks>
        /// Reading never creates the entry. The same hierarchy is returned for the
        /// lifetime of this form, so its cache is shared by everyone holding the form.
        /// </remarks>
        /// <returns>The <see cref="PdfFieldTree"/>, or null if the form has no /Fields entry.</returns>
        public PdfFieldTree GetFieldTree()
        {
            UInt32 result = NativeMethods.InteractiveForm_GetFieldTree(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFieldTree(data);
        }

        /// <summary>
        /// Attach a field hierarchy to the form as its /Fields entry, replacing any existing one.
        /// </summary>
        /// <remarks>
        /// Obtain an empty hierarchy from <see cref="PdfFieldTree.CreateFromDocument"/>.
        /// The hierarchy has to belong to the same document as the form.
        /// </remarks>
        /// <param name="fieldTree">The field hierarchy to attach.</param>
        public void SetFieldTree(PdfFieldTree fieldTree)
        {
            UInt32 result = NativeMethods.InteractiveForm_SetFieldTree(Handle, fieldTree.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the flags specifying document-level characteristics related to signature fields.
        /// </summary>
        /// <returns>The <see cref="PdfSignatureFlags"/>, or null if the form has no /SigFlags entry.</returns>
        public PdfSignatureFlags GetSignatureFlags()
        {
            UInt32 result = NativeMethods.InteractiveForm_GetSignatureFlags(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSignatureFlags(data);
        }

        /// <summary>
        /// Set the flags specifying document-level characteristics related to signature fields.
        /// </summary>
        /// <param name="flags">The flags to set, see <see cref="PdfSignatureFlags.Create"/>.</param>
        public void SetSignatureFlags(PdfSignatureFlags flags)
        {
            UInt32 result = NativeMethods.InteractiveForm_SetSignatureFlags(Handle, flags.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the document-wide default appearance string (/DA entry).
        /// </summary>
        /// <returns>The default appearance string, or null if the entry is not present.</returns>
        public PdfStringObject GetDefaultAppearance()
        {
            UInt32 result = NativeMethods.InteractiveForm_GetDefaultAppearance(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Set the document-wide default appearance string (/DA entry).
        /// </summary>
        /// <param name="value">The default appearance string.</param>
        public void SetDefaultAppearance(PdfStringObject value)
        {
            UInt32 result = NativeMethods.InteractiveForm_SetDefaultAppearance(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the default appearance string a field is rendered with: the field's own entry,
        /// then its ancestors through /Parent, then this form's document default.
        /// </summary>
        /// <param name="field">The field to resolve the default appearance for.</param>
        /// <returns>The resolved default appearance string, or null if it is present nowhere.</returns>
        public PdfStringObject ResolveDefaultAppearance(PdfField field)
        {
            UInt32 result = NativeMethods.InteractiveForm_ResolveDefaultAppearance(Handle, field.Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the document-wide default quadding (/Q entry) - the text justification of variable text fields.
        /// </summary>
        /// <returns>The default quadding, or null if the entry is not present.</returns>
        public PdfQuaddingType? GetQuadding()
        {
            UInt32 result = NativeMethods.InteractiveForm_GetQuadding(Handle, out PdfQuaddingType data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfQuaddingType>.CheckedCast(data);
        }

        /// <summary>
        /// Set the document-wide default quadding (/Q entry).
        /// </summary>
        /// <param name="value">The default quadding.</param>
        public void SetQuadding(PdfQuaddingType value)
        {
            UInt32 result = NativeMethods.InteractiveForm_SetQuadding(Handle, (Int32)value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the quadding a field is rendered with: the field's own entry, then its ancestors
        /// through /Parent, then this form's document default, and finally
        /// <see cref="PdfQuaddingType.LeftJustified"/> - so it always resolves.
        /// </summary>
        /// <param name="field">The field to resolve the quadding for.</param>
        /// <returns>The resolved quadding.</returns>
        public PdfQuaddingType ResolveQuadding(PdfField field)
        {
            UInt32 result = NativeMethods.InteractiveForm_ResolveQuadding(Handle, field.Handle, out PdfQuaddingType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfQuaddingType>.CheckedCast(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
