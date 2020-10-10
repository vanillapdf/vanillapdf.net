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
            const string TEST_DATA = "TEST_DATA";

            Stream = PdfInputOutputStream.CreateFromMemory();

            Stream.WriteString(TEST_DATA);

            var readBuffer = Stream.ReadBuffer(100);
            var checkString = readBuffer.GetDataString();

            Assert.AreEqual(TEST_DATA, checkString);
        }
    }
}
