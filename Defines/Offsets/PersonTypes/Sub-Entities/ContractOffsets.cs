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
        public const short StartDate = 0x3C;
        public const short EndDate = 0x40;
        public const short JoinDate = 0x44;
        public const short SquadStatus = 0x4C;
        public const short TransferStatus = 0x4E;
        public const short SquadNumber = 0x53;
        public const short Clauses = 0x58;
        public const short Bonuses = 0x58;
        public const short LoyaltyBonus = 0x90;
        public const short Type = 0x9A;
    }
}
