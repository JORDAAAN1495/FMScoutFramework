using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Utilities;

namespace FMScoutFramework.Core.Entities.InGame {
  public class HumanManager : Person, IHumanManager {
    private HumanManagerOffsets Offsets;
    public Int64 Address;
    public HumanManager(Int64 memoryAddress, IVersion version)
      : base(memoryAddress + Math.Abs(version.PersonOffsets.HumanManager), version) {
      this.Offsets = new HumanManagerOffsets(version);
      this.Address = memoryAddress;
    }

    public HumanManager(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
      : base(memoryAddress + Math.Abs(version.PersonOffsets.HumanManager), originalBytes, version) {
      this.Offsets = new HumanManagerOffsets(version);
      this.Address = memoryAddress;
    }

    public void Save() {
      PropertyInvoker.Set<byte>(HumanManagerOffsets.Unsackable, OriginalBytes, Address, DatabaseMode, (IsUnsackable == true ? (byte)0x2 : (byte)0x0));
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

    private bool? _isUnsackable = null;
    public bool? IsUnsackable {
      get {
        if (_isUnsackable == null) {
          byte flag = PropertyInvoker.Get<byte>(HumanManagerOffsets.Unsackable, OriginalBytes, Address, DatabaseMode);
          if (flag == 0x2) {
            _isUnsackable = true;
          }
          else {
            _isUnsackable = false;
          }
        }

        return _isUnsackable;
      }
      set {
        if (_isUnsackable != value) {
          isDirty = true;
          _isUnsackable = value;
        }
      }
    }

    private ActualPerson _actualPerson = null;
    public ActualPerson ActualPerson {
      get {
        if (_actualPerson == null) {
          _actualPerson = new ActualPerson((Address + Offsets.ActualPerson), Version);
        }

        return _actualPerson;
      }
    }
  }
}
