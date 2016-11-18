using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using System.ComponentModel;
using System.Windows.Media;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Defines.Offsets;

namespace FMScoutFramework.Core.Entities.InGame {
    #region Enums
    public enum AwardVotingType {
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

    public enum AwardPeriod {
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

    public enum AwardRecipientType {
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

    public enum AwardType {
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
        [Description("Fair Play Team")]
        ATFairPlayTeam = 42,
        [Description("Comeback")]
        ATComeback = 43,
        [Description("Most Clean Sheets")]
        ATMostCleanSheets = 44
    }

    public enum AwardNumberOfPlacings {
        [Description("Not Set")]
        ANPNotSet = 0,
        [Description("1st to 3rd Place")]
        ANP1stTo3rdPlace = 1,
        [Description("1st Place Only")]
        ANP1stPlaceOnly = 2,
        [Description("1st and 2nd Place")]
        ANP1stAnd2ndPlace = 3
    }

    public enum AwardPosition {
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

    public enum AwardSide {
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

    public enum AwardVotingFormat {
        [Description("Not Set")]
        AVFNotSet = 0,
        [Description("One Vote Each")]
        AVFOneVoteEach = 1,
        [Description("Three Votes Weighted")]
        AVF3VotesWeighted = 2,
        [Description("Five Votes Weighted")]
        AVF5VotesWeighted = 3
    }

    public enum AwardRunBy {
        [Description("Not Set")]
        ARBNotSet = 0,
        [Description("FA")]
        ARBFA = 1,
        [Description("Media")]
        ARBMedia = 2,
        [Description("Players")]
        ARBPlayers = 3
    }

    public enum AwardBased {
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

    public enum AwardUseStatsFrom {
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

    public enum AwardAllowPreviousWinner {
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

    public class Award : BaseObject, IAward {
        public Award(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) { }
        public Award(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) { }

        public int RowID {
            get {
                return PropertyInvoker.Get<Int32>(AwardOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int UID {
            get {
                return PropertyInvoker.Get<Int32>(AwardOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name {
            get {
                return PropertyInvoker.GetString(AwardOffsets.Name, 0x0, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Color colour {
            get {
                return Color.FromArgb(0, 0, 0, 0);
            }
        }

        public int voting {
            get {
                return 0;
            }
            set {
                voting = value;
            }
        }

        public override string ToString() {
            return Name + " (0x" + MemoryAddress.ToString("X") + ")";
        }
    }
}
