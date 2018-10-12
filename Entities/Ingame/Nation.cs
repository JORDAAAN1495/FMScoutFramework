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
      PropertyInvoker.Set<Int64>(NationOffsets.Capital, OriginalBytes, MemoryAddress, DatabaseMode, CapitalAddress);
      PropertyInvoker.Set<short>(NationOffsets.FIFAPosition, OriginalBytes, MemoryAddress, DatabaseMode, FIFAPosition);
      PropertyInvoker.Set<short>(NationOffsets.FIFARankingPoints, OriginalBytes, MemoryAddress, DatabaseMode, FIFARankingPoints);
      PropertyInvoker.Set<bool>(NationOffsets.DoesNotAllowDualNationality, OriginalBytes, MemoryAddress, DatabaseMode, DoesNotAllowDualNationality.GetValueOrDefault(false));
      PropertyInvoker.Set<byte>(NationOffsets.YearsToGainNationality, OriginalBytes, MemoryAddress, DatabaseMode, YearsToGainNationality);
      PropertyInvoker.Set<byte>(NationOffsets.ForeignManagerLikelihood, OriginalBytes, MemoryAddress, DatabaseMode, ForeignManagerLikelihood);
      PropertyInvoker.Set<byte>(NationOffsets.EconomicFactor, OriginalBytes, MemoryAddress, DatabaseMode, EconomicFactor);
      PropertyInvoker.Set<byte>(NationOffsets.MaxYouthAge, OriginalBytes, MemoryAddress, DatabaseMode, MaxYouthAge);
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
          int teamAddress = PropertyInvoker.Get<Int32>(NationOffsets.Teams, OriginalBytes, MemoryAddress, DatabaseMode);
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
          isDirty = true;
        }
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

    public override string ToString() {
      return Name;
    }
  }
}
