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
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xB0;
        }

        return 0xA8;
      }
    }

    public short YouthImportance {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xE1;
        }

        return 0xD9;
      }
    }

    public short TrainingFacilities {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xDA;
        }

        return 0xC7;
      }
    }

    public short ChairmanStatus {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xB8;
        }

        return 0xB0;
      }
    }

    public short YouthFacilities {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xE6;
        }

        return 0xD2;
      }
    }

    public short JuniorCoaching {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xE7;
        }

        return 0xD1;
      }
    }

    public short YouthRecruitment {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xE8;
        }

        return 0xE0;
      }
    }

    public short Morale {
      get {
        if (this.Version.GetType() == typeof(Steam_20_4_0_Windows) ||
            this.Version.GetType() == typeof(Steam_20_4_1_Windows) ||
            this.Version.GetType() == typeof(GamePass_20_4_1_Windows)) {
          return 0xDA;
        }

        return 0xD3;
      }
    }
  }
}
