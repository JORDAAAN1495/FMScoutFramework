using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Defines.Offsets;
using FMScoutFramework.Core.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class LeagueStage : BaseObject, ILeaguesStage
    {
        public LeagueStageOffsets Offsets;
        public LeagueStage(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version){
            this.Offsets = new LeagueStageOffsets(Version);
        }
        public LeagueStage(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version){
            this.Offsets = new LeagueStageOffsets(Version);
        }

        public void Save() {
            PropertyInvoker.Set<byte>(Offsets.NumberOfTeams, OriginalBytes, MemoryAddress, DatabaseMode, NumberOfTeams.GetValueOrDefault(0));
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

        private List<LeagueTableEntry> _leagueTable = new List<LeagueTableEntry>();
        public List<LeagueTableEntry> LeagueTable {
            get {
                if (_leagueTable.Count == 0) {
                    Int64 count = ProcessManager.ReadArrayLength((MemoryAddress + Offsets.LeagueTable));
                    if (count > 0) {
                        Int64 startAddress = PropertyInvoker.Get<Int64>(Offsets.LeagueTable, OriginalBytes, MemoryAddress, DatabaseMode);
                        for (Int64 i = 0; i < count; i++) {
                            _leagueTable.Add(PropertyInvoker.GetPointer<LeagueTableEntry>((i * 0x8), OriginalBytes, startAddress, DatabaseMode, Version));
                        }
                    }
                }

                return _leagueTable;
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

        public LeagueStageSettings Settings {
            get {
                return PropertyInvoker.GetPointer<LeagueStageSettings>(Offsets.StageSettings, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }
    }
}
