using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.PersonTypes.Sub_Entities
{
    public sealed class PreferredMovesOffsets
    {
        public IVersion Version;

        public PreferredMovesOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short FlagsOne
        {
            get
            {
                return 0x0;
            }
        }

        public short FlagsTwo
        {
            get
            {
                return 0x4;
            }
        }
    }
}