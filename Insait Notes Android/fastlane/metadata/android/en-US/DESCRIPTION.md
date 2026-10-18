# Insait Notes — F-Droid Listing Description

Store listing text for **Insait Notes** (Android), ready to paste into the
F-Droid / Fastlane metadata fields. The plain-text files in this folder
(`title.txt`, `short_description.txt`, `full_description.txt`) already contain
the values below.

| Field | File | Limit |
|-------|------|-------|
| Title | `title.txt` | 30 characters |
| Short description | `short_description.txt` | 80 characters |
| Full description | `full_description.txt` | 4000 characters |
| Changelog | `changelogs/<versionCode>.txt` | - |

## Title

```text
Insait Notes
```

## Short description

```text
Fast, private, offline notepad with tabs, pinning and 5 languages.
```

## Full description

```text
Insait Notes is a lightweight notepad for Android built around tabs.
Keep everything you need in one place — one tab per topic — and switch
between them instantly.

Feature highlights:

• Tab-based notes - organise thoughts as tabs instead of separate files.
• Tab Center - a searchable overview of all tabs in a card grid.
• Pin important tabs - long-press a card to pin it to the top.
• Delete with undo - an accidental delete can be undone from the snackbar.
• Search across tabs - filter by tab name or by note content.
• Autosave - notes are saved shortly after you stop typing.
• Five languages - English, Russian, German, Ukrainian and Turkish.
• Lightweight and fast - native .NET MAUI app with a clean, simple UI.

Privacy first:

• 100% offline - the app never connects to the internet.
• No accounts, no ads, no analytics, no third-party tracking SDKs.
• No special permissions - notes are stored only in the app's own
  private storage folder.
• Your notes stay on your device and are never uploaded anywhere.

Insait Notes is free and open source (MIT licensed). The source code,
including the build recipe, is available in the project repository.
```

## Changelog (versionCode 1 / versionName 1.0.1)

```text
First release for F-Droid.
- Tab-based note editor with autosave.
- Tab Center with search, pinning and delete-with-undo.
- Five interface languages (EN, RU, DE, UA, TR).
- Fully offline, no permissions, no tracking.
```

## F-Droid / store notes

* **License:** MIT (see the repository `LICENSE`). F-Droid only distributes
  free and open source software, so a FOSS license is required.
* **Permissions:** none. The app is fully offline; no network or storage
  permissions are declared in `AndroidManifest.xml`.
* **Anti-features:** none (no ads, no tracking, no non-free dependencies).
* **Package id:** `com.olehkurylo.insaitnotes`.
* **Screenshots:** drop PNG/JPG files into
  `images/phoneScreenshots/` (see the README in that folder).
