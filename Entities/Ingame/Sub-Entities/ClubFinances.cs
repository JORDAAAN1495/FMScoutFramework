using System;
using System.Collections.Generic;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Attributes;
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
        public ClubFinances (int memoryAddress, IVersion version)
            : base (memoryAddress, version)
        {
            this.ClubFinancesOffsets = new ClubFinancesOffsets (version);
        }
        public ClubFinances (int memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        {
            this.ClubFinancesOffsets = new ClubFinancesOffsets (version);
        }

        public int Balance {
            get {
                int rotateAmount = (int)((MemoryAddress + ClubFinancesOffsets.Balance) & 0x1f);
                uint encryptedBalance = (uint)ProcessManager.ReadInt32 (MemoryAddress + ClubFinancesOffsets.Balance);

                encryptedBalance = BitwiseOperations.rol (encryptedBalance, rotateAmount);
                encryptedBalance = (encryptedBalance ^ 0xFAECECF1);
                encryptedBalance = ~encryptedBalance;
                encryptedBalance = BitwiseOperations.ror (encryptedBalance, 0x17);
                encryptedBalance = ~encryptedBalance;

                return (int)encryptedBalance;
            }
            set {
                int rotateAmount = (int)((MemoryAddress + ClubFinancesOffsets.Balance) & 0x1f);
                uint encryptedBalance = (uint)value;

                encryptedBalance = ~encryptedBalance;
                encryptedBalance = BitwiseOperations.rol (encryptedBalance, 0x17);
                encryptedBalance = ~encryptedBalance;
                encryptedBalance = (encryptedBalance ^ 0xFAECECF1);
                encryptedBalance = BitwiseOperations.ror (encryptedBalance, rotateAmount);

                PropertyInvoker.Set<int> (ClubFinancesOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int RemainingBudget {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.RemainingBudget, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.RemainingBudget, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int SeasonTransferFunds {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.SeasonTransferFunds, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.SeasonTransferFunds, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int TransferIncomePercentage {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.TransferIncomePercentage, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.TransferIncomePercentage, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int YouthGrantIncome {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.YouthGrantIncome, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.YouthGrantIncome, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int WeeklyWageBudget {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.WeeklyWageBudget, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.WeeklyWageBudget, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int HighestWage {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.HighestWage, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.HighestWage, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int HighestWagePaid {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.HighestWagePaid, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.HighestWagePaid, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int LatestSeasonTicketsAddress {
            get {
                return PropertyInvoker.Get<Int32> (ClubFinancesOffsets.LatestSeasonTicketSales, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (ClubFinancesOffsets.LatestSeasonTicketSales, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public int LastestSeasonTickets {
            get {
                return PropertyInvoker.Get<Int32> (0x0, OriginalBytes, this.LatestSeasonTicketsAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<Int32> (0x0, OriginalBytes, this.LatestSeasonTicketsAddress, DatabaseMode, value);
            }
        }
    }
}