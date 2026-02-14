using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit
{
    /// <summary>
    /// Tests that each UpgradePolicy level produces the expected object types
    /// from accessor methods in the syntax and content layers.
    /// </summary>
    [TestFixture]
    public class UpgradePolicyTest
    {
        private PdfFile _file;
        private PdfDocument _document;
        private PdfContentInstructionCollection _instructions;
        private PdfContentObjectText _textObject;
        private ulong _instructionCount;
        private ulong _operationCount;
        private UpgradePolicy _originalPolicy;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _originalPolicy = LibraryInstance.UpgradePolicy;

            var pdfPath = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "Resources", "19005-1_FAQ.PDF");

            _file = PdfFile.Open(pdfPath);
            _file.Initialize();
            _document = PdfDocument.OpenFile(_file);

            // Temporarily use Full to discover text objects
            LibraryInstance.UpgradePolicy = UpgradePolicy.Full;

            using var catalog = _document.GetCatalog();
            using var tree = catalog.GetPages();
            using var page = tree.GetPage(1);
            using var contents = page.GetContents();
            _instructions = contents.GetInstructionCollection();
            _instructionCount = _instructions.GetInstructionsSize();

            for (ulong i = 0; i < _instructionCount; i++) {
                using var instruction = _instructions.At(i);
                if (instruction is PdfContentObject contentObject
                    && contentObject.GetObjectType() == PdfContentObjectType.Text) {
                    _textObject = PdfContentObjectText.FromContentObject(contentObject);
                    _operationCount = _textObject.GetOperationsSize();
                    break;
                }
            }

            LibraryInstance.UpgradePolicy = _originalPolicy;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _textObject?.Dispose();
            _instructions?.Dispose();
            _document?.Dispose();
            _file?.Dispose();
            LibraryInstance.UpgradePolicy = _originalPolicy;
        }

        [TearDown]
        public void RestorePolicy()
        {
            LibraryInstance.UpgradePolicy = _originalPolicy;
        }

        #region Dictionary accessor

        [TestCase(UpgradePolicy.None, typeof(PdfObject))]
        [TestCase(UpgradePolicy.ResolveOnly, typeof(PdfObject))]
        [TestCase(UpgradePolicy.Single, typeof(PdfStringObject))]
        [TestCase(UpgradePolicy.Full, typeof(PdfLiteralStringObject))]
        public void DictionaryFind_StringValue_ReturnsExpectedType(UpgradePolicy policy, Type expectedType)
        {
            using var dict = PdfDictionaryObject.Create();
            using var key = PdfNameObject.CreateFromDecodedString("Key");
            using var str = PdfLiteralStringObject.CreateFromDecodedString("value");
            dict.Insert(key, str);

            LibraryInstance.UpgradePolicy = policy;
            using var found = dict.Find(key);
            ClassicAssert.AreEqual(expectedType, found.GetType());
        }

        #endregion

        #region Array accessor

        [TestCase(UpgradePolicy.None, typeof(PdfObject))]
        [TestCase(UpgradePolicy.ResolveOnly, typeof(PdfObject))]
        [TestCase(UpgradePolicy.Single, typeof(PdfStringObject))]
        [TestCase(UpgradePolicy.Full, typeof(PdfLiteralStringObject))]
        public void ArrayGetValue_StringValue_ReturnsExpectedType(UpgradePolicy policy, Type expectedType)
        {
            using var arr = PdfArrayObject.Create();
            using var str = PdfLiteralStringObject.CreateFromDecodedString("value");
            arr.Append(str);

            LibraryInstance.UpgradePolicy = policy;
            using var found = arr.GetValue(0);
            ClassicAssert.AreEqual(expectedType, found.GetType());
        }

        #endregion

        #region Content instruction accessor

        [Test]
        public void InstructionAt_PolicyNone_ReturnsBaseInstruction()
        {
            LibraryInstance.UpgradePolicy = UpgradePolicy.None;
            using var instruction = _instructions.At(0);
            ClassicAssert.AreEqual(typeof(PdfContentInstruction), instruction.GetType());
        }

        [TestCase(UpgradePolicy.ResolveOnly)]
        [TestCase(UpgradePolicy.Single)]
        public void InstructionAt_ResolveOnlyOrSingle_ReturnsOneLevel(UpgradePolicy policy)
        {
            LibraryInstance.UpgradePolicy = policy;
            using var instruction = _instructions.At(0);
            // One level upgrade: should be Operation or Object, not further derived
            ClassicAssert.IsTrue(
                instruction.GetType() == typeof(PdfContentOperation)
                || instruction.GetType() == typeof(PdfContentObject),
                $"Expected PdfContentOperation or PdfContentObject, got {instruction.GetType().Name}");
        }

        [Test]
        public void InstructionAt_PolicyFull_ReturnsLeafType()
        {
            LibraryInstance.UpgradePolicy = UpgradePolicy.Full;
            using var instruction = _instructions.At(0);
            // Full upgrade: should be a leaf type, not a base type
            ClassicAssert.AreNotEqual(typeof(PdfContentInstruction), instruction.GetType());
            ClassicAssert.AreNotEqual(typeof(PdfContentOperation), instruction.GetType());
            ClassicAssert.AreNotEqual(typeof(PdfContentObject), instruction.GetType());
        }

        #endregion

        #region Content operation accessor

        [Test]
        public void OperationAt_PolicyNone_ReturnsBaseOperation()
        {
            ClassicAssert.IsNotNull(_textObject, "No text object found in test PDF");

            LibraryInstance.UpgradePolicy = UpgradePolicy.None;
            using var operation = _textObject.GetOperationAt(0);
            ClassicAssert.AreEqual(typeof(PdfContentOperation), operation.GetType());
        }

        [TestCase(UpgradePolicy.ResolveOnly)]
        [TestCase(UpgradePolicy.Single)]
        [TestCase(UpgradePolicy.Full)]
        public void OperationAt_NonNone_ReturnsLeafType(UpgradePolicy policy)
        {
            ClassicAssert.IsNotNull(_textObject, "No text object found in test PDF");

            LibraryInstance.UpgradePolicy = policy;
            using var operation = _textObject.GetOperationAt(0);
            // Operation upgrade always goes to leaf type
            ClassicAssert.AreNotEqual(typeof(PdfContentOperation), operation.GetType());
            ClassicAssert.IsInstanceOf<PdfContentOperation>(operation);
        }

        #endregion
    }
}
