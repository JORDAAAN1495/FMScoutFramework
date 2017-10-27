using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame {
    public enum APEthnicity {
        [Description("Northern European")]
        APENorthernEuropean             = 0,
        [Description("Mediterranean Hispanic")]
        APEMediterraneanHispanic        = 1,
        [Description("North African/Middle Eastern")]
        APENorthAfricanMiddleEastern    = 2,
        [Description("African/Caribbean")]
        APEAfricanCaribbean             = 3,
        [Description("Asian")]
        APEAsian                        = 4,
        [Description("South East Asian")]
        APESouthEastAsian               = 5,
        [Description("Pacific Islander")]
        APEPacificIslander              = 6,
        [Description("Native American")]
        APENativeAmerican               = 7,
        [Description("Native Australian")]
        APENativeAustralian             = 8,
        [Description("Mixed Race (Black/White)")]
        APEMixedRaceBlackWhite          = 9,
        [Description("East Asian")]
        APEEastAsian                    = 10
    }

    public enum APHairColour {
        [Description("Unknown")]
        APHUnknown              = 0,
        [Description("Blonde")]
        APHBlonde               = 1,
        [Description("Light Brown")]
        APHLightBrown           = 2,
        [Description("Dark Brown")]
        APHDarkBrown            = 3,
        [Description("Red")]
        APHRed                  = 4,
        [Description("Black")]
        APHBlack                = 5,
        [Description("Grey")]
        APHGrey                 = 6,
        [Description("Bald")]
        APHBald                 = 7,
        [Description("Changeable (Normal)")]
        APHChangeableNormal     = 8,
        [Description("Changeable (Dramatic)")]
        APHChangeableDramatic   = 9
    }

    public enum APSkinTone {
        [Description("Not Set (Defaults to 6)")]
        APSSkinTone0 = -1,
        [Description("Skin Tone 1")]
        APSSkinTone1 = 0,
        [Description("Skin Tone 2")]
        APSSkinTone2 = 1,
        [Description("Skin Tone 3")]
        APSSkinTone3 = 2,
        [Description("Skin Tone 4")]
        APSSkinTone4 = 3,
        [Description("Skin Tone 5")]
        APSSkinTone5 = 4,
        [Description("Skin Tone 6")]
        APSSkinTone6 = 5,
        [Description("Skin Tone 7")]
        APSSkinTone7 = 6,
        [Description("Skin Tone 8")]
        APSSkinTone8 = 7,
        [Description("Skin Tone 9")]
        APSSkinTone9 = 8,
        [Description("Skin Tone 10")]
        APSSkinTone10 = 9,
        [Description("Skin Tone 11")]
        APSSkinTone11 = 10,
        [Description("Skin Tone 12")]
        APSSkinTone12 = 11,
        [Description("Skin Tone 13")]
        APSSkinTone13 = 12,
        [Description("Skin Tone 14")]
        APSSkinTone14 = 13,
        [Description("Skin Tone 15")]
        APSSkinTone15 = 14,
        [Description("Skin Tone 16")]
        APSSkinTone16 = 15,
        [Description("Skin Tone 17")]
        APSSkinTone17 = 16,
        [Description("Skin Tone 18")]
        APSSkinTone18 = 17,
        [Description("Skin Tone 19")]
        APSSkinTone19 = 18,
        [Description("Skin Tone 20")]
        APSSkinTone20 = 19
    }

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
            PropertyInvoker.Set<DateTime>(ActualPersonOffsets.DateOfBirth, OriginalBytes, MemoryAddress, DatabaseMode, DateOfBirth);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.Ethnicity, OriginalBytes, MemoryAddress, DatabaseMode, Ethnicity);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.HairColour, OriginalBytes, MemoryAddress, DatabaseMode, HairColour);
            PropertyInvoker.Set<sbyte>(ActualPersonOffsets.SkinTone, OriginalBytes, MemoryAddress, DatabaseMode, SkinTone);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode, InternationalApps);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.U21InternationalApps, OriginalBytes, MemoryAddress, DatabaseMode, U21InternationalApps);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode, InternationalGoals);
            PropertyInvoker.Set<byte>(ActualPersonOffsets.U21InternationalGoals, OriginalBytes, MemoryAddress, DatabaseMode, U21InternationalGoals);
            PropertyInvoker.Set<Int64>(ActualPersonOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, NationAddress);
            PropertyInvoker.Set<Int64>(ActualPersonOffsets.Contract, OriginalBytes, MemoryAddress, DatabaseMode, ContractAddress.GetValueOrDefault(0x0));
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

        private Int64 _nationAddress = 0x0;
        public Int64 NationAddress {
            get {
                if (_nationAddress == 0x0) {
                    _nationAddress = PropertyInvoker.Get<Int64>(ActualPersonOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _nationAddress;
            }
            set {
                if (_nationAddress != value) {
                    isDirty = true;
                    _nationAddress = value;
                    _nation = null;
                }
            }
        }

        private Nation _nation;
        public Nation Nation {
            get {
                if (_nation == null) {
                    _nation = new Nation(NationAddress, Version);
                }
                return _nation;
            }
        }

        public string SearchableNationality {
            get {
                string res = Nation.Name;
                foreach (Relationship rel in Relationships) {
                    if ((RelationshipType)rel.Type == RelationshipType.RTHasNationality) {
                        res += " " + ((Nation)rel.AssociatedObject).Name;
                    }
                }

                return res;
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

        private List<Relationship> _relationships = new List<Relationship>();
        public List<Relationship> Relationships {
            get {
                if (_relationships.Count == 0) {
                    // It's a pointer to an array
                    Int64 arrayStartAddress = PropertyInvoker.Get<Int64>(ActualPersonOffsets.Relationships, OriginalBytes, MemoryAddress, DatabaseMode);
                    if (arrayStartAddress > 0x0) {
                        List<Relationship> ret = new List<Relationship>();
                        // Get the array length
                        Int64 numOfRelationships = ProcessManager.ReadArrayLength(arrayStartAddress, 0x10);
                        Int64 firstItemAddress = PropertyInvoker.Get<Int64>(0x0, OriginalBytes, arrayStartAddress, DatabaseMode);
                        for (Int64 i = 0; i < numOfRelationships; i++) {
                            ret.Add(new Relationship(firstItemAddress + (i * 0x10), Version));
                        }

                        _relationships = ret;
                    }
                }

                return _relationships;
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

        private sbyte _skinTone = 0;
        public sbyte SkinTone {
            get {
                if (_skinTone == 0) {
                    _skinTone = PropertyInvoker.Get<sbyte>(ActualPersonOffsets.SkinTone, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private Int64? _contractAddress = null;
        public Int64? ContractAddress {
            get {
                if (_contractAddress == null) {
                    _contractAddress = PropertyInvoker.Get<Int64>(ActualPersonOffsets.Contract, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _contractAddress;
            }
            set {
                if (_contractAddress != value) {
                    isDirty = true;
                    _contractAddress = value;
                    _contract = null;
                }
            }
        }

        private Contract _contract = null;
        public Contract Contract {
            get {
                if (_contract == null) {
                    if (ContractAddress.HasValue) {
                        _contract = new Contract(ContractAddress.Value, Version);
                    }
                }

                return _contract;
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

        private JobAttributes _jobAttributes;
        public JobAttributes JobAttributes {
            get {
                if (_jobAttributes == null) {
                    _jobAttributes = PropertyInvoker.GetPointer<JobAttributes>(ActualPersonOffsets.JobAttributes, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }

                return _jobAttributes;
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
