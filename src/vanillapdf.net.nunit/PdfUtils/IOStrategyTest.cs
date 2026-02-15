using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class IOStrategyTest
    {
        [Test]
        public void PdfFile_OpenWithStrategy_FileStream()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");
            using var file = PdfFile.Open(path, IOStrategyType.FileStream);
            file.Initialize();

            ClassicAssert.IsNotNull(file);
            ClassicAssert.IsNotNull(file.Filename);
        }

        [Test]
        public void PdfFile_CreateWithStrategy_FileStream()
        {
            string path = Path.GetTempFileName();
            try {
                using var file = PdfFile.Create(path, IOStrategyType.FileStream);
                ClassicAssert.IsNotNull(file);
            } finally {
                File.Delete(path);
            }
        }

        [Test]
        public void PdfDocument_OpenWithStrategy_FileStream()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");
            using var document = PdfDocument.Open(path, IOStrategyType.FileStream);

            ClassicAssert.IsNotNull(document);

            using var catalog = document.GetCatalog();
            ClassicAssert.IsNotNull(catalog);
        }

        [Test]
        public void PdfDocument_CreateWithStrategy_FileStream()
        {
            string path = Path.GetTempFileName();
            try {
                using var document = PdfDocument.Create(path, IOStrategyType.FileStream);
                ClassicAssert.IsNotNull(document);
            } finally {
                File.Delete(path);
            }
        }
    }
}
