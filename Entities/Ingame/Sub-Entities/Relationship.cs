using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Defines.Offsets;
using System.ComponentModel;
using FMScoutFramework.Entities.Ingame.Interfaces;
using FMScoutFramework.Core.Offsets;

namespace FMScoutFramework.Core.Entities.InGame {
    public enum RelationshipType {
        [Description("Favourite Person")]
        RTFavouritePerson                   = 1,
        [Description("Disliked Person")]
        RTDislikedPerson                    = 2,
        [Description("Favourite Club")]
        RTFavouriteClub                     = 3,
        [Description("Disliked Club")]
        RTDislikedClub                      = 4,
        [Description("Rival Club")]
        RTRivalClub                         = 5,
        [Description("Rival Nation")]
        RTRivalNation                       = 6,
        [Description("Relative Born in Nation")]
        RTRelativeBordInNation              = 7,
        [Description("Born in Nation")]
        RTBornInNation                      = 8,
        [Description("Has Nationality")]
        RTHasNationality                    = 9,
        [Description("International Retirement")]
        RTInternationalRetirement           = 10,
        [Description("Player in Temporary Team")]
        RTPlayerInTemporaryTeam             = 11,
        [Description("Manager in Temporary Team")]
        RTManagerInTemporaryTeam            = 12,
        [Description("Superdraft Allocation")]
        RTSuperdraftAllocation              = 13,
        [Description("Famous Old Star")]
        RTFamousOldStar                     = 14,
        [Description("Pundit for Nation")]
        RTPunditForNation                   = 15,
        [Description("Disliked Teammate")]
        RTDislikedTeamMate                  = 16,
        [Description("Disliked Assistant Manager")]
        RTDislikedAssistantManager          = 17,
        [Description("Disliked Manager")]
        RTDislikedManager                   = 18,
        [Description("Sold Star Player")]
        RTSoldStarPlayer                    = 19,
        [Description("Sold Youngster")]
        RTSoldYoungster                     = 20,
        [Description("Waiver Draft Allocation")]
        RTWaiverDraftAllocation             = 21,
        [Description("Rival Person")]
        RTRivalPerson                       = 22,
        [Description("Reserve Stadium")]
        RTReserveStadium                    = 23,
        [Description("Unknown 24")]
        RTUnknown24                         = 24,
        [Description("Registered for Squad")]
        RTRegisteredForSquad                = 25,
        [Description("Newly Selected In Squad")]
        RTNewSelectedInSquad                = 26,
        [Description("Newly Unselected from Squad")]
        RTNewUnselectedFromSquad            = 27,
        [Description("Selected for Other Team / Ineligible")]
        RTSelectedForOtherTeamInel          = 28,
        [Description("Club Has Squad Selection")]
        RTClubHasSquadSelection             = 29,
        [Description("Player Form (Media)")]
        RTMediaPlayerForm                   = 30,
        [Description("Big Name Purchase (Media)")]
        RTMediaBigNamePurchase              = 31,
        [Description("Player Unhappy at Sale of Player (Media)")]
        RTMediaPlayerUnhappyAtSaleOfPlayer  = 32,
        [Description("Long Serving Player Leaves (Media)")]
        RTMediaLongServingPlayerLeaves      = 33,
        [Description("Transfer Rumour (Media)")]
        RTMediaTransferRumour               = 34,
        [Description("Player Requests Leave (Media)")]
        RTMediaPlayerRequestsLeave          = 35,
        [Description("Wait (Media)")]
        RTMediaWait                         = 36,
        [Description("Player Contract (Media)")]
        RTMediaPlayerContract               = 37,
        [Description("Poll (Media)")]
        RTMediaPoll                         = 38,
        [Description("Player Morale (Media)")]
        RTMediaPlayerMorale                 = 39,
        [Description("Remove Player Morale (Media)")]
        RTMediaRemovePlayerMorale           = 40,
        [Description("Transfer Speculation (Media)")]
        RTMediaTransferSpeculation          = 41,
        [Description("RPS (Media)")]
        RTMediaRPS                          = 42,
        [Description("Agent Approach (Media)")]
        RTMediaAgentApproach                = 43,
        [Description("Player Conflict (Media)")]
        RTMediaPlayerConflict               = 44,
        [Description("Job Application")]
        RTJobApplication                    = 50,
        [Description("Sell Player End of Season")]
        RTSellPlayerEndOfSeason             = 60,
        [Description("Getting Improved Contract if Play Well")]
        RTPlayerGettingImprovedContractIfPlayWell   = 61,
        [Description("Improved Contract at the End of the Season")]
        RTPlayerImprovedContractAtEndOfSeason       = 62,
        [Description("Will Get Opportunities at the End of the Season")]
        RTPlayerImprovedOpportunitiesAtEndOfSeason  = 63,
        [Description("Not Selling Player")]
        RTNotSellingPlayer                          = 64,
        [Description("Not Getting Improved Contract")]
        RTPlayerNotGettingImprovedContract          = 65,
        [Description("Will be Selected when Chosen")]
        RTPlayerWillBeSelectedWhenChosen            = 66,
        [Description("Trained in Nation")]
        RTTrainedInNation                           = 70,
        [Description("Trained at Club")]
        RTTrainedAtClub                             = 72,
        [Description("45 Minutes Only (IFI)")]
        RTIFI45                                     = 80,
        [Description("Withdrawn (IFI)")]
        RTIFIWithdrawn                              = 81,
        [Description("Unhappy at being withdrawn (IFI)")]
        RTIFIUnhappyAtBeingWithdrawn                = 82,
        [Description("PRAT Player")]
        RTPratPlayer                                = 85,
        [Description("Recent Media")]
        RTRecentMedia                               = 86,
        [Description("Transfer Rumour")]
        RTTransferRumour                            = 87,
        [Description("Serious Injury")]
        RTSeriousInjury                             = 88,
        [Description("Unbeaten Run (TFN)")]
        RTUnbeatenRun                               = 90,
        [Description("Straight Wins (TFN)")]
        RTStraightWins                              = 91,
        [Description("Games without Win")]
        RTGamesWithoutWin                           = 92,
        [Description("Straight Defeats")]
        RTStraightDefeats                           = 93,
        [Description("Injury Crisis at Club")]
        RTInjuryCrisis                              = 95,
        [Description("Player Praise")]
        RTPlayerPraise                              = 96,
        [Description("Games Played")]
        RTGamesPlayed                               = 106
    }

