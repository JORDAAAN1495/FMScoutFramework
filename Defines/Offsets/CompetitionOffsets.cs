using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Defines.Offsets {
  public sealed class CompetitionOffsets {
    public IVersion Version;

    public CompetitionOffsets(IVersion version) {
      this.Version = version;
    }

    public const short RowID = 0x8;
    public const short UID = 0xC;

    public short Name {
      get {
        return 0x58;
      }
    }

    public short ShortName {
      get {
        return 0x60;
      }
    }

    public short ThreeLetterName {
      get {
        return 0x68;
      }
    }

    public short Continent {
      get {
        return 0x70;
      }
    }

    public short Nation {
      get {
        return 0x78;
      }
    }

    public short ParentCompetition {
      get {
        return 0x80;
      }
    }

    public short NorthCity {
      get {
        return 0x98;
      }
    }

    public short SouthCity {
      get {
        return 0xA0;
      }
    }

    public short WestCity {
      get {
        return 0xA8;
      }
    }

    public short EastCity {
      get {
        return 0xB0;
      }
    }

    // Past Year Winner / Runner Up / Third Placed
    public short PastWinners { // Alternative Names?
      get {
        return 0xB8;
      }
    }

    public short LastHistory { // CompetitionHistory DB Entity
      get {
        return 0xC0;
      }
    }

    public short ActualCompetition {
      get {
        return 0xC8;
      }
    }

    public short Champions {
      get {
        return 0xD0;
      }
    }

    public short ForegroundColour {
      get {
        return 0x128;
      }
    }

    public short BackgroundColour {
      get {
        return 0x12C;
      }
    }

    public short TrimColour {
      get {
        return 0x130;
      }
    }

    public short WinterBallStartDate {
      get {
        return 0x148;
      }
    }

    public short WinterBallEndDate {
      get {
        return 0x14C;
      }
    }

    public short WinterBallColour {
      get {
        return 0x158;
      }
    }

    public short MinimumPitchLength {
      get {
        return 0x160;
      }
    }

    public short MinimumPitchWidth {
      get {
        return 0x162;
      }
    }

    public short MaximumPitchLength {
      get {
        return 0x164;
      }
    }

    public short MaximumPitchWidth {
      get {
        return 0x166;
      }
    }

    public short Reputation {
      get {
        return 0x16A; // 0x13E;
      }
    }

    public short OriginalReputation {
      get {
        return 0x16C;
      }
    }

    public short LastReputationPos {
      get {
        return 0x16E;
      }
    }

    public short CurrentReputation {
      get {
        return 0x170;
      }
    }

    public short PercentageOfTopDivisionReputation {
      get {
        return 0x172;
      }
    }

    public short NameType {
      get {
        return 0x186;
      }
    }

    public short Flags {
      get {
        return 0x187;
      }
    }

    public short DivisionLevel {
      get {
        return 0x188;
      }
    }

    public short Type {
      get {
        return 0x189;
      }
    }

    public short UsesExtraOfficials {
      get {
        return 0x18A;
      }
    }

    public short UsesSeatedOnlyStadiums {
      get {
        return 0x18B;
      }
    }

    public short WageBudgetTurnoverPercentage {
      get {
        return 0x18C;
      }
    }
  }
}
