using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities.Competitions
{
    public sealed class LeagueTableEntryOffsets
    {
        public IVersion Version;

        public LeagueTableEntryOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short GoalsScored
        {
            get
            {
                return 0x8;
            }
        }

        public short GoalsAgainst
        {
            get
            {
                return 0xA;
            }
        }

        public short Points
        {
            get
            {
                return 0xC;
            }
        }

        public short GamesPlayed
        {
            get
            {
                return 0xF;
            }
        }

        public short GamesWon
        {
            get
            {
                return 0x10;
            }
        }

        public short GamesDrawn
        {
            get
            {
                return 0x11;
            }
        }

        public short GamesLost
        {
            get
            {
                return 0x12;
            }
        }

        public short Team
        {
            get
            {
                return 0x78;
            }
        }
    }
}