    public enum RelationshipRecordType {
        RTRecordNotUsed     = 0,
        RTClub              = 1,
        RTNation            = 2,
        RTPerson            = 3,
        RTTeam              = 4,
        RTStadium           = 5
    }

    public enum FavouritePersonReasons {
        [Description("No Reason")]
        FPRNoReason         = 1,
        [Description("Brother")]
        FPRBrother          = 2,
        [Description("Son")]
        FPRSon              = 3,
        [Description("Relation")]
        FPRRelation         = 4,
        [Description("Teammate")]
        FPRTeammate         = 5,
        [Description("Manager")]
        FPRManager          = 6,
        [Description("Player")]
        FPRPlayer           = 7,
        [Description("Backroom Staff")]
        FPRBackroomStaff    = 8,
        [Description("Idol")]
        FPRIdol             = 9,
        [Description("Friend")]
        FPRFriend           = 10
    }

    public enum FavouriteClubReasons {
        [Description("No Reason")]
        FCRNoReason         = 0,
        [Description("Supporter")]
        FCRSupporter        = 1,
        [Description("Player")]
        FCRPlayer           = 2,
        [Description("Manager")]
        FCRManager          = 3,
        [Description("Backroom Staff")]
        FCRBackroomStaff    = 4
    }

    public class Relationship : BaseObject, IRelationship {
        private RelationshipOffsets RelationshipOffsets;

        public Relationship (Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) {
            RelationshipOffsets = new RelationshipOffsets(version);
        }
        public Relationship (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) {
            RelationshipOffsets = new RelationshipOffsets(version);
        }

