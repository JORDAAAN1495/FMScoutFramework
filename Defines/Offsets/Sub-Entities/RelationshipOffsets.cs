using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class RelationshipOffsets {
        public IVersion Version;

        public RelationshipOffsets(IVersion version) {
            this.Version = version;
        }

        public short AssociatedAddress {
            get {
                return 0x0;
            }
        }

        public short Info {
            get {
                return 0x8;
            }
        }

        public short RecordType {
            get {
                return 0xA;
            }
        }

        public short Type {
            get {
                return 0xB;
            }
        }

        public short Level {
            get {
                return 0xC;
            }
        }

        public short Permanent {
            get {
                return 0xD;
            }
        }
    }
}
