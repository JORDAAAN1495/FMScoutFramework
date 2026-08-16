using FM.Pitchside.Core.Defines.Versions;
using System.Diagnostics;

namespace FM.Pitchside.Core.Defines
{
    /// <summary>
    /// Holder for the Football Manager process
    /// </summary>
    public class FMProcess
    {
        public Process Process { get; set; }
        public IntPtr Pointer { get; set; }
        public Int64 EndPoint { get; set; }
        public Int64 BaseAddress { get; set; }
        public string VersionDescription { get; set; }
        public int HeapAddress { get; set; }
        public IVersion Version { get; set; }
        public uint ProcessTask { get; set; }
    }
}