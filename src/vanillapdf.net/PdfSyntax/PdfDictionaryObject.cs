using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfDictionaryObject : PdfObject
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

        public void Insert(PdfNameObject key, PdfObject data)
        {
            UInt32 result = NativeMethods.DictionaryObject_Insert(Handle, key.Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Remove(PdfNameObject key)
        {
            UInt32 result = NativeMethods.DictionaryObject_Remove(Handle, key.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static CreateDelgate DictionaryObject_Create = LibraryInstance.GetFunction<CreateDelgate>("DictionaryObject_Create");
            public static FindDelgate DictionaryObject_Find = LibraryInstance.GetFunction<FindDelgate>("DictionaryObject_Find");
            public static ContainsDelgate DictionaryObject_Contains = LibraryInstance.GetFunction<ContainsDelgate>("DictionaryObject_Contains");
            //public static AppendDelgate DictionaryObject_Iterator = LibraryInstance.GetFunction<AppendDelgate>("DictionaryObject_Iterator");
            public static RemoveDelgate DictionaryObject_Remove = LibraryInstance.GetFunction<RemoveDelgate>("DictionaryObject_Remove");
            public static InsertDelgate DictionaryObject_Insert = LibraryInstance.GetFunction<InsertDelgate>("DictionaryObject_Insert");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfDictionaryObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FindDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ContainsDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertDelgate(PdfDictionaryObjectSafeHandle handle, PdfNameObjectSafeHandle key, PdfObjectSafeHandle data);
        }
    }
}
