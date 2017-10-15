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

        public void Save() {
            #region String Save Experiments

            /*
            // If we're changing the name, we need to allocate a new region.
            // If we allocated previously, make sure to dealloc first
            // TODO: Dealloc

            // Let's first get the name in bytes. We'll need to know how much data to allocate
            byte[] newName = ProcessManager.GetFMStringBytes(_name);

            // Allocate memory for the bytes
            Int64 newAddress = ProcessManager.AllocateProcessBytes(newName.Length);
            Int64 address = newAddress;

            // Prepend an int
            PropertyInvoker.Set<int>(0x0, OriginalBytes, address, DatabaseMode, 1);
            address += 0x4;

            // Write the bytes at the location
            ProcessManager.WriteProcessMemory(address, newName, (uint)newName.Length);
            Int64 namePtr = address;
            address += newName.Length;

            // Append some extra bytes at the end
            byte[] extras = new byte[] {
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0x0,
                0xF,
                0x0,
                0x0,
                0x0                
            };
            ProcessManager.WriteProcessMemory(address, extras, (uint)extras.Length);

            // And replace the pointer in the club object
            PropertyInvoker.Set<Int64>(ClubOffsets.Name, OriginalBytes, MemoryAddress, DatabaseMode, namePtr);
            */
            #endregion

            PropertyInvoker.Set<Int64>(ClubOffsets.City, OriginalBytes, MemoryAddress, DatabaseMode, CityAddress);

            isDirty = false;
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

        private string _name;
        public string Name {
            get {
                if (String.IsNullOrEmpty(_name)) {
                    _name = PropertyInvoker.GetString(ClubOffsets.Name, -1, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _name;
            }
            set {
                if (_name != value) {
                    _name = value;
                    isDirty = true;
                }
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

        private Int64 _cityAddress = 0x0;
        public Int64 CityAddress {
            get {
                if (_cityAddress == 0x0) {
                    _cityAddress = PropertyInvoker.Get<Int64>(ClubOffsets.City, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _cityAddress;
            }
            set {
                if (_cityAddress != value) {
                    isDirty = true;
                    _cityAddress = value;
                    _city = null;
                }
            }
        }

        private City _city;
        public City City {
            get {
                if (_city == null) {
                    _city = new City(CityAddress, Version);
                }

                return _city;
            }
        }

        public string ShortName {
            get {
                return PropertyInvoker.GetString(ClubOffsets.ShortName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
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
                    Int64 sponsorshipArrayPtr = PropertyInvoker.Get<Int64>(ClubOffsets.ClubSponshorshipDeals, OriginalBytes, MemoryAddress, DatabaseMode);
                    if (sponsorshipArrayPtr > 0x0) {
                        int sponsorshipCount = ProcessManager.ReadArrayLength(sponsorshipArrayPtr);
                        if (sponsorshipCount > 0) {
                            Int64 sponsorshipsAddress = PropertyInvoker.Get<Int64>(0x0, OriginalBytes, sponsorshipArrayPtr, DatabaseMode);
                            for (int i = 0; i < sponsorshipCount; i++) {
                                _sponsorshipDeals.Add(PropertyInvoker.GetPointer<ClubSponsorshipDeal>(0x0, OriginalBytes, (sponsorshipsAddress + (i * 0x8)), DatabaseMode, Version));
                            }
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
