# Vanilla.PDF .NET

[![Repo size](https://img.shields.io/github/repo-size/vanillapdf/vanillapdf.net?label=size)](https://github.com/vanillapdf/vanillapdf.net) [![NuGet version](https://img.shields.io/nuget/v/vanillapdf.net.svg)](https://www.nuget.org/packages/vanillapdf.net/) [![NuGet downloads](https://img.shields.io/nuget/dt/vanillapdf.net.svg)](https://www.nuget.org/packages/vanillapdf.net/) [![Nightly build](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml/badge.svg)](https://github.com/vanillapdf/vanillapdf.net/actions/workflows/nightly-nuget.yml) [![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)

The official .NET binding for the core [Vanilla.PDF](https://github.com/vanillapdf/vanillapdf) C++17 library. Exposes a high-performance API to create, inspect, edit and sign PDF documents from any .NET Standard 2.0+ application.

---

## 📋 Table of Contents

1. [Key Features](#key-features)
2. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
3. [Usage](#usage)
4. [Samples & Local Docs](#samples--local-docs)
5. [Building & Testing](#building--testing)
6. [Contributing](#contributing)
7. [Support](#support)
8. [License](#license)

---

## 🔑 Key Features
- Cross-platform: Windows, Linux, macOS (.NET Standard 2.0+)
- Native performance via a thin interop layer
- Comprehensive PDF model with digital signatures
- Lightweight with no large dependencies
- Thread-safe API for concurrent use
- CI/CD ready with GitHub Actions

---

## 🚀 Getting Started

### Prerequisites
- .NET SDK 8.0 or newer

### Installation
```bash
dotnet add package vanillapdf.net
```

---

## ✍️ Usage
```csharp
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;

using var file = PdfFile.Open("input.pdf");
file.Initialize();
using var document = PdfDocument.OpenFile(file);
using var catalog = document.GetCatalog();
ulong pageCount = catalog.GetPages().GetPageCount();
Console.WriteLine($"Pages: {pageCount}");
```

---

## 📁 Samples & Local Docs
- Browse `samples/` for practical examples.
- See the [CHANGELOG](https://github.com/vanillapdf/vanillapdf.net/blob/main/CHANGELOG.txt) and
  [LICENSE](https://github.com/vanillapdf/vanillapdf.net/blob/main/LICENSE.txt).

---

## 🚧 Building & Testing
```bash
dotnet restore
dotnet build
# from repository root to run tests
cd .. && dotnet test vanillapdf.net.sln
```

---

## 👍 Contributing
Contributions are welcome. Please open issues or pull requests on GitHub.

---

## 💬 Support
Use the [issue tracker](https://github.com/vanillapdf/vanillapdf.net/issues) or [contact us](https://vanillapdf.com/contact).

---

## 📜 License
Apache-2.0. See the [LICENSE](https://github.com/vanillapdf/vanillapdf.net/blob/main/LICENSE.txt) for details.

