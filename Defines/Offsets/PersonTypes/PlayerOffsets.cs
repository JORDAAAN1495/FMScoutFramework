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

        public short PlayerAttributes
        {
            get
            { return 0x90; }
        }

        public short Injuries
        {
            get { return 0xDC;}
        }

        public short BansOffset
        {
            get
            {
                return 0xC;
            }
        }

        public short Team
        {
            get { return 0xFC; }
        }

        public short Value
        {
            get { return 0x108; }
        }

        public short AskingPrice
        {
            get { return 0x10C; }
        }

        public short Fitness
        {
            get { return 0x130; }
        }

        public short Jadedness
        {
            get { return 0x132; }
        }

        public short Condition
        {
            get { return 0x134; }
        }

        public short HomeReputation
        {
            get { return 0x136; }
        }

        public short CurrentReputation
        {
            get { return 0x138; }
        }

        public short WorldReputation
        {
            get { return 0x13A; }
        }

        public short CA
        {
            get { return 0x13C; }
        }

        public short PA
        {
            get { return 0x13E; }
        }

        public short Weight
        {
            get { return 0x140; }
        }

        public short Height
        {
            get { return 0x142; }
        }

        public short InternationalApps
        {
            get { return 0x240; }
        }

        public short U21InternationalApps
        {
            get { return 0x241; }
        }

        public short InternationalGoals
        {
            get { return 0x242; }
        }

        public short U21InternationalGoals
        {
            get { return 0x243; }
        }

        public short RowID
        {
            get { return 0x154; }
        }

        public short UID
        {
            get { return 0x158;}
        }
    }
}
