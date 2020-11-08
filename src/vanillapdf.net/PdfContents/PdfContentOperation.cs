using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net
{
	public enum PdfContentOperationType
	{
		Undefined = 0,
		Generic,
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
	}

	public class PdfContentOperation : PdfUnknown
    {
        internal PdfContentOperation(PdfContentOperationSafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperation()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

		public PdfContentOperationType GetOperationType()
		{
			UInt32 result = NativeMethods.ContentOperation_GetOperationType(Handle, out Int32 data);
			if (result != PdfReturnValues.ERROR_SUCCESS) {
				throw PdfErrors.GetLastErrorException();
			}

			return EnumUtil<PdfContentOperationType>.CheckedCast(data);
		}

		private static class NativeMethods
        {
			public static GetInstructionTypeDelgate ContentOperation_GetOperationType = LibraryInstance.GetFunction<GetInstructionTypeDelgate>("ContentOperation_GetOperationType");

			[UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
			public delegate UInt32 GetInstructionTypeDelgate(PdfContentInstructionSafeHandle handle, out Int32 data);
		}
    }
}
