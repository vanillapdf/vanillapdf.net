using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax.Extensions;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A dictionary object is an associative table containing pairs of objects
    /// </summary>
    public class PdfDictionaryObject : PdfObject, IDictionary<PdfNameObject, PdfObject>
    {
        internal PdfDictionaryObjectSafeHandle Handle { get; }

        internal PdfDictionaryObject(PdfDictionaryObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfDictionaryObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfDictionaryObject on success, throws exception on failure</returns>
        public static PdfDictionaryObject Create()
        {
            UInt32 result = NativeMethods.DictionaryObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        /// <summary>
        /// Get the number of entries in the dictionary.
        /// </summary>
        /// <returns>Total count of items.</returns>
        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.DictionaryObject_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Retrieve the value associated with the specified key.
        /// </summary>
        /// <param name="key">Dictionary key.</param>
        /// <returns>The stored <see cref="PdfObject"/>.</returns>
        public PdfObject Find(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Find(Handle, key.ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Try to get a value for the given key.
        /// </summary>
        /// <param name="key">Dictionary key.</param>
        /// <param name="value">Value if found.</param>
        /// <returns><c>true</c> if the key exists.</returns>
        public bool TryFind(PdfNameObject key, out PdfObject value)
        {
            UInt32 result = NativeMethods.DictionaryObject_TryFind(Handle, key.ObjectHandle, out var contains, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            if (!contains) {
                value = null;
                return false;
            }

            value = new PdfObject(data);
            return true;
        }

        /// <summary>
        /// Retrieve the value for a key converted to a specific type.
        /// </summary>
        /// <typeparam name="T">Expected object type.</typeparam>
        /// <param name="key">Dictionary key.</param>
        /// <returns>Object converted to <typeparamref name="T"/>.</returns>
        public T FindAs<T>(PdfNameObject key) where T : PdfObject
        {
            var result = Find(key);
            return PdfObjectConverter<T>.Convert(result);
        }

        /// <summary>
        /// Attempt to retrieve a value for a key converted to a given type.
        /// Resolves indirect references and validates type.
        /// </summary>
        /// <typeparam name="T">Expected object type.</typeparam>
        /// <param name="key">Dictionary key.</param>
        /// <param name="value">Converted value when found and type matches.</param>
        /// <returns><c>true</c> when the key exists and the value is of the expected type.</returns>
        public bool TryFindAs<T>(PdfNameObject key, out T value) where T : PdfObject
        {
            if (!TryFind(key, out var pdfObject)) {
                value = null;
                return false;
            }

            using (pdfObject) {
                using (var upgraded = pdfObject.Upgrade()) {
                    value = PdfObjectConverter<T>.TryConvert(upgraded);
                    return value != null;
                }
            }
        }

        /// <summary>
        /// Check if the dictionary contains an entry for the given key.
        /// </summary>
        /// <param name="key">Dictionary key.</param>
        /// <returns><c>true</c> when the key is present.</returns>
        public bool Contains(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Contains(Handle, key.ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get dictionary object iterator
        /// </summary>
        /// <returns>Handle to iterator for enumerating objects within dictionary on success, throws exception on failure</returns>
        public PdfDictionaryObjectIterator GetIterator()
        {
            UInt32 result = NativeMethods.DictionaryObject_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObjectIterator(value);
        }

        /// <summary>
        /// Insert a key/value pair into the dictionary.
        /// </summary>
        /// <param name="key">Dictionary key.</param>
        /// <param name="data">Value to insert.</param>
        /// <param name="overwrite">Overwrite existing entry when <c>true</c>.</param>
        public void Insert(PdfNameObject key, PdfObject data, bool overwrite = false)
        {
            UInt32 result = NativeMethods.DictionaryObject_Insert(Handle, key.ObjectHandle, data.ObjectHandle, overwrite);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public bool Remove(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Remove(Handle, key.ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            UInt32 result = NativeMethods.DictionaryObject_Clear(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert object to dictionary object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfDictionaryObject if the object can be converted, throws exception on failure</returns>
        public static PdfDictionaryObject FromObject(PdfObject data)
        {
            return new PdfDictionaryObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to dictionary object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfDictionaryObject if the object is a dictionary, null otherwise</returns>
        public static PdfDictionaryObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.Dictionary) {
                return null;
            }

            return new PdfDictionaryObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #region IDictionary<PdfNameObject, PdfObject>

        /// <inheritdoc/>
        public ICollection<PdfNameObject> Keys
        {
            get
            {
                List<PdfNameObject> result = new List<PdfNameObject>();
                foreach(var item in this) {
                    result.Add(item.Key);
                }

                return result;
            }
        }

        /// <inheritdoc/>
        public ICollection<PdfObject> Values
        {
            get
            {
                List<PdfObject> result = new List<PdfObject>();
                foreach (var item in this) {
                    result.Add(item.Value);
                }

                return result;
            }
        }

        /// <inheritdoc/>
        public int Count => (int)GetSize();

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public PdfObject this[PdfNameObject key] { get => Find(key); set => Insert(key, value, true); }

        /// <inheritdoc/>
        public void Add(PdfNameObject key, PdfObject value)
        {
            Insert(key, value);
        }

        /// <inheritdoc/>
        public bool ContainsKey(PdfNameObject key)
        {
            return Contains(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(PdfNameObject key, out PdfObject value)
        {
            return TryFind(key, out value);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            Insert(item.Key, item.Value);
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            return Contains(item.Key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<PdfNameObject, PdfObject>[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var item in this) {
                array[currentIndex++] = item;
            }
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            return Remove(item.Key);
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        public IEnumerator<KeyValuePair<PdfNameObject, PdfObject>> GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        #endregion
    }
}
