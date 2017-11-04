using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
    public sealed class ActualCompetitionOffsets {
        public IVersion Version;

        public ActualCompetitionOffsets(IVersion version) {
            this.Version = version;
        }

        public short StagesOne {
            get {
                return 0x100;
            }
        }

        public short StagesTwo {
            get {
                return 0x118;
            }
        }
    }
}