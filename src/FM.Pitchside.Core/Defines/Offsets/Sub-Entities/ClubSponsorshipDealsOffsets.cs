using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities
{
    public sealed class ClubSponsorshipDealsOffsets
    {
        public IVersion Version;
        public ClubSponsorshipDealsOffsets(IVersion version)
        {
            Version = version;
        }

        public const short StartDate = 0x0;
        public const short EndDate = 0x4;
        public const short TotalIncome = 0x8;
        public const short ValuePerSeason = 0xC;
        public const short SponsorshipType = 0x12;
    }
}