using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class ClubInfoTwoOffsets {
    private IVersion Version;
    public ClubInfoTwoOffsets(IVersion version) {
      Version = version;
    }

    public const short SixLetterName = 0x0;
    public const short ClubDebts = 0x48;

    public short YearFounded {
      get {
        return 0xC0;
      }
    }

    public short YouthImportance {
      get {
        return 0xE1;
      }
    }

    public short TrainingFacilities {
      get {
        return 0xE8;
      }
    }

    public short ChairmanStatus {
      get {
        return 0xC6;
      }
    }

    public short YouthFacilities {
      get {
        return 0xF3;
      }
    }

    public short JuniorCoaching {
      get {
        return 0xF4;
      }
    }

    public short YouthRecruitment {
      get {
        return 0xF5;
      }
    }

    public short Morale {
      get {
        return 0xE7;
      }
    }
  }
}
