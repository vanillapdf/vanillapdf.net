using NUnit.Framework;
using System;
using System.IO;
using System.Text;
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
            string sourceFile = "Resources\\19005-1_FAQ.PDF";
            string[] comparePageFiles = new string[] {
                "Resources\\19005-1_FAQ-1.txt",
                "Resources\\19005-1_FAQ-2.txt",
                "Resources\\19005-1_FAQ-3.txt",
                "Resources\\19005-1_FAQ-4.txt",
                "Resources\\19005-1_FAQ-5.txt",
                "Resources\\19005-1_FAQ-6.txt",
                "Resources\\19005-1_FAQ-7.txt",
                "Resources\\19005-1_FAQ-8.txt",
            };

            CheckDocumentContents(sourceFile, comparePageFiles);
        }

        private void CheckDocumentContents(string sourcePath, string[] comparePageFiles)
        {
            var sourceStream = PdfInputOutputStream.CreateFromFile(sourcePath);
            var sourceFile = PdfFile.OpenStream(sourceStream, "sourceStream");
            //var sourceFile = PdfFile.Open(sourcePath);

            sourceFile.Initialize();

            PdfDocument document = PdfDocument.OpenFile(sourceFile);
            PdfCatalog catalog = document.GetCatalog();
            PdfPageTree tree = catalog.GetPages();

            for (ulong i = 0; i < tree.GetPageCount(); ++i) {
                var pageObject = tree.GetPage(i + 1);
                var pageContents = pageObject.GetContents();

                StringBuilder stringBuilder = new StringBuilder();

                for (ulong j = 0; j < pageContents.GetInstructionsSize(); ++j) {
                    var contentInstruction = pageContents.GetInstructionAt(j);

                    if (contentInstruction.GetInstructionType() == PdfContentInstructionType.Operation) {

                    }

                    if (contentInstruction.GetInstructionType() == PdfContentInstructionType.Object) {
                        var contentObject = PdfContentObject.FromContentInstruction(contentInstruction);

                        if (contentObject.GetObjectType() == PdfContentObjectType.Text) {
                            var contentObjectText = PdfContentObjectText.FromContentObject(contentObject);

                            for (ulong k = 0; k < contentObjectText.GetOperationsSize(); ++k) {
                                var contentOperation = contentObjectText.GetOperationAt(k);

                                if (contentOperation.GetOperationType() == PdfContentOperationType.Generic) {
                                    var contentOperationGeneric = PdfContentOperationGeneric.FromContentOperation(contentOperation);
                                    var contentOperator = contentOperationGeneric.GetOperator();

                                    if (contentOperator.GetOperatorType() == PdfContentOperatorType.TextNextLine) {
                                        stringBuilder.Append(Environment.NewLine);
                                    }

                                    if (contentOperator.GetOperatorType() == PdfContentOperatorType.TextTranslate ||
                                        contentOperator.GetOperatorType() == PdfContentOperatorType.TextTranslateLeading) {
                                        var operand_0 = contentOperationGeneric.GetOperandAt(0);
                                        var operand_1 = contentOperationGeneric.GetOperandAt(1);

                                        //var text_translate_x = PdfRealObject.FromObject(operand_0);

                                        double text_translate_y = 0.0f;
                                        if (operand_1.GetObjectType() == PdfObjectType.Integer) {
                                            var operand_1_integer = PdfIntegerObject.FromObject(operand_1);
                                            text_translate_y = operand_1_integer.IntegerValue;
                                        }

                                        if (operand_1.GetObjectType() == PdfObjectType.Real) {
                                            var operand_1_real = PdfRealObject.FromObject(operand_1);
                                            text_translate_y = operand_1_real.Value;
                                        }

                                        // If -258.67 - 10.92 Td, Append newline
                                        if (text_translate_y < -1.0f) {
                                            stringBuilder.Append(Environment.NewLine);
                                        }
                                    }
                                }

                                if (contentOperation.GetOperationType() == PdfContentOperationType.TextShow) {
                                    var contentOperationTextShow = PdfContentOperationTextShow.FromContentOperation(contentOperation);
                                    stringBuilder.Append(contentOperationTextShow.Value.Value.GetDataString());
                                    continue;
                                }

                                if (contentOperation.GetOperationType() == PdfContentOperationType.TextShowArray) {
                                    var contentOperationTextShowArray = PdfContentOperationTextShowArray.FromContentOperation(contentOperation);

                                    foreach (var showItem in contentOperationTextShowArray.Value) {
                                        if (showItem.GetObjectType() == PdfObjectType.String) {
                                            var showText = PdfStringObject.FromObject(showItem);
                                            stringBuilder.Append(showText.Value.GetDataString());
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
                Assert.AreEqual(compareResult, stringBuilder.ToString());
            }
        }
    }
}
