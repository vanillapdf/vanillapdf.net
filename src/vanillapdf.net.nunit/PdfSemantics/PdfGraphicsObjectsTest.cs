using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSemantics
{
    /// <summary>
    /// Standalone value objects introduced or extended in native 2.4.0:
    /// real-number rectangles, matrices, transparency groups, appearance
    /// characteristics and signature flags.
    /// </summary>
    [TestFixture]
    public class PdfGraphicsObjectsTest
    {
        [Test]
        public void TestRectangleRealCoordinates()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftXReal = 10.25;
            rect.LowerLeftYReal = 20.5;
            rect.UpperRightXReal = 300.75;
            rect.UpperRightYReal = 400.125;

            ClassicAssert.AreEqual(10.25, rect.LowerLeftXReal);
            ClassicAssert.AreEqual(20.5, rect.LowerLeftYReal);
            ClassicAssert.AreEqual(300.75, rect.UpperRightXReal);
            ClassicAssert.AreEqual(400.125, rect.UpperRightYReal);
        }

        [Test]
        public void TestRectangleDeprecatedIntegerCoordinatesTruncate()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftXReal = 10.75;
            rect.UpperRightYReal = 400.5;

#pragma warning disable CS0618 // The deprecated integer accessors must keep working until they are removed
            ClassicAssert.AreEqual(10, rect.LowerLeftX);
            ClassicAssert.AreEqual(400, rect.UpperRightY);

            rect.UpperRightX = 50;
#pragma warning restore CS0618

            ClassicAssert.AreEqual(50.0, rect.UpperRightXReal);
        }

        [Test]
        public void TestMatrixIdentityAndCoefficients()
        {
            using var matrix = PdfMatrix.Create();
            ClassicAssert.AreEqual(1.0, matrix.A);
            ClassicAssert.AreEqual(0.0, matrix.B);
            ClassicAssert.AreEqual(0.0, matrix.C);
            ClassicAssert.AreEqual(1.0, matrix.D);
            ClassicAssert.AreEqual(0.0, matrix.E);
            ClassicAssert.AreEqual(0.0, matrix.F);

            matrix.A = 2.5;
            matrix.F = -7.25;
            ClassicAssert.AreEqual(2.5, matrix.A);
            ClassicAssert.AreEqual(-7.25, matrix.F);
        }

        [Test]
        public void TestTransparencyGroupFlags()
        {
            using var group = PdfTransparencyGroup.Create();
            ClassicAssert.IsNull(group.GetIsolated());
            ClassicAssert.IsNull(group.GetKnockout());

            group.SetIsolated(true);
            group.SetKnockout(false);
            ClassicAssert.AreEqual(true, group.GetIsolated());
            ClassicAssert.AreEqual(false, group.GetKnockout());

            using var dictionary = group.BaseObject;
            using var subtype = dictionary.Find("S");
            using var subtypeName = PdfNameObject.FromObject(subtype);
            ClassicAssert.AreEqual("Transparency", (string)subtypeName);
        }

        [Test]
        public void TestAppearanceCharacteristics()
        {
            using var characteristics = PdfAppearanceCharacteristics.Create();
            ClassicAssert.IsNull(characteristics.BorderColor);
            ClassicAssert.IsNull(characteristics.NormalCaption);

            ClassicAssert.IsNull(characteristics.GetRotation());
            characteristics.SetRotation(90);
            ClassicAssert.AreEqual(90, characteristics.GetRotation());

            using var border = PdfColor.CreateRGB(1.0, 0.0, 0.0);
            characteristics.BorderColor = border;
            using var retrievedBorder = characteristics.BorderColor;
            ClassicAssert.AreEqual(PdfColorSpaceType.DeviceRGB, retrievedBorder.ColorSpace);
            ClassicAssert.AreEqual(1.0, retrievedBorder.Red);

            using var caption = PdfLiteralStringObject.CreateFromDecodedString("Sign here");
            characteristics.NormalCaption = caption;
            using var retrievedCaption = characteristics.NormalCaption;
            ClassicAssert.AreEqual("Sign here", retrievedCaption.Value.StringData);
        }

        [Test]
        public void TestSignatureFlags()
        {
            using var flags = PdfSignatureFlags.Create();
            ClassicAssert.IsFalse(flags.SignaturesExist);
            ClassicAssert.IsFalse(flags.AppendOnly);

            flags.SignaturesExist = true;
            flags.AppendOnly = true;
            ClassicAssert.IsTrue(flags.SignaturesExist);
            ClassicAssert.IsTrue(flags.AppendOnly);
        }
    }
}
