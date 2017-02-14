using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;

namespace FMScoutFramework.Core.Entities
{
    public class Global
    {
        private readonly IVersion _version;
        public Global (IVersion version)
        {
            _version = version;
        }

        public DateTime InGameDate {
#if MAC
            get { return ProcessManager.ReadDateTime (_version.MemoryAddresses.CurrentDateTime); }
#endif
#if WINDOWS
            get { return ProcessManager.ReadDateTime (ProcessManager.fmProcess.BaseAddress + _version.MemoryAddresses.CurrentDateTime); }
#endif
        }

        public int ActiveObjectID {
            get {
                return ProcessManager.ReadInt32(ProcessManager.fmProcess.BaseAddress + _version.MemoryAddresses.ActiveObject);
            }
        }
    }

    public enum DatabaseModeEnum { Realtime, Cached }
}
