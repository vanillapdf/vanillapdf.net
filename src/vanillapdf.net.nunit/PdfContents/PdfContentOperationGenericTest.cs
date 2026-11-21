using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit.PdfContents
{
    [TestFixture]
    public class PdfContentOperationGenericTest
    {
        [Test]
        public void GetOperandAtAs_WithMismatchedType_ThrowsObjectTypeException()
        {
            // An operator unknown to the parser is kept as a generic operation
            using var operation = ParseFirstGenericOperation("/NotANumber XYZ\n");

            Assert.Throws<PdfObjectTypeException>(() => operation.GetOperandAtAs<PdfIntegerObject>(0));
        }

        private static PdfContentOperationGeneric ParseFirstGenericOperation(string content)
        {
            using var fileStream = PdfInputOutputStream.CreateFromMemory();
            using var file = PdfFile.CreateStream(fileStream, "contentStream");
            using var buffer = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes(content));
            using var inputStream = PdfInputStream.CreateFromBuffer(buffer);
            using var parser = PdfContentParser.Create(file, inputStream);
            using var instructions = parser.ReadInstructionCollection();

            ClassicAssert.AreEqual(1, instructions.GetInstructionsSize());

            using var instruction = instructions.At(0);
            using var operation = PdfContentOperation.FromContentInstruction(instruction);
            ClassicAssert.AreEqual(PdfContentOperationType.Generic, operation.GetOperationType());

            return PdfContentOperationGeneric.FromContentOperation(operation);
        }
    }
}
