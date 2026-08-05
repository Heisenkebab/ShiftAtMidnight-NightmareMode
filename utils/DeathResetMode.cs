namespace NIGHTMAREMODE;

// When a death should wipe the run. A single setting rather than two bools, so
// "only one of these can be on" is impossible to violate instead of being enforced
// by a pair of SettingChanged handlers that fight each other.
internal enum DeathResetMode
{
    Never,
    AnyoneDies,
    EveryoneDies
}