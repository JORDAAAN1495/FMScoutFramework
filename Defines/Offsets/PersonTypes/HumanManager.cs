using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class HumanManagerOffsets {
    public IVersion Version;

    public HumanManagerOffsets(IVersion version) {
      this.Version = version;
    }

    public const short Unsackable = 0x308;
    public const short SquadRegistrationOptions = 0x309;
    public const short Characteristic = 0x305;

    public short ActualPerson {
      get {
        return 0x480;
      }
    }
  }
}
