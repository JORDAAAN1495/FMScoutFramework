using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Defines.Offsets;
using System;
using System.ComponentModel;
using System.Drawing;

namespace FMScoutFramework.Core.Entities.InGame
{
    #region Enums
    public enum AwardVotingType
    {
        [Description("No Voting")]
        AVTNoVoting = 1,
        [Description("Competition Club Manager")]
        AVTCompetitionClubManager = 2,
        [Description("Nation Club Manager")]
        AVTNationClubManager = 3,
        [Description("Continent Club Manager")]
        AVTContinentClubManager = 4,
        [Description("Continent Nation Manager")]
        AVTContinentNationManager = 5,
        [Description("World Club Manager")]
        AVTWorldClubManager = 6,
        [Description("World Nation Manager")]
        AVTWorldNationManager = 7,
        [Description("Any Manager")]
        AVTAnyManager = 8
    }

    public enum AwardPeriod
    {
        [Description("Invalid")]
        APInvalid = 0,
        [Description("of the Week")]
        APOfTheWeek = 1,
        [Description("of the Month")]
        APOfTheMonth = 2,
        [Description("of the Season")]
        APOfTheSeason = 3,
        [Description("of the Competition")]
        APOfTheCompetition = 4,
        [Description("of the Year")]
        APOfTheYear = 5,
        [Description("on specified date")]
        APOnSpecifiedDate = 6,
        [Description("of the Round")]
        APOfTheRound = 7,
        [Description("of the Opening or Closing Season")]
        APOfTheOpeningOrClosingSeason = 8
    }

    public enum AwardRecipientType
    {
        [Description("Not Set")]
        ARTNotSet = 0,
        [Description("Player")]
        ARTPlayer = 1,
        [Description("Non Player")]
        ARTNonPlayer = 2,
        [Description("Player or Non Player")]
        ARTPlayerOrNonPlayer = 3,
        [Description("Referee")]
        ARTReferee = 4,
        [Description("Rookie")]
        ARTRookie = 5,
        [Description("Squad of Players")]
        ARTSquadOfPlayers = 6,
        [Description("Club or Nation")]
        ARTClubOrNation = 7
    }

    public enum AwardType
    {
        [Description("Ignore")]
        ATIgnore = -1,
        [Description("Not Set")]
        ATNotSet = 0,
        [Description("Club")]
        ATClub = 1,
        [Description("Manager")]
        ATManager = 3,
        [Description("Top Goalscorer")]
        ATTopGoalscorer = 4,
        [Description("Most Man of the Match")]
        ATMostManOfTheMatch = 5,
        [Description("Fair Play Team")]
        ATFairPlayTeam = 8,
        [Description("Personality")]
        ATPersonality = 14,
        [Description("Goal")]
        ATGoal = 15,
        [Description("Referee")]
        ATReferee = 16,
        [Description("Youth Policy")]
        ATYouthPolicy = 17,
        [Description("Recruitment Policy")]
        ATRecruitmentPolicy = 19,
        [Description("Assistant Referee")]
        ATAssistantReferee = 20,
        [Description("Chairman")]
        ATChairman = 21,
        [Description("Physio")]
        ATPhysio = 22,
        [Description("Coach")]
        ATCoach = 24,
        [Description("Most Assists")]
        ATMostAssists = 30,
        [Description("Quickest Goal")]
        ATQuickestGoal = 31,
        [Description("100th League Goal")]
        AT100thLeagueGoal = 32,
        [Description("Flair Player")]
        ATFlairPlayer = 33,
        [Description("Highest Average Rating")]
        ATHighestAverageRating = 37,
        [Description("Highest Reputation")]
        ATHighestReputation = 38,
        [Description("Least Goals Conceded")]
        ATLeastGoalsConceded = 39,
        [Description("Most Improved")]
        ATMostImproved = 41,
        [Description("Comeback")]
        ATComeback = 43,
        [Description("Most Clean Sheets")]
        ATMostCleanSheets = 44
    }

