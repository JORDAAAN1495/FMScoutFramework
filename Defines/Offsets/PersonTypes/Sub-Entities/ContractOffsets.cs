using FMScoutFramework.Core.Entities.GameVersions;
using System;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class ContractOffsets
    {
        public IVersion Version;

        public ContractOffsets (IVersion Version)
        {
            this.Version = Version;
        }

        public const short Person = 0x8;
        public const short Team = 0x10;
        public const short Wage = 0x18;
        public const short JobType = 0x1C;
        public const short Unhappinesses = 0x20;
        public const short StartDate = 0x34;
        public const short EndDate = 0x38;
        public const short JoinDate = 0x3C;
        public const short SquadStatus = 0x48;
        public const short TransferStatus = 0x4A;
        public const short SquadNumber = 0x4D;
        public const short Clauses = 0x58;
        public const short Bonuses = 0x58;
        public const short LoyaltyBonus = 0xA0;
        public const short Type = 0x9A;
    }
}
