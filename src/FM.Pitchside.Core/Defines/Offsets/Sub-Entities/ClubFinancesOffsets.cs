using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities
{
    public sealed class ClubFinancesOffsets
    {
        public IVersion Version;

        public ClubFinancesOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short Balance = 0x14;
        public const short AverageTicketPrice = 0x18;
        public const short AverageSeasonTicketPrice = 0x1C;
        public const short MatchTicketPriceRatio = 0x20;
        public const short SeasonTicketPriceRatio = 0x24;
        public const short RatioForChangeInSeasonTicketHolders = 0x28;
        public const short EmbargoStartDate = 0x2C;
        public const short EmbargoEndDate = 0x30;
        public const short EmbargoAppealDate = 0x34;
        public const short SugarDaddy = 0x3C;
        public const short RemainingBudget = 0x7A4;
        public const short SeasonTransferFunds = 0x7A8;
        public const short TransferIncomePercentage = 0x7B0;
        public const short YouthGrantIncome = 0x7B4;
        public const short StadiumRentalPerYear = 0x7D8;
        public const short StartingLastYearsTurnover = 0x6B0;
        public const short WeeklyWageBudget = 0x7E8;
        public const short HighestWage = 0x7EC;
        public const short WeeklyWageBudgetUsed = 0x7F4;
        public const short HighestWagePaid = 0x808;
        public const short HighestNonPlayerWagePaid = 0x80C;
        public const short LatestSeasonTicketSales = 0x838;
        public const short FFPMaxWeeklyWageTotal = 0x6F4;
        public const short EnteredFSBankruptState = 0x498;
        public const short TrainingExpansionFlag = 0x4A6;
        public const short YouthExpansionFlag = 0x4A7;
        public const short StateOfEmergency = 0x745;
        public const short StadiumRentalPercentageOfGateReceipts = 0x4B1;
        public const short CorporateFacilitiesRevenueLevel = 0x4B2;
        public const short IncreaseStaffWages = 0x4B3;
        public const short CorporateFacilities = 0x88D;
    }
}