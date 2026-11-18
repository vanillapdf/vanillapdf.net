using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Validates the low-level structure of a PDF file: header, trailer and cross-reference tables.
    /// </summary>
    /// <remarks>
    /// The validator inspects the structures produced by <see cref="PdfFile.Initialize"/>, so the file
    /// has to be initialized first. Initialization recovers most malformed files by scanning for objects
    /// when the cross-reference parsing fails. A file that cannot be initialized at all cannot be validated,
    /// in that case the initialization error itself is the diagnostic.
    /// </remarks>
    public static class PdfFileStructureValidator
    {
        /// <summary>
        /// Validate the structure of an initialized file.
        /// </summary>
        /// <param name="file">The initialized file to validate.</param>
        /// <returns>A new <see cref="PdfFileStructureValidationResult"/> listing every issue found.</returns>
        public static PdfFileStructureValidationResult Validate(PdfFile file)
        {
            UInt32 result = NativeMethods.FileStructureValidator_Validate(file.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFileStructureValidationResult(data);
        }
    }
}
