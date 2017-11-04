using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
    public sealed class LeagueStageSettingsOffsets {
        public IVersion Version;

        public LeagueStageSettingsOffsets(IVersion version) {
            this.Version = version;
        }

        public short StageName {
            get {
                return 0x10;
            }
        }

        public short RanksTeams { // logical & with 0x2 == 0 ? yes : no
            get {
                return 0x4D;
            }
        }

        public short MatchRules {
            get {
                return 0x50;
            }
        }

        public short NumberOfTeams {
            get {
                return 0x53;
            }
        }

        public short IsStageHidden {
            get {
                return 0x6B;
            }
        }

        public short UsesHomeStadiums { // Pointer to pointer to +0x18
            get {
                return 0x108;
            }
        }

        public short PrizeMoney {
            get {
                return 0x148;
            }
        }

        public short PromotedTeams {
            get {
                return 0x228;
            }
        }

        public short DrawDate {
            get {
                return 0x260;
            }
        }
    }
}