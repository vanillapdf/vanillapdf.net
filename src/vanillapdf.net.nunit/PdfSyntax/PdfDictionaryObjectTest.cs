using NUnit.Framework;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfDictionaryObjectTest
    {
        [Test]
        public void TestIterator()
        {
            var DictionaryObject = PdfDictionaryObject.Create();

            Assert.AreEqual(DictionaryObject.Count, 0);
            Assert.AreEqual(DictionaryObject.Keys.Count, 0);
            Assert.AreEqual(DictionaryObject.Values.Count, 0);

            PdfNameObject key = PdfNameObject.Create();
            PdfNameObject value = PdfNameObject.Create();

            DictionaryObject.Insert(key, value);

            int count = 0;
            foreach(var pair in DictionaryObject) {
                Assert.NotNull(pair.Key);
                Assert.NotNull(pair.Value);

                count++;
            }

            Assert.AreEqual(count, 1);
            Assert.AreEqual(DictionaryObject.Count, 1);
            Assert.AreEqual(DictionaryObject.Keys.Count, 1);
            Assert.AreEqual(DictionaryObject.Values.Count, 1);

            DictionaryObject.Remove(key);

            Assert.AreEqual(DictionaryObject.Count, 0);
            Assert.AreEqual(DictionaryObject.Keys.Count, 0);
            Assert.AreEqual(DictionaryObject.Values.Count, 0);
        }
    }
}
