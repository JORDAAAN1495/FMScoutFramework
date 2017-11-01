using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Defines.Offsets;
using FMScoutFramework.Core.Managers;
using System.Collections.Generic;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Competition : BaseObject, ICompetition
    {
        public CompetitionOffsets CompetitionOffsets;
        public Competition(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version){
            this.CompetitionOffsets = new CompetitionOffsets(Version);
        }
        public Competition(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version){
            this.CompetitionOffsets = new CompetitionOffsets(Version);
        }

        public void Save() {
            PropertyInvoker.Set<Int16>(CompetitionOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode, Reputation.GetValueOrDefault(0));
            PropertyInvoker.Set<Int16>(CompetitionOffsets.CurrentReputation, OriginalBytes, MemoryAddress, DatabaseMode, NationalReputation.GetValueOrDefault(0));
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

        public Int32 RowID {
            get {
                return PropertyInvoker.Get<Int32>(CompetitionOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Int32 UID {
            get {
                return PropertyInvoker.Get<Int32>(CompetitionOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name {
            get {
                return PropertyInvoker.GetString(CompetitionOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string ShortName {
            get {
                return PropertyInvoker.GetString(CompetitionOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string ThreeLetterName {
            get {
                return PropertyInvoker.GetString(CompetitionOffsets.ThreeLetterName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Nation Nation {
            get {
                return PropertyInvoker.GetPointer<Nation>(CompetitionOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private Int16? _reputation;
        public Int16? Reputation {
            get {
                if (_reputation == null) {
                    _reputation = PropertyInvoker.Get<Int16>(CompetitionOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _reputation;
            }
            set {
                if (_reputation != value) {
                    _reputation = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _nationalReputation;
        public Int16? NationalReputation {
            get {
                if (_nationalReputation == null) {
                    _nationalReputation = PropertyInvoker.Get<Int16>(CompetitionOffsets.CurrentReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _nationalReputation;
            }
            set {
                if (_nationalReputation != value) {
                    _nationalReputation = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _minimumPitchLength;
        public Int16? MinimumPitchLength {
            get {
                if (_minimumPitchLength == null) {
                    _minimumPitchLength = PropertyInvoker.Get<Int16>(CompetitionOffsets.MinimumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _minimumPitchLength;
            }
            set {
                if (_minimumPitchLength != value) {
                    _minimumPitchLength = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _maximumPitchLength;
        public Int16? MaximumPitchLength {
            get {
                if (_maximumPitchLength == null) {
                    _maximumPitchLength = PropertyInvoker.Get<Int16>(CompetitionOffsets.MaximumPitchLength, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _maximumPitchLength;
            }
            set {
                if (_maximumPitchLength != value) {
                    _maximumPitchLength = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _minimumPitchWidth;
        public Int16? MinimumPitchWidth {
            get {
                if (_minimumPitchWidth == null) {
                    _minimumPitchWidth = PropertyInvoker.Get<Int16>(CompetitionOffsets.MinimumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _minimumPitchWidth;
            }
            set {
                if (_minimumPitchWidth != value) {
                    _minimumPitchWidth = value;
                    isDirty = true;
                }
            }
        }

        private Int16? _maximumPitchWidth;
        public Int16? MaximumPitchWidth {
            get {
                if (_maximumPitchWidth == null) {
                    _maximumPitchWidth = PropertyInvoker.Get<Int16>(CompetitionOffsets.MaximumPitchWidth, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _maximumPitchWidth;
            }
            set {
                if (_maximumPitchWidth != value) {
                    _maximumPitchWidth = value;
                    isDirty = true;
                }
            }
        }

        //private List<Int64> _infoPointers = new List<Int64>();
        //public List<Int64> InfoPointers {
        //    get {
        //        if (_infoPointers.Count == 0) {
        //            Int64 pointersAddress = PropertyInvoker.Get<Int64>(CompetitionOffsets.SmallNumbersArray, OriginalBytes, MemoryAddress, DatabaseMode);
        //            int infosCount = ProcessManager.ReadArrayLength(pointersAddress);
        //            if (infosCount > 0) {
        //                Int64 startAddress = PropertyInvoker.Get<Int64>(0, OriginalBytes, pointersAddress, DatabaseMode);
        //                for (int i = 0; i < infosCount; i++) {
        //                    Int64 pointer = PropertyInvoker.Get<Int64>((i * 0x8), OriginalBytes, startAddress, DatabaseMode);
        //                    // First item in the array is a pointer to an array of three quads of numbers?
        //                    // Second item is another pointer to an array of two quads of numbers
        //                    // etc, etc
        //                }
        //            }
        //        }

        //        return _infoPointers;
        //    }
        //}
    }
}
