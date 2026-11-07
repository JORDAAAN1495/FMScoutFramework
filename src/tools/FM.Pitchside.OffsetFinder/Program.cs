using System.Diagnostics;
using FM.Pitchside.Core;
using FM.Pitchside.Core.Converters;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.VirtualMemory.Managers;
using FM.Pitchside.Core.VirtualMemory.Rtti;

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

// rtti-* commands can also run offline against a game_plugin.dll on disk (no running FM needed):
//   rtti-vtable TEAM@db --file "C:\...\game_plugin.dll"
var offlineFile = GetOption(rest, "--file");
if (command.StartsWith("rtti-", StringComparison.Ordinal) && offlineFile != null)
{
    RttiCommands.Run(command, rest, RttiImage.FromFile(offlineFile));
    return;
}

// fm27-managers goes through the real library path (FMCore.LoadData -> version detection -> Fm27Game),
// so it manages its own attach instead of using the module-level one below.
if (command == "fm27-managers")
{
    RunFm27Managers();
    return;
}

if (command == "fm27-players")
{
    RunFm27Players(rest);
    return;
}

if (command == "fm27-scout")
{
    RunFm27Scout(rest);
    return;
}

if (command == "fm27-roles")
{
    RunFm27Roles(rest);
    return;
}

if (command == "fm27-squads")
{
    RunFm27Squads(rest);
    return;
}

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

    case "scan-heap":
        RunScanHeap(rest);
        break;

    case "find-pointers-to":
        RunFindPointersTo(rest);
        break;

    case "find-vtable-base":
        RunFindVtableBase(baseAddress, moduleSize, rest);
        break;

    case "scan-heap-arrays":
        RunScanHeapArrays(baseAddress, moduleSize, rest);
        break;

    case "scan-heap-int":
        RunScanHeapInt(rest);
        break;

    case "find-pointers-in-range":
        RunFindPointersInRange(baseAddress, moduleSize, rest);
        break;

    case "rtti-name":
    case "rtti-vtable":
    case "rtti-list":
        RttiCommands.Run(command, rest, new RttiImage(ReadModuleSnapshot(baseAddress, moduleSize), baseAddress));
        break;

    case "rtti-instances":
        RunRttiInstances(new RttiImage(ReadModuleSnapshot(baseAddress, moduleSize), baseAddress), baseAddress, rest);
        break;

    case "rtti-object":
        RttiCommands.Run(command, rest, new RttiImage(ReadModuleSnapshot(baseAddress, moduleSize), baseAddress), (address, length) =>
        {
            var bytes = ProcessManager.ReadProcessMemory(address, (uint)length, out var bytesRead);
            return bytesRead >= length ? bytes : null;
        });
        break;

    default:
        PrintUsage();
        break;
}

static void RunFm27Managers()
{
    var core = new FMCore();
    try
    {
        core.LoadData();
    }
    catch (FMProcessNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }

    Console.WriteLine($"Matched version definition: {core.gameManager.Version.Description}");
    if (core.Fm27 == null)
    {
        Console.WriteLine("That is not an RTTI-based version, so there is no FM27 model to query.");
        return;
    }

    Console.WriteLine($"  game_plugin.dll base 0x{core.Fm27.ModuleBase:X}, manager-details vtable RVA 0x{core.Fm27.HumanManagerDetailsVtableRva:X}");
    Console.WriteLine("Scanning the heap for human managers...");
    var sw = Stopwatch.StartNew();
    var managers = core.FindHumanManagers();
    Console.WriteLine($"Found {managers.Count} human manager(s) in {sw.Elapsed.TotalSeconds:F1}s:");
    foreach (var m in managers)
    {
        Console.WriteLine($"  {m.DisplayName}  -  {m.ClubName}");
        Console.WriteLine($"      full='{m.FullName}' first='{m.FirstName}' second='{m.SecondName}' common='{m.CommonName}'");
        Console.WriteLine($"      details@0x{m.DetailsAddress:X} team@0x{m.TeamAddress:X} club@0x{m.ClubAddress:X} record@0x{m.RecordAddress:X}");
    }
}

