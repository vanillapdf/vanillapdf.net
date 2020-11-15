namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Representing error state in the managed code
    /// </summary>
    public class PdfManagedException : PdfBaseException
    {
        public PdfManagedException(string message) : base(message)
        {
        }
    }
}
