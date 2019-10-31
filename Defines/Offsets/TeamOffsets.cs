using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class TeamOffsets {
    public IVersion Version;

    public TeamOffsets(IVersion version) {
      this.Version = version;
    }

    public const short RowID = 0x8;
    public const short UID = 0xC;
    public const short Club = 0x18;
    public const short PreviousReputation = 0x2E;
    public const short TeamType = 0x30;
    public short Players {
      get { return 0x38; }
    }
    public short Stadium {
      get { return 0x60; }
    }
    public const short Manager = 0x78;
    public const short Reputation = 0xA8;
  }
}
