using FM.Pitchside.Core.Defines.Offsets.Sub_Entities;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Entities.Ingame.Sub_Entities
{
    public class RivalNation : BaseObject, IRivalNation
    {
        public RivalNationOffsets RivalNationOffsets;
        public RivalNation(int memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.RivalNationOffsets = new RivalNationOffsets(version);
        }
        public RivalNation(int memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.RivalNationOffsets = new RivalNationOffsets(version);
        }

        private int RivalNationAddress
        {
            get
            {
                return PropertyInvoker.Get<Int32>(RivalNationOffsets.NationAddress, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Nation Nation
        {
            get
            {
                return PropertyInvoker.GetPointer<Nation>(RivalNationOffsets.NationAddress, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        public Byte Level
        {
            get
            {
                return PropertyInvoker.Get<Byte>(RivalNationOffsets.Level, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public override string ToString()
        {
            return this.Nation.Name;
        }
    }
}