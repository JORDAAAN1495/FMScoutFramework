using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;

namespace FMScoutFramework.Core.Entities.InGame {
    public class ActualPerson : BaseObject, IActualPerson {
        public ActualPersonOffsets ActualPersonOffsets;
        public ActualPerson(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) {
            this.ActualPersonOffsets = new ActualPersonOffsets(version);
        }
        public ActualPerson(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) {
            this.ActualPersonOffsets = new ActualPersonOffsets(version);
        }

        public void Save() {
            PropertyInvoker.Set<DateTime>(ActualPersonOffsets.DateOfBirth, OriginalBytes, MemoryAddress, DatabaseMode, _dateOfBirth);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.Ethnicity, OriginalBytes, MemoryAddress, DatabaseMode, _ethnicity);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.HairColour, OriginalBytes, MemoryAddress, DatabaseMode, _hairColour);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.SkinTone, OriginalBytes, MemoryAddress, DatabaseMode, _skinTone);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode, _internationalApps);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.U21InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode, _u21InternationalApps);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode, _internationalGoals);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.U21InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode, _u21InternationalGoals);
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

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth {
            get {
                if (_dateOfBirth.Year < 1900) {
                    _dateOfBirth = PropertyInvoker.Get<DateTime>(ActualPersonOffsets.DateOfBirth, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _dateOfBirth;
            }
            set {
                if (_dateOfBirth != value) {
                    isDirty = true;
                    _dateOfBirth = value;
                }
            }
        }

        public int Age {
            get {
                Global InGameGlobal = new Global(Version);
                DateTime now = InGameGlobal.InGameDate;
                int age = now.Year - this.DateOfBirth.Year;
                if (this.DateOfBirth > now.AddYears(-age)) {
                    age--;
                }

                return age;
            }
        }

        public string FullName {
            get {
                return PropertyInvoker.GetString(ActualPersonOffsets.FullName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string FirstName {
            get {
                return PropertyInvoker.GetString(ActualPersonOffsets.FirstName, Version.MemoryAddresses.StringOffset, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string LastName {
            get {
                return PropertyInvoker.GetString(ActualPersonOffsets.LastName, Version.MemoryAddresses.StringOffset, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string CommonName {
            get {
                return PropertyInvoker.GetString(ActualPersonOffsets.CommonName, Version.MemoryAddresses.StringOffset, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string SearchName {
            get {
                return this.FirstName + " " + this.LastName + " " + this.CommonName + " " + this.FullName;
            }
        }

        public string VisibleName {
            get {
                if (!string.IsNullOrEmpty(this.CommonName) && this.CommonName.Length > 1) {
                    return this.CommonName;
                }
                else {
                    return this.FirstName + " " + this.LastName;
                }
            }
        }

        public City CityOfBirth {
            get {
                return PropertyInvoker.GetPointer<City>(ActualPersonOffsets.CityOfBirth, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public Nation Nation {
            get {
                return PropertyInvoker.GetPointer<Nation>(ActualPersonOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private PersonAttributes _attributes;
        public PersonAttributes Attributes {
            get {
                if (_attributes == null) {
                    _attributes = new PersonAttributes((MemoryAddress + ActualPersonOffsets.Attributes), Version);
                }
                return _attributes;
            }
        }

        private byte _ethnicity = 0;
        public byte Ethnicity {
            get {
                if (_ethnicity == 0) {
                    _ethnicity = PropertyInvoker.Get<byte>(ActualPersonOffsets.Ethnicity, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _ethnicity;
            }
            set {
                if (_ethnicity != value) {
                    isDirty = true;
                    _ethnicity = value;
                }
            }
        }

        private byte _hairColour = 0;
        public byte HairColour {
            get {
                if (_hairColour == 0) {
                    _hairColour = PropertyInvoker.Get<byte>(ActualPersonOffsets.HairColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _hairColour;
            }
            set {
                if (_hairColour != value) {
                    isDirty = true;
                    _hairColour = value;
                }
            }
        }

        private byte _skinTone = 0;
        public byte SkinTone {
            get {
                if (_skinTone == 0) {
                    _skinTone = PropertyInvoker.Get<byte>(ActualPersonOffsets.SkinTone, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _skinTone;
            }
            set {
                if (_skinTone != value) {
                    isDirty = true;
                    _skinTone = value;
                }
            }
        }

        public Contract Contract {
            get {
                Int64 pointerAddress = ProcessManager.ReadInt64((MemoryAddress + ActualPersonOffsets.Contract));
                if (pointerAddress > 0) {
                    return PropertyInvoker.GetPointer<Contract>(ActualPersonOffsets.Contract, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }
                else {
                    return null;
                }
            }
        }

        private PreferredMoves _preferredMoves;
        public PreferredMoves PreferredMoves {
            get {
                if (_preferredMoves == null) {
                    _preferredMoves = new PreferredMoves((MemoryAddress + ActualPersonOffsets.PreferredMoves), Version);
                }

                return _preferredMoves;
            }
        }

        private byte _internationalApps = 0;
        public byte InternationalApps {
            get {
                if (_internationalApps == 0) {
                    _internationalApps = PropertyInvoker.Get<byte>(ActualPersonOffsets.InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _internationalApps;
            }
            set {
                if (_internationalApps != value) {
                    isDirty = true;
                    _internationalApps = value;
                }
            }
        }

        private byte _u21InternationalApps = 0;
        public byte U21InternationalApps {
            get {
                if (_u21InternationalApps == 0) {
                    _u21InternationalApps = PropertyInvoker.Get<byte>(ActualPersonOffsets.U21InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _u21InternationalApps;
            }
            set {
                if (_u21InternationalApps != value) {
                    isDirty = true;
                    _u21InternationalApps = value;
                }
            }
        }

        private byte _internationalGoals = 0;
        public byte InternationalGoals {
            get {
                if (_internationalGoals == 0) {
                    _internationalGoals = PropertyInvoker.Get<byte>(ActualPersonOffsets.InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _internationalGoals;
            }
            set {
                if (_internationalGoals != value) {
                    isDirty = true;
                    _internationalGoals = value;
                }
            }
        }

        private byte _u21InternationalGoals = 0;
        public byte U21InternationalGoals {
            get {
                if (_u21InternationalGoals == 0) {
                    _u21InternationalGoals = PropertyInvoker.Get<byte>(ActualPersonOffsets.U21InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _u21InternationalGoals;
            }
            set {
                if (_u21InternationalGoals != value) {
                    isDirty = true;
                    _u21InternationalGoals = value;
                }
            }
        }

        // Virtuals
        public bool IsFreeAgent {
            get {
                if (Contract == null) {
                    return true;
                }

                return false;
            }
        }
    }
}
