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
    public class LeagueTableEntry : BaseObject, ILeagueTableEntry
    {
        public LeagueTableEntryOffsets Offsets;
        public LeagueTableEntry(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version){
            this.Offsets = new LeagueTableEntryOffsets(Version);
        }
        public LeagueTableEntry(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version){
            this.Offsets = new LeagueTableEntryOffsets(Version);
        }

        public void Save() {
            PropertyInvoker.Set<byte>(Offsets.GoalsScored, OriginalBytes, MemoryAddress, DatabaseMode, GoalsScored.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.GoalsAgainst, OriginalBytes, MemoryAddress, DatabaseMode, GoalsAgainst.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.Points, OriginalBytes, MemoryAddress, DatabaseMode, Points.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.GamesPlayed, OriginalBytes, MemoryAddress, DatabaseMode, GamesPlayed.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.GamesWon, OriginalBytes, MemoryAddress, DatabaseMode, GamesWon.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.GamesDrawn, OriginalBytes, MemoryAddress, DatabaseMode, GamesDrawn.GetValueOrDefault(0));
            PropertyInvoker.Set<byte>(Offsets.GamesLost, OriginalBytes, MemoryAddress, DatabaseMode, GamesLost.GetValueOrDefault(0));
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

        private byte? _goalsScored;
        public byte? GoalsScored {
            get {
                if (_goalsScored == null) {
                    _goalsScored = PropertyInvoker.Get<byte>(Offsets.GoalsScored, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _goalsScored;
            }
            set {
                if (_goalsScored != value) {
                    _goalsScored = value;
                    isDirty = true;
                }
            }
        }

        private byte? _goalsAgainst;
        public byte? GoalsAgainst {
            get {
                if (_goalsAgainst == null) {
                    _goalsAgainst = PropertyInvoker.Get<byte>(Offsets.GoalsAgainst, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _goalsAgainst;
            }
            set {
                if (_goalsAgainst != value) {
                    _goalsAgainst = value;
                    isDirty = true;
                }
            }
        }

        private byte? _points;
        public byte? Points {
            get {
                if (_points == null) {
                    _points = PropertyInvoker.Get<byte>(Offsets.Points, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _points;
            }
            set {
                if (_points != value) {
                    _points = value;
                    isDirty = true;
                }
            }
        }

        private byte? _gamesPlayed;
        public byte? GamesPlayed {
            get {
                if (_gamesPlayed == null) {
                    _gamesPlayed = PropertyInvoker.Get<byte>(Offsets.GamesPlayed, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _gamesPlayed;
            }
            set {
                if (_gamesPlayed != value) {
                    _gamesPlayed = value;
                    isDirty = true;
                }
            }
        }

        private byte? _gamesWon;
        public byte? GamesWon {
            get {
                if (_gamesWon == null) {
                    _gamesWon = PropertyInvoker.Get<byte>(Offsets.GamesWon, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _gamesWon;
            }
            set {
                if (_gamesWon != value) {
                    _gamesWon = value;
                    isDirty = true;
                }
            }
        }

        private byte? _gamesDrawn;
        public byte? GamesDrawn {
            get {
                if (_gamesDrawn == null) {
                    _gamesDrawn = PropertyInvoker.Get<byte>(Offsets.GamesDrawn, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _gamesDrawn;
            }
            set {
                if (_gamesDrawn != value) {
                    _gamesDrawn = value;
                    isDirty = true;
                }
            }
        }

        private byte? _gamesLost;
        public byte? GamesLost {
            get {
                if (_gamesLost == null) {
                    _gamesLost = PropertyInvoker.Get<byte>(Offsets.GamesLost, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _gamesLost;
            }
            set {
                if (_gamesLost != value) {
                    _gamesLost = value;
                    isDirty = true;
                }
            }
        }

        public Team Team {
            get {
                return PropertyInvoker.GetPointer<Team>(Offsets.Team, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }
    }
}
