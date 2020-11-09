using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
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

            int countForeach = 0;
            foreach (var current in ArrayObject) {
                Assert.NotNull(current);

                countForeach++;
            }

            int countFor = 0;
            for (ulong i = 0; i < ArrayObject.GetSize(); ++i) {
                var current = ArrayObject.GetValue(i);
                Assert.NotNull(current);

                countFor++;
            }

            Assert.AreEqual(1, countForeach);
            Assert.AreEqual(1, countFor);
            Assert.AreEqual(1, ArrayObject.Count);

            // TODO not working
            //ArrayObject.Remove(item);
            ArrayObject.Remove(0);

            Assert.AreEqual(0, ArrayObject.Count);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfArrayObject.Create();
            }

            GC.Collect();
        }

        [Test]
        public void TestInsertBoundaries()
        {
            var ArrayObject = PdfArrayObject.Create();
            var NameObject = PdfNameObject.Create();

            try {
                ArrayObject.Insert(0, NameObject);
            }
            catch (Exception ex) {
                Assert.IsTrue(ex is PdfGeneralException);
            }

            try {
                ArrayObject.Insert(-1, NameObject);
            }
            catch (Exception ex) {
                Assert.IsTrue(ex is PdfGeneralException);
            }

            try {
                ArrayObject.Insert(1000, NameObject);
            }
            catch (Exception ex) {
                Assert.IsTrue(ex is PdfGeneralException);
            }
        }

        [Test]
        public void TestRemoveBoundaries()
        {
            var ArrayObject = PdfArrayObject.Create();

            bool removed_zero = ArrayObject.Remove(0);
            bool removed_neg = ArrayObject.Remove(OneTimeSetup.PLATFORM_MAXIMUM_VALUE);

            Assert.IsFalse(removed_zero);
            Assert.IsFalse(removed_neg);

        }
    }
}
