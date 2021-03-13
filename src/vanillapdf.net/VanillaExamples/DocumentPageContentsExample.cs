using System;
using System.Text;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.Examples
{
    internal class DocumentPageContentsExample
    {
        public static void PrintDocumentPageContents(string path)
        {
            var document = PdfDocument.Open(path);
            var catalog = document.GetCatalog();
            var pageTree = catalog.GetPages();

            PrintDocumentPages(pageTree);
        }

        private static void PrintDocumentPages(PdfPageTree pageTree)
        {
            for (ulong i = 0; i < pageTree.GetPageCount(); ++i) {
                PrintDocumentPage(i, pageTree.GetPage(i + 1));
            }
        }

        private static void PrintDocumentPage(ulong pageId, PdfPageObject pageObject)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var pageContents = pageObject.GetContents();
            for (ulong j = 0; j < pageContents.GetInstructionsSize(); ++j) {
                var contentInstruction = pageContents.GetInstructionAt(j);

                if (contentInstruction.GetInstructionType() == PdfContentInstructionType.Operation) {
                    // TODO ? maybe gather other operations as well
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
                                stringBuilder.Append(contentOperationTextShow.Value.Value.StringData);
                                continue;
                            }

                            if (contentOperation.GetOperationType() == PdfContentOperationType.TextShowArray) {
                                var contentOperationTextShowArray = PdfContentOperationTextShowArray.FromContentOperation(contentOperation);

                                foreach (var showItem in contentOperationTextShowArray.Value) {
                                    if (showItem.GetObjectType() == PdfObjectType.String) {
                                        var showText = PdfStringObject.FromObject(showItem);
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

            PrintDocumentPageContents(pageId, stringBuilder.ToString());
        }

        private static void PrintDocumentPageContents(ulong pageId, string content)
        {
            Console.Out.WriteLine($"Page: {pageId}, Contents: {content}");
        }
    }
}
