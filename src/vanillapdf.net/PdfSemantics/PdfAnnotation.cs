using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    public enum PdfAnnotationType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,
        Text,
        Link,
        FreeText,
        Line,
        Square,
        Circle,
        Polygon,
        PolyLine,
        Highlight,
        Underline,
        Squiggly,
        StrikeOut,
        RubberStamp,
        Caret,
        Ink,
        Popup,
        FileAttachment,
        Sound,
        Movie,
        Widget,
        Screen,
        PrinterMark,
        TrapNetwork,
        Watermark,
        TripleD,
        Redaction,
    };

    public class PdfAnnotation : PdfUnknown
    {
        internal PdfAnnotation(PdfAnnotationSafeHandle handle) : base(handle)
        {
        }

        static PdfAnnotation()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfAnnotationSafeHandle).TypeHandle);
        }

        public PdfAnnotationType GetAnnotationType()
        {
            UInt32 result = NativeMethods.Annotation_GetAnnotationType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfAnnotationType>.CheckedCast(data);
        }

        private static class NativeMethods
        {
            public static AnnotationGetTypeDelgate Annotation_GetAnnotationType = LibraryInstance.GetFunction<AnnotationGetTypeDelgate>("Annotation_GetAnnotationType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AnnotationGetTypeDelgate(PdfAnnotationSafeHandle handle, out Int32 data);
        }
    }
}
