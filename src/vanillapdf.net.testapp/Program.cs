using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.Utils;

namespace vanillapdf.net.testapp;

class Program
{
    static int Main(string[] args)
    {
        string? pdfPath = args.Length > 0 ? args[0] : FindTestPdf();

        if (pdfPath == null)
        {
            Console.Error.WriteLine("No PDF file specified and no test PDF found.");
            Console.Error.WriteLine("Usage: vanillapdf.net.testapp <pdf-file>");
            return 1;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return 1;
        }

        try
        {
            Console.WriteLine($"Opening PDF: {pdfPath}");

            LibraryInstance.Initialize(AppContext.BaseDirectory);

            using var file = PdfFile.Open(pdfPath);
            file.Initialize();

            Console.WriteLine($"  PDF Version: {file.Version}");

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();

            var pageCount = pages.GetPageCount();
            Console.WriteLine($"  Page Count: {pageCount}");

            if (pageCount > 0)
            {
                using var firstPage = pages.GetPage(0);
                using var resources = firstPage.GetResources();
                Console.WriteLine($"  First page has resources: {resources != null}");
            }

            Console.WriteLine("PDF operations completed successfully.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    static string? FindTestPdf()
    {
        var resourcesPath = Path.Combine(AppContext.BaseDirectory, "Resources");
        if (Directory.Exists(resourcesPath))
        {
            var pdfFiles = Directory.GetFiles(resourcesPath, "*.pdf", SearchOption.AllDirectories);
            return pdfFiles.FirstOrDefault();
        }
        return null;
    }
}
