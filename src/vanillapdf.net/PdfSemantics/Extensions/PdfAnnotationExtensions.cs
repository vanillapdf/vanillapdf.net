using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics.Extensions
{
    /// <summary>
    /// Extension methods for PdfAnnotation type checking and conversion.
    /// </summary>
    public static class PdfAnnotationExtensions
    {
        /// <summary>
        /// Checks if the annotation is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfAnnotation annotation) where T : PdfAnnotation
        {
            using (var upgraded = annotation.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the annotation as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfAnnotation annotation) where T : PdfAnnotation
        {
            var upgraded = annotation.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived annotation type.
        /// </summary>
        internal static PdfAnnotation Upgrade(this PdfAnnotation annotation)
        {
            var annotationType = annotation.GetAnnotationType();
            switch (annotationType) {
                default:
                    throw new PdfManagedException($"Cannot upgrade annotation with unsupported type: {annotationType}");
            }
        }
    }
}
