using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class ClubInfoTwoOffsets {
    private IVersion Version;
    public ClubInfoTwoOffsets(IVersion version) {
      Version = version;
    }

    public const short SixLetterName = 0x0;
    public const short ClubDebts = 0x50;

    public short YearFounded {
      get {
        return 0xD8;
      }
    }

    public short YouthImportance {
      get {
        return 0xF9;
      }
    }

    public short TrainingFacilities {
      get {
        return 0x100;
      }
    }

    public short ChairmanStatus {
      get {
        return 0xDE;
      }
    }

    public short YouthFacilities {
      get {
        return 0x103;
      }
    }

    public short JuniorCoaching {
      get {
        return 0x10C;
      }
    }

    public short YouthRecruitment {
      get {
        return 0x10D;
      }
    }

    public short Morale {
      get {
        return 0xFF;
      }
    }
  }
}
