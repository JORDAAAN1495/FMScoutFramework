using System;
using System.Globalization;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{

    public enum BonusType {
        [Description("Appearance Fee")]
        BTAppearanceFee = 0,
        [Description("Goal Fee")]
        BTGoalFee = 1,
        [Description("Clean Sheet Fee")]
        BTCleanSheetFee = 2,
        [Description("Team of the Year")]
        BTTeamOfTheYear = 3,
        [Description("Top Goalscorer")]
        BTTopGoalscorer = 4,
        [Description("Promotion Fee")]
        BTPromotionFee = 5,
        [Description("Avoid Relegation Fee")]
        BTAvoidRelegationFee = 6,
        [Description("International Cap Fee")]
        BTInternationalCapFee = 7,
        [Description("Unused Substitute Fee")]
        BTUnusedSubstituteFee = 8
    }

    public class ContractBonus : BaseObject, IContractBonus
    {
        public ContractBonus (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        { }
        public ContractBonus (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        { }

        public void Save() {
            PropertyInvoker.Set<byte>(ContractBonusOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<int>(ContractBonusOffsets.Value, OriginalBytes, MemoryAddress, DatabaseMode, Value);
            PropertyInvoker.Set<byte>(ContractBonusOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode, Info);
            _isDirty = false;
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

        private byte _type = 0;
        public byte Type {
            get {
                if (_type == 0) {
                    _type = PropertyInvoker.Get<byte>(ContractBonusOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _type;
            }
            set {
                if (_type != value) {
                    isDirty = true;
                    _type = value;
                }
            }
        }

        private int _value = 0;
        public int Value {
            get {
                if (_value == 0) {
                    _value = PropertyInvoker.Get<Int32>(ContractBonusOffsets.Value, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _value;
            }
            set {
                if (_value != value) {
                    isDirty = true;
                    _value = value;
                }
            }
        }

        private byte _info = 0;
        public byte Info {
            get {
                if (_info == 0) {
                    _info = PropertyInvoker.Get<byte>(ContractBonusOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _info;
            }
            set {
                if (_info != value) {
                    isDirty = true;
                    _info = value;
                }
            }
        }
    }
}
