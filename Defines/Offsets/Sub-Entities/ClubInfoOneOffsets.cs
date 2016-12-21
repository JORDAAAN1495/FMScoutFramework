using FMScoutFramework.Core.Entities.GameVersions;
using System;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ClubInfoOneOffsets {
        public IVersion Version;

        public ClubInfoOneOffsets(IVersion version) {
            Version = version;
        }

        public short AverageAttendance {
            get {
                if (Version.GetType() == typeof(Steam_17_2_0_Windows)) {
                    return 0x68;
                }
                return 0x70;
            }
        }

        public short MinimumAttendance {
            get {
                if (Version.GetType() == typeof(Steam_17_2_0_Windows)) {
                    return 0x6C;
                }
                return 0x74;
            }
        }

        public short MaximumAttendance {
            get {
                if (Version.GetType() == typeof(Steam_17_2_0_Windows)) {
                    return 0x70;
                }
                return 0x78;
            }
        }

        public short TacticalAttributes {
            get {
                if (Version.GetType() == typeof(Steam_17_2_0_Windows)) {
                    return 0x74;
                }
                return 0x7C;
            }
        }

        public short Kits {
            get {
                if (Version.GetType() == typeof(Steam_17_2_0_Windows)) {
                    return 0x88;
                }
                return 0x90;
            }
        }
    }
}
