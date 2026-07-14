# Contributing to Vanilla.PDF .NET

Thanks for your interest in improving the official .NET binding for
[Vanilla.PDF](https://github.com/vanillapdf/vanillapdf)! This document covers
how to build, test, and submit changes.

By participating in this project you agree to abide by our
[Code of Conduct](CODE_OF_CONDUCT.md).

## Ways to Contribute

- **Report bugs** and request features via the
  [issue tracker](https://github.com/vanillapdf/vanillapdf.net/issues).
- **Improve documentation** — README, XML doc comments, samples.
- **Submit code** — bug fixes and features via pull requests.

For questions about the underlying PDF engine (parsing, signing, encryption
internals), note that this repository only contains the managed binding; the
native implementation lives in the
[vanillapdf](https://github.com/vanillapdf/vanillapdf) repository.

## Prerequisites

- The **.NET SDK 8.0 or newer**.
- No native toolchain is required — the platform-specific native libraries are
  pulled in through the `vanillapdf` NuGet dependency.

## Building & Testing

```bash
# restore dependencies
dotnet restore

# build the solution
dotnet build src/vanillapdf.net.sln

# run all tests (Release mirrors CI)
dotnet test src/vanillapdf.net.sln --configuration Release

# run a single test by name
dotnet test src/vanillapdf.net.sln --filter "FullyQualifiedName~TestMethodName"

# verify the native libraries publish correctly across runtimes
./scripts/test_dotnet_publish.sh
```

New behavior should come with NUnit tests in `src/vanillapdf.net.nunit`. Test
resources live in `src/vanillapdf.net.nunit/Resources/`.

## Branching

- Name work branches `fix/...` or `feature/...` (for example, `fix/version-prefix`
  or `feature/inline-image-data`). This includes release-prep chores such as
  version bumps.
- **Do not** use the `release/*` prefix for ordinary work. It is reserved for
  long-lived release branches that receive backports (for example, `release/2.2`).
  Branch-protection rulesets and CI `push` triggers match that prefix, so an
  ordinary branch named `release/...` is wrongly subjected to release-branch
  protections and runs its checks twice.

## Coding Style

Match the surrounding code. The house style is:

- Opening braces `{` go on the **same line** for `for`, `foreach`, `if`/`else`,
  and `using` statements.
- Prefer `using` **declarations** (without braces) over `using` blocks when they
  simplify control flow:

  ```csharp
  // Preferred
  using var file = PdfFile.Open("input.pdf");
  using var document = PdfDocument.OpenFile(file);
  ```

- Keep lambdas to three lines or fewer. Never write inline multi-parameter
  lambdas — extract a named method instead.
- Always use braces for multi-line blocks. The single-line form (without braces)
  is only for three or more sequential guard conditions:

  ```csharp
  // Standard — always use braces
  if (condition) {
      return null;
  }

  // Single-line, only for 3+ sequential guards
  if (conditionA) return null;
  if (conditionB) return null;
  if (conditionC) return null;
  ```

- Public API members should carry XML documentation comments (`GenerateDocumentationFile`
  is enabled, so missing docs surface as build warnings).

## Interop Guidelines

- P/Invoke declarations are centralized in `Interop/NativeMethods.*.cs`. Use
  `DllImport` for the .NET Standard 2.0 target and `LibraryImport` for .NET 7+.
- Native handles are wrapped in `SafeHandle` types under `Utils/SafeHandles/`.
- Managed objects are `IDisposable` wrappers around a `SafeHandle`; there is no
  common base class and no managed reference counting. `Dispose()` disposes the
  handle, whose `ReleaseHandle` calls the native release function.
- Any object returned across the interop boundary owns a handle. A wrapper that
  hands out derived objects must let the caller dispose them — dropping a handle
  on the floor leaks native memory.

## Submitting a Pull Request

1. Fork the repository and create a `fix/...` or `feature/...` branch.
2. Make your change, add tests, and ensure `dotnet test` passes in Release.
3. Keep the change focused; unrelated cleanups belong in separate PRs.
4. Open the pull request against `main` with a clear description of the change
   and its motivation.

## License

By contributing, you agree that your contributions will be licensed under the
[Apache License 2.0](LICENSE.txt), consistent with the rest of the project.
