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
        public const short RemainingBudget = 0x48;
        public const short SeasonTransferFunds = 0x4C;
        public const short TransferIncomePercentage = 0x50;
        public const short YouthGrantIncome = 0x58;
        public const short StadiumRentalPerYear = 0x74;
        public const short StartingLastYearsTurnover = 0x78;
        public const short WeeklyWageBudget = 0x80;
        public const short HighestWage = 0x84;
        public const short WeeklyWageBudgetUsed = 0x8C;
        public const short HighestWagePaid = 0xA0;
        public const short HighestNonPlayerWagePaid = 0xA4;
        public const short LatestSeasonTicketSales = 0xD0;
        public const short FFPMaxWeeklyWageTotal = 0xC4;
        public const short EnteredFSBankruptState = 0x498;
        public const short TrainingExpansionFlag = 0x4A6;
        public const short YouthExpansionFlag = 0x4A7;
        public const short StateOfEmergency = 0x4AE;
        public const short StadiumRentalPercentageOfGateReceipts = 0x4B1;
        public const short CorporateFacilitiesRevenueLevel = 0x4B2;
        public const short IncreateStaffWages = 0x4B3;
    }
}
