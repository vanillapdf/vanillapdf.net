# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the official .NET binding for the [Vanilla.PDF](https://github.com/vanillapdf/vanillapdf) C++17 library. It provides a high-performance API for creating, inspecting, editing, and signing PDF documents via P/Invoke interop with native libraries.

## Build and Test Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build src/vanillapdf.net.sln

# Run all tests
dotnet test src/vanillapdf.net.sln

# Run tests with Release configuration (used in CI)
dotnet test src/vanillapdf.net.sln --configuration Release

# Run a single test by name
dotnet test src/vanillapdf.net.sln --filter "FullyQualifiedName~TestMethodName"

# Verify native library publishing (cross-platform script)
./scripts/test_dotnet_publish.sh
```

## Architecture

### Native Interop Layer

The library wraps a native C++17 library (`vanillapdf` NuGet package) using P/Invoke. Key components:

- **`Utils/LibraryInstance.cs`**: Manages native library loading per platform (Windows/Linux/macOS). Provides `GetFunction<T>()` to resolve native symbols at runtime.
- **`Utils/PlatformUtils.cs`**: Platform-specific implementations for loading native libraries (`WindowsPlatformUtils`, `LinuxPlatformUtils`, `MacPlatformUtils`).
- **`Utils/SafeHandles/`**: SafeHandle wrappers for native pointers, ensuring proper cleanup.

### Object Hierarchy

All managed objects inherit from `PdfUnknown` (in `PdfUtils/`), which implements COM-style reference counting (`AddRef`/`Release`) and `IDisposable`. Objects must be disposed to release native resources.

### Namespace Organization

- **`vanillapdf.net.PdfSyntax`**: Low-level PDF structure (`PdfFile`, `PdfObject`, `PdfDictionaryObject`, `PdfArrayObject`, etc.). Use `PdfFile.Open()` to open files and `Initialize()` to parse xref tables.
- **`vanillapdf.net.PdfSemantics`**: High-level PDF model (`PdfDocument`, `PdfCatalog`, `PdfPage`, `PdfAnnotation`, etc.). Built on top of syntax layer.
- **`vanillapdf.net.PdfContents`**: Content stream parsing (`PdfContentParser`, `PdfContentInstruction`, text/font operations).
- **`vanillapdf.net.PdfUtils`**: Utilities including streams (`PdfInputStream`, `PdfOutputStream`), buffers, signing keys, and logging.

### Typical Usage Pattern

```csharp
using var file = PdfFile.Open("input.pdf");
file.Initialize();  // Required: parses cross-reference tables
using var document = PdfDocument.OpenFile(file);
using var catalog = document.GetCatalog();
```

### Test Project

Tests use NUnit (`vanillapdf.net.nunit` project). The `OneTimeSetup.cs` initializes the native library via `LibraryInstance.Initialize(TestContext.CurrentContext.TestDirectory)`. Test resources are in `src/vanillapdf.net.nunit/Resources/`.

## Target Frameworks

- Main library: .NET Standard 2.0 and .NET 8.0 (multi-targeting)
- Test project: .NET 8.0, .NET 9.0, and .NET 10.0

## Coding Style

- **Lambda functions**: Keep lambdas to 3 lines or fewer. Never use inline lambdas with multiple parameters - extract to a named method instead.
