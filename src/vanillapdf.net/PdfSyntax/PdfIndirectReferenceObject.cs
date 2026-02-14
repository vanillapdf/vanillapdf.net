using System;
using vanillapdf.net.Interop;
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
            return new PdfIndirectReferenceObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to indirect reference object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfIndirectReferenceObject if the object is an indirect reference, null otherwise</returns>
        public static PdfIndirectReferenceObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.IndirectReference) {
                return null;
            }

            return new PdfIndirectReferenceObject(data.ObjectHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
