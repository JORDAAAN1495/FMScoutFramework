using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ClubDebtsOffsets {
        public IVersion Version;

        public ClubDebtsOffsets(IVersion version) {
            this.Version = version;
        }

        public const short TotalAmount = 0x0;
        public const short MonthlyPayable = 0x4;
        public const short MonthlyInterest = 0x8;
        public const short EndDate = 0x10;
        public const short StartDate = 0x14;
        public const short Source = 0x18;
    }
}
