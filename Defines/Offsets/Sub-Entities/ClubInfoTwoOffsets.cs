using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
    public sealed class ClubInfoTwoOffsets {
        private IVersion Version;
        public ClubInfoTwoOffsets(IVersion version) {
            Version = version;
        }

        public const short SixLetterName = 0x0;
        public const short ClubDebts = 0x40;
        public const short YearFounded = 0x98;
        public const short YouthImportance = 0xB8;
        public const short TrainingFacilities = 0xEA;
        public const short ChairmanStatus = 0xA0;
        public const short YouthFacilities = 0xF6;
        public const short JuniorCoaching = 0xF7;
        public const short YouthRecruitment = 0xF8;
        public const short Morale = 0xEB;
    }
}
