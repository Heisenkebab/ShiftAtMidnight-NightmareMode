# thunderstore/

Source files for the Thunderstore package. Built with:

```
dotnet build -c Release -t:ThunderstorePackage
```

Output lands in `artifacts/NIGHTMAREMODE-<version>.zip`, ready to upload.

## Files

- `manifest.json.in` — template for `manifest.json`. `{VERSION}` is replaced with
  `<Version>` from `NIGHTMAREMODE.csproj` at package time, so the manifest version
  and `PluginInfo.PLUGIN_VERSION` can never drift. Bump `<Version>` to release.
- `icon.png` — **not committed yet, you need to make this.** Must be exactly
  256x256 and a real PNG; Thunderstore rejects anything else. Transparency is
  fine, but keep a clear border so it reads on both light and dark themes.

## Editing the manifest template

Keep it **ASCII-only**. It is written out with an ASCII encoder specifically to
avoid emitting a UTF-8 BOM, which breaks strict JSON parsers. If you need a
non-ASCII character, use a JSON `\uXXXX` escape instead of the literal.

Constraints Thunderstore enforces:

- `name` — only `a-zA-Z0-9_`
- `description` — max 250 characters
- `version_number` — strict `Major.Minor.Patch`
- `website_url` — must be present, use `""` if unused
- `dependencies` — exact `Team-Package-Version` strings

## Before the first upload

`dependencies` currently lists the generic `BepInEx-BepInExPack-5.4.2100`, which
is the **Mono** pack and is almost certainly wrong for this mod — Nightmare Mode
targets BepInEx 6 IL2CPP. Open the Shift At Midnight page on Thunderstore and
copy the exact identifier of the BepInEx pack that players actually install for
this game, then replace that entry.

`ModSettingsMenu` is a `SoftDependency` in `Plugin.cs`, so it deliberately is not
listed here — mention it in the package README as optional instead.
