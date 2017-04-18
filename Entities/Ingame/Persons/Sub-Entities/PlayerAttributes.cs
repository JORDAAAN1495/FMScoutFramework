using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class PlayerAttributes : BaseObject, IPlayerAttributes
    {
        public PlayerAttributes (Int64 memoryAddress, IVersion version)
            : base (memoryAddress, version)
        { }
        public PlayerAttributes (Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        { }

        public void Save() {
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.GoalKeeper, OriginalBytes, MemoryAddress, DatabaseMode, Goalkeeper);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Sweeper, OriginalBytes, MemoryAddress, DatabaseMode, Sweeper);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.DefenderLeft, OriginalBytes, MemoryAddress, DatabaseMode, DefenderLeft);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.DefenderCenter, OriginalBytes, MemoryAddress, DatabaseMode, DefenderCenter);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.DefenderRight, OriginalBytes, MemoryAddress, DatabaseMode, DefenderRight);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.DefensiveMidfielder, OriginalBytes, MemoryAddress, DatabaseMode, DefensiveMidfielder);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.MidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode, MidfielderLeft);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.MidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode, MidfielderCenter);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.MidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode, MidfielderRight);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.AttackingMidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode, AttackingMidfielderLeft);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.AttackingMidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode, AttackingMidfielderCenter);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.AttackingMidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode, AttackingMidfielderRight);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Striker, OriginalBytes, MemoryAddress, DatabaseMode, Striker);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.WingbackLeft, OriginalBytes, MemoryAddress, DatabaseMode, WingbackLeft);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.WingbackRight, OriginalBytes, MemoryAddress, DatabaseMode, WingbackRight);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Crossing, OriginalBytes, MemoryAddress, DatabaseMode, Crossing);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Dribbling, OriginalBytes, MemoryAddress, DatabaseMode, Dribbling);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Finishing, OriginalBytes, MemoryAddress, DatabaseMode, Finishing);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Heading, OriginalBytes, MemoryAddress, DatabaseMode, Heading);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.LongShots, OriginalBytes, MemoryAddress, DatabaseMode, LongShots);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode, Marking);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.OffTheBall, OriginalBytes, MemoryAddress, DatabaseMode, OffTheBall);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Passing, OriginalBytes, MemoryAddress, DatabaseMode, Passing);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Penalties, OriginalBytes, MemoryAddress, DatabaseMode, Penalties);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Tackling, OriginalBytes, MemoryAddress, DatabaseMode, Tackling);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Creativity, OriginalBytes, MemoryAddress, DatabaseMode, Creativity);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Handling, OriginalBytes, MemoryAddress, DatabaseMode, Handling);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.AerialAbility, OriginalBytes, MemoryAddress, DatabaseMode, AerialAbility);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.CommandOfArea, OriginalBytes, MemoryAddress, DatabaseMode, CommandOfArea);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Communication, OriginalBytes, MemoryAddress, DatabaseMode, Communication);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Kicking, OriginalBytes, MemoryAddress, DatabaseMode, Kicking);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Throwing, OriginalBytes, MemoryAddress, DatabaseMode, Throwing);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Anticipation, OriginalBytes, MemoryAddress, DatabaseMode, Anticipation);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Decisions, OriginalBytes, MemoryAddress, DatabaseMode, Decisions);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.OneOnOnes, OriginalBytes, MemoryAddress, DatabaseMode, OneOnOnes);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Positioning, OriginalBytes, MemoryAddress, DatabaseMode, Positioning);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Reflexes, OriginalBytes, MemoryAddress, DatabaseMode, Reflexes);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.FirstTouch, OriginalBytes, MemoryAddress, DatabaseMode, FirstTouch);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Technique, OriginalBytes, MemoryAddress, DatabaseMode, Technique);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.LeftFoot, OriginalBytes, MemoryAddress, DatabaseMode, LeftFoot);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.RightFoot, OriginalBytes, MemoryAddress, DatabaseMode, RightFoot);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Flair, OriginalBytes, MemoryAddress, DatabaseMode, Flair);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Corners, OriginalBytes, MemoryAddress, DatabaseMode, Corners);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Teamwork, OriginalBytes, MemoryAddress, DatabaseMode, Teamwork);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.WorkRate, OriginalBytes, MemoryAddress, DatabaseMode, Workrate);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.LongThrows, OriginalBytes, MemoryAddress, DatabaseMode, LongThrows);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Eccentricity, OriginalBytes, MemoryAddress, DatabaseMode, Eccentricity);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.RushingOut, OriginalBytes, MemoryAddress, DatabaseMode, RushingOut);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.TendencyToPunch, OriginalBytes, MemoryAddress, DatabaseMode, TendencyToPunch);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Acceleration, OriginalBytes, MemoryAddress, DatabaseMode, Acceleration);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.FreekickTaking, OriginalBytes, MemoryAddress, DatabaseMode, FreekickTaking);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Strength, OriginalBytes, MemoryAddress, DatabaseMode, Strength);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Stamina, OriginalBytes, MemoryAddress, DatabaseMode, Stamina);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Pace, OriginalBytes, MemoryAddress, DatabaseMode, Pace);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Jumping, OriginalBytes, MemoryAddress, DatabaseMode, Jumping);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Influence, OriginalBytes, MemoryAddress, DatabaseMode, Influence);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Dirtiness, OriginalBytes, MemoryAddress, DatabaseMode, Dirtiness);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode, Balance);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Bravery, OriginalBytes, MemoryAddress, DatabaseMode, Bravery);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Consistency, OriginalBytes, MemoryAddress, DatabaseMode, Consistency);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Aggression, OriginalBytes, MemoryAddress, DatabaseMode, Aggression);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Agility, OriginalBytes, MemoryAddress, DatabaseMode, Agility);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.ImportantMatches, OriginalBytes, MemoryAddress, DatabaseMode, ImportantMatches);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.InjuryProneness, OriginalBytes, MemoryAddress, DatabaseMode, InjuryProneness);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode, Versatility);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.NaturalFitness, OriginalBytes, MemoryAddress, DatabaseMode, NaturalFitness);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode, Determination);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Composure, OriginalBytes, MemoryAddress, DatabaseMode, Composure);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Concentration, OriginalBytes, MemoryAddress, DatabaseMode, Concentration);
            PropertyInvoker.Set<byte>(PlayerAttributeOffsets.Sweeper, OriginalBytes, MemoryAddress, DatabaseMode, Sweeper);

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

        private byte _goalkeeper = 0;
        public byte Goalkeeper {
            get {
                if (_goalkeeper == 0) {
                    _goalkeeper = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.GoalKeeper, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _goalkeeper;
            }
            set {
                if (_goalkeeper != value) {
                    isDirty = true;
                    _goalkeeper = value;
                }
            }
        }

        private byte _sweeper = 0;
        public byte Sweeper {
            get {
                if (_sweeper == 0) {
                    _sweeper = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Sweeper, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _sweeper;
            }
            set {
                if (_sweeper != value) {
                    isDirty = true;
                    _sweeper = value;
                }
            }
        }

        private byte _defenderLeft = 0;
        public byte DefenderLeft {
            get {
                if (_defenderLeft == 0) {
                    _defenderLeft = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.DefenderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _defenderLeft;
            }
            set {
                if (_defenderLeft != value) {
                    isDirty = true;
                    _defenderLeft = value;
                }
            }
        }

        private byte _defenderCenter = 0;
        public byte DefenderCenter {
            get {
                if (_defenderCenter == 0) {
                    _defenderCenter = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.DefenderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _defenderCenter;
            }
            set {
                if (_defenderCenter != value) {
                    isDirty = true;
                    _defenderCenter = value;
                }
            }
        }

        private byte _defenderRight = 0;
        public byte DefenderRight {
            get {
                if (_defenderRight == 0) {
                    _defenderRight = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.DefenderRight, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _defenderRight;
            }
            set {
                if (_defenderRight != value) {
                    isDirty = true;
                    _defenderRight = value;
                }
            }
        }

        private byte _defensiveMidfielder = 0;
        public byte DefensiveMidfielder {
            get {
                if (_defensiveMidfielder == 0) {
                    _defensiveMidfielder = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.DefensiveMidfielder, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _defensiveMidfielder;
            }
            set {
                if (_defensiveMidfielder != value) {
                    isDirty = true;
                    _defensiveMidfielder = value;
                }
            }
        }

        private byte _midfielderLeft = 0;
        public byte MidfielderLeft {
            get {
                if (_midfielderLeft == 0) {
                    _midfielderLeft = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.MidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _midfielderLeft;
            }
            set {
                if (_midfielderLeft != value) {
                    isDirty = true;
                    _midfielderLeft = value;
                }
            }
        }

        private byte _midfielderCenter = 0;
        public byte MidfielderCenter {
            get {
                if (_midfielderCenter == 0) {
                    _midfielderCenter = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.MidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _midfielderCenter;
            }
            set {
                if (_midfielderCenter != value) {
                    isDirty = true;
                    _midfielderCenter = value;
                }
            }
        }

        private byte _midfielderRight = 0;
        public byte MidfielderRight {
            get {
                if (_midfielderRight == 0) {
                    _midfielderRight = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.MidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _midfielderRight;
            }
            set {
                if (_midfielderRight != value) {
                    isDirty = true;
                    _midfielderRight = value;
                }
            }
        }

        private byte _attackingMidfielderLeft = 0;
        public byte AttackingMidfielderLeft {
            get {
                if (_attackingMidfielderLeft == 0) {
                    _attackingMidfielderLeft = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.AttackingMidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _attackingMidfielderLeft;
            }
            set {
                if (_attackingMidfielderLeft != value) {
                    isDirty = true;
                    _attackingMidfielderLeft = value;
                }
            }
        }

        private byte _attackingMidfielderCenter = 0;
        public byte AttackingMidfielderCenter {
            get {
                if (_attackingMidfielderCenter == 0) {
                    _attackingMidfielderCenter = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.AttackingMidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _attackingMidfielderCenter;
            }
            set {
                if (_attackingMidfielderCenter != value) {
                    isDirty = true;
                    _attackingMidfielderCenter = value;
                }
            }
        }

        private byte _attackingMidfielderRight = 0;
        public byte AttackingMidfielderRight {
            get {
                if (_attackingMidfielderRight == 0) {
                    _attackingMidfielderRight = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.AttackingMidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _attackingMidfielderRight;
            }
            set {
                if (_attackingMidfielderRight != value) {
                    isDirty = true;
                    _attackingMidfielderRight = value;
                }
            }
        }

        private byte _striker = 0;
        public byte Striker {
            get {
                if (_striker == 0) {
                    _striker = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Striker, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _striker;
            }
            set {
                if (_striker != value) {
                    isDirty = true;
                    _striker = value;
                }
            }
        }

        private byte _wingbackLeft = 0;
        public byte WingbackLeft {
            get {
                if (_wingbackLeft == 0) {
                    _wingbackLeft = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.WingbackLeft, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _wingbackLeft;
            }
            set {
                if (_wingbackLeft != value) {
                    isDirty = true;
                    _wingbackLeft = value;
                }
            }
        }

        private byte _wingbackRight = 0;
        public byte WingbackRight {
            get {
                if (_wingbackRight == 0) {
                    _wingbackRight = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.WingbackRight, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _wingbackRight;
            }
            set {
                isDirty = true;
                _wingbackRight = value;
            }
        }

        public string Position {
            get {
                string final = "";
                if (Goalkeeper > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "GK";
                }
                if (Sweeper > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "SW";
                }
                if (DefenderLeft > 15 || DefenderCenter > 15 || DefenderRight > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "D (";
                    if (DefenderRight > 15)
                        final += "R";
                    if (DefenderLeft > 15)
                        final += "L";
                    if (DefenderCenter > 15)
                        final += "C";
                    final += ")";
                }
                if (WingbackLeft > 15 || WingbackRight > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "WB (";
                    if (WingbackRight > 15)
                        final += "R";
                    if (WingbackLeft > 15)
                        final += "L";
                    final += ")";
                }
                if (DefensiveMidfielder > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "DM";
                }
                if (AttackingMidfielderLeft > 15 || AttackingMidfielderCenter > 15 || AttackingMidfielderRight > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "AM (";
                    if (AttackingMidfielderLeft > 15)
                        final += "L";
                    if (AttackingMidfielderRight > 15)
                        final += "R";
                    if (AttackingMidfielderCenter > 15)
                        final += "C";
                    final += ")";
                }
                if (MidfielderLeft > 15 || MidfielderCenter > 15 || MidfielderRight > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "M (";
                    if (MidfielderRight > 15)
                        final += "R";
                    if (MidfielderLeft > 15)
                        final += "L";
                    if (MidfielderCenter > 15)
                        final += "C";
                    final += ")";
                }
                if (Striker > 15) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "ST";
                }

                return final;
            }
        }

        private byte _crossing = 0;
        public byte Crossing {
            get {
                if (_crossing == 0) {
                    _crossing = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Crossing, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _crossing;
            }
            set {
                if (_crossing != value) {
                    isDirty = true;
                    _crossing = value;
                }
            }
        }

        private byte _dribbling = 0;
        public byte Dribbling {
            get {
                if (_dribbling == 0) {
                    _dribbling = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Dribbling, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _dribbling;
            }
            set {
                if (_dribbling != value) {
                    isDirty = true;
                    _dribbling = value;
                }
            }
        }

        private byte _finishing = 0;
        public byte Finishing {
            get {
                if (_finishing == 0) {
                    _finishing = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Finishing, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _finishing;
            }
            set {
                if (_finishing != value) {
                    isDirty = true;
                    _finishing = value;
                }
            }
        }

        private byte _heading = 0;
        public byte Heading {
            get {
                if (_heading == 0) {
                    _heading = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Heading, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _heading;
            }
            set {
                if (_heading != value) {
                    isDirty = true;
                    _heading = value;
                }
            }
        }

        private byte _longShots = 0;
        public byte LongShots {
            get {
                if (_longShots == 0) {
                    _longShots = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.LongShots, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _longShots;
            }
            set {
                if (_longShots != value) {
                    isDirty = true;
                    _longShots = value;
                }
            }
        }

        private byte _marking = 0;
        public byte Marking {
            get {
                if (_marking == 0) {
                    _marking = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _marking;
            }
            set {
                if (_marking != value) {
                    isDirty = true;
                    _marking = value;
                }
            }
        }

        private byte _offTheBall = 0;
        public byte OffTheBall {
            get {
                if (_offTheBall == 0) {
                    _offTheBall = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.OffTheBall, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _offTheBall;
            }
            set {
                if (_offTheBall != value) {
                    isDirty = true;
                    _offTheBall = value;
                }
            }
        }

        private byte _passing = 0;
        public byte Passing {
            get {
                if (_passing == 0) {
                    _passing = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Passing, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _passing;
            }
            set {
                if (_passing != value) {
                    isDirty = true;
                    _passing = value;
                }
            }
        }

        private byte _penalties = 0;
        public byte Penalties {
            get {
                if (_penalties == 0) {
                    _penalties = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Penalties, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _penalties;
            }
            set {
                if (_penalties != value) {
                    isDirty = true;
                    _penalties = value;
                }
            }
        }

        private byte _tackling = 0;
        public byte Tackling {
            get {
                if (_tackling == 0) {
                    _tackling = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Tackling, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _tackling;
            }
            set {
                if (_tackling != value) {
                    isDirty = true;
                    _tackling = value;
                }
            }
        }

        private byte _creativity = 0;
        public byte Creativity {
            get {
                if (_creativity == 0) {
                    _creativity = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Creativity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _creativity;
            }
            set {
                if (_creativity != value) {
                    isDirty = true;
                    _creativity = value;
                }
            }
        }

        private byte _handling = 0;
        public byte Handling {
            get {
                if (_handling == 0) {
                    _handling = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Handling, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _handling;
            }
            set {
                if (_handling != value) {
                    isDirty = true;
                    _handling = value;
                }
            }
        }

        private byte _aerialAbility = 0;
        public byte AerialAbility {
            get {
                if (_aerialAbility == 0) {
                    _aerialAbility = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.AerialAbility, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _aerialAbility;
            }
            set {
                if (_aerialAbility != value) {
                    isDirty = true;
                    _aerialAbility = value;
                }
            }
        }

        private byte _commandOfArea = 0;
        public byte CommandOfArea {
            get {
                if (_commandOfArea == 0) {
                    _commandOfArea = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.CommandOfArea, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _commandOfArea;
            }
            set {
                if (_commandOfArea != value) {
                    isDirty = true;
                    _commandOfArea = value;
                }
            }
        }

        private byte _communication = 0;
        public byte Communication {
            get {
                if (_communication == 0) {
                    _communication = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Communication, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _communication;
            }
            set {
                if (_communication != value) {
                    isDirty = true;
                    _communication = value;
                }
            }
        }

        private byte _kicking = 0;
        public byte Kicking {
            get {
                if (_kicking == 0) {
                    _kicking = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Kicking, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _kicking;
            }
            set {
                if (_kicking != value) {
                    isDirty = true;
                    _kicking = value;
                }
            }
        }

        private byte _throwing = 0;
        public byte Throwing {
            get {
                if (_throwing == 0) {
                    _throwing = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Throwing, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _throwing;
            }
            set {
                if (_throwing != value) {
                    isDirty = true;
                    _throwing = value;
                }
            }
        }

        private byte _anticipation = 0;
        public byte Anticipation {
            get {
                if (_anticipation ==0) {
                    _anticipation = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Anticipation, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _anticipation;
            }
            set {
                if (_anticipation != value) {
                    isDirty = true;
                    _anticipation = value;
                }
            }
        }

        private byte _decisions = 0;
        public byte Decisions {
            get {
                if (_decisions == 0) {
                    _decisions = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Decisions, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _decisions;
            }
            set {
                if (_decisions != value) {
                    isDirty = true;
                    _decisions = value;
                }
            }
        }

        private byte _oneOnOnes = 0;
        public byte OneOnOnes {
            get {
                if (_oneOnOnes == 0) {
                    _oneOnOnes = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.OneOnOnes, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _oneOnOnes;
            }
            set {
                if (_oneOnOnes != value) {
                    isDirty = true;
                    _oneOnOnes = value;
                }
            }
        }

        private byte _positioning = 0;
        public byte Positioning {
            get {
                if (_positioning == 0) {
                    _positioning = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Positioning, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _positioning;
            }
            set {
                if (_positioning != value) {
                    isDirty = true;
                    _positioning = value;
                }
            }
        }

        private byte _reflexes = 0;
        public byte Reflexes {
            get {
                if (_reflexes == 0) {
                    _reflexes = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Reflexes, OriginalBytes, MemoryAddress, DatabaseMode);
                }

                return _reflexes;
            }
            set {
                if (_reflexes != value) {
                    isDirty = true;
                    _reflexes = value;
                }
            }
        }

        private byte _firstTouch = 0;
        public byte FirstTouch {
            get {
                if (_firstTouch == 0) {
                    _firstTouch = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.FirstTouch, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _firstTouch;
            }
            set {
                if (_firstTouch != value) {
                    isDirty = true;
                    _firstTouch = value;
                }
            }
        }

        private byte _technique = 0;
        public byte Technique {
            get {
                if (_technique == 0) {
                    _technique = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Technique, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _technique;
            }
            set {
                if (_technique != value) {
                    isDirty = true;
                    _technique = value;
                }
            }
        }

        private byte _leftFoot = 0;
        public byte LeftFoot {
            get {
                if (_leftFoot == 0) {
                    _leftFoot = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.LeftFoot, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _leftFoot;
            }
            set {



                if (_leftFoot != value) {
                    isDirty = true;
                    _leftFoot = value;
                }
            }
        }

        private byte _rightFoot = 0;
        public byte RightFoot {
            get {
                if (_rightFoot == 0) {
                    _rightFoot = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.RightFoot, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _rightFoot;
            }
            set {
                if (_rightFoot != value) {
                    isDirty = true;
                    _rightFoot = value;
                }
            }
        }

        private byte _flair = 0;
        public byte Flair {
            get {
                if (_flair == 0) {
                    _flair = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Flair, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _flair;
            }
            set {
                if (_flair != value) {
                    isDirty = true;
                    _flair = value;
                }
            }
        }

        private byte _corners = 0;
        public byte Corners {
            get {
                if (_corners == 0) {
                    _corners = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Corners, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _corners;
            }
            set {
                if (_corners != value) {
                    isDirty = true;
                    _corners = value;
                }
            }
        }

        private byte _teamwork = 0;
        public byte Teamwork {
            get {
               if (_teamwork == 0) {
                    _teamwork = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Teamwork, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _teamwork;
            }
            set {
                if (_teamwork != value) {
                    isDirty = true;
                    _teamwork = value;
                }
            }
        }

        private byte _workrate = 0;
        public byte Workrate {
            get {
                if (_workrate == 0) {
                    _workrate = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.WorkRate, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _workrate;
            }
            set {
                if (_workrate != value) {
                    isDirty = true;
                    _workrate = value;
                }
            }
        }

        private byte _longThrows = 0;
        public byte LongThrows {
            get {
                if (_longThrows == 0) {
                    _longThrows = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.LongThrows, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _longThrows;
            }
            set {
                if (_longThrows != value) {
                    isDirty = true;
                    _longThrows = value;
                }
            }
        }

        private byte _eccentricity = 0;
        public byte Eccentricity {
            get {
                if (_eccentricity == 0) {
                    _eccentricity = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Eccentricity, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _eccentricity;
            }
            set {
                if (_eccentricity != value) {
                    isDirty = true;
                    _eccentricity = value;
                }
            }
        }

        private byte _rushingOut = 0;
        public byte RushingOut {
            get {
                if (_rushingOut == 0) {
                    _rushingOut = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.RushingOut, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _rushingOut;
            }
            set {
                if (_rushingOut != value) {
                    isDirty = true;
                    _rushingOut = value;
                }
            }
        }

        private byte _tendencyToPunch = 0;
        public byte TendencyToPunch {
            get {
                if (_tendencyToPunch == 0) {
                    _tendencyToPunch = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.TendencyToPunch, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _tendencyToPunch;
            }
            set {
                if (_tendencyToPunch != value) {
                    isDirty = true;
                    _tendencyToPunch = value;
                }
            }
        }

        private byte _acceleration = 0;
        public byte Acceleration {
            get {
                if (_acceleration == 0) {
                    _acceleration = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Acceleration, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _acceleration;
            }
            set {
                if (_acceleration != value) {
                    isDirty = true;
                    _acceleration = value;
                }
            }
        }

        private byte _freekickTaking = 0;
        public byte FreekickTaking {
            get {
                if (_freekickTaking == 0) {
                    _freekickTaking = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.FreekickTaking, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _freekickTaking;
            }
            set {
                if (_freekickTaking != value) {
                    isDirty = true;
                    _freekickTaking = value;
                }
            }
        }

        private byte _strength = 0;
        public byte Strength {
            get {
                if (_strength == 0) {
                    _strength = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Strength, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _strength;
            }
            set {
                if (_strength != value) {
                    isDirty = true;
                    _strength = value;
                }
            }
        }

        private byte _stamina = 0;
        public byte Stamina {
            get {
                if (_stamina == 0) {
                    _stamina = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Stamina, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _stamina;
            }
            set {
                if (_stamina != value) {
                    isDirty = true;
                    _stamina = value;
                }
            }
        }

        private byte _pace = 0;
        public byte Pace {
            get {
                if (_pace == 0) {
                    _pace = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Pace, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _pace;
            }
            set {
                if (_pace != value) {
                    isDirty = true;
                    _pace = value;
                }
            }
        }

        private byte _jumping = 0;
        public byte Jumping {
            get {
                if (_jumping == 0) {
                    _jumping = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Jumping, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _jumping;
            }
            set {
                if (_jumping != value) {
                    isDirty = true;
                    _jumping = value;
                }
            }
        }

        private byte _influence = 0;
        public byte Influence {
            get {
                if (_influence == 0) {
                    _influence = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Influence, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _influence;
            }
            set {
                if (_influence != value) {
                    isDirty = true;
                    _influence = value;
                }
            }
        }

        private byte _dirtiness = 0;
        public byte Dirtiness {
            get {
                if (_dirtiness == 0) {
                    _dirtiness = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Dirtiness, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _dirtiness;
            }
            set {
                if (_dirtiness != value) {
                    isDirty = true;
                    _dirtiness = value;
                }
            }
        }

        private byte _balance = 0;
        public byte Balance {
            get {
                if (_balance == 0) {
                    _balance = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _balance;
            }
            set {
                if (_balance != value) {
                    isDirty = true;
                    _balance = value;
                }
            }
        }

        private byte _bravery = 0;
        public byte Bravery {
            get {
                if (_bravery == 0) {
                    _bravery = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Bravery, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _bravery;
            }
            set {
                if (_bravery != value) {
                    isDirty = true;
                    _bravery = value;
                }
            }
        }

        private byte _consistency = 0;
        public byte Consistency {
            get {
                if (_consistency == 0) {
                    _consistency = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Consistency, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _consistency;
            }
            set {
                if (_consistency != value) {
                    isDirty = true;
                    _consistency = value;
                }
            }
        }

        private byte _aggression = 0;
        public byte Aggression {
            get {
                if (_aggression == 0) {
                    _aggression = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Aggression, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _aggression;
            }
            set {
                if (_aggression != value) {
                    isDirty = true;
                    _aggression = value;
                }
            }
        }

        private byte _agility = 0;
        public byte Agility {
            get {
                if (_agility == 0) {
                    _agility = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Agility, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _agility;
            }
            set {
                if (_agility != value) {
                    isDirty = true;
                    _agility = value;
                }
            }
        }

        private byte _importantMatches = 0;
        public byte ImportantMatches {
            get {
                if (_importantMatches == 0) {
                    _importantMatches = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.ImportantMatches, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _importantMatches;
            }
            set {
                if (_importantMatches != value) {
                    isDirty = true;
                    _importantMatches = value;
                }
            }
        }

        private byte _injuryProneness = 0;
        public byte InjuryProneness {
            get {
                if (_injuryProneness == 0) {
                    _injuryProneness = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.InjuryProneness, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _injuryProneness;
            }
            set {
                if (_injuryProneness != value) {
                    isDirty = true;
                    _injuryProneness = value;
                }
            }
        }

        private byte _versatility = 0;
        public byte Versatility {
            get {
                if (_versatility == 0) {
                    _versatility = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _versatility;
            }
            set {
                if (_versatility != value) {
                    isDirty = true;
                    _versatility = value;
                }
            }
        }

        private byte _naturalFitness = 0;
        public byte NaturalFitness {
            get {
                if (_naturalFitness == 0) {
                    _naturalFitness = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.NaturalFitness, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _naturalFitness;
            }
            set {
                if (_naturalFitness != value) {
                    isDirty = true;
                    _naturalFitness = value;
                }
            }
        }

        private byte _determination = 0;
        public byte Determination {
            get {
                if (_determination == 0) {
                    _determination = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _determination;
            }
            set {
                if (_determination != value) {
                    isDirty = true;
                    _determination = value;
                }
            }
        }

        private byte _composure = 0;
        public byte Composure {
            get {
                if (_composure == 0) {
                    _composure = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Composure, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _composure;
            }
            set {
                if (_composure != value) {
                    isDirty = true;
                    _composure = value;
                }
            }
        }

        private byte _concentration = 0;
        public byte Concentration {
            get {
                if (_concentration == 0) {
                    _concentration = PropertyInvoker.Get<byte>(PlayerAttributeOffsets.Concentration, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _concentration;
            }
            set {
                if (_concentration != value) {
                    isDirty = true;
                    _concentration = value;
                }
            }
        }
    }
}
