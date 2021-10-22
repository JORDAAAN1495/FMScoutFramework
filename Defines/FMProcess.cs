using System;
using FMScoutFramework.Core.Entities.GameVersions;
using Windows.System.Diagnostics;

namespace FMScoutFramework.Core
{
    /// <summary>
    /// Holder for the Football Manager process
    /// </summary>
    public class FMProcess
    {
        public ProcessDiagnosticInfo Process { get; set; }
        public IntPtr Pointer { get; set; }
        public Int64 EndPoint { get; set; }
        public Int64 BaseAddress { get; set; }
        public string VersionDescription { get; set; }
        public int HeapAddress { get; set; }
        public IVersion Version { get; set; }
        public uint ProcessTask { get; set; }
    }
}