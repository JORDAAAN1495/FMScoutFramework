using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ClubInfoTwoOffsets {
        private IVersion Version;
        public ClubInfoTwoOffsets(IVersion version) {
            Version = version;
        }

        public const short SixLetterName = 0x0;
        public const short ClubDebts = 0x48;
        public const short YearFounded = 0xA8;
        public const short YouthImportance = 0xC6;
        public const short TrainingFacilities = 0xC7;
        public const short ChairmanStatus = 0xA0;
        public const short YouthFacilities = 0xD2;
        public const short JuniorCoaching = 0xD1;
        public const short YouthRecruitment = 0xE0;
        public const short Morale = 0xB0;
    }
}
