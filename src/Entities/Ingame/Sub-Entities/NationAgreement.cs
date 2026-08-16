using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;

namespace FMScoutFramework.Core.Entities.InGame {
  public class NationAgreement : BaseObject, INationAgreement {
    public NationAgreementOffsets NationAgreementOffsets;

    public NationAgreement(Int64 memoryAddress, IVersion version)
      : base(memoryAddress, version) {
      this.NationAgreementOffsets = new NationAgreementOffsets(Version);
    }

    public NationAgreement(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
      : base(memoryAddress, originalBytes, version) {
      this.NationAgreementOffsets = new NationAgreementOffsets(Version);
    }

    public void Save() {
      PropertyInvoker.Set<Int64>(NationAgreementOffsets.AgreementAddress, OriginalBytes, MemoryAddress, DatabaseMode, this.AgreementAddress);
      isDirty = false;
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

    private Int64 _agreementAddress = 0x0;
    public Int64 AgreementAddress {
      get {
        if (_agreementAddress == 0x0) {
          _agreementAddress = PropertyInvoker.Get<Int64>(NationAgreementOffsets.AgreementAddress, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _agreementAddress;
      }
      set {
        if (_agreementAddress != value) {
          _agreementAddress = value;
          isDirty = true;
        }
      }
    }

    private Agreement _agreement;
    public Agreement Agreement {
      get {
        if (_agreement == null) {
          _agreement = PropertyInvoker.GetPointer<Agreement>(0x0, OriginalBytes, AgreementAddress, DatabaseMode, this.Version);
        }

        return _agreement;
      }
      set {
        if (_agreement != value) {
          _agreement = value;
        }
      }
    }

    private DateTime _startDate;
    public DateTime StartDate {
      get {
        if (_startDate.Year < 1900) {
          _startDate = PropertyInvoker.Get<DateTime>(NationAgreementOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _startDate;
      }
      set {
        if (_startDate != value) {
          _startDate = value;
          isDirty = true;
        }
      }
    }

    private DateTime _endDate;
    public DateTime EndDate {
      get {
        if (_endDate.Year < 1900) {
          _endDate = PropertyInvoker.Get<DateTime>(NationAgreementOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _endDate;
      }
      set {
        if (_endDate != value) {
          _endDate = value;
          isDirty = true;
        }
      }
    }
  }
}