using FMScoutFramework.Core.Entities.GameVersions;
using System;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ClubInfoOneOffsets {
        public IVersion Version;

        public ClubInfoOneOffsets(IVersion version) {
            Version = version;
        }

        public const short AverageAttendance = 0x70;
        public const short MinimumAttendance = 0x74;
        public const short MaximumAttendance = 0x78;
        public const short TacticalAttributes = 0x7C;
        public const short Kits = 0x90;
    }
}
