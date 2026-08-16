# FMScoutFramework

Closed-source C# library that reads (and writes) the memory of a running Football Manager
process, for use by FM scouting tools. Tracks offsets for Steam builds from FM17 through FM22
(`22.4.1`).

## Building

The maintained project is `FMScoutFramework.csproj` (SDK-style, targets `netstandard2.1`),
referenced by `FMScoutFramework.slnx`:

```
dotnet build FMScoutFramework.csproj
```

The other root `.csproj` files (`FMScoutFrameworkStandard.csproj`, `FMScoutFrameworkUniversal.csproj`,
`FMSFramework.csproj`) are legacy/abandoned and not expected to build.

Mac builds additionally require the `vendor/MacProcessMemoryAPI` submodule:

```
git submodule update --init
```

## How it works

`FMCore.LoadData()` locates the running `fm` process and matches it to a known game version by
probing live memory. Once matched, in-game entities (players, clubs, staff, competitions, etc.)
are read through per-version memory offsets defined under `Defines/`.

See `CLAUDE.md` / `AGENTS.md` for architecture details and codebase conventions.

## Status

x64 only, Windows-first (Mac support via a submodule, not actively maintained). No public API
stability guarantees — this is a private library consumed by other AppCake tools.
