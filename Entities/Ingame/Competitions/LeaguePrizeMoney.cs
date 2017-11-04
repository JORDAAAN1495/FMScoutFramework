using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Defines.Offsets;
using FMScoutFramework.Core.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class LeaguePrizeMoney : BaseObject, ILeaguePrizeMoney
    {
        public LeaguePrizeMoneyOffsets Offsets;
        public LeaguePrizeMoney(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version){
            this.Offsets = new LeaguePrizeMoneyOffsets(Version);
        }
        public LeaguePrizeMoney(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version){
            this.Offsets = new LeaguePrizeMoneyOffsets(Version);
        }

        public void Save() {
            PropertyInvoker.Set<Int32>(Offsets.Amount, OriginalBytes, MemoryAddress, DatabaseMode, Amount.GetValueOrDefault(0));
            isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty {
            get {
                return _isDirty;
            }
            set {
                if (value) {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        private Int32? _amount;
        public Int32? Amount {
            get {
                if (_amount == null) {
                    _amount = PropertyInvoker.Get<Int32>(Offsets.Amount, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _amount;
            }
            set {
                if (_amount != value) {
                    _amount = value;
                    isDirty = true;
                }
            }
        }
    }
}
