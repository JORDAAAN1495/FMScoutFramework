using System.Diagnostics;
using FM.Pitchside.Core;
using FM.Pitchside.Core.Converters;
using FM.Pitchside.Core.VirtualMemory.Managers;

// Live memory scanner used to help locate the offsets needed for a new FM version class
// (Defines/Versions/Steam_*_Windows.cs). Run this while the target FM build is running.
//
// Workflow:
//   1. `find-continents` to brute-force a MainAddress/XorDistance pair whose Continent
//      table resolves to exactly 7 entries (the same invariant SupportsProcess() checks).
//   2. `find-date` to locate the CurrentDateTime RVA, cross-checked by eye against the
//      date shown in the FM UI.
//   3. `check`/`peek` to manually inspect and confirm a specific candidate.
//
// Finding the Player/Staff/PlayerStaff/HumanManager pointer-table addresses and the
// per-person struct offsets is a separate, more manual step not covered here.

if (args.Length == 0)
{
    PrintUsage();
    return;
}

var command = args[0].ToLowerInvariant();
var rest = args.Skip(1).ToArray();

var (process, _) = Attach();
if (process == null)
{
    Console.WriteLine("Could not find a running 'fm' process. Start Football Manager and try again.");
    return;
}

var moduleName = GetOption(rest, "--module");
var targetModule = ResolveModule(process, moduleName);
if (targetModule == null)
{
    Console.WriteLine($"Could not find a loaded module matching '{moduleName}'. Loaded modules:");
    foreach (ProcessModule m in process.Modules)
    {
        Console.WriteLine($"  {m.ModuleName}  (0x{m.ModuleMemorySize:X})");
    }
    return;
}

long baseAddress = targetModule.BaseAddress.ToInt64();
var moduleSize = targetModule.ModuleMemorySize;
Console.WriteLine($"Attached to PID {process.Id} - Module={targetModule.ModuleName} - BaseAddress=0x{baseAddress:X} - ModuleSize=0x{moduleSize:X} ({moduleSize / 1024 / 1024} MB)");
Console.WriteLine($"ProductVersion: {ProcessManager.fmProcess?.VersionDescription}");
Console.WriteLine();

switch (command)
{
    case "info":
        break;

    case "find-continents":
        RunFindContinents(baseAddress, moduleSize, rest);
        break;

    case "find-date":
        RunFindDate(baseAddress, moduleSize, rest);
        break;

    case "check":
        RunCheck(baseAddress, rest);
        break;

    case "peek":
        RunPeek(rest);
        break;

    case "find-string-field":
        RunFindStringField(rest);
        break;

    case "scan-arrays":
        RunScanArrays(rest, baseAddress, moduleSize);
        break;

    case "identify-slots":
        RunIdentifySlots(baseAddress, moduleSize, rest);
        break;

    default:
        PrintUsage();
        break;
}

