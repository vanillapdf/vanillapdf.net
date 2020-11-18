using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfDictionaryObject : PdfObject, IDictionary<PdfNameObject, PdfObject>
    {
        internal PdfDictionaryObject(PdfDictionaryObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfDictionaryObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfDictionaryObject Create()
        {
            UInt32 result = NativeMethods.DictionaryObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.DictionaryObject_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfObject Find(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Find(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public bool Contains(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Contains(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public PdfDictionaryObjectIterator GetIterator()
        {
            UInt32 result = NativeMethods.DictionaryObject_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObjectIterator(value);
        }

        public void Insert(PdfNameObject key, PdfObject data)
        {
            UInt32 result = NativeMethods.DictionaryObject_Insert(Handle, key.Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool Remove(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Remove(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public void Clear()
        {
            UInt32 result = NativeMethods.DictionaryObject_Clear(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfDictionaryObject FromObject(PdfObject data)
        {
            return new PdfDictionaryObject(data.Handle);
        }

        #region IDictionary<PdfNameObject, PdfObject>

        /** \name IDictionary<PdfNameObject, PdfObject>
        *  @{
        */

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

        public int Count => (int)GetSize();
        public bool IsReadOnly => false;

        public PdfObject this[PdfNameObject key] { get => Find(key); set => Insert(key, value); }

        public void Add(PdfNameObject key, PdfObject value)
        {
            Insert(key, value);
        }

        public bool ContainsKey(PdfNameObject key)
        {
            return Contains(key);
        }

        public bool TryGetValue(PdfNameObject key, out PdfObject value)
        {
            if (Contains(key)) {
                value = Find(key);
                return true;
            } else {
                value = null;
                return false;
            }
        }

        public void Add(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            Insert(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            if (!Contains(item.Key)) {
                return false;
            }

            var value = Find(item.Key);
            return value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<PdfNameObject, PdfObject>[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var item in this) {
                array[currentIndex++] = item;
            }
        }

        public bool Remove(KeyValuePair<PdfNameObject, PdfObject> item)
        {
            if (Contains(item)) {
                return false;
            }

            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<PdfNameObject, PdfObject>> GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        /** @}*/

        #endregion

        private static class NativeMethods
        {
            public static CreateDelgate DictionaryObject_Create = LibraryInstance.GetFunction<CreateDelgate>("DictionaryObject_Create");
            public static GetSizeDelgate DictionaryObject_GetSize = LibraryInstance.GetFunction<GetSizeDelgate>("DictionaryObject_GetSize");
            public static FindDelgate DictionaryObject_Find = LibraryInstance.GetFunction<FindDelgate>("DictionaryObject_Find");
            public static ContainsDelgate DictionaryObject_Contains = LibraryInstance.GetFunction<ContainsDelgate>("DictionaryObject_Contains");
            public static GetIteratorDelgate DictionaryObject_GetIterator = LibraryInstance.GetFunction<GetIteratorDelgate>("DictionaryObject_GetIterator");
            public static RemoveDelgate DictionaryObject_Remove = LibraryInstance.GetFunction<RemoveDelgate>("DictionaryObject_Remove");
            public static ClearDelgate DictionaryObject_Clear = LibraryInstance.GetFunction<ClearDelgate>("DictionaryObject_Clear");
            public static InsertDelgate DictionaryObject_Insert = LibraryInstance.GetFunction<InsertDelgate>("DictionaryObject_Insert");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfDictionaryObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSizeDelgate(PdfDictionaryObjectSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FindDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ContainsDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIteratorDelgate(PdfDictionaryObjectSafeHandle handle, out PdfDictionaryObjectIteratorSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ClearDelgate(PdfDictionaryObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, PdfObjectSafeHandle data);
        }
    }
}
