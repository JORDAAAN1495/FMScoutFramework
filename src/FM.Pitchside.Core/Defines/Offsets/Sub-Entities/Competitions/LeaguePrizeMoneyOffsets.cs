using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities.Competitions
{
    public sealed class LeaguePrizeMoneyOffsets
    {
        public IVersion Version;

        public LeaguePrizeMoneyOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short Amount
        {
            get
            {
                return 0x0;
            }
        }
    }
}