# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is
Closed-source C# library that reads/writes the memory of a running Football Manager process (`fm`) via P/Invoke, consumed by FM scouting tools. Covers FM17–FM22 Steam builds (offsets tracked up to `22.4.1`). Library only — no app entrypoint, no CI.

`AGENTS.md` predates a recent fork/modernization (`git log`: "Setup initial fork", "Update FMScoutFramework.csproj", "Update to netstandard2.1") and its **Build** section is now stale — see below for the current state. Its architecture/namespace notes are still accurate and reproduced here.

## Build
- `FMScoutFramework.slnx` is the tracked solution and points at a single project, `FMScoutFramework.csproj`.
- `FMScoutFramework.csproj` was converted to SDK-style (`Microsoft.NET.Sdk`), targets `netstandard2.1`, `UseWPF=true`, and now uses **implicit globbing** instead of an explicit `<Compile Include>` list. New `.cs` files under the tracked folders are picked up automatically — you no longer need to edit the csproj by hand for this project.
- Builds with a plain SDK build: `dotnet build FMScoutFramework.csproj`. No VS/MSBuild.exe workaround needed anymore (verified working).
- The other three root csproj files are legacy/abandoned, unchanged since the fork — do not "fix" them unless asked:
  - `FMScoutFrameworkStandard.csproj` (netstandard2.0) — duplicate-`AssemblyInfo` conflicts, references a deleted `kernel32.dll`, pulls in WPF markup code.
  - `FMScoutFrameworkUniversal.csproj` (UWP, still an explicit `<Compile Include>` list of ~167 files) — references source files not on disk and needs the VS UWP workload.
  - `FMSFramework.csproj` — empty stub, ignore.
- **Ignore `src/` and `tests/`** at the repo root: both are untracked, contain zero `.cs` files (just leftover `bin/`/`obj/` build cache and empty folders from a prior restructuring attempt), and are not part of the build. `FMScoutFramework.csproj` has explicit `<Compile Remove>` entries for stray generated files under `src/obj` and `tests/.../obj` to keep globbing from tripping over them.
- Mac builds need `git submodule update --init` (`vendor/MacProcessMemoryAPI`, compiled by an xcodebuild BeforeBuild step in the `Debug|Mac` config); irrelevant for Windows work.
- Platform code is split by `#if WINDOWS` / `#if MAC`. Trap: the `Debug|Mac` configuration defines `DEBUG;WINDOWS;` — only `Release|Mac` defines `MAC`.

## Editing rules that differ from defaults
- Namespaces do NOT follow folders. Copy a sibling file's namespace, never derive it from the path:
  - `Defines/Versions` → `FMScoutFramework.Core.Entities.GameVersions`
  - `Defines/Offsets` → `FMScoutFramework.Core.Offsets`
  - `VirtualMemory/Managers` → `FMScoutFramework.Core.Managers`
  - `VirtualMemory/ProcessMemoryAPI.cs` → `FMScoutFramework` (no `.Core`)
  - `Entities/Ingame` → `...Entities.InGame`
- Two `AssemblyInfo.cs` files exist (root and `Properties/`) but `GenerateAssemblyInfo` is now `false` and `Properties/AssemblyInfo.cs` is explicitly `<Compile Remove>`d — only the root one is currently compiled.

## Architecture (how it's wired)
- Entry: `FMCore.LoadData()` → `GameManager.findFMProcess()` opens the `fm` process via kernel32 P/Invoke (`VirtualMemory/ProcessMemoryAPI.cs`) and picks a game version by reflection: every non-interface `IIVersion` in the assembly (found via `Assembly.GetCallingAssembly().GetTypes()`) is instantiated and `SupportsProcess()` probes live memory (continents count == 7, sane in-game date, then `FileVersionInfo.ProductVersion`, e.g. `"22.4.1+1662587"`).
- Supporting a new FM build = copy the nearest `Defines/Versions/Steam_*_Windows.cs`, update its `VersionMemoryAddresses`/offsets and expected `ProductVersion`. Version classes must live in this assembly (discovery uses `Assembly.GetCallingAssembly()`).
- Entities under `Entities/Ingame/` derive from `BaseObject`; properties call `PropertyInvoker.Get/Set<T>(offset, OriginalBytes, MemoryAddress, DatabaseMode)` with offsets from classes in `Defines/Offsets/` constructed with the matched `IVersion`. `DatabaseModeEnum.Realtime` reads live process memory; `Cached` reads the `OriginalBytes` snapshot.
- Per-version static table addresses carry `[MemoryAddress(CountLength, BytesToSkip)]` attributes (`Attributes/MemoryAddressAttribute.cs`) consumed by `ObjectManager`/`GameManager`.
- `FMCore` is the public facade: construct with a `DatabaseModeEnum`, call `LoadData()`, then read the `Awards`/`Clubs`/`Players`/etc. `IEnumerable<T>` properties (backed by `ObjectManager.ObjectStore`) or `MetaData` (current in-game date, active object ID, version string).
- x64 only in practice (`PlatformTarget x64`, all pointer math is `Int64`).

## Verification
- There is no test suite or linter. Verification = the project builds (`dotnet build FMScoutFramework.csproj`) and the consuming app can read a live FM process. Don't add tests that require a running game.
