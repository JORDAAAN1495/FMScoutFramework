using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces.PersonTypes;

namespace FM.Pitchside.Core.Entities.Ingame.Persons
{
    public class PlayerStaff : BaseObject, IPlayerStaff
    {
        public PlayerStaff(int memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public PlayerStaff(int memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}