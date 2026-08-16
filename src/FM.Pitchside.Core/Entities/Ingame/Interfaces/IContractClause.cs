namespace FM.Pitchside.Core.Entities.Ingame.Interfaces
{
    public interface IContractClause
    {
        sbyte Info { get; }
        sbyte Type { get; }
        int Value { get; }
    }
}