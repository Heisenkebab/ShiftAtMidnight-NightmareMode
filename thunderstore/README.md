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
- `icon.png` — the package icon. If you replace it, it must stay exactly 256x256
  and a real PNG; Thunderstore rejects anything else. Transparency is fine, but
  keep a clear border so it reads on both light and dark themes.

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

## Dependencies

`ModSettingsMenu` is a hard dependency — `Plugin.cs` declares a plain
`[BepInDependency(...)]` with no `DependencyFlags.SoftDependency`, so the plugin
will not load without it. It is correctly listed in `dependencies` as
`Ice_Box_Studio_SAM-ModSettingsMenu-1.1.0`, and the root `README.MD` lists it
under Requirements. Keep all three in sync if that ever changes.
