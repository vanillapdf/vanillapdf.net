using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfArrayObject : PdfObject, IList<PdfObject>
    {
        internal PdfArrayObject(PdfArrayObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfArrayObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfArrayObject Create()
        {
            UInt32 result = NativeMethods.ArrayObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfArrayObject(data);
        }

        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.ArrayObject_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfObject GetValue(UInt64 index)
        {
            UInt32 result = NativeMethods.ArrayObject_GetValue(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public void SetValue(UInt64 index, PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_SetValue(Handle, new UIntPtr(index), item.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Append(PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_Append(Handle, item.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Insert(UInt64 index, PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_Insert(Handle, new UIntPtr(index), item.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool Remove(UInt64 index)
        {
            UInt32 result = NativeMethods.ArrayObject_Remove(Handle, new UIntPtr(index));
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return false;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return true;
        }

        public void Clear()
        {
            UInt32 result = NativeMethods.ArrayObject_Clear(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfArrayObject FromObject(PdfObject data)
        {
            return new PdfArrayObject(data.Handle);
        }

        #region IList<PdfObject>

        public int Count => (int)GetSize();
        public bool IsReadOnly => false;
        public PdfObject this[int index] { get => GetValue((UInt64)index); set => SetValue((UInt64)index, value); }

        public int IndexOf(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                var current = GetValue((UInt64)i);

                if (item.Equals(current)) {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, PdfObject item)
        {
            Insert((UInt64)index, item);
        }

        public void RemoveAt(int index)
        {
            Remove((UInt64)index);
        }

        public void Add(PdfObject item)
        {
            Append(item);
        }

        public bool Contains(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                var current = GetValue((UInt64)i);

                if (item.Equals(current)) {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(PdfObject[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; ++i) {
                array[arrayIndex++] = GetValue((UInt64)i);
            }
        }

        public bool Remove(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                var current = GetValue((UInt64)i);

                if (item.Equals(current)) {
                    return Remove((UInt64)i);
                }
            }

            return false;
        }

        public IEnumerator<PdfObject> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i) {
                yield return GetValue((UInt64)i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; ++i) {
                yield return GetValue((UInt64)i);
            }
        }

        #endregion

        private static class NativeMethods
        {
            public static CreateDelgate ArrayObject_Create = LibraryInstance.GetFunction<CreateDelgate>("ArrayObject_Create");
            public static GetSizeDelgate ArrayObject_GetSize = LibraryInstance.GetFunction<GetSizeDelgate>("ArrayObject_GetSize");
            public static GetValueDelgate ArrayObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ArrayObject_GetValue");
            public static SetValueDelgate ArrayObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("ArrayObject_SetValue");
            public static AppendDelgate ArrayObject_Append = LibraryInstance.GetFunction<AppendDelgate>("ArrayObject_Append");
            public static InsertDelgate ArrayObject_Insert = LibraryInstance.GetFunction<InsertDelgate>("ArrayObject_Insert");
            public static RemoveDelgate ArrayObject_Remove = LibraryInstance.GetFunction<RemoveDelgate>("ArrayObject_Remove");
            public static ClearDelgate ArrayObject_Clear = LibraryInstance.GetFunction<ClearDelgate>("ArrayObject_Clear");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfArrayObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSizeDelgate(PdfArrayObjectSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AppendDelgate(PdfArrayObjectSafeHandle handle, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ClearDelgate(PdfArrayObjectSafeHandle handle);
        }
    }
}
