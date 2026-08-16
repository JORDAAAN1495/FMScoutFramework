namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IContractBonus
    {
        byte Type { get; set; }
        int Value { get; set; }
        byte Info { get; set; }
    }
}