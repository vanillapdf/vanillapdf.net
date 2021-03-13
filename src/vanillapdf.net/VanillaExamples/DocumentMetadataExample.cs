using System;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.Examples
{
    internal class DocumentMetadataExample
    {
        public static void PrintDocumentMetadata(string path)
        {
            var file = PdfFile.Open(path);
            var document = PdfDocument.OpenFile(file);
            var catalog = document.GetCatalog();
            var pageTree = catalog.GetPages();

            var isEncrypted = file.Encrypted;
            var version = catalog.Version;
            var pageCount = pageTree.GetPageCount();

            string versionString = (version.HasValue ? Convert.ToString(version.Value) : "Not specified");
            string encryptedString = isEncrypted ? "Yes" : "No";

            AddDocumentMetadata("Version", versionString);
            AddDocumentMetadata("Encrypted", encryptedString);
            AddDocumentMetadata("Pages", Convert.ToString(pageCount));
        }

        private static void AddDocumentMetadata(string key, string value)
        {
            Console.Out.WriteLine($"{key}: {value}");
        }
    }
}
