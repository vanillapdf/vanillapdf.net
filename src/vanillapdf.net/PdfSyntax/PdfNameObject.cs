using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A name object is an atomic symbol uniquely defined by a sequence of characters
    /// </summary>
    public class PdfNameObject : PdfObject, IEquatable<PdfNameObject>
    {
        internal PdfNameObjectSafeHandle Handle { get; }

        internal PdfNameObject(PdfNameObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Raw encoded value of the name.
        /// </summary>
        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Hash value calculated from the name string.
        /// </summary>
        public UInt64 Hash
        {
            get { return GetHash(); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfNameObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfNameObject on success, throws exception on failure</returns>
        public static PdfNameObject Create()
        {
            UInt32 result = NativeMethods.NameObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        /// <summary>
        /// Create a <see cref="PdfNameObject"/> from a raw encoded string value.
        /// </summary>
        /// <param name="data">Encoded string representation.</param>
        /// <returns>Newly created name object.</returns>
        public static PdfNameObject CreateFromEncodedString(string data)
        {
            UInt32 result = NativeMethods.NameObject_CreateFromEncodedString(data, out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(handle);
        }

        /// <summary>
        /// Create a <see cref="PdfNameObject"/> from a decoded string value.
        /// </summary>
        /// <param name="data">Decoded string representation.</param>
        /// <returns>Newly created name object.</returns>
        public static PdfNameObject CreateFromDecodedString(string data)
        {
            UInt32 result = NativeMethods.NameObject_CreateFromDecodedString(data, out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(handle);
        }

        private PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.NameObject_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private void SetValue(PdfBuffer data)
        {
            UInt32 result = NativeMethods.NameObject_SetValue(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private UInt64 GetHash()
        {
            UInt32 result = NativeMethods.NameObject_Hash(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Implicit conversion from <see cref="string"/> to <see cref="PdfNameObject"/>.
        /// </summary>
        public static implicit operator PdfNameObject(string data)
        {
            return CreateFromDecodedString(data);
        }

        /// <summary>
        /// Implicit conversion from <see cref="PdfNameObject"/> to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(PdfNameObject data)
        {
            return data.ToString();
        }

        /// <summary>
        /// Convert object to name object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNameObject if the object can be converted, throws exception on failure</returns>
        public static PdfNameObject FromObject(PdfObject data)
        {
            return new PdfNameObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to name object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNameObject if the object is a name, null otherwise</returns>
        public static PdfNameObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.Name) {
                return null;
            }

            return new PdfNameObject(data.ObjectHandle);
        }

        #region IEquatable<PdfNameObject>

        /// <inheritdoc/>
        public bool Equals(PdfNameObject other)
        {
            if (other == null) {
                return false;
            }

            UInt32 result = NativeMethods.NameObject_Equals(Handle, other.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #endregion

        #region object

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is PdfNameObject) {
                return Equals(obj as PdfNameObject);
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)GetHash();
        }

        #endregion

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
