using NUnit.Framework;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfArrayObjectTest
    {
        [Test]
        public void TestInterface()
        {
            var ArrayObject = PdfArrayObject.Create();

            Assert.AreEqual(0, ArrayObject.Count);

            PdfNameObject item = PdfNameObject.Create();

            ArrayObject.Add(item);

            int count = 0;
            foreach(var current in ArrayObject) {
                Assert.NotNull(current);

                count++;
            }

            Assert.AreEqual(1, count);
            Assert.AreEqual(1, ArrayObject.Count);

            // TODO not working
            //ArrayObject.Remove(item);
            ArrayObject.Remove(0);

            Assert.AreEqual(0, ArrayObject.Count);
        }
    }
}
