using System;
using System.IO;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.test
{
    class Program
    {
        static void Main(string[] args)
        {
            MiscUtils.InitializeClasses();

            PdfLogging.SetSeverity(LoggingSeverity.Debug);
            if (PdfLogging.GetSeverity() != LoggingSeverity.Debug) {
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
            catch (Exception ex) {
                Console.Out.WriteLine("Error with file {0}: {1}", path, ex.Message);

                var lastError = PdfErrors.GetLastError();
                var lastErrorName = PdfReturnValues.GetValueName(lastError);
                var lastErrorMessage = PdfErrors.GetLastErrorMessage();

                Console.Out.WriteLine("Last error {0} ({1}): {2}", lastErrorName, lastError, lastErrorMessage);
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

                using (var xrefChain = file.GetXrefChain())
                using (var xrefChainIterator = xrefChain.GetIterator()) {
                    while (true) {
                        if (!xrefChain.IsIteratorValid(xrefChainIterator)) {
                            break;
                        }

                        using (var xref = xrefChainIterator.GetValue()) {

                            var xrefOffset = xref.GetLastXrefOffset();
                            Console.Out.WriteLine("Analyzing xref at offset {0}", xrefOffset);

                            using (var trailerDictionary = xref.GetTrailerDictionary()) {
                                // Check trailer dictionary
                            }

                            using (var xrefIterator = xref.GetIterator()) {
                                while (true) {
                                    if (!xref.IsIteratorValid(xrefIterator)) {
                                        break;
                                    }

                                    using (var entry = xrefIterator.GetValue()) {
                                        // We got entry!!!

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
                                            var usedReference = usedEntry.GetReference();

                                            Console.Out.WriteLine("Found USED entry: [{0} {1}] {2} {3}",
                                                entry.GetObjectNumber(),
                                                entry.GetGenerationNumber(),
                                                usedEntry.GetOffset(),
                                                usedReference.GetObjectType());
                                        }

                                        if (entry.GetEntryType() == PdfXrefEntryType.Compressed) {
                                            var compressedEntry = PdfXrefCompressedEntry.FromEntry(entry);
                                            var compressedReference = compressedEntry.GetReference();

                                            Console.Out.WriteLine("Found COMPRESSED entry: [{0} {1}] [{2} {3}] {4}",
                                                entry.GetObjectNumber(),
                                                entry.GetGenerationNumber(),
                                                compressedEntry.GetObjectStreamNumber(),
                                                compressedEntry.GetIndex(),
                                                compressedReference.GetObjectType());
                                        }
                                    }

                                    xrefIterator.Next();
                                }
                            }
                        }

                        xrefChainIterator.Next();
                    }
                }

                using (PdfDocument document = PdfDocument.OpenFile(file)) {
                    PdfCatalog catalog = document.GetCatalog();
                    PdfPageTree tree = catalog.GetPageTree();

                    var count = tree.GetPageCount();
                    Console.Out.WriteLine("File contains {0} pages", count);

                    var destinationFilename = String.Format("{0}_out.pdf", filename);
                    Console.Out.WriteLine("Saving file to {0}", destinationFilename);

                    document.Save(destinationFilename);
                    Console.Out.WriteLine("File {0} tested successfully", path);
                }
            }

            
        }
    }
}