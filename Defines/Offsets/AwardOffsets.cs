using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets
{
    public sealed class AwardOffsets
    {
        public IVersion Version;

        public AwardOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short RowID = 0x8;
        public const short UID = 0xC;
        public const short Name = 0x30;
        public const short ShortName = 0x38;
        public const short AwardDate = 0x48;
        public const short AnnouncementDate = 0x4C;
        public const short Position = 0x70;
        public const short RunBy = 0x78;
        public const short Period = 0x7A;
        public const short Voting = 0x7B;
        public const short Based = 0x7D;
        public const short RecipientType = 0x80;

        public const short Formation = 0x0;
        public const short MaximumAge = 0x0;
        public const short WinnerHomeReputation = 0x0;
        public const short WinnerWorldReputation = 0x0;
        
        public const short Side = 0x0;
        public const short UseStatsFrom = 0x0;
        public const short UseSubsForTeamAward = 0x0;
        public const short AllowPreviousWinner = 0x0;
        
        public const short VotingFormat = 0x0;
        public const short MinimumPercentageOfGamesPlayed = 0x0;
        
        public const short MinimumAge = 0x0;
        public const short Placings = 0x0;
        public const short AwardReputation = 0x0;
        public const short ContinentPtr = 0x0;
        public const short NationPtr = 0x0;
        public const short CompetitionPtr = 0x0;
        
        
    }
}