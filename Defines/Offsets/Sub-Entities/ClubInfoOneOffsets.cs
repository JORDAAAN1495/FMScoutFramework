using FMScoutFramework.Core.Entities.GameVersions;
using System;

namespace FMScoutFramework.Core.Offsets
{
  public sealed class ClubInfoOneOffsets
  {
    public IVersion Version;

    public ClubInfoOneOffsets(IVersion version) {
      Version = version;
    }

    public short AverageAttendance {
      get {
        return 0x68;
      }
    }

    public short MinimumAttendance {
      get {
        return 0x6C;
      }
    }

    public short MaximumAttendance {
      get {
        return 0x70;
      }
    }

    public short TacticalAttributes {
      get {
        return 0x74;
      }
    }

    public short Kits {
      get {
        return 0x78;
      }
    }

    public short ForegroundColour {
      get {
        return 0xA0;
      }
    }

    public short BackgroundColour {
      get {
        return 0xA8;
      }
    }
  }
}
