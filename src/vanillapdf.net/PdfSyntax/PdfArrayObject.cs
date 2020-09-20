using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfArrayObject : PdfObject
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

        public PdfObject At(UInt64 index)
        {
            UInt32 result = NativeMethods.ArrayObject_At(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
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

        public void Remove(UInt64 index)
        {
            UInt32 result = NativeMethods.ArrayObject_Remove(Handle, new UIntPtr(index));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static CreateDelgate ArrayObject_Create = LibraryInstance.GetFunction<CreateDelgate>("ArrayObject_Create");
            public static GetSizeDelgate ArrayObject_GetSize = LibraryInstance.GetFunction<GetSizeDelgate>("ArrayObject_GetSize");
            public static AtDelgate ArrayObject_At = LibraryInstance.GetFunction<AtDelgate>("ArrayObject_At");
            public static AppendDelgate ArrayObject_Append = LibraryInstance.GetFunction<AppendDelgate>("ArrayObject_Append");
            public static InsertDelgate ArrayObject_Insert = LibraryInstance.GetFunction<InsertDelgate>("ArrayObject_Insert");
            public static RemoveDelgate ArrayObject_Remove = LibraryInstance.GetFunction<RemoveDelgate>("ArrayObject_Remove");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfArrayObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSizeDelgate(PdfArrayObjectSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AtDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AppendDelgate(PdfArrayObjectSafeHandle handle, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveDelgate(PdfArrayObjectSafeHandle handle, UIntPtr index);
        }
    }
}
