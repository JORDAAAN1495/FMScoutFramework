using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
  public sealed class KitOffsets
  {
    public IVersion Version;
    public KitOffsets(IVersion version) {
      Version = version;
    }

    public const short ForegroundColour = 0x0;
    public const short BackgroundColour = 0x4;
    public const short OutlineColour = 0x8;
    public const short NumberColour = 0xC;
    public const short OutlineNumberColour = 0x10;
    public const short Style = 0x14;
    public const short Competition = 0x18;
    public const short Type = 0x22;
    public const short RecordType = 0x23; 
    public const short Outfield = 0x25;
  }
}
