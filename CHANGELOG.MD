# Changelog

All notable changes to Nightmare Mode are documented here.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project
adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

Nothing yet.

## [1.0.2] - 2026-08-13

### Changed

- **Packaging** — The Thunderstore zip now includes `CHANGELOG.md`, so release notes are readable
  from the mod page instead of only here.

### Fixed

- **Spiders** — Health scaling now actually works. The multiplier was only ever applied to a
  spider's current health, and with maximum health left unscaled the increase never stuck, so
  spiders went down in the usual number of hits no matter what you set. Both values are now scaled
  once when the spider spawns. If you had already turned the multiplier up to compensate, expect
  spiders to be considerably tougher than they were.

## [1.0.1] - 2026-08-13

### Changed

- **Quota** — The default quota multiplier is now 2x, down from 3x. This only affects fresh
  installs: BepInEx writes the value into your config on first run, so existing players keep 3x
  until they change it or delete the `Quota Scaling` line from
  `BepInEx/config/nightmaremode.cfg`

## [1.0.0] - 2026-08-10

Initial release. Every value below is exposed in the config, and the section names match the
feature names, so anything you want to dial back is easy to find.

### Added

- **General** — A master `Enabled` toggle that turns every patch off at once, without uninstalling
  the mod.
- **Quota** — Daily store quotas are multiplied, starting from Day 1 and applying to every day
  after. Default 3x, adjustable from 1x to 10x. The scaling is applied once on the host, so
  clients never compound it.
- **Thieves** — Thieves can spawn from the very first day instead of only appearing later on, on a
  configurable interval (default: a 50% chance every 20 seconds). How much money you get back when
  a thief is caught is configurable too, and defaults to nothing.
- **Damage** — Every hit the player takes is multiplied. Default 3x.
- **Spiders** — Spider health (default 4x) and movement speed (default 2x) are both scaled up.
- **Customers** — Every customer gets a patience timer, so there are no more infinite-patience
  shoppers. Default 60 seconds, adjustable from 5 to 180.
- **Boxes** — Restock boxes hold 5 items instead of 15 by default, adjustable from 1 to 15. The
  Day 1 tutorial shelf is stripped by the matching amount so the tutorial still works out.
- **Vents** — You get pushed back out of vents much faster, so they are no longer a safe hiding
  spot. Default 2.30 against the game's 1.15. This one is handled purely client-side, so each
  player's own push-out speed applies to them rather than the host's.
- **Game reset** — Failure wipes the save entirely instead of just costing you the day. Dying is
  configurable between never resetting, a single death ending the run for everyone (the default),
  and only a full team wipe ending it. Ending a day without meeting the quota also wipes the run,
  and can be turned off separately. When a run ends, you are returned to the main menu with a
  NIGHTMARE MODE panel explaining what killed it.
- **Packaging** — A `ThunderstorePackage` build target that produces an upload-ready zip from a
  single `<Version>` value in the csproj.

[Unreleased]: https://github.com/Heisenkebab/ShiftAtMidnight-NightmareMode/compare/v1.0.2...HEAD
[1.0.2]: https://github.com/Heisenkebab/ShiftAtMidnight-NightmareMode/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Heisenkebab/ShiftAtMidnight-NightmareMode/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Heisenkebab/ShiftAtMidnight-NightmareMode/releases/tag/v1.0.0
