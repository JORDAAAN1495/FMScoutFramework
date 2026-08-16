using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.PersonTypes.Sub_Entities
{

    public sealed class JobAttributesOffsets
    {

        public IVersion Version;

        public JobAttributesOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short Manager
        {
            get
            {
                return 0x0;
            }
        }

        public short AssistantManager
        {
            get
            {
                return 0x1;
            }
        }

        public short Coach
        {
            get
            {
                return 0x2;
            }
        }

        public short Physio
        {
            get
            {
                return 0x3;
            }
        }

        public short Scout
        {
            get
            {
                return 0x4;
            }
        }

        public short GoalkeeperCoach
        {
            get
            {
                return 0x5;
            }
        }

        public short FitnessCoach
        {
            get
            {
                return 0x6;
            }
        }

        public short Chairman
        {
            get
            {
                return 0x7;
            }
        }

        public short DirectorOfFootball
        {
            get
            {
                return 0x8;
            }
        }

        public short HeadOfYouthDevelopment
        {
            get
            {
                return 0x9;
            }
        }
    }
}