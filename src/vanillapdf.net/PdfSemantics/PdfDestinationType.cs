namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Available destination types for PDF navigation.
    /// </summary>
    public enum PdfDestinationType
    {
        /// <summary>
        /// Undefined destination type.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Display the page with coordinates (left, top) at upper-left corner
        /// and contents magnified by zoom factor.
        /// </summary>
        XYZ,

        /// <summary>
        /// Display the page with contents magnified to fit entire page
        /// within the window both horizontally and vertically.
        /// </summary>
        Fit,

        /// <summary>
        /// Display the page with vertical coordinate top at top edge
        /// and contents magnified to fit entire width within window.
        /// </summary>
        FitHorizontal,

        /// <summary>
        /// Display the page with horizontal coordinate left at left edge
        /// and contents magnified to fit entire height within window.
        /// </summary>
        FitVertical,

        /// <summary>
        /// Display the page with contents magnified to fit the specified
        /// rectangle (left, bottom, right, top) within window.
        /// </summary>
        FitRectangle,

        /// <summary>
        /// Display the page with contents magnified to fit bounding box
        /// entirely within window both horizontally and vertically.
        /// </summary>
        FitBoundingBox,

        /// <summary>
        /// Display the page with vertical coordinate top at top edge
        /// and contents magnified to fit bounding box width within window.
        /// </summary>
        FitBoundingBoxHorizontal,

        /// <summary>
        /// Display the page with horizontal coordinate left at left edge
        /// and contents magnified to fit bounding box height within window.
        /// </summary>
        FitBoundingBoxVertical,
    }
}
