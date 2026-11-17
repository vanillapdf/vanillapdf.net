using System;
using System.Text;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The field hierarchy of an interactive form (PDF 1.2, section 12.7.3).
    /// </summary>
    /// <remarks>
    /// The tree offers two views over the hierarchy:
    /// <list type="bullet">
    /// <item>The flat view - <see cref="GetFieldCount"/>, <see cref="GetField"/> and
    /// <see cref="FindField"/> - enumerates the resolved terminal fields in document order.
    /// This is the default path for form filling.</item>
    /// <item>The structural view starts at the top level with <see cref="GetRootChildCount"/> and
    /// <see cref="GetRootChild"/> and continues down through <see cref="PdfField.GetChildCount"/>
    /// and <see cref="PdfField.GetChild"/>.</item>
    /// </list>
    /// Every field mutation goes through the tree, because the tree owns the cache behind the flat view.
    /// After editing /Fields, /Kids or /Parent through the dictionary API, call <see cref="Invalidate"/>.
    /// </remarks>
    public class PdfFieldTree : IDisposable
    {
        internal PdfFieldTreeSafeHandle Handle { get; }

        internal PdfFieldTree(PdfFieldTreeSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create an empty field hierarchy belonging to the document.
        /// </summary>
        /// <remarks>
        /// Fields can be added right away, but the hierarchy does not take effect until it is
        /// attached to a form with <see cref="PdfAcroForm.SetFieldTree"/>.
        /// </remarks>
        /// <param name="document">The document that owns the new hierarchy.</param>
        /// <returns>A new <see cref="PdfFieldTree"/> instance.</returns>
        public static PdfFieldTree CreateFromDocument(PdfDocument document)
        {
            UInt32 result = NativeMethods.FieldTree_CreateFromDocument(document.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFieldTree(data);
        }

        /// <summary>
        /// Get the number of resolved terminal fields in the hierarchy.
        /// </summary>
        /// <returns>The number of terminal fields.</returns>
        public UInt64 GetFieldCount()
        {
            UInt32 result = NativeMethods.FieldTree_GetFieldCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the resolved terminal field at the given zero-based index, in document order.
        /// </summary>
        /// <param name="index">Zero-based index within the flat view.</param>
        /// <returns>The <see cref="PdfField"/> at <paramref name="index"/>.</returns>
        public PdfField GetField(UInt64 index)
        {
            UInt32 result = NativeMethods.FieldTree_GetField(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <summary>
        /// Find a terminal field by its fully qualified name - the partial field names
        /// joined with '.', the form <see cref="PdfField.GetQualifiedName"/> produces.
        /// </summary>
        /// <remarks>
        /// A name shared by several fields resolves to the first terminal field in document order.
        /// </remarks>
        /// <param name="qualifiedName">The fully qualified field name.</param>
        /// <returns>The matching <see cref="PdfField"/>, or null if no terminal field has that name.</returns>
        public PdfField FindField(string qualifiedName)
        {
            byte[] name = Encoding.UTF8.GetBytes(qualifiedName);

            UInt32 result = NativeMethods.FieldTree_FindField(Handle, name, new UIntPtr((uint)name.Length), out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <summary>
        /// Get the number of top-level fields - the entries of the root /Fields array
        /// that are field dictionaries, groups included.
        /// </summary>
        /// <returns>The number of top-level fields.</returns>
        public UInt64 GetRootChildCount()
        {
            UInt32 result = NativeMethods.FieldTree_GetRootChildCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the top-level field at the given zero-based index, in /Fields order.
        /// </summary>
        /// <param name="index">Zero-based index among the top-level fields.</param>
        /// <returns>The top-level <see cref="PdfField"/> at <paramref name="index"/>.</returns>
        public PdfField GetRootChild(UInt64 index)
        {
            UInt32 result = NativeMethods.FieldTree_GetRootChild(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfField(data);
        }

        /// <summary>
        /// Append a top-level field to the root /Fields array.
        /// </summary>
        /// <remarks>
        /// The field's dictionary has to be an indirect object within the document,
        /// and none of the fully qualified names it brings may already exist.
        /// </remarks>
        /// <param name="child">The field to append.</param>
        public void AddRootChild(PdfField child)
        {
            UInt32 result = NativeMethods.FieldTree_AddRootChild(Handle, child.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Insert a top-level field at the given zero-based position among the root /Fields entries.
        /// </summary>
        /// <param name="index">Position in the /Fields array; the array size appends.</param>
        /// <param name="child">The field to insert.</param>
        public void InsertRootChild(UInt64 index, PdfField child)
        {
            UInt32 result = NativeMethods.FieldTree_InsertRootChild(Handle, new UIntPtr(index), child.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Append a child field to a parent field of this hierarchy.
        /// </summary>
        /// <remarks>
        /// Adding a child to a terminal field turns it into a group.
        /// </remarks>
        /// <param name="parent">The parent field within this hierarchy.</param>
        /// <param name="child">The field to append.</param>
        public void AddChild(PdfField parent, PdfField child)
        {
            UInt32 result = NativeMethods.FieldTree_AddChild(Handle, parent.Handle, child.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Insert a child field at the given zero-based position among the parent's /Kids entries.
        /// </summary>
        /// <param name="parent">The parent field within this hierarchy.</param>
        /// <param name="index">Position among the parent's /Kids entries; the child count appends.</param>
        /// <param name="child">The field to insert.</param>
        public void InsertChild(PdfField parent, UInt64 index, PdfField child)
        {
            UInt32 result = NativeMethods.FieldTree_InsertChild(Handle, parent.Handle, new UIntPtr(index), child.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove a field - top-level or nested - from the hierarchy, together with its subtree.
        /// </summary>
        /// <remarks>
        /// The widget annotations of the removed fields stay in the page /Annots arrays;
        /// remove them through <see cref="PdfPageAnnotations"/>.
        /// </remarks>
        /// <param name="field">The field to remove.</param>
        public void RemoveChild(PdfField field)
        {
            UInt32 result = NativeMethods.FieldTree_RemoveChild(Handle, field.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Discard the cached flat view, so the next access rebuilds it from the dictionaries.
        /// </summary>
        /// <remarks>
        /// Call this after editing /Fields, /Kids or /Parent through the dictionary API.
        /// </remarks>
        public void Invalidate()
        {
            UInt32 result = NativeMethods.FieldTree_Invalidate(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
