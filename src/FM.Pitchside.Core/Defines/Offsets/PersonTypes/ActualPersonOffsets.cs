using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.PersonTypes
{
    public sealed class ActualPersonOffsets
    {
        public IVersion Version;

        public ActualPersonOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short DateOfBirth
        {
            get
            {
                return 0x1C;
            }
        }

        public short FullName
        {
            get
            {
                return 0x20;
            }
        }

        public short FirstName
        {
            get
            {
                return 0x30;
            }
        }

        public short LastName
        {
            get
            {
                return 0x38;
            }
        }

        public short CommonName
        {
            get
            {
                return 0x40;
            }
        }

        public short CityOfBirth
        {
            get
            {
                return 0x68;
            }
        }

        public short Nation
        {
            get
            {
                return 0x48;
            }
        }

        public short Attributes
        {
            get
            {
                return 0x50;
            }
        }

        public short Relationships
        {
            get
            {
                return 0x58;
            }
        }

        public short Ethnicity
        {
            get
            {
                return 0x68;
            }
        }

        public short HairColour
        {
            get
            {
                return 0x69;
            }
        }

        public short SkinTone
        {
            get
            {
                return 0x6B;
            }
        }

        public short Contract
        {
            get
            {
                return 0xA0;
            }
        }

        public short PreferredMoves
        {
            get
            {
                return 0xC8;
            }
        }

        public short JobAttributes
        {
            get
            {
                return 0xD8;
            }
        }

        public const short FreezeAttributes = 0x13A;

        public short InternationalApps
        {
            get
            {
                return 0x13C;
            }
        }

        public short U21InternationalApps
        {
            get
            {
                return 0x13D;
            }
        }

        public short InternationalGoals
        {
            get
            {
                return 0x13E;
            }
        }

        public short U21InternationalGoals
        {
            get
            {
                return 0x13F;
            }
        }
    }
}