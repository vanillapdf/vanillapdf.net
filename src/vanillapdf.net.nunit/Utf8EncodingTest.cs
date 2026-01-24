using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit
{
    /// <summary>
    /// Tests for string encoding handling in PDF objects.
    /// Verifies how encoded strings with hex escapes are interpreted.
    /// </summary>
    [TestFixture]
    public class Utf8EncodingTest
    {
        #region LiteralStringObject Encoding Tests

        [Test]
        public void LiteralStringObject_CreateFromEncodedString_OctalEscape_101()
        {
            // \101 = octal 101 = decimal 65 = ASCII 'A'
            const string encodedValue = "Test\\101Value";
            using var obj = PdfLiteralStringObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            // Verify the octal escape was decoded to 'A'
            ClassicAssert.AreEqual("TestAValue", result);
        }

        [Test]
        public void LiteralStringObject_CreateFromEncodedString_OctalEscape_110()
        {
            // \110 = octal 110 = decimal 72 = ASCII 'H'
            const string encodedValue = "\\110ello";
            using var obj = PdfLiteralStringObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual("Hello", result);
        }

        [Test]
        public void LiteralStringObject_CreateFromEncodedString_BackslashEscapes()
        {
            // Test standard PDF escape sequences: \n, \r, \t, \\
            const string encodedValue = "Line1\\nLine2\\tTab\\\\Backslash";
            using var obj = PdfLiteralStringObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual("Line1\nLine2\tTab\\Backslash", result);
        }

        [Test]
        public void LiteralStringObject_CreateFromEncodedString_ParenthesisEscapes()
        {
            // Test escaped parentheses: \( and \)
            const string encodedValue = "Text\\(with\\)parens";
            using var obj = PdfLiteralStringObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual("Text(with)parens", result);
        }

        [Test]
        public void LiteralStringObject_CreateFromDecodedString_RoundTrip()
        {
            const string originalValue = "Test String 123";
            using var obj = PdfLiteralStringObject.CreateFromDecodedString(originalValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual(originalValue, result);
        }

        #endregion

        #region NameObject Encoding Tests

        [Test]
        public void NameObject_CreateFromEncodedString_HexEscape()
        {
            // #41 = 'A'
            const string encodedValue = "Test#41Name";
            using var obj = PdfNameObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual("TestAName", result);
        }

        [Test]
        public void NameObject_CreateFromDecodedString_RoundTrip()
        {
            const string originalValue = "SimpleName";
            using var obj = PdfNameObject.CreateFromDecodedString(originalValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual(originalValue, result);
        }

        #endregion

        #region HexadecimalStringObject Encoding Tests

        [Test]
        public void HexadecimalStringObject_CreateFromEncodedString_Valid()
        {
            // Hex string "48656C6C6F" = "Hello"
            const string encodedValue = "48656C6C6F";
            using var obj = PdfHexadecimalStringObject.CreateFromEncodedString(encodedValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual("Hello", result);
        }

        [Test]
        public void HexadecimalStringObject_CreateFromDecodedString_RoundTrip()
        {
            const string originalValue = "Test";
            using var obj = PdfHexadecimalStringObject.CreateFromDecodedString(originalValue);

            var buffer = obj.Value;
            var result = buffer.StringData;

            ClassicAssert.AreEqual(originalValue, result);
        }

        #endregion
    }
}
