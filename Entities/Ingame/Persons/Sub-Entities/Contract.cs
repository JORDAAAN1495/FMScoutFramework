using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using FMScoutFramework.Extensions;

namespace FMScoutFramework.Core.Entities.InGame
{
    public enum JobType {
        [Description("Not Set")]
        JTNotSet                    = 0,
        [Description("Manager")]
        JTManager                   = 1,
        [Description("Assistant Manager")]
        JTAssistant                 = 2,
        [Description("Coach")]
        JTCoach                     = 3,
        [Description("Physio")]
        JTPhysio                    = 4,
        [Description("Scout")]
        JTScout                     = 5,
        [Description("Goalkeeper Coach")]
        JTGoalkeeperCoach           = 6,
        [Description("Fitness Coach")]
        JTFitnessCoach              = 7,
        [Description("Chairman")]
        JTChairman                  = 8,
        [Description("Director of Football")]
        JTDirectorOfFootball        = 9,
        [Description("Head of Youth Development")]
        JTHeadOfYouthDevelopment    = 10,
        [Description("Director")]
        JTDirector                  = 11,
        [Description("Managing Director")]
        JTManagingDirector          = 12,
        [Description("Owner")]
        JTOwner                     = 13,
        [Description("President")]
        JTPresident                 = 14,
        [Description("Head of Physiotherapy")]
        JTHeadOfPhysiotherapy       = 15,
        [Description("Chief Scout")]
        JTChiefScout                = 16,
        [Description("General Manager")]
        JTGeneralManager            = 17,
        [Description("Player / Assistant Manager")]
        JTPlayerAssistantManager    = 18,
        [Description("Player / Coach")]
        JTPlayerCoach               = 19,
        [Description("Player / Fitness Coach")]
        JTPlayerFitnessCoach        = 20,
        [Description("Player / Goalkeeper Coach")]
        JTPlayerGoalkeeperCoach     = 21,
        [Description("Player / Manager")]
        JTPlayerManager             = 22,
        [Description("Player / Youth Team Coach")]
        JTPlayerYouthTeamCoach      = 23,
        [Description("Player / Head of Youth Development")]
        JTPlayerHeadOfYouthDev      = 24,
        [Description("Player / Chief Scout")]
        JTPlayerChiefScout          = 25,
        [Description("U23 Sports Scientist")]
        JTU23SportsScientist        = 26,
        [Description("U18 Sports Scientist")]
        JTU18SportsScientist        = 27,
        [Description("U18 Data Analyst")]
        JTU18DataAnalyst            = 28,
        [Description("Head of Sports Science")]
        JTHeadOfSportsScience       = 29,
        [Description("Chief Doctor")]
        JTChiefDoctor               = 30,
        [Description("Chief Data Analyst")]
        JTChiefDataAnalyst          = 31
    }

    public enum SquadStatusType {
        [Description("Not Set")]
        SSTNotSet            = 0,
        [Description("Key Player")]
        SSTKeyPlayer         = 1,
        [Description("First Team Regular")]
        SSTFirstTeamRegular  = 2,
        [Description("Squad Rotation")]
        SSTSquadRotation     = 3,
        [Description("Backup Player")]
        SSTBackupPlayer      = 4,
        [Description("Hot Prospect")]
        SSTHotProspect       = 5,
        [Description("Decent Youngster")]
        SSTDecentYoungster   = 6,
        [Description("Not Needed")]
        SSTNotNeeded         = 7
    }

    public enum TransferStatusType {
        [Description("Not Set")]
        TSTNotSet                   = 0,
        [Description("Transfer Listed")]
        TSTNotListed                = 1,
        [Description("Listed for Loan")]
        TSTListedForLoan            = 2,
        [Description("Transfer and Loan Listed")]
        TSTTransferAndLoanListed    = 3
    }

    public enum ContractType {
        [Description("Part Time")]
        CTPartTime      = 0,
        [Description("Full Time")]
        CTFullTime      = 1,
        [Description("Amateur")]
        CTAmateur       = 2,
        [Description("Youth")]
        CTYouth         = 3,
        [Description("Non Contract")]
        CTNonContract   = 4
    }

