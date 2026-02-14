#!/bin/bash
#
# Creates GitHub labels required by the backport workflow.
# Safe to re-run — existing labels are skipped.
#
# Usage:
#   ./.github/scripts/setup-labels.sh
#
# Requires: gh CLI authenticated with repo access
#

set -e

# Release branches that need backport labels.
# Add new entries here when creating a new release branch.
RELEASE_BRANCHES=(
    "release/2.1"
    "release/2.2"
)

BACKPORT_COLOR="0052CC"
BACKPORTED_COLOR="0E8A16"

for branch in "${RELEASE_BRANCHES[@]}"; do
    echo "Setting up labels for $branch"

    gh label create "backport $branch" \
        --color "$BACKPORT_COLOR" \
        --description "Backport to $branch branch" \
        2>/dev/null || echo "  Label 'backport $branch' already exists"

    gh label create "backported $branch" \
        --color "$BACKPORTED_COLOR" \
        --description "Backported to $branch branch" \
        2>/dev/null || echo "  Label 'backported $branch' already exists"
done

echo "Done"
