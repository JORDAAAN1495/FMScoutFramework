using FM.Pitchside.Core;

var core = new FMCore();

core.LoadData();

var players = core.Players
    .Where(x => x.ActualPerson.FullName.Contains("Yoro", StringComparison.OrdinalIgnoreCase))
    .ToList();

Console.WriteLine($"{core.Players.Count()} players loaded, {players.Count} matched \"Yoro\":");
foreach (var player in players)
{
    Console.WriteLine($"{player.ActualPerson.FirstName} {player.ActualPerson.LastName}");
}