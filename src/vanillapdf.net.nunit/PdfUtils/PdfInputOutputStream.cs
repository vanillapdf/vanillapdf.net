using NUnit.Framework;
using System.Linq;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfInputOutputStreamTest
    {
        private PdfInputOutputStream Stream { get; set; }

        [SetUp]
        public void Setup()
        {
            Stream = PdfInputOutputStream.CreateFromMemory();
            Assert.NotNull(Stream);
        }

        [Test]
        public void TestData()
        {
            Stream = PdfInputOutputStream.CreateFromMemory();

            byte[] data = Stream.Read(100);

            Assert.NotNull(data);
        }
    }
}
