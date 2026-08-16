using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class StaffAttributes : BaseObject, IStaffAttributes
    {

        private StaffAttributeOffsets StaffAttributeOffsets;

        public StaffAttributes(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            StaffAttributeOffsets = new StaffAttributeOffsets(version);
        }

        public StaffAttributes(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            StaffAttributeOffsets = new StaffAttributeOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Attacking, OriginalBytes, MemoryAddress, DatabaseMode, Attacking);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Business, OriginalBytes, MemoryAddress, DatabaseMode, Business);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Adaptability, OriginalBytes, MemoryAddress, DatabaseMode, Adaptability);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Directness, OriginalBytes, MemoryAddress, DatabaseMode, Directness);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.LevelOfDiscipline, OriginalBytes, MemoryAddress, DatabaseMode, LevelOfDiscipline);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.FreeRoles, OriginalBytes, MemoryAddress, DatabaseMode, FreeRoles);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Interference, OriginalBytes, MemoryAddress, DatabaseMode, Interference);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode, Marking);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Offside, OriginalBytes, MemoryAddress, DatabaseMode, Offside);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Patience, OriginalBytes, MemoryAddress, DatabaseMode, Patience);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Pressing, OriginalBytes, MemoryAddress, DatabaseMode, Pressing);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Resources, OriginalBytes, MemoryAddress, DatabaseMode, Resources);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.WorkingWithYoungsters, OriginalBytes, MemoryAddress, DatabaseMode, WorkingWithYoungsters);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode, Determination);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.BuyingPlayers, OriginalBytes, MemoryAddress, DatabaseMode, BuyingPlayers);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.MindGames, OriginalBytes, MemoryAddress, DatabaseMode, MindGames);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.SittingBack, OriginalBytes, MemoryAddress, DatabaseMode, SittingBack);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.UseOfPlaymaker, OriginalBytes, MemoryAddress, DatabaseMode, UseOfPlaymaker);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.UseOfSubstitutions, OriginalBytes, MemoryAddress, DatabaseMode, UseOfSubstitutions);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Depth, OriginalBytes, MemoryAddress, DatabaseMode, Depth);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Flamboyancy, OriginalBytes, MemoryAddress, DatabaseMode, Flamboyancy);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Flexibility, OriginalBytes, MemoryAddress, DatabaseMode, Flexibility);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.HardnessOfTraining, OriginalBytes, MemoryAddress, DatabaseMode, HardnessOfTraining);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.SquadRotation, OriginalBytes, MemoryAddress, DatabaseMode, SquadRotation);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Tempo, OriginalBytes, MemoryAddress, DatabaseMode, Tempo);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Width, OriginalBytes, MemoryAddress, DatabaseMode, Width);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.CoachingPlayers, OriginalBytes, MemoryAddress, DatabaseMode, CoachingPlayers);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.CoachingGoalKeepers, OriginalBytes, MemoryAddress, DatabaseMode, CoachingGoalKeepers);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.JudgingPlayerAbility, OriginalBytes, MemoryAddress, DatabaseMode, JudgingPlayerAbility);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.JudgingPlayerPotential, OriginalBytes, MemoryAddress, DatabaseMode, JudgingPlayerPotential);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.ManManagement, OriginalBytes, MemoryAddress, DatabaseMode, ManManagement);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Motivating, OriginalBytes, MemoryAddress, DatabaseMode, Motivating);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Physiotherapy, OriginalBytes, MemoryAddress, DatabaseMode, Physiotherapy);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.TacticalKnowledge, OriginalBytes, MemoryAddress, DatabaseMode, TacticalKnowledge);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.CoachingAttacking, OriginalBytes, MemoryAddress, DatabaseMode, CoachingAttacking);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.CoachingDefending, OriginalBytes, MemoryAddress, DatabaseMode, CoachingDefending);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.CoachingFitness, OriginalBytes, MemoryAddress, DatabaseMode, CoachingFitness);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Mental, OriginalBytes, MemoryAddress, DatabaseMode, Mental);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Technical, OriginalBytes, MemoryAddress, DatabaseMode, Technical);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Tactical, OriginalBytes, MemoryAddress, DatabaseMode, Tactical);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.DirtinessAllowance, OriginalBytes, MemoryAddress, DatabaseMode, DirtinessAllowance);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.GoalkeeperHandling, OriginalBytes, MemoryAddress, DatabaseMode, GoalkeeperHandling);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.GoalkeeperDistribution, OriginalBytes, MemoryAddress, DatabaseMode, GoalkeeperDistribution);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode, Versatility);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.JudgingPlayerData, OriginalBytes, MemoryAddress, DatabaseMode, JudgingPlayerData);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.JudgingTeamData, OriginalBytes, MemoryAddress, DatabaseMode, JudgingTeamData);
            PropertyInvoker.Set<byte>(StaffAttributeOffsets.PresentingData, OriginalBytes, MemoryAddress, DatabaseMode, PresentingData);

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

        private byte _attacking = 0;
        public byte Attacking
        {
            get
            {
                if (_attacking == 0)
                {
                    _attacking = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Attacking, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _attacking;
            }
            set
            {
                if (_attacking != value)
                {
                    isDirty = true;
                    _attacking = value;
                }
            }
        }

        private byte _business = 0;
        public byte Business
        {
            get
            {
                if (_business == 0)
                {
                    _business = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Business, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _business;
            }
            set
            {
                if (_business != value)
                {
                    isDirty = true;
                    _business = value;
                }
            }
        }

        private byte _adaptability = 0;
        public byte Adaptability
        {
            get
            {
                if (_adaptability == 0)
                {
                    _adaptability = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Adaptability, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _adaptability;
            }
            set
            {
                if (_adaptability != value)
                {
                    isDirty = true;
                    _adaptability = value;
                }
            }
        }

        private byte _directness = 0;
        public byte Directness
        {
            get
            {
                if (_directness == 0)
                {
                    _directness = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Directness, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _directness;
            }
            set
            {
                if (_directness != value)
                {
                    isDirty = true;
                    _directness = value;
                }
            }
        }

        private byte _levelOfDiscipline = 0;
        public byte LevelOfDiscipline
        {
            get
            {
                if (_levelOfDiscipline == 0)
                {
                    _levelOfDiscipline = PropertyInvoker.Get<byte>(StaffAttributeOffsets.LevelOfDiscipline, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _levelOfDiscipline;
            }
            set
            {
                if (_levelOfDiscipline != value)
                {
                    isDirty = true;
                    _levelOfDiscipline = value;
                }
            }
        }

        private byte _freeRoles = 0;
        public byte FreeRoles
        {
            get
            {
                if (_freeRoles == 0)
                {
                    _freeRoles = PropertyInvoker.Get<byte>(StaffAttributeOffsets.FreeRoles, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _freeRoles;
            }
            set
            {
                if (_freeRoles != value)
                {
                    isDirty = true;
                    _freeRoles = value;
                }
            }
        }

        private byte _interference = 0;
        public byte Interference
        {
            get
            {
                if (_interference == 0)
                {
                    _interference = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Interference, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _interference;
            }
            set
            {
                if (_interference != value)
                {
                    isDirty = true;
                    _interference = value;
                }
            }
        }

        private byte _marking = 0;
        public byte Marking
        {
            get
            {
                if (_marking == 0)
                {
                    _marking = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _marking;
            }
            set
            {
                if (_marking != value)
                {
                    isDirty = true;
                    _marking = value;
                }
            }
        }

        private byte _offside = 0;
        public byte Offside
        {
            get
            {
                if (_offside == 0)
                {
                    _offside = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Offside, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _offside;
            }
            set
            {
                if (_offside != value)
                {
                    isDirty = true;
                    _offside = value;
                }
            }
        }

        private byte _patience = 0;
        public byte Patience
        {
            get
            {
                if (_patience == 0)
                {
                    _patience = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Patience, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _patience;
            }
            set
            {
                if (_patience != value)
                {
                    isDirty = true;
                    _patience = value;
                }
            }
        }

        private byte _pressing = 0;
        public byte Pressing
        {
            get
            {
                if (_pressing == 0)
                {
                    _pressing = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Pressing, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _pressing;
            }
            set
            {
                if (_pressing != value)
                {
                    isDirty = true;
                    _pressing = value;
                }
            }
        }

        private byte _resources = 0;
        public byte Resources
        {
            get
            {
                if (_resources == 0)
                {
                    _resources = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Resources, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _resources;
            }
            set
            {
                if (_resources != value)
                {
                    isDirty = true;
                    _resources = value;
                }
            }
        }

        private byte _workingWithYoungsters = 0;
        public byte WorkingWithYoungsters
        {
            get
            {
                if (_workingWithYoungsters == 0)
                {
                    _workingWithYoungsters = PropertyInvoker.Get<byte>(StaffAttributeOffsets.WorkingWithYoungsters, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _workingWithYoungsters;
            }
            set
            {
                if (_workingWithYoungsters != value)
                {
                    isDirty = true;
                    _workingWithYoungsters = value;
                }
            }
        }

        private byte _determination = 0;
        public byte Determination
        {
            get
            {
                if (_determination == 0)
                {
                    _determination = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _determination;
            }
            set
            {
                if (_determination != value)
                {
                    isDirty = true;
                    _determination = value;
                }
            }
        }

        private byte _buyingPlayers = 0;
        public byte BuyingPlayers
        {
            get
            {
                if (_buyingPlayers == 0)
                {
                    _buyingPlayers = PropertyInvoker.Get<byte>(StaffAttributeOffsets.BuyingPlayers, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _buyingPlayers;
            }
            set
            {
                if (_buyingPlayers != value)
                {
                    isDirty = true;
                    _buyingPlayers = value;
                }
            }
        }

        private byte _mindGames = 0;
        public byte MindGames
        {
            get
            {
                if (_mindGames == 0)
                {
                    _mindGames = PropertyInvoker.Get<byte>(StaffAttributeOffsets.MindGames, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _mindGames;
            }
            set
            {
                if (_mindGames != value)
                {
                    isDirty = true;
                    _mindGames = value;
                }
            }
        }

        private byte _sittingBack = 0;
        public byte SittingBack
        {
            get
            {
                if (_sittingBack == 0)
                {
                    _sittingBack = PropertyInvoker.Get<byte>(StaffAttributeOffsets.SittingBack, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _sittingBack;
            }
            set
            {
                if (_sittingBack != value)
                {
                    isDirty = true;
                    _sittingBack = value;
                }
            }
        }

        private byte _useOfPlaymaker = 0;
        public byte UseOfPlaymaker
        {
            get
            {
                if (_useOfPlaymaker == 0)
                {
                    _useOfPlaymaker = PropertyInvoker.Get<byte>(StaffAttributeOffsets.UseOfPlaymaker, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _useOfPlaymaker;
            }
            set
            {
                if (_useOfPlaymaker != value)
                {
                    isDirty = true;
                    _useOfPlaymaker = value;
                }
            }
        }

        private byte _useOfSubstitutions = 0;
        public byte UseOfSubstitutions
        {
            get
            {
                if (_useOfSubstitutions == 0)
                {
                    _useOfSubstitutions = PropertyInvoker.Get<byte>(StaffAttributeOffsets.UseOfSubstitutions, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _useOfSubstitutions;
            }
            set
            {
                if (_useOfSubstitutions != value)
                {
                    isDirty = true;
                    _useOfSubstitutions = value;
                }
            }
        }

        private byte _depth = 0;
        public byte Depth
        {
            get
            {
                if (_depth == 0)
                {
                    _depth = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Depth, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _depth;
            }
            set
            {
                if (_depth != value)
                {
                    isDirty = true;
                    _depth = value;
                }
            }
        }

        private byte _flamboyancy = 0;
        public byte Flamboyancy
        {
            get
            {
                if (_flamboyancy == 0)
                {
                    _flamboyancy = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Flamboyancy, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _flamboyancy;
            }
            set
            {
                if (_flamboyancy != value)
                {
                    isDirty = true;
                    _flamboyancy = value;
                }
            }
        }

        private byte _flexibility = 0;
        public byte Flexibility
        {
            get
            {
                if (_flexibility == 0)
                {
                    _flexibility = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Flexibility, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _flexibility;
            }
            set
            {
                if (_flexibility != value)
                {
                    isDirty = true;
                    _flexibility = value;
                }
            }
        }

        private byte _hardnessOfTraining = 0;
        public byte HardnessOfTraining
        {
            get
            {
                if (_hardnessOfTraining == 0)
                {
                    _hardnessOfTraining = PropertyInvoker.Get<byte>(StaffAttributeOffsets.HardnessOfTraining, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _hardnessOfTraining;
            }
            set
            {
                if (_hardnessOfTraining != value)
                {
                    isDirty = true;
                    _hardnessOfTraining = value;
                }
            }
        }

        private byte _squadRotation = 0;
        public byte SquadRotation
        {
            get
            {
                if (_squadRotation == 0)
                {
                    _squadRotation = PropertyInvoker.Get<byte>(StaffAttributeOffsets.SquadRotation, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _squadRotation;
            }
            set
            {
                if (_squadRotation != value)
                {
                    isDirty = true;
                    _squadRotation = value;
                }
            }
        }

        private byte _tempo = 0;
        public byte Tempo
        {
            get
            {
                if (_tempo == 0)
                {
                    _tempo = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Tempo, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _tempo;
            }
            set
            {
                if (_tempo != value)
                {
                    isDirty = true;
                    _tempo = value;
                }
            }
        }

        private byte _width = 0;
        public byte Width
        {
            get
            {
                if (_width == 0)
                {
                    _width = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Width, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _width;
            }
            set
            {
                if (_width != value)
                {
                    isDirty = true;
                    _width = value;
                }
            }
        }

        private byte _coachingPlayers = 0;
        public byte CoachingPlayers
        {
            get
            {
                if (_coachingPlayers == 0)
                {
                    _coachingPlayers = PropertyInvoker.Get<byte>(StaffAttributeOffsets.CoachingPlayers, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coachingPlayers;
            }
            set
            {
                if (_coachingPlayers != value)
                {
                    isDirty = true;
                    _coachingPlayers = value;
                }
            }
        }

        private byte _coachingGoalKeepers = 0;
        public byte CoachingGoalKeepers
        {
            get
            {
                if (_coachingGoalKeepers == 0)
                {
                    _coachingGoalKeepers = PropertyInvoker.Get<byte>(StaffAttributeOffsets.CoachingGoalKeepers, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coachingGoalKeepers;
            }
            set
            {
                if (_coachingGoalKeepers != value)
                {
                    isDirty = true;
                    _coachingGoalKeepers = value;
                }
            }
        }

        private byte _judgingPlayerAbility = 0;
        public byte JudgingPlayerAbility
        {
            get
            {
                if (_judgingPlayerAbility == 0)
                {
                    _judgingPlayerAbility = PropertyInvoker.Get<byte>(StaffAttributeOffsets.JudgingPlayerAbility, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _judgingPlayerAbility;
            }
            set
            {
                if (_judgingPlayerAbility != value)
                {
                    isDirty = true;
                    _judgingPlayerAbility = value;
                }
            }
        }

        private byte _judgingPlayerPotential = 0;
        public byte JudgingPlayerPotential
        {
            get
            {
                if (_judgingPlayerPotential == 0)
                {
                    _judgingPlayerPotential = PropertyInvoker.Get<byte>(StaffAttributeOffsets.JudgingPlayerPotential, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _judgingPlayerPotential;
            }
            set
            {
                if (_judgingPlayerPotential != value)
                {
                    isDirty = true;
                    _judgingPlayerPotential = value;
                }
            }
        }

        private byte _manManagement = 0;
        public byte ManManagement
        {
            get
            {
                if (_manManagement == 0)
                {
                    _manManagement = PropertyInvoker.Get<byte>(StaffAttributeOffsets.ManManagement, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _manManagement;
            }
            set
            {
                if (_manManagement != value)
                {
                    isDirty = true;
                    _manManagement = value;
                }
            }
        }

        private byte _motivating = 0;
        public byte Motivating
        {
            get
            {
                if (_motivating == 0)
                {
                    _motivating = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Motivating, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _motivating;
            }
            set
            {
                if (_motivating != value)
                {
                    isDirty = true;
                    _motivating = value;
                }
            }
        }

        private byte _physiotherapy = 0;
        public byte Physiotherapy
        {
            get
            {
                if (_physiotherapy == 0)
                {
                    _physiotherapy = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Physiotherapy, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _physiotherapy;
            }
            set
            {
                if (_physiotherapy != value)
                {
                    isDirty = true;
                    _physiotherapy = value;
                }
            }
        }

        private byte _tacticalKnowledge = 0;
        public byte TacticalKnowledge
        {
            get
            {
                if (_tacticalKnowledge == 0)
                {
                    _tacticalKnowledge = PropertyInvoker.Get<byte>(StaffAttributeOffsets.TacticalKnowledge, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _tacticalKnowledge;
            }
            set
            {
                if (_tacticalKnowledge != value)
                {
                    isDirty = true;
                    _tacticalKnowledge = value;
                }
            }
        }

        private byte _coachingAttacking = 0;
        public byte CoachingAttacking
        {
            get
            {
                if (_coachingAttacking == 0)
                {
                    _coachingAttacking = PropertyInvoker.Get<byte>(StaffAttributeOffsets.CoachingAttacking, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coachingAttacking;
            }
            set
            {
                if (_coachingAttacking != value)
                {
                    isDirty = true;
                    _coachingAttacking = value;
                }
            }
        }

        private byte _coachingDefending = 0;
        public byte CoachingDefending
        {
            get
            {
                if (_coachingDefending == 0)
                {
                    _coachingDefending = PropertyInvoker.Get<byte>(StaffAttributeOffsets.CoachingDefending, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coachingDefending;
            }
            set
            {
                if (_coachingDefending != value)
                {
                    isDirty = true;
                    _coachingDefending = value;
                }
            }
        }

        private byte _coachingFitness = 0;
        public byte CoachingFitness
        {
            get
            {
                if (_coachingFitness == 0)
                {
                    _coachingFitness = PropertyInvoker.Get<byte>(StaffAttributeOffsets.CoachingFitness, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _coachingFitness;
            }
            set
            {
                if (_coachingFitness != value)
                {
                    isDirty = true;
                    _coachingFitness = value;
                }
            }
        }

        private byte _mental = 0;
        public byte Mental
        {
            get
            {
                if (_mental == 0)
                {
                    _mental = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Mental, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _mental;
            }
            set
            {
                if (_mental != value)
                {
                    isDirty = true;
                    _mental = value;
                }
            }
        }

        private byte _technical = 0;
        public byte Technical
        {
            get
            {
                if (_technical == 0)
                {
                    _technical = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Technical, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _technical;
            }
            set
            {
                if (_technical != value)
                {
                    isDirty = true;
                    _technical = value;
                }
            }
        }

        private byte _tactical = 0;
        public byte Tactical
        {
            get
            {
                if (_tactical == 0)
                {
                    _tactical = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Tactical, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _tactical;
            }
            set
            {
                if (_tactical != value)
                {
                    isDirty = true;
                    _tactical = value;
                }
            }
        }

        private byte _dirtinessAllowance = 0;
        public byte DirtinessAllowance
        {
            get
            {
                if (_dirtinessAllowance == 0)
                {
                    _dirtinessAllowance = PropertyInvoker.Get<byte>(StaffAttributeOffsets.DirtinessAllowance, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _dirtinessAllowance;
            }
            set
            {
                if (_dirtinessAllowance != value)
                {
                    isDirty = true;
                    _dirtinessAllowance = value;
                }
            }
        }

        private byte _goalkeeperHandling = 0;
        public byte GoalkeeperHandling
        {
            get
            {
                if (_goalkeeperHandling == 0)
                {
                    _goalkeeperHandling = PropertyInvoker.Get<byte>(StaffAttributeOffsets.GoalkeeperHandling, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _goalkeeperHandling;
            }
            set
            {
                if (_goalkeeperHandling != value)
                {
                    isDirty = true;
                    _goalkeeperHandling = value;
                }
            }
        }

        private byte _goalkeeperDistribution = 0;
        public byte GoalkeeperDistribution
        {
            get
            {
                if (_goalkeeperDistribution == 0)
                {
                    _goalkeeperDistribution = PropertyInvoker.Get<byte>(StaffAttributeOffsets.GoalkeeperDistribution, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _goalkeeperDistribution;
            }
            set
            {
                if (_goalkeeperDistribution != value)
                {
                    isDirty = true;
                    _goalkeeperDistribution = value;
                }
            }
        }

        private byte _versatility = 0;
        public byte Versatility
        {
            get
            {
                if (_versatility == 0)
                {
                    _versatility = PropertyInvoker.Get<byte>(StaffAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _versatility;
            }
            set
            {
                if (_versatility != value)
                {
                    isDirty = true;
                    _versatility = value;
                }
            }
        }

        private byte _judgingPlayerData = 0;
        public byte JudgingPlayerData
        {
            get
            {
                if (_judgingPlayerData == 0)
                {
                    _judgingPlayerData = PropertyInvoker.Get<byte>(StaffAttributeOffsets.JudgingPlayerData, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _judgingPlayerData;
            }
            set
            {
                if (_judgingPlayerData != value)
                {
                    isDirty = true;
                    _judgingPlayerData = value;
                }
            }
        }

        private byte _judgingTeamData = 0;
        public byte JudgingTeamData
        {
            get
            {
                if (_judgingTeamData == 0)
                {
                    _judgingTeamData = PropertyInvoker.Get<byte>(StaffAttributeOffsets.JudgingTeamData, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _judgingTeamData;
            }
            set
            {
                if (_judgingTeamData != value)
                {
                    isDirty = true;
                    _judgingTeamData = value;
                }
            }
        }

        private byte _presentingData = 0;
        public byte PresentingData
        {
            get
            {
                if (_presentingData == 0)
                {
                    _presentingData = PropertyInvoker.Get<byte>(StaffAttributeOffsets.PresentingData, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _presentingData;
            }
            set
            {
                if (_presentingData != value)
                {
                    isDirty = true;
                    _presentingData = value;
                }
            }
        }
    }
}