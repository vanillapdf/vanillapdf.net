using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Base class for syntactic tokens
    /// </summary>
    public class PdfObject : PdfUnknown
    {
        internal PdfObjectSafeHandle ObjectHandle { get; }

        internal PdfObject(PdfObjectSafeHandle handle) : base(handle)
        {
            ObjectHandle = handle;
        }

        static PdfObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Offset of the object in the source document
        /// </summary>
        public Int64 Offset => GetOffset();

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfObjectType GetObjectType()
        {
            UInt32 result = NativeMethods.Object_GetObjectType(ObjectHandle, out PdfObjectType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfObjectType>.CheckedCast(data);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            using (var buffer = ToStringInternal()) {
                return buffer.StringData;
            }
        }

        private Int64 GetOffset()
        {
            UInt32 result = NativeMethods.Object_GetOffset(ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private PdfBuffer ToStringInternal()
        {
            UInt32 result = NativeMethods.Object_ToString(ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        /// <summary>
        /// Get a PDF data representation of the current object
        /// </summary>
        /// <returns>A new isntance of PdfBuffer with PDF data representation, throws exception on failure</returns>
        public PdfBuffer ToPdf()
        {
            UInt32 result = NativeMethods.Object_ToPdf(ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        /// <summary>
        /// Get object attribute list attached to this PDF object
        /// </summary>
        /// <returns>A new instance of PdfObjectAttributeList attached to the PDF object, throws exception on failure</returns>
        public PdfObjectAttributeList GetAttributeList()
        {
            UInt32 result = NativeMethods.Object_GetAttributeList(ObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObjectAttributeList(data);
        }

        internal virtual PdfObject ConvertTo<T>() where T : PdfObject
        {
            throw new PdfManagedException($"Could not convert object of type {GetType()}/{GetObjectType()} to {typeof(T)}");
        }

        internal static PdfObject GetAsDerivedObject(PdfObject pdfObject, bool removeIndirection = true)
        {
            // Loop instead of recursion to resolve indirection
            while (removeIndirection && pdfObject.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(pdfObject)) {
                    pdfObject = reference.ReferencedObject;
                }
            }

            var objectType = pdfObject.GetObjectType();

            switch (objectType) {
                case PdfObjectType.Array:
                    return PdfArrayObject.FromObject(pdfObject);
                case PdfObjectType.Boolean:
                    return PdfBooleanObject.FromObject(pdfObject);
                case PdfObjectType.Dictionary:
                    return PdfDictionaryObject.FromObject(pdfObject);
                case PdfObjectType.IndirectReference:
                    return PdfIndirectReferenceObject.FromObject(pdfObject);
                case PdfObjectType.Integer:
                    return PdfIntegerObject.FromObject(pdfObject);
                case PdfObjectType.Name:
                    return PdfNameObject.FromObject(pdfObject);
                case PdfObjectType.Null:
                    return PdfNullObject.FromObject(pdfObject);
                case PdfObjectType.Real:
                    return PdfRealObject.FromObject(pdfObject);
                case PdfObjectType.Stream:
                    return PdfStreamObject.FromObject(pdfObject);
                case PdfObjectType.String:
                    return PdfStringObject.FromObject(pdfObject);
                default:
                    throw new PdfManagedException($"Invalid object type: {objectType}");
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            ObjectHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate Object_GetObjectType = LibraryInstance.GetFunction<GetTypeDelgate>("Object_GetObjectType");
            public static GetOffsetDelgate Object_GetOffset = LibraryInstance.GetFunction<GetOffsetDelgate>("Object_GetOffset");
            public static ToStringDelgate Object_ToString = LibraryInstance.GetFunction<ToStringDelgate>("Object_ToString");
            public static ToPdfDelgate Object_ToPdf = LibraryInstance.GetFunction<ToPdfDelgate>("Object_ToPdf");
            public static GetAttributeListDelgate Object_GetAttributeList = LibraryInstance.GetFunction<GetAttributeListDelgate>("Object_GetAttributeList");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfObjectSafeHandle handle, out PdfObjectType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOffsetDelgate(PdfObjectSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ToStringDelgate(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ToPdfDelgate(PdfObjectSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetAttributeListDelgate(PdfObjectSafeHandle handle, out PdfObjectAttributeListSafeHandle data);
        }
    }
}