static void RunFm27Squads(string[] args)
{
    string? clubFilter = GetOption(args, "--club");
    int max = (int)ParseHexOrDec(GetOption(args, "--max"), 30);

    var core = new FMCore();
    try
    {
        core.LoadData();
    }
    catch (FMProcessNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }

    var game = core.Fm27;
    if (game == null || !game.PlayersAvailable)
    {
        Console.WriteLine("Player classes did not resolve for this build - nothing to read.");
        return;
    }

    Console.WriteLine($"Team vtable resolved: {(game.TeamVtableRva != 0 ? "yes (0x" + game.TeamVtableRva.ToString("X") + ")" : "NO - squads/loans unavailable")}");
    var players = game.FindPlayers();
    var squads = game.FindSquads(players);
    Console.WriteLine($"{players.Count:N0} players, {squads.Count:N0} squads.");
    Console.WriteLine();

    string Pct(int n) => players.Count == 0 ? "n/a" : $"{n * 100.0 / players.Count:F1}%";
    Console.WriteLine("Population:");
    Console.WriteLine($"  has an owner club (contract)      {Pct(players.Count(p => p.ClubName.Length > 0))}   ({players.Count(p => p.ClubName.Length == 0):N0} without)");
    Console.WriteLine($"  has a recognised current team     {Pct(players.Count(p => p.CurrentTeamAddress != 0))}   ({players.Count(p => p.CurrentTeamAddress == 0):N0} without)");
    Console.WriteLine($"  registered with another club      {players.Count(p => p.PlaysForOtherClub):N0}   = {players.Count(p => p.IsWithAffiliateClub):N0} with an affiliate (B/reserve) club + {players.Count(p => p.IsOnLoan):N0} loans (heuristic)");
    Console.WriteLine($"  contract club but no current team {players.Count(p => p.ClubName.Length > 0 && p.CurrentTeamAddress == 0):N0}");
    Console.WriteLine($"  clubs with a squad                {squads.Select(s => s.ClubAddress).Distinct().Count():N0}");
    Console.WriteLine();

    Console.WriteLine("Squad types (count of squads, median size):");
    foreach (var group in squads.GroupBy(s => s.SquadType).OrderByDescending(g => g.Count()))
    {
        var sizes = group.Select(s => s.Players.Count).OrderBy(n => n).ToList();
        Console.WriteLine($"  {Fm27Names.SquadName(group.Key),-12} (type {group.Key,2}): {group.Count(),5:N0} squads, median {sizes[sizes.Count / 2]} players, max {sizes.Last()}");
    }

    Console.WriteLine();
    foreach (var manager in game.FindHumanManagers())
    {
        var own = squads.Where(s => s.ClubAddress == manager.ClubAddress).ToList();
        Console.WriteLine($"Human manager {manager.DisplayName} - {manager.ClubName}: this club has {own.Count} squad(s): " +
                          string.Join(", ", own.Select(s => $"{s.Squad} {s.Players.Count}")));
        var firstTeam = squads.FirstOrDefault(s => s.TeamAddress == manager.TeamAddress);
        Console.WriteLine(firstTeam == null
            ? "  (the manager's team address is not one of the squads)"
            : $"  the manager's team (0x{manager.TeamAddress:X}) is the '{firstTeam.Squad}' squad with {firstTeam.Players.Count} players");
    }

    if (args.Contains("--debug"))
    {
        Console.WriteLine();
        Console.WriteLine("-- most common LOAN moves (owner club -> club they are playing for):");
        foreach (var pair in players.Where(p => p.IsOnLoan).GroupBy(p => (p.ClubName, p.LoanClubName)).OrderByDescending(g => g.Count()).Take(8))
        {
            Console.WriteLine($"   {pair.Count(),4}  {pair.Key.ClubName} -> {pair.Key.LoanClubName}   [{string.Join(", ", pair.GroupBy(p => p.Squad).Select(g => g.Key + " " + g.Count()))}]");
        }

        Console.WriteLine("-- most common AFFILIATE moves (should all be B/reserve clubs):");
        foreach (var pair in players.Where(p => p.IsWithAffiliateClub).GroupBy(p => (p.ClubName, p.CurrentClubName)).OrderByDescending(g => g.Count()).Take(6))
        {
            Console.WriteLine($"   {pair.Count(),4}  {pair.Key.ClubName} -> {pair.Key.CurrentClubName}");
        }

        Console.WriteLine("-- loans into the 6 clubs that take the most loanees (a real loan club has many different owners):");
        foreach (var dest in players.Where(p => p.IsOnLoan).GroupBy(p => p.LoanClubName).OrderByDescending(g => g.Count()).Take(6))
        {
            Console.WriteLine($"   {dest.Count(),4} loanees at {dest.Key} from {dest.Select(p => p.ClubName).Distinct().Count()} different clubs");
        }

        Console.WriteLine("-- 12 highest-CA loans:");
        foreach (var p in players.Where(x => x.IsOnLoan).OrderByDescending(x => x.CurrentAbility).Take(12))
        {
            Console.WriteLine($"   {p.DisplayName,-24} CA {p.CurrentAbility,3}  {p.ClubName} -> {p.LoanClubName} ({p.Squad})");
        }

        // Where do the contract's two team pointers (+16, +24) and the current team point, for a true loan,
        // a B-team player and an ordinary player?
        string ClubOfTeam(long team) => team == 0 ? "(null)"
            : ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(ProcessHeap.ReadPointer(team + Fm27Game.Layout.TeamClub) + Fm27Game.Layout.ClubName)) ?? "(unreadable)";
        var samples = new List<(string Label, Fm27PlayerInfo? P)>
        {
            ("true loan", players.FirstOrDefault(p => p.DisplayName == "Omar Marmoush")),
            ("true loan", players.FirstOrDefault(p => p.DisplayName == "Ronald Araujo")),
            ("true loan", players.FirstOrDefault(p => p.DisplayName == "Jack Grealish")),
            ("B-team", players.Where(p => p.IsOnLoan && p.ClubName == "Futbol Club Barcelona" && p.LoanClubName.Contains("Atlètic")).OrderByDescending(p => p.CurrentAbility).FirstOrDefault()),
            ("B-team", players.Where(p => p.IsOnLoan && p.ClubName.StartsWith("Real Madrid") && p.LoanClubName.EndsWith(" C")).OrderByDescending(p => p.CurrentAbility).FirstOrDefault()),
            ("ordinary", players.FirstOrDefault(p => p.DisplayName == "Bruno Fernandes" && p.CurrentAbility > 150)),
        };
        Console.WriteLine("-- contract pointers: +16 / +24 / current team  (club each belongs to):");
        foreach (var (label, p) in samples)
        {
            if (p == null || p.ContractAddress == 0) { Console.WriteLine($"   {label,-9} (not found)"); continue; }
            long t16 = ProcessHeap.ReadPointer(p.ContractAddress + 16);
            long t24 = ProcessHeap.ReadPointer(p.ContractAddress + 24);
            Console.WriteLine($"   {label,-9} {p.DisplayName,-18} +16: {ClubOfTeam(t16),-30} +24: {ClubOfTeam(t24),-30} current: {p.CurrentClubName}");
        }

        // Is there a field on the destination's club or team record that points back at the owner club
        // (or owner team)? That would be the game's own "parent club" link for B teams.
        Console.WriteLine("-- parent-link search: qwords in the destination club (0x600 bytes) / team (0x300 bytes) equal to the owner club/team:");
        foreach (var (label, p) in samples)
        {
            if (p == null || p.CurrentClubAddress == 0 || p.ClubAddress == 0) continue;
            long ownerTeam = ProcessHeap.ReadPointer(p.ContractAddress + 24);
            var found = new List<string>();
            foreach (var (what, baseAddr, length) in new[] { ("club", p.CurrentClubAddress, 0x600), ("team", p.CurrentTeamAddress, 0x300) })
            {
                var bytes = ProcessHeap.TryRead(baseAddr, length);
                if (bytes == null) continue;
                for (int i = 0; i + 8 <= bytes.Length; i += 8)
                {
                    long v = BitConverter.ToInt64(bytes, i);
                    if (v == p.ClubAddress) found.Add($"{what}+0x{i:X}=ownerClub");
                    else if (v == ownerTeam) found.Add($"{what}+0x{i:X}=ownerTeam");
                }
            }

            Console.WriteLine($"   {label,-9} {p.DisplayName,-18} {p.ClubName} -> {p.CurrentClubName}:  {(found.Count == 0 ? "no pointer back to the owner" : string.Join(", ", found))}");
        }

        // And the other direction: does the OWNER's club/team record point at the destination (a "B team" list)?
        Console.WriteLine("-- reverse search: qwords in the owner club (0x600) / owner team (0x300) equal to the destination club/team:");
        foreach (var (label, p) in samples)
        {
            if (p == null || p.CurrentClubAddress == 0 || p.ClubAddress == 0) continue;
            long ownerTeam = ProcessHeap.ReadPointer(p.ContractAddress + 24);
            var found = new List<string>();
            foreach (var (what, baseAddr, length) in new[] { ("club", p.ClubAddress, 0x600), ("team", ownerTeam, 0x300) })
            {
                var bytes = ProcessHeap.TryRead(baseAddr, length);
                if (bytes == null) continue;
                for (int i = 0; i + 8 <= bytes.Length; i += 8)
                {
                    long v = BitConverter.ToInt64(bytes, i);
                    if (v == p.CurrentClubAddress) found.Add($"{what}+0x{i:X}=destClub");
                    else if (v == p.CurrentTeamAddress) found.Add($"{what}+0x{i:X}=destTeam");
                }
            }

            Console.WriteLine($"   {label,-9} {p.DisplayName,-18} {p.ClubName} -> {p.CurrentClubName}:  {(found.Count == 0 ? "no direct pointer" : string.Join(", ", found))}");
        }

        Console.WriteLine("-- 8 largest squads:");
        foreach (var s in squads.OrderByDescending(x => x.Players.Count).Take(8))
        {
            Console.WriteLine($"   {s.Players.Count,4}  {s.ClubName} {s.Squad}   (avg CA {s.Players.Average(p => p.CurrentAbility):F0}, on loan in: {s.Players.Count(p => p.IsOnLoan)})");
        }

        Console.WriteLine("-- first-team size distribution (all clubs): " + string.Join("  ",
            squads.Where(s => s.SquadType == 0).GroupBy(s => s.Players.Count switch { <= 5 => "1-5", <= 15 => "6-15", <= 25 => "16-25", <= 40 => "26-40", _ => "41+" })
                .OrderBy(g => g.Key, StringComparer.Ordinal).Select(g => $"{g.Key}: {g.Count():N0}")));
        Console.WriteLine("-- first teams of big clubs: " + string.Join("  ", new[] { "Chelsea", "Real Madrid", "Barcelona", "Manchester City", "Arsenal" }
            .Select(c => (c, s: squads.FirstOrDefault(x => x.SquadType == 0 && x.ClubName.Contains(c, StringComparison.OrdinalIgnoreCase))))
            .Select(x => $"{x.c} {x.s?.Players.Count.ToString() ?? "?"}")));
    }

    Console.WriteLine();
    Console.WriteLine("Spot-check: where the game says famous players are (highest-CA match):");
    foreach (var name in new[] { "Mbappé", "Haaland", "Courtois", "Harry Kane", "Declan Rice", "Olise", "Dembélé", "Leny Yoro", "Bruno Fernandes", "Lamine Yamal", "Jude Bellingham" })
    {
        var p = players.Where(x => x.DisplayName.Contains(name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(x => x.CurrentAbility).FirstOrDefault();
        Console.WriteLine(p == null
            ? $"  {name,-18} (not found)"
            : $"  {p.DisplayName,-22} CA {p.CurrentAbility,3}  {(p.ClubName.Length == 0 ? "(no club)" : p.ClubName),-26} {(p.Squad.Length == 0 ? "" : p.Squad),-11} {(p.IsOnLoan ? "ON LOAN at " + p.LoanClubName : "")}");
    }

    var toShow = clubFilter == null
        ? squads.Where(s => game.FindHumanManagers().Any(m => m.TeamAddress == s.TeamAddress)).ToList()
        : squads.Where(s => s.ClubName.Contains(clubFilter, StringComparison.OrdinalIgnoreCase)).ToList();

    foreach (var squad in toShow.Take(6))
    {
        Console.WriteLine();
        Console.WriteLine($"{squad.ClubName} - {squad.Squad} ({squad.Players.Count} players):");
        foreach (var p in squad.Players.OrderByDescending(x => x.CurrentAbility).Take(max))
        {
            Console.WriteLine($"  {p.DisplayName,-24} {string.Join("/", p.BestPositions),-10} CA {p.CurrentAbility,3}  best IP: {p.BestInPossessionRole ?? "-",-24} best OOP: {p.BestOutOfPossessionRole ?? "-",-26} {(p.IsOnLoan ? "(loan from " + p.ClubName + ")" : "")}");
        }
    }
}

static void RunFm27Roles(string[] args)
{
    string? nameFilter = GetOption(args, "--name");
    int max = (int)ParseHexOrDec(GetOption(args, "--max"), 10);

    var core = new FMCore();
    try
    {
        core.LoadData();
    }
    catch (FMProcessNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }

    var game = core.Fm27;
    if (game == null || !game.PlayersAvailable)
    {
        Console.WriteLine("Player classes did not resolve for this build - nothing to read.");
        return;
    }

    var players = game.FindPlayers();
    Console.WriteLine($"Read {players.Count:N0} players.");
    Console.WriteLine();

    bool ValidRole(string?[] table, byte id) => id < table.Length && table[id] != null;
    int inPoss = players.Count(p => ValidRole(Fm27Names.RolesInPossession, p.TopRoleIds[0]));
    int outPoss = players.Count(p => ValidRole(Fm27Names.RolesOutOfPossession, p.TopRoleIds[3]));
    Console.WriteLine($"Top in-possession role id is a named role : {inPoss * 100.0 / players.Count:F1}%");
    Console.WriteLine($"Top out-of-possession role id is named    : {outPoss * 100.0 / players.Count:F1}%");
    Console.WriteLine($"All 15 position ratings in 0..20           : {players.Count(p => p.PositionRatings.All(r => r <= 20)) * 100.0 / players.Count:F1}%");
    Console.WriteLine($"No position rated at all (all zero)        : {players.Count(p => p.BestPositionRating == 0):N0} players");
    Console.WriteLine();

    // Cross-tab: players grouped by their single best position -> what roles the game says suit them.
    Console.WriteLine("Best position -> most common top roles (in possession | out of possession)");
    foreach (int index in Enumerable.Range(0, Fm27Names.Positions.Length))
    {
        var group = players.Where(p => p.BestPositionRating > 0 && Array.IndexOf(p.PositionRatings, (byte)p.BestPositionRating) == index).ToList();
        if (group.Count == 0)
        {
            continue;
        }

        string Top(Func<Fm27PlayerInfo, IReadOnlyList<string>> pick) =>
            string.Join(", ", group.SelectMany(p => pick(p).Take(1)).GroupBy(n => n).OrderByDescending(g => g.Count()).Take(3)
                .Select(g => $"{g.Key} {g.Count() * 100 / group.Count}%"));
        Console.WriteLine($"  {Fm27Names.Positions[index],-3} n={group.Count,6:N0}  IP: {Top(p => p.InPossessionRoles)}  |  OOP: {Top(p => p.OutOfPossessionRoles)}");
    }

    // The one hard rule that needs no football knowledge about specific roles.
    var keepers = players.Where(p => p.IsGoalkeeper).ToList();
    int keeperRoleOk = keepers.Count(p => p.InPossessionRoles.Count > 0 && p.InPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase)
                                          && p.OutOfPossessionRoles.Count > 0 && p.OutOfPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase));
    int outfieldKeeperRole = players.Where(p => !p.IsGoalkeeper).Count(p => p.InPossessionRoles.Count > 0 && p.InPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase));
    Console.WriteLine();
    Console.WriteLine($"Goalkeepers (GK >= 10): {keepers.Count:N0}; with goalkeeper roles on top: {keeperRoleOk:N0} ({(keepers.Count == 0 ? 0 : keeperRoleOk * 100.0 / keepers.Count):F1}%)");
    Console.WriteLine($"Outfield players whose top in-possession role is a goalkeeper role: {outfieldKeeperRole:N0}");
    if (args.Contains("--debug"))
    {
        var odd = keepers.Where(p => !(p.InPossessionRoles.Count > 0 && p.InPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase)
                                       && p.OutOfPossessionRoles.Count > 0 && p.OutOfPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase))).ToList();
        Console.WriteLine($"-- {odd.Count:N0} goalkeepers without goalkeeper roles on top. Why:");
        Console.WriteLine($"   unnamed players among them        : {odd.Count(p => p.DisplayName.Length == 0):N0}");
        Console.WriteLine($"   IP top id invalid / OOP top invalid: {odd.Count(p => p.InPossessionRoles.Count == 0):N0} / {odd.Count(p => p.OutOfPossessionRoles.Count == 0):N0}");
        Console.WriteLine($"   IP ok but OOP not a keeper role    : {odd.Count(p => p.InPossessionRoles.Count > 0 && p.InPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase) && !(p.OutOfPossessionRoles.Count > 0 && p.OutOfPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase))):N0}");
        Console.WriteLine($"   OOP ok but IP not a keeper role    : {odd.Count(p => p.OutOfPossessionRoles.Count > 0 && p.OutOfPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase) && !(p.InPossessionRoles.Count > 0 && p.InPossessionRoles[0].Contains("keeper", StringComparison.OrdinalIgnoreCase))):N0}");
        Console.WriteLine($"   GK rating among them: min {odd.Min(p => (int)p.PositionRatings[0])}, avg {odd.Average(p => (int)p.PositionRatings[0]):F1}; CA avg {odd.Average(p => p.CurrentAbility):F1}");
        foreach (var p in odd.Take(8))
        {
            Console.WriteLine($"   {p.DisplayName,-24} GK {p.PositionRatings[0]} best {string.Join(",", p.BestPositions)}  IP [{string.Join(" > ", p.InPossessionRoles)}] ids {string.Join(",", p.TopRoleIds)}  OOP [{string.Join(" > ", p.OutOfPossessionRoles)}]");
        }
    }

    Console.WriteLine();

    var shown = (nameFilter == null
            ? players.Where(p => p.Nation.Length > 0).OrderByDescending(p => p.CurrentAbility).Take(max)
            : players.Where(p => p.DisplayName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).OrderByDescending(p => p.CurrentAbility).Take(max))
        .ToList();
    Console.WriteLine(nameFilter == null ? $"Top {shown.Count} players by CA:" : $"{shown.Count} player(s) matching '{nameFilter}':");
    foreach (var p in shown)
    {
        Console.WriteLine($"  {p.DisplayName} (CA {p.CurrentAbility}, b.{p.BirthYear})");
        Console.WriteLine($"      positions : " + string.Join("  ", Enumerable.Range(0, 15).Where(i => p.PositionRatings[i] > 0).OrderByDescending(i => p.PositionRatings[i]).Select(i => $"{Fm27Names.Positions[i]} {p.PositionRatings[i]}")));
        Console.WriteLine($"      best      : {string.Join(", ", p.BestPositions)}");
        Console.WriteLine($"      IP roles  : {string.Join(" > ", p.InPossessionRoles)}   (ids {p.TopRoleIds[0]},{p.TopRoleIds[1]},{p.TopRoleIds[2]})");
        Console.WriteLine($"      OOP roles : {string.Join(" > ", p.OutOfPossessionRoles)}   (ids {p.TopRoleIds[3]},{p.TopRoleIds[4]},{p.TopRoleIds[5]})");
    }
}

