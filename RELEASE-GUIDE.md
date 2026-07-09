# 🧩 Vanilla.PDF .NET Release Guide

This guide describes how stable and pre-release versions of the `vanillapdf.net`
NuGet package are cut. It mirrors the release model used by the native
[vanillapdf](https://github.com/vanillapdf/vanillapdf) project.

The guiding principle: **the git tag is created last.** It only comes into
existence when the reviewed draft release is published, after every build and
validation step has passed. Nothing is tagged up front, so a failed build never
leaves a dangling tag behind.

---

## 🏷️ Creating a Release (Pre-release or Stable)

Tags are **not** created manually with `git tag` / `git push`. The `release.yml`
workflow owns tag creation end to end.

1. **Dry run.** Trigger `release.yml` via `workflow_dispatch` — from the Actions
   tab, or:

   ```bash
   gh workflow run release.yml -f tag=v2.3.0-rc.1 -f dry_run=true
   ```

   With `dry_run: true` (the default) it builds and tests the package on every
   supported OS and verifies the version, but publishes and tags nothing.

2. **Real run.** Re-run with `dry_run: false`. This builds the package for real,
   pushes it to the GitHub Packages staging feed, and creates the GitHub release
   as a **draft** — still without creating the tag.

3. **Review.** The `production` environment gate pauses the workflow. Review and
   edit the draft release body in the GitHub UI, then approve the gate.

4. **Publish.** Approving publishes the draft, which is what actually **creates
   the tag** (at the commit the workflow ran on) and binds the release to it.
   The package is then pushed to NuGet.org via OIDC trusted publishing.

> Whether a tag counts as a pre-release is derived automatically from its suffix
> (`-alpha.N`, `-beta.N`, `-rc.N`) by the `prepare` job in `release.yml`. Stable
> tags (`vX.Y.Z` with no suffix) are additionally evaluated for whether they are
> the newest version, which drives the GitHub "Latest" release flag.

---

## 📦 Version Source

`<VersionPrefix>` in `src/vanillapdf.net/vanillapdf.net.csproj` is the single
source of truth for the release version. The `verify` job fails the run if the
tag's base version (ignoring any pre-release suffix) does not match it.

So the version bump to `<VersionPrefix>` must be committed **before** triggering
a real release. The pre-release suffix is supplied only through the tag input
(e.g. `-rc.1`) and is applied to the package via `dotnet pack --version-suffix`.

---

## 🔁 Branching

- Ordinary work happens on `main`; name branches `fix/...` or `feature/...`.
- Pre-release tags (`alpha`, `beta`, `rc`) are normally cut from `main`.
- `release/x.y` branches are reserved for long-lived release lines that receive
  backports, and are matched by branch/tag protection rulesets. Do not use that
  prefix for ordinary work. See `CLAUDE.md` for the full branch-naming policy.

---

## 🧪 Workflow Summary

| Workflow | Trigger | Publishes? |
| --- | --- | --- |
| `release.yml` | `workflow_dispatch` (real releases) · `pull_request` (dry-run validation of workflow changes) | ✅ unless `dry_run: true` |
| `build-nuget.yml` | called by `release.yml`; also on pushes/PRs that touch it | ❌ builds & tests only |
| `github-release.yml` | called by `release.yml` | Creates the **draft** only; the tag is created when the draft is published |

Notes:

- `release.yml` has **no** `push: tags:` trigger. On `pull_request` it runs the
  build/verify path with a synthetic tag derived from `<VersionPrefix>`, and is
  always a dry run.
- Publishing the draft uses `BOT_TOKEN` so the new tag is accepted under the
  repository's tag-protection ruleset.
- The `staging` and `production` GitHub environments provide the human approval
  gate and scope the publishing credentials.

---

*Release notes themselves live on the
[GitHub Releases page](https://github.com/vanillapdf/vanillapdf.net/releases);
see `CHANGELOG.md`.*
