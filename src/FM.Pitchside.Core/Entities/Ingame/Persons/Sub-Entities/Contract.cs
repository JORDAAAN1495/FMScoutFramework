using FM.Pitchside.Core.Defines.Offsets.PersonTypes.Sub_Entities;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.Extensions;
using FM.Pitchside.Core.VirtualMemory.Managers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FM.Pitchside.Core.Entities.Ingame.Persons.Sub_Entities
{
    public enum JobType
    {
        [Description("Not Set")]
        JTNotSet = 0,
        [Description("Player")]
        JTPlayer = 1,
        [Description("Coach")]
        JTCoach = 2,
        [Description("Player/Coach")]
        JTPlayerCoach = 3,
        [Description("Chairman")]
        JTChairman = 4,
        [Description("Director")]
        JTDirector = 6,
        [Description("Managing Director")]
        JTManagingDirector = 8,
        [Description("Director of Football")]
        JTDirectorOfFootball = 10,
        [Description("Physiotherapist")]
        JTPhysiotherapist = 12,
        [Description("Scout")]
        JTScout = 14,
        [Description("Manager")]
        JTManager = 16,
        [Description("Player/Manager")]
        JTPlayerManager = 17,
        [Description("Assistant Manager")]
        JTAssistantManager = 20,
        [Description("Player/Assistant Manager")]
        JTPlayerAssistantManager = 21,
        [Description("Media Pundit")]
        JTMediaPundit = 22,
        [Description("General Manager")]
        JTGeneralManager = 24,
        [Description("Fitness Coach")]
        JTFitnessCoach = 26,
        [Description("Player/Fitness Coach")]
        JTPlayerFitnessCoach = 27,
        [Description("Goalkeeper Coach")]
        JTGoalkeeperCoach = 34,
        [Description("Player/Goalkeeper Coach")]
        JTPlayerGoalkeeperCoach = 35,
        [Description("Chief Data Analyst")]
        JTChiefDataAnalyst = 36,
        [Description("Chief Doctor")]
        JTChiefDoctor = 38,
        [Description("Head of Sports Science")]
        JTHeadOfSportsScience = 40,
        [Description("U18 Data Analyst")]
        JTU18DataAnalyst = 42,
        [Description("Chief Scout")]
        JTChiefScout = 44,
        [Description("Player/Chief Scout")]
        JTPlayerChiefScout = 45,
        [Description("U18 Sports Scientist")]
        JTU18SportsScientist = 46,
        [Description("U23 Sports Scientist")]
        JTU23SportsScientist = 48,
        [Description("Player/Youth Team Coach")]
        JTPlayerYouthTeamCoach = 49,
        [Description("Head of Physiotherapy")]
        JTHeadOfPhysiotherapy = 50,
        [Description("U19 Manager")]
        JTU19Manager = 52,
        [Description("First Team Coach")]
        JTFirstTeamCoach = 54,
        [Description("Head of Youth Development")]
        JTHeadOfYouthDevelopment = 64,
        [Description("Player/Head of Youth Development")]
        JTPlayerHeadOfYouthDev = 65,
        [Description("Owner")]
        JTOwner = 66,
        [Description("President")]
        JTPresident = 70,
        [Description("Caretaker Manager")]
        JTCaretakerManager = 144
    }

    public enum SquadStatusType
    {
        [Description("Not Set")]
        SSTNotSet = 0,
        [Description("Key Player")]
        SSTKeyPlayer = 1,
        [Description("First Team Regular")]
        SSTFirstTeamRegular = 2,
        [Description("Squad Rotation")]
        SSTSquadRotation = 3,
        [Description("Backup Player")]
        SSTBackupPlayer = 4,
        [Description("Hot Prospect")]
        SSTHotProspect = 5,
        [Description("Decent Youngster")]
        SSTDecentYoungster = 6,
        [Description("Not Needed")]
        SSTNotNeeded = 7
    }

    public enum TransferStatusType
    {
        [Description("Not Set")]
        TSTNotSet = 0,
        [Description("Transfer Listed")]
        TSTListed = 1,
        [Description("Listed for Loan")]
        TSTListedForLoan = 2,
        [Description("Transfer and Loan Listed")]
        TSTTransferAndLoanListed = 3,
        [Description("Transfer Lister by Request")]
        TSTListedByRequest = 12,
        [Description("Listed by Request & Loan Listed")]
        TSTByRequestAndLoanListed = 15,
        [Description("Not Available for Loan")]
        TSTNotAvailableForLoan = 64,
        [Description("Transfer Listed / NA for Loan")]
        TSTTransferListedNotForLoan = 69,
        [Description("Listed by Request / NA for Loan")]
        TSTByRequestNotLoan = 76
    }

    public enum ContractType
    {
        [Description(" ")]
        CTNull = -1,
        [Description("Part Time")]
        CTPartTime = 0,
        [Description("Full Time")]
        CTFullTime = 1,
        [Description("Amateur")]
        CTAmateur = 2,
        [Description("Youth")]
        CTYouth = 3,
        [Description("Non Contract")]
        CTNonContract = 4
    }

    public class Contract : BaseObject, IContract
    {
        public ContractOffsets ContractOffsets;
        public Contract(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            ContractOffsets = new ContractOffsets(version);
        }
        public Contract(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            ContractOffsets = new ContractOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<int>(ContractOffsets.Wage, OriginalBytes, MemoryAddress, DatabaseMode, Wage);
            PropertyInvoker.Set<byte>(ContractOffsets.JobType, OriginalBytes, MemoryAddress, DatabaseMode, JobType);
            PropertyInvoker.Set<Int64>(ContractOffsets.Unhappinesses, OriginalBytes, MemoryAddress, DatabaseMode, UnhappinessPointer);
            PropertyInvoker.Set<DateTime>(ContractOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode, StartDate);
            PropertyInvoker.Set<DateTime>(ContractOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode, EndDate);
            PropertyInvoker.Set<DateTime>(ContractOffsets.JoinDate, OriginalBytes, MemoryAddress, DatabaseMode, JoinDate);
            PropertyInvoker.Set<byte>(ContractOffsets.SquadStatus, OriginalBytes, MemoryAddress, DatabaseMode, SquadStatus);
            PropertyInvoker.Set<byte>(ContractOffsets.TransferStatus, OriginalBytes, MemoryAddress, DatabaseMode, TransferStatus);
            PropertyInvoker.Set<byte>(ContractOffsets.SquadNumber, OriginalBytes, MemoryAddress, DatabaseMode, SquadNumber);
            PropertyInvoker.Set<int>(ContractOffsets.LoyaltyBonus, OriginalBytes, MemoryAddress, DatabaseMode, LoyaltyBonus);
            PropertyInvoker.Set<byte>(ContractOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<Int64>(ContractOffsets.Team, OriginalBytes, MemoryAddress, DatabaseMode, TeamAddress.GetValueOrDefault(0x0));
            _isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                if (value)
                {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        public Person Person
        {
            get
            {
                return PropertyInvoker.GetPointer<Person>(ContractOffsets.Person, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private Int64? _teamAddress = null;
        public Int64? TeamAddress
        {
            get
            {
                if (_teamAddress == null)
                {
                    _teamAddress = PropertyInvoker.Get<Int64>(ContractOffsets.Team, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _teamAddress;
            }
            set
            {
                if (_teamAddress != value)
                {
                    isDirty = true;
                    _teamAddress = value;
                    _team = null;
                }
            }
        }

        private Team _team;
        public Team Team
        {
            get
            {
                if (_team == null && TeamAddress.HasValue)
                {
                    _team = new Team(TeamAddress.Value, Version);
                }

                return _team;
            }
        }

        private int _wage = 0;
        public int Wage
        {
            get
            {
                if (_wage == 0)
                {
                    _wage = PropertyInvoker.Get<int>(ContractOffsets.Wage, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _wage;
            }
            set
            {
                if (_wage != value)
                {
                    isDirty = true;
                    _wage = value;
                }
            }
        }

        private byte _jobType = 0;
        public byte JobType
        {
            get
            {
                if (_jobType == 0)
                {
                    _jobType = PropertyInvoker.Get<byte>(ContractOffsets.JobType, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _jobType;
            }
            set
            {
                if (_jobType != value)
                {
                    isDirty = true;
                    _jobType = value;
                }
            }
        }

        private Int64 _unhappinessPointer = 0;
        public Int64 UnhappinessPointer
        {
            get
            {
                if (_unhappinessPointer == 0)
                {
                    _unhappinessPointer = PropertyInvoker.Get<Int64>(ContractOffsets.Unhappinesses, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _unhappinessPointer;
            }
            set
            {
                if (_unhappinessPointer != value)
                {
                    isDirty = true;
                    _unhappinessPointer = value;
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                if (_startDate.Year < 1900)
                {
                    _startDate = PropertyInvoker.Get<DateTime>(ContractOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _startDate;
            }
            set
            {
                if (_startDate != value)
                {
                    isDirty = true;
                    _startDate = value;
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                if (_endDate.Year < 1900)
                {
                    _endDate = PropertyInvoker.Get<DateTime>(ContractOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _endDate;
            }
            set
            {
                if (_endDate != value)
                {
                    isDirty = true;
                    _endDate = value;
                }
            }
        }

        private DateTime _joinDate;
        public DateTime JoinDate
        {
            get
            {
                if (_joinDate.Year < 1900)
                {
                    _joinDate = PropertyInvoker.Get<DateTime>(ContractOffsets.JoinDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _joinDate;
            }
            set
            {
                if (_joinDate != value)
                {
                    isDirty = true;
                    _joinDate = value;
                }
            }
        }

        private byte _squadStatus = 0;
        public byte SquadStatus
        {
            get
            {
                if (_squadStatus == 0)
                {
                    _squadStatus = PropertyInvoker.Get<byte>(ContractOffsets.SquadStatus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _squadStatus;
            }
            set
            {
                if (_squadStatus != value)
                {
                    isDirty = true;
                    _squadStatus = value;
                }
            }
        }

        private byte _transferStatus = 0;
        public byte TransferStatus
        {
            get
            {
                if (_transferStatus == 0)
                {
                    _transferStatus = PropertyInvoker.Get<byte>(ContractOffsets.TransferStatus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _transferStatus;
            }
            set
            {
                if (_transferStatus != value)
                {
                    isDirty = true;
                    _transferStatus = value;
                }
            }
        }

        private byte _squadNumber = 0;
        public byte SquadNumber
        {
            get
            {
                if (_squadNumber == 0)
                {
                    _squadNumber = PropertyInvoker.Get<byte>(ContractOffsets.SquadNumber, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _squadNumber;
            }
            set
            {
                if (_squadNumber != value)
                {
                    isDirty = true;
                    _squadNumber = value;
                }
            }
        }

        private List<ContractClause> _clauses = new List<ContractClause>();
        public List<ContractClause> Clauses
        {
            get
            {
                if (_clauses.Count == 0)
                {
                    List<ContractClause> res = new List<ContractClause>();
                    Int64 address = ProcessManager.ReadInt64((MemoryAddress + ContractOffsets.Clauses));
                    if (address > 0)
                    {
                        Int64 numberOfClauses = ProcessManager.ReadArrayLength((MemoryAddress + ContractOffsets.Clauses));
                        for (int i = 0; i < numberOfClauses; i++)
                        {
                            ContractClause cc = new ContractClause((address + (i * 8)), Version);
                            if (cc != null)
                            {
                                res.Add(cc);
                            }
                        }
                    }

                    _clauses = res;
                }

                return _clauses;
            }
        }

        private int _loyaltyBonus = 0;
        public int LoyaltyBonus
        {
            get
            {
                if (_loyaltyBonus == 0)
                {
                    _loyaltyBonus = PropertyInvoker.Get<int>(ContractOffsets.LoyaltyBonus, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _loyaltyBonus;
            }
            set
            {
                if (_loyaltyBonus != value)
                {
                    isDirty = true;
                    _loyaltyBonus = value;
                }
            }
        }

        private byte _type = 0;
        public byte Type
        {
            get
            {
                if (_type == 0)
                {
                    _type = PropertyInvoker.Get<byte>(ContractOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _type;
            }
            set
            {
                if (_type != value)
                {
                    isDirty = true;
                    _type = value;
                }
            }
        }

        // Virtuals
        public bool IsContractExpired
        {
            get
            {
                Global inGameGlobal = new Global(Version);
                DateTime now = inGameGlobal.InGameDate;
                if (EndDate < now)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsContractExpiring
        {
            get
            {
                Global inGameGlobal = new Global(Version);
                DateTime now = inGameGlobal.InGameDate;
                var dateSpan = DateTimeSpan.CompareDates(now, EndDate);
                if (dateSpan.Months <= 6 && dateSpan.Years == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool HasRelegationReleaseClause
        {
            get
            {
                bool result = false;
                foreach (ContractClause clause in this.Clauses)
                {
                    if (clause == null)
                    {
                        continue;
                    }
                    if (clause.Type == (int)ContractClauseType.CCTRelegationRelease)
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        public bool HasNonPromotionReleaseClause
        {
            get
            {
                bool result = false;
                foreach (ContractClause clause in Clauses.ToList())
                {
                    if (clause.Type == (int)ContractClauseType.CCTNonPromotionRelease)
                    {
                        result = true;
                    }
                }

                return result;
            }
        }

        public int MinFeeReleaseAmount
        {
            get
            {
                int result = 999999999;
                foreach (ContractClause clause in Clauses.ToList())
                {
                    if (clause.Type == (int)ContractClauseType.CCTMinFeeRelease)
                    {
                        result = clause.Value;
                    }
                }

                return result;
            }
        }

        // Functions
        public void RemoveUnhappy()
        {
            if (UnhappinessPointer > 0x0)
            {
                PropertyInvoker.Set<Int64>(ContractOffsets.Unhappinesses, OriginalBytes, MemoryAddress, DatabaseMode, 0);
                _unhappinessPointer = 0;
            }
        }
    }
}