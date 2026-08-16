using FM.Pitchside.Core.Defines.Versions;

namespace FM.Pitchside.Core.Defines.Offsets.Sub_Entities
{
    public sealed class NationAgreementOffsets
    {
        public IVersion Version;

        public NationAgreementOffsets(IVersion version)
        {
            this.Version = version;
        }

        public const short AgreementAddress = 0x0;
        public const short StartDate = 0x8;
        public const short EndDate = 0xC;
    }
}