using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Staff : Person, IStaff
    {
        private StaffOffsets StaffOffsets;
        private Int64 Address;
        public Staff (Int64 memoryAddress, IVersion version)
            : base (memoryAddress + Math.Abs(version.PersonOffsets.Staff), version)
        {
            this.StaffOffsets = new StaffOffsets (version);
            this.Address = memoryAddress;
        }
        public Staff (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress + Math.Abs(version.PersonOffsets.Staff), originalBytes, version)
        {
            this.StaffOffsets = new StaffOffsets (version);
            this.Address = memoryAddress;
        }

        public void Save() {
            PropertyInvoker.Set<short>(StaffOffsets.HomeReputation, OriginalBytes, Address, DatabaseMode, HomeReputation);
            PropertyInvoker.Set<short>(StaffOffsets.CurrentReputation, OriginalBytes, Address, DatabaseMode, CurrentReputation);
            PropertyInvoker.Set<short>(StaffOffsets.WorldReputation, OriginalBytes, Address, DatabaseMode, WorldReputation);
            PropertyInvoker.Set<short>(StaffOffsets.CurrentAbility, OriginalBytes, Address, DatabaseMode, CurrentAbility);
            PropertyInvoker.Set<short>(StaffOffsets.PotentialAbility, OriginalBytes, Address, DatabaseMode, PotentialAbility);

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

        private StaffAttributes _staffAttributes;
        public StaffAttributes StaffAttributes {
            get {
                if (_staffAttributes == null) {
                    _staffAttributes = new StaffAttributes((Address + StaffOffsets.StaffAttributes), Version);
                }

                return _staffAttributes;
            }
        }

        private short _homeReputation = 0;
        public short HomeReputation {
            get {
                if (_homeReputation == 0) {
                    _homeReputation = PropertyInvoker.Get<short>(StaffOffsets.HomeReputation, OriginalBytes, Address, DatabaseMode);
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
                    _currentReputation = PropertyInvoker.Get<short>(StaffOffsets.CurrentReputation, OriginalBytes, Address, DatabaseMode);
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
                    _worldReputation = PropertyInvoker.Get<short>(StaffOffsets.WorldReputation, OriginalBytes, Address, DatabaseMode);
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

        private short _currentAbility = 0;
        public short CurrentAbility {
            get {
                if (_currentAbility == 0) {
                    _currentAbility = PropertyInvoker.Get<short>(StaffOffsets.CurrentAbility, OriginalBytes, Address, DatabaseMode);
                }

                return _currentAbility;
            }
            set {
                if (_currentAbility != value) {
                    isDirty = true;
                    _currentAbility = value;
                }
            }
        }

        private short _potentialAbility = 0;
        public short PotentialAbility {
            get {
                if (_potentialAbility == 0) {
                    _potentialAbility = PropertyInvoker.Get<short>(StaffOffsets.PotentialAbility, OriginalBytes, Address, DatabaseMode);
                }

                return _potentialAbility;
            }
            set {
                if (_potentialAbility != value) {
                    isDirty = true;
                    _potentialAbility = value;
                }
            }
        }

        private ActualPerson _actualPerson;
        public ActualPerson ActualPerson {
            get {
                if (_actualPerson == null) {
                    _actualPerson = new ActualPerson((Address + StaffOffsets.ActualPerson), Version);
                }

                return _actualPerson;
            }
        }

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

        public string Offset {
            get {
                return "0x" + Address.ToString("X");
            }
        }

        public override string ToString() {
            return string.Format("{0} {1}", this.ActualPerson.FirstName, this.ActualPerson.LastName);
        }
    }
}
