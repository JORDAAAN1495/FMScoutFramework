using System;
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
                return 0x1F8;
            }
        }

        public const short Injuries = 0xE0;
        public const short BansPtr = 0x18;
        public const short Team = 0x120;
        public const short Value = 0x138;
        public const short AskingPrice = 0x140;
        public const short Fitness = 0x15C;
        public const short Jadedness = 0x15E;
        public const short Condition = 0x160;
        public const short HomeReputation = 0x162;
        public const short CurrentReputation = 0x164;
        public const short WorldReputation = 0x166;
        public const short CA = 0x168;
        public const short PA = 0x16A;
        public const short Weight = 0x16C;
        public const short Height = 0x16E;
        public const short PlayerAttributes = 0x174;
    }
}
