#!/usr/bin/env bash
set -euo pipefail

get_rid() {
    local os=$(uname -s)
    local arch=$(uname -m)
    case "$os" in
        Linux)
            case "$arch" in
                x86_64) echo "linux-x64";;
                aarch64) echo "linux-arm64";;
                *) echo "Unsupported architecture $arch on Linux" >&2; return 1;;
            esac
            ;;
        Darwin)
            case "$arch" in
                x86_64) echo "osx-x64";;
                arm64) echo "osx-arm64";;
                *) echo "Unsupported architecture $arch on macOS" >&2; return 1;;
            esac
            ;;
        MINGW*|MSYS*|CYGWIN*|Windows_NT)
            case "$arch" in
                x86_64|amd64) echo "win-x64";;
                i686|i386) echo "win-x86";;
                *) echo "Unsupported architecture $arch on Windows" >&2; return 1;;
            esac
            ;;
        *)
            echo "Unsupported OS $os" >&2
            return 1
            ;;
    esac
}

get_exe_name() {
    local os=$(uname -s)
    case "$os" in
        MINGW*|MSYS*|CYGWIN*|Windows_NT)
            echo "vanillapdf.net.testapp.exe"
            ;;
        *)
            echo "vanillapdf.net.testapp"
            ;;
    esac
}

RID=$(get_rid)
EXE_NAME=$(get_exe_name)
SCRIPT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
REPO_ROOT=$(dirname "$SCRIPT_DIR")
PROJECT="$REPO_ROOT/src/vanillapdf.net.testapp/vanillapdf.net.testapp.csproj"
OUTPUT_DIR=$(mktemp -d)

echo "Publishing Native AOT for $RID..."

# Publish with AOT (PublishAot is already set in the project file for net10.0)
dotnet publish "$PROJECT" -c Release -r "$RID" -f net10.0 -o "$OUTPUT_DIR" --nologo

# Check that the executable was created
EXE_PATH="$OUTPUT_DIR/$EXE_NAME"
if [ ! -f "$EXE_PATH" ]; then
    echo "AOT executable not found at $EXE_PATH" >&2
    ls -la "$OUTPUT_DIR"
    exit 1
fi

# Show the executable size
EXE_SIZE=$(ls -lh "$EXE_PATH" | awk '{print $5}')
echo "AOT executable size: $EXE_SIZE"

# Run the executable to verify it works
echo "Running AOT executable..."
if "$EXE_PATH"; then
    echo "Native AOT validation successful!"
else
    echo "Native AOT executable failed to run" >&2
    exit 1
fi

rm -rf "$OUTPUT_DIR"
