using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Utilities;
using FMScoutFramework.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public enum TeamType
    {
        [Description("First")]
        TTFirst = 0,
        [Description("Reserves")]
        TTReserves = 1,
        [Description("A")]
        TTA = 2,
        [Description("B")]
        TTB = 3,
        [Description("Superdraft A")]
        TTSuperdraftA = 4,
        [Description("Superdraft B")]
        TTSuperdraftB = 5,
        [Description("Superdraft C")]
        TTSuperdraftC = 6,
        [Description("Superdraft D")]
        TTSuperdraftD = 7,
        [Description("Waivers")]
        TTWaivers = 8,
        [Description("U23")]
        TTU23 = 9,
        [Description("U21")]
        TTU21 = 10,
        [Description("U19")]
        TTU19 = 11,
        [Description("U18")]
        TTU18 = 12,
        [Description("C")]
        TTC = 13,
        [Description("Amateur")]
        TTAmateur = 14,
        [Description("II")]
        TTII = 15,
        [Description("Team 2")]
        TTTeam2 = 16,
        [Description("Team 3")]
        TTTeam3 = 17,
        [Description("U20")]
        TTU20 = 18,
        [Description("Youth Evaluation")]
        TTYouthEvaluation = 22,
        [Description("Dutch Reserves")]
        TTDutchReserves = 30
    }

    public class Team : BaseObject, ITeam
    {
        public TeamOffsets TeamOffsets;
        public Team(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.TeamOffsets = new TeamOffsets(version);
        }
        public Team(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.TeamOffsets = new TeamOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<short>(TeamOffsets.PreviousReputation, OriginalBytes, MemoryAddress, DatabaseMode, PreviousReputation);
            PropertyInvoker.Set<byte>(TeamOffsets.TeamType, OriginalBytes, MemoryAddress, DatabaseMode, TeamType);

            int rotateAmount = (int)((MemoryAddress + TeamOffsets.Reputation) & 0xf);
            uint decryptedRep = Reputation;
            // Decrypt here if needed

            PropertyInvoker.Set<ushort>(TeamOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode, (ushort)decryptedRep);

            // Save the playersArray
            Int64 arrayStartAddress = PropertyInvoker.Get<Int64>(TeamOffsets.Players, OriginalBytes, MemoryAddress, DatabaseMode);
            Int64 i = 0;
            foreach (Int64 playerAddress in PlayersAddresses)
            {
                PropertyInvoker.Set<Int64>((i * 0x8), OriginalBytes, arrayStartAddress, DatabaseMode, playerAddress);
                i++;
            }
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
                return PropertyInvoker.Get<Int32>(TeamOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int UID
        {
            get
            {
                return PropertyInvoker.Get<Int32>(TeamOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private Int64 ClubPtr
        {
            get
            {
                return PropertyInvoker.Get<Int32>(TeamOffsets.Club, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public Club Club
        {
            get
            {
                return PropertyInvoker.GetPointer<Club>(TeamOffsets.Club, OriginalBytes, MemoryAddress, DatabaseMode, Version);
            }
        }

        private short _previousReputation = 0;
        private short PreviousReputation
        {
            get
            {
                if (_previousReputation == 0)
                {
                    _previousReputation = PropertyInvoker.Get<Int16>(TeamOffsets.PreviousReputation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _previousReputation;
            }
            set
            {
                if (_previousReputation != value)
                {
                    _previousReputation = value;
                    isDirty = true;
                }
            }
        }

        private byte _teamType = 0;
        public byte TeamType
        {
            get
            {
                if (_teamType == 0)
                {
                    _teamType = PropertyInvoker.Get<byte>(TeamOffsets.TeamType, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _teamType;
            }
            set
            {
                if (_teamType != value)
                {
                    _teamType = value;
                    isDirty = true;
                }
            }
        }

        private ushort _reputation = 0;
        public ushort Reputation
        {
            get
            {
                if (_reputation == 0)
                {
                    int rotateAmount = (int)((MemoryAddress + TeamOffsets.Reputation) & 0xf);
                    uint encryptedRep = PropertyInvoker.Get<ushort>(TeamOffsets.Reputation, OriginalBytes, MemoryAddress, DatabaseMode);
                    // Decrypt here

                    _reputation = (ushort)encryptedRep;
                }
                return _reputation;
            }
            set
            {
                if (_reputation != value)
                {
                    _reputation = value;
                    isDirty = true;
                }
            }
        }

        private List<Int64> _playersAddresses = new List<Int64>();
        public List<Int64> PlayersAddresses
        {
            get
            {
                if (_playersAddresses.Count == 0)
                {
                    List<Int64> result = new List<Int64>();
                    Int64 playerCount = ProcessManager.ReadArrayLength((MemoryAddress + TeamOffsets.Players));
                    if (playerCount > 0)
                    {
                        Int64 arrayStartAddress = PropertyInvoker.Get<Int64>(TeamOffsets.Players, OriginalBytes, MemoryAddress, DatabaseMode);
                        for (int i = 0; i < playerCount; i++)
                        {
                            Int64 playerAddress = PropertyInvoker.Get<Int64>((i * 0x8), OriginalBytes, arrayStartAddress, DatabaseMode);
                            result.Add(playerAddress);
                        }
                    }

                    _playersAddresses = result;
                }

                return _playersAddresses;
            }
            set
            {
                if (_playersAddresses != value)
                {
                    isDirty = true;
                    _playersAddresses = value;
                    _players.Clear();
                }
            }
        }

        private List<Player> _players = new List<Player>();
        public List<Player> Players
        {
            get
            {
                if (_players.Count == 0)
                {
                    List<Player> result = new List<Player>();
                    Int64 playerCount = ProcessManager.ReadArrayLength((MemoryAddress + TeamOffsets.Players));
                    if (playerCount > 0)
                    {
                        foreach (Int64 pAddr in PlayersAddresses)
                        {
                            // Before adding, make sure we support the player type!
                            Int64 personType = ProcessManager.ReadInt64(pAddr + Math.Abs(Version.PersonOffsets.Player)) - ProcessManager.fmProcess.BaseAddress;
                            if (personType == Version.PersonEnum.Player)
                            {
                                result.Add(new Player(pAddr, Version));
                            }
                        }
                    }
                    _players = result;
                }

                return _players;
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{0} ({1})", this.Club.Name, ((TeamType)this.TeamType).GetDescription());
            }
        }

        public override string ToString()
        {
            if (this.Club.Name != "-")
                return string.Format("{0} ({1})", this.Club.Name, ((TeamType)this.TeamType).GetDescription());
            else
                return "-";
        }

        public void SwapPlayerAddresses(Int64 oldAddress, Int64 newAddress)
        {
            // Find the oldAddress in the team array
            // In 2020 there's 0x8 bytes difference between the pointer and the actual person address
            List<Int64> newPlayerArray = new List<Int64>();
            for (int i = 0; i < PlayersAddresses.Count; i++)
            {
                if (PlayersAddresses[i] == oldAddress)
                {
                    PlayersAddresses[i] = newAddress;
                }
                newPlayerArray.Add(PlayersAddresses[i]);
            }

            PlayersAddresses = newPlayerArray;
        }
    }
}