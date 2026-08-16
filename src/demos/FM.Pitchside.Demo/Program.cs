using FMScoutFramework.Core;

var core = new FMCore();

core.LoadData();

var players = core.Players
    .Where(x => x.ActualPerson.LastName.Equals("Rashford", StringComparison.OrdinalIgnoreCase))
    .ToList();

foreach (var player in players)
{
    Console.WriteLine(player.ActualPerson.VisibleName);
    Console.WriteLine(player.Team.Name);
}