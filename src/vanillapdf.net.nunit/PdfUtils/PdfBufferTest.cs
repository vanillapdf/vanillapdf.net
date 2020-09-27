using NUnit.Framework;
using System.Linq;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfBufferTest
    {
        private PdfBuffer Buffer { get; set; }

        [SetUp]
        public void Setup()
        {
            Buffer = PdfBuffer.Create();
            Assert.NotNull(Buffer);
        }

        [Test]
        public void TestData()
        {
            Buffer = PdfBuffer.Create();
            var testData = new byte[1024];

            Buffer.SetData(testData);
            var checkData = Buffer.GetData();

            Assert.IsTrue(testData.SequenceEqual(checkData));
        }

        [Test]
        public void TestDataString()
        {
            const string TEST_DATA = "TEST_DATA";

            Buffer = PdfBuffer.Create();
            Buffer.SetDataString(TEST_DATA);
            var checkData = Buffer.GetDataString();

            Assert.AreEqual(TEST_DATA, checkData);
        }
    }
}
