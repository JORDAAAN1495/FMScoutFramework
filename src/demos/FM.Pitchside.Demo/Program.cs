using FM.Pitchside.Core;
using FM.Pitchside.Core.VirtualMemory.Managers;

var core = new FMCore();

core.LoadData();

// FM26 support is still early: Player.ActualPerson/.Team read through FM17-22-era offsets
// that don't apply to this build yet. Only the Person object's own FullName field has been
// located so far (see Steam_26_0_0_0_Windows.KnownFieldOffsets.PersonFullName), so read
// that directly rather than through the not-yet-updated ActualPerson/PlayerOffsets chain.
static string FullName(Int64 personAddress) => ProcessManager.ReadString(personAddress + 0x40, -1);

var players = core.Players
    .Select(x => new { Player = x, FullName = FullName(x.Address) })
    .Where(x => x.FullName.Contains("Yoro", StringComparison.OrdinalIgnoreCase))
    .ToList();

Console.WriteLine($"{core.Players.Count()} players loaded, {players.Count} matched \"Yoro\":");
foreach (var player in players)
{
    Console.WriteLine(player.FullName);
}