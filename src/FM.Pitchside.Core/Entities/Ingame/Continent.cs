using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;

namespace FM.Pitchside.Core.Entities.Ingame
{
    public class Continent : BaseObject, IContinent
    {
        public Continent(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Continent(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}