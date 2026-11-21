namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Thrown when an object read from the PDF is not of the requested type and cannot be converted to it.
    /// </summary>
    /// <remarks>
    /// This indicates a malformed or unusual input file rather than a programming error,
    /// so it can be caught to skip the offending object.
    /// </remarks>
    public class PdfObjectTypeException : PdfManagedException
    {
        internal PdfObjectTypeException(string message) : base(message)
        {
        }
    }
}
