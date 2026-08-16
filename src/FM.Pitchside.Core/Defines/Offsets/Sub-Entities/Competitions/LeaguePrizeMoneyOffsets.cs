using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
    public sealed class LeaguePrizeMoneyOffsets {
        public IVersion Version;

        public LeaguePrizeMoneyOffsets(IVersion version) {
            this.Version = version;
        }

        public short Amount {
            get {
                return 0x0;
            }
        }
    }
}