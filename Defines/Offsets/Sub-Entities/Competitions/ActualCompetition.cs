using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
    public sealed class ActualCompetition {
        public IVersion Version;

        public ActualCompetition(IVersion version) {
            this.Version = version;
        }

        public short Stages {
            get {
                return 0x118;
            }
        }
    }
}