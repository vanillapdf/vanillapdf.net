using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfIndirectReferenceObject : PdfObject
    {
        internal PdfIndirectReferenceObject(PdfIndirectReferenceObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfIndirectReferenceObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfObject ReferencedObject
        {
            get { return GetReferencedObject(); }
            set { SetReferencedObject(value); }
        }

        public static PdfIndirectReferenceObject Create()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIndirectReferenceObject(data);
        }

        public UInt64 GetReferencedObjectNumber()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedObjectNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public UInt16 GetReferencedGenerationNumber()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedGenerationNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public PdfObject GetReferencedObject()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public void SetReferencedObject(PdfObject value)
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_SetReferencedObject(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfIndirectReferenceObject FromObject(PdfObject data)
        {
            return new PdfIndirectReferenceObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate IndirectReferenceObject_Create = LibraryInstance.GetFunction<CreateDelgate>("IndirectReferenceObject_Create");
            public static GetReferencedObjectNumberDelgate IndirectReferenceObject_GetReferencedObjectNumber = LibraryInstance.GetFunction<GetReferencedObjectNumberDelgate>("IndirectReferenceObject_GetReferencedObjectNumber");
            public static GetReferencedGenerationNumberDelgate IndirectReferenceObject_GetReferencedGenerationNumber = LibraryInstance.GetFunction<GetReferencedGenerationNumberDelgate>("IndirectReferenceObject_GetReferencedGenerationNumber");

            public static GetReferencedObjectDelgate IndirectReferenceObject_GetReferencedObject = LibraryInstance.GetFunction<GetReferencedObjectDelgate>("IndirectReferenceObject_GetReferencedObject");
            public static SetReferencedObjectDelgate IndirectReferenceObject_SetReferencedObject = LibraryInstance.GetFunction<SetReferencedObjectDelgate>("IndirectReferenceObject_SetReferencedObject");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfIndirectReferenceObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReferencedObjectNumberDelgate(PdfIndirectReferenceObjectSafeHandle handle, out UInt64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReferencedGenerationNumberDelgate(PdfIndirectReferenceObjectSafeHandle handle, out UInt16 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReferencedObjectDelgate(PdfIndirectReferenceObjectSafeHandle handle, out PdfObjectSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetReferencedObjectDelgate(PdfIndirectReferenceObjectSafeHandle handle, PdfObjectSafeHandle value);
        }
    }
}
