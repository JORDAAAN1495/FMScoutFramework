using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class ClubInfoTwoOffsets {
    private IVersion Version;
    public ClubInfoTwoOffsets(IVersion version) {
      Version = version;
    }

    public const short SixLetterName = 0x0;
    public const short ClubDebts = 0x58;

    public short YearFounded {
      get {
        return 0xE0;
      }
    }

    public short YouthImportance {
      get {
        return 0x101;
      }
    }

    public short TrainingFacilities {
      get {
        return 0x108;
      }
    }

    public short ChairmanStatus {
      get {
        return 0xE6;
      }
    }

    public short YouthFacilities {
      get {
        return 0x113;
      }
    }

    public short JuniorCoaching {
      get {
        return 0x114;
      }
    }

    public short YouthRecruitment {
      get {
        return 0x115;
      }
    }

    public short Morale {
      get {
        return 0x107;
      }
    }
  }
}
