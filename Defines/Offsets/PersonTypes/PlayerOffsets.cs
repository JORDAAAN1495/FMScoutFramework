using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class PlayerOffsets
    {

        public IVersion Version;

        public PlayerOffsets(IVersion version) {
            this.Version = version;
        }

        public short ActualPerson {
            get {
                return 0x1E8;
            }
        }

        public const short Injuries = 0xD8;
        public const short BansPtr = 0x18;
        public const short Team = 0x110;
        public const short Value = 0x128;
        public const short AskingPrice = 0x130;
        public const short Fitness = 0x14C;
        public const short Jadedness = 0x14E;
        public const short Condition = 0x150;
        public const short HomeReputation = 0x152;
        public const short CurrentReputation = 0x154;
        public const short WorldReputation = 0x156;
        public const short CA = 0x158;
        public const short PA = 0x15A;
        public const short Weight = 0x15C;
        public const short Height = 0x15E;
        public const short PlayerAttributes = 0x164;
        public const short DeclaredForNation = 0x1A9;
    }
}
