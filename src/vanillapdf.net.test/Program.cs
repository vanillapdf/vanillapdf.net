using System;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.test
{
    class Program
    {
        static void Main(string[] args)
        {
            //MiscUtils.InitializeClasses();

            PdfLogging.Severity = PdfLoggingSeverity.Debug;
            if (PdfLogging.Severity != PdfLoggingSeverity.Debug) {
                Console.WriteLine("Could not set logging severity");
            }

            PdfLogging.Enable();
            for (int i = 0; i < args.Length; ++i) {
                TestFile(args[i], null);
            }

            PdfLogging.Disable();

            Console.ReadKey();
        }

        static void TestFile(string path, string password)
        {
            try {
                TestFileInternal(path, password);
            }
            catch (PdfUnmanagedException ex) {
                Console.Out.WriteLine("Error with file {0}: {1}", path, ex.Message);

                // Last error is only available for unmanaged exceptions
                var lastError = PdfErrors.GetLastError();
                var lastErrorName = PdfReturnValues.GetValueName(lastError);
                var lastErrorMessage = PdfErrors.GetLastErrorMessage();

                Console.Out.WriteLine("Last error {0} ({1}): {2}", lastErrorName, lastError, lastErrorMessage);
            }
            catch (Exception ex) {
                Console.Out.WriteLine("Error with file {0}: {1}", path, ex.Message);
            }
        }

        static void TestFileInternal(string path, string password)
        {
            Console.Out.WriteLine("Testing file: {0}", path);

            var filename = Path.GetFileNameWithoutExtension(path);
            using (PdfFile file = PdfFile.Open(path)) {
                file.Initialize();

                if (file.IsEncrypted()) {
                    Console.Out.WriteLine("File {0} is encrypted, using password", path);

                    bool isCorrect = file.SetEncryptionPassword(password);
                    if (isCorrect) {
                        Console.Out.WriteLine("File {0} is unlocked with correct password", path);
                    } else {
                        Console.Out.WriteLine("File {0} is encrypted, but the password is incorrect", path);
                    }
                }

                using (var xrefChain = file.GetXrefChain()) {
                    CheckXrefChain(xrefChain);
                }

                using (PdfDocument document = PdfDocument.OpenFile(file)) {
                    PdfCatalog catalog = document.GetCatalog();
                    PdfPageTree tree = catalog.GetPages();

                    var count = tree.GetPageCount();
                    Console.Out.WriteLine("File contains {0} pages", count);

                    var destinationFilename = String.Format("{0}_out.pdf", filename);
                    Console.Out.WriteLine("Saving file to {0}", destinationFilename);

                    document.Save(destinationFilename);
                    Console.Out.WriteLine("File {0} tested successfully", path);
                }
            }
        }

        static void CheckXrefChain(PdfXrefChain chain)
        {
            foreach (var xref in chain) {
                CheckXref(xref);
            }
        }

        static void CheckXref(PdfXref xref)
        {
            var xrefOffset = xref.GetLastXrefOffset();
            Console.Out.WriteLine("Analyzing xref at offset {0}", xrefOffset);

            using (var trailerDictionary = xref.GetTrailerDictionary()) {
                // Check trailer dictionary
            }

            foreach (var entry in xref) {
                CheckXrefEntry(entry);
            }
        }

        static void CheckXrefEntry(PdfXrefEntry entry)
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

                    CheckObject(usedReference);
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

                    CheckObject(compressedReference);
                }
            }
        }

        static void CheckObject(PdfObject data)
        {
            if (data.GetObjectType() == PdfObjectType.Array) {
                var converted = PdfArrayObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Boolean) {
                var converted = PdfBooleanObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Dictionary) {
                var converted = PdfDictionaryObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.IndirectReference) {
                var converted = PdfIndirectReferenceObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Integer) {
                var converted = PdfIntegerObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Name) {
                var converted = PdfNameObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Null) {
                var converted = PdfNullObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Real) {
                var converted = PdfRealObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.Stream) {
                var converted = PdfStreamObject.FromObject(data);
            }

            if (data.GetObjectType() == PdfObjectType.String) {
                var converted = PdfStringObject.FromObject(data);
            }
        }
    }
}