using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// The issues found by <see cref="PdfFileStructureValidator.Validate"/>.
    /// </summary>
    public class PdfFileStructureValidationResult : IDisposable
    {
        internal PdfFileStructureValidationResultSafeHandle Handle { get; }

        internal PdfFileStructureValidationResult(PdfFileStructureValidationResultSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// True when no issue has <see cref="PdfFileStructureIssueSeverityType.Error"/> severity.
        /// Warnings and informational issues do not make the file invalid.
        /// </summary>
        public bool IsValid => GetIsValid();

        /// <summary>
        /// Total number of issues found, of any severity.
        /// </summary>
        public UInt64 IssueCount => GetIssueCount();

        /// <summary>
        /// Number of issues with <see cref="PdfFileStructureIssueSeverityType.Error"/> severity.
        /// </summary>
        public UInt64 ErrorCount => GetErrorCount();

        /// <summary>
        /// Number of issues with <see cref="PdfFileStructureIssueSeverityType.Warning"/> severity.
        /// </summary>
        public UInt64 WarningCount => GetWarningCount();

        /// <summary>
        /// Get the issue at the given zero-based index.
        /// </summary>
        /// <param name="index">Zero-based index, less than <see cref="IssueCount"/>.</param>
        /// <returns>The <see cref="PdfFileStructureIssue"/> at <paramref name="index"/>.</returns>
        public PdfFileStructureIssue GetIssue(UInt64 index)
        {
            UInt32 result = NativeMethods.FileStructureValidationResult_GetIssueAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileStructureIssue(data);
        }

        private bool GetIsValid()
        {
            UInt32 result = NativeMethods.FileStructureValidationResult_IsValid(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private UInt64 GetIssueCount()
        {
            UInt32 result = NativeMethods.FileStructureValidationResult_GetIssueCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        private UInt64 GetErrorCount()
        {
            UInt32 result = NativeMethods.FileStructureValidationResult_GetErrorCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        private UInt64 GetWarningCount()
        {
            UInt32 result = NativeMethods.FileStructureValidationResult_GetWarningCount(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
