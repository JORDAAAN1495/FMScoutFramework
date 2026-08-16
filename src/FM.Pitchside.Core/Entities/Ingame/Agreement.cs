using FM.Pitchside.Core.Defines.Offsets;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Entities.Ingame
{
    public class Agreement : BaseObject, IAgreement
    {
        public Agreement(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public Agreement(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public new Int64 MemoryAddress
        {
            get
            {
                return base.MemoryAddress;
            }
        }

        public Int32 RowID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(AgreementOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Int32 UID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(AgreementOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name
        {
            get
            {
                return PropertyInvoker.GetString(AgreementOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }
    }
}