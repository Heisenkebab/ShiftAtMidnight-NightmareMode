---
name: cut-release
description: Prepare a release of Nightmare Mode — bump the version, write the CHANGELOG entry, update the README if features changed, commit as "docs: release x.x.x", create the git tag, and produce the GitHub release notes. Use whenever the user asks to "update the files for a new patch/minor/major version", "cut a release", "prep a release", or "bump the version" after finishing a feature or patch.
---

# Cut a release

Prepares everything needed to publish a new version. Stops at the tag — pushing and
uploading to Thunderstore stay the user's call.

Takes an optional bump level: `patch`, `minor`, or `major`. If the user didn't say
which, infer it from the changes (see step 2) and state your choice before editing.

## The one rule that matters

Thunderstore always serves the **highest version ever uploaded**. A mistyped version
permanently buries every release after it. Get the number right before tagging.

## Steps

### 1. Survey what shipped

```
git describe --tags --abbrev=0      # last released tag
git log <last-tag>..HEAD --oneline  # what's landed since
git status --short                  # must be clean of source changes
```

If there are uncommitted source changes, stop and tell the user — commit the feature
first, then release. Untracked files unrelated to the release are fine to leave alone.

### 2. Pick the version

Current version is `<Version>` in `NIGHTMAREMODE.csproj`. This is the **single source
of truth** — it feeds both `PluginInfo.PLUGIN_VERSION` and the Thunderstore manifest's
`version_number`. There is no other place to edit.

Semver, judged by player-facing impact:

- **patch** — bug fixes, balance corrections to existing features, no new config keys.
- **minor** — a new patch/feature, a new config section or key, new behaviour a player
  would notice and could turn off.
- **major** — a config key removed or renamed, a default changed in a way that breaks
  existing saves or configs, or a dependency bump players must act on.

Changing a _default_ is a patch or minor, not major — BepInEx only writes defaults into
the config on first run, so existing players keep their old value. Say so in the
changelog when it happens (1.0.1's quota entry is the model).

### 3. Bump the csproj

Edit `<Version>` in `NIGHTMAREMODE.csproj`. Nothing else in that file changes.

### 4. Write the CHANGELOG entry

`CHANGELOG.MD` follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

- Turn `## [Unreleased]` content into `## [X.Y.Z] - YYYY-MM-DD` using today's date, and
  leave a fresh `## [Unreleased]` above it containing `Nothing yet.`
- If Unreleased says `Nothing yet.`, write the entry from the commit log instead.
- Group under `### Added` / `### Changed` / `### Fixed` / `### Removed`. Only the
  headings you actually need.
- Entries are `- **Feature** — prose`, where **Feature** matches the config section name
  the player sees in-game. Write for a player, not a developer: what changed in play,
  what the new default is, and what they'd do about it. Mention the config key path only
  when they might need to edit it by hand.
- Wrap at roughly 100 characters, matching the existing file.
- Update the link refs at the bottom: repoint `[Unreleased]` to compare from the new tag
  to `HEAD`, and add a `[X.Y.Z]` compare link from the previous tag. Base URL is
  `https://github.com/Heisenkebab/ShiftAtMidnight-NightmareMode`.

### 5. Update the README if — and only if — it's now wrong

`README.MD` describes features in prose and names some defaults ("multiplied by 2x").
Check the **Features** list against what changed and fix anything now stale. Add a bullet
for a genuinely new feature. Don't restate the changelog there, and don't touch
**Planned**, **Requirements**, or **Installation** unless the release actually changed them.

### 6. Commit and tag

The commit message is always `docs: release X.Y.Z` — no scope, no body, matching every
previous release commit.

```
git commit -m "docs: release X.Y.Z"
git tag -a vX.Y.Z -m "vX.Y.Z"
```

The commit touches exactly `NIGHTMAREMODE.csproj`, `CHANGELOG.MD`, and — when needed —
`README.MD`. Tags are annotated, prefixed `v`, message identical to the tag name.

**Do not push and do not push the tag.** Stop here.

### 7. Write the GitHub release notes

Release notes are derived from the changelog, never written twice. Copy the new version's
section body into `artifacts/notes-X.Y.Z.md` — the `### Changed` / `### Fixed` headings and
their bullets, **without** the `## [X.Y.Z] - YYYY-MM-DD` heading, since the release is
already titled and dated by GitHub.

`artifacts/` is gitignored, so the notes land beside the upload zip and never get committed.

Nothing else needs writing: Thunderstore renders the full `CHANGELOG.md` from inside the
zip on its own, and the mod page description comes from `README.MD`.

### 8. Hand off

Report the new version, then give the user the remaining steps as commands they run:

```
git push && git push origin vX.Y.Z
gh release create vX.Y.Z --title "vX.Y.Z" --notes-file artifacts/notes-X.Y.Z.md
dotnet build -c Release -t:ThunderstorePackage
```

Push the tag **before** `gh release create`. If the tag isn't on the remote yet, gh creates
a new one off the default branch (`main`) — and releases are usually cut from `dev`, so it
would tag the wrong commit.

The build produces `artifacts/NIGHTMAREMODE-X.Y.Z.zip`, ready to upload to Thunderstore.
Offer to run it if they want the zip verified before pushing — it's local and safe.
Uploading is theirs.

Never suggest GitHub's "auto-generate release notes" button. It lists commit subjects, and
`fix(spider): scale health and maxHealth once on spawn` tells a player deciding whether to
update precisely nothing. Conventional Commits are for the developer; changelog prose is
for the player.

Finally, end the response by displaying the release notes in full, as a markdown quote
block, so they can be read and copied without opening the file.
