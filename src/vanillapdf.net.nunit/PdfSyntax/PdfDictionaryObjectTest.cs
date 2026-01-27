using NUnit.Framework;
using NUnit.Framework.Legacy;
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

            ClassicAssert.AreEqual(DictionaryObject.Count, 0);
            ClassicAssert.AreEqual(DictionaryObject.Keys.Count, 0);
            ClassicAssert.AreEqual(DictionaryObject.Values.Count, 0);

            PdfNameObject key = PdfNameObject.Create();
            PdfNameObject value = PdfNameObject.Create();

            DictionaryObject.Insert(key, value);

            int count = 0;
            foreach(var pair in DictionaryObject) {
                ClassicAssert.NotNull(pair.Key);
                ClassicAssert.NotNull(pair.Value);

                count++;
            }

            ClassicAssert.AreEqual(count, 1);
            ClassicAssert.AreEqual(DictionaryObject.Count, 1);
            ClassicAssert.AreEqual(DictionaryObject.Keys.Count, 1);
            ClassicAssert.AreEqual(DictionaryObject.Values.Count, 1);

            DictionaryObject.Remove(key);

            ClassicAssert.AreEqual(DictionaryObject.Count, 0);
            ClassicAssert.AreEqual(DictionaryObject.Keys.Count, 0);
            ClassicAssert.AreEqual(DictionaryObject.Values.Count, 0);
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

            ClassicAssert.AreEqual(dictionaryObject.Count, 1);

            bool contains = dictionaryObject.Contains(new KeyValuePair<PdfNameObject, PdfObject>(key, value));
            ClassicAssert.IsTrue(contains);

            bool removed = dictionaryObject.Remove(new KeyValuePair<PdfNameObject, PdfObject>(key, value));
            ClassicAssert.IsTrue(removed);

            ClassicAssert.AreEqual(dictionaryObject.Count, 0);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfDictionaryObject.Create();
            }

            GC.Collect();
        }

        [Test]
        public void FindAs_WithRealObject_ReturnsInteger()
        {
            // PDF spec allows integers and reals to be used interchangeably
            using var dict = PdfDictionaryObject.Create();
            using var key = PdfNameObject.Create();
            key.Value.StringData = "Width";
            using var realObject = PdfRealObject.Create();
            realObject.Value = 612.0;
            dict.Insert(key, realObject);

            using var integerFromReal = dict.FindAs<PdfIntegerObject>(key);

            ClassicAssert.IsNotNull(integerFromReal);
            ClassicAssert.AreEqual(612, integerFromReal.IntegerValue);
        }

        [Test]
        public void FindAs_WithIntegerObject_ReturnsReal()
        {
            using var dict = PdfDictionaryObject.Create();
            using var key = PdfNameObject.Create();
            key.Value.StringData = "Height";
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 792;
            dict.Insert(key, integerObject);

            using var realFromInteger = dict.FindAs<PdfRealObject>(key);

            ClassicAssert.IsNotNull(realFromInteger);
            ClassicAssert.AreEqual(792.0, realFromInteger.Value);
        }

        [Test]
        public void TryFindAs_WithRealObject_ReturnsInteger()
        {
            using var dict = PdfDictionaryObject.Create();
            using var key = PdfNameObject.Create();
            key.Value.StringData = "Width";
            using var realObject = PdfRealObject.Create();
            realObject.Value = 612.5;
            dict.Insert(key, realObject);

            bool success = dict.TryFindAs<PdfIntegerObject>(key, out var integerFromReal);

            ClassicAssert.IsTrue(success, "TryFindAs should succeed for Real->Integer conversion");
            ClassicAssert.IsNotNull(integerFromReal);
            ClassicAssert.AreEqual(612, integerFromReal.IntegerValue);
            integerFromReal.Dispose();
        }

        [Test]
        public void TryFindAs_WithIntegerObject_ReturnsReal()
        {
            using var dict = PdfDictionaryObject.Create();
            using var key = PdfNameObject.Create();
            key.Value.StringData = "Height";
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 792;
            dict.Insert(key, integerObject);

            bool success = dict.TryFindAs<PdfRealObject>(key, out var realFromInteger);

            ClassicAssert.IsTrue(success, "TryFindAs should succeed for Integer->Real conversion");
            ClassicAssert.IsNotNull(realFromInteger);
            ClassicAssert.AreEqual(792.0, realFromInteger.Value);
            realFromInteger.Dispose();
        }
    }
}
