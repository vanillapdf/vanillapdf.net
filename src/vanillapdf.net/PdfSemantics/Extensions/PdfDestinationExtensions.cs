using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics.Extensions
{
    /// <summary>
    /// Extension methods for PdfDestination type checking and conversion.
    /// </summary>
    public static class PdfDestinationExtensions
    {
        /// <summary>
        /// Checks if the destination is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfDestination destination) where T : PdfDestination
        {
            using (var upgraded = destination.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the destination as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfDestination destination) where T : PdfDestination
        {
            var upgraded = destination.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived destination type.
        /// </summary>
        internal static PdfDestination Upgrade(this PdfDestination destination)
        {
            var destinationType = destination.DestinationType;
            switch (destinationType) {
                case PdfDestinationType.XYZ:
                    return new PdfXYZDestination((PdfXYZDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.Fit:
                    return new PdfFitDestination((PdfFitDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitHorizontal:
                    return new PdfFitHorizontalDestination((PdfFitHorizontalDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitVertical:
                    return new PdfFitVerticalDestination((PdfFitVerticalDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitRectangle:
                    return new PdfFitRectangleDestination((PdfFitRectangleDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitBoundingBox:
                    return new PdfFitBoundingBoxDestination((PdfFitBoundingBoxDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitBoundingBoxHorizontal:
                    return new PdfFitBoundingBoxHorizontalDestination((PdfFitBoundingBoxHorizontalDestinationSafeHandle)destination.Handle);
                case PdfDestinationType.FitBoundingBoxVertical:
                    return new PdfFitBoundingBoxVerticalDestination((PdfFitBoundingBoxVerticalDestinationSafeHandle)destination.Handle);
                default:
                    throw new PdfManagedException($"Cannot upgrade destination with unsupported type: {destinationType}");
            }
        }
    }
}
