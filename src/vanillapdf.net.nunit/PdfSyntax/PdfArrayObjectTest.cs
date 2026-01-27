using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfArrayObjectTest
    {
        [Test]
        public void TestInterface()
        {
            using var ArrayObject = PdfArrayObject.Create();

            ClassicAssert.AreEqual(0, ArrayObject.Count);

            using PdfNameObject item = PdfNameObject.Create();

            ArrayObject.Add(item);

            int countForeach = 0;
            foreach (var current in ArrayObject) {
                ClassicAssert.NotNull(current);

                countForeach++;
            }

            int countFor = 0;
            for (ulong i = 0; i < ArrayObject.GetSize(); ++i) {
                var current = ArrayObject.GetValue(i);
                ClassicAssert.NotNull(current);

                countFor++;
            }

            ClassicAssert.AreEqual(1, countForeach);
            ClassicAssert.AreEqual(1, countFor);
            ClassicAssert.AreEqual(1, ArrayObject.Count);

            // TODO not working
            //ArrayObject.Remove(item);
            ArrayObject.Remove(0);

            ClassicAssert.AreEqual(0, ArrayObject.Count);
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
            using var ArrayObject = PdfArrayObject.Create();
            using var NameObject = PdfNameObject.Create();

            try {
                ArrayObject.Insert(0, NameObject);
            }
            catch (Exception ex) {
                ClassicAssert.IsTrue(ex is PdfGeneralException);
            }

            try {
                ArrayObject.Insert(-1, NameObject);
            }
            catch (Exception ex) {
                ClassicAssert.IsTrue(ex is PdfGeneralException);
            }

            try {
                ArrayObject.Insert(1000, NameObject);
            }
            catch (Exception ex) {
                ClassicAssert.IsTrue(ex is PdfGeneralException);
            }
        }

        [Test]
        public void TestRemoveBoundaries()
        {
            using var ArrayObject = PdfArrayObject.Create();

            bool removed_zero = ArrayObject.Remove(0);
            bool removed_neg = ArrayObject.Remove(OneTimeSetup.PLATFORM_MAXIMUM_VALUE);

            ClassicAssert.IsFalse(removed_zero);
            ClassicAssert.IsFalse(removed_neg);

        }

        [Test]
        public void GetValueAs_WithRealObject_ReturnsInteger()
        {
            // PDF spec allows integers and reals to be used interchangeably
            // e.g., MediaBox can contain [0 0 612.0 792.0] where 612.0 is a Real
            using var array = PdfArrayObject.Create();
            using var realObject = PdfRealObject.Create();
            realObject.Value = 612.0;
            array.Append(realObject);

            using var integerFromReal = array.GetValueAs<PdfIntegerObject>(0);

            ClassicAssert.IsNotNull(integerFromReal);
            ClassicAssert.AreEqual(612, integerFromReal.IntegerValue);
        }

        [Test]
        public void GetValueAs_WithIntegerObject_ReturnsReal()
        {
            using var array = PdfArrayObject.Create();
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 612;
            array.Append(integerObject);

            using var realFromInteger = array.GetValueAs<PdfRealObject>(0);

            ClassicAssert.IsNotNull(realFromInteger);
            ClassicAssert.AreEqual(612.0, realFromInteger.Value);
        }

        [Test]
        public void TryGetValueAs_WithRealObject_ReturnsInteger()
        {
            using var array = PdfArrayObject.Create();
            using var realObject = PdfRealObject.Create();
            realObject.Value = 792.5;
            array.Append(realObject);

            bool success = array.TryGetValueAs<PdfIntegerObject>(0, out var integerFromReal);

            ClassicAssert.IsTrue(success, "TryGetValueAs should succeed for Real->Integer conversion");
            ClassicAssert.IsNotNull(integerFromReal);
            ClassicAssert.AreEqual(792, integerFromReal.IntegerValue);
            integerFromReal.Dispose();
        }

        [Test]
        public void TryGetValueAs_WithIntegerObject_ReturnsReal()
        {
            using var array = PdfArrayObject.Create();
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 792;
            array.Append(integerObject);

            bool success = array.TryGetValueAs<PdfRealObject>(0, out var realFromInteger);

            ClassicAssert.IsTrue(success, "TryGetValueAs should succeed for Integer->Real conversion");
            ClassicAssert.IsNotNull(realFromInteger);
            ClassicAssert.AreEqual(792.0, realFromInteger.Value);
            realFromInteger.Dispose();
        }
    }
}