// fm27-scout --pos ST [--min-pos 15] [--max-age 23] [--min-ca 120] [--min-pa 150] [--asof 2026-08-01] [--max 40]
static void RunFm27Scout(string[] args)
{
    string pos = GetOption(args, "--pos") ?? "ST";
    int minPos = (int)ParseHexOrDec(GetOption(args, "--min-pos"), 15);
    int maxAge = (int)ParseHexOrDec(GetOption(args, "--max-age"), 23);
    int minCa = (int)ParseHexOrDec(GetOption(args, "--min-ca"), 110);
    int minPa = (int)ParseHexOrDec(GetOption(args, "--min-pa"), 140);
    int max = (int)ParseHexOrDec(GetOption(args, "--max"), 40);
    DateTime asOf = DateTime.TryParse(GetOption(args, "--asof"), out var d) ? d : new DateTime(2026, 8, 1);

    var core = new FMCore();
    core.LoadData();
    var game = core.Fm27;
    if (game == null || !game.PlayersAvailable)
    {
        Console.WriteLine("FM27 player classes not available.");
        return;
    }

    var players = game.FindPlayers();
    int Age(Fm27PlayerInfo p) => p.BirthDate is { } b ? (int)((asOf - b).TotalDays / 365.25) : 99;

    var hits = players
        .Where(p => p.GetPositionRating(pos) >= minPos && Age(p) <= maxAge
                    && p.CurrentAbility >= minCa && p.PotentialAbility >= minPa)
        .OrderByDescending(p => p.PotentialAbility).ThenByDescending(p => p.CurrentAbility)
        .Take(max).ToList();

    Console.WriteLine($"{hits.Count} shown (pos {pos} >= {minPos}, age <= {maxAge} as of {asOf:yyyy-MM-dd}, CA >= {minCa}, PA >= {minPa}); {players.Count:N0} players scanned.");
    foreach (var p in hits)
    {
        Console.WriteLine($"  {p.DisplayName,-24} {Age(p),2}  {p.Nation,-14} {p.ClubName ?? "-",-26} CA {p.CurrentAbility,3} PA {p.PotentialAbility,3}  {string.Join("/", p.BestPositions),-10} {p.HeightCm} cm  IP: {p.BestInPossessionRole}");
    }
}

