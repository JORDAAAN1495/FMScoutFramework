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

    public Agreement Agreement {
      get {
        return PropertyInvoker.GetPointer<Agreement>(0x0, OriginalBytes, AgreementAddress, DatabaseMode, this.Version);
      }
    }
  }
}