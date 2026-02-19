using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Fit destination that fits the entire page within the window.
    /// Display the page with contents magnified to fit entire page
    /// within the window both horizontally and vertically.
    /// </summary>
    public class PdfFitDestination : PdfDestination
    {
        internal new PdfFitDestinationSafeHandle Handle { get; }

        internal PdfFitDestination(PdfFitDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base destination type.
        /// </summary>
        public static PdfFitDestination FromDestination(PdfDestination data)
        {
            return new PdfFitDestination(data.Handle);
        }
    }
}
