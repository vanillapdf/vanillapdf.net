using System;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Base exception class for all managed and unmanaged derived exceptions
    /// </summary>
    public abstract class PdfBaseException : Exception
    {
        internal PdfBaseException(string message) : base(message)
        {
        }
    }
}
