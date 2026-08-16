using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities.Competitions
{
    public sealed class ActualCompetitionOffsets
    {
        public IVersion Version;

        public ActualCompetitionOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short StagesOne
        {
            get
            {
                return 0x1A8;
            }
        }

        public short StagesTwo
        {
            get
            {
                return 0x1E8;
            }
        }
    }
}