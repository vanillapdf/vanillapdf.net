using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents reference to another object
    /// </summary>
    public class PdfIndirectReferenceObject : PdfObject
    {
        internal PdfIndirectReferenceObjectSafeHandle Handle { get; }

        internal PdfIndirectReferenceObject(PdfIndirectReferenceObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfIndirectReferenceObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIndirectReferenceObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Object referenced by this indirect reference.
        /// </summary>
        public PdfObject ReferencedObject
        {
            get { return GetReferencedObject(); }
            set { SetReferencedObject(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfIndirectReferenceObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfIndirectReferenceObject on success, throws exception on failure</returns>
        public static PdfIndirectReferenceObject Create()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIndirectReferenceObject(data);
        }

        /// <summary>
        /// Get the object number referenced by this indirect reference.
        /// </summary>
        /// <returns>The referenced object number.</returns>
        public UInt64 GetReferencedObjectNumber()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedObjectNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get the generation number of the referenced object.
        /// </summary>
        /// <returns>The referenced generation number.</returns>
        public UInt16 GetReferencedGenerationNumber()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedGenerationNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        internal override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfIndirectReferenceObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        private PdfObject GetReferencedObject()
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_GetReferencedObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        private void SetReferencedObject(PdfObject value)
        {
            UInt32 result = NativeMethods.IndirectReferenceObject_SetReferencedObject(Handle, value.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert object to indirect reference object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfIndirectReferenceObject if the object can be converted, throws exception on failure</returns>
        public static PdfIndirectReferenceObject FromObject(PdfObject data)
        {
            // This optimization does have severe side-effects and it's not worth it
            //if (data is PdfIndirectReferenceObject pdfIndirectReferenceObject) {
            //    return pdfIndirectReferenceObject;
            //}

            return new PdfIndirectReferenceObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
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
