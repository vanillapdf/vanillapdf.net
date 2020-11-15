using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public enum PdfContentOperatorType
    {
		Undefined = 0,
		Unknown,
		LineWidth,
		LineCap,
		LineJoin,
		MiterLimit,
		DashPattern,
		ColorRenderingIntent,
		Flatness,
		GraphicsState,
		SaveGraphicsState,
		RestoreGraphicsState,
		TransformationMatrix,
		BeginSubpath,
		Line,
		FullCurve,
		FinalCurve,
		InitialCurve,
		CloseSubpath,
		Rectangle,
		Stroke,
		CloseAndStroke,
		FillPathNonzero,
		FillPathCompatibility,
		FillPathEvenOdd,
		FillStrokeNonzero,
		FillStrokeEvenOdd,
		CloseFillStrokeNonzero,
		CloseFillStrokeEvenOdd,
		EndPath,
		ClipPathNonzero,
		ClipPathEvenOdd,
		BeginText,
		EndText,
		CharacterSpacing,
		WordSpacing,
		HorizontalScaling,
		Leading,
		TextFont,
		TextRenderingMode,
		TextRise,
		TextTranslate,
		TextTranslateLeading,
		TextMatrix,
		TextNextLine,
		TextShow,
		TextShowArray,
		TextNextLineShow,
		TextNextLineShowSpacing,
		SetCharWidth,
		SetCacheDevice,
		ColorSpaceStroke,
		ColorSpaceNonstroke,
		SetColorStroke,
		SetColorStrokeExtended,
		SetColorNonstroke,
		SetColorNonstrokeExtended,
		SetStrokingColorSpaceGray,
		SetNonstrokingColorSpaceGray,
		SetStrokingColorSpaceRGB,
		SetNonstrokingColorSpaceRGB,
		SetStrokingColorSpaceCMYK,
		SetNonstrokingColorSpaceCMYK,
		ShadingPaint,
		BeginInlineImageObject,
		BeginInlineImageData,
		EndInlineImageObject,
		InvokeXObject,
		DefineMarkedContentPoint,
		DefineMarkedContentPointWithPropertyList,
		BeginMarkedContentSequence,
		BeginMarkedContentSequenceWithPropertyList,
		EndMarkedContentSequence,
		BeginCompatibilitySection,
		EndCompatibilitySection
	};

    public class PdfContentOperator : PdfUnknown
    {
        internal PdfContentOperator(PdfContentOperatorSafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfContentOperatorType GetOperatorType()
        {
            UInt32 result = NativeMethods.ContentOperator_GetOperatorType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentOperatorType>.CheckedCast(data);
        }

		public PdfBuffer GetValue()
		{
			UInt32 result = NativeMethods.ContentOperator_GetValue(Handle, out var value);
			if (result != PdfReturnValues.ERROR_SUCCESS) {
				throw PdfErrors.GetLastErrorException();
			}

			return new PdfBuffer(value);
		}

		private static class NativeMethods
        {
            public static GetOperatorTypeDelgate ContentOperator_GetOperatorType = LibraryInstance.GetFunction<GetOperatorTypeDelgate>("ContentOperator_GetOperatorType");
			public static GetValueDelgate ContentOperator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentOperator_GetValue");

			[UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperatorTypeDelgate(PdfContentOperatorSafeHandle handle, out Int32 data);

			[UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
			public delegate UInt32 GetValueDelgate(PdfContentOperatorSafeHandle handle, out PdfBufferSafeHandle value);
		}
    }
}
