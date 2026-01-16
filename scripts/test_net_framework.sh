#!/usr/bin/env bash
set -euo pipefail

# This script tests the .NET Framework (net481) build of the testapp.
# It only runs on Windows since .NET Framework is Windows-only.

SCRIPT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
REPO_ROOT=$(dirname "$SCRIPT_DIR")
PROJECT="$REPO_ROOT/src/vanillapdf.net.testapp/vanillapdf.net.testapp.csproj"
TEST_PDF="$REPO_ROOT/src/vanillapdf.net.nunit/Resources/minimalist.pdf"

# Check if running on Windows
case "$(uname -s)" in
    MINGW*|MSYS*|CYGWIN*|Windows_NT)
        echo "Running .NET Framework test on Windows..."
        ;;
    *)
        echo "Skipping .NET Framework test (Windows-only)"
        exit 0
        ;;
esac

# Build the testapp for net481
echo "Building testapp for net481..."
dotnet build "$PROJECT" -c Release -f net481 --nologo

# Run the testapp
EXE_PATH="$REPO_ROOT/src/vanillapdf.net.testapp/bin/Release/net481/vanillapdf.net.testapp.exe"
if [ ! -f "$EXE_PATH" ]; then
    echo "net481 executable not found at $EXE_PATH" >&2
    exit 1
fi

echo "Running net481 testapp..."
if "$EXE_PATH" "$TEST_PDF"; then
    echo ".NET Framework validation successful!"
else
    echo ".NET Framework executable failed to run" >&2
    exit 1
fi
