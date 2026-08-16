using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame {
    public enum DebtType {
        [Description("None")]
        DTNone                                  = 0,
        [Description("Bank")]
        DTBank                                  = 1,
        [Description("Chairman")]
        DTChairman                              = 2,
        [Description("CVA")]
        DTCVA                                   = 3,
        [Description("PFA")]
        DTPFA                                   = 4,
        [Description("State")]
        DTState                                 = 5,
        [Description("Supporters Trust")]
        DTSupportersTrust                       = 6,
        [Description("Gift Repayable When Profitable")]
        DTGiftRepayableWhenProfitable           = 7,
        [Description("Gift Repayable on Departure")]
        DTGiftRepayableOnDeparture              = 8,
        [Description("Gift Non Repayable")]
        DTGiftNonRepayable                      = 9,
        [Description("Loan Repayable on Promotion to Top Division")]
        DTLoanRepayableOnPromotionToTopDivision = 10,
        [Description("Loan Repayable when Established in Top Division")]
        DTLoanRepayableWhenEstablishedInTopDiv  = 11,
        [Description("Director")]
        DTDirector                              = 12,
        [Description("PIK Loan (Total Amount of Loan Repayable on Maturity)")]
        DTPIKLoan                               = 13,
        [Description("Lump Sum Paid on Promotion to Top Division")]
        DTLumpSumPaidOnPromotionToTopDiv        = 14,
        [Description("Miscellaneous")]
        DTMiscellaneous                         = 15
    }

    public class ClubDebt : BaseObject, IClubDebt {

        public ClubDebt(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public ClubDebt(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public void Save() {
            PropertyInvoker.Set<int>(ClubDebtsOffsets.TotalAmount, OriginalBytes, MemoryAddress, DatabaseMode, TotalAmount);
            PropertyInvoker.Set<byte>(ClubDebtsOffsets.Source, OriginalBytes, MemoryAddress, DatabaseMode, Source);
            PropertyInvoker.Set<DateTime>(ClubDebtsOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode, StartDate);
            PropertyInvoker.Set<DateTime>(ClubDebtsOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode, EndDate);
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

        private int _totalAmount = 0;
        public int TotalAmount {
            get {
                if (_totalAmount == 0) {
                    _totalAmount = PropertyInvoker.Get<int>(ClubDebtsOffsets.TotalAmount, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _totalAmount;
            }
            set {
                if (_totalAmount != value) {
                    _totalAmount = value;
                    isDirty = true;
                }
            }
        }

        private byte _source = 0;
        public byte Source {
            get {
                if (_source == 0) {
                    _source = PropertyInvoker.Get<byte>(ClubDebtsOffsets.Source, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _source;
            }
            set {
                if (_source != value) {
                    _source = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate {
            get {
                if (_startDate.Year < 1900) {
                    _startDate = PropertyInvoker.Get<DateTime>(ClubDebtsOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _startDate;
            }
            set {
                if (_startDate != value) {
                    _startDate = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate {
            get {
                if (_endDate.Year < 1900) {
                    _endDate = PropertyInvoker.Get<DateTime>(ClubDebtsOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _endDate;
            }
            set {
                if (_endDate != value) {
                    _endDate = value;
                    isDirty = true;
                }
            }
        }


    }
}
