using NUnit.Framework;
using System;
using System.Linq;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfBufferTest
    {
        [Test]
        public void TestData()
        {
            var Buffer = PdfBuffer.Create();
            var testData = new byte[1024];

            Buffer.SetData(testData);
            var checkData = Buffer.GetData();

            Assert.IsTrue(testData.SequenceEqual(checkData));
        }

        [Test]
        public void TestDataString()
        {
            const string TEST_DATA = "TEST_DATA";

            var Buffer = PdfBuffer.Create();
            Buffer.SetDataString(TEST_DATA);
            var checkData = Buffer.GetDataString();

            Assert.AreEqual(TEST_DATA, checkData);
        }

        [Test]
        public void TestEquality()
        {
            const string TEST_DATA = "TEST_DATA";

            var buffer1 = PdfBuffer.Create();
            var buffer2 = PdfBuffer.Create();

            buffer1.SetDataString(TEST_DATA);
            buffer2.SetDataString(TEST_DATA);

            Assert.IsTrue(buffer1.Equals(buffer2));
            Assert.IsTrue(buffer2.Equals(buffer1));

            Assert.AreEqual(buffer1.GetHashCode(), buffer2.GetHashCode());
        }

        [Test]
        public void TestInequality()
        {
            const string TEST_DATA1 = "TEST_DATA1";
            const string TEST_DATA2 = "TEST_DATA2";

            var buffer1 = PdfBuffer.Create();
            var buffer2 = PdfBuffer.Create();

            buffer1.SetDataString(TEST_DATA1);
            buffer2.SetDataString(TEST_DATA2);

            Assert.IsFalse(buffer1.Equals(buffer2));
            Assert.IsFalse(buffer2.Equals(buffer1));

            Assert.AreNotEqual(buffer1.GetHashCode(), buffer2.GetHashCode());
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfBuffer.Create();
            }

            GC.Collect();
        }
    }
}
