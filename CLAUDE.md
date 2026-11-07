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

## RTTI-based versions (FM27)
FM27 (Unity 6000.3, native `game_plugin.dll`) does **not** use the flat `MainAddress`/`XorDistance` master table that FM17–FM26 use — the brute-force search found no such table. `Steam_27_0_0_0_Windows` instead resolves game classes **by name through MSVC RTTI** and finds live objects by scanning the heap for their vtable pointers:
- `VirtualMemory/Rtti/RttiImage.cs` — type descriptor → Complete Object Locator → vtable, for a class name like `CLUB_HUMAN_MANAGER_DETAILS` or `HUMAN_NON_PLAYER@db`. A class can have several vtables (multiple inheritance); the COL's sub-object offset tells them apart.
- `VirtualMemory/Rtti/ProcessHeap.cs` — whole-process qword scan and the game's length-prefixed string reader.
- `Defines/Versions/Fm27Game.cs` — the FM27 model: `TryCreate` (used by `SupportsProcess`) and `FindHumanManagers()`. Field offsets live in `Fm27Game.Layout`.
- Versions implementing `IFm27Version` have no `ObjectManager` tables (the legacy lists stay empty); data comes from `FMCore.Fm27` / `FMCore.FindHumanManagers()`. Detection is gated on the Unity engine line (`6000.3.`) so it can't claim FM26 (`6000.0.x`).
- Players: `Fm27Game.FindPlayers()` returns `Fm27PlayerInfo` records (uid, names, nation, birth date, height, CA, PA). A player is `db::PLAYING_DATA` (+0) plus `db::ACTUAL_PLAYER` at sub-object +648 (the "id" vtable, +320 from block start) and +688; instances are found by the +648 vtable and confirmed by the other two. `core.Players` is also populated with legacy `Player` entities (base = the +648 sub-object), but **only identity/name offsets are valid for FM27** — the rest of that entity's offsets are FM22-era. Prefer `FindPlayers()`.
- Positions and roles: `Fm27PlayerInfo.PositionRatings` (15 bytes at block+8, 0–20, order in `Fm27Names.Positions`: GK SW DL DC DR DM ML MC MR AML AMC AMR ST WBL WBR) and `TopRoleIds` (6 bytes at block+305: three in-possession then three out-of-possession role ids, resolved by `Fm27Names`). Helpers: `PositionsAtLeast`, `BestPositions`, `InPossessionRoles`, `OutOfPossessionRoles`, `BestInPossessionRole`/`BestOutOfPossessionRole`, `IsGoalkeeper` (GK ≥ 10). The 3+3 split is verified; that the first entry of each trio is the *best* role is **unverified** (inherited from FM Moleman's naming) — compare against FM's role view before relying on it. **Per-role star ratings are not stored** — the game computes them by running its own code (FM Moleman emulates it); don't try to read them from the player block. Validate with `OffsetFinder fm27-roles`.
- Clubs and squads: each `Fm27PlayerInfo` carries `ClubName`/`ClubAddress` (owner club, from the contract at block+488 → team pointer at contract+24, fallback +16 → `team+0x30` club → `club+0xC0` name), `CurrentTeamAddress`/`SquadType`/`Squad` (the team pointer at **block−24**; valid only if the object's vtable is `TEAM@db`; squad type is the byte at `team+40`), and `CurrentClubName`. `FindSquads()` groups players by current team. **A loan and a B-team registration look identical in the contract** (parent club both times; no parent-link field found on the club/team records), so `PlaysForOtherClub` is the fact and `IsOnLoan` / `IsWithAffiliateClub` are a heuristic (a destination club ≥8 players with ≥50% owned by one club is that club's affiliate). Small feeder sides (e.g. Young Lions) can still read as loans. ~13% of players have no club (free agents/unattached).
- `Fm27Game.Refresh()` does the single heap scan that managers and players share (~25–40 s); `LoadData()` calls it. Uid `0xFFFFFFFF` means unassigned; ~0.6% of players are unnamed generated-player slots.
- Not done yet: staff, clubs, teams and the other legacy tables for FM27.
- Debug with `FM.Pitchside.OffsetFinder`: `rtti-name`, `rtti-vtable`, `rtti-list`, `rtti-object`, `rtti-instances`, `fm27-managers`. The `rtti-*` commands also run offline with `--file <dll>`.

## Verification
- There is no test suite or linter. Verification = the project builds (`dotnet build FMScoutFramework.csproj`) and the consuming app can read a live FM process. Don't add tests that require a running game.
