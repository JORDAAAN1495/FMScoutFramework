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
        // Clauses
        [Description("Minimum Fee Release")]
        CCTMinFeeRelease                            = 0,
        [Description("Relegation Release")]
        CCTRelegationRelease                        = 1,
        [Description("Non Promotion Release")]
        CCTNonPromotionRelease                      = 2,
        [Description("Yearly Wage Rise (%)")]
        CCTYearlyWageRisePercentage                 = 3,
        [Description("Promotion Wage Rise")]
        CCTPromotionWageRise                        = 4,
        [Description("Relegation Wage Drop")]
        CCTRelegationWageDrop                       = 5,
        [Description("Non-Playing Job Offer Release")]
        CCTNonPlayingJobOfferRelease                = 6,
        [Description("Sell On Fee (%)")]
        CCTSellOnFeePercentageOld                   = 7,
        [Description("Sell On Fee (%)")]
        CCTSellOnFeePercentage                      = 8,
        [Description("Sell On Fee Profit (%)")]
        CCTSellOnFeeProfitPercentage                = 9,
        [Description("Seasonal Landmark Goal Bonus")]
        CBTSeasonalLandmarkGoalBonus                = 10,
        [Description("One-Year Extension After League Games (Final Season)")]
        CCTOneYearExtAfterLeagueGamesFinalSeason    = 11,
        [Description("Match Highest Earner")]
        CCTMatchHighestEarner                       = 12,
        [Description("Wage After Reaching Club League Games")]
        CCTWageAfterReachingClubLeagueGames         = 13,
        [Description("Top Division Promotion Wage Rise")]
        CCTTopDivisionPromotionRise                 = 14,
        [Description("Top Division Relegation Wage Drop")]
        CCTTopDivisionRelegationDrop                = 15,
        [Description("Minimum Fee Release Clause (Foreign Clubs)")]
        CCTMinimumFeeReleaseForeignClubs            = 16,
        [Description("Minimum Fee Release Clause (Domestic Clubs in Higher Division)")]
        CCTMinimumFeeReleaseDomesticInHigher        = 17,
        [Description("Minimum Fee Release Clause (Domestic Clubs)")]
        CCTMinimumFeeReleaseDomesticClubs           = 18,
        [Description("Wage After Reaching International Appearances")]
        CCTWageAfterReachingInternationalApps       = 19,
        [Description("Optional Contract Extension By Club")]
        CCTOptionalContractExtensionByClub          = 22,
        [Description("One-Year Extension After League Games (Promoted Final Season)")]
        CCTOneYearExtAfterLeagueGamesPromFinalSeas  = 25,
        [Description("One-Year Extension After League Games (Avoid Relegation Final Season)")]
        CCTOneYearExtAfterLeagueGamesAvoifRelFSeas  = 26,
        [Description("Minimum Fee Release Clause (Clubs in a Major Continental Competition)")]
        CCTMinimumFeeReleaseClubsInMajorContinental = 27,
        [Description("Contract Extension After Promotion")]
        CCTContractExtensionAfterPromotion          = 29,
        [Description("Injury Release Clause")]
        CCTInjuryReleaseClause                      = 30,
        [Description("Minimum Fee Release Clause (Clubs in a Continental Competition)")]
        CCTMinimumFeeReleaseClubsInContinental      = 31,
        [Description("Appearance Fee")]
        CBTAppearanceFee                = 32,
        [Description("Goal Bonus")]
        CBTGoalBonus                    = 33,
        [Description("Clean Sheet Bonus")]
        CBTCleanSheetBonus              = 34,
        [Description("Team of the Year Bonus (Division)")]
        CBTDivisionTeamOfTheYearBonus   = 35,
        [Description("Top Goalscorer Bonus (Division)")]
        CBTDivisionTopGoalscorerBonus   = 36,
        [Description("International Cap Bonus")]
        CBTInternationalCapBonus        = 37,
        [Description("Unused Substitute Fee")]
        CBTUnusedSubstituteFee          = 38,
        [Description("Will Leave At End Of Contract")]
        CCTWillLeaveAtEndOfContract             = 54,
        [Description("Active Relegation Release Clause")]
        CCTActiveRelegationRelease              = 55,
        [Description("Active Non Promotion Release Clause")]
        CCTActiveNonPromotionRelease            = 56,
        [Description("Committee Assigned Minimum Fee Release Clause")]
        CCTCommitteeAssignedMinimumFeeRelease   = 57
    }

    public class ContractClause : BaseObject, IContractClause {
        public ContractClause(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public ContractClause(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public void Save() {
            PropertyInvoker.Set<int>(ContractClausesOffsets.Value, OriginalBytes, MemoryAddress, DatabaseMode, Value);
            PropertyInvoker.Set<sbyte>(ContractClausesOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<sbyte>(ContractClausesOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode, Info);
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

        private sbyte _type = 0;
        public sbyte Type {
            get {
                if (_type == 0) {
                    _type = PropertyInvoker.Get<sbyte>(ContractClausesOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private sbyte _info = 0;
        public sbyte Info {
            get {
                if (_info == 0) {
                    _info = PropertyInvoker.Get<sbyte>(ContractClausesOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode);
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
