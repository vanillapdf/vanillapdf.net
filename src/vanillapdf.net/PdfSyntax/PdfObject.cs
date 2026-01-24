using System;
using vanillapdf.net.Interop;
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

        internal static PdfObject GetAsDerivedObject(PdfObject pdfObject)
        {
            return GetAsDerivedObject(pdfObject, true);
        }

        internal static PdfObject GetAsDerivedObject(PdfObject pdfObject, bool removeIndirection)
        {
            if (pdfObject.GetObjectType() == PdfObjectType.Array) {
                return PdfArrayObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Boolean) {
                return PdfBooleanObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Dictionary) {
                return PdfDictionaryObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.IndirectReference) {
                if (!removeIndirection) {
                    return PdfIndirectReferenceObject.FromObject(pdfObject);
                }

                using (var pdfReference = PdfIndirectReferenceObject.FromObject(pdfObject)) {
                    return GetAsDerivedObject(pdfReference.ReferencedObject);
                }
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Integer) {
                return PdfIntegerObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Name) {
                return PdfNameObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Null) {
                return PdfNullObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Real) {
                return PdfRealObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.Stream) {
                return PdfStreamObject.FromObject(pdfObject);
            }

            if (pdfObject.GetObjectType() == PdfObjectType.String) {
                return PdfStringObject.FromObject(pdfObject);
            }

            throw new PdfManagedException($"Invalid object type: {pdfObject.GetObjectType()}");
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            ObjectHandle?.Dispose();
        }
    }
}
