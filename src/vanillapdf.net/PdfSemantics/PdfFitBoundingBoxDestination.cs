using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitB destination that fits bounding box within window.
    /// Display the page with contents magnified to fit bounding box
    /// entirely within window both horizontally and vertically.
    /// </summary>
    public class PdfFitBoundingBoxDestination : PdfDestination
    {
        internal new PdfFitBoundingBoxDestinationSafeHandle Handle { get; }

        internal PdfFitBoundingBoxDestination(PdfFitBoundingBoxDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }
    }
}
