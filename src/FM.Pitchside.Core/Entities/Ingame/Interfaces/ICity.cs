namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface ICity
    {
        short Altitude { get; }
        short Attraction { get; }
        int UID { get; }
        float Latitude { get; }
        float Longitude { get; }
        string Name { get; }
        int NationPtr { get; }
    }
}