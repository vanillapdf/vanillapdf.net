using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfFileStructureValidatorTest
    {
        private static readonly string ValidPath = Path.Combine("Resources", "minimalist.pdf");

        // Cross-reference stream whose entry for the object stream points into that same stream
        private static readonly string ObjectStreamPath = Path.Combine("Resources", "objstm_self_reference.pdf");

        // minimalist.pdf with the trailer /Size changed from 4 to 5
        private static readonly string SizeMismatchPath = Path.Combine("Resources", "trailer_size_mismatch.pdf");

        [Test]
        public void TestValidFileHasNoIssues()
        {
            using var file = PdfFile.Open(ValidPath);
            file.Initialize();

            using var result = PdfFileStructureValidator.Validate(file);

            ClassicAssert.IsTrue(result.IsValid);
            ClassicAssert.AreEqual(0UL, result.IssueCount);
            ClassicAssert.AreEqual(0UL, result.ErrorCount);
            ClassicAssert.AreEqual(0UL, result.WarningCount);
        }

        [Test]
        public void TestObjectIssues()
        {
            using var file = PdfFile.Open(ObjectStreamPath);
            file.Initialize();

            using var result = PdfFileStructureValidator.Validate(file);

            ClassicAssert.IsFalse(result.IsValid);
            ClassicAssert.AreEqual(2UL, result.IssueCount);
            ClassicAssert.AreEqual(2UL, result.ErrorCount);
            ClassicAssert.AreEqual(0UL, result.WarningCount);

            using var first = result.GetIssue(0);
            ClassicAssert.AreEqual(PdfFileStructureIssueSeverityType.Error, first.Severity);
            ClassicAssert.AreEqual(PdfFileStructureIssueCodeType.XrefCompressedEntryObjectStreamCompressed, first.Code);
            ClassicAssert.AreEqual("Compressed xref entry references object stream 1 which is itself stored inside another object stream", first.Message);
            ClassicAssert.AreEqual(1UL, first.ObjectNumber);
            ClassicAssert.AreEqual((UInt16)0, first.GenerationNumber);

            using var second = result.GetIssue(1);
            ClassicAssert.AreEqual(PdfFileStructureIssueCodeType.XrefCompressedEntryObjectStreamCompressed, second.Code);
            ClassicAssert.AreEqual(7UL, second.ObjectNumber);
            ClassicAssert.AreEqual((UInt16)0, second.GenerationNumber);
        }

        [Test]
        public void TestFileLevelIssue()
        {
            using var file = PdfFile.Open(SizeMismatchPath);
            file.Initialize();

            using var result = PdfFileStructureValidator.Validate(file);

            ClassicAssert.IsTrue(result.IsValid);
            ClassicAssert.AreEqual(1UL, result.IssueCount);
            ClassicAssert.AreEqual(0UL, result.ErrorCount);
            ClassicAssert.AreEqual(1UL, result.WarningCount);

            using var issue = result.GetIssue(0);
            ClassicAssert.AreEqual(PdfFileStructureIssueSeverityType.Warning, issue.Severity);
            ClassicAssert.AreEqual(PdfFileStructureIssueCodeType.TrailerSizeMismatch, issue.Code);
            ClassicAssert.AreEqual("Trailer /Size value (5) does not match expected value (4)", issue.Message);
            ClassicAssert.IsNull(issue.ObjectNumber);
            ClassicAssert.IsNull(issue.GenerationNumber);
        }

        [Test]
        public void TestGetIssueOutOfRangeThrows()
        {
            using var file = PdfFile.Open(ObjectStreamPath);
            file.Initialize();

            using var result = PdfFileStructureValidator.Validate(file);

            Assert.Throws<PdfParameterValueException>(() => result.GetIssue(2));
        }
    }
}
