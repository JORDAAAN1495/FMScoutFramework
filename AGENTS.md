# AGENTS.md

## What this is
Closed-source C# library that reads/writes the memory of a running Football Manager process (`fm`), consumed by FM scouting tools. Covers FM21–FM22 Steam builds (last commit: "Final FM2022"). Library only — no app entrypoint, no tests, no CI. README is a stub; this file is the real onboarding doc.

## Build
- No `.sln` is tracked. Four `.csproj` files at the root compile (mostly) the same sources into `FMScoutFramework.dll`:
  - `FMScoutFramework.csproj` — legacy non-SDK, .NET Framework 4.6.1, `WINDOWS` define, x64. **This is the only maintained project; build this one.**
  - `FMScoutFrameworkStandard.csproj` (netstandard2.0, SDK-style) — abandoned; verified not to compile (duplicate `AssemblyInfo` attributes from two globbed AssemblyInfo.cs files, `AssemblyVersion("1.0.*")` vs determinism, WPF `System.Windows.Markup` use in `Extensions/EnumBindingSourceExtension.cs`, and it copies a `kernel32.dll` that was deleted from the repo). Do not "fix" it unless asked.
  - `FMScoutFrameworkUniversal.csproj` (UWP) — abandoned; references `Compile` files not on disk (e.g. `Defines\Versions\Steam_21_4_0_1_Windows.cs`) and needs the VS UWP workload.
  - `FMSFramework.csproj` — empty stub, ignore.
- `dotnet build` fails on the legacy project (error MSB3644). Use VS 2022 MSBuild:
  `& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" FMScoutFramework.csproj /p:Configuration=Debug`
- If MSB3644 persists, the net461 targeting pack is missing (machines often have only 4.7.2/4.8 packs); add `/p:TargetFrameworkVersion=v4.7.2` — verified to build.

## Editing rules that differ from defaults
- `FMScoutFramework.csproj` (and the UWP one) use explicit `<Compile Include>` lists: **new .cs files must be added to the csproj by hand** or they silently won't compile. The two lists have drifted apart — don't try to sync them.
- Two `AssemblyInfo.cs` files exist (root and `Properties/`); different projects compile each. Both must stay.
- Namespaces do NOT follow folders. Copy a sibling file's namespace, never derive it from the path: `Defines/Versions` → `FMScoutFramework.Core.Entities.GameVersions`, `Defines/Offsets` → `FMScoutFramework.Core.Offsets`, `VirtualMemory/Managers` → `FMScoutFramework.Core.Managers`, `VirtualMemory/ProcessMemoryAPI.cs` → `FMScoutFramework` (no `.Core`), `Entities/Ingame` → `...Entities.InGame`.
- Platform code is split by `#if WINDOWS` / `#if MAC`. Trap: the `Debug|Mac` configuration defines `DEBUG;WINDOWS;` — only `Release|Mac` defines `MAC`.

## Architecture (how it's wired)
- Entry: `FMCore.LoadData()` → `GameManager.findFMProcess()` opens the `fm` process via kernel32 P/Invoke (`VirtualMemory/ProcessMemoryAPI.cs`) and picks a game version by reflection: every non-interface `IIVersion` in the assembly is instantiated and `SupportsProcess()` probes live memory (continents count == 7, sane in-game date, then `FileVersionInfo.ProductVersion`, e.g. `"22.4.1+1662587"`).
- Supporting a new FM build = copy the nearest `Defines/Versions/Steam_*_Windows.cs`, update its `VersionMemoryAddresses`/offsets and expected `ProductVersion`, and add the file to `FMScoutFramework.csproj`. Discovery uses `Assembly.GetCallingAssembly()`, so version classes must live in this assembly.
- Entities under `Entities/Ingame/` derive from `BaseObject`; properties call `PropertyInvoker.Get/Set<T>(offset, OriginalBytes, MemoryAddress, DatabaseMode)` with offsets from classes in `Defines/Offsets/` constructed with the matched `IVersion`. `DatabaseModeEnum.Realtime` reads live process memory; `Cached` reads the `OriginalBytes` snapshot.
- Per-version static table addresses carry `[MemoryAddress(CountLength, BytesToSkip)]` attributes (`Attributes/MemoryAddressAttribute.cs`) consumed by `ObjectManager`/`GameManager`.
- x64 only in practice (`PlatformTarget x64`, all pointer math is `Int64`).

## Verification
- There is no test suite, linter, or CI. Verification = the legacy project compiles and the consuming app can read a live FM process. Don't add tests that require a running game.
- Mac builds need `git submodule update --init` (`vendor/MacProcessMemoryAPI`, compiled by an xcodebuild BeforeBuild step in the `Debug|Mac` config); irrelevant for Windows work.
