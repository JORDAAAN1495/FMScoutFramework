using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class StaffOffsets
    {

        public IVersion Version;

        public StaffOffsets (IVersion version)
        {
            this.Version = version;
        }

        public short StaffAttributes {
            get { return 0x8; }
        }

        public short HomeReputation {
            get { return 0xBC; }
        }

        public short CurrentReputation {
            get { return 0xBE; }
        }

        public short WorldReputation {
            get { return 0xC0; }
        }

        public short CurrentAbility {
            get { return 0xC2; }
        }

        public short PotentialAbility {
            get { return 0xC4; }
        }

        public short ActualPerson {
            get { return 0x100; }
        }
    }
}
