using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities.Competitions
{
    public sealed class LeagueStageOffsets
    {
        public IVersion Version;

        public LeagueStageOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short LeagueTable
        {
            get
            {
                return 0x88;
            }
        }

        public short NumberOfTeams
        {
            get
            {
                return 0xA0;
            }
        }

        public short StageSettings
        {
            get
            {
                return 0xA8;
            }
        }
    }
}