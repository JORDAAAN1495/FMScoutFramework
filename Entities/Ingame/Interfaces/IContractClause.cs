namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IContractClause
    {
        byte Info { get; }
        byte Type { get; }
        int Value { get; }
    }
}