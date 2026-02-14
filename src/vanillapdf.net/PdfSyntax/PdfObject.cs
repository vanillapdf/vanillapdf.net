using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Base class for syntactic tokens
    /// </summary>
    public class PdfObject : IDisposable
    {
        internal PdfObjectSafeHandle ObjectHandle { get; }

        internal PdfObject(PdfObjectSafeHandle handle)
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

        /// <summary>
        /// Resolves indirect references and returns the resolved object.
        /// The caller must dispose the returned object.
        /// </summary>
        public static PdfObject Resolve(PdfObject obj)
        {
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    obj = reference.ReferencedObject;
                }
            }
            return obj;
        }

        /// <summary>
        /// Creates a resolved PdfObject directly from a handle.
        /// Takes ownership of the handle and disposes intermediates internally.
        /// </summary>
        private protected static PdfObject ResolveRaw(PdfObjectSafeHandle handle)
        {
            var obj = new PdfObject(handle);
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    var resolved = reference.ReferencedObject;
                    obj.Dispose();
                    obj = resolved;
                }
            }
            return obj;
        }

        /// <inheritdoc/>

        public virtual void Dispose()
        {
            ObjectHandle?.Dispose();
        }
    }
}
