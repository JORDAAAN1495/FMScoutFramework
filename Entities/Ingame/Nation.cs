using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;

namespace FMScoutFramework.Core.Entities.InGame {
  public enum GainNationalityType {
    [Description("Continuous")]
    GNTContinuous     = 1,
    [Description("Accumulative")]
    GNTAccumulative   = 2
  }

  public enum NationImportanceType {
    [Description("Not Set")]
    NINotSet          = 0,
    [Description("Very Important")]
    NIVeryImportant   = 1,
    [Description("Important")]
    NIImportant       = 2,
    [Description("Unimportant")]
    NIUnimportant     = 3,
    [Description("Useless")]
    NIUseless         = 4
  }

  public enum NationStateOfDevelopmentType {
    [Description("Not Set")]
    NSDTNotSet        = 0,
    [Description("Developed")]
    NSDTDeveloped     = 1,
    [Description("Developing")]
    NSDTDeveloping    = 2,
    [Description("Third World")]
    NSDTThirdWorld    = 3
  }

  public class Nation : BaseObject, INation {
    public NationOffsets NationOffsets;
    public Nation(Int64 memoryAddress, IVersion version)
        : base(memoryAddress, version) {
      this.NationOffsets = new NationOffsets(Version);
    }
    public Nation(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
        : base(memoryAddress, originalBytes, version) {
      this.NationOffsets = new NationOffsets(Version);
    }

    public void Save() {
      if (_haveAgreementsResized) {
        // Need to do some stuff to reallocate agreements in order to resize
        // Length is 0x8 + 0x4 + 0x4 (Int64Ptr, Date, Date)
        int totalLength = 0x10 * this.Agreements.Count;
        Int64 newAddress = ProcessManager.AllocateProcessBytes(totalLength);
        Int64 endAddress = newAddress + totalLength;

        // Start writing at the new address
        int offset = 0x0;
        foreach (NationAgreement agreement in this.Agreements) {
          PropertyInvoker.Set<Int64>((offset + NationAgreementOffsets.AgreementAddress), OriginalBytes, newAddress, DatabaseMode, agreement.AgreementAddress);
          PropertyInvoker.Set<DateTime>((offset + NationAgreementOffsets.StartDate), OriginalBytes, newAddress, DatabaseMode, agreement.StartDate);
          PropertyInvoker.Set<DateTime>((offset + NationAgreementOffsets.EndDate), OriginalBytes, newAddress, DatabaseMode, agreement.EndDate);

          offset += 0x10;
        }

        // Write out the pointer to the new address as an array in the Nation object
        PropertyInvoker.Set<Int64>(NationOffsets.Agreements, OriginalBytes, MemoryAddress, DatabaseMode, newAddress);
        PropertyInvoker.Set<Int64>((NationOffsets.Agreements + 0x8), OriginalBytes, MemoryAddress, DatabaseMode, endAddress);
        PropertyInvoker.Set<Int64>((NationOffsets.Agreements + 0x10), OriginalBytes, MemoryAddress, DatabaseMode, endAddress);
      }

      PropertyInvoker.Set<Int64>(NationOffsets.Capital, OriginalBytes, MemoryAddress, DatabaseMode, CapitalAddress);
      PropertyInvoker.Set<short>(NationOffsets.FIFAPosition, OriginalBytes, MemoryAddress, DatabaseMode, FIFAPosition);
      PropertyInvoker.Set<short>(NationOffsets.FIFARankingPoints, OriginalBytes, MemoryAddress, DatabaseMode, FIFARankingPoints);
      PropertyInvoker.Set<bool>(NationOffsets.DoesNotAllowDualNationality, OriginalBytes, MemoryAddress, DatabaseMode, DoesNotAllowDualNationality.GetValueOrDefault(false));
      PropertyInvoker.Set<byte>(NationOffsets.YearsToGainNationality, OriginalBytes, MemoryAddress, DatabaseMode, YearsToGainNationality);
      PropertyInvoker.Set<byte>(NationOffsets.GainNationalityType, OriginalBytes, MemoryAddress, DatabaseMode, GainNationalityType);
      PropertyInvoker.Set<byte>(NationOffsets.ForeignManagerLikelihood, OriginalBytes, MemoryAddress, DatabaseMode, ForeignManagerLikelihood);
      PropertyInvoker.Set<byte>(NationOffsets.EconomicFactor, OriginalBytes, MemoryAddress, DatabaseMode, EconomicFactor);
      PropertyInvoker.Set<byte>(NationOffsets.MaxYouthAge, OriginalBytes, MemoryAddress, DatabaseMode, MaxYouthAge);
      PropertyInvoker.Set<byte>(NationOffsets.Importance, OriginalBytes, MemoryAddress, DatabaseMode, Importance);
      PropertyInvoker.Set<byte>(NationOffsets.StateOfDevelopment, OriginalBytes, MemoryAddress, DatabaseMode, StateOfDevelopment);

      _isDirty = false;
    }

    private bool _haveAgreementsResized = false;

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

    public string Offset {
      get {
        return "0x" + MemoryAddress.ToString("X");
      }
    }

    public Int32 RowID {
      get {
        return ProcessManager.ReadInt32(MemoryAddress + NationOffsets.RowID);
      }
    }

    public Int32 UID {
      get {
        return ProcessManager.ReadInt32(MemoryAddress + NationOffsets.UID);
      }
    }

    public Team[] Teams {
      get {
        int teamCount = ProcessManager.ReadArrayLength(MemoryAddress + NationOffsets.Teams);
        Team[] result = new Team[teamCount];

        for (int i = 0; i < teamCount; i++) {
          Int64 teamAddress = PropertyInvoker.Get<Int64>(NationOffsets.Teams, OriginalBytes, MemoryAddress, DatabaseMode);
          result[i] = PropertyInvoker.GetPointer<Team>(0x0, OriginalBytes, (teamAddress + (i * 0x8)), DatabaseMode, Version);
        }

        return result;
      }
    }

    public RivalNation[] RivalNations {
      get {
        Int64 nationCount = ProcessManager.ReadArrayLength(MemoryAddress + NationOffsets.RivalNations, 0xC);
        RivalNation[] result = new RivalNation[nationCount];

        for (int i = 0; i < nationCount; i++) {
          int nationAddress = PropertyInvoker.Get<Int32>(NationOffsets.RivalNations, OriginalBytes, MemoryAddress, DatabaseMode);
          result[i] = new RivalNation((nationAddress + (i * 0xC)), Version);
        }

        return result;
      }
    }

    private List<NationAgreement> _agreements = new List<NationAgreement>();
    public List<NationAgreement> Agreements {
      get {
        if (_agreements.Count == 0) {
          Int64 agreementsCount = ProcessManager.ReadArrayLength(MemoryAddress + NationOffsets.Agreements, 0x10);
          if (agreementsCount > 0) {
            Int64 AgreementsArrayAddress = PropertyInvoker.Get<Int64>(NationOffsets.Agreements, OriginalBytes, MemoryAddress, DatabaseMode);
            for (int i = 0; i < agreementsCount; i++) {
              _agreements.Add(new NationAgreement((AgreementsArrayAddress + (i * 0x10)), Version));
            }
          }
        }

        return _agreements;
      }
      set {
        if (_agreements != value) {
          _agreements = value;
          _haveAgreementsResized = true;
          isDirty = true;
        }
      }
    }

    private ClubInfoOne _clubInfoOne;
    public ClubInfoOne ClubInfoOne {
      get {
        if (_clubInfoOne == null) {
          _clubInfoOne = PropertyInvoker.GetPointer<ClubInfoOne>(NationOffsets.ClubInfoOne, OriginalBytes, MemoryAddress, DatabaseMode, this.Version);
        }

        return _clubInfoOne;
      }
    }

    public string Name {
      get {
        return PropertyInvoker.GetString(NationOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
      }
    }

    public string ShortName {
      get {
        return PropertyInvoker.GetString(NationOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
      }
    }

    public string ThreeLetterName {
      get {
        return PropertyInvoker.GetString(NationOffsets.ThreeLetterName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
      }
    }

    public string NationalityName {
      get {
        return PropertyInvoker.GetString(NationOffsets.Nationality, -1, OriginalBytes, MemoryAddress, DatabaseMode);
      }
    }

    private short _FIFAPosition = 0;
    public short FIFAPosition {
      get {
        if (_FIFAPosition == 0) {
          _FIFAPosition = PropertyInvoker.Get<short>(NationOffsets.FIFAPosition, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _FIFAPosition;
      }
      set {
        _FIFAPosition = value;
        isDirty = true;
      }
    }

    private short _FIFARankingPoints = 0;
    public short FIFARankingPoints {
      get {
        if (_FIFARankingPoints == 0) {
          _FIFARankingPoints = PropertyInvoker.Get<short>(NationOffsets.FIFARankingPoints, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _FIFARankingPoints;
      }
      set {
        _FIFARankingPoints = value;
        isDirty = true;
      }
    }

    private Int64 _capitalAddress = 0x0;
    public Int64 CapitalAddress {
      get {
        if (_capitalAddress == 0x0) {
          _capitalAddress = PropertyInvoker.Get<Int64>(NationOffsets.Capital, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _capitalAddress;
      }
      set {
        if (_capitalAddress != value) {
          isDirty = true;
          _capitalAddress = value;
          _capital = null;
        }
      }
    }

    private City _capital;
    public City Capital {
      get {
        if (_capital == null) {
          _capital = new City(CapitalAddress, Version);
        }
        return _capital;
      }
    }

    public Continent Continent {
      get {
        return PropertyInvoker.GetPointer<Continent>(NationOffsets.Continent, OriginalBytes, MemoryAddress, DatabaseMode, this.Version);
      }
    }

    private byte _gainNationalityType = 0;
    public byte GainNationalityType {
      get {
        if (_gainNationalityType == 0) {
          _gainNationalityType = PropertyInvoker.Get<byte>(NationOffsets.GainNationalityType, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _gainNationalityType;
      }
      set {
        if (_gainNationalityType != value) {
          _gainNationalityType = value;
          isDirty = true;
        }
      }
    }

    private bool? _doesNotAllowDualNationality = null;
    public bool? DoesNotAllowDualNationality {
      get {
        if (_doesNotAllowDualNationality == null) {
          _doesNotAllowDualNationality = PropertyInvoker.Get<bool>(NationOffsets.DoesNotAllowDualNationality, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _doesNotAllowDualNationality;
      }
      set {
        if (_doesNotAllowDualNationality != value) {
          _doesNotAllowDualNationality = value;
          isDirty = true;
        }
      }
    }

    private byte _yearsToGainNationality = 0;
    public byte YearsToGainNationality {
      get {
        if (_yearsToGainNationality == 0) {
          _yearsToGainNationality = PropertyInvoker.Get<byte>(NationOffsets.YearsToGainNationality, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _yearsToGainNationality;
      }
      set {
        if (_yearsToGainNationality != value) {
          _yearsToGainNationality = value;
          isDirty = true;
        }
      }
    }

    private byte _foreignManagerLikelihood = 0;
    public byte ForeignManagerLikelihood {
      get {
        if (_foreignManagerLikelihood == 0) {
          _foreignManagerLikelihood = PropertyInvoker.Get<byte>(NationOffsets.ForeignManagerLikelihood, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _foreignManagerLikelihood;
      }
      set {
        if (_foreignManagerLikelihood != value) {
          _foreignManagerLikelihood = value;
          isDirty = true;
        }
      }
    }

    private byte _economicFactor = 0;
    public byte EconomicFactor {
      get {
        if (_economicFactor == 0) {
          _economicFactor = PropertyInvoker.Get<byte>(NationOffsets.EconomicFactor, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _economicFactor;
      }
      set {
        if (_economicFactor != value) {
          _economicFactor = value;
          isDirty = true;
        }
      }
    }

    private byte _maxYouthAge = 0;
    public byte MaxYouthAge {
      get {
        if (_maxYouthAge == 0) {
          _maxYouthAge = PropertyInvoker.Get<byte>(NationOffsets.MaxYouthAge, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _maxYouthAge;
      }
      set {
        if (_maxYouthAge != value) {
          _maxYouthAge = value;
          isDirty = true;
        }
      }
    }

    private byte _importance = 0;
    public byte Importance {
      get {
        if (_importance == 0) {
          _importance = PropertyInvoker.Get<byte>(NationOffsets.Importance, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _importance;
      }
      set {
        if (_importance != value) {
          _importance = value;
          isDirty = true;
        }
      }
    }

    private byte _stateOfDevelopment = 0;
    public byte StateOfDevelopment {
      get {
        if (_stateOfDevelopment == 0) {
          _stateOfDevelopment = PropertyInvoker.Get<byte>(NationOffsets.StateOfDevelopment, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _stateOfDevelopment;
      }
      set {
        if (_stateOfDevelopment != value) {
          _stateOfDevelopment = value;
          isDirty = true;
        }
      }
    }

    public override string ToString() {
      return Name;
    }
  }
}
