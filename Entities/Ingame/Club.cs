using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
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

        public string Offset {
            get {
                return "0x" + MemoryAddress.ToString("X");
            }
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

        private List<Team> _teams = new List<Team>();
        public List<Team> Teams {
            get {
                if (_teams.Count == 0) {
                    // Try and get the teams if it's 0
                    int teamCount = ProcessManager.ReadArrayLength(MemoryAddress + ClubOffsets.Teams);
                    if (teamCount > 0) {
                        Int64 TeamArrayAddress = PropertyInvoker.Get<Int64>(ClubOffsets.Teams, OriginalBytes, MemoryAddress, DatabaseMode);
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

        //public string Fullname {
        //    get {
        //        return PropertyInvoker.GetString(ClubOffsets.Fullname, 0x0, OriginalBytes, MemoryAddress, DatabaseMode);
        //    }
        //}

        public string Name {
            get {
                return PropertyInvoker.GetString(ClubOffsets.Name, 0x0, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private Nation _basedNation;
        public Nation BasedNation {
            get {
                if (_basedNation == null) {
                    _basedNation = PropertyInvoker.GetPointer<Nation>(ClubOffsets.BasedNation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }
                return _basedNation;
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
                return PropertyInvoker.GetString(ClubOffsets.ShortName, 0x0, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private Nation _nation;
        public Nation Nation {
            get {
                if (_nation == null) {
                    _nation = PropertyInvoker.GetPointer<Nation>(ClubOffsets.Nation, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }
                return _nation;
            }
        }

        private ClubFinances _clubFinances;
        public ClubFinances ClubFinances {
            get {
                if (_clubFinances == null) {
                    _clubFinances = PropertyInvoker.GetPointer<ClubFinances>(ClubOffsets.ClubFinances, OriginalBytes, MemoryAddress, DatabaseMode, Version);
                }
                return _clubFinances;
            }
        }

        private List<ClubSponsorshipDeal> _sponsorshipDeals = new List<ClubSponsorshipDeal>();
        public List<ClubSponsorshipDeal> SponsorshipDeals {
            get {
                if (_sponsorshipDeals.Count == 0) {
                    int sponsorshipCount = ProcessManager.ReadArrayLength(MemoryAddress + ClubOffsets.ClubSponshorshipDeals);
                    if (sponsorshipCount > 0) {
                        Int64 sponsorshipsAddress = PropertyInvoker.Get<Int64>(ClubOffsets.ClubSponshorshipDeals, OriginalBytes, MemoryAddress, DatabaseMode);
                        for (int i = 0; i < sponsorshipCount; i++) {
                            _sponsorshipDeals.Add(PropertyInvoker.GetPointer<ClubSponsorshipDeal>(0x0, OriginalBytes, (sponsorshipsAddress + (i * 0x8)), DatabaseMode, Version));
                        }
                    }
                }
                return _sponsorshipDeals;
            }
        }

        public override string ToString ()
        {
            return Name;
        }
    }
}
