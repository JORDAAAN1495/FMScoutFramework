using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets
{
    public sealed class CityOffsets
    {
        public IVersion Version;

        public CityOffsets (IVersion version)
        {
            this.Version = version;
        }

        public const short RowID = 0x4;
        public const short ID = 0x8;
        public const short Name = 0x18;
        public const short NationPtr = 0x20;
        public const short LanguagePtr = 0x28;
        public const short Latitude = 0x48;
        public const short Longitude = 0x4C;
        public const short LocalRegionPtr = 0x2C;
        public const short Altitude = 0x50;
        public const short InhabitantsRange = 0x52;
        public const short Attraction = 0x54;
    }
}