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

RID=$(get_rid)
SCRIPT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)
REPO_ROOT=$(dirname "$SCRIPT_DIR")
PROJECT="$REPO_ROOT/src/vanillapdf.net/vanillapdf.net.csproj"
OUTPUT_DIR=$(mktemp -d)

echo "Publishing $PROJECT for $RID..."

dotnet publish "$PROJECT" -c Release -f netstandard2.0 -r "$RID" -o "$OUTPUT_DIR" --no-self-contained --nologo

NATIVE_DIR="$OUTPUT_DIR/runtimes/$RID/native"

if [ ! -d "$NATIVE_DIR" ]; then
    echo "Missing native directory $NATIVE_DIR" >&2
    exit 1
fi

if [ -z "$(ls -A "$NATIVE_DIR")" ]; then
    echo "Native directory $NATIVE_DIR is empty" >&2
    exit 1
fi

echo "dotnet publish produced native files in $NATIVE_DIR"

rm -rf "$OUTPUT_DIR"
