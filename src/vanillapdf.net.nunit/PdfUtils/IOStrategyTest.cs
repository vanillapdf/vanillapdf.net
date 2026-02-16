using System;
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
        #region PdfFile Open

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
        public void PdfFile_OpenWithStrategy_FileStream_VersionMatches()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");

            using var fileDefault = PdfFile.Open(path);
            fileDefault.Initialize();

            using var fileStrategy = PdfFile.Open(path, IOStrategyType.FileStream);
            fileStrategy.Initialize();

            ClassicAssert.AreEqual(fileDefault.Version, fileStrategy.Version);
        }

        [Test]
        public void PdfFile_OpenWithStrategy_FileStream_AllTestDocuments()
        {
            foreach (var documentPath in OneTimeSetup.TEST_DOCUMENTS) {
                using var file = PdfFile.Open(documentPath, IOStrategyType.FileStream);
                file.Initialize();

                ClassicAssert.IsNotNull(file.Filename);
                ClassicAssert.IsFalse(file.Encrypted);
            }
        }

        [Test]
        public void PdfFile_OpenWithStrategy_Undefined_ThrowsException()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");
            Assert.Throws<PdfParameterValueException>(() =>
                PdfFile.Open(path, IOStrategyType.Undefined));
        }

        [Test]
        public void PdfFile_OpenWithStrategy_Memory()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");
            using var file = PdfFile.Open(path, IOStrategyType.Memory);
            file.Initialize();

            ClassicAssert.IsNotNull(file);
            ClassicAssert.IsNotNull(file.Filename);
        }

        [Test]
        public void PdfFile_OpenWithStrategy_Memory_AllTestDocuments()
        {
            foreach (var documentPath in OneTimeSetup.TEST_DOCUMENTS) {
                using var file = PdfFile.Open(documentPath, IOStrategyType.Memory);
                file.Initialize();

                ClassicAssert.IsNotNull(file.Filename);
                ClassicAssert.IsFalse(file.Encrypted);
            }
        }

        #endregion

        #region PdfFile Create

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
        public void PdfFile_CreateWithStrategy_Memory()
        {
            string path = Path.GetTempFileName();
            try {
                using var file = PdfFile.Create(path, IOStrategyType.Memory);
                ClassicAssert.IsNotNull(file);
            } finally {
                File.Delete(path);
            }
        }

        [Test]
        public void PdfFile_CreateWithStrategy_FileStream_WriteAndRead()
        {
            string path = Path.GetTempFileName();
            try {
                using (var sourceFile = PdfFile.Open(
                    Path.Combine("Resources", "minimalist.pdf"))) {
                    sourceFile.Initialize();

                    using var destFile = PdfFile.Create(path, IOStrategyType.FileStream);
                    using var writer = PdfFileWriter.Create();
                    writer.Write(sourceFile, destFile);
                }

                using var readBack = PdfFile.Open(path, IOStrategyType.FileStream);
                readBack.Initialize();
                ClassicAssert.IsNotNull(readBack.Version);
            } finally {
                File.Delete(path);
            }
        }

        #endregion

        #region PdfDocument via PdfFile with Strategy

        [Test]
        public void PdfDocument_ViaFileWithStrategy_FileStream_PageAccess()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");
            using var file = PdfFile.Open(path, IOStrategyType.FileStream);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();

            var pageCount = pages.GetPageCount();
            ClassicAssert.Greater(pageCount, (ulong)0);

            using var page = pages.GetPage(1);
            ClassicAssert.IsNotNull(page);
        }

        [Test]
        public void PdfDocument_ViaFileWithStrategy_Memory_PageAccess()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");
            using var file = PdfFile.Open(path, IOStrategyType.Memory);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();

            var pageCount = pages.GetPageCount();
            ClassicAssert.Greater(pageCount, (ulong)0);

            using var page = pages.GetPage(1);
            ClassicAssert.IsNotNull(page);
        }

        [Test]
        public void PdfDocument_ViaFileWithStrategy_CatalogAccess()
        {
            foreach (var documentPath in OneTimeSetup.TEST_DOCUMENTS) {
                using var file = PdfFile.Open(documentPath, IOStrategyType.FileStream);
                file.Initialize();
                using var document = PdfDocument.OpenFile(file);
                using var catalog = document.GetCatalog();
                ClassicAssert.IsNotNull(catalog);
            }
        }

        #endregion

        #region PdfDocument Open

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
        public void PdfDocument_OpenWithStrategy_Memory()
        {
            string path = Path.Combine("Resources", "minimalist.pdf");
            using var document = PdfDocument.Open(path, IOStrategyType.Memory);

            ClassicAssert.IsNotNull(document);

            using var catalog = document.GetCatalog();
            ClassicAssert.IsNotNull(catalog);
        }

        [Test]
        public void PdfDocument_OpenWithStrategy_FileStream_PageAccess()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");
            using var document = PdfDocument.Open(path, IOStrategyType.FileStream);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();

            var pageCount = pages.GetPageCount();
            ClassicAssert.Greater(pageCount, (ulong)0);

            using var page = pages.GetPage(1);
            ClassicAssert.IsNotNull(page);
        }

        [Test]
        public void PdfDocument_OpenWithStrategy_Memory_PageAccess()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");
            using var document = PdfDocument.Open(path, IOStrategyType.Memory);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();

            var pageCount = pages.GetPageCount();
            ClassicAssert.Greater(pageCount, (ulong)0);

            using var page = pages.GetPage(1);
            ClassicAssert.IsNotNull(page);
        }

        [Test]
        public void PdfDocument_OpenWithStrategy_FileStream_SamePageCountAsDefault()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");

            ulong defaultCount;
            using (var doc = PdfDocument.Open(path)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                defaultCount = pages.GetPageCount();
            }

            ulong strategyCount;
            using (var doc = PdfDocument.Open(path, IOStrategyType.FileStream)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                strategyCount = pages.GetPageCount();
            }

            ClassicAssert.AreEqual(defaultCount, strategyCount);
        }

        [Test]
        public void PdfDocument_OpenWithStrategy_Memory_SamePageCountAsDefault()
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");

            ulong defaultCount;
            using (var doc = PdfDocument.Open(path)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                defaultCount = pages.GetPageCount();
            }

            ulong strategyCount;
            using (var doc = PdfDocument.Open(path, IOStrategyType.Memory)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                strategyCount = pages.GetPageCount();
            }

            ClassicAssert.AreEqual(defaultCount, strategyCount);
        }

        #endregion

        #region PdfDocument Create

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

        [Test]
        public void PdfDocument_CreateWithStrategy_Memory()
        {
            string path = Path.GetTempFileName();
            try {
                using var document = PdfDocument.Create(path, IOStrategyType.Memory);
                ClassicAssert.IsNotNull(document);
            } finally {
                File.Delete(path);
            }
        }

        [Test]
        public void PdfDocument_CreateWithStrategy_FileStream_SaveAndReopen()
        {
            string createPath = Path.GetTempFileName();
            string savePath = Path.GetTempFileName();
            try {
                using (var document = PdfDocument.Create(createPath, IOStrategyType.FileStream)) {
                    document.Save(savePath);
                }

                using var file = PdfFile.Open(savePath, IOStrategyType.FileStream);
                file.Initialize();
                using var reopened = PdfDocument.OpenFile(file);
                using var catalog = reopened.GetCatalog();
                ClassicAssert.IsNotNull(catalog);
            } finally {
                File.Delete(createPath);
                File.Delete(savePath);
            }
        }

        #endregion

        #region Strategy equivalence

        [TestCase(IOStrategyType.FileStream)]
        [TestCase(IOStrategyType.Memory)]
        public void PdfFile_Strategy_ProduceSameVersionAsDefault(IOStrategyType strategy)
        {
            string path = Path.Combine("Resources", "minimalist.pdf");

            PdfVersion defaultVersion;
            using (var file = PdfFile.Open(path)) {
                file.Initialize();
                defaultVersion = file.Version;
            }

            PdfVersion strategyVersion;
            using (var file = PdfFile.Open(path, strategy)) {
                file.Initialize();
                strategyVersion = file.Version;
            }

            ClassicAssert.AreEqual(defaultVersion, strategyVersion);
        }

        [TestCase(IOStrategyType.FileStream)]
        [TestCase(IOStrategyType.Memory)]
        public void PdfDocument_Strategy_ProduceSamePageCountAsDefault(IOStrategyType strategy)
        {
            string path = Path.Combine("Resources", "19005-1_FAQ.PDF");

            ulong defaultCount;
            using (var doc = PdfDocument.Open(path)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                defaultCount = pages.GetPageCount();
            }

            ulong strategyCount;
            using (var doc = PdfDocument.Open(path, strategy)) {
                using var catalog = doc.GetCatalog();
                using var pages = catalog.GetPages();
                strategyCount = pages.GetPageCount();
            }

            ClassicAssert.AreEqual(defaultCount, strategyCount);
        }

        #endregion
    }
}
