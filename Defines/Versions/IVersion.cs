using FMScoutFramework.Core.Managers;

namespace FMScoutFramework.Core.Entities.GameVersions
{
    public interface IVersion
    {
        string Description { get; }
        IVersionMemoryAddresses MemoryAddresses { get; }
        IVersionPersonEnumPointers PersonEnum { get; }
        IPersonVersionOffsets PersonOffsets { get; }
        GameManager gameManager { get; set; }
    }

    internal interface IIVersion : IVersion
    {
        bool SupportsProcess (FMProcess process, byte [] context);
    }
}
