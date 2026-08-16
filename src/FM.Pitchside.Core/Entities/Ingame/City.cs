using FM.Pitchside.Core.Defines.Offsets;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;
using System.ComponentModel;

namespace FM.Pitchside.Core.Entities.Ingame
{
    public enum InhabitantsRange
    {
        [Description("Not Set")]
        CIRNotSet = 0,
        [Description("0 - 1000")]
        CIR_0_1000 = 1,
        [Description("1001 - 2500")]
        CIR_1001_2500 = 2,
        [Description("2501 - 5000")]
        CIR_2501_5000 = 3,
        [Description("5001 - 10000")]
        CIR_5001_10000 = 4,
        [Description("10001 - 25000")]
        CIR_10001_25000 = 5,
        [Description("25001 - 50000")]
        CIR_25001_50000 = 6,
        [Description("50001 - 100000")]
        CIR_50001_100000 = 7,
        [Description("100001 - 250000")]
        CIR_100001_250000 = 8,
        [Description("250001 - 500000")]
        CIR_250001_500000 = 9,
        [Description("500001 - 1000000")]
        CIR_500001_1000000 = 10,
        [Description("1000001 - 2500000")]
        CIR_1000001_2500000 = 11,
        [Description("2500001 - 5000000")]
        CIR_2500001_5000000 = 12,
        [Description("5000001 - 10000000")]
        CIR_5000000_10000000 = 13,
        [Description("10000001 - 20000000")]
        CIR_10000001_20000000 = 14,
        [Description("20000001+")]
        CIR_20000001_OR_OVER = 15
    }

    public class City : BaseObject, ICity
    {
        public City(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public City(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }

        public void Save()
        {
            PropertyInvoker.Set<short>(CityOffsets.Attraction, OriginalBytes, MemoryAddress, DatabaseMode, Attraction);
            PropertyInvoker.Set<float>(CityOffsets.Latitude, OriginalBytes, MemoryAddress, DatabaseMode, Latitude);
            PropertyInvoker.Set<float>(CityOffsets.Longitude, OriginalBytes, MemoryAddress, DatabaseMode, Longitude);
            PropertyInvoker.Set<short>(CityOffsets.Altitude, OriginalBytes, MemoryAddress, DatabaseMode, Altitude);
            PropertyInvoker.Set<short>(CityOffsets.InhabitantsRange, OriginalBytes, MemoryAddress, DatabaseMode, InhabitantsRange);
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

        public int UID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(CityOffsets.ID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int RowID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(CityOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Offset
        {
            get
            {
                return "0x" + MemoryAddress.ToString("X");
            }
        }

        public string Name
        {
            get
            {
                return PropertyInvoker.GetString(CityOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string CitizenName
        {
            get
            {
                return "-";
            }
        }

        public Int32 WeatherPtr
        {
            get
            {
                return 0;
            }
        }

        public Int32 NationPtr
        {
            get
            {
                return 0;
            }
        }

        public Int32 LanguagePtr
        {
            get
            {
                return 0;
            }
        }

        public Int32 LocalRegionPtr
        {
            get
            {
                return 0;
            }
        }

        private short _attraction = 0;
        public short Attraction
        {
            get
            {
                if (_attraction == 0)
                {
                    _attraction = PropertyInvoker.Get<short>(CityOffsets.Attraction, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _attraction;
            }
            set
            {
                _attraction = value;
                isDirty = true;
            }
        }

        private float _latitude = 0.0f;
        public float Latitude
        {
            get
            {
                if (_latitude == 0.0f)
                {
                    _latitude = PropertyInvoker.Get<float>(CityOffsets.Latitude, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _latitude;
            }
            set
            {
                _latitude = value;
                isDirty = true;
            }
        }

        private float _longitude = 0.0f;
        public float Longitude
        {
            get
            {
                if (_longitude == 0.0f)
                {
                    _longitude = PropertyInvoker.Get<float>(CityOffsets.Longitude, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _longitude;
            }
            set
            {
                _longitude = value;
                isDirty = true;
            }
        }

        private short _altitude = 0;
        public short Altitude
        {
            get
            {
                if (_altitude == 0)
                {
                    _altitude = PropertyInvoker.Get<short>(CityOffsets.Altitude, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _altitude;
            }
            set
            {
                _altitude = value;
                isDirty = true;
            }
        }

        private short _inhabitantsRange = 0;
        public short InhabitantsRange
        {
            get
            {
                if (_inhabitantsRange == 0)
                {
                    _inhabitantsRange = PropertyInvoker.Get<short>(CityOffsets.InhabitantsRange, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _inhabitantsRange;
            }
            set
            {
                _inhabitantsRange = value;
                isDirty = true;
            }
        }
    }
}