static void RunFm27Players(string[] args)
{
    string? nameFilter = GetOption(args, "--name");
    int max = (int)ParseHexOrDec(GetOption(args, "--max"), 15);

    var core = new FMCore();
    var load = Stopwatch.StartNew();
    try
    {
        core.LoadData();
    }
    catch (FMProcessNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }

    Console.WriteLine($"Matched version definition: {core.gameManager.Version.Description}  (LoadData {load.Elapsed.TotalSeconds:F1}s)");
    var game = core.Fm27;
    if (game == null || !game.PlayersAvailable)
    {
        Console.WriteLine("Player classes did not resolve for this build - nothing to read.");
        return;
    }

    Console.WriteLine($"  player vtables: attrs 0x{game.PlayerAttrsVtableRva:X}, id 0x{game.PlayerIdVtableRva:X}, person 0x{game.PlayerPersonVtableRva:X}");

    var read = Stopwatch.StartNew();
    var players = game.FindPlayers();
    Console.WriteLine($"Read {players.Count:N0} players in {read.Elapsed.TotalSeconds:F1}s.");
    Console.WriteLine();

    // Plausibility of the decoded fields across the whole population.
    int Count(Func<Fm27PlayerInfo, bool> test) => players.Count(test);
    string Pct(int n) => players.Count == 0 ? "n/a" : $"{n * 100.0 / players.Count:F1}%";
    Console.WriteLine("Sanity (share of players whose field is in a plausible range):");
    Console.WriteLine($"  full name present      {Pct(Count(p => p.FullName.Length > 0))}");
    Console.WriteLine($"  CA 1..200              {Pct(Count(p => p.CurrentAbility >= 1 && p.CurrentAbility <= 200))}");
    Console.WriteLine($"  PA 1..200              {Pct(Count(p => p.PotentialAbility >= 1 && p.PotentialAbility <= 200))}");
    Console.WriteLine($"  PA >= CA               {Pct(Count(p => p.PotentialAbility >= p.CurrentAbility))}");
    Console.WriteLine($"  valid birth date       {Pct(Count(p => p.BirthDate.HasValue && p.BirthYear >= 1960 && p.BirthYear <= 2016))}");
    Console.WriteLine($"  height 140..215 cm     {Pct(Count(p => p.HeightCm >= 140 && p.HeightCm <= 215))}");
    Console.WriteLine($"  nation present         {Pct(Count(p => p.Nation.Length > 0))}");
    Console.WriteLine($"  display name present   {Pct(Count(p => p.DisplayName.Length > 0))}   ({Count(p => p.DisplayName.Length == 0):N0} unnamed)");
    Console.WriteLine($"  uid unassigned (-1)    {Count(p => p.Uid == uint.MaxValue):N0}");
    var named = players.Where(p => p.DisplayName.Length > 0 && p.Uid != uint.MaxValue).ToList();
    Console.WriteLine($"  unique uids (named, assigned) {named.Select(p => p.Uid).Distinct().Count():N0} of {named.Count:N0}");
    var unnamed = players.Where(p => p.DisplayName.Length == 0).ToList();
    if (unnamed.Count > 0)
    {
        Console.WriteLine($"  unnamed players: CA avg {unnamed.Average(p => p.CurrentAbility):F1}, max {unnamed.Max(p => p.CurrentAbility)}; named: CA avg {players.Where(p => p.DisplayName.Length > 0).Average(p => p.CurrentAbility):F1}");
    }
    Console.WriteLine();

    // The legacy Player entity (core.Players) should agree with the new records on identity.
    var byAddress = players.ToDictionary(p => p.Address, p => p, EqualityComparer<long>.Default);
    int checkedCount = 0, uidMismatch = 0, nameMismatch = 0, missing = 0;
    foreach (var legacy in core.Players.Take(5000))
    {
        checkedCount++;
        if (!byAddress.TryGetValue(legacy.Address, out var info)) { missing++; continue; }
        if ((uint)legacy.UID != info.Uid) uidMismatch++;
        if (legacy.ActualPerson.FullName != info.FullName) nameMismatch++;
    }

    Console.WriteLine($"Legacy core.Players: {core.Players.Count():N0} entities. Cross-check of the first {checkedCount:N0} against the new records: " +
                      $"{uidMismatch} uid mismatches, {nameMismatch} name mismatches, {missing} missing.");

    if (args.Contains("--debug"))
    {
        Console.WriteLine("-- name mismatches (legacy vs new), first 8:");
        int shownMismatches = 0;
        foreach (var legacy in core.Players.Take(5000))
        {
            if (shownMismatches >= 8) break;
            if (byAddress.TryGetValue(legacy.Address, out var info) && legacy.ActualPerson.FullName != info.FullName)
            {
                shownMismatches++;
                Console.WriteLine($"   @0x{legacy.Address:X}  legacy='{legacy.ActualPerson.FullName}'  new='{info.FullName}'  display='{info.DisplayName}'");
            }
        }

        Console.WriteLine("-- duplicate uids, first 6 groups:");
        foreach (var group in players.GroupBy(p => p.Uid).Where(g => g.Count() > 1).Take(6))
        {
            Console.WriteLine($"   uid {group.Key}:");
            foreach (var p in group)
            {
                Console.WriteLine($"      @0x{p.Address:X}  {p.DisplayName}  CA {p.CurrentAbility} PA {p.PotentialAbility}  b.{p.BirthYear}/{p.BirthDayOfYear}  {p.HeightCm}cm");
            }
        }
    }

    Console.WriteLine();

    var shown = (nameFilter == null
            ? players.Take(max)
            : players.Where(p => p.FullName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)
                              || p.DisplayName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).Take(max))
        .ToList();

    Console.WriteLine(nameFilter == null ? $"First {shown.Count} players:" : $"{shown.Count} player(s) matching '{nameFilter}':");
    foreach (var p in shown)
    {
        Console.WriteLine($"  {p.DisplayName,-26} uid {p.Uid,-9} {p.Nation,-16} b. {p.BirthDate:yyyy-MM-dd} (year {p.BirthYear}, day {p.BirthDayOfYear})  {p.HeightCm} cm  CA {p.CurrentAbility}  PA {p.PotentialAbility} (raw {p.PotentialAbilityRaw})");
    }
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
    var tableOffsets = ParseHexList(GetOption(args, "--offsets") ?? GetOption(args, "--offset"),
        defaultValue: new long[] { 0x30 });
    long from = ParseHex(GetOption(args, "--from"), 0x0);
    long to = ParseHex(GetOption(args, "--to"), moduleSize);

    Console.WriteLine($"Downloading a snapshot of the module image (0x{to - from:X} bytes)...");
    var snapshot = ReadModuleSnapshot(baseAddress + from, to - from);

    Console.WriteLine($"Scanning {(to - from) / 8:N0} candidate RVAs x {xors.Length} xor value(s) x {tableOffsets.Length} table offset(s) for a count of 7...");

    int candidatesChecked = 0;
    int hits = 0;
    foreach (var tableOffset in tableOffsets)
    {
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
                    Console.WriteLine($"  MATCH  MainAddress=0x{rva:X}  XorDistance=0x{xor:X}  TableOffset=0x{tableOffset:X}");
                }
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {candidatesChecked:N0} plausible pointer(s) live-checked, {hits} match(es) found.");
    if (hits == 0)
    {
        Console.WriteLine("No matches. Try widening --xors, or the Continent slot may have moved - retry with --offsets.");
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

static void RunScanHeap(string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: scan-heap <text> [--max-hits 50] [--case-sensitive]");
        Console.WriteLine("Searches EVERY committed/readable region of the process (not just a module's");
        Console.WriteLine("static image) for the raw UTF8 bytes of <text> - use this to find heap-allocated");
        Console.WriteLine("objects (e.g. a player/manager name) that a module-only scan can never reach.");
        return;
    }

    string text = args[0];
    int maxHits = (int)ParseHexOrDec(GetOption(args, "--max-hits"), 50);
    bool caseSensitive = args.Any(a => string.Equals(a, "--case-sensitive", StringComparison.OrdinalIgnoreCase));
    byte[] needle = System.Text.Encoding.UTF8.GetBytes(text);

    Console.WriteLine($"Scanning all committed/readable process memory for \"{text}\"...");
    Console.WriteLine();

    const long chunkSize = 16 * 1024 * 1024;
    long totalBytes = 0;
    int regionCount = 0;
    int hits = 0;
    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var (regionBase, regionSize) in ProcessManager.EnumerateReadableRegions())
    {
        regionCount++;
        long offset = 0;
        while (offset < regionSize)
        {
            long thisChunk = Math.Min(chunkSize, regionSize - offset);
            byte[]? buffer;
            try
            {
                buffer = ProcessManager.ReadProcessMemory(regionBase + offset, (uint)thisChunk);
            }
            catch
            {
                break;
            }
            if (buffer == null || buffer.Length == 0) break;

            totalBytes += buffer.Length;

            int searchFrom = 0;
            while (true)
            {
                int idx = IndexOfBytes(buffer, searchFrom, needle, caseSensitive);
                if (idx < 0) break;

                long hitAddress = regionBase + offset + idx;
                hits++;
                Console.WriteLine($"  MATCH  0x{hitAddress:X}  (region 0x{regionBase:X}, size 0x{regionSize:X})");
                searchFrom = idx + 1;
                if (hits >= maxHits)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Hit --max-hits ({maxHits}), stopping early.");
                    goto done;
                }
            }

            if (thisChunk < chunkSize) break;
            offset += thisChunk - (needle.Length - 1);
        }
    }

    done:
    Console.WriteLine();
    Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s. Scanned {regionCount} region(s), {totalBytes / 1024 / 1024:N0} MB, {hits} match(es).");
}