        public void Save() {
            PropertyInvoker.Set<Int64>(RelationshipOffsets.AssociatedAddress, OriginalBytes, MemoryAddress, DatabaseMode, AssociatedObjectAddress);
            PropertyInvoker.Set<byte>(RelationshipOffsets.RecordType, OriginalBytes, MemoryAddress, DatabaseMode, RecordType);
            PropertyInvoker.Set<sbyte>(RelationshipOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode, Info);
            PropertyInvoker.Set<byte>(RelationshipOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<byte>(RelationshipOffsets.Permanent, OriginalBytes, MemoryAddress, DatabaseMode, _permanent);
            PropertyInvoker.Set<byte>(RelationshipOffsets.Level, OriginalBytes, MemoryAddress, DatabaseMode, _level);

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

        private Int64 _associatedObjectAddress = 0;
        public Int64 AssociatedObjectAddress {
            get {
                if (_associatedObjectAddress == 0x0) {
                    _associatedObjectAddress = PropertyInvoker.Get<Int64>(RelationshipOffsets.AssociatedAddress, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _associatedObjectAddress;
            }
            set {
                if (_associatedObjectAddress != value) {
                    isDirty = true;
                    _associatedObjectAddress = value;
                    // Force - reload the associated object
                    _associatedObject = null;
                }
            }
        } 


        private object _associatedObject = null;
        public object AssociatedObject {
            get {
                if (_associatedObject == null) {

                    switch ((RelationshipRecordType)RecordType) {
                        case RelationshipRecordType.RTClub:
                            _associatedObject = new Club(AssociatedObjectAddress, Version);
                            break;
                        case RelationshipRecordType.RTNation:
                            _associatedObject = new Nation(AssociatedObjectAddress, Version);
                            break;
                        case RelationshipRecordType.RTPerson:
                            // What kind of person? Switch known relationships and return the right type
                            Int64 PersonAddress = AssociatedObjectAddress;
                            Int64 PersonType = PropertyInvoker.Get<Int64>(0x0, OriginalBytes, PersonAddress, DatabaseMode);

                            if (PersonType == Version.PersonEnum.Player) {
                                _associatedObject = new Player((PersonAddress + Version.PersonOffsets.Player), Version);
                            }
                            else if (PersonType == Version.PersonEnum.Staff) {
                                _associatedObject = new Staff((PersonAddress + Version.PersonOffsets.Staff), Version);
                            }
                            break;
                        case RelationshipRecordType.RTStadium:
                            _associatedObject = new Stadium(AssociatedObjectAddress, Version);
                            break;
                        case RelationshipRecordType.RTTeam:
                            _associatedObject = new Team(AssociatedObjectAddress, Version);
                            break;
                        default:
                            _associatedObject = null;
                            break;
                    }
                }

                return _associatedObject;
            }
        }

        private byte _recordType = 0;
        public byte RecordType {
            get {
                if (_recordType == 0) {
                    _recordType = PropertyInvoker.Get<byte>(RelationshipOffsets.RecordType, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _recordType;
            }
            set {
                if (_recordType != value) {
                    isDirty = true;
                    _recordType = value;
                }
            }
        }

        private sbyte _info = -1;
        public sbyte Info {
            get {
                if (_info == -1) {
                    _info = PropertyInvoker.Get<sbyte>(RelationshipOffsets.Info, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private byte _type = 0;
        public byte Type {
            get {
                if (_type == 0) {
                    _type = PropertyInvoker.Get<byte>(RelationshipOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
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

        private byte _level = 0;
        public byte Level {
            get {
                if (_level == 0) {
                    _level = PropertyInvoker.Get<byte>(RelationshipOffsets.Level, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _level;
            }
            set {
                if (_level != value) {
                    isDirty = true;
                    _level = value;
                }
            }
        }

        private byte _permanent = 0;
        public bool Permanent {
            get {
                if (_permanent == 0) {
                    _permanent = PropertyInvoker.Get<byte>(RelationshipOffsets.Permanent, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return (_permanent == 79);
            }
            set {
                byte val = value == true ? (byte)79 : (byte)0;
                if (_permanent != val) {
                    isDirty = true;
                    _permanent = val;
                }
            }
        }

        public string Offset {
            get {
                return "0x" + MemoryAddress.ToString("X");
            }
        }
    }
}
