using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.Examples
{
    internal class DocumentObjectsExample
    {
        public static void PrintDocumentObjects(string path)
        {
            var file = PdfFile.Open(path);

            file.Initialize();
            CheckXrefChain(file.XrefChain);
        }

        private static void CheckXrefChain(PdfXrefChain chain)
        {
            foreach (var xref in chain) {
                CheckXref(xref);
            }
        }

        private static void CheckXref(PdfXref xref)
        {
            foreach (var entry in xref) {
                CheckXrefEntry(entry);
            }
        }

        private static void CheckXrefEntry(PdfXrefEntry entry)
        {
            if (entry.GetEntryType() == PdfXrefEntryType.Null) {
            }

            if (entry.GetEntryType() == PdfXrefEntryType.Free) {
                var freeEntry = PdfXrefFreeEntry.FromEntry(entry);

                Console.Out.WriteLine("Found FREE entry: [{0} {1}] {2}",
                    entry.GetObjectNumber(),
                    entry.GetGenerationNumber(),
                    freeEntry.GetNextFreeObjectNumber());
            }

            if (entry.GetEntryType() == PdfXrefEntryType.Used) {
                var usedEntry = PdfXrefUsedEntry.FromEntry(entry);

                using (var usedReference = usedEntry.GetReference()) {

                    Console.Out.WriteLine("Found USED entry: [{0} {1}] {2} {3}",
                        entry.GetObjectNumber(),
                        entry.GetGenerationNumber(),
                        usedEntry.GetOffset(),
                        usedReference.GetObjectType());
                }
            }

            if (entry.GetEntryType() == PdfXrefEntryType.Compressed) {
                var compressedEntry = PdfXrefCompressedEntry.FromEntry(entry);

                using (var compressedReference = compressedEntry.GetReference()) {

                    Console.Out.WriteLine("Found COMPRESSED entry: [{0} {1}] [{2} {3}] {4}",
                        entry.GetObjectNumber(),
                        entry.GetGenerationNumber(),
                        compressedEntry.GetObjectStreamNumber(),
                        compressedEntry.GetIndex(),
                        compressedReference.GetObjectType());
                }
            }
        }
    }
}
