using FM.Pitchside.Core.Entities.Ingame.Sub_Entities;

namespace FM.Pitchside.Core.Entities.Ingame.Interfaces
{
    public interface INation
    {
        int UID { get; }
        string Name { get; }
        string NationalityName { get; }
        RivalNation[] RivalNations { get; }
        string ShortName { get; }
        Team[] Teams { get; }
        string ThreeLetterName { get; }
    }
}