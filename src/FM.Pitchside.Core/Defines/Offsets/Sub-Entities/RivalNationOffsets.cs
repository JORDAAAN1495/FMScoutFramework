using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities
{
    public sealed class RivalNationOffsets
    {
        public IVersion Version;
        public RivalNationOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short NationAddress = 0x0;
        public const short Level = 0x8;
    }
}