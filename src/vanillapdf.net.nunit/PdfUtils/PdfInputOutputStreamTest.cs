using NUnit.Framework;
using System;

namespace vanillapdf.net.nunit.Utils
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
            var checkString = readBuffer.GetDataString();

            Assert.AreEqual(TEST_DATA, checkString);
        }
    }
}
