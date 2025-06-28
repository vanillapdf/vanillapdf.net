namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Derived types of \ref PdfContentOperator
    /// </summary>
    public enum PdfContentOperatorType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>Unknown or unsupported operator.</summary>
        Unknown,

        /// <summary>Set the current line width.</summary>
        LineWidth,

        /// <summary>Set the line cap style.</summary>
        LineCap,

        /// <summary>Set the line join style.</summary>
        LineJoin,

        /// <summary>Set the miter limit.</summary>
        MiterLimit,

        /// <summary>Define a dash pattern.</summary>
        DashPattern,

        /// <summary>Specify color rendering intent.</summary>
        ColorRenderingIntent,

        /// <summary>Set the flatness tolerance.</summary>
        Flatness,

        /// <summary>Load a graphics state object.</summary>
        GraphicsState,

        /// <summary>Save the graphics state.</summary>
        SaveGraphicsState,

        /// <summary>Restore the graphics state.</summary>
        RestoreGraphicsState,

        /// <summary>Modify the transformation matrix.</summary>
        TransformationMatrix,

        /// <summary>Begin a new subpath.</summary>
        BeginSubpath,

        /// <summary>Append a line segment.</summary>
        Line,

        /// <summary>Append a complete Bézier curve.</summary>
        FullCurve,

        /// <summary>Append the final control points of a Bézier curve.</summary>
        FinalCurve,

        /// <summary>Append the initial control points of a Bézier curve.</summary>
        InitialCurve,

        /// <summary>Close the current subpath.</summary>
        CloseSubpath,

        /// <summary>Append a rectangle.</summary>
        Rectangle,

        /// <summary>Stroke the current path.</summary>
        Stroke,

        /// <summary>Close and stroke the current path.</summary>
        CloseAndStroke,

        /// <summary>Fill path using the nonzero winding rule.</summary>
        FillPathNonzero,

        /// <summary>Fill path for compatibility.</summary>
        FillPathCompatibility,

        /// <summary>Fill path using the even-odd rule.</summary>
        FillPathEvenOdd,

        /// <summary>Fill and then stroke path using the nonzero winding rule.</summary>
        FillStrokeNonzero,

        /// <summary>Fill and then stroke path using the even-odd rule.</summary>
        FillStrokeEvenOdd,

        /// <summary>Close, fill, and stroke path using the nonzero winding rule.</summary>
        CloseFillStrokeNonzero,

        /// <summary>Close, fill, and stroke path using the even-odd rule.</summary>
        CloseFillStrokeEvenOdd,

        /// <summary>End the path without filling or stroking.</summary>
        EndPath,

        /// <summary>Modify clipping path using the nonzero winding rule.</summary>
        ClipPathNonzero,

        /// <summary>Modify clipping path using the even-odd rule.</summary>
        ClipPathEvenOdd,

        /// <summary>Begin a text object.</summary>
        BeginText,

        /// <summary>End the current text object.</summary>
        EndText,

        /// <summary>Set character spacing.</summary>
        CharacterSpacing,

        /// <summary>Set word spacing.</summary>
        WordSpacing,

        /// <summary>Set horizontal scaling.</summary>
        HorizontalScaling,

        /// <summary>Set text leading.</summary>
        Leading,

        /// <summary>Select the text font and size.</summary>
        TextFont,

        /// <summary>Set text rendering mode.</summary>
        TextRenderingMode,

        /// <summary>Set text rise.</summary>
        TextRise,

        /// <summary>Move text position.</summary>
        TextTranslate,

        /// <summary>Move text position and set leading.</summary>
        TextTranslateLeading,

        /// <summary>Set text matrix.</summary>
        TextMatrix,

        /// <summary>Move to start of next text line.</summary>
        TextNextLine,

        /// <summary>Show a text string.</summary>
        TextShow,

        /// <summary>Show text strings from an array.</summary>
        TextShowArray,

        /// <summary>Move to next line and show text.</summary>
        TextNextLineShow,

        /// <summary>Set word and character spacing, move to next line and show text.</summary>
        TextNextLineShowSpacing,

        /// <summary>Set glyph width in type 3 font.</summary>
        SetCharWidth,

        /// <summary>Establish text in the glyph cache.</summary>
        SetCacheDevice,

        /// <summary>Set stroking color space by name.</summary>
        SetStrokingColorSpaceName,

        /// <summary>Set nonstroking color space by name.</summary>
        SetNonstrokingColorSpaceName,

        /// <summary>Set stroking color space to a device space.</summary>
        SetStrokingColorSpaceDevice,

        /// <summary>Set stroking color space to an extended device space.</summary>
        SetStrokingColorSpaceDeviceExtended,

        /// <summary>Set nonstroking color space to a device space.</summary>
        SetNonstrokingColorSpaceDevice,

        /// <summary>Set nonstroking color space to an extended device space.</summary>
        SetNonstrokingColorSpaceDeviceExtended,

        /// <summary>Set stroking gray level.</summary>
        SetStrokingColorSpaceGray,

        /// <summary>Set nonstroking gray level.</summary>
        SetNonstrokingColorSpaceGray,

        /// <summary>Set stroking RGB color.</summary>
        SetStrokingColorSpaceRGB,

        /// <summary>Set nonstroking RGB color.</summary>
        SetNonstrokingColorSpaceRGB,

        /// <summary>Set stroking CMYK color.</summary>
        SetStrokingColorSpaceCMYK,

        /// <summary>Set nonstroking CMYK color.</summary>
        SetNonstrokingColorSpaceCMYK,

        /// <summary>Paint with a shading pattern.</summary>
        ShadingPaint,

        /// <summary>Begin an inline image object.</summary>
        BeginInlineImageObject,

        /// <summary>Begin inline image data.</summary>
        BeginInlineImageData,

        /// <summary>End the inline image object.</summary>
        EndInlineImageObject,

        /// <summary>Invoke an external object.</summary>
        InvokeXObject,

        /// <summary>Define a marked-content point.</summary>
        DefineMarkedContentPoint,

        /// <summary>Define a marked-content point with property list.</summary>
        DefineMarkedContentPointWithPropertyList,

        /// <summary>Begin a marked-content sequence.</summary>
        BeginMarkedContentSequence,

        /// <summary>Begin a marked-content sequence with property list.</summary>
        BeginMarkedContentSequenceWithPropertyList,

        /// <summary>End the marked-content sequence.</summary>
        EndMarkedContentSequence,

        /// <summary>Begin a compatibility section.</summary>
        BeginCompatibilitySection,

        /// <summary>End a compatibility section.</summary>
        EndCompatibilitySection
    };
}
