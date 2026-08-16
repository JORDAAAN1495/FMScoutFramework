using FM.Pitchside.Core.Defines.Offsets;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Entities.Ingame
{
    public class Injury : BaseObject, IInjury
    {
        public Injury(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Injury(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }

        public Int32 RowID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(InjuryOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Int32 ID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(InjuryOffsets.ID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string SentenceName
        {
            get
            {
                return PropertyInvoker.GetString(InjuryOffsets.SentenceName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name
        {
            get
            {
                return PropertyInvoker.GetString(InjuryOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }
    }
}