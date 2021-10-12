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
        return 0x40;
      }
    }

    public short ShortName {
      get {
        return 0x48;
      }
    }

    public short ThreeLetterName {
      get {
        return 0x50;
      }
    }

    public short Continent {
      get {
        return 0x58;
      }
    }

    public short Nation {
      get {
        return 0x60;
      }
    }

    public short ParentCompetition {
      get {
        return 0x68;
      }
    }

    public short NorthCity {
      get {
        return 0x88;
      }
    }

    public short SouthCity {
      get {
        return 0x90;
      }
    }

    public short WestCity {
      get {
        return 0x98;
      }
    }

    public short EastCity {
      get {
        return 0xA0;
      }
    }

    // Past Year Winner / Runner Up / Third Placed
    public short PastWinners { // Alternative Names?
      get {
        return 0xC0;
      }
    }

    public short LastHistory { // CompetitionHistory DB Entity
      get {
        return 0xC8;
      }
    }

    public short ActualCompetition {
      get {
        return 0xB0;
      }
    }

    public short Champions {
      get {
        return 0xD8;
      }
    }

    public short ForegroundColour {
      get {
        return 0x118;
      }
    }

    public short BackgroundColour {
      get {
        return 0x11C;
      }
    }

    public short TrimColour {
      get {
        return 0x120;
      }
    }

    public short WinterBallStartDate {
      get {
        return 0x150;
      }
    }

    public short WinterBallEndDate {
      get {
        return 0x160;
      }
    }

    public short WinterBallColour {
      get {
        return 0x164;
      }
    }

    public short MinimumPitchLength {
      get {
        return 0x14C;
      }
    }

    public short MinimumPitchWidth {
      get {
        return 0x14E;
      }
    }

    public short MaximumPitchLength {
      get {
        return 0x150;
      }
    }

    public short MaximumPitchWidth {
      get {
        return 0x152;
      }
    }

    public short Reputation {
      get {
        return 0x178; // 0x13E;
      }
    }

    public short OriginalReputation {
      get {
        return 0x17A;
      }
    }

    public short LastReputationPos {
      get {
        return 0x17C;
      }
    }

    public short CurrentReputation {
      get {
        return 0x17E;
      }
    }

    public short PercentageOfTopDivisionReputation {
      get {
        return 0x180;
      }
    }

    public short NameType {
      get {
        return 0x197;
      }
    }

    public short Flags {
      get {
        return 0x198;
      }
    }

    public short DivisionLevel {
      get {
        return 0x199;
      }
    }

    public short Type {
      get {
        return 0x19A;
      }
    }

    public short UsesExtraOfficials {
      get {
        return 0x19B;
      }
    }

    public short UsesSeatedOnlyStadiums {
      get {
        return 0x19C;
      }
    }

    public short WageBudgetTurnoverPercentage {
      get {
        return 0x19D;
      }
    }
  }
}
