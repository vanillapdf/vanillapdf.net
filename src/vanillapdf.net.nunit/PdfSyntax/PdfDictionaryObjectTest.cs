using NUnit.Framework;
using System;
using System.Collections.Generic;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfDictionaryObjectTest
    {
        [Test]
        public void TestIterator()
        {
            using var DictionaryObject = PdfDictionaryObject.Create();

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

        [Test]
        public void TestKeyValuePair()
        {
            using var dictionaryObject = PdfDictionaryObject.Create();

            using PdfNameObject key = PdfNameObject.Create();
            using PdfIntegerObject value = PdfIntegerObject.Create();

            key.Value.StringData = "TEST";
            value.IntegerValue = 1;

            dictionaryObject.Add(new KeyValuePair<PdfNameObject,PdfObject>(key, value));

            Assert.AreEqual(dictionaryObject.Count, 1);

            bool contains = dictionaryObject.Contains(new KeyValuePair<PdfNameObject, PdfObject>(key, value));
            Assert.IsTrue(contains);

            bool removed = dictionaryObject.Remove(new KeyValuePair<PdfNameObject, PdfObject>(key, value));
            Assert.IsTrue(removed);

            Assert.AreEqual(dictionaryObject.Count, 0);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfDictionaryObject.Create();
            }

            GC.Collect();
        }
    }
}
