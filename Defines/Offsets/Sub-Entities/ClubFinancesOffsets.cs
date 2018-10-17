using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class ClubFinancesOffsets
    {
        public IVersion Version;

        public ClubFinancesOffsets (IVersion version)
        {
            this.Version = version;
        }

        public const short Balance = 0x18;
        public const short AverageTicketPrice = 0x1C;
        public const short AverageSeasonTicketPrice = 0x20;
        public const short MatchTicketPriceRatio = 0x24;
        public const short SeasonTicketPriceRatio = 0x28;
        public const short RatioForChangeInSeasonTicketHolders = 0x2C;
        public const short EmbargoStartDate = 0x30;
        public const short EmbargoEndDate = 0x34;
        public const short EmbargoAppealDate = 0x38;
        public const short SugarDaddy = 0x3D;
        public const short RemainingBudget = 0x67C;
        public const short SeasonTransferFunds = 0x680;
        public const short TransferIncomePercentage = 0x684;
        public const short YouthGrantIncome = 0x68C;
        public const short StadiumRentalPerYear = 0x6A8;
        public const short StartingLastYearsTurnover = 0x6AC;
        public const short WeeklyWageBudget = 0x6B4;
        public const short HighestWage = 0x6B8;
        public const short WeeklyWageBudgetUsed = 0x6C0;
        public const short HighestWagePaid = 0x6D4;
        public const short HighestNonPlayerWagePaid = 0x6D8;
        public const short LatestSeasonTicketSales = 0x70C;
        public const short FFPMaxWeeklyWageTotal = 0x6F4;
        public const short EnteredFSBankruptState = 0x498;
        public const short TrainingExpansionFlag = 0x4A6;
        public const short YouthExpansionFlag = 0x4A7;
        public const short StateOfEmergency = 0x74D;
        public const short StadiumRentalPercentageOfGateReceipts = 0x4B1;
        public const short CorporateFacilitiesRevenueLevel = 0x4B2;
        public const short IncreaseStaffWages = 0x4B3;
        public const short CorporateFacilities = 0x759;
    }
}
