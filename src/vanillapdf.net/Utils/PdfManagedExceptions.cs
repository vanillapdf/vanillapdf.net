using System;
using System.Collections.Generic;
using System.Text;

namespace vanillapdf.net.Utils
{
    public class PdfManagedException : PdfBaseException
    {
        public PdfManagedException(string message) : base(message)
        {
        }
    }
}