static ProcessModule? ResolveModule(Process process, string? name)
{
    if (string.IsNullOrWhiteSpace(name))
    {
        return process.MainModule;
    }

    foreach (ProcessModule m in process.Modules)
    {
        if (string.Equals(m.ModuleName, name, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(m.ModuleName, name + ".dll", StringComparison.OrdinalIgnoreCase))
        {
            return m;
        }
    }

    return null;
}

static (Process? process, long baseAddress) Attach()
{
    var gm = new GameManager { logger = FMCore.logger };
    gm.findFMProcess();

    var fmProcess = ProcessManager.fmProcess;
    if (fmProcess?.Process == null)
    {
        return (null, 0);
    }

    return (fmProcess.Process, fmProcess.BaseAddress);
}

static void RunFindContinents(long baseAddress, long moduleSize, string[] args)
{
    var xors = ParseHexList(GetOption(args, "--xors"),
        defaultValue: new long[] { 0x68, 0x88, 0x58, 0x60, 0x70, 0x78, 0x80, 0x90, 0x98, 0xA0 });
    long tableOffset = ParseHex(GetOption(args, "--offset"), 0x30);
    long from = ParseHex(GetOption(args, "--from"), 0x0);
    long to = ParseHex(GetOption(args, "--to"), moduleSize);

    Console.WriteLine($"Downloading a snapshot of the module image (0x{to - from:X} bytes)...");
    var snapshot = ReadModuleSnapshot(baseAddress + from, to - from);

    Console.WriteLine($"Scanning {(to - from) / 8:N0} candidate RVAs x {xors.Length} xor value(s) (table offset 0x{tableOffset:X}) for a count of 7...");

    int candidatesChecked = 0;
    int hits = 0;
    for (long i = 0; i + tableOffset + 8 <= snapshot.LongLength; i += 8)
    {
        long p1 = BitConverter.ToInt64(snapshot, (int)(i + tableOffset));
        if (!LooksLikePointer(p1)) continue;

        candidatesChecked++;

        foreach (var xor in xors)
        {
            long p2 = ProcessManager.ReadInt64(p1 + xor);
            if (!LooksLikePointer(p2)) continue;

            long startPtr = ProcessManager.ReadInt64(p2);
            long endPtr = ProcessManager.ReadInt64(p2 + 8);
            if (startPtr == 0 || endPtr < startPtr) continue;

            long count = (endPtr - startPtr) / 8;
            if (count == 7)
            {
                hits++;
                long rva = from + i;
                Console.WriteLine($"  MATCH  MainAddress=0x{rva:X}  XorDistance=0x{xor:X}");
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {candidatesChecked:N0} plausible pointer(s) live-checked, {hits} match(es) found.");
    if (hits == 0)
    {
        Console.WriteLine("No matches. Try widening --xors, or the Continent slot may have moved - retry with --offset.");
    }
    else if (hits > 1)
    {
        Console.WriteLine("Multiple candidates matched by coincidence - use `check` on each together with `find-date` to disambiguate.");
    }
}

static void RunFindDate(long baseAddress, long moduleSize, string[] args)
{
    int yearMin = (int)ParseHexOrDec(GetOption(args, "--year-min"), 2024);
    int yearMax = (int)ParseHexOrDec(GetOption(args, "--year-max"), 2032);
    long from = ParseHex(GetOption(args, "--from"), 0x0);
    long to = ParseHex(GetOption(args, "--to"), moduleSize);

    Console.WriteLine($"Downloading a snapshot of the module image (0x{to - from:X} bytes)...");
    var snapshot = ReadModuleSnapshot(baseAddress + from, to - from);

    Console.WriteLine($"Scanning for a date-shaped value with year in [{yearMin}, {yearMax}]...");
    Console.WriteLine();

    int hits = 0;
    for (long i = 0; i + 4 <= snapshot.LongLength; i += 2)
    {
        int days = BitConverter.ToInt16(snapshot, (int)i) & 0x1FF;
        int year = BitConverter.ToInt16(snapshot, (int)(i + 2));
        if (days is > 0 and <= 366 && year >= yearMin && year <= yearMax)
        {
            var dt = DateConverter.FromFmDateTime(days - 1, year);
            long rva = from + i;
            Console.WriteLine($"  RVA=0x{rva:X}  -> {dt:yyyy-MM-dd} (raw days={days}, year={year})");
            hits++;
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {hits} candidate(s) found. Compare against the in-game date shown in FM26 to pick the right one.");
}

static void RunIdentifySlots(long moduleBaseAddress, long moduleSize, string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: identify-slots <mainAddressHex> <xorHex> [--offsets 0x10,0x18,...]");
        return;
    }

    long mainAddress = ParseHex(args[0], 0);
    long xor = ParseHex(args[1], 0);
    var offsets = ParseHexList(GetOption(args, "--offsets"), new long[]
    {
        0x10, 0x18, 0x20, 0x28, 0x30, 0x38, 0x40, 0x48, 0x50, 0x58, 0x60, 0x68, 0x70,
        0x78, 0x80, 0x88, 0x90, 0x98, 0xA0, 0xA8, 0xB0, 0xB8, 0xC0, 0xC8
    });

    Console.WriteLine($"Identifying {offsets.Length} table slot(s) by element[0]'s vtable...");
    Console.WriteLine();

    foreach (var offset in offsets)
    {
        long p1 = ProcessManager.ReadInt64(moduleBaseAddress + mainAddress + offset);
        long p2 = ProcessManager.ReadInt64(p1 + xor);
        long startPtr = ProcessManager.ReadInt64(p2);
        long endPtr = ProcessManager.ReadInt64(p2 + 8);
        if (startPtr == 0 || endPtr < startPtr)
        {
            Console.WriteLine($"  offset 0x{offset:X}: (no valid array)");
            continue;
        }

        long count = (endPtr - startPtr) / 8;
        string verdict = DescribeAsPointerArray(startPtr, moduleBaseAddress, moduleSize);
        Console.WriteLine($"  offset 0x{offset:X}: count={count}  {verdict}");
    }
}

static void RunCheck(long baseAddress, string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: check <mainAddressHex> <xorHex> [--offset 0x30]");
        return;
    }

    long mainAddress = ParseHex(args[0], 0);
    long xor = ParseHex(args[1], 0);
    long tableOffset = ParseHex(GetOption(args, "--offset"), 0x30);

    long p1 = ProcessManager.ReadInt64(baseAddress + mainAddress + tableOffset);
    long p2 = ProcessManager.ReadInt64(p1 + xor);
    long startPtr = ProcessManager.ReadInt64(p2);
    long endPtr = ProcessManager.ReadInt64(p2 + 8);
    long count = startPtr == 0 ? 0 : (endPtr - startPtr) / 8;

    Console.WriteLine($"p1 (MainAddress+offset) = 0x{p1:X}");
    Console.WriteLine($"p2 (p1+xor)             = 0x{p2:X}");
    Console.WriteLine($"array bounds            = 0x{startPtr:X} .. 0x{endPtr:X}");
    Console.WriteLine($"count                   = {count}{(count == 7 ? "  <-- matches Continent!" : "")}");
}

static void RunPeek(string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: peek <addressHex> [count]");
        return;
    }

    long address = ParseHex(args[0], 0);
    int count = args.Length > 1 && int.TryParse(args[1], out var c) ? c : 8;

    for (int i = 0; i < count; i++)
    {
        long value = ProcessManager.ReadInt64(address + i * 8);
        Console.WriteLine($"0x{address + i * 8:X}: 0x{value:X16}  ({value})");
    }
}

static void RunFindStringField(string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: find-string-field <objectAddressHex> <text> [--range 0x1000] [--hops 1] [--range2 0x800]");
        return;
    }

    long address = ParseHex(args[0], 0);
    string text = args[1];
    long range = ParseHex(GetOption(args, "--range"), 0x1000);
    int hops = (int)ParseHexOrDec(GetOption(args, "--hops"), 1);
    long range2 = ParseHex(GetOption(args, "--range2"), 0x800);

    if (hops <= 1)
    {
        Console.WriteLine($"Scanning 1 hop from 0x{address:X} (range 0x{range:X}) for \"{text}\"...");
        Console.WriteLine();

        int hits = ScanForStringPointers(address, range, text, (offset, ptr, value) =>
            Console.WriteLine($"  MATCH  +0x{offset:X} -> 0x{ptr:X} = \"{value}\""));

        Console.WriteLine();
        Console.WriteLine($"Done. {hits} match(es).");
        return;
    }

    Console.WriteLine($"Scanning 2 hops from 0x{address:X} (range 0x{range:X}, then 0x{range2:X} per candidate sub-object) for \"{text}\"...");
    Console.WriteLine();

    var candidates = CollectPointerSlots(address, range);
    Console.WriteLine($"{candidates.Count} candidate sub-object pointer(s) to check...");
    Console.WriteLine();

    int totalHits = 0;
    foreach (var (offset, subObject) in candidates)
    {
        totalHits += ScanForStringPointers(subObject, range2, text, (offset2, ptr, value) =>
            Console.WriteLine($"  MATCH  +0x{offset:X} -> [0x{subObject:X}] +0x{offset2:X} -> 0x{ptr:X} = \"{value}\""));
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {totalHits} match(es) across {candidates.Count} candidate(s).");
}

static List<(long offset, long value)> CollectPointerSlots(long address, long range)
{
    long from = address - range / 2;
    var snapshot = ReadModuleSnapshot(from, range);
    var result = new List<(long, long)>();

    for (long i = 0; i + 8 <= snapshot.LongLength; i += 8)
    {
        long ptr = BitConverter.ToInt64(snapshot, (int)i);
        if (!LooksLikePointer(ptr)) continue;

        result.Add((from + i - address, ptr));
    }

    return result;
}

static int ScanForStringPointers(long address, long range, string text, Action<long, long, string> onMatch)
{
    long from = address - range / 2;
    var snapshot = ReadModuleSnapshot(from, range);

    int hits = 0;
    for (long i = 0; i + 8 <= snapshot.LongLength; i += 8)
    {
        long ptr = BitConverter.ToInt64(snapshot, (int)i);
        if (!LooksLikePointer(ptr)) continue;

        int len = ProcessManager.ReadInt32(ptr);
        if (len <= 0 || len > 500) continue;

        byte[] bytes = ProcessManager.ReadProcessMemory(ptr + 4, (uint)len);
        string value;
        try
        {
            value = System.Text.Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            continue;
        }

        if (value.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
        {
            hits++;
            onMatch(from + i - address, ptr, value);
        }
    }

    return hits;
}

static void RunScanArrays(string[] args, long moduleBaseAddress, long moduleSize)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: scan-arrays <objectAddressHex> [--range 0x200] [--xors 0x0] [--min 1] [--max 5000]");
        Console.WriteLine("Pass --module game_plugin.dll so element pointers can be checked against its vtables.");
        return;
    }

    long address = ParseHex(args[0], 0);
    long range = ParseHex(GetOption(args, "--range"), 0x200);
    var xors = ParseHexList(GetOption(args, "--xors"), new long[] { 0x0 });
    long minCount = ParseHexOrDec(GetOption(args, "--min"), 1);
    long maxCount = ParseHexOrDec(GetOption(args, "--max"), 5000);

    Console.WriteLine($"Scanning fields of 0x{address:X} (range 0x{range:X}) x {xors.Length} xor value(s) for array bounds (count {minCount}-{maxCount})...");
    Console.WriteLine();

    var snapshot = ReadModuleSnapshot(address, range);

    int hits = 0;
    for (long i = 0; i + 8 <= snapshot.LongLength; i += 8)
    {
        long fieldValue = BitConverter.ToInt64(snapshot, (int)i);
        if (!LooksLikePointer(fieldValue)) continue;

        foreach (var xor in xors)
        {
            long p2 = fieldValue + xor;
            long start = ProcessManager.ReadInt64(p2);
            long end = ProcessManager.ReadInt64(p2 + 8);
            if (start == 0 || end < start) continue;

            long count = (end - start) / 8;
            if (count < minCount || count > maxCount) continue;

            hits++;
            string verdict = DescribeAsPointerArray(start, moduleBaseAddress, moduleSize);
            Console.WriteLine($"  field +0x{i:X} (value 0x{fieldValue:X}, xor 0x{xor:X}) -> bounds 0x{start:X}..0x{end:X} = {count} entries  {verdict}");
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {hits} candidate(s).");
}

static byte[] ReadModuleSnapshot(long baseAddress, long size)
{
    var buffer = new byte[size];
    const int chunk = 16 * 1024 * 1024;
    long offset = 0;
    while (offset < size)
    {
        int len = (int)Math.Min(chunk, size - offset);
        byte[] part = ProcessManager.ReadProcessMemory(baseAddress + offset, (uint)len, out _);
        Array.Copy(part, 0, buffer, offset, len);
        offset += len;
    }

    return buffer;
}

static bool LooksLikePointer(long value) => value > 0x10000 && value < 0x00007FFFFFFFFFFF;

static string DescribeAsPointerArray(long arrayStart, long moduleBaseAddress, long moduleSize)
{
    long element0 = ProcessManager.ReadInt64(arrayStart);
    if (!LooksLikePointer(element0))
    {
        return "(element[0] isn't pointer-shaped)";
    }

    long vtbl = ProcessManager.ReadInt64(element0);
    if (vtbl < moduleBaseAddress || vtbl >= moduleBaseAddress + moduleSize)
    {
        return "(element[0]'s [0x0] isn't an in-module vtable)";
    }

    long rva = vtbl - moduleBaseAddress;
    return GetKnownVtableRvas().TryGetValue(rva, out var name)
        ? $"<-- element[0] vtable RVA=0x{rva:X} = {name}!"
        : $"(element[0] vtable RVA=0x{rva:X}, unrecognized type)";
}

static Dictionary<long, string> GetKnownVtableRvas() => new()
{
    [0x4509D68] = "Player",
    [0x44C13A8] = "Club",
    [0x4565018] = "Staff",
    [0x44A5238] = "HumanManager",
};

static string? GetOption(string[] args, string name)
{
    for (int i = 0; i < args.Length - 1; i++)
    {
        if (string.Equals(args[i], name, StringComparison.OrdinalIgnoreCase))
        {
            return args[i + 1];
        }
    }

    return null;
}

static long ParseHex(string? text, long defaultValue)
{
    if (string.IsNullOrWhiteSpace(text)) return defaultValue;
    text = text.Trim();
    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) text = text[2..];
    return Convert.ToInt64(text, 16);
}

static long ParseHexOrDec(string? text, long defaultValue)
{
    if (string.IsNullOrWhiteSpace(text)) return defaultValue;
    text = text.Trim();
    return text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
        ? Convert.ToInt64(text[2..], 16)
        : long.Parse(text);
}

static long[] ParseHexList(string? text, long[] defaultValue)
{
    if (string.IsNullOrWhiteSpace(text)) return defaultValue;
    return text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(t => ParseHex(t, 0))
        .ToArray();
}

static void PrintUsage()
{
    Console.WriteLine("""
        FM.Pitchside.OffsetFinder - live memory scanner to help locate offsets for a new FM version.
        Run this while the target Football Manager build is running.

        All commands accept a global --module <name> option (e.g. --module game_plugin.dll)
        to scan a module other than the main exe. Defaults to the main module.

        Usage:
          info
              Attach and print process/base-address/version info only.

          find-continents [--xors 0x68,0x88,...] [--offset 0x30] [--from 0x0] [--to <moduleSize>]
              Brute-force scan for a MainAddress + XorDistance combination whose "Continent"
              table resolves to exactly 7 entries (the same check SupportsProcess() uses).

          find-date [--year-min 2024] [--year-max 2032] [--from 0x0] [--to <moduleSize>]
              Scan for a CurrentDateTime RVA that decodes to a plausible in-game date.
              Cross-check candidates against the date shown in the FM UI.

          check <mainAddressHex> <xorHex> [--offset 0x30]
              Re-run the continent-count check for one specific candidate.

          peek <addressHex> [count]
              Hex-dump `count` (default 8) Int64 values starting at a live address.

          find-string-field <objectAddressHex> <text> [--range 0x1000] [--hops 1] [--range2 0x800]
              Scan the qwords around a live object address for a pointer to an
              FM-encoded string (Int32 length + UTF8 bytes) containing `text`.
              Reports the matching field's byte offset from the object address -
              use with a known object (e.g. a captured player) to find real
              struct field offsets from ground truth instead of guessing.
              --hops 2 treats every pointer-shaped qword in range as a candidate
              sub-object and searches range2 around each of those too, for names
              reached through one extra level of indirection (e.g. Club -> Team).

          scan-arrays <objectAddressHex> [--range 0x200] [--xors 0x0] [--min 1] [--max 5000]
              Treat every pointer-shaped qword in a known object's own fields as a
              candidate array-bounds pointer (same {start,end}/8 check find-continents
              uses) and report any that resolve to a plausible entry count. Use on a
              captured object (e.g. a club) to find list fields like a squad array.

          identify-slots <mainAddressHex> <xorHex> [--offsets 0x10,0x18,...]
              Walk the master table (default: every FM22-era slot 0x10-0xC8) and, for
              each slot that resolves to a real array, check element[0]'s vtable
              against the known vtables - use this to find which slot (if any) really
              is Club/Player/etc, instead of guessing from count magnitude alone.
        """);
}
