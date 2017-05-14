using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Utilities;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public enum SugarDaddyType
    {
        [Description("None")]
        SDT_NONE = 0,
        [Description("Foreground")]
        SDT_FOREGROUND = 1,
        [Description("Background")]
        SDT_BACKGROUND = 2,
        [Description("Underwriter")]
        SDT_UNDERWRITER = 3
    }

    public class ClubFinances : BaseObject, IClubFinances
    {
        public ClubFinancesOffsets ClubFinancesOffsets;
        public ClubFinances (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        {
            this.ClubFinancesOffsets = new ClubFinancesOffsets (version);
        }
        public ClubFinances (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        {
            this.ClubFinancesOffsets = new ClubFinancesOffsets (version);
        }

        public void Save() {
            #region Balance Encrypter
            int rotateAmount = (int)(MemoryAddress + ClubFinancesOffsets.Balance) & 0x1F;
            uint decryptedBalance = (uint)Balance;
            if (Version.GetType() != typeof(Steam_17_2_0_Windows) &&
                Version.GetType() != typeof(Steam_17_2_1_Windows) &&
                Version.GetType() != typeof(Steam_17_3_0_Windows) &&
                Version.GetType() != typeof(Steam_17_3_1_Windows) &&
                Version.GetType() != typeof(Steam_Touch_17_2_0_Windows) &&
                Version.GetType() != typeof(Steam_Touch_17_3_0_Windows) &&
                Version.GetType() != typeof(Steam_Touch_17_3_1_Windows)) {
                decryptedBalance = BitwiseOperations.rol(decryptedBalance, rotateAmount);
                decryptedBalance = decryptedBalance ^ 0x16F175CB;
                decryptedBalance = BitwiseOperations.ror(decryptedBalance, 0x16);
                decryptedBalance = ~decryptedBalance;
            }
            #endregion
            PropertyInvoker.Set<uint>(ClubFinancesOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode, decryptedBalance);
            PropertyInvoker.Set<float>(ClubFinancesOffsets.AverageTicketPrice, OriginalBytes, MemoryAddress, DatabaseMode, AverageTicketPrice);
            PropertyInvoker.Set<float>(ClubFinancesOffsets.AverageSeasonTicketPrice, OriginalBytes, MemoryAddress, DatabaseMode, AverageSeasonTicketPrice);
            PropertyInvoker.Set<float>(ClubFinancesOffsets.MatchTicketPriceRatio, OriginalBytes, MemoryAddress, DatabaseMode, MatchTicketPriceRatio);
            PropertyInvoker.Set<float>(ClubFinancesOffsets.SeasonTicketPriceRatio, OriginalBytes, MemoryAddress, DatabaseMode, SeasonTicketPriceRatio);
            PropertyInvoker.Set<DateTime>(ClubFinancesOffsets.EmbargoStartDate, OriginalBytes, MemoryAddress, DatabaseMode, EmbargoStartDate);
            PropertyInvoker.Set<DateTime>(ClubFinancesOffsets.EmbargoEndDate, OriginalBytes, MemoryAddress, DatabaseMode, EmbargoEndDate);
            PropertyInvoker.Set<DateTime>(ClubFinancesOffsets.EmbargoAppealDate, OriginalBytes, MemoryAddress, DatabaseMode, EmbargoAppealDate);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.RemainingBudget, OriginalBytes, MemoryAddress, DatabaseMode, RemainingBudget);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.SeasonTransferFunds, OriginalBytes, MemoryAddress, DatabaseMode, SeasonTransferFunds);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.TransferIncomePercentage, OriginalBytes, MemoryAddress, DatabaseMode, TransferIncomePercentage);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.YouthGrantIncome, OriginalBytes, MemoryAddress, DatabaseMode, YouthGrantIncome);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.WeeklyWageBudget, OriginalBytes, MemoryAddress, DatabaseMode, WeeklyWageBudget);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.HighestWage, OriginalBytes, MemoryAddress, DatabaseMode, HighestWage);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.WeeklyWageBudgetUsed, OriginalBytes, MemoryAddress, DatabaseMode, WeeklyWageBudgetUsed);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.HighestWagePaid, OriginalBytes, MemoryAddress, DatabaseMode, HighestWagePaid);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.HighestNonPlayerWagePaid, OriginalBytes, MemoryAddress, DatabaseMode, HighestNonPlayerWagePaid);
            PropertyInvoker.Set<int>(ClubFinancesOffsets.LatestSeasonTicketSales, OriginalBytes, MemoryAddress, DatabaseMode, LatestSeasonTickets);
            PropertyInvoker.Set<byte>(ClubFinancesOffsets.SugarDaddy, OriginalBytes, MemoryAddress, DatabaseMode, SugarDaddy);
            _isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty {
            get {
                return _isDirty;
            }
            set {
                if (value) {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        private int _balance = 0;
        public int Balance {
            get {
                if (_balance == 0) {
                    int rotateAmount = (int)(MemoryAddress + ClubFinancesOffsets.Balance) & 0x1F;
                    uint encryptedBalance = PropertyInvoker.Get<uint>(ClubFinancesOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode);

                    if (Version.GetType() != typeof(Steam_17_2_0_Windows) &&
                        Version.GetType() != typeof(Steam_17_2_1_Windows) &&
                        Version.GetType() != typeof(Steam_17_3_0_Windows) &&
                        Version.GetType() != typeof(Steam_17_3_1_Windows) &&
                        Version.GetType() != typeof(Steam_Touch_17_2_0_Windows) &&
                        Version.GetType() != typeof(Steam_Touch_17_3_0_Windows) &&
                        Version.GetType() != typeof(Steam_Touch_17_3_1_Windows)) {
                        encryptedBalance = ~encryptedBalance;
                        encryptedBalance = BitwiseOperations.rol(encryptedBalance, 0x16);
                        encryptedBalance = encryptedBalance ^ 0x16F175CB;
                        encryptedBalance = BitwiseOperations.ror(encryptedBalance, rotateAmount);
                    }

                    _balance = (int)encryptedBalance;
                }
                return _balance;
            }
            set {
                if (_balance != value) {
                    _balance = value;
                    isDirty = true;
                }
            }
        }

        private float _averageTicketPrice = 0.0f;
        public float AverageTicketPrice {
            get {
                if (_averageTicketPrice == 0.0f) {
                    _averageTicketPrice = PropertyInvoker.Get<float>(ClubFinancesOffsets.AverageTicketPrice, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _averageTicketPrice;
            }
            set {
                if (_averageTicketPrice != value) {
                    _averageTicketPrice = value;
                    isDirty = true;
                }
            }
        }

        private float _averageSeasonTicketPrice = 0.0f;
        public float AverageSeasonTicketPrice {
            get {
                if (_averageSeasonTicketPrice == 0.0f) {
                    _averageSeasonTicketPrice = PropertyInvoker.Get<float>(ClubFinancesOffsets.AverageSeasonTicketPrice, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _averageSeasonTicketPrice;
            }
            set {
                if (_averageSeasonTicketPrice != value) {
                    _averageSeasonTicketPrice = value;
                    isDirty = true;
                }
            }
        }

        private float _matchTicketPriceRatio = 0.0f;
        public float MatchTicketPriceRatio {
            get {
                if (_matchTicketPriceRatio == 0.0f) {
                    _matchTicketPriceRatio = PropertyInvoker.Get<float>(ClubFinancesOffsets.MatchTicketPriceRatio, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _matchTicketPriceRatio;
            }
            set {
                if (_matchTicketPriceRatio != value) {
                    _matchTicketPriceRatio = value;
                    isDirty = true;
                }
            }
        }

        private float _seasonTicketPriceRatio = 0.0f;
        public float SeasonTicketPriceRatio {
            get {
                if (_seasonTicketPriceRatio == 0.0f) {
                    _seasonTicketPriceRatio = PropertyInvoker.Get<float>(ClubFinancesOffsets.SeasonTicketPriceRatio, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _seasonTicketPriceRatio;
            }
            set {
                if (_seasonTicketPriceRatio != value) {
                    _seasonTicketPriceRatio = value;
                    _isDirty = true;
                }
            }
        }

        private float _ratioForChangeInSeasonTicketHolders = 0.0f;
        public float RatioForChangeInSeasonTicketHolders {
            get {
                if (_ratioForChangeInSeasonTicketHolders == 0.0f) {
                    _ratioForChangeInSeasonTicketHolders = PropertyInvoker.Get<float>(ClubFinancesOffsets.RatioForChangeInSeasonTicketHolders, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _ratioForChangeInSeasonTicketHolders;
            }
            set {
                if (_ratioForChangeInSeasonTicketHolders != value) {
                    _ratioForChangeInSeasonTicketHolders = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _embargoStartDate;
        public DateTime EmbargoStartDate {
            get {
                if (_embargoStartDate.Year < 1970) {
                    _embargoStartDate = PropertyInvoker.Get<DateTime>(ClubFinancesOffsets.EmbargoStartDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _embargoStartDate;
            }
            set {
                if (_embargoStartDate != value) {
                    _embargoStartDate = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _embargoEndDate;
        public DateTime EmbargoEndDate {
            get {
                if (_embargoEndDate.Year < 1970) {
                    _embargoEndDate = PropertyInvoker.Get<DateTime>(ClubFinancesOffsets.EmbargoEndDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _embargoEndDate;
            }
            set {
                if (_embargoEndDate != value) {
                    _embargoEndDate = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _embargoAppealDate;
        public DateTime EmbargoAppealDate {
            get {
                if (_embargoAppealDate.Year < 1970) {
                    _embargoAppealDate = PropertyInvoker.Get<DateTime>(ClubFinancesOffsets.EmbargoAppealDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _embargoAppealDate;
            }
            set {
                if (_embargoAppealDate != value) {
                    _embargoAppealDate = value;
                    isDirty = true;
                }
            }
        }

        private int _remainingBudget = 0;
        public int RemainingBudget {
            get {
                if (_remainingBudget == 0) {
                    _remainingBudget = PropertyInvoker.Get<int>(ClubFinancesOffsets.RemainingBudget, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _remainingBudget;
            }
            set {
                if (_remainingBudget != value) {
                    _remainingBudget = value;
                    isDirty = true;
                }
            }
        }

        private int _seasonTransferFunds = 0;
        public int SeasonTransferFunds {
            get {
                if (_seasonTransferFunds == 0) {
                    _seasonTransferFunds = PropertyInvoker.Get<int>(ClubFinancesOffsets.SeasonTransferFunds, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _seasonTransferFunds;
            }
            set {
                if (_seasonTransferFunds != value) {
                    _seasonTransferFunds = value;
                    isDirty = true;
                }
            }
        }

        private int _transferIncomePercentage = 0;
        public int TransferIncomePercentage {
            get {
                if (_transferIncomePercentage == 0) {
                    _transferIncomePercentage = PropertyInvoker.Get<int>(ClubFinancesOffsets.TransferIncomePercentage, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _transferIncomePercentage;
            }
            set {
                if (_transferIncomePercentage != value) {
                    _transferIncomePercentage = value;
                    isDirty = true;
                }
            }
        }

        private int _youthGrantIncome = 0;
        public int YouthGrantIncome {
            get {
                if (_youthGrantIncome == 0) {
                    _youthGrantIncome = PropertyInvoker.Get<int>(ClubFinancesOffsets.YouthGrantIncome, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _youthGrantIncome;
            }
            set {
                if (_youthGrantIncome != value) {
                    _youthGrantIncome = value;
                    isDirty = true;
                }
            }
        }

        private int _weeklyWageBudget = 0;
        public int WeeklyWageBudget {
            get {
                if (_weeklyWageBudget == 0) {
                    _weeklyWageBudget = PropertyInvoker.Get<int>(ClubFinancesOffsets.WeeklyWageBudget, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _weeklyWageBudget;
            }
            set {
                if (_weeklyWageBudget != value) {
                    _weeklyWageBudget = value;
                    isDirty = true;
                }
            }
        }

        private int _highestWage = 0;
        public int HighestWage {
            get {
                if (_highestWage == 0) {
                    _highestWage = PropertyInvoker.Get<int>(ClubFinancesOffsets.HighestWage, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _highestWage;
            }
            set {
                if (_highestWage != value) {
                    _highestWage = value;
                    isDirty = true;
                }
            }
        }

        private int _weeklyWageBudgetUsed = 0;
        public int WeeklyWageBudgetUsed {
            get {
                if (_weeklyWageBudgetUsed == 0) {
                    _weeklyWageBudgetUsed = PropertyInvoker.Get<int>(ClubFinancesOffsets.WeeklyWageBudgetUsed, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _weeklyWageBudgetUsed;
            }
            set {
                if (_weeklyWageBudgetUsed != value) {
                    _weeklyWageBudgetUsed = value;
                    isDirty = true;
                }
            }
        }

        private int _highestWagePaid = 0;
        public int HighestWagePaid {
            get {
                if (_highestWagePaid == 0) {
                    _highestWagePaid = PropertyInvoker.Get<int>(ClubFinancesOffsets.HighestWagePaid, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _highestWagePaid;
            }
            set {
                if (_highestWagePaid != value) {
                    _highestWagePaid = value;
                    isDirty = true;
                }
            }
        }

        private int _highestNonPlayerWagePaid = 0;
        public int HighestNonPlayerWagePaid {
            get {
                if (_highestNonPlayerWagePaid == 0) {
                    _highestNonPlayerWagePaid = PropertyInvoker.Get<int>(ClubFinancesOffsets.HighestNonPlayerWagePaid, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _highestNonPlayerWagePaid;
            }
            set {
                if (_highestNonPlayerWagePaid != value) {
                    _highestNonPlayerWagePaid = value;
                    isDirty = true;
                }
            }
        }

        private int _latestSeasonTickets = 0;
        public int LatestSeasonTickets {
            get {
                if (_latestSeasonTickets == 0) {
                    _latestSeasonTickets = PropertyInvoker.Get<int>(ClubFinancesOffsets.LatestSeasonTicketSales, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _latestSeasonTickets;
            }
            set {
                if (_latestSeasonTickets != value) {
                    _latestSeasonTickets = value;
                    isDirty = true;
                }
            }
        }

        private byte _sugarDaddy = 0;
        public byte SugarDaddy {
            get {
                if (_sugarDaddy == 0) {
                    _sugarDaddy = PropertyInvoker.Get<byte>(ClubFinancesOffsets.SugarDaddy, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _sugarDaddy;
            }
            set {
                if (_sugarDaddy != value) {
                    _sugarDaddy = value;
                    isDirty = true;
                }
            }
        }
    }
}