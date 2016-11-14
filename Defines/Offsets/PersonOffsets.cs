using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class PersonOffsets
    {
        public IVersion Version;

        public PersonOffsets (IVersion version)
        {
            this.Version = version;
        }

        public short DateOfBirth {
            get { return 0x178; }
        }

        public short Fullname {
            get { return 0x17C; }
        }

        public short Firstname {
            get { return 0x184; }
        }

        public short Lastname {
            get { return 0x188; }
        }

        public short Nickname {
            get { return 0x180; }
        }

        public short CityOfBirth {
            get { return 0x0; }
        }

        public short Nationality {
            get { return 0x194; }
        }

        public short Attributes {
            get { return 0x198; }
        }

        public short Contract {
            get { return 0x1C4; }
        }

        public short Club {
            get { return 0x22C; }
        }
    }
}