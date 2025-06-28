namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Derived types of \ref PdfAnnotation
    /// </summary>
    public enum PdfAnnotationType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>Text annotation.</summary>
        Text,

        /// <summary>Link annotation.</summary>
        Link,

        /// <summary>Free text annotation.</summary>
        FreeText,

        /// <summary>Line annotation.</summary>
        Line,

        /// <summary>Square annotation.</summary>
        Square,

        /// <summary>Circle annotation.</summary>
        Circle,

        /// <summary>Polygon annotation.</summary>
        Polygon,

        /// <summary>Polyline annotation.</summary>
        PolyLine,

        /// <summary>Highlight annotation.</summary>
        Highlight,

        /// <summary>Underline annotation.</summary>
        Underline,

        /// <summary>Squiggly underline annotation.</summary>
        Squiggly,

        /// <summary>Strikeout annotation.</summary>
        StrikeOut,

        /// <summary>Rubber stamp annotation.</summary>
        RubberStamp,

        /// <summary>Caret annotation.</summary>
        Caret,

        /// <summary>Ink annotation.</summary>
        Ink,

        /// <summary>Popup annotation.</summary>
        Popup,

        /// <summary>File attachment annotation.</summary>
        FileAttachment,

        /// <summary>Sound annotation.</summary>
        Sound,

        /// <summary>Movie annotation.</summary>
        Movie,

        /// <summary>Widget annotation.</summary>
        Widget,

        /// <summary>Screen annotation.</summary>
        Screen,

        /// <summary>Printer mark annotation.</summary>
        PrinterMark,

        /// <summary>Trap network annotation.</summary>
        TrapNetwork,

        /// <summary>Watermark annotation.</summary>
        Watermark,

        /// <summary>3D annotation.</summary>
        TripleD,

        /// <summary>Redaction annotation.</summary>
        Redaction,
    };
}
