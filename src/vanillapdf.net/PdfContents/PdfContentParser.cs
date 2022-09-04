using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// An object used for reading PDF PostScript instructions
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

        /// <summary>
        /// Create a new instance of \ref PdfContentParser with default value
        /// </summary>
        /// <param name="sourceFile">Handle to the file the contents should be associated with</param>
        /// <param name="inputStream">The actual data to be parsed</param>
        /// <returns>New instance of \ref PdfContentParser on success, throws exception on failure</returns>
        public static PdfContentParser Create(PdfFile sourceFile, PdfInputStream inputStream)
        {
            UInt32 result = NativeMethods.ContentParser_Create(sourceFile.Handle, inputStream.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentParser(data);
        }

        /// <summary>
        /// Reads the stream data set in the constructor and returns the sequence of PDF PostScript instructions
        /// </summary>
        /// <returns>New instance of \ref PdfContentInstructionCollection on success, throws exception on failure</returns>
        public PdfContentInstructionCollection ReadInstructionCollection()
        {
            UInt32 result = NativeMethods.ContentParser_ReadInstructionCollection(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstructionCollection(data);
        }

        private protected override void DisposeCustomHandle()
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
