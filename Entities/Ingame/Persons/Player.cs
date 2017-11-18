using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Utilities;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Player : Person, IPlayer
    {
        private PlayerOffsets PlayerOffsets;
        public Int64 Address;
        public Player (Int64 memoryAddress, IVersion version)
            : base (memoryAddress + Math.Abs(version.PersonOffsets.Player), version)
        {
            this.PlayerOffsets = new PlayerOffsets (version);
            this.Address = memoryAddress;
        }
        public Player (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress + Math.Abs(version.PersonOffsets.Player), originalBytes, version)
        {
            this.PlayerOffsets = new PlayerOffsets (version);
            this.Address = memoryAddress;
        }

        public void Save() {
            PropertyInvoker.Set<int>(PlayerOffsets.Value, OriginalBytes, Address, DatabaseMode, Value);
            PropertyInvoker.Set<int>(PlayerOffsets.AskingPrice, OriginalBytes, Address, DatabaseMode, AskingPrice);
            PropertyInvoker.Set<short>(PlayerOffsets.Fitness, OriginalBytes, Address, DatabaseMode, Fitness);
            PropertyInvoker.Set<short>(PlayerOffsets.Jadedness, OriginalBytes, Address, DatabaseMode, Jadedness);
            PropertyInvoker.Set<short>(PlayerOffsets.Condition, OriginalBytes, Address, DatabaseMode, Condition);
            PropertyInvoker.Set<short>(PlayerOffsets.HomeReputation, OriginalBytes, Address, DatabaseMode, HomeReputation);
            PropertyInvoker.Set<short>(PlayerOffsets.CurrentReputation, OriginalBytes, Address, DatabaseMode, CurrentReputation);
            PropertyInvoker.Set<short>(PlayerOffsets.WorldReputation, OriginalBytes, Address, DatabaseMode, WorldReputation);
            PropertyInvoker.Set<short>(PlayerOffsets.CA, OriginalBytes, Address, DatabaseMode, CA);
            PropertyInvoker.Set<short>(PlayerOffsets.PA, OriginalBytes, Address, DatabaseMode, PA);
            PropertyInvoker.Set<short>(PlayerOffsets.Weight, OriginalBytes, Address, DatabaseMode, Weight);
            PropertyInvoker.Set<short>(PlayerOffsets.Height, OriginalBytes, Address, DatabaseMode, Height);
            PropertyInvoker.Set<Int64>(PlayerOffsets.Team, OriginalBytes, Address, DatabaseMode, TeamAddress.GetValueOrDefault(0x0));
            _isDirty = false;
        }

        public void HealPlayer() {
            if (InjuriesPtr > 0) {
                PropertyInvoker.Set<Int64>(0x0, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
                PropertyInvoker.Set<Int64>(0x8, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
                PropertyInvoker.Set<Int64>(0xF, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
            }

            PropertyInvoker.Set<short>(PlayerOffsets.Fitness, OriginalBytes, Address, DatabaseMode, 10000);
            PropertyInvoker.Set<short>(PlayerOffsets.Condition, OriginalBytes, Address, DatabaseMode, 10000);

            _fitness = 0;
            _condition = 0;
            IsInjured = null;
        }

        public void DestroyPlayer() {
            PropertyInvoker.Set<short>(PlayerOffsets.Condition, OriginalBytes, Address, DatabaseMode, 1);
            PropertyInvoker.Set<short>(PlayerOffsets.Fitness, OriginalBytes, Address, DatabaseMode, 1);
            PropertyInvoker.Set<short>(PlayerOffsets.Jadedness, OriginalBytes, Address, DatabaseMode, -500);

            _fitness = 0;
            _condition = 0;
            _jadedness = 0;
        }

        public void RemoveBan() {
            // Wipe out the array pointer
            PropertyInvoker.Set<Int64>(PlayerOffsets.BansPtr, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
            PropertyInvoker.Set<Int64>(PlayerOffsets.BansPtr + 0x8, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
            PropertyInvoker.Set<Int64>(PlayerOffsets.BansPtr + 0xF, OriginalBytes, InjuriesPtr, DatabaseMode, 0);
            IsBanned = false;
        }

        public double PlayerGrowthPotential {
            get {
                double DAP = ((Attributes.Determination / 5) * 0.05) + (ActualPerson.Attributes.Ambition * 0.09) + (ActualPerson.Attributes.Professionalism * 0.115);
                if (ActualPerson.Age < 24) {
                    if (PA <= (CA + 10)) {
                        DAP -= 0.5;
                    }
                }
                else if (ActualPerson.Age >= 24 && ActualPerson.Age < 29) {
                    DAP -= 0.5;
                    if (PA <= (CA + 10)) {
                        DAP -= 0.5;
                    }
                }
                else if (ActualPerson.Age >= 29 && ActualPerson.Age < 34) {
                    DAP -= 1;
                    if (PA <= (CA + 10)) {
                        DAP -= 0.5;
                    }
                }
                else if (ActualPerson.Age >= 34) {
                    if (PA <= (CA + 10) && (Attributes.Goalkeeper / 5) >= 15) {
                        DAP += 0.5;
                    }
                    else {
                        DAP = 0;
                    }
                }

                return DAP;
            }
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

        private ActualPerson _actualPerson = null;
        public ActualPerson ActualPerson {
            get {
                if (_actualPerson == null) {
                    _actualPerson = new ActualPerson((Address + PlayerOffsets.ActualPerson), Version);
                }

                return _actualPerson;
            }
        }

        public Int64 InjuriesPtr {
            get {
                return PropertyInvoker.Get<Int64>(PlayerOffsets.Injuries, OriginalBytes, Address, DatabaseMode);
            }
        }

        private bool? _isInjured;
        public bool? IsInjured {
            get {
                if (_isInjured == null) {
                    Int64 InjuryCount = ProcessManager.ReadArrayLength(InjuriesPtr, 0x8);
                    _isInjured = InjuryCount > 0;
                }

                return _isInjured;
            }
            set {
                if (_isInjured != value) {
                    _isInjured = value;
                }
            }
        }

        public Int64 BansPtr {
            get {
                return PropertyInvoker.Get<Int64>(PlayerOffsets.BansPtr, OriginalBytes, InjuriesPtr, DatabaseMode);
            }
        }

        private bool _isBanned = false;
        public bool IsBanned {
            get {
                if (!_isBanned) {
                    _isBanned = BansPtr > 0;
                }

                return _isBanned;
            }
            set {
                if (_isBanned != value) {
                    _isBanned = value;
                }
            }
        }

        private Int64? _teamAddress = null;
        public Int64? TeamAddress {
            get {
                if (_teamAddress == null) {
                    _teamAddress = PropertyInvoker.Get<Int64>(PlayerOffsets.Team, OriginalBytes, Address, DatabaseMode);
                }

                return _teamAddress;
            }
            set {
                if (_teamAddress != value) {
                    isDirty = true;
                    _teamAddress = value;
                    _team = null;
                }
            }
        }

        private Team _team;
        public Team Team {
            get {
                if (_team == null && TeamAddress.HasValue) {
                    _team = new Team(TeamAddress.Value, Version);
                }
                return _team;
            }
        }

        private int _value = 0;
        public int Value {
            get {
                if (_value == 0) {
                    _value = PropertyInvoker.Get<int>(PlayerOffsets.Value, OriginalBytes, Address, DatabaseMode);
                }

                return _value;
            }
            set {
                if (_value != value) {
                    isDirty = true;
                    _value = value;
                }
            }
        }

        private int _askingPrice = 0;
        public int AskingPrice {
            get {
                if (_askingPrice == 0) {
                    _askingPrice = PropertyInvoker.Get<int>(PlayerOffsets.AskingPrice, OriginalBytes, Address, DatabaseMode);
                }

                return _askingPrice;
            }
            set {
                if (_askingPrice != value) {
                    isDirty = true;
                    _askingPrice = value;
                }
            }
        }

        private short _fitness = 0;
        public short Fitness {
            get {
                if (_fitness == 0) {
                    _fitness = PropertyInvoker.Get<short>(PlayerOffsets.Fitness, OriginalBytes, Address, DatabaseMode);
                }

                return _fitness;
            }
            set {
                if (_fitness != value) {
                    isDirty = true;
                    _fitness = value;
                }
            }
        }

        private short _jadedness = 0;
        public short Jadedness {
            get {
                if (_jadedness == 0) {
                    _jadedness = PropertyInvoker.Get<short>(PlayerOffsets.Jadedness, OriginalBytes, Address, DatabaseMode);
                }

                return _jadedness;
            }
            set {
                if (_jadedness != value) {
                    isDirty = true;
                    _jadedness = value;
                }
            }
        }

        private short _condition = 0;
        public short Condition {
            get {
                if (_condition == 0) {
                    _condition = PropertyInvoker.Get<short>(PlayerOffsets.Condition, OriginalBytes, Address, DatabaseMode);
                }

                return _condition;
            }
            set {
                if (_condition != value) {
                    isDirty = true;
                    _condition = value;
                }
            }
        }

        private short _homeReputation = 0;
        public short HomeReputation {
            get {
                if (_homeReputation == 0) {
                    _homeReputation = PropertyInvoker.Get<short>(PlayerOffsets.HomeReputation, OriginalBytes, Address, DatabaseMode);
                }

                return _homeReputation;
            }
            set {
                if (_homeReputation != value) {
                    isDirty = true;
                    _homeReputation = value;
                }
            }
        }

        private short _currentReputation = 0;
        public short CurrentReputation {
            get {
                if (_currentReputation == 0) {
                    _currentReputation = PropertyInvoker.Get<short>(PlayerOffsets.CurrentReputation, OriginalBytes, Address, DatabaseMode);
                }

                return _currentReputation;
            }
            set {
                if (_currentReputation != value) {
                    isDirty = true;
                    _currentReputation = value;
                }
            }
        }

        private short _worldReputation = 0;
        public short WorldReputation {
            get {
                if (_worldReputation == 0) {
                    _worldReputation = PropertyInvoker.Get<short>(PlayerOffsets.WorldReputation, OriginalBytes, Address, DatabaseMode);
                }

                return _worldReputation;
            }
            set {
                if (_worldReputation != value) {
                    isDirty = true;
                    _worldReputation = value;
                }
            }
        }

        private short _ca = 0;
        public short CA {
            get {
                if (_ca == 0) {
                    _ca = PropertyInvoker.Get<short>(PlayerOffsets.CA, OriginalBytes, Address, DatabaseMode);
                }

                return _ca;
            }
            set {
                if (_ca != value) {
                    isDirty = true;
                    _ca = value;
                }
            }
        }

        private short _pa = 0;
        public short PA {
            get {
                if (_pa == 0) {
                    _pa = PropertyInvoker.Get<short>(PlayerOffsets.PA, OriginalBytes, Address, DatabaseMode);
                }

                return _pa;
            }
            set {
                if (_pa != value) {
                    isDirty = true;
                    _pa = value;
                }
            }
        }

        private short _weight = 0;
        public short Weight {
            get {
                if (_weight == 0) {
                    _weight = PropertyInvoker.Get<short>(PlayerOffsets.Weight, OriginalBytes, Address, DatabaseMode);
                }

                return _weight;
            }
            set {
                if (_weight != value) {
                    isDirty = true;
                    _weight = value;
                }
            }
        }

        private short _height = 0;
        public short Height {
            get {
                if (_height == 0) {
                    _height = PropertyInvoker.Get<short>(PlayerOffsets.Height, OriginalBytes, Address, DatabaseMode);
                }

                return _height;
            }
            set {
                if (_height != value) {
                    isDirty = true;
                    _height = value;
                }
            }
        }

        private PlayerAttributes _attributes;
        public PlayerAttributes Attributes {
            get {
                if (_attributes == null) {
                    _attributes = new PlayerAttributes((Address + PlayerOffsets.PlayerAttributes), Version);
                }
                return _attributes;
            }
        }

        // Virtuals
        public string ContractStatus {
            get {
                string res = "";
                if (ActualPerson.IsFreeAgent) {
                    res = "Free Agent";
                }
                
                if (!ActualPerson.IsFreeAgent && ActualPerson.Contract.IsContractExpired) {
                    res = "Expired";
                }

                if (!ActualPerson.IsFreeAgent && ActualPerson.Contract.IsContractExpiring) {
                    res = "Expires (6m)";
                }

                return res;
            }
        }

        private RoleRatings _roleRatings = null;
        public RoleRatings RoleRatings {
            get {
                if (_roleRatings == null) {
                    _roleRatings = new RoleRatings(this);
                }

                return _roleRatings;
            }
        }

        public bool IsRegen {
            get {
                return (UID > 98041249);
            }
        }

        public string Offset {
            get {
                return "0x" + this.MemoryAddress.ToString("X");
            }
        }

        public override string ToString() {
            return string.Format("{0} {1}", this.ActualPerson.FirstName, this.ActualPerson.LastName);
        }
    }
}
