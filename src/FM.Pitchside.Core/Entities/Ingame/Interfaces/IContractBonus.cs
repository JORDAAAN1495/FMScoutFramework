namespace FM.Pitchside.Core.Entities.Ingame.Interfaces
{
    public interface IContractBonus
    {
        byte Type { get; set; }
        int Value { get; set; }
        byte Info { get; set; }
    }
}