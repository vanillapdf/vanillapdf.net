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
                    return PdfXYZDestination.FromDestination(destination);
                case PdfDestinationType.Fit:
                    return PdfFitDestination.FromDestination(destination);
                case PdfDestinationType.FitHorizontal:
                    return PdfFitHorizontalDestination.FromDestination(destination);
                case PdfDestinationType.FitVertical:
                    return PdfFitVerticalDestination.FromDestination(destination);
                case PdfDestinationType.FitRectangle:
                    return PdfFitRectangleDestination.FromDestination(destination);
                case PdfDestinationType.FitBoundingBox:
                    return PdfFitBoundingBoxDestination.FromDestination(destination);
                case PdfDestinationType.FitBoundingBoxHorizontal:
                    return PdfFitBoundingBoxHorizontalDestination.FromDestination(destination);
                case PdfDestinationType.FitBoundingBoxVertical:
                    return PdfFitBoundingBoxVerticalDestination.FromDestination(destination);
                default:
                    throw new PdfManagedException($"Cannot upgrade destination with unsupported type: {destinationType}");
            }
        }
    }
}
