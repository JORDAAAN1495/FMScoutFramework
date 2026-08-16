using FM.Pitchside.Core.Defines.Offsets.Sub_Entities.Competitions;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces.Competitions;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Entities.Ingame.Competitions
{
    public class LeaguePrizeMoney : BaseObject, ILeaguePrizeMoney
    {
        public LeaguePrizeMoneyOffsets Offsets;
        public LeaguePrizeMoney(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.Offsets = new LeaguePrizeMoneyOffsets(Version);
        }
        public LeaguePrizeMoney(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.Offsets = new LeaguePrizeMoneyOffsets(Version);
        }

        public void Save()
        {
            PropertyInvoker.Set<Int32>(Offsets.Amount, OriginalBytes, MemoryAddress, DatabaseMode, Amount.GetValueOrDefault(0));
            isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                if (value)
                {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        private Int32? _amount;
        public Int32? Amount
        {
            get
            {
                if (_amount == null)
                {
                    _amount = PropertyInvoker.Get<Int32>(Offsets.Amount, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _amount;
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    isDirty = true;
                }
            }
        }
    }
}