    public class Contract : BaseObject, IContract
    {
        public ContractOffsets ContractOffsets;
        public Contract (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        {
            ContractOffsets = new ContractOffsets (version);
        }
        public Contract (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        {
            ContractOffsets = new ContractOffsets (version);
        }

        public void Save() {
            PropertyInvoker.Set<int>(ContractOffsets.Wage, OriginalBytes, MemoryAddress, DatabaseMode, _wage);
            PropertyInvoker.Set<byte>(ContractOffsets.JobType, OriginalBytes, MemoryAddress, DatabaseMode, _jobType);
            PropertyInvoker.Set<Int64>(ContractOffsets.Unhappinesses, OriginalBytes, MemoryAddress, DatabaseMode, _unhappinessPointer);
            PropertyInvoker.Set<DateTime>(ContractOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode, _startDate);
            PropertyInvoker.Set<DateTime>(ContractOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode, _endDate);
            PropertyInvoker.Set<DateTime>(ContractOffsets.JoinDate, OriginalBytes, MemoryAddress, DatabaseMode, _joinDate);
            PropertyInvoker.Set<byte>(ContractOffsets.SquadStatus, OriginalBytes, MemoryAddress, DatabaseMode, _squadStatus);
            PropertyInvoker.Set<byte>(ContractOffsets.TransferStatus, OriginalBytes, MemoryAddress, DatabaseMode, _transferStatus);
            PropertyInvoker.Set<byte>(ContractOffsets.SquadNumber, OriginalBytes, MemoryAddress, DatabaseMode, _squadNumber);
            PropertyInvoker.Set<int>(ContractOffsets.LoyaltyBonus, OriginalBytes, MemoryAddress, DatabaseMode, _loyaltyBonus);
            PropertyInvoker.Set<byte>(ContractOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, _type);
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

        public Person Person {
            get {
                return PropertyInvoker.GetPointer<Person>(ContractOffsets.Person, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public Team Team {
            get {
                return PropertyInvoker.GetPointer<Team>(ContractOffsets.Team, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private int _wage = 0;
        public int Wage {
            get {
                if (_wage == 0) {
                    _wage = PropertyInvoker.Get<int>(ContractOffsets.Wage, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _wage;
            }
            set {
                if (_wage != value) {
                    isDirty = true;
                    _wage = value;
                }
            }
        }

        private byte _jobType = 0;
        public byte JobType {
            get {
                if (_jobType == 0) {
                    _jobType = PropertyInvoker.Get<byte>(ContractOffsets.JobType, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _jobType;
            }
            set {
                if (_jobType != value) {
                    isDirty = true;
                    _jobType = value;
                }
            }
        }

        private Int64 _unhappinessPointer = 0;
        public Int64 UnhappinessPointer {
            get {
                if (_unhappinessPointer == 0) {
                    _unhappinessPointer = PropertyInvoker.Get<Int64>(ContractOffsets.Unhappinesses, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _unhappinessPointer;
            }
            set {
                if (_unhappinessPointer != value) {
                    isDirty = true;
                    _unhappinessPointer = value;
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate {
            get {
                if (_startDate.Year < 1900) {
                    _startDate = PropertyInvoker.Get<DateTime>(ContractOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _startDate;
            }
            set {
                if (_startDate != value) {
                    isDirty = true;
                    _startDate = value;
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate {
            get {
                if (_endDate.Year < 1900) {
                    _endDate = PropertyInvoker.Get<DateTime>(ContractOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _endDate;
            }
            set {
                if (_endDate != value) {
                    isDirty = true;
                    _endDate = value;
                }
            }
        }

        private DateTime _joinDate;
        public DateTime JoinDate {
            get {
                if (_joinDate.Year < 1900) {
                    _joinDate = PropertyInvoker.Get<DateTime>(ContractOffsets.JoinDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _joinDate;
            }
            set {
                if (_joinDate != value) {
                    isDirty = true;
                    _joinDate = value;
                }
            }
        }

        private byte _squadStatus = 0;
        public byte SquadStatus {
            get {
                if (_squadStatus == 0) {
                    _squadStatus = PropertyInvoker.Get<byte>(ContractOffsets.SquadStatus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _squadStatus;
            }
            set {
                if (_squadStatus != value) {
                    isDirty = true;
                    _squadStatus = value;
                }
            }
        }

        private byte _transferStatus = 0;
        public byte TransferStatus {
            get {
                if (_transferStatus == 0) {
                    _transferStatus = PropertyInvoker.Get<byte>(ContractOffsets.TransferStatus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _transferStatus;
            }
            set {
                if (_transferStatus != value) {
                    isDirty = true;
                    _transferStatus = value;
                }
            }
        }

        private byte _squadNumber = 0;
        public byte SquadNumber {
            get {
                if (_squadNumber == 0) {
                    _squadNumber = PropertyInvoker.Get<byte>(ContractOffsets.SquadNumber, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _squadNumber;
            }
            set {
                if (_squadNumber != value) {
                    isDirty = true;
                    _squadNumber = value;
                }
            }
        }

        private List<ContractClause> _clauses = new List<ContractClause>();
        public List<ContractClause> Clauses {
            get {
                if (_clauses.Count == 0) {
                    List<ContractClause> res = new List<ContractClause>();
                    Int64 address = ProcessManager.ReadInt64((MemoryAddress + ContractOffsets.Clauses));
                    if (address > 0) {
                        Int64 numberOfClauses = ProcessManager.ReadArrayLength((MemoryAddress + ContractOffsets.Clauses));
                        for (int i = 0; i < numberOfClauses; i++) {
                            ContractClause cc = new ContractClause((address + (i * 8)), Version);
                            if (cc != null) {
                                res.Add(cc);
                            }
                        }
                    }

                    _clauses = res;
                }

                return _clauses;
            }
        }

        private List<ContractBonus> _bonuses = new List<ContractBonus>();
        public List<ContractBonus> Bonuses {
            get {
                if (_bonuses.Count == 0) {
                    lock (_bonuses) {
                        Int64 address = ProcessManager.ReadInt64((MemoryAddress + ContractOffsets.Bonuses));
                        if (address > 0) {
                            Int64 numberOfBonuses = ProcessManager.ReadArrayLength((MemoryAddress + ContractOffsets.Bonuses));
                            for (int i = 0; i < numberOfBonuses; i++) {
                                ContractBonus cb = new ContractBonus((address + (i * 8)), Version);
                                if (cb != null) {
                                    _bonuses.Add(cb);
                                }
                            }
                        }
                    }
                }

                return _bonuses;
            }
        }

        private int _loyaltyBonus = 0;
        public int LoyaltyBonus {
            get {
                if (_loyaltyBonus == 0) {
                    _loyaltyBonus = PropertyInvoker.Get<int>(ContractOffsets.LoyaltyBonus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _loyaltyBonus;
            }
            set {
                if (_loyaltyBonus != value) {
                    isDirty = true;
                    _loyaltyBonus = value;
                }
            }
        }

        private byte _type = 0;
        public byte Type {
            get {
                if (_type == 0) {
                    _type = PropertyInvoker.Get<byte>(ContractOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _type;
            }
            set {
                if (_type != value) {
                    isDirty = true;
                    _type = value;
                }
            }
        }

        // Virtuals
        public bool IsContractExpired {
            get {
                Global inGameGlobal = new Global(Version);
                DateTime now = inGameGlobal.InGameDate;
                if (EndDate < now) {
                    return true;
                }

                return false;
            }
        }

        public bool IsContractExpiring {
            get {
                Global inGameGlobal = new Global(Version);
                DateTime now = inGameGlobal.InGameDate;
                var dateSpan = DateTimeSpan.CompareDates(now, EndDate);
                if (dateSpan.Months <= 6 && dateSpan.Years == 0) {
                    return true;
                }

                return false;
            }
        }

        public bool HasRelegationReleaseClause {
            get {
                bool result = false;
                foreach (ContractClause clause in this.Clauses) {
                    if (clause == null) {
                        continue;
                    }
                    if (clause.Type == (int)ContractClauseType.CCTRelegationRelease) {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        public bool HasNonPromotionReleaseClause {
            get {
                bool result = false;
                foreach (ContractClause clause in Clauses.ToList()) {
                    if (clause.Type == (int)ContractClauseType.CCTNonPromotionRelease) {
                        result = true;
                    }
                }

                return result;
            }
        }

        public int MinFeeReleaseAmount {
            get {
                int result = 999999999;
                foreach (ContractClause clause in Clauses.ToList()) {
                    if (clause.Type == (int)ContractClauseType.CCTMinFeeRelease) {
                        result = clause.Value;
                    }
                }

                return result;
            }
        }
    }
}
