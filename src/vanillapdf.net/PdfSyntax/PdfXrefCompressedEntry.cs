using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Compressed entry means that the object is located within (7.5.7 Object streams) compressed object stream.
    /// This entry type can be only found in cross-reference streams.
    /// </summary>
    public class PdfXrefCompressedEntry : PdfXrefEntry
    {
        internal PdfXrefCompressedEntrySafeHandle Handle { get; }

        internal PdfXrefCompressedEntry(PdfXrefCompressedEntrySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Reference to the object represented by this entry
        /// </summary>
        /// <returns>Reference to the object represented by this entry on success, throws exception on failure</returns>
        public PdfObject Reference
        {
            get => GetReference();
            set => SetReference(value);
        }

        private PdfObject GetReference()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetReference(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        private void SetReference(PdfObject value)
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_SetReference(Handle, value.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// The index of this object within the object stream.
        /// </summary>
        /// <returns>Index within object stream on success, throws exception on failure</returns>
        public UInt64 GetIndex()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetIndex(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// The object number of the object stream in which this object is stored.
        /// The generation number of the object stream shall be implicitly 0.
        /// </summary>
        /// <returns>Object number of the object stream on success, throws exception on failure</returns>
        public UInt64 GetObjectStreamNumber()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetObjectStreamNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Convert entry to compressed stream entry
        /// </summary>
        /// <param name="entry">Handle to \ref PdfXrefEntry to be converted</param>
        /// <returns>A new instance of \ref PdfXrefCompressedEntry if the object can be converted, throws exception on failure</returns>
        public static PdfXrefCompressedEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefCompressedEntry(entry.BaseEntryHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
