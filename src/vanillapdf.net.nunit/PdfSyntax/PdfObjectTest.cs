using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfObjectTest
    {
        [Test]
        public void Hash_SameContent_IsEqual()
        {
            using var first = PdfIntegerObject.Create();
            using var second = PdfIntegerObject.Create();

            first.IntegerValue = 42;
            second.IntegerValue = 42;

            ClassicAssert.AreEqual(first.Hash, second.Hash);
        }

        [Test]
        public void Hash_DifferentContent_Differs()
        {
            using var first = PdfIntegerObject.Create();
            using var second = PdfIntegerObject.Create();

            first.IntegerValue = 42;
            second.IntegerValue = 43;

            ClassicAssert.AreNotEqual(first.Hash, second.Hash);
        }

        [Test]
        public void Hash_RepeatedCalls_AreStable()
        {
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 42;

            ClassicAssert.AreEqual(integerObject.Hash, integerObject.Hash);
        }
    }
}
