using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets
{
    public sealed class PersonOffsets
    {
        public IVersion Version;

        public PersonOffsets(IVersion version)
        {
            this.Version = version;
        }

        public short RowID
        {
            get
            {
                return 0x8;
            }
        }

        public short UID
        {
            get
            {
                return 0xC;
            }
        }
    }
}