using NUnit.Framework;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfFileWriterTest
    {
        [Test]
        public void TestCustom()
        {
            var sourceStream = PdfInputOutputStream.CreateFromFile("Resources\\minimalist.pdf");
            var destinationStream = PdfInputOutputStream.CreateFromMemory();

            var sourceFile = PdfFile.OpenStream(sourceStream, "sourceStream");
            sourceFile.Initialize();

            var destinationFile = PdfFile.CreateStream(destinationStream, "destinationStream");

            PdfFileWriter writer = PdfFileWriter.Create();
            writer.Write(sourceFile, destinationFile);
        }
    }
}