static int IndexOfBytes(byte[] haystack, int start, byte[] needle, bool caseSensitive)
{
    for (int i = start; i + needle.Length <= haystack.Length; i++)
    {
        bool match = true;
        for (int j = 0; j < needle.Length; j++)
        {
            byte a = haystack[i + j];
            byte b = needle[j];
            if (!caseSensitive)
            {
                if (a is >= (byte)'A' and <= (byte)'Z') a += 32;
                if (b is >= (byte)'A' and <= (byte)'Z') b += 32;
            }
            if (a != b)
            {
                match = false;
                break;
            }
        }
        if (match) return i;
    }
    return -1;
}

static void RunFindPointersTo(string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: find-pointers-to <addressHex> [--max-hits 50]");
        Console.WriteLine("Scans every committed/readable region of the process for an 8-byte-aligned");
        Console.WriteLine("qword exactly equal to <addressHex> - use this to find which object field(s)");
        Console.WriteLine("hold a pointer to a heap address you've already found (e.g. via scan-heap).");
        return;
    }

    long target = ParseHex(args[0], 0);
    int maxHits = (int)ParseHexOrDec(GetOption(args, "--max-hits"), 50);

    Console.WriteLine($"Scanning all committed/readable process memory for a pointer == 0x{target:X}...");
    Console.WriteLine();

    const long chunkSize = 16 * 1024 * 1024;
    long totalBytes = 0;
    int hits = 0;
    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var (regionBase, regionSize) in ProcessManager.EnumerateReadableRegions())
    {
        long offset = 0;
        while (offset < regionSize)
        {
            long thisChunk = Math.Min(chunkSize, regionSize - offset);
            byte[]? buffer;
            try
            {
                buffer = ProcessManager.ReadProcessMemory(regionBase + offset, (uint)thisChunk);
            }
            catch
            {
                break;
            }
            if (buffer == null || buffer.Length < 8)
            {
                offset += thisChunk;
                continue;
            }

            totalBytes += buffer.Length;

            for (int i = 0; i + 8 <= buffer.Length; i += 8)
            {
                long value = BitConverter.ToInt64(buffer, i);
                if (value == target)
                {
                    long hitAddress = regionBase + offset + i;
                    hits++;
                    Console.WriteLine($"  MATCH  field @ 0x{hitAddress:X}  (in region 0x{regionBase:X}, size 0x{regionSize:X})");
                    if (hits >= maxHits)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Hit --max-hits ({maxHits}), stopping early.");
                        goto done;
                    }
                }
            }

            offset += thisChunk;
        }
    }

    done:
    Console.WriteLine();
    Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s. Scanned {totalBytes / 1024 / 1024:N0} MB, {hits} match(es).");
}

