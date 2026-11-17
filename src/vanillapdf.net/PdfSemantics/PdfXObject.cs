using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Base class for external objects - self-contained content streams that can be
    /// painted from other content streams, such as form XObjects and image XObjects.
    /// </summary>
    public class PdfXObject : IDisposable
    {
        internal PdfXObjectSafeHandle XObjectHandle { get; }

        internal PdfXObject(PdfXObjectSafeHandle handle)
        {
            XObjectHandle = handle;
        }

        /// <summary>
        /// Underlying low-level object of the XObject.
        /// </summary>
        public PdfObject BaseObject
        {
            get => GetBaseObject();
        }

        /// <summary>
        /// Get derived type of current object.
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure.</returns>
        public PdfXObjectType GetXObjectType()
        {
            UInt32 result = NativeMethods.XObject_GetXObjectType(XObjectHandle, out PdfXObjectType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfXObjectType>.CheckedCast(data);
        }

        private PdfObject GetBaseObject()
        {
            UInt32 result = NativeMethods.XObject_GetBaseObject(XObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            XObjectHandle?.Dispose();
        }
    }
}
