using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfFileWriterTest
    {
        public class PdfCustomFileWriterObserverContext : PdfFileWriterObserverContext
        {
            public override uint OnAfterEntryOffsetRecalculation(PdfXrefEntry data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnAfterObjectOffsetRecalculation(PdfObject data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnAfterObjectWrite(PdfObject data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnAfterOutputFlush(PdfInputOutputStream data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnBeforeEntryOffsetRecalculation(PdfXrefEntry data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnBeforeObjectOffsetRecalculation(PdfObject data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnBeforeObjectWrite(PdfObject data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnBeforeOutputFlush(PdfInputOutputStream data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnFinalizing(PdfInputOutputStream data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint OnInitializing(PdfInputOutputStream data)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }
        }

        [Test]
        public void TestWrite()
        {
            foreach (var sourcePath in OneTimeSetup.TEST_DOCUMENTS) {
                TestWriteInternal(sourcePath);
            }
        }

        [Test]
        public void TestLicensing()
        {
            try {
                PdfCustomFileWriterObserverContext observerContext = new PdfCustomFileWriterObserverContext();
                PdfFileWriterObserver fileWriterObserver = PdfFileWriterObserver.CreateCustom(observerContext);
            }
            catch (Exception ex) {
                Assert.IsTrue(ex is PdfLicenseRequiredException);
            }
        }

        private void TestWriteInternal(string sourcePath)
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourcePath);
            using var destinationStream = PdfInputOutputStream.CreateFromMemory();

            using var sourceFile = PdfFile.OpenStream(sourceStream, "sourceStream");
            sourceFile.Initialize();

            using var destinationFile = PdfFile.CreateStream(destinationStream, "destinationStream");

            using var writer = PdfFileWriter.Create();
            writer.Write(sourceFile, destinationFile);
        }
    }
}
