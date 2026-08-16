using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.PersonTypes
{
    public sealed class HumanManagerOffsets
    {
        public IVersion Version;

        public HumanManagerOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short Unsackable = 0x414;
        public const short SquadRegistrationOptions = 0x415;
        public const short Characteristic = 0x411;

        public short ActualPerson
        {
            get
            {
                return 0x480;
            }
        }
    }
}