using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Used entry means that an indirect object is allocated within the PDF file and this entry points to it's offset.
    /// </summary>
    public class PdfXrefUsedEntry : PdfXrefEntry
    {
        internal PdfXrefUsedEntrySafeHandle Handle { get; }

        internal PdfXrefUsedEntry(PdfXrefUsedEntrySafeHandle handle) : base(handle)
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

        /// <summary>
        /// Number of bytes from the beginning of the file to the beginning of the referenced object.
        /// </summary>
        /// <returns>Offset of the object in the source document on success, throws exception on failure</returns>
        public Int64 GetOffset()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetOffset(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return (Int64)data;
        }

        private PdfObject GetReference()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetReference(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        private void SetReference(PdfObject value)
        {
            UInt32 result = NativeMethods.XrefUsedEntry_SetReference(Handle, value.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert object to compressed stream entry
        /// </summary>
        /// <param name="entry">Handle to \ref PdfXrefEntry to be converted</param>
        /// <returns>A new instance of \ref PdfXrefUsedEntry if the object can be converted, throws exception on failure</returns>
        public static PdfXrefUsedEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefUsedEntry(entry.BaseEntryHandle);
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion
    }
}
