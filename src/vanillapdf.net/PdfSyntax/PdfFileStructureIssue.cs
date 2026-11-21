using System;
using System.Text;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A single structural issue found by <see cref="PdfFileStructureValidator.Validate"/>.
    /// </summary>
    public class PdfFileStructureIssue : IDisposable
    {
        internal PdfFileStructureIssueSafeHandle Handle { get; }

        internal PdfFileStructureIssue(PdfFileStructureIssueSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// How serious the issue is.
        /// </summary>
        public PdfFileStructureIssueSeverityType Severity => GetSeverity();

        /// <summary>
        /// The kind of the issue.
        /// </summary>
        public PdfFileStructureIssueCodeType Code => GetCode();

        /// <summary>
        /// Human-readable description of the issue.
        /// </summary>
        public string Message => GetMessage();

        /// <summary>
        /// Object number of the object the issue concerns, or null for a file-level issue.
        /// </summary>
        public UInt64? ObjectNumber => GetObjectNumber();

        /// <summary>
        /// Generation number of the object the issue concerns, or null for a file-level issue.
        /// </summary>
        public UInt16? GenerationNumber => GetGenerationNumber();

        private PdfFileStructureIssueSeverityType GetSeverity()
        {
            UInt32 result = NativeMethods.FileStructureIssue_GetSeverity(Handle, out PdfFileStructureIssueSeverityType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfFileStructureIssueSeverityType>.CheckedCast(data);
        }

        private PdfFileStructureIssueCodeType GetCode()
        {
            UInt32 result = NativeMethods.FileStructureIssue_GetCode(Handle, out PdfFileStructureIssueCodeType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfFileStructureIssueCodeType>.CheckedCast(data);
        }

        private string GetMessage()
        {
            UInt32 result = NativeMethods.FileStructureIssue_GetMessage(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            // Native versions up to 2.4.0-alpha.1 include the null terminator in the buffer
            using (var buffer = new PdfBuffer(data)) {
                return Encoding.UTF8.GetString(buffer.Data).TrimEnd('\0');
            }
        }

        private UInt64? GetObjectNumber()
        {
            UInt32 result = NativeMethods.FileStructureIssue_GetObjectNumber(Handle, out UInt64 data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private UInt16? GetGenerationNumber()
        {
            UInt32 result = NativeMethods.FileStructureIssue_GetGenerationNumber(Handle, out UInt16 data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
