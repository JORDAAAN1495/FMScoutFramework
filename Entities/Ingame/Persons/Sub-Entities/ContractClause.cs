using System;
using System.Globalization;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public enum ContractClauseType {
        [Description("Minimum Fee Release")]
        CCTMinFeeRelease                = 0,
        [Description("Relegation Release")]
        CCTRelegationRelease            = 1,
        [Description("Non Promotion Release")]
        CCTNonPromotionRelease          = 2,
        [Description("Yearly Wage Rise (%)")]
        CCTYearlyWageRisePercentage     = 3,
        [Description("Promotion Wage Rise")]
        CCTPromotionWageRise            = 4,
        [Description("Relegation Wage Drop")]
        CCTRelegationWageDrop           = 5,
        [Description("Non-Playing Job Offser Release")]
        CCTNonPlayingJobOfferRelease    = 6,
        [Description("Sell On Fee (%)")]
        CCTSellOnFeePercentage          = 7,

    }

    public class ContractClause : BaseObject, IContractClause {
        public ContractClause(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public ContractClause(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public void Save() {
            PropertyInvoker.Set<int>(ContractClausesOffsets.Value, OriginalBytes, MemoryAddress, DatabaseMode, Value);
            PropertyInvoker.Set<byte>(ContractClausesOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<byte>(ContractClausesOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode, Info);
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

        private int _value = 0;
        public int Value {
            get {
                if (_value == 0) {
                    _value = PropertyInvoker.Get<int>(ContractClausesOffsets.Value, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private byte _type = 0;
        public byte Type {
            get {
                if (_type == 0) {
                    _type = PropertyInvoker.Get<byte>(ContractClausesOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private byte _info = 0;
        public byte Info {
            get {
                if (_info == 0) {
                    _info = PropertyInvoker.Get<byte>(ContractClausesOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode);
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
