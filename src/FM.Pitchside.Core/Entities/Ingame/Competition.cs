using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Defines.Offsets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace FMScoutFramework.Core.Entities.InGame
{
    public enum CompTypes
    {
        [Description("Domestic Top Division")]
        CTDomesticTopDivision = 0,
        [Description("Domestic Division")]
        CTDomesticDivision = 1,
        [Description("Domestic Main Cup")]
        CTMainCup = 2,
        [Description("Domestic League Cup")]
        CTLeagueCup = 3,
        [Description("Domestic Cup")]
        CTDomesticCup = 4,
        [Description("Super Cup")]
        CTSuperCup = 5,
        [Description("International Finals")]
        CTInternationalFinals = 6,
        [Description("International Qualifiers")]
        CTInternationalQualifiers = 7,
        [Description("Reserve Division")]
        CTReserveDivision = 8,
        [Description("Club Friendly")]
        CTClubFriendly = 9,
        [Description("Main Continental Intarnational Finals")]
        CTMainContinentalIntFinals = 10,
        [Description("Main Continental International Qualifiers")]
        CTMainContinentalIntQuals = 11,
        [Description("Club Finals")]
        CTClubFinals = 12,
        [Description("Friendly")]
        CTFriendly = 13,
        [Description("Domestic Other Division")]
        CTDomesticOtherDivision = 14,
        [Description("U21 International Finals")]
        CTU21InternationalFinals = 15,
        [Description("U21 International Qualifiers")]
        CTU21InternationalQualifiers = 16,
        [Description("U19 International Finals")]
        CTU19InternationalFinals = 17,
        [Description("U19 International Qualifiers")]
        CTU19InternationalQualifiers = 18,
        [Description("U23 International Finals")]
        CTU23InternationalFinals = 19,
        [Description("U23 International Qualifiers")]
        CTU23InternationalQualifiers = 20,
        [Description("Club Champions Cup")]
        CTClubChampionsCup = 21,

        [Description("Reserve Cup")]
        CTReserveCup = 23,
        [Description("International Friendly")]
        CTInternationalFriendly = 24,
        [Description("U20 International Finals")]
        CTU20InternationalFinals = 25,
        [Description("All Stars Cup")]
        CTAllStarsCup = 26,
        [Description("U20 International Qualifiers")]
        CTU20InternationalQualifiers = 27,
        [Description("Inactive Competition")]
        CTInactiveCompetition = 28,
        [Description("Inactive Other Competition")]
        CTInactiveOtherCompetition = 29

        //[Description("U22 International Finals")]
        //CNTU22InternationalFinals   = 15,
        //[Description("U22 International Qualifiers")]
        //CNTU22InternationalQualifiers   = 20,
    }

    public enum CompNameTypes
    {
        [Description("Original Database Name")]
        CNTOriginalDatabaseName = 0,
        [Description("Friendly Name")]
        CNTFriendlyName = 1,
        [Description("Reserves League Name")]
        CNTReservesLeagueName = 2,
        [Description("U19 League Name")]
        CNTU19LeagueName = 3,
        [Description("Name on Server Only")]
        CNTNameOnServerOnly = 4,
        [Description("U21 League Name")]
        CNTU21LeagueName = 5,
        [Description("U18 League Name")]
        CNTU18LeagueName = 6,
        [Description("U20 League Name")]
        CNTU20LeagueName = 7,
        [Description("Reserves Cup Name")]
        CNTReservesCupName = 8,
        [Description("U19 Cup Name")]
        CNTU19CupName = 9,
        [Description("U21 Cup Name")]
        CNTU21CupName = 10,
        [Description("U18 Cup Name")]
        CNTU18CupName = 11,
        [Description("U20 Cup Name")]
        CNTU20CupName = 12,
        [Description("Youth Cup U19 Name")]
        CNTYouthCupU19Name = 13,
        [Description("Friendly Cup")]
        CNTFriendlyCup = 14
    }

    public class Competition : BaseObject, ICompetition
    {
        private enum CompetitionFlags
        {
            CNTUsesSquadNumbers = 0x1,
            CNTIsExtinct = 0x4
        }
        public CompetitionOffsets CompetitionOffsets;
        public Competition(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.CompetitionOffsets = new CompetitionOffsets(Version);
        }
        public Competition(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.CompetitionOffsets = new CompetitionOffsets(Version);
        }

        public void Save()
        {
            PropertyInvoker.Set<Int64>(CompetitionOffsets.NorthCity, OriginalBytes, MemoryAddress, DatabaseMode, NorthCityAddress.GetValueOrDefault(0));
            PropertyInvoker.Set<Int64>(CompetitionOffsets.SouthCity, OriginalBytes, MemoryAddress, DatabaseMode, SouthCityAddress.GetValueOrDefault(0));
            PropertyInvoker.Set<Int64>(CompetitionOffsets.WestCity, OriginalBytes, MemoryAddress, DatabaseMode, WestCityAddress.GetValueOrDefault(0));
            PropertyInvoker.Set<Int64>(CompetitionOffsets.EastCity, OriginalBytes, MemoryAddress, DatabaseMode, EastCityAddress.GetValueOrDefault(0));
            PropertyInvoker.Set<Color>(CompetitionOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode, ForegroundColour);
            PropertyInvoker.Set<Color>(CompetitionOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode, BackgroundColour);
            PropertyInvoker.Set<Color>(CompetitionOffsets.TrimColour, OriginalBytes, MemoryAddress, DatabaseMode, TrimColour);
            PropertyInvoker.Set<Int16>(CompetitionOffsets.MinimumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode, MinimumPitchLength.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.MaximumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode, MaximumPitchLength.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.MinimumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode, MinimumPitchWidth.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.MaximumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode, MaximumPitchWidth.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode, Reputation.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.OriginalReputation, OriginalBytes, MemoryAddress, DatabaseMode, OriginalReputation.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.LastReputationPos, OriginalBytes, MemoryAddress, DatabaseMode, LastReputationPos.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.CurrentReputation, OriginalBytes, MemoryAddress, DatabaseMode, CurrentReputation.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(CompetitionOffsets.PercentageOfTopDivisionReputation, OriginalBytes, MemoryAddress, DatabaseMode, PercentageOfTopDivisionReputation.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(CompetitionOffsets.NameType, OriginalBytes, MemoryAddress, DatabaseMode, NameType.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(CompetitionOffsets.DivisionLevel, OriginalBytes, MemoryAddress, DatabaseMode, DivisionLevel.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(CompetitionOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type.GetValueOrDefault(0));
            PropertyInvoker.Set<bool>(CompetitionOffsets.UsesSeatedOnlyStadiums, OriginalBytes, MemoryAddress, DatabaseMode, UsesSeatedOnlyStadiums.GetValueOrDefault(false));
            PropertyInvoker.Set<byte>(CompetitionOffsets.WageBudgetTurnoverPercentage, OriginalBytes, MemoryAddress, DatabaseMode, WageBudgetTurnoverPercentage.GetValueOrDefault(0));
            PropertyInvoker.Set<bool>(CompetitionOffsets.UsesExtraOfficials, OriginalBytes, MemoryAddress, DatabaseMode, UsesExtraOfficials.GetValueOrDefault(false));

            byte newFlags = 0x0;
            newFlags |= (IsExtinct.GetValueOrDefault(false) ? (byte)CompetitionFlags.CNTIsExtinct : (byte)0);
            newFlags |= (UsesSquadNumbers.GetValueOrDefault(false) ? (byte)CompetitionFlags.CNTUsesSquadNumbers : (byte)0);
            PropertyInvoker.Set<byte>(CompetitionOffsets.Flags, OriginalBytes, MemoryAddress, DatabaseMode, newFlags);

            isDirty = false;
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

        public string Offset
        {
            get
            {
                return "0x" + MemoryAddress.ToString("X");
            }
        }

        public Int32 RowID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(CompetitionOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Int32 UID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(CompetitionOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name
        {
            get
            {
                return PropertyInvoker.GetString(CompetitionOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string ShortName
        {
            get
            {
                return PropertyInvoker.GetString(CompetitionOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string ThreeLetterName
        {
            get
            {
                return PropertyInvoker.GetString(CompetitionOffsets.ThreeLetterName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Continent Continent
        {
            get
            {
                return PropertyInvoker.GetPointer<Continent>(CompetitionOffsets.Continent, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public Nation Nation
        {
            get
            {
                return PropertyInvoker.GetPointer<Nation>(CompetitionOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public Competition ParentCompetition
        {
            get
            {
                return PropertyInvoker.GetPointer<Competition>(CompetitionOffsets.ParentCompetition, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private Int64? _northCityAddress;
        public Int64? NorthCityAddress
        {
            get
            {
                if (_northCityAddress == null)
                {
                    _northCityAddress = PropertyInvoker.Get<Int64>(CompetitionOffsets.NorthCity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _northCityAddress;
            }
            set
            {
                if (_northCityAddress != value)
                {
                    _northCityAddress = value;
                    isDirty = true;
                }
            }
        }

        private City _northCity;
        public City NorthCity
        {
            get
            {
                if (_northCity == null)
                {
                    _northCity = new City(NorthCityAddress.GetValueOrDefault(0), Version);
                }
                return _northCity;
            }
            set
            {
                if (_northCity != value)
                {
                    _northCity = value;
                }
            }
        }

        private Int64? _southCityAddress;
        public Int64? SouthCityAddress
        {
            get
            {
                if (_southCityAddress == null)
                {
                    _southCityAddress = PropertyInvoker.Get<Int64>(CompetitionOffsets.SouthCity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _southCityAddress;
            }
            set
            {
                if (_southCityAddress != value)
                {
                    _southCityAddress = value;
                    isDirty = true;
                }
            }
        }

        private City _southCity;
        public City SouthCity
        {
            get
            {
                if (_southCity == null)
                {
                    _southCity = new City(SouthCityAddress.GetValueOrDefault(0), Version);
                }
                return _southCity;
            }
            set
            {
                if (_southCity != value)
                {
                    _southCity = value;
                }
            }
        }

        private Int64? _westCityAddress;
        public Int64? WestCityAddress
        {
            get
            {
                if (_westCityAddress == null)
                {
                    _westCityAddress = PropertyInvoker.Get<Int64>(CompetitionOffsets.WestCity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _westCityAddress;
            }
            set
            {
                if (_westCityAddress != value)
                {
                    _westCityAddress = value;
                    isDirty = true;
                }
            }
        }

        private City _westCity;
        public City WestCity
        {
            get
            {
                if (_westCity == null)
                {
                    _westCity = new City(WestCityAddress.GetValueOrDefault(0), Version);
                }
                return _westCity;
            }
            set
            {
                if (_westCity != value)
                {
                    _westCity = value;
                }
            }
        }

        private Int64? _eastCityAddress;
        public Int64? EastCityAddress
        {
            get
            {
                if (_eastCityAddress == null)
                {
                    _eastCityAddress = PropertyInvoker.Get<Int64>(CompetitionOffsets.EastCity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _eastCityAddress;
            }
            set
            {
                if (_eastCityAddress != value)
                {
                    _eastCityAddress = value;
                    isDirty = true;
                }
            }
        }

        private City _eastCity;
        public City EastCity
        {
            get
            {
                if (_eastCity == null)
                {
                    _eastCity = new City(EastCityAddress.GetValueOrDefault(0), Version);
                }
                return _eastCity;
            }
            set
            {
                if (_eastCity != value)
                {
                    _eastCity = value;
                }
            }
        }

        // TODO: PastWinners / Alternative Names?

        public ActualCompetition ActualCompetition
        {
            get
            {
                return PropertyInvoker.GetPointer<ActualCompetition>(CompetitionOffsets.ActualCompetition, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        // TODO: Champions

        private Color _foregroundColour;
        public Color ForegroundColour
        {
            get
            {
                if (_foregroundColour.IsEmpty)
                {
                    _foregroundColour = PropertyInvoker.Get<Color>(CompetitionOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _foregroundColour;
            }
            set
            {
                if (_foregroundColour != value)
                {
                    _foregroundColour = value;
                    isDirty = true;
                }
            }
        }

        private Color _backgroundColour;
        public Color BackgroundColour
        {
            get
            {
                if (_backgroundColour.IsEmpty)
                {
                    _backgroundColour = PropertyInvoker.Get<Color>(CompetitionOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _backgroundColour;
            }
            set
            {
                if (_backgroundColour != value)
                {
                    _backgroundColour = value;
                    isDirty = true;
                }
            }
        }

        private Color _trimColour;
        public Color TrimColour
        {
            get
            {
                if (_trimColour.IsEmpty)
                {
                    _trimColour = PropertyInvoker.Get<Color>(CompetitionOffsets.TrimColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _trimColour;
            }
            set
            {
                if (_trimColour != value)
                {
                    _trimColour = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _minimumPitchLength;
        public Int16? MinimumPitchLength
        {
            get
            {
                if (_minimumPitchLength == null)
                {
                    _minimumPitchLength = PropertyInvoker.Get<Int16>(CompetitionOffsets.MinimumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _minimumPitchLength;
            }
            set
            {
                if (_minimumPitchLength != value)
                {
                    _minimumPitchLength = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _maximumPitchLength;
        public Int16? MaximumPitchLength
        {
            get
            {
                if (_maximumPitchLength == null)
                {
                    _maximumPitchLength = PropertyInvoker.Get<Int16>(CompetitionOffsets.MaximumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _maximumPitchLength;
            }
            set
            {
                if (_maximumPitchLength != value)
                {
                    _maximumPitchLength = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _minimumPitchWidth;
        public Int16? MinimumPitchWidth
        {
            get
            {
                if (_minimumPitchWidth == null)
                {
                    _minimumPitchWidth = PropertyInvoker.Get<Int16>(CompetitionOffsets.MinimumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _minimumPitchWidth;
            }
            set
            {
                if (_minimumPitchWidth != value)
                {
                    _minimumPitchWidth = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _maximumPitchWidth;
        public Int16? MaximumPitchWidth
        {
            get
            {
                if (_maximumPitchWidth == null)
                {
                    _maximumPitchWidth = PropertyInvoker.Get<Int16>(CompetitionOffsets.MaximumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _maximumPitchWidth;
            }
            set
            {
                if (_maximumPitchWidth != value)
                {
                    _maximumPitchWidth = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _reputation;
        public Int16? Reputation
        {
            get
            {
                if (_reputation == null)
                {
                    _reputation = PropertyInvoker.Get<Int16>(CompetitionOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _reputation;
            }
            set
            {
                if (_reputation != value)
                {
                    _reputation = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _originalReputation;
        public Int16? OriginalReputation
        {
            get
            {
                if (_originalReputation == null)
                {
                    _originalReputation = PropertyInvoker.Get<Int16>(CompetitionOffsets.OriginalReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _originalReputation;
            }
            set
            {
                if (_originalReputation != value)
                {
                    _originalReputation = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _lastReputationPos;
        public Int16? LastReputationPos
        {
            get
            {
                if (_lastReputationPos == null)
                {
                    _lastReputationPos = PropertyInvoker.Get<Int16>(CompetitionOffsets.LastReputationPos, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _lastReputationPos;
            }
            set
            {
                if (_lastReputationPos != value)
                {
                    _lastReputationPos = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _currentReputation;
        public Int16? CurrentReputation
        {
            get
            {
                if (_currentReputation == null)
                {
                    _currentReputation = PropertyInvoker.Get<Int16>(CompetitionOffsets.CurrentReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _currentReputation;
            }
            set
            {
                if (_currentReputation != value)
                {
                    _currentReputation = value;
                    isDirty = true;
                }
            }
        }

        private byte? _percentageOfTopDivisionReputation;
        public byte? PercentageOfTopDivisionReputation
        {
            get
            {
                if (_percentageOfTopDivisionReputation == null)
                {
                    _percentageOfTopDivisionReputation = PropertyInvoker.Get<byte>(CompetitionOffsets.PercentageOfTopDivisionReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _percentageOfTopDivisionReputation;
            }
            set
            {
                if (_percentageOfTopDivisionReputation != value)
                {
                    _percentageOfTopDivisionReputation = value;
                    isDirty = true;
                }
            }
        }

        private byte? _nameType;
        public byte? NameType
        {
            get
            {
                if (_nameType == null)
                {
                    _nameType = PropertyInvoker.Get<byte>(CompetitionOffsets.NameType, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _nameType;
            }
            set
            {
                if (_nameType != value)
                {
                    _nameType = value;
                    isDirty = true;
                }
            }
        }

        private byte? _divisionLevel;
        public byte? DivisionLevel
        {
            get
            {
                if (_divisionLevel == null)
                {
                    _divisionLevel = PropertyInvoker.Get<byte>(CompetitionOffsets.DivisionLevel, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _divisionLevel;
            }
            set
            {
                if (_divisionLevel != value)
                {
                    _divisionLevel = value;
                    isDirty = true;
                }
            }
        }

        private byte? _type;
        public byte? Type
        {
            get
            {
                if (_type == null)
                {
                    _type = PropertyInvoker.Get<byte>(CompetitionOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    isDirty = true;
                }
            }
        }

        private bool? _usesSeatedOnlyStadiums;
        public bool? UsesSeatedOnlyStadiums
        {
            get
            {
                if (_usesSeatedOnlyStadiums == null)
                {
                    _usesSeatedOnlyStadiums = PropertyInvoker.Get<bool>(CompetitionOffsets.UsesSeatedOnlyStadiums, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _usesSeatedOnlyStadiums;
            }
            set
            {
                if (_usesSeatedOnlyStadiums != value)
                {
                    _usesSeatedOnlyStadiums = value;
                    isDirty = true;
                }
            }
        }

        private byte? _wageBudgetTurnoverPercentage;
        public byte? WageBudgetTurnoverPercentage
        {
            get
            {
                if (_wageBudgetTurnoverPercentage == null)
                {
                    _wageBudgetTurnoverPercentage = PropertyInvoker.Get<byte>(CompetitionOffsets.WageBudgetTurnoverPercentage, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _wageBudgetTurnoverPercentage;
            }
            set
            {
                if (_wageBudgetTurnoverPercentage != value)
                {
                    _wageBudgetTurnoverPercentage = value;
                    isDirty = true;
                }
            }
        }

        private bool? _usesExtraOfficials;
        public bool? UsesExtraOfficials
        {
            get
            {
                if (_usesExtraOfficials == null)
                {
                    _usesExtraOfficials = PropertyInvoker.Get<bool>(CompetitionOffsets.UsesExtraOfficials, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _usesExtraOfficials;
            }
            set
            {
                if (_usesExtraOfficials != value)
                {
                    _usesExtraOfficials = value;
                    isDirty = true;
                }
            }
        }

        private byte? _flags;
        public byte? Flags
        {
            get
            {
                if (_flags == null)
                {
                    _flags = PropertyInvoker.Get<byte>(CompetitionOffsets.Flags, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _flags;
            }
            set
            {
                if (_flags != value)
                {
                    _flags = value;
                    isDirty = true;
                }
            }
        }

        private bool? _isExtinct;
        public bool? IsExtinct
        {
            get
            {
                if (_isExtinct == null)
                {
                    _isExtinct = (Flags.GetValueOrDefault(0) & (byte)CompetitionFlags.CNTIsExtinct) > 0 ? true : false;
                }
                return _isExtinct;
            }
            set
            {
                if (_isExtinct != value)
                {
                    _isExtinct = value;
                    isDirty = true;
                }
            }
        }

        private bool? _usesSquadNumbers;
        public bool? UsesSquadNumbers
        {
            get
            {
                if (_usesSquadNumbers == null)
                {
                    _usesSquadNumbers = (Flags.GetValueOrDefault(0) & (byte)CompetitionFlags.CNTUsesSquadNumbers) > 0 ? true : false;
                }
                return _usesSquadNumbers;
            }
            set
            {
                if (_usesSquadNumbers != value)
                {
                    _usesSquadNumbers = value;
                    isDirty = true;
                }
            }
        }

        public bool HasStages
        {
            get
            {
                return (ActualCompetition != null && ActualCompetition.LeagueStages.Count > 0);
            }
        }

        public bool HasPrizeMoney
        {
            get
            {
                bool result = false;
                if (this.HasStages)
                {
                    foreach (LeagueStage stage in ActualCompetition.LeagueStages)
                    {
                        if (stage.Settings != null)
                        {
                            result |= stage.Settings.PrizeMoney.Count > 0 ? true : false;
                        }
                    }
                }

                return result;
            }
        }

        public bool HasTable
        {
            get
            {
                bool result = false;
                if (this.HasStages)
                {
                    foreach (LeagueStage stage in ActualCompetition.LeagueStages)
                    {
                        if (stage.LeagueTable.Count > 0)
                        {
                            result = true;
                        }
                    }
                }

                return result;
            }
        }
    }
}