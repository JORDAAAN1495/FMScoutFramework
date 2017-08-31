namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IAward
    {
        int RowID { get; }
        int UID { get; }
        string Name { get; }
        byte Voting { get; set; }
    }
}
