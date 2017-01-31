using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ActualPersonOffsets {
        public IVersion Version;

        public ActualPersonOffsets(IVersion version) {
            this.Version = version;
        }

        public short DateOfBirth {
            get {
                return 0x1C;
            }
        }

        public short FullName {
            get {
                return 0x20;
            }
        }

        public short FirstName {
            get {
                return 0x30;
            }
        }

        public short LastName {
            get {
                return 0x38;
            }
        }

        public short CommonName {
            get {
                return 0x40;
            }
        }

        public short CityOfBirth {
            get {
                return 0x48;
            }
        }

        public short Nation {
            get {
                return 0x50;
            }
        }

        public short Attributes {
            get {
                return 0x58;
            }
        }

        public short Ethnicity {
            get {
                return 0x68;
            }
        }

        public short HairColour {
            get {
                return 0x69;
            }
        }

        public short SkinTone {
            get {
                return 0x6A;
            }
        }

        public short Contract {
            get {
                return 0xA0;
            }
        }

        public short PreferredMoves {
            get {
                return 0xE0;
            }
        }

        public short InternationalApps {
            get {
                return 0x140;
            }
        }

        public short U21InternationalApps {
            get {
                return 0x141;
            }
        }

        public short InternationalGoals {
            get {
                return 0x142;
            }
        }

        public short U21InternationalGoals {
            get {
                return 0x143;
            }
        }
    }
}
