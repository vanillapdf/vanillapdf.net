using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfInputOutputStreamTest
    {
        [Test]
        public void TestData()
        {
            const string TEST_DATA = "TEST_DATA";

            var Stream = PdfInputOutputStream.CreateFromMemory();

            Stream.WriteString(TEST_DATA);

            var readBuffer = Stream.ReadBuffer(100);

            ClassicAssert.AreEqual(TEST_DATA, readBuffer.StringData);
        }
    }
}
