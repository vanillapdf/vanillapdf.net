using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PdfContentParser : PdfUnknown
    {
        internal PdfContentParserSafeHandle Handle { get; }

        internal PdfContentParser(PdfContentParserSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentParser()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentParserSafeHandle).TypeHandle);
        }

        public static PdfContentParser Create(PdfFile sourceFile, PdfInputStream inputStream)
        {
            UInt32 result = NativeMethods.ContentParser_Create(sourceFile.Handle, inputStream.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentParser(data);
        }

        public PdfContentInstructionCollection ReadInstructionCollection()
        {
            UInt32 result = NativeMethods.ContentParser_ReadInstructionCollection(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstructionCollection(data);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate ContentParser_Create = LibraryInstance.GetFunction<CreateDelgate>("ContentParser_Create");
            public static ReadInstructionCollectionDelgate ContentParser_ReadInstructionCollection = LibraryInstance.GetFunction<ReadInstructionCollectionDelgate>("ContentParser_ReadInstructionCollection");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(PdfFileSafeHandle sourceFile, PdfInputStreamSafeHandle inputStream, out PdfContentParserSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ReadInstructionCollectionDelgate(PdfContentParserSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);
        }
    }
}
