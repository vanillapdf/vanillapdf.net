﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfInputStreamTest
    {
        [Test]
        public void TestData()
        {
            const string TEST_DATA = "TEST_DATA";

            using (var Buffer = PdfBuffer.Create()) {
                Buffer.StringData = TEST_DATA;

                using (var Stream = PdfInputStream.CreateFromBuffer(Buffer)) {

                    var checkBuffer = Stream.ToBuffer();

                    ClassicAssert.AreEqual(Buffer, checkBuffer);
                }
            }
        }
    }
}
