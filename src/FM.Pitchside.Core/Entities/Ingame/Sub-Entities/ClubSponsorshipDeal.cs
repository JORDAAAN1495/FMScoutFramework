using FM.Pitchside.Core.Defines.Offsets.Sub_Entities;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;
using System.ComponentModel;

namespace FM.Pitchside.Core.Entities.Ingame.Sub_Entities
{
    public enum SponsorshipType
    {
        [Description("None")]
        STNone = 0,
        [Description("Main Kit")]
        STMainKit = 1,
        [Description("Government Council Grant")]
        STGovernmentCouncilGrant = 2,
        [Description("Stadium")]
        STStadium = 3,
        [Description("General")]
        STGeneral = 4,
        [Description("Individual TV Deal")]
        STIndividualTVDeal = 5,
        [Description("Other Income")]
        STOtherIncome = 6,
        [Description("Club Membership")]
        STClubMembership = 7,
        [Description("Secondary Kit")]
        STSecondaryKit = 8,
        [Description("Other Kit")]
        STOtherKit = 9,
        [Description("Parachute Payment")]
        STParachutePayment = 10,
        [Description("Back of Shirt")]
        STBackOfShirt = 11,
        [Description("Shorts")]
        STShorts = 12,
        [Description("Training Kit")]
        STTrainingKit = 13,
        [Description("Youth Team")]
        STYouthTeam = 14,
        [Description("Training Ground")]
        STTrainingGround = 15,
        [Description("Continental Competition")]
        STContinentalCompetition = 16,
        [Description("Equity Injection")]
        STEquityInjection = 17,
        [Description("Total Commercial Income")]
        STTotalCommercialIncome = 18
    }

    public class ClubSponsorshipDeal : BaseObject, IClubSponsorshipDeal
    {
        private ClubSponsorshipDealsOffsets ClubSponsorshipDealsOffsets;
        public ClubSponsorshipDeal(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            ClubSponsorshipDealsOffsets = new ClubSponsorshipDealsOffsets(version);
        }
        public ClubSponsorshipDeal(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            ClubSponsorshipDealsOffsets = new ClubSponsorshipDealsOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<DateTime>(ClubSponsorshipDealsOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode, StartDate);
            PropertyInvoker.Set<DateTime>(ClubSponsorshipDealsOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode, EndDate);
            PropertyInvoker.Set<int>(ClubSponsorshipDealsOffsets.TotalIncome, OriginalBytes, MemoryAddress, DatabaseMode, TotalDealValue);
            PropertyInvoker.Set<byte>(ClubSponsorshipDealsOffsets.SponsorshipType, OriginalBytes, MemoryAddress, DatabaseMode, SponsorshipType);
            _isDirty = false;
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                if (_startDate.Year < 1970)
                {
                    _startDate = PropertyInvoker.Get<DateTime>(ClubSponsorshipDealsOffsets.StartDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _startDate;
            }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    isDirty = true;
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                if (_endDate.Year < 1970)
                {
                    _endDate = PropertyInvoker.Get<DateTime>(ClubSponsorshipDealsOffsets.EndDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _endDate;
            }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    isDirty = true;
                }
            }
        }

        private int _totalDealValue = 0;
        public int TotalDealValue
        {
            get
            {
                if (_totalDealValue == 0)
                {
                    _totalDealValue = PropertyInvoker.Get<int>(ClubSponsorshipDealsOffsets.TotalIncome, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _totalDealValue;
            }
            set
            {
                if (_totalDealValue != value)
                {
                    _totalDealValue = value;
                    isDirty = true;
                }
            }
        }

        private byte _sponsorshipType = 0;
        public byte SponsorshipType
        {
            get
            {
                if (_sponsorshipType == 0)
                {
                    _sponsorshipType = PropertyInvoker.Get<byte>(ClubSponsorshipDealsOffsets.SponsorshipType, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _sponsorshipType;
            }
            set
            {
                if (_sponsorshipType != value)
                {
                    _sponsorshipType = value;
                    isDirty = true;
                }
            }
        }
    }
}