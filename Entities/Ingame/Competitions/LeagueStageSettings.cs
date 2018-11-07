using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Defines.Offsets;
using FMScoutFramework.Core.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame {
  public class LeagueStageSettings : BaseObject, ILeaguesStageSettings {

    private enum RankingFlags : byte {
      RFRanksTeams = 0x2
    };
    public LeagueStageSettingsOffsets Offsets;

    public LeagueStageSettings(Int64 memoryAddress, IVersion version)
        : base(memoryAddress, version) {
      this.Offsets = new LeagueStageSettingsOffsets(Version);
    }
    public LeagueStageSettings(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
        : base(memoryAddress, originalBytes, version) {
      this.Offsets = new LeagueStageSettingsOffsets(Version);
    }

    public void Save() {
      PropertyInvoker.Set<byte>(Offsets.StageName, OriginalBytes, MemoryAddress, DatabaseMode, (byte)(RanksTeams.GetValueOrDefault(true) ? 0 : 2));
      PropertyInvoker.Set<byte>(Offsets.NumberOfTeams, OriginalBytes, MemoryAddress, DatabaseMode, NumberOfTeams.GetValueOrDefault(0));
      PropertyInvoker.Set<bool>(Offsets.IsStageHidden, OriginalBytes, MemoryAddress, DatabaseMode, isStageHidden.GetValueOrDefault(false));
      PropertyInvoker.Set<DateTime>(Offsets.DrawDate, OriginalBytes, MemoryAddress, DatabaseMode, DrawDate);

      Int64 uhsPointer = PropertyInvoker.Get<Int16>(Offsets.UsesHomeStadiums, OriginalBytes, MemoryAddress, DatabaseMode);
      if (uhsPointer > 0x0) {
        Int64 uhsPointerPointer = PropertyInvoker.Get<Int16>(0, OriginalBytes, uhsPointer, DatabaseMode);
        if (uhsPointerPointer > 0x0) {
          PropertyInvoker.Set<bool>(0x18, OriginalBytes, MemoryAddress, DatabaseMode, UsesHomeStadiums.GetValueOrDefault(false));
        }
      }

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

    private string _stageName;
    public string StageName {
      get {
        if (String.IsNullOrEmpty(_stageName)) {
          _stageName = PropertyInvoker.GetString(Offsets.StageName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
          if (_stageName == "-") {
            _stageName = "League Table";
          }
        }
        return _stageName;
      }
      set {
        if (_stageName != value) {
          _stageName = value;
        }
      }
    }

    private bool? _ranksTeams;
    public bool? RanksTeams {
      get {
        if (_ranksTeams == null) {
          byte ranksTeamsVal = PropertyInvoker.Get<byte>(Offsets.RanksTeams, OriginalBytes, MemoryAddress, DatabaseMode);
          _ranksTeams = (ranksTeamsVal & (byte)RankingFlags.RFRanksTeams) == 0 ? true : false;
        }

        return _ranksTeams;
      }
      set {
        if (_ranksTeams != value) {
          _ranksTeams = value;
          isDirty = true;
        }
      }
    }

    private byte? _numberOfTeams;
    public byte? NumberOfTeams {
      get {
        if (_numberOfTeams == null) {
          _numberOfTeams = PropertyInvoker.Get<byte>(Offsets.NumberOfTeams, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _numberOfTeams;
      }
      set {
        if (_numberOfTeams != value) {
          _numberOfTeams = value;
          isDirty = true;
        }
      }
    }

    private bool? _isStageHidden;
    public bool? isStageHidden {
      get {
        if (_isStageHidden == null) {
          _isStageHidden = PropertyInvoker.Get<bool>(Offsets.IsStageHidden, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _isStageHidden;
      }
      set {
        if (_isStageHidden != value) {
          _isStageHidden = value;
          isDirty = true;
        }
      }
    }

    private bool? _usesHomeStadiums;
    public bool? UsesHomeStadiums {
      get {
        if (_usesHomeStadiums == null) {
          Int64 pointer = PropertyInvoker.Get<Int16>(Offsets.UsesHomeStadiums, OriginalBytes, MemoryAddress, DatabaseMode);
          if (pointer > 0x0) {
            Int64 pointerPointer = PropertyInvoker.Get<Int16>(0, OriginalBytes, pointer, DatabaseMode);
            if (pointerPointer > 0x0) {
              _usesHomeStadiums = PropertyInvoker.Get<bool>(0x18, OriginalBytes, pointerPointer, DatabaseMode);
            }
          }
        }
        return _usesHomeStadiums;
      }
      set {
        if (_usesHomeStadiums != value) {
          _usesHomeStadiums = value;
          isDirty = true;
        }
      }
    }

    private List<LeaguePrizeMoney> _prizeMoney = new List<LeaguePrizeMoney>();
    public List<LeaguePrizeMoney> PrizeMoney {
      get {
        if (_prizeMoney.Count == 0) {
          Int64 numberOfPrizes = ProcessManager.ReadArrayLength((Offsets.PrizeMoney + MemoryAddress), 0x4);
          if (numberOfPrizes > 0) {
            Int64 startAddress = PropertyInvoker.Get<Int64>(Offsets.PrizeMoney, OriginalBytes, MemoryAddress, DatabaseMode);
            for (Int64 i = 0; i < numberOfPrizes; i++) {
              _prizeMoney.Add(new LeaguePrizeMoney((startAddress + (i * 0x4)), Version));
            }
          }
        }

        return _prizeMoney;
      }
    }

    private DateTime _drawDate;
    public DateTime DrawDate {
      get {
        if (_drawDate.Year < 1900) {
          _drawDate = PropertyInvoker.Get<DateTime>(Offsets.DrawDate, OriginalBytes, MemoryAddress, DatabaseMode);
        }
        return _drawDate;
      }
      set {
        if (_drawDate != value) {
          _drawDate = value;
          isDirty = true;
        }
      }
    }
  }
}
