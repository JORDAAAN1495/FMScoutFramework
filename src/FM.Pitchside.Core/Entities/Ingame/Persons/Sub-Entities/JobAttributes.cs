using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class JobAttributes : BaseObject, IJobAttributes
    {

        private JobAttributesOffsets JobAttributesOffsets;

        public JobAttributes(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            this.JobAttributesOffsets = new JobAttributesOffsets(version);
        }
        public JobAttributes(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            this.JobAttributesOffsets = new JobAttributesOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<byte>(JobAttributesOffsets.Manager, OriginalBytes, MemoryAddress, DatabaseMode, Manager);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.AssistantManager, OriginalBytes, MemoryAddress, DatabaseMode, AssistantManager);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.Coach, OriginalBytes, MemoryAddress, DatabaseMode, Coach);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.Physio, OriginalBytes, MemoryAddress, DatabaseMode, Physio);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.Scout, OriginalBytes, MemoryAddress, DatabaseMode, Scout);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.GoalkeeperCoach, OriginalBytes, MemoryAddress, DatabaseMode, GoalkeeperCoach);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.FitnessCoach, OriginalBytes, MemoryAddress, DatabaseMode, FitnessCoach);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.Chairman, OriginalBytes, MemoryAddress, DatabaseMode, Chairman);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.DirectorOfFootball, OriginalBytes, MemoryAddress, DatabaseMode, DirectorOfFootball);
            PropertyInvoker.Set<byte>(JobAttributesOffsets.HeadOfYouthDevelopment, OriginalBytes, MemoryAddress, DatabaseMode, HeadOfYouthDevelopment);

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

        private byte _manager = 0;
        public byte Manager
        {
            get
            {
                if (_manager == 0)
                {
                    _manager = PropertyInvoker.Get<byte>(JobAttributesOffsets.Manager, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _manager;
            }
            set
            {
                if (_manager != value)
                {
                    isDirty = true;
                    _manager = value;
                }
            }
        }

        private byte _assistantManager = 0;
        public byte AssistantManager
        {
            get
            {
                if (_assistantManager == 0)
                {
                    _assistantManager = PropertyInvoker.Get<byte>(JobAttributesOffsets.AssistantManager, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _assistantManager;
            }
            set
            {
                if (_assistantManager != value)
                {
                    isDirty = true;
                    _assistantManager = value;
                }
            }
        }

        private byte _coach = 0;
        public byte Coach
        {
            get
            {
                if (_coach == 0)
                {
                    _coach = PropertyInvoker.Get<byte>(JobAttributesOffsets.Coach, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coach;
            }
            set
            {
                if (_coach != value)
                {
                    isDirty = true;
                    _coach = value;
                }
            }
        }

        private byte _physio = 0;
        public byte Physio
        {
            get
            {
                if (_physio == 0)
                {
                    _physio = PropertyInvoker.Get<byte>(JobAttributesOffsets.Physio, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _physio;
            }
            set
            {
                if (_physio != value)
                {
                    isDirty = true;
                    _physio = value;
                }
            }
        }

        private byte _scout = 0;
        public byte Scout
        {
            get
            {
                if (_scout == 0)
                {
                    _scout = PropertyInvoker.Get<byte>(JobAttributesOffsets.Scout, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _scout;
            }
            set
            {
                if (_scout != value)
                {
                    isDirty = true;
                    _scout = value;
                }
            }
        }

        private byte _goalkeeperCoach = 0;
        public byte GoalkeeperCoach
        {
            get
            {
                if (_goalkeeperCoach == 0)
                {
                    _goalkeeperCoach = PropertyInvoker.Get<byte>(JobAttributesOffsets.GoalkeeperCoach, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _goalkeeperCoach;
            }
            set
            {
                if (_goalkeeperCoach != value)
                {
                    isDirty = true;
                    _goalkeeperCoach = value;
                }
            }
        }

        private byte _fitnessCoach = 0;
        public byte FitnessCoach
        {
            get
            {
                if (_fitnessCoach == 0)
                {
                    _fitnessCoach = PropertyInvoker.Get<byte>(JobAttributesOffsets.FitnessCoach, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _fitnessCoach;
            }
            set
            {
                if (_fitnessCoach != value)
                {
                    isDirty = true;
                    _fitnessCoach = value;
                }
            }
        }

        private byte _chairman = 0;
        public byte Chairman
        {
            get
            {
                if (_chairman == 0)
                {
                    _chairman = PropertyInvoker.Get<byte>(JobAttributesOffsets.Chairman, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _chairman;
            }
            set
            {
                if (_chairman != value)
                {
                    isDirty = true;
                    _chairman = value;
                }
            }
        }

        private byte _directorOfFootball = 0;
        public byte DirectorOfFootball
        {
            get
            {
                if (_directorOfFootball == 0)
                {
                    _directorOfFootball = PropertyInvoker.Get<byte>(JobAttributesOffsets.DirectorOfFootball, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _directorOfFootball;
            }
            set
            {
                if (_directorOfFootball != value)
                {
                    isDirty = true;
                    _directorOfFootball = value;
                }
            }
        }

        private byte _headOfYouthDevelopment = 0;
        public byte HeadOfYouthDevelopment
        {
            get
            {
                if (_headOfYouthDevelopment == 0)
                {
                    _headOfYouthDevelopment = PropertyInvoker.Get<byte>(JobAttributesOffsets.HeadOfYouthDevelopment, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _headOfYouthDevelopment;
            }
            set
            {
                if (_headOfYouthDevelopment != value)
                {
                    isDirty = true;
                    _headOfYouthDevelopment = value;
                }
            }
        }
    }
}