using System;
using System.Text;
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
            UInt32 result = NativeMethods.Field_GetType(Handle, out PdfFieldType data);
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

        /// <summary>
        /// Set the partial field name (/T entry).
        /// </summary>
        /// <remarks>
        /// The PERIOD is the separator of fully qualified names, so a partial name shall not contain it.
        /// </remarks>
        /// <param name="value">The partial field name.</param>
        public void SetName(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.Field_SetName(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Set the alternate field name (/TU entry), used for display in user interfaces.
        /// </summary>
        /// <param name="value">The alternate field name.</param>
        public void SetAlternateName(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.Field_SetAlternateName(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Determine whether this is a terminal field - a logical field the user interacts with,
        /// whose children are widget annotations only - as opposed to a group of other fields.
        /// </summary>
        /// <remarks>
        /// This is independent of <see cref="GetFieldType"/>: a group can report a typed field type
        /// through its own or an inherited /FT entry.
        /// </remarks>
        /// <returns>True for a terminal field, false for a group.</returns>
        public bool IsTerminal()
        {
            UInt32 result = NativeMethods.Field_IsTerminal(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get the number of child fields - the /Kids entries that are fields, not widget annotations.
        /// </summary>
        /// <returns>The number of child fields, zero for a terminal field.</returns>
        public UInt64 GetChildCount()
        {
            UInt32 result = NativeMethods.Field_GetChildCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the child field at the given zero-based index, in /Kids order.
        /// </summary>
        /// <param name="index">Zero-based index among the child fields.</param>
        /// <returns>The child <see cref="PdfField"/> at <paramref name="index"/>.</returns>
        public PdfField GetChild(UInt64 index)
        {
            UInt32 result = NativeMethods.Field_GetChild(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <summary>
        /// Get the parent field, as written in the /Parent entry.
        /// </summary>
        /// <returns>The parent <see cref="PdfField"/>, or null for a top-level field.</returns>
        public PdfField GetParent()
        {
            UInt32 result = NativeMethods.Field_GetParent(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <summary>
        /// Get the number of widget annotations of the field - the /Kids entries that are not child fields.
        /// A field merged with its widget annotation reports one.
        /// </summary>
        /// <returns>The number of widget annotations.</returns>
        public UInt64 GetWidgetCount()
        {
            UInt32 result = NativeMethods.Field_GetWidgetCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the widget annotation at the given zero-based index, in /Kids order.
        /// </summary>
        /// <param name="index">Zero-based index among the widget annotations.</param>
        /// <returns>The <see cref="PdfWidgetAnnotation"/> at <paramref name="index"/>.</returns>
        public PdfWidgetAnnotation GetWidget(UInt64 index)
        {
            UInt32 result = NativeMethods.Field_GetWidget(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfWidgetAnnotation(data);
        }

        /// <summary>
        /// Attach a widget annotation to the field through /Kids and /Parent.
        /// </summary>
        /// <remarks>
        /// The page's /Annots entry is the page's own and is set with <see cref="PdfPageAnnotations"/>.
        /// Create the widget with <see cref="PdfWidgetAnnotation.CreateFromDocument"/>.
        /// </remarks>
        /// <param name="widget">The widget annotation to attach.</param>
        public void AddWidget(PdfWidgetAnnotation widget)
        {
            UInt32 result = NativeMethods.Field_AddWidget(Handle, widget.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Detach a widget annotation from the field's /Kids, clearing its /Parent.
        /// The page's /Annots entry is left untouched.
        /// </summary>
        /// <param name="widget">The widget annotation to detach.</param>
        /// <returns>False when the widget is not a kid of this field.</returns>
        public bool RemoveWidget(PdfWidgetAnnotation widget)
        {
            UInt32 result = NativeMethods.Field_RemoveWidget(Handle, widget.Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get the fully qualified field name - the partial field names (/T entries)
        /// joined with '.' from the root of the field hierarchy down to this field.
        /// </summary>
        /// <returns>The fully qualified name, in the form <see cref="PdfFieldTree.FindField"/> accepts.</returns>
        public string GetQualifiedName()
        {
            UInt32 result = NativeMethods.Field_GetQualifiedName(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            using (var buffer = new PdfBuffer(data)) {
                return Encoding.UTF8.GetString(buffer.Data);
            }
        }

        /// <summary>
        /// Get the field value (/V entry) as a raw object, resolved through the /Parent chain.
        /// </summary>
        /// <remarks>
        /// The object type depends on the field type. The typed accessors of the derived
        /// field classes, such as <see cref="PdfTextField.GetValue"/>, expose the same entry with its type.
        /// </remarks>
        /// <returns>The value object, or null if present on neither the field nor any ancestor.</returns>
        public PdfObject GetRawValue()
        {
            UInt32 result = NativeMethods.Field_GetValue(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Get the default field value (/DV entry) as a raw object, resolved through the /Parent chain.
        /// </summary>
        /// <returns>The default value object, or null if present on neither the field nor any ancestor.</returns>
        public PdfObject GetRawDefaultValue()
        {
            UInt32 result = NativeMethods.Field_GetDefaultValue(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Get the default appearance string (/DA entry) for variable text fields,
        /// resolved through the /Parent chain.
        /// </summary>
        /// <remarks>
        /// The document-wide default is not consulted; <see cref="PdfAcroForm.ResolveDefaultAppearance"/>
        /// performs the full lookup.
        /// </remarks>
        /// <returns>The default appearance string, or null if present on neither the field nor any ancestor.</returns>
        public PdfStringObject GetDefaultAppearance()
        {
            UInt32 result = NativeMethods.Field_GetDefaultAppearance(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Set the default appearance string (/DA entry) in this field's own dictionary,
        /// overriding any inherited value.
        /// </summary>
        /// <param name="value">The default appearance string.</param>
        public void SetDefaultAppearance(PdfStringObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.Field_SetDefaultAppearance(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the quadding (/Q entry) - the text justification of variable text fields -
        /// resolved through the /Parent chain.
        /// </summary>
        /// <remarks>
        /// The document-wide default is not consulted; <see cref="PdfAcroForm.ResolveQuadding"/>
        /// performs the full lookup.
        /// </remarks>
        /// <returns>The quadding, or null if present on neither the field nor any ancestor.</returns>
        public PdfQuaddingType? GetQuadding()
        {
            UInt32 result = NativeMethods.Field_GetQuadding(Handle, out PdfQuaddingType data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfQuaddingType>.CheckedCast(data);
        }

        /// <summary>
        /// Set the quadding (/Q entry) in this field's own dictionary, overriding any inherited value.
        /// </summary>
        /// <param name="value">The quadding.</param>
        public void SetQuadding(PdfQuaddingType value)
        {
            UInt32 result = NativeMethods.Field_SetQuadding(Handle, (Int32)value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Underlying low-level field dictionary.
        /// </summary>
        /// <remarks>
        /// After changing /Fields, /Kids or /Parent through the dictionary,
        /// call <see cref="PdfFieldTree.Invalidate"/>.
        /// </remarks>
        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.Field_GetBaseObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
