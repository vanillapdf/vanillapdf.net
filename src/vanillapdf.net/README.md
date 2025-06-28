# Vanilla.PDF .NET

A lightweight .NET wrapper for the native [Vanilla.PDF](https://github.com/vanillapdf/vanillapdf) library. Use it to create, inspect and sign PDF documents with native performance on Windows, Linux and macOS.

## Install

```bash
dotnet add package vanillapdf.net
```

## Quick start

```csharp
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

using var file = PdfFile.Open("input.pdf");
file.Initialize();
using var doc = PdfDocument.OpenFile(file);
Console.WriteLine(doc.GetCatalog().GetPages().GetPageCount());
```

## Resources

- [Changelog](https://github.com/vanillapdf/vanillapdf.net/blob/main/CHANGELOG.txt)
- [License](https://github.com/vanillapdf/vanillapdf.net/blob/main/LICENSE.txt)
