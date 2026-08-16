using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class ClubInfoOne : BaseObject, IClubInfoOne
    {
        private ClubInfoOneOffsets ClubInfoOneOffsets;
        public ClubInfoOne(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.ClubInfoOneOffsets = new ClubInfoOneOffsets(version);
        }
        public ClubInfoOne(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.ClubInfoOneOffsets = new ClubInfoOneOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<int>(ClubInfoOneOffsets.AverageAttendance, OriginalBytes, MemoryAddress, DatabaseMode, AverageAttendance);
            PropertyInvoker.Set<int>(ClubInfoOneOffsets.MinimumAttendance, OriginalBytes, MemoryAddress, DatabaseMode, MinimumAttendance);
            PropertyInvoker.Set<int>(ClubInfoOneOffsets.MaximumAttendance, OriginalBytes, MemoryAddress, DatabaseMode, MaximumAttendance);
            _isDirty = false;
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

        private int _averageAttendance = 0;
        public int AverageAttendance
        {
            get
            {
                if (_averageAttendance == 0)
                {
                    _averageAttendance = PropertyInvoker.Get<int>(ClubInfoOneOffsets.AverageAttendance, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _averageAttendance;
            }
            set
            {
                if (_averageAttendance != value)
                {
                    _averageAttendance = value;
                    isDirty = true;
                }
            }
        }

        private int _minimumAttendance = 0;
        public int MinimumAttendance
        {
            get
            {
                if (_minimumAttendance == 0)
                {
                    _minimumAttendance = PropertyInvoker.Get<int>(ClubInfoOneOffsets.MinimumAttendance, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _minimumAttendance;
            }
            set
            {
                if (_minimumAttendance != value)
                {
                    _minimumAttendance = value;
                    isDirty = true;
                }
            }
        }

        private int _maximumAttendance = 0;
        public int MaximumAttendance
        {
            get
            {
                if (_maximumAttendance == 0)
                {
                    _maximumAttendance = PropertyInvoker.Get<int>(ClubInfoOneOffsets.MaximumAttendance, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _maximumAttendance;
            }
            set
            {
                if (_maximumAttendance != value)
                {
                    _maximumAttendance = value;
                    isDirty = true;
                }
            }
        }

        public Color ForegroundColour
        {
            get
            {
                return PropertyInvoker.Get<Color>(ClubInfoOneOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Color BackgroundColour
        {
            get
            {
                return PropertyInvoker.Get<Color>(ClubInfoOneOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private List<Kit> _kits = new List<Kit>();
        public List<Kit> Kits
        {
            get
            {
                if (_kits.Count == 0)
                {
                    int numberOfKits = ProcessManager.ReadArrayLength(MemoryAddress + ClubInfoOneOffsets.Kits);
                    if (numberOfKits > 0)
                    {
                        Int64 kitsArrayAddress = PropertyInvoker.Get<Int64>(ClubInfoOneOffsets.Kits, OriginalBytes, MemoryAddress, DatabaseMode);
                        for (int i = 0; i < numberOfKits; i++)
                        {
                            _kits.Add(PropertyInvoker.GetPointer<Kit>(0x0, OriginalBytes, (kitsArrayAddress + (i * 0x8)), DatabaseMode, Version));
                        }
                    }
                }

                return _kits;
            }
        }

        public Kit TextKit
        {
            get
            {
                Kit result = null;
                foreach (Kit kit in Kits)
                {
                    if ((KitType)kit.Type == KitType.KTHome && (KitRecordType)kit.RecordType == KitRecordType.KRTText)
                    {
                        result = kit;
                        break;
                    }
                }

                return result;
            }
        }

        public Kit MainKit
        {
            get
            {
                Kit result = null;
                foreach (Kit kit in Kits)
                {
                    if ((KitType)kit.Type == KitType.KTHome && (KitRecordType)kit.RecordType == KitRecordType.KRTShirt && kit.OutfieldPlayer == 1)
                    {
                        result = kit;
                        break;
                    }
                }

                return result;
            }
        }
    }
}