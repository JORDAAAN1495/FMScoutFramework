using FM.Pitchside.Core;

var core = new FMCore();

core.LoadData();

if (core.Fm27 != null)
{
    // FM27 is located through RTTI rather than the legacy master tables. Today it can read the human
    // manager(s), the players (with positions, roles and clubs), squads, and every backroom staff
    // member/manager in the world (~54k live at once - job/role isn't decoded yet, see
    // Fm27Game.FindStaff); the other legacy tables are not mapped yet.
    var fm27Players = core.Fm27.FindPlayers();
    var fm27Squads = core.Fm27.FindSquads(fm27Players);
    var fm27Staff = core.Fm27.FindStaff();

    foreach (var manager in core.FindHumanManagers())
    {
        Console.WriteLine($"{manager.DisplayName} manages {manager.ClubName}");

        var squad = fm27Squads.FirstOrDefault(s => s.TeamAddress == manager.TeamAddress);
        if (squad != null)
        {
            Console.WriteLine($"{squad.Squad}: {squad.Players.Count} players, best by ability:");
            foreach (var p in squad.Players.OrderByDescending(x => x.CurrentAbility).Take(5))
            {
                Console.WriteLine($"  {p.DisplayName,-22} {string.Join("/", p.BestPositions),-10} CA {p.CurrentAbility}  {string.Join(" > ", p.InPossessionRoles.Take(1))}");
            }
        }

        var clubStaff = fm27Staff.Where(s => s.ClubAddress == manager.ClubAddress).ToList();
        Console.WriteLine($"{manager.ClubName} staff: {clubStaff.Count}");
        foreach (var s in clubStaff.Take(10))
        {
            Console.WriteLine($"  {s.DisplayName}");
        }
    }

    Console.WriteLine($"{fm27Staff.Count} staff/manager record(s) loaded in total.");

    var fm27Matches = fm27Players
        .Where(x => x.DisplayName.Contains("Mainoo", StringComparison.OrdinalIgnoreCase))
        .ToList();

    Console.WriteLine($"{fm27Players.Count} players loaded, {fm27Matches.Count} matched \"Yoro\":");
    foreach (var player in fm27Matches)
    {
        Console.WriteLine($"{player.DisplayName} ({player.Nation}, born {player.BirthDate:yyyy-MM-dd}, CA {player.CurrentAbility}/PA {player.PotentialAbility})");
    }

    return;
}

var players = core.Players
    .Where(x => x.ActualPerson.FullName.Contains("Yoro", StringComparison.OrdinalIgnoreCase))
    .ToList();

Console.WriteLine($"{core.Players.Count()} players loaded, {players.Count} matched \"Yoro\":");
foreach (var player in players)
{
    Console.WriteLine($"{player.ActualPerson.FirstName} {player.ActualPerson.LastName}");
}