    public enum AwardNumberOfPlacings
    {
        [Description("Not Set")]
        ANPNotSet = 0,
        [Description("1st to 3rd Place")]
        ANP1stTo3rdPlace = 1,
        [Description("1st Place Only")]
        ANP1stPlaceOnly = 2,
        [Description("1st and 2nd Place")]
        ANP1stAnd2ndPlace = 3
    }

    public enum AwardPosition
    {
        [Description("Not Set")]
        APNotSet = 0,
        [Description("Goalkeeper")]
        APGoalkeeper = 1,
        [Description("Sweeper")]
        APSweeper = 2,
        [Description("Full Back")]
        APFullBack = 12,
        [Description("Defender")]
        APDefender = 30,
        [Description("Wing Back")]
        APWingBack = 96,
        [Description("Defensive Midfielder")]
        APDefensiveMidfielder = 128,
        [Description("Midfielder")]
        APMidfielder = 16256,
        [Description("Attacking Midfielder")]
        APAttackingMidfielder = 14336,
        [Description("Winger")]
        APWinger = 6144,
        [Description("Striker")]
        APStriker = 16384
    }

    public enum AwardSide
    {
        [Description("Any")]
        ASAny = 0,
        [Description("Left")]
        ASLeft = 1,
        [Description("Right")]
        ASRight = 2,
        [Description("Centre")]
        ASCentre = 3,
        [Description("Left or Right")]
        ASLeftOrRight = 4
    }

    public enum AwardVotingFormat
    {
        [Description("Not Set")]
        AVFNotSet = 0,
        [Description("One Vote Each")]
        AVFOneVoteEach = 1,
        [Description("Three Votes Weighted")]
        AVF3VotesWeighted = 2,
        [Description("Five Votes Weighted")]
        AVF5VotesWeighted = 3
    }

    public enum AwardRunBy
    {
        [Description("Not Set")]
        ARBNotSet = 0,
        [Description("FA")]
        ARBFA = 1,
        [Description("Media")]
        ARBMedia = 2,
        [Description("Players")]
        ARBPlayers = 3
    }

    public enum AwardBased
    {
        [Description("Not Set")]
        ABNotSet = 0,
        [Description("Foreign Based Nationals")]
        ABForeignBasedNationals = 1,
        [Description("Domestic Based")]
        ABDomesticBased = 2,
        [Description("Domestic Based Foreigners")]
        ABDomesticBasedForeigners = 3,
        [Description("Domestic Based Nationals")]
        ABDomesticBasedNationals = 4,
        [Description("Anywhere Nationals")]
        ABAnywhereNationals = 5,
        [Description("Anywhere Anyone")]
        ABAnywhereAnyone = 6,
        [Description("Domestic Based African")]
        ABDomesticBasedAfrican = 7,
        [Description("Continent Anyone")]
        ABContinentAnyone = 8
    }

    public enum AwardUseStatsFrom
    {
        [Description("Not Set")]
        AUSNotSet = 0,
        [Description("Domestic League")]
        AUSDomesticLeague = 1,
        [Description("Domestic Cup")]
        AUSDomesticCup = 2,
        [Description("Domestic Overall")]
        AUSDomesticOverall = 3,
        [Description("Continental")]
        AUSContinental = 4,
        [Description("Overall Club")]
        AUSOverallClub = 5,
        [Description("International")]
        AUSInternational = 6,
        [Description("Overall")]
        AUSOverall = 7
    }

    public enum AwardAllowPreviousWinner
    {
        [Description("Not Set")]
        APWNotSet = 0,
        [Description("Never")]
        APWNever = 1,
        [Description("Rarely")]
        APWRarely = 2,
        [Description("Yes")]
        APWYes = 3
    }
    #endregion

