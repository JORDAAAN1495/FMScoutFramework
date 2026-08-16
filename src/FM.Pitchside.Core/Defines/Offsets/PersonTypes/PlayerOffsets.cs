using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.PersonTypes
{
    public sealed class PlayerOffsets
    {

        public IVersion Version;

        public PlayerOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short ActualPerson
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x1F0;
                }

                return 0x298;
            }
        }

        public const short Weight = 0x14C;
        public const short Height = 0x14E;
        public const short Injuries = 0xF8;
        public const short BansPtr = 0x18;
        public const short Team = 0x130;
        public const short Value = 0x1d0;
        public const short AskingPrice = 0x1d4;

        public short Fitness
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x148;
                }

                return 0x1F0;
            }
        }

        public short Jadedness
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x14A;
                }

                return 0x1F2;
            }
        }

        public short Condition
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x14C;
                }

                return 0x1F4;
            }
        }

        public short HomeReputation
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x14E;
                }

                return 0x1F6;
            }
        }

        public short CurrentReputation
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x150;
                }

                return 0x1F8;
            }
        }

        public short WorldReputation
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x152;
                }

                return 0x1FA;
            }
        }

        public short CA
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x154;
                }

                return 0x1FC;
            }
        }

        public short PA
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x156;
                }

                return 0x1FE;
            }
        }


        public short PlayerAttributes
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x15C;
                }

                return 0x204;
            }
        }

        public short DeclaredForNation
        {
            get
            {
                if (Version.isTouch)
                {
                    return 0x15C;
                }

                return 0x257;
            }
        }
    }
}