using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Defines.Offsets;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
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
        public City (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        { }
        public City (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        { }

        public int UID {
            get {
                return PropertyInvoker.Get<Int32> (CityOffsets.ID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int RowID {
            get {
                return PropertyInvoker.Get<Int32> (CityOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name {
            get {
                return PropertyInvoker.GetString (CityOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Nation Nation {
            get {
                return PropertyInvoker.GetPointer<Nation> (CityOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public short Attraction {
            get {
                return PropertyInvoker.Get<short> (CityOffsets.Attraction, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public float Latitude {
            get {
                return PropertyInvoker.Get<float> (CityOffsets.Latitude, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public float Longitude {
            get {
                return PropertyInvoker.Get<float> (CityOffsets.Longitude, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public short Altitude {
            get {
                return PropertyInvoker.Get<short> (CityOffsets.Altitude, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }
    }
}