    public class Award : BaseObject, IAward
    {
        public Award(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public Award(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public void Save()
        {
            PropertyInvoker.Set<DateTime>(AwardOffsets.AwardDate, OriginalBytes, MemoryAddress, DatabaseMode, AwardDate);
            PropertyInvoker.Set<DateTime>(AwardOffsets.AnnouncementDate, OriginalBytes, MemoryAddress, DatabaseMode, AnnouncementDate);
            PropertyInvoker.Set<short>(AwardOffsets.Position, OriginalBytes, MemoryAddress, DatabaseMode, Position);
            PropertyInvoker.Set<byte>(AwardOffsets.RunBy, OriginalBytes, MemoryAddress, DatabaseMode, RunBy);
            PropertyInvoker.Set<byte>(AwardOffsets.Period, OriginalBytes, MemoryAddress, DatabaseMode, Period);
            PropertyInvoker.Set<byte>(AwardOffsets.Voting, OriginalBytes, MemoryAddress, DatabaseMode, Voting);
            PropertyInvoker.Set<byte>(AwardOffsets.Based, OriginalBytes, MemoryAddress, DatabaseMode, Based);
            PropertyInvoker.Set<byte>(AwardOffsets.VotingFormat, OriginalBytes, MemoryAddress, DatabaseMode, VotingFormat);
            PropertyInvoker.Set<byte>(AwardOffsets.RecipientType, OriginalBytes, MemoryAddress, DatabaseMode, RecipientType);
            PropertyInvoker.Set<byte>(AwardOffsets.Formation, OriginalBytes, MemoryAddress, DatabaseMode, Formation);
            PropertyInvoker.Set<Color>(AwardOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode, ForegroundColour);
            PropertyInvoker.Set<Color>(AwardOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode, BackgroundColour);
            PropertyInvoker.Set<Color>(AwardOffsets.TrimColour, OriginalBytes, MemoryAddress, DatabaseMode, TrimColour);
            PropertyInvoker.Set<byte>(AwardOffsets.AwardReputation, OriginalBytes, MemoryAddress, DatabaseMode, AwardReputation);
            PropertyInvoker.Set<sbyte>(AwardOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<byte>(AwardOffsets.MinimumAge, OriginalBytes, MemoryAddress, DatabaseMode, MinimumAge);
            PropertyInvoker.Set<byte>(AwardOffsets.MaximumAge, OriginalBytes, MemoryAddress, DatabaseMode, MaximumAge);
            PropertyInvoker.Set<byte>(AwardOffsets.WinnerHomeReputation, OriginalBytes, MemoryAddress, DatabaseMode, WinnerHomeReputation);
            PropertyInvoker.Set<byte>(AwardOffsets.WinnerWorldReputation, OriginalBytes, MemoryAddress, DatabaseMode, WinnerWorldReputation);
            PropertyInvoker.Set<byte>(AwardOffsets.Placings, OriginalBytes, MemoryAddress, DatabaseMode, Placings);
            PropertyInvoker.Set<byte>(AwardOffsets.Side, OriginalBytes, MemoryAddress, DatabaseMode, Side);
            PropertyInvoker.Set<byte>(AwardOffsets.UseStatsFrom, OriginalBytes, MemoryAddress, DatabaseMode, UseStatsFrom);
            PropertyInvoker.Set<byte>(AwardOffsets.MinimumPercentageOfGamesPlayed, OriginalBytes, MemoryAddress, DatabaseMode, MinimumPercentageOfGamesPlayed);
            PropertyInvoker.Set<byte>(AwardOffsets.AllowPreviousWinner, OriginalBytes, MemoryAddress, DatabaseMode, AllowPreviousWinner);
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

        public int RowID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(AwardOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int UID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(AwardOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Offset
        {
            get
            {
                return "0x" + MemoryAddress.ToString("X");
            }
        }

        public string Name
        {
            get
            {
                string name = PropertyInvoker.GetString(AwardOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
                if (string.IsNullOrEmpty(name))
                {
                    name = "-";
                }

                return name;
            }
        }

        public string ShortName
        {
            get
            {
                string shortname = PropertyInvoker.GetString(AwardOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
                if (string.IsNullOrEmpty(shortname))
                {
                    shortname = "-";
                }

                return shortname;
            }
        }

        private DateTime _awardDate;
        public DateTime AwardDate
        {
            get
            {
                if (_awardDate.Year < 1900)
                {
                    _awardDate = PropertyInvoker.Get<DateTime>(AwardOffsets.AwardDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _awardDate;
            }
            set
            {
                if (_awardDate != value)
                {
                    isDirty = true;
                    _awardDate = value;
                }
            }
        }

        private DateTime _announcementDate;
        public DateTime AnnouncementDate
        {
            get
            {
                if (_announcementDate.Year < 1900)
                {
                    _announcementDate = PropertyInvoker.Get<DateTime>(AwardOffsets.AnnouncementDate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _announcementDate;
            }
            set
            {
                isDirty = true;
                _announcementDate = value;
            }
        }

        private short _position = -1;
        public short Position
        {
            get
            {
                if (_position == -1)
                {
                    _position = PropertyInvoker.Get<short>(AwardOffsets.Position, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _position;
            }
            set
            {
                isDirty = true;
                _position = value;
            }
        }

        private byte _runBy = 0;
        public byte RunBy
        {
            get
            {
                if (_runBy == 0)
                {
                    _runBy = PropertyInvoker.Get<byte>(AwardOffsets.RunBy, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _runBy;
            }
            set
            {
                isDirty = true;
                _runBy = value;
            }
        }

        private byte _period = 0;
        public byte Period
        {
            get
            {
                if (_period == 0)
                {
                    _period = PropertyInvoker.Get<byte>(AwardOffsets.Period, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _period;
            }
            set
            {
                isDirty = true;
                _period = value;
            }
        }

        private byte _voting = 0;
        public byte Voting
        {
            get
            {
                if (_voting == 0)
                {
                    _voting = PropertyInvoker.Get<byte>(AwardOffsets.Voting, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _voting;
            }
            set
            {
                isDirty = true;
                _voting = value;
            }
        }

        private byte _based = 0;
        public byte Based
        {
            get
            {
                if (_based == 0)
                {
                    _based = PropertyInvoker.Get<byte>(AwardOffsets.Based, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _based;
            }
            set
            {
                isDirty = true;
                _based = value;
            }
        }

        private byte _votingFormat = 0;
        public byte VotingFormat
        {
            get
            {
                if (_votingFormat == 0)
                {
                    _votingFormat = PropertyInvoker.Get<byte>(AwardOffsets.VotingFormat, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _votingFormat;
            }
            set
            {
                isDirty = true;
                _votingFormat = value;
            }
        }

        private byte _recipientType = 0;
        public byte RecipientType
        {
            get
            {
                if (_recipientType == 0)
                {
                    _recipientType = PropertyInvoker.Get<byte>(AwardOffsets.RecipientType, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _recipientType;
            }
            set
            {
                isDirty = true;
                _recipientType = value;
            }
        }

        private byte _formation = 0;
        public byte Formation
        {
            get
            {
                if (_formation == 0)
                {
                    _formation = PropertyInvoker.Get<byte>(AwardOffsets.Formation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _formation;
            }
            set
            {
                isDirty = true;
                _formation = value;
            }
        }

        private Color _foregroundColour;
        public Color ForegroundColour
        {
            get
            {
                if (_foregroundColour == Color.Empty)
                {
                    _foregroundColour = PropertyInvoker.Get<Color>(AwardOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _foregroundColour;
            }
            set
            {
                if (_foregroundColour != value)
                {
                    isDirty = true;
                    _foregroundColour = value;
                }
            }
        }

        private Color _backgroundColour;
        public Color BackgroundColour
        {
            get
            {
                if (_backgroundColour == Color.Empty)
                {
                    _backgroundColour = PropertyInvoker.Get<Color>(AwardOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _backgroundColour;
            }
            set
            {
                if (_backgroundColour != value)
                {
                    isDirty = true;
                    _backgroundColour = value;
                }
            }
        }

        private Color _trimColour;
        public Color TrimColour
        {
            get
            {
                if (_trimColour == Color.Empty)
                {
                    _trimColour = PropertyInvoker.Get<Color>(AwardOffsets.TrimColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _trimColour;
            }
            set
            {
                if (_trimColour != value)
                {
                    isDirty = true;
                    _trimColour = value;
                }
            }
        }

        private byte _awardReputation = 0;
        public byte AwardReputation
        {
            get
            {
                if (_awardReputation == 0)
                {
                    _awardReputation = PropertyInvoker.Get<byte>(AwardOffsets.AwardReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _awardReputation;
            }
            set
            {
                isDirty = true;
                _awardReputation = value;
            }
        }

        private sbyte _type = -1;
        public sbyte Type
        {
            get
            {
                if (_type == -1)
                {
                    _type = PropertyInvoker.Get<sbyte>(AwardOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _type;
            }
            set
            {
                isDirty = true;
                _type = value;
            }
        }

        private byte _minimumAge = 0;
        public byte MinimumAge
        {
            get
            {
                if (_minimumAge == 0)
                {
                    _minimumAge = PropertyInvoker.Get<byte>(AwardOffsets.MinimumAge, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _minimumAge;
            }
            set
            {
                isDirty = true;
                _minimumAge = value;
            }
        }

        private byte _maximumAge = 0;
        public byte MaximumAge
        {
            get
            {
                if (_maximumAge == 0)
                {
                    _maximumAge = PropertyInvoker.Get<byte>(AwardOffsets.MaximumAge, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _maximumAge;
            }
            set
            {
                isDirty = true;
                _maximumAge = value;
            }
        }

        private byte _winnerHomeReputation = 0;
        public byte WinnerHomeReputation
        {
            get
            {
                if (_winnerHomeReputation == 0)
                {
                    _winnerHomeReputation = PropertyInvoker.Get<byte>(AwardOffsets.WinnerHomeReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _winnerHomeReputation;
            }
            set
            {
                isDirty = true;
                _winnerHomeReputation = value;
            }
        }

        private byte _winnerWorldReputation = 0;
        public byte WinnerWorldReputation
        {
            get
            {
                if (_winnerWorldReputation == 0)
                {
                    _winnerWorldReputation = PropertyInvoker.Get<byte>(AwardOffsets.WinnerWorldReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _winnerWorldReputation;
            }
            set
            {
                isDirty = true;
                _winnerWorldReputation = value;
            }
        }

        private byte _placings = 0;
        public byte Placings
        {
            get
            {
                if (_placings == 0)
                {
                    _placings = PropertyInvoker.Get<byte>(AwardOffsets.Placings, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _placings;
            }
            set
            {
                isDirty = true;
                _placings = value;
            }
        }

        private byte _side = 0;
        public byte Side
        {
            get
            {
                if (_side == 0)
                {
                    _side = PropertyInvoker.Get<byte>(AwardOffsets.Side, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _side;
            }
            set
            {
                isDirty = true;
                _side = value;
            }
        }

        private byte _useStatsFrom = 0;
        public byte UseStatsFrom
        {
            get
            {
                if (_useStatsFrom == 0)
                {
                    _useStatsFrom = PropertyInvoker.Get<byte>(AwardOffsets.UseStatsFrom, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _useStatsFrom;
            }
            set
            {
                isDirty = true;
                _useStatsFrom = value;
            }
        }

        private byte _minimumPercentageOfGamesPlayed = 0;
        public byte MinimumPercentageOfGamesPlayed
        {
            get
            {
                if (_minimumPercentageOfGamesPlayed == 0)
                {
                    _minimumPercentageOfGamesPlayed = PropertyInvoker.Get<byte>(AwardOffsets.MinimumPercentageOfGamesPlayed, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _minimumPercentageOfGamesPlayed;
            }
            set
            {
                isDirty = true;
                _minimumPercentageOfGamesPlayed = value;
            }
        }

        private byte _allowPreviousWinner = 0;
        public byte AllowPreviousWinner
        {
            get
            {
                if (_allowPreviousWinner == 0)
                {
                    _allowPreviousWinner = PropertyInvoker.Get<byte>(AwardOffsets.AllowPreviousWinner, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _allowPreviousWinner;
            }
            set
            {
                isDirty = true;
                _allowPreviousWinner = value;
            }
        }

        public int NationID
        {
            get
            {
                return 0;
            }
        }

        public int CompetitionID
        {
            get
            {
                return 0;
            }
        }

        public int ContinentID
        {
            get
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return Name + " (0x" + MemoryAddress.ToString("X") + ")";
        }
    }
}