static void RunFindVtableBase(long moduleBaseAddress, long moduleSize, string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: find-vtable-base <fieldAddressHex> [--range 0x200]");
        Console.WriteLine("Walks backward 8 bytes at a time from <fieldAddressHex> looking for a qword");
        Console.WriteLine("that points inside the target module (game_plugin.dll) - the object's vtable");
        Console.WriteLine("is conventionally at the object's own base (+0x0). Use on a field address found");
        Console.WriteLine("via find-pointers-to to recover the containing object's base and vtable RVA.");
        return;
    }

    long fieldAddress = ParseHex(args[0], 0);
    long range = ParseHex(GetOption(args, "--range"), 0x200);

    Console.WriteLine($"Scanning backward from 0x{fieldAddress:X} (range 0x{range:X}) for a plausible object base (vtable at +0x0 inside the module)...");
    Console.WriteLine();

    int hits = 0;
    for (long candidate = fieldAddress; candidate >= fieldAddress - range; candidate -= 8)
    {
        long vtbl;
        try
        {
            vtbl = ProcessManager.ReadInt64(candidate);
        }
        catch
        {
            continue;
        }

        if (vtbl < moduleBaseAddress || vtbl >= moduleBaseAddress + moduleSize) continue;

        long rva = vtbl - moduleBaseAddress;
        long fieldOffset = fieldAddress - candidate;
        hits++;
        Console.WriteLine($"  candidate base=0x{candidate:X}  vtableRVA=0x{rva:X}  field at +0x{fieldOffset:X}");
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {hits} candidate(s). Cross-check vtableRVA against other known objects to confirm which is the real class vtable.");
}

