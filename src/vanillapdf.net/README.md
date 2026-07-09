# Vanilla.PDF .NET

[![NuGet](https://img.shields.io/nuget/v/vanillapdf.net?color=blue)](https://www.nuget.org/packages/vanillapdf.net) [![Downloads](https://img.shields.io/nuget/dt/vanillapdf.net?color=blue)](https://www.nuget.org/packages/vanillapdf.net) [![License](https://img.shields.io/badge/license-Apache%202.0-blue)](https://github.com/vanillapdf/vanillapdf.net/blob/main/LICENSE.txt) [![Build](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml/badge.svg)](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml)

The official .NET binding for the [Vanilla.PDF](https://github.com/vanillapdf/vanillapdf) C++17 library. It exposes a high-performance, cross-platform API to **create, inspect, edit and digitally sign** PDF documents from any .NET Standard 2.0+ application, through a thin P/Invoke layer over the native SDK.

---

## ✨ Highlights

- **Cross-platform** — Windows, Linux and macOS (x86, x64, ARM64) via the bundled native runtime.
- **Broad framework support** — targets .NET Standard 2.0 and .NET 8.0, so it runs on .NET Framework 4.6.1+, .NET Core, .NET 5+ and Mono.
- **AOT- and trim-friendly** — `LibraryImport`-based interop with AOT/trim analyzers enabled on .NET 8+.
- **Digital signatures** — sign with PKCS#12 keys (CMS/PKCS#7) and verify against a trusted certificate store.
- **Encryption** — open and save password- or certificate-protected documents (AES / RC4).
- **Full PDF model** — low-level syntax (objects, xref, streams, filters) and a high-level semantic model (pages, annotations, AcroForm fields, outlines, destinations, fonts).
- **Thread-safe** — the underlying library synchronizes access to file objects.

Vanilla.PDF is a document **structure** toolkit. It does **not** rasterize, render or display PDF pages; pair it with a rendering engine if you need to draw pages to a bitmap or UI.

---

## 🚀 Getting Started

```bash
dotnet add package vanillapdf.net
```

Runs on any .NET Standard 2.0-compatible runtime (.NET Framework 4.6.1+, .NET Core 2.0+, .NET 5/6/7/8+, Mono). The platform-specific native libraries ship inside the package — no extra tooling required.

---

## ✍️ Usage

> Every Vanilla.PDF object wraps a native resource and implements `IDisposable`. Always dispose them — the `using` declaration is idiomatic — to release native memory promptly.

```csharp
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

using var file = PdfFile.Open("input.pdf");
file.Initialize();  // parses the cross-reference tables

using var document = PdfDocument.OpenFile(file);
using var catalog = document.GetCatalog();
using var pages = catalog.GetPages();

Console.WriteLine($"PDF version: {file.Version}");
Console.WriteLine($"Page count:  {pages.GetPageCount()}");
```

More examples — signing, signature verification, AcroForm, content parsing — are in the
[repository README](https://github.com/vanillapdf/vanillapdf.net/blob/main/README.md) and the
[test suite](https://github.com/vanillapdf/vanillapdf.net/tree/main/src/vanillapdf.net.nunit).

---

## 📦 Layers

| Namespace | Purpose |
|---|---|
| `vanillapdf.net.PdfSyntax` | Low-level PDF structure: `PdfFile`, objects, arrays, dictionaries, streams, xref. |
| `vanillapdf.net.PdfSemantics` | High-level model: `PdfDocument`, catalog, pages, annotations, AcroForm, signatures. |
| `vanillapdf.net.PdfContents` | Content-stream parsing: operators, text/font operations, inline images. |
| `vanillapdf.net.PdfUtils` | Streams, buffers, signing keys, certificate stores, logging. |

---

## 🔗 Links

- **Source & issues:** [github.com/vanillapdf/vanillapdf.net](https://github.com/vanillapdf/vanillapdf.net)
- **Changelog:** [GitHub Releases](https://github.com/vanillapdf/vanillapdf.net/releases)
- **License:** [Apache-2.0](https://github.com/vanillapdf/vanillapdf.net/blob/main/LICENSE.txt) · third-party notices in [NOTICE.md](https://github.com/vanillapdf/vanillapdf.net/blob/main/NOTICE.md)
- **Support:** [issue tracker](https://github.com/vanillapdf/vanillapdf.net/issues) or [contact us](https://vanillapdf.com/contact)
