namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Representing error state in the managed code
    /// </summary>
    public class PdfManagedException : PdfBaseException
    {
        internal PdfManagedException(string message) : base(message)
        {
        }
    }
}
