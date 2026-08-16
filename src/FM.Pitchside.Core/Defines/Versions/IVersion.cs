using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Defines.Versions
{
    public interface IVersion
    {
        string Description { get; }
        IVersionMemoryAddresses MemoryAddresses { get; }
        IVersionPersonEnumPointers PersonEnum { get; }
        IPersonVersionOffsets PersonOffsets { get; }
        GameManager gameManager { get; set; }
        bool isTouch { get; set; }
    }

    internal interface IIVersion : IVersion
    {
        bool SupportsProcess(FMProcess process, byte[] context);
    }
}