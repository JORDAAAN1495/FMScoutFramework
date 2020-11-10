using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class HumanManagerOffsets {
    public IVersion Version;

    public HumanManagerOffsets(IVersion version) {
      this.Version = version;
    }

    public const short Unsackable = 0x2B0;
    public const short SquadRegistrationOptions = 0x2B1;
    public const short Characteristic = 0x305;

    public short ActualPerson {
      get {
        return 0x480;
      }
    }
  }
}
