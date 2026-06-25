using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfLibraryInfoTest
    {
        [Test]
        public void TestVersionIsPopulated()
        {
            // The native library currently reports a 2.x version
            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.VersionMajor, 2);
            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.VersionMinor, 0);
            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.VersionPatch, 0);
            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.VersionBuild, 0);
        }

        [Test]
        public void TestAuthorIsNotEmpty()
        {
            ClassicAssert.IsFalse(string.IsNullOrEmpty(PdfLibraryInfo.Author));
        }

        [Test]
        public void TestBuildDateIsPlausible()
        {
            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.BuildDay, 1);
            ClassicAssert.LessOrEqual(PdfLibraryInfo.BuildDay, 31);

            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.BuildMonth, 1);
            ClassicAssert.LessOrEqual(PdfLibraryInfo.BuildMonth, 12);

            ClassicAssert.GreaterOrEqual(PdfLibraryInfo.BuildYear, 2020);
        }
    }
}
