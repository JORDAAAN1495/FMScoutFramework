using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using System.Collections.Generic;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Club : BaseObject, IClub
    {
        public ClubOffsets ClubOffsets;
        public Club (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        {
            this.ClubOffsets = new ClubOffsets (Version);
        }
        public Club (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        {
            this.ClubOffsets = new ClubOffsets (Version);
        }

        public Int32 RowID {
            get {
                return PropertyInvoker.Get<Int32> (ClubOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Int32 UID {
            get {
                return PropertyInvoker.Get<Int32> (ClubOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        // TEMP
        public int cityID { get; set; }
        public List<ClubSponsorshipDeal> sponsorshipDeals = new List<ClubSponsorshipDeal>();
        public City city { get; set; }

        private List<Team> _teams = new List<Team>();
        public List<Team> Teams {
            get {
                if (_teams.Count == 0) {
                    // Try and get the teams if it's 0
                    int teamCount = ProcessManager.ReadArrayLength(MemoryAddress + ClubOffsets.Teams);
                    Int64 TeamArrayAddress = PropertyInvoker.Get<Int64>(ClubOffsets.Teams, OriginalBytes, MemoryAddress, DatabaseMode);
                    if (teamCount > 0) {
                        for (int i = 0; i < teamCount; i++) {
                            _teams.Add(PropertyInvoker.GetPointer<Team>(0x0, OriginalBytes, (TeamArrayAddress + (i * 0x8)), DatabaseMode, Version));
                        }
                    }
                }

                return _teams;
            }
        }

        private ClubInfoOne _infoOne;
        public ClubInfoOne InfoOne {
            get {
                if (_infoOne == null) {
                    _infoOne = PropertyInvoker.GetPointer<ClubInfoOne>(ClubOffsets.ClubInfoOne, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }
                return _infoOne;
            }
        }

        private ClubInfoTwo _infoTwo;
        public ClubInfoTwo InfoTwo {
            get {
                if (_infoTwo == null) {
                    _infoTwo = PropertyInvoker.GetPointer<ClubInfoTwo>(ClubOffsets.ClubInfoTwo, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }

                return _infoTwo;
            }
        }

        public string Fullname {
            get {
                return PropertyInvoker.GetString(ClubOffsets.Fullname, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Name {
            get {
                return PropertyInvoker.GetString(ClubOffsets.Name, 0x0, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private City _city;
        public City City {
            get {
                if (_city == null) {
                    _city = PropertyInvoker.GetPointer<City>(ClubOffsets.City, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }

                return _city;
            }
        }

        public string ShortName {
            get {
                return PropertyInvoker.GetString(ClubOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }



        public override string ToString ()
        {
            return Name;
        }
    }
}
