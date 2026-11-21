using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A content stream's named resources shall be defined
    /// by a resource dictionary, which shall enumerate the
    /// named resources needed by the operators in the
    /// content stream and the names by which they can be referred to.
    /// </summary>
    public class PdfResourceDictionary : IDisposable
    {
        internal PdfResourceDictionarySafeHandle Handle { get; }

        internal PdfResourceDictionary(PdfResourceDictionarySafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Underlying low-level resource dictionary object.
        /// </summary>
        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
        }

        /// <summary>
        /// Create an empty resource dictionary.
        /// </summary>
        /// <remarks>
        /// The dictionary becomes part of the document once it is attached through
        /// <see cref="PdfPageObject.SetResources"/> or <see cref="PdfFormXObject.Resources"/>.
        /// </remarks>
        /// <returns>A new <see cref="PdfResourceDictionary"/> instance.</returns>
        public static PdfResourceDictionary Create()
        {
            UInt32 result = NativeMethods.ResourceDictionary_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfResourceDictionary(data);
        }

        /// <summary>
        /// Create a resource dictionary from an existing low-level dictionary object.
        /// </summary>
        /// <param name="dictionary">The resource dictionary object.</param>
        /// <returns>A new <see cref="PdfResourceDictionary"/> instance.</returns>
        public static PdfResourceDictionary CreateFromDictionary(PdfDictionaryObject dictionary)
        {
            UInt32 result = NativeMethods.ResourceDictionary_CreateFromDictionary(dictionary.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfResourceDictionary(data);
        }

        /// <summary>
        /// A dictionary that maps resource names to font dictionaries.
        /// </summary>
        /// <returns>Handle to \ref PdfFontMap object on success, throws exception on failure</returns>
        public PdfFontMap GetFontMap()
        {
            UInt32 result = NativeMethods.ResourceDictionary_GetFontMap(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFontMap(data);
        }

        /// <summary>
        /// Get the number of entries in the XObject sub-dictionary, zero when it is absent.
        /// </summary>
        /// <returns>The number of XObject entries.</returns>
        public UInt64 GetXObjectCount()
        {
            UInt32 result = NativeMethods.ResourceDictionary_GetXObjectCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the resource name of the XObject entry at the specified position.
        /// </summary>
        /// <param name="index">Position of the entry, below <see cref="GetXObjectCount"/>.</param>
        /// <returns>The resource name.</returns>
        public PdfNameObject GetXObjectName(UInt64 index)
        {
            UInt32 result = NativeMethods.ResourceDictionary_GetXObjectName(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        /// <summary>
        /// Look up a named entry in the XObject sub-dictionary.
        /// </summary>
        /// <remarks>
        /// Check <see cref="PdfXObject.GetXObjectType"/> and convert the result to the concrete type,
        /// e.g. via <see cref="PdfFormXObject.FromXObject"/>.
        /// </remarks>
        /// <param name="name">The resource name to look up.</param>
        /// <returns>The <see cref="PdfXObject"/>, or null if there is no such entry.</returns>
        public PdfXObject FindXObject(PdfNameObject name)
        {
            UInt32 result = NativeMethods.ResourceDictionary_FindXObject(Handle, name.Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXObject(data);
        }

        /// <summary>
        /// Register an XObject under the resource name, creating the XObject sub-dictionary
        /// when missing and overwriting an existing entry.
        /// </summary>
        /// <remarks>
        /// The XObject's stream has to be registered within the document, see <see cref="PdfFormXObject.CreateFromDocument"/>.
        /// </remarks>
        /// <param name="name">The resource name to register the XObject under.</param>
        /// <param name="value">The XObject to register.</param>
        public void AddXObject(PdfNameObject name, PdfXObject value)
        {
            UInt32 result = NativeMethods.ResourceDictionary_AddXObject(Handle, name.Handle, value.XObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove a named entry from the XObject sub-dictionary.
        /// </summary>
        /// <param name="name">The resource name to remove.</param>
        /// <returns>False when there is no such entry.</returns>
        public bool RemoveXObject(PdfNameObject name)
        {
            UInt32 result = NativeMethods.ResourceDictionary_RemoveXObject(Handle, name.Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.ResourceDictionary_GetBaseObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            // Native declares a plain object, but a resource dictionary is always backed by a dictionary
            using (var obj = new PdfObject(data)) {
                return PdfDictionaryObject.FromObject(obj);
            }
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
