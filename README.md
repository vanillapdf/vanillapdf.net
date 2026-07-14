# Vanilla.PDF .NET

[![NuGet](https://img.shields.io/nuget/v/vanillapdf.net?color=blue)](https://www.nuget.org/packages/vanillapdf.net) [![Downloads](https://img.shields.io/nuget/dt/vanillapdf.net?color=blue)](https://www.nuget.org/packages/vanillapdf.net) [![License](https://img.shields.io/badge/license-Apache%202.0-blue)](LICENSE.txt) [![Build](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml/badge.svg)](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml)

The official .NET binding for the [Vanilla.PDF](https://github.com/vanillapdf/vanillapdf) C++17 library. It exposes a high‑performance, cross‑platform API to **create, inspect, edit and digitally sign** PDF documents from any .NET Standard 2.0+ application, through a thin P/Invoke layer over the native SDK.

---

## 📋 Table of Contents

1. [Highlights](#-highlights)
2. [What It Does (and Doesn't)](#-what-it-does-and-doesnt)
3. [Getting Started](#-getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
4. [Usage](#-usage)
   - [Inspect a document](#inspect-a-document)
   - [Sign a document](#sign-a-document)
   - [Verify a signature](#verify-a-signature)
5. [Architecture](#-architecture)
6. [Samples & Documentation](#-samples--documentation)
7. [Building & Testing](#-building--testing)
8. [Versioning & Compatibility](#-versioning--compatibility)
9. [Contributing](#-contributing)
10. [Security](#-security)
11. [Support](#-support)
12. [License](#-license)

---

## ✨ Highlights

- **Cross‑platform** — Windows, Linux and macOS (x86, x64, ARM64) via the bundled native runtime.
- **Broad framework support** — targets .NET Standard 2.0 and .NET 8.0, so it runs on .NET Framework 4.6.1+, .NET Core, .NET 5+ and Mono.
- **AOT- and trim-friendly** — `LibraryImport`-based interop with full AOT/trim analyzers enabled on .NET 8+.
- **Digital signatures** — sign with PKCS#12 keys (CMS/PKCS#7) and verify signatures against a trusted certificate store.
- **Encryption** — open and save documents protected with password- or certificate-based encryption (AES / RC4).
- **Full PDF model** — from low-level syntax (objects, xref, streams, filters) to a high-level semantic model (pages, annotations, AcroForm fields, outlines, destinations, fonts).
- **Thread-safe** — the underlying library synchronizes access to file objects, so instances can be used concurrently.
- **Native performance** — heavy lifting happens in the C++17 core; the managed layer is a thin, allocation-conscious wrapper.

---

## 🎯 What It Does (and Doesn't)

Vanilla.PDF is a document **structure** toolkit: it reads, writes, edits and signs the internals of PDF files.

It does **not** rasterize or render pages, perform text layout / font shaping, or display PDFs on screen. If you need to draw PDFs to a bitmap or a UI surface, pair Vanilla.PDF with a dedicated rendering engine.

---

## 🚀 Getting Started

### Prerequisites

- **To consume the package:** any runtime compatible with .NET Standard 2.0 — .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5/6/7/8+ or Mono. No native tooling is required; the platform-specific native libraries ship inside the NuGet package.
- **To build from source:** the .NET SDK 8.0 or newer.

### Installation

```bash
dotnet add package vanillapdf.net
```

---

## ✍️ Usage

> Every Vanilla.PDF object wraps a native resource and implements `IDisposable`. Always dispose them — the `using` declaration is the idiomatic way — to release native memory promptly.

### Inspect a document

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

### Sign a document

```csharp
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

using var file = PdfFile.Open("input.pdf");
file.Initialize();

using var document = PdfDocument.OpenFile(file);
using var destination = PdfFile.Create("signed.pdf");
using var settings = PdfDocumentSignatureSettings.Create();

using var keyBuffer = PdfBuffer.Create();
keyBuffer.Data = File.ReadAllBytes("certificate.pfx");
using var key = PdfPKCS12Key.CreateFromBuffer(keyBuffer, "key-password");

settings.SigningKey = key;
settings.Digest = PdfMessageDigestAlgorithmType.SHA256;

document.Sign(destination, settings);
```

### Verify a signature

```csharp
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

using var file = PdfFile.Open("signed.pdf");
file.Initialize();

using var document = PdfDocument.OpenFile(file);
using var catalog = document.GetCatalog();
using var acroForm = catalog.GetAcroForm();
using var fields = acroForm.GetFields();
using var store = TrustedCertificateStore.Create();
store.LoadSystemDefaults();

for (ulong i = 0; i < fields.GetSize(); i++) {
    using var field = fields.GetValueAt(i);
    if (field.GetFieldType() != PdfFieldType.Signature) continue;

    using var signatureField = PdfSignatureField.FromField(field);
    using var signature = signatureField.GetValue();
    if (signature == null) continue;

    using var result = signature.Verify(document, store);
    Console.WriteLine($"{result.SignerCommonName}: {result.Status}");
}
```

---

## 🏗️ Architecture

The library is organized into layers that mirror the native SDK:

| Namespace | Purpose |
|---|---|
| `vanillapdf.net.PdfSyntax` | Low-level PDF structure: `PdfFile`, objects, arrays, dictionaries, streams, xref. |
| `vanillapdf.net.PdfSemantics` | High-level model built on the syntax layer: `PdfDocument`, catalog, pages, annotations, AcroForm, signatures. |
| `vanillapdf.net.PdfContents` | Content-stream parsing: operators, text and font operations, inline images. |
| `vanillapdf.net.PdfUtils` | Streams, buffers, signing keys, certificate stores, logging. |

Every object the library hands you is `IDisposable` and owns a native handle, so dispose it when you are done — a `using` declaration is the easiest way. Objects returned from a getter (a page from a page tree, a catalog from a document) own their own handle and must each be disposed; disposing the parent does not dispose them.

---

## 📁 Samples & Documentation

- **Runnable console sample:** [`src/vanillapdf.net.testapp`](src/vanillapdf.net.testapp) opens a PDF and prints its version, page count and resources.
- **Executable examples:** the [`src/vanillapdf.net.nunit`](src/vanillapdf.net.nunit) test suite is the most complete, up-to-date source of usage patterns (signing, verification, AcroForm, content parsing, encryption).
- **Release history:** see the [GitHub Releases](https://github.com/vanillapdf/vanillapdf.net/releases) page (the authoritative changelog).

> A dedicated documentation site for the .NET binding is planned ([#125](https://github.com/vanillapdf/vanillapdf.net/issues/125)). Until then, the code samples above are the canonical reference.

---

## 🔧 Building & Testing

```bash
git clone https://github.com/vanillapdf/vanillapdf.net.git
cd vanillapdf.net

dotnet restore
dotnet build src/vanillapdf.net.sln

# run the test suite (Release mirrors CI)
dotnet test src/vanillapdf.net.sln --configuration Release

# run a single test
dotnet test src/vanillapdf.net.sln --filter "FullyQualifiedName~TestMethodName"

# verify the native libraries publish correctly across runtimes
./scripts/test_dotnet_publish.sh
```

To test unreleased builds published to GitHub Packages:

```bash
export NUGET_AUTH_TOKEN=<your-token>
dotnet nuget add source "https://nuget.pkg.github.com/vanillapdf/index.json" \
  --name github --username <username> --password "$NUGET_AUTH_TOKEN" \
  --store-password-in-clear-text
# nuget.org stays configured, so packages restore from both sources
```

---

## 🧭 Versioning & Compatibility

The binding follows [Semantic Versioning](https://semver.org/) and its major/minor version tracks the native `vanillapdf` package it wraps. Each release pins a specific native `vanillapdf` version via a NuGet dependency, so restoring the managed package always pulls a compatible runtime.

---

## 👍 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for build instructions, branch naming and coding style, and note our [Code of Conduct](CODE_OF_CONDUCT.md). Open issues and pull requests on [GitHub](https://github.com/vanillapdf/vanillapdf.net).

---

## 🔒 Security

Found a vulnerability? Please follow the coordinated-disclosure process in [SECURITY.md](SECURITY.md) rather than opening a public issue.

---

## 💬 Support

Use the [issue tracker](https://github.com/vanillapdf/vanillapdf.net/issues) for bugs and feature requests, or [contact us](https://vanillapdf.com/contact) for commercial inquiries.

---

## 📜 License

Licensed under the Apache License 2.0 — see [LICENSE.txt](LICENSE.txt). Third-party components bundled with the native runtime are listed in [NOTICE.md](NOTICE.md).
