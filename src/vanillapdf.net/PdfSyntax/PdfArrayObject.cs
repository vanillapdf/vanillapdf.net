using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// An array object is a one-dimensional collection of objects arranged sequentially
    /// </summary>
    public class PdfArrayObject : PdfObject, IList<PdfObject>
    {
        internal PdfArrayObjectSafeHandle Handle { get; }

        internal PdfArrayObject(PdfArrayObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfArrayObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfArrayObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Create a new instance of \ref PdfArrayObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfArrayObject on success, throws exception on failure</returns>
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

            using (var baseObject = new PdfObject(data)) {
                return GetAsDerivedObject(baseObject);
            }
        }

        public T GetValueAs<T>(UInt64 index) where T : PdfObject
        {
            var result = GetValue(index);
            return (T)result.ConvertTo<T>();
        }

        public void SetValue(UInt64 index, PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_SetValue(Handle, new UIntPtr(index), item.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Append(PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_Append(Handle, item.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Insert(UInt64 index, PdfObject item)
        {
            UInt32 result = NativeMethods.ArrayObject_Insert(Handle, new UIntPtr(index), item.ObjectHandle);
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

        internal override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfArrayObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Convert object to array object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfArrayObject if the object can be converted, throws exception on failure</returns>
        public static PdfArrayObject FromObject(PdfObject data)
        {
            if (data is PdfArrayObject pdfArrayObject) {
                return pdfArrayObject;
            }

            return new PdfArrayObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #region private helper

        private PdfObject GetValue(int index)
        {
            UInt64 indexConverted = MiscUtils.PlatformIntegerConversion(index);
            return GetValue(indexConverted);
        }

        private void SetValue(int index, PdfObject item)
        {
            UInt64 indexConverted = MiscUtils.PlatformIntegerConversion(index);
            SetValue(indexConverted, item);
        }

        #endregion

        #region IList<PdfObject>

        /// <inheritdoc/>
        public int Count => (int)GetSize();

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public PdfObject this[int index] { get => GetValue(index); set => SetValue(index, value); }

        /// <inheritdoc/>
        public int IndexOf(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                using (var current = GetValue(i)) {
                    if (item.Equals(current)) {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <inheritdoc/>
        public void Insert(int index, PdfObject item)
        {
            UInt64 indexConverted = MiscUtils.PlatformIntegerConversion(index);
            Insert(indexConverted, item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            UInt64 indexConverted = MiscUtils.PlatformIntegerConversion(index);
            Remove(indexConverted);
        }

        /// <inheritdoc/>
        public void Add(PdfObject item)
        {
            Append(item);
        }

        /// <inheritdoc/>
        public bool Contains(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                using (var current = GetValue(i)) {

                    if (item.Equals(current)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public void CopyTo(PdfObject[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; ++i) {
                array[arrayIndex++] = GetValue(i);
            }
        }

        /// <inheritdoc/>
        public bool Remove(PdfObject item)
        {
            for (int i = 0; i < Count; ++i) {
                using (var current = GetValue(i)) {

                    if (item.Equals(current)) {
                        UInt64 indexConverted = MiscUtils.PlatformIntegerConversion(i);
                        return Remove(indexConverted);
                    }
                }
            }

            return false;
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        public IEnumerator<PdfObject> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i) {
                yield return GetValue(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; ++i) {
                yield return GetValue(i);
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
