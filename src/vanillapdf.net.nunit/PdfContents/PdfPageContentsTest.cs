using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfContents
{
    [TestFixture]
    public class PdfPageContentsTest
    {
        [Test]
        public void TestDocumentContents()
        {
            //string testDirectory = TestContext.CurrentContext.TestDirectory;
            string sourceFile = Path.Combine("Resources", "19005-1_FAQ.PDF");
            string[] comparePageFiles = new string[] {
                Path.Combine("Resources", "19005-1_FAQ-1.txt"),
                Path.Combine("Resources", "19005-1_FAQ-2.txt"),
                Path.Combine("Resources", "19005-1_FAQ-3.txt"),
                Path.Combine("Resources", "19005-1_FAQ-4.txt"),
                Path.Combine("Resources", "19005-1_FAQ-5.txt"),
                Path.Combine("Resources", "19005-1_FAQ-6.txt"),
                Path.Combine("Resources", "19005-1_FAQ-7.txt"),
                Path.Combine("Resources", "19005-1_FAQ-8.txt"),
            };

            CheckDocumentContents(sourceFile, comparePageFiles);
        }

        private void CheckDocumentContents(string sourcePath, string[] comparePageFiles)
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourcePath);
            using var sourceFile = PdfFile.OpenStream(sourceStream, "sourceStream");
            //var sourceFile = PdfFile.Open(sourcePath);

            sourceFile.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(sourceFile);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            for (ulong i = 0; i < tree.GetPageCount(); ++i) {
                using var pageObject = tree.GetPage(i + 1);
                using var pageContents = pageObject.GetContents();

                StringBuilder stringBuilder = new StringBuilder();

                for (ulong j = 0; j < pageContents.GetInstructionsSize(); ++j) {
                    using var contentInstruction = pageContents.GetInstructionAt(j);

                    if (contentInstruction.GetInstructionType() == PdfContentInstructionType.Operation) {

                    }

                    if (contentInstruction.GetInstructionType() == PdfContentInstructionType.Object) {
                        using var contentObject = PdfContentObject.FromContentInstruction(contentInstruction);

                        if (contentObject.GetObjectType() == PdfContentObjectType.Text) {
                            using var contentObjectText = PdfContentObjectText.FromContentObject(contentObject);

                            for (ulong k = 0; k < contentObjectText.GetOperationsSize(); ++k) {
                                using var contentOperation = contentObjectText.GetOperationAt(k);

                                if (contentOperation.GetOperationType() == PdfContentOperationType.Generic) {
                                    using var contentOperationGeneric = PdfContentOperationGeneric.FromContentOperation(contentOperation);
                                    using var contentOperator = contentOperationGeneric.GetOperator();

                                    if (contentOperator.GetOperatorType() == PdfContentOperatorType.TextNextLine) {
                                        stringBuilder.Append(Environment.NewLine);
                                    }

                                    if (contentOperator.GetOperatorType() == PdfContentOperatorType.TextTranslate ||
                                        contentOperator.GetOperatorType() == PdfContentOperatorType.TextTranslateLeading) {
                                        using var operand_0 = contentOperationGeneric.GetOperandAt(0);
                                        using var operand_1 = contentOperationGeneric.GetOperandAt(1);

                                        //var text_translate_x = PdfRealObject.FromObject(operand_0);

                                        double text_translate_y = 0.0f;
                                        if (operand_1.GetObjectType() == PdfObjectType.Integer) {
                                            using var operand_1_integer = PdfIntegerObject.FromObject(operand_1);
                                            text_translate_y = operand_1_integer.IntegerValue;
                                        }

                                        if (operand_1.GetObjectType() == PdfObjectType.Real) {
                                            using var operand_1_real = PdfRealObject.FromObject(operand_1);
                                            text_translate_y = operand_1_real.Value;
                                        }

                                        // If -258.67 - 10.92 Td, Append newline
                                        if (text_translate_y < -1.0f) {
                                            stringBuilder.Append(Environment.NewLine);
                                        }
                                    }
                                }

                                if (contentOperation.GetOperationType() == PdfContentOperationType.TextShow) {
                                    using var contentOperationTextShow = PdfContentOperationTextShow.FromContentOperation(contentOperation);
                                    stringBuilder.Append(contentOperationTextShow.Value.Value.StringData);
                                    continue;
                                }

                                if (contentOperation.GetOperationType() == PdfContentOperationType.TextShowArray) {
                                    using var contentOperationTextShowArray = PdfContentOperationTextShowArray.FromContentOperation(contentOperation);

                                    foreach (var showItem in contentOperationTextShowArray.Value) {
                                        if (showItem.GetObjectType() == PdfObjectType.String) {
                                            using var showText = PdfStringObject.FromObject(showItem);
                                            stringBuilder.Append(showText.Value.StringData);
                                        }
                                    }

                                    continue;
                                }
                            }

                            // End of text object
                            stringBuilder.Append(Environment.NewLine);
                        }
                    }
                }

                //File.WriteAllText($"20131231103232738561744-{i}.txt", stringBuilder.ToString());
                string compareResult = File.ReadAllText(comparePageFiles[i]);

                string normalizedActual = Regex.Replace(stringBuilder.ToString(), @"\s", "");
                string normalizedExpected = Regex.Replace(compareResult, @"\s", "");

                ClassicAssert.AreEqual(normalizedExpected, normalizedActual);
            }
        }
    }
}
