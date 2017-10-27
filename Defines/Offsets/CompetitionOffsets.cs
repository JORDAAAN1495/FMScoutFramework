using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
    public sealed class CompetitionOffsets {
        public IVersion Version;

        public CompetitionOffsets(IVersion version) {
            this.Version = version;
        }

        public const short RowID = 0x8;
        public const short UID = 0xC;

        public short Name {
            get {
                return 0x58;
            }
        }

        public short ShortName {
            get {
                return 0x60;
            }
        }

        public short ThreeLetterName {
            get {
                return 0x68;
            }
        }

        public short Continent {
            get {
                return 0x70;
            }
        }

        public short Nation {
            get {
                return 0x78;
            }
        }

        // Past Year Winner / Runner Up / Third Placed
        public short PastWinners {
            get {
                return 0xB8;
            }
        }

        public short MoreInfoContainer {
            get {
                return 0xC0;
            }
        }

        public short SmallNumbersArray {
            get {
                return 0xD0;
            }
        }

        public short Reputation {
            get {
                return 0x13E;
            }
        }

        public short NationalReputation {
            get {
                return 0x140;
            }
        }

        public short MinimumPitchLength {
            get {
                return 0x134;
            }
        }

        public short MinimumPitchWidth {
            get {
                return 0x136;
            }
        }

        public short MaximumPitchLength {
            get {
                return 0x138;
            }
        }

        public short MaximumPitchWidth {
            get {
                return 0x13A;
            }
        }

        public short CompInfos {
            get {
                return 0x13C;
            }
        }
    }
}
