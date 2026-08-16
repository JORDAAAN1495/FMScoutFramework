using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets
{
    public sealed class AgreementOffsets
    {
        public IVersion Version;

        public AgreementOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short RowID = 0x4;
        public const short UID = 0xC;
        public const short Name = 0x20;
    }
}