using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents a list of object attributes
    /// </summary>
    public class PdfObjectAttributeList : IDisposable
    {
        internal PdfObjectAttributeListSafeHandle Handle { get; }

        internal PdfObjectAttributeList(PdfObjectAttributeListSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfObjectAttributeList with default value
        /// </summary>
        /// <returns>New instance of \ref PdfObjectAttributeList on success, throws exception on failure</returns>
        public static PdfObjectAttributeList Create()
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObjectAttributeList(data);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the list
        /// </summary>
        /// <param name="value">The object to use as the value of the element to add</param>
        public void Add(PdfBaseObjectAttribute value)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Add(Handle, value.BaseAttributeHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Determines whether the list contains an element with the specified key
        /// </summary>
        /// <param name="key">The key to locate in the list</param>
        /// <returns>true if the list contains an element with the key; otherwise, false</returns>
        public bool Contains(PdfObjectAttributeType key)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Contains(Handle, key, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Removes the element with the specified key from the list
        /// </summary>
        /// <param name="key">The key of the element to remove</param>
        public void Remove(PdfObjectAttributeType key)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Remove(Handle, key);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Gets the element with the specified key
        /// </summary>
        /// <param name="key">The key of the element to remove</param>
        /// <returns>New instance of \ref PdfBaseObjectAttribute on success, throws exception on failure</returns>
        public PdfBaseObjectAttribute Get(PdfObjectAttributeType key)
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Get(Handle, key, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBaseObjectAttribute(data);
        }

        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void Clear()
        {
            UInt32 result = NativeMethods.ObjectAttributeList_Clear(Handle);
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
