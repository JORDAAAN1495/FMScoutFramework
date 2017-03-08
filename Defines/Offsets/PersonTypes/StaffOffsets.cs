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
            get { return 0xA4; }
        }

        public short CurrentReputation {
            get { return 0xA6; }
        }

        public short WorldReputation {
            get { return 0xA8; }
        }

        public short CurrentAbility {
            get { return 0xAA; }
        }

        public short PotentialAbility {
            get { return 0xAC; }
        }

        public short ActualPerson {
            get { return 0xE8; }
        }
    }
}
