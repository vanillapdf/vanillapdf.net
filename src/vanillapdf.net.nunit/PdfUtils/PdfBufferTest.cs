using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            using var Buffer = PdfBuffer.Create();
            var testData = new byte[1024];

            Buffer.Data = testData;

            ClassicAssert.IsTrue(testData.SequenceEqual(Buffer.Data));
        }

        [Test]
        public void TestDataEmpty()
        {
            using var Buffer = PdfBuffer.Create();
            var testData = new byte[0];

            Buffer.Data = testData;

            ClassicAssert.IsTrue(testData.SequenceEqual(Buffer.Data));
        }

        [Test]
        public void TestDataString()
        {
            const string TEST_DATA = "TEST_DATA";

            using var Buffer = PdfBuffer.Create();
            Buffer.StringData = TEST_DATA;

            ClassicAssert.AreEqual(TEST_DATA, Buffer.StringData);
        }

        [Test]
        public void TestEquality()
        {
            const string TEST_DATA = "TEST_DATA";

            using var buffer1 = PdfBuffer.Create();
            using var buffer2 = PdfBuffer.Create();

            buffer1.StringData = TEST_DATA;
            buffer2.StringData = TEST_DATA;

            ClassicAssert.IsTrue(buffer1.Equals(buffer2));
            ClassicAssert.IsTrue(buffer2.Equals(buffer1));

            ClassicAssert.AreEqual(buffer1.GetHashCode(), buffer2.GetHashCode());
        }

        [Test]
        public void TestInequality()
        {
            const string TEST_DATA1 = "TEST_DATA1";
            const string TEST_DATA2 = "TEST_DATA2";

            using var buffer1 = PdfBuffer.Create();
            using var buffer2 = PdfBuffer.Create();

            buffer1.StringData = TEST_DATA1;
            buffer2.StringData = TEST_DATA2;

            ClassicAssert.IsFalse(buffer1.Equals(buffer2));
            ClassicAssert.IsFalse(buffer2.Equals(buffer1));

            ClassicAssert.AreNotEqual(buffer1.GetHashCode(), buffer2.GetHashCode());
        }

        [Test]
        public void TestOffsetAccess()
        {
            using var buffer = PdfBuffer.Create();
            buffer.Data = new byte[2];
            buffer[0] = 0x01;
            buffer[1] = 0x02;

            ClassicAssert.AreEqual(0x01, buffer[0]);
            ClassicAssert.AreEqual(0x02, buffer[1]);
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
