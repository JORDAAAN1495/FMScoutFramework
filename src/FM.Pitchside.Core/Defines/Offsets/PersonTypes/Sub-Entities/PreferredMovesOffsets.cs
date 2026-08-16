using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class PreferredMovesOffsets {
        public IVersion Version;

        public PreferredMovesOffsets(IVersion version) {
            this.Version = version;
        }

        public short FlagsOne {
            get {
                return 0x0;
            }
        }

        public short FlagsTwo {
            get {
                return 0x4;
            }
        }
    }
}
