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

        public short ParentCompetition {
            get {
                return 0x80;
            }
        }

        public short NorthCity {
            get {
                return 0x98;
            }
        }

        public short SouthCity {
            get {
                return 0xA0;
            }
        }

        public short WestCity {
            get {
                return 0xA8;
            }
        }

        public short EastCity {
            get {
                return 0xB0;
            }
        }

        // Past Year Winner / Runner Up / Third Placed
        public short PastWinners { // Alternative Names?
            get {
                return 0xB8;
            }
        }

        public short LastHistory {
            get {
                return 0xC0;
            }
        }

        public short ActualCompetition {
            get {
                return 0xC8;
            }
        }

        public short Champions {
            get {
                return 0xD0;
            }
        }

        public short ForegroundColour {
            get {
                return 0xFC;
            }
        }

        public short BackgroundColour {
            get {
                return 0x100;
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

        public short Reputation {
            get {
                return 0x13E;
            }
        }

        public short OriginalReputation {
            get {
                return 0x140;
            }
        }

        public short LastReputationPos {
            get {
                return 0x142;
            }
        }

        public short CurrentReputation {
            get {
                return 0x144;
            }
        }

        public short PercentageOfTopDivisionReputation {
            get {
                return 0x146;
            }
        }

        public short NameType {
            get {
                return 0x15A;
            }
        }

        public short DivisionLevel {
            get {
                return 0x15C;
            }
        }

        public short Type {
            get {
                return 0x15D;
            }
        }
        public short UsesSeatedOnlyStadiums {
            get {
                return 0x15F;
            }
        }

        public short WageBudgetTurnoverPercentage {
            get {
                return 0x160;
            }
        }
    }
}
