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

        private DateTime _inGameDate;
        public DateTime InGameDate {
            get {
                if (_inGameDate.Year <= 1900) {
                    _inGameDate = ProcessManager.ReadDateTime(ProcessManager.fmProcess.BaseAddress + _version.MemoryAddresses.CurrentDateTime);
                }

                return _inGameDate;
            }
            set {
                if (_inGameDate != value) {
                    _inGameDate = value;
                }
            }
        }

        public int ActiveObjectID {
            get {
                return ProcessManager.ReadInt32(ProcessManager.fmProcess.BaseAddress + _version.MemoryAddresses.ActiveObject);
            }
        }
    }

    public enum DatabaseModeEnum { Realtime, Cached }
}
