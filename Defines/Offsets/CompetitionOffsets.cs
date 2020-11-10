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
        return 0xA0;
      }
    }

    public short SouthCity {
      get {
        return 0xA8;
      }
    }

    public short WestCity {
      get {
        return 0xB0;
      }
    }

    public short EastCity {
      get {
        return 0xB8;
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
        return 0xD0;
      }
    }

    public short Champions {
      get {
        return 0xD8;
      }
    }

    public short ForegroundColour {
      get {
        return 0x130;
      }
    }

    public short BackgroundColour {
      get {
        return 0x134;
      }
    }

    public short TrimColour {
      get {
        return 0x138;
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
        return 0x168;
      }
    }

    public short MinimumPitchWidth {
      get {
        return 0x16A;
      }
    }

    public short MaximumPitchLength {
      get {
        return 0x16C;
      }
    }

    public short MaximumPitchWidth {
      get {
        return 0x16E;
      }
    }

    public short Reputation {
      get {
        return 0x172; // 0x13E;
      }
    }

    public short OriginalReputation {
      get {
        return 0x174;
      }
    }

    public short LastReputationPos {
      get {
        return 0x176;
      }
    }

    public short CurrentReputation {
      get {
        return 0x178;
      }
    }

    public short PercentageOfTopDivisionReputation {
      get {
        return 0x17A;
      }
    }

    public short NameType {
      get {
        return 0x191;
      }
    }

    public short Flags {
      get {
        return 0x192;
      }
    }

    public short DivisionLevel {
      get {
        return 0x193;
      }
    }

    public short Type {
      get {
        return 0x194;
      }
    }

    public short UsesExtraOfficials {
      get {
        return 0x195;
      }
    }

    public short UsesSeatedOnlyStadiums {
      get {
        return 0x196;
      }
    }

    public short WageBudgetTurnoverPercentage {
      get {
        return 0x197;
      }
    }
  }
}
