using System;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit
{
    /// <summary>
    /// Keeps a stream, the file on top of it and the document together,
    /// so a test can modify a document and read it back entirely in memory.
    /// </summary>
    internal sealed class TestDocument : IDisposable
    {
        private const string StreamName = "testDocument";

        private readonly PdfInputOutputStream _stream;
        private readonly PdfFile _file;

        public PdfDocument Document { get; }

        private TestDocument(PdfInputOutputStream stream)
        {
            _stream = stream;
            _file = PdfFile.OpenStream(_stream, StreamName);
            _file.Initialize();
            Document = PdfDocument.OpenFile(_file);
        }

        /// <summary>
        /// Open an existing document from disk.
        /// </summary>
        public static TestDocument Open(string path)
        {
            return new TestDocument(PdfInputOutputStream.CreateFromFile(path));
        }

        /// <summary>
        /// Save the document into a new in-memory stream and open the result.
        /// </summary>
        public TestDocument SaveAndReopen()
        {
            var stream = PdfInputOutputStream.CreateFromMemory();
            using (var destination = PdfFile.CreateStream(stream, StreamName)) {
                Document.SaveFile(destination);
            }

            return new TestDocument(stream);
        }

        public void Dispose()
        {
            Document.Dispose();
            _file.Dispose();
            _stream.Dispose();
        }
    }
}
