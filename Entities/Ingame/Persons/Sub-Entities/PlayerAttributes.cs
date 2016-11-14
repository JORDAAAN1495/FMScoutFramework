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

        public byte Goalkeeper {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.GoalKeeper, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte Sweeper {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Sweeper, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte DefenderLeft {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.DefenderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte DefenderCenter {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.DefenderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte DefenderRight {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.DefenderRight, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte DefensiveMidfielder {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.DefensiveMidfielder, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte MidfielderLeft {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.MidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte MidfielderCenter {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.MidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte MidfielderRight {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.MidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte AttackingMidfielderLeft {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.AttackingMidfielderLeft, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte AttackingMidfielderCenter {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.AttackingMidfielderCenter, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte AttackingMidfielderRight {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.AttackingMidfielderRight, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte Striker {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Striker, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte WingbackLeft {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.WingbackLeft, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public byte WingbackRight {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.WingbackRight, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public string Position {
            get {
                string final = "";
                if (Goalkeeper == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "GK";
                }
                if (Sweeper == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "SW";
                }
                if (DefenderLeft == 20 || DefenderCenter == 20 || DefenderRight == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "D ";
                    if (DefenderRight == 20)
                        final += "R";
                    if (DefenderLeft == 20)
                        final += "L";
                    if (DefenderCenter == 20)
                        final += "C";
                }
                if (WingbackLeft == 20 || WingbackRight == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "WB ";
                    if (WingbackRight == 20)
                        final += "R";
                    if (WingbackLeft == 20)
                        final += "L";
                }
                if (DefensiveMidfielder == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "DM";
                }
                if (AttackingMidfielderLeft == 20 || AttackingMidfielderCenter == 20 || AttackingMidfielderRight == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "AM ";
                } else if (MidfielderLeft == 20 || MidfielderCenter == 20 || MidfielderRight == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "M ";
                }
                if (AttackingMidfielderLeft == 20 || AttackingMidfielderCenter == 20 || AttackingMidfielderRight == 20 ||
                    MidfielderLeft == 20 || MidfielderCenter == 20 || MidfielderRight == 20) {
                    if (AttackingMidfielderRight == 20 || MidfielderRight == 20)
                        final += "R";
                    if (AttackingMidfielderLeft == 20 || MidfielderLeft == 20)
                        final += "L";
                    if (AttackingMidfielderCenter == 20 || MidfielderCenter == 20)
                        final += "C";
                }
                if (Striker == 20) {
                    if (final.Length > 0)
                        final += ", ";
                    final += "F C";
                }

                return final;
            }
        }

        public byte Crossing {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Crossing, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Crossing, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Dribbling {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Dribbling, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Dribbling, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Finishing {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Finishing, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Finishing, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Heading {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Heading, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Heading, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte LongShots {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.LongShots, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.LongShots, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Marking {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Marking, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte OffTheBall {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.OffTheBall, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.OffTheBall, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Passing {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Passing, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Passing, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Penalties {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Penalties, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Penalties, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Tackling {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Tackling, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Tackling, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Creativity {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Creativity, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Creativity, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Handling {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Handling, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Handling, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte AerialAbility {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.AerialAbility, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.AerialAbility, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte CommandOfArea {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.CommandOfArea, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.CommandOfArea, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Communication {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Communication, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Communication, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Kicking {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Kicking, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Kicking, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Throwing {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Throwing, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Throwing, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Anticipation {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Anticipation, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Anticipation, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Decisions {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Decisions, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Decisions, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte OneOnOnes {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.OneOnOnes, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.OneOnOnes, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Positioning {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Positioning, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Positioning, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Reflexes {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Reflexes, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Reflexes, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte FirstTouch {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.FirstTouch, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.FirstTouch, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Technique {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Technique, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Technique, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte LeftFoot {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.LeftFoot, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.LeftFoot, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte RightFoot {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.RightFoot, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.RightFoot, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Flair {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Flair, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Flair, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Corners {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Corners, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Corners, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Teamwork {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Teamwork, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Teamwork, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Workrate {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.WorkRate, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.WorkRate, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte LongThrows {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.LongThrows, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.LongThrows, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Eccentricity {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Eccentricity, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Eccentricity, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte RushingOut {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.RushingOut, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.RushingOut, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte TendencyToPunch {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.TendencyToPunch, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.TendencyToPunch, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Acceleration {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Acceleration, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Acceleration, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte FreekickTaking {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.FreekickTaking, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.FreekickTaking, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Strength {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Strength, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Strength, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Stamina {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Stamina, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Stamina, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Pace {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Pace, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Pace, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Jumping {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Jumping, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Jumping, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Influence {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Influence, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Influence, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Dirtiness {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Dirtiness, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Dirtiness, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Balance {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Balance, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Bravery {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Bravery, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Bravery, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Consistency {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Consistency, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Consistency, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Aggression {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Aggression, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Aggression, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Agility {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Agility, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Agility, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte ImportantMatches {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.ImportantMatches, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.ImportantMatches, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte InjuryProneness {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.InjuryProneness, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.InjuryProneness, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Versatility {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Versatility, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte NaturalFitness {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.NaturalFitness, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.NaturalFitness, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Determination {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Determination, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Composure {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Composure, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Composure, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }

        public byte Concentration {
            get {
                return PropertyInvoker.Get<byte> (PlayerAttributeOffsets.Concentration, OriginalBytes, MemoryAddress, DatabaseMode);
            }
            set {
                PropertyInvoker.Set<byte> (PlayerAttributeOffsets.Concentration, OriginalBytes, MemoryAddress, DatabaseMode, value);
            }
        }
    }
}
