# Security Policy

Vanilla.PDF processes untrusted input — parsing, decrypting, and validating
signatures on PDF files that may be malicious. We take security issues
seriously and appreciate coordinated disclosure.

## Supported Versions

Security fixes are provided for the latest released `2.x` version of the
`vanillapdf.net` package. Older versions may not receive patches; please upgrade
to the current release before reporting.

| Version | Supported          |
| ------- | ------------------ |
| 2.x     | :white_check_mark: |
| < 2.0   | :x:                |

## Reporting a Vulnerability

**Please do not report security vulnerabilities through public GitHub issues,
pull requests, or discussions.**

Instead, use one of the private channels below:

1. **GitHub private vulnerability reporting** (preferred) — open the
   [Security tab](https://github.com/vanillapdf/vanillapdf.net/security/advisories/new)
   of this repository and choose *Report a vulnerability*.
2. **Email** — reach us through <https://vanillapdf.com/contact>.

Please include as much of the following as you can:

- The affected version(s) of `vanillapdf.net` and the target framework/runtime.
- A description of the vulnerability and its impact.
- Steps to reproduce, ideally with a minimal proof-of-concept and, where
  relevant, a sample PDF that triggers the issue.
- Any known mitigations or workarounds.

## What to Expect

- We aim to acknowledge new reports within a few business days.
- We will keep you informed as we investigate and work on a fix.
- Once a fix is available, we will coordinate a release and public disclosure,
  and credit you if you wish.

Because much of the security-sensitive logic lives in the native
[vanillapdf](https://github.com/vanillapdf/vanillapdf) engine, some reports may
be forwarded to, or fixed in, that project. We will let you know if that is the
case.

Thank you for helping keep Vanilla.PDF and its users safe.
