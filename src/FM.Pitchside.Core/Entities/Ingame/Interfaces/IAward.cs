namespace FM.Pitchside.Core.Entities.Ingame.Interfaces
{
    public interface IAward
    {
        int RowID { get; }
        int UID { get; }
        string Name { get; }
        byte Voting { get; set; }
    }
}