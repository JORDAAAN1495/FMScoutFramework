namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IContractClause
    {
        sbyte Info { get; }
        sbyte Type { get; }
        int Value { get; }
    }
}