static void RunFindPointersInRange(long moduleBaseAddress, long moduleSize, string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: find-pointers-in-range <loAddressHex> <hiAddressHex>");
        Console.WriteLine("Scans the target module's OWN static image (not heap) for any 8-aligned");
        Console.WriteLine("qword whose value falls within [lo, hi) - use this to find the static");
        Console.WriteLine("global field that roots a heap region/array you've already located, instead");
        Console.WriteLine("of blindly brute-forcing table offsets/xors across the whole module.");
        return;
    }

    long lo = ParseHex(args[0], 0);
    long hi = ParseHex(args[1], 0);
    long from = ParseHex(GetOption(args, "--from"), 0x0);
    long to = ParseHex(GetOption(args, "--to"), moduleSize);

    Console.WriteLine($"Scanning module static image RVA [0x{from:X}, 0x{to:X}) for a pointer in [0x{lo:X}, 0x{hi:X})...");
    Console.WriteLine();

    var snapshot = ReadModuleSnapshot(moduleBaseAddress + from, to - from);
    int hits = 0;

    for (long i = 0; i + 8 <= snapshot.LongLength; i += 8)
    {
        long value = BitConverter.ToInt64(snapshot, (int)i);
        if (value < lo || value >= hi) continue;

        long rva = from + i;
        hits++;
        Console.WriteLine($"  MATCH  staticRVA=0x{rva:X}  (abs 0x{moduleBaseAddress + rva:X})  value=0x{value:X}");
    }

    Console.WriteLine();
    Console.WriteLine($"Done. {hits} match(es).");
}

static void RunScanHeapInt(string[] args)
{
    if (args.Length < 1)
    {
        Console.WriteLine("Usage: scan-heap-int <decimalValue> [--width 4|8|both] [--max-hits 50]");
        Console.WriteLine("Searches every committed/readable region of the process for an aligned");
        Console.WriteLine("occurrence of <decimalValue> as a little-endian Int32 and/or Int64 - use");
        Console.WriteLine("this for a stable numeric anchor (e.g. a manager/save unique ID) instead");
        Console.WriteLine("of a display string, which is often rebuilt into a scratch buffer per-frame.");
        return;
    }

    long value = long.Parse(args[0]);
    string width = GetOption(args, "--width") ?? "both";
    int maxHits = (int)ParseHexOrDec(GetOption(args, "--max-hits"), 50);

    var needles = new List<(int stride, byte[] bytes)>();
    if (width is "4" or "both") needles.Add((4, BitConverter.GetBytes((int)value)));
    if (width is "8" or "both") needles.Add((8, BitConverter.GetBytes(value)));

    Console.WriteLine($"Scanning all committed/readable process memory for aligned {value} ({string.Join(", ", needles.Select(n => $"{n.stride * 8}-bit"))})...");
    Console.WriteLine();

    const long chunkSize = 16 * 1024 * 1024;
    long totalBytes = 0;
    int hits = 0;
    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var (regionBase, regionSize) in ProcessManager.EnumerateReadableRegions())
    {
        long offset = 0;
        while (offset < regionSize)
        {
            long thisChunk = Math.Min(chunkSize, regionSize - offset);
            long readSize = Math.Min(thisChunk + 8, regionSize - offset);
            byte[]? buffer;
            try
            {
                buffer = ProcessManager.ReadProcessMemory(regionBase + offset, (uint)readSize);
            }
            catch
            {
                break;
            }
            if (buffer == null || buffer.Length < 4)
            {
                offset += thisChunk;
                continue;
            }

            totalBytes += thisChunk;

            foreach (var (stride, needleBytes) in needles)
            {
                for (long i = 0; i + needleBytes.Length <= buffer.Length && i < thisChunk; i += stride)
                {
                    bool match = true;
                    for (int j = 0; j < needleBytes.Length; j++)
                    {
                        if (buffer[i + j] != needleBytes[j]) { match = false; break; }
                    }
                    if (!match) continue;

                    long hitAddress = regionBase + offset + i;
                    hits++;
                    Console.WriteLine($"  MATCH  0x{hitAddress:X}  ({stride * 8}-bit, region 0x{regionBase:X}, size 0x{regionSize:X})");
                    if (hits >= maxHits)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Hit --max-hits ({maxHits}), stopping early.");
                        goto done;
                    }
                }
            }

            offset += thisChunk;
        }
    }

    done:
    Console.WriteLine();
    Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s. Scanned {totalBytes / 1024 / 1024:N0} MB, {hits} match(es).");
}

static void RunScanHeapArrays(long moduleBaseAddress, long moduleSize, string[] args)
{
    long minCount = ParseHexOrDec(GetOption(args, "--min"), 50000);
    long maxCount = ParseHexOrDec(GetOption(args, "--max"), 300000);
    int maxHits = (int)ParseHexOrDec(GetOption(args, "--max-hits"), 50);
    bool skipVtableCheck = args.Any(a => string.Equals(a, "--no-vtable-check", StringComparison.OrdinalIgnoreCase));

    Console.WriteLine($"Scanning all committed/readable process memory for a heap-resident array-bounds pair (count {minCount:N0}-{maxCount:N0})...");
    Console.WriteLine("Use this to find a large master table (e.g. the ~120k-entry Person list) directly,");
    Console.WriteLine("without needing to already know the static global struct that roots it.");
    Console.WriteLine();

    const long chunkSize = 16 * 1024 * 1024;
    long totalBytes = 0;
    int hits = 0;
    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var (regionBase, regionSize) in ProcessManager.EnumerateReadableRegions())
    {
        long offset = 0;
        while (offset < regionSize)
        {
            long thisChunk = Math.Min(chunkSize, regionSize - offset);
            long readSize = Math.Min(thisChunk + 8, regionSize - offset);
            byte[]? buffer;
            try
            {
                buffer = ProcessManager.ReadProcessMemory(regionBase + offset, (uint)readSize);
            }
            catch
            {
                break;
            }
            if (buffer == null || buffer.Length < 16)
            {
                offset += thisChunk;
                continue;
            }

            totalBytes += thisChunk;

            for (long i = 0; i + 16 <= buffer.Length; i += 8)
            {
                long start = BitConverter.ToInt64(buffer, (int)i);
                long end = BitConverter.ToInt64(buffer, (int)(i + 8));
                if (!LooksLikePointer(start) || end < start) continue;

                long count = (end - start) / 8;
                if (count < minCount || count > maxCount) continue;

                string verdict = "(vtable check skipped)";
                if (!skipVtableCheck)
                {
                    long element0;
                    long vtbl;
                    try
                    {
                        element0 = ProcessManager.ReadInt64(start);
                        if (!LooksLikePointer(element0)) continue;
                        vtbl = ProcessManager.ReadInt64(element0);
                    }
                    catch
                    {
                        continue;
                    }
                    if (vtbl < moduleBaseAddress || vtbl >= moduleBaseAddress + moduleSize) continue;
                    verdict = $"element[0] vtable RVA=0x{vtbl - moduleBaseAddress:X}";
                }

                long hitAddress = regionBase + offset + i;
                hits++;
                Console.WriteLine($"  MATCH  boundsAddr=0x{hitAddress:X}  bounds=0x{start:X}..0x{end:X}  count={count:N0}  {verdict}");
                if (hits >= maxHits)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Hit --max-hits ({maxHits}), stopping early.");
                    goto done;
                }
            }

            offset += thisChunk;
        }
    }

    done:
    Console.WriteLine();
    Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s. Scanned {totalBytes / 1024 / 1024:N0} MB, {hits} match(es).");
}

// rtti-instances <Class> [--part N] [--contains <valueHex> [--range 0x800]] [--max 50]
// Finds live objects of a class by scanning process memory for qwords equal to the class's vtable
// address (an object's first qword at the sub-object offset). With --contains, only instances that
// hold <valueHex> as an 8-byte-aligned qword within [object base, hit + range) are reported, with
// the offset(s) relative to the hit (the vtable slot / sub-object address).
static void RunRttiInstances(RttiImage image, long moduleBase, string[] args)
{
    if (args.Length < 1 || args[0].StartsWith("--", StringComparison.Ordinal))
    {
        Console.WriteLine("Usage: rtti-instances <ClassName> [--part <subObjectOffset>] [--contains <valueHex> [--range 0x800]] [--max 50]");
        return;
    }

    long? part = GetOption(args, "--part") is { } p ? ParseHexOrDec(p, 0) : null;
    long? contains = GetOption(args, "--contains") is { } c ? ParseHex(c, 0) : null;
    long range = ParseHexOrDec(GetOption(args, "--range"), 0x800);
    int max = (int)ParseHexOrDec(GetOption(args, "--max"), 50);

    // vtable absolute address -> sub-object offset of that vtable inside the complete object
    var targets = new Dictionary<long, (long VtableRva, int Part)>();
    foreach (string raw in RttiImage.RawCandidates(args[0]))
    {
        foreach (long td in image.FindExactTypeDescriptors(raw))
        {
            foreach (var col in image.LocatorsFor(td))
            {
                if (part.HasValue && col.SubObjectOffset != part.Value) continue;
                foreach (long vt in image.VtablesFor(col.Rva))
                {
                    targets[moduleBase + vt] = (vt, col.SubObjectOffset);
                }
            }
        }
    }

    if (targets.Count == 0)
    {
        Console.WriteLine("No vtable found for that class/part (see rtti-vtable / rtti-list).");
        return;
    }

    foreach (var (abs, info) in targets)
    {
        Console.WriteLine($"Looking for vtable RVA 0x{info.VtableRva:X} (sub-object +{info.Part}) = 0x{abs:X}");
    }

    long lo = targets.Keys.Min();
    long hi = targets.Keys.Max();
    const long chunkSize = 16 * 1024 * 1024;
    int instances = 0, reported = 0;
    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var (regionBase, regionSize) in ProcessManager.EnumerateReadableRegions())
    {
        for (long offset = 0; offset < regionSize; offset += chunkSize)
        {
            long thisChunk = Math.Min(chunkSize, regionSize - offset);
            byte[]? buffer;
            try { buffer = ProcessManager.ReadProcessMemory(regionBase + offset, (uint)thisChunk); }
            catch { break; }
            if (buffer == null || buffer.Length < 8) continue;

            for (int i = 0; i + 8 <= buffer.Length; i += 8)
            {
                long value = BitConverter.ToInt64(buffer, i);
                if (value < lo || value > hi || !targets.TryGetValue(value, out var info)) continue;

                instances++;
                long hit = regionBase + offset + i;
                if (reported >= max && !contains.HasValue) continue;

                if (!contains.HasValue)
                {
                    reported++;
                    Console.WriteLine($"  instance: hit @ 0x{hit:X}  object base @ 0x{hit - info.Part:X}  (vtable +{info.Part})");
                    continue;
                }

                long start = hit - info.Part;
                int length = (int)(info.Part + range);
                byte[]? block = ProcessManager.ReadProcessMemory(start, (uint)length, out long got);
                if (block == null || got < length) continue;

                var offsets = new List<long>();
                for (int k = 0; k + 8 <= length; k += 8)
                {
                    if (BitConverter.ToInt64(block, k) == contains.Value) offsets.Add(k - info.Part);
                }

                if (offsets.Count > 0 && reported < max)
                {
                    reported++;
                    Console.WriteLine($"  MATCH: hit @ 0x{hit:X}  object base @ 0x{start:X}  holds 0x{contains.Value:X} at hit+{string.Join(", hit+", offsets.Select(o => $"0x{o:X} ({o})"))}");
                }
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s. {instances:N0} instance(s) of the class found, {reported} reported.");
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

          rtti-name <vtableRvaHex>
              Name the C++ class that owns the vtable at this module RVA, by reading its MSVC RTTI
              Complete Object Locator (vtable[-1]). Also prints the sub-object offset and the base
              classes. Use --module game_plugin.dll. Try it on a known vtable such as the FM27
              singleton at 0x4B05678 to learn what class it really is.

          rtti-vtable <ClassName> [--part <subObjectOffset>]
              Find the vtable RVA(s) of a class by name (TEAM@db, db::TEAM or .?AVTEAM@db@@), one per
              sub-object. Classes with multiple inheritance have several vtables - pick the one whose
              sub-object offset matches the part of the object you care about.

          rtti-list [substring] [--max 200]
              List RTTI type descriptors in the module whose name contains the substring (case-
              insensitive). With 25 or fewer matches, also prints each vtable.

          rtti-object <addressHex> [--count 48]
              Dump a live object's qwords. A pointer to another object is labelled with that object's
              RTTI class (read from its vtable), so links to person/club/etc. appear by name. Live only.

          rtti-instances <ClassName> [--part N] [--contains <valueHex> [--range 0x800]] [--max 50]
              Find live objects of a class by scanning the heap for its vtable address. With
              --contains, report only instances holding that qword (e.g. a team pointer) and the
              offset it sits at, relative to the vtable slot. Live only.

          fm27-managers
              End-to-end check of the RTTI-based FM27 support: runs FMCore.LoadData() (version
              detection included) and lists the human manager(s) of the loaded save with their club.
              Needs a save to be loaded. Does not take --module.

          fm27-players [--name <text>] [--max 15]
              Reads every player through the RTTI-based FM27 support, prints sanity statistics over
              the whole population, cross-checks the legacy core.Players entities against them, and
              lists players (optionally filtered by name). Needs a save to be loaded.

          fm27-roles [--name <text>] [--max 10]
              Validates the position and stored top-role data over every player (best position vs.
              the roles the game assigns, goalkeeper consistency) and prints positions + roles for the
              top players by CA, or for players matching --name. Needs a save to be loaded.

          fm27-squads [--club <text>] [--max 30]
              Validates club membership and squads (owner club from the contract, current team and
              squad type, loans) over every player, spot-checks famous players' clubs, and lists the
              human manager's squad (or every squad of clubs matching --club). Needs a save loaded.

          Offline mode: add --file <path-to-game_plugin.dll> to any rtti-* command to run it against
          the DLL on disk without a running game.
        """);
}
