using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;

namespace FM.Pitchside.Core.Entities.Ingame.Sub_Entities
{
    public class NationTaxRule : BaseObject, INationTaxRule
    {
        public NationTaxRule(int memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public NationTaxRule(int memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}