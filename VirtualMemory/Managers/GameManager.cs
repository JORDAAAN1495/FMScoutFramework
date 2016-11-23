using System;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Configuration;
using System.IO;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Managers
{
    public class GameManager
    {
        private bool fmLoaded;
        private bool fmLoading;
        public LogWriter logger { get; set; }

        public GameManager ()
        {
            this.fmLoaded = false;
            this.fmLoading = false;
        }

        public bool FMLoaded {
            get { return fmLoaded; }
        }

        public bool FMLoading {
            get { return fmLoading; }
            set { fmLoading = value; }
        }

        public static string LastErrorMessage {
            get; set;
        }

        public IVersion Version {
            get;
            private set;
        }

        #region MAC
#if MAC
        public bool findFMProcess ()
        {
            FMProcess fmProcess = new FMProcess ();
            Process [] fmProcesses = Process.GetProcessesByName ("fm");
            if (fmProcesses.Length > 0) {
                Process activeProcess = fmProcesses [0];
                // Try to get the pTask
                uint ptask = ProcessMemoryAPI.GetProcessTaskForPID (activeProcess.Id);
                fmProcess.Process = activeProcess;
                fmProcess.ProcessTask = ptask;
                fmProcess.BaseAddress = ProcessManager.GetASLROffset (fmProcess.ProcessTask);
                // fmProcess.EndPoint = ProcessManager.GetProcessEndPoint (activeProcess.Id);

                ProcessManager.fmProcess = fmProcess;

                // Search for the current version
                foreach (var versionType in Assembly.GetCallingAssembly ().GetTypes ().Where (t => typeof (IIVersion).IsAssignableFrom (t))) {
                    if (versionType.IsInterface)
                        continue;
                    var instance = (IIVersion)Activator.CreateInstance (versionType);

                    if (instance.SupportsProcess (fmProcess, null)) {
                        Version = instance;
                        fmProcess.Version = instance;
                        break;
                    }
                }

                fmLoaded = (Version != null);
            }

            return fmLoaded;
        }
#endif
        #endregion
        #region WINDOWS
#if WINDOWS
        public bool findFMProcess() {
            FMProcess fmProcess = new FMProcess ();
            Process[] fmProcesses = Process.GetProcessesByName ("fm");

            if (fmProcesses.Length > 0) {
                logger.LogWrite("Found > 0 FM Processes.");
                Process activeProcess = fmProcesses [0];

                logger.LogWrite("Opening Process for r/w.");
                fmProcess.Pointer = ProcessMemoryAPI.OpenProcess (0x001F0FFF, 1, (uint)activeProcess.Id);
                // fmProcess.EndPoint = ProcessManager.GetProcessEndPoint (fmProcess.Pointer);
                logger.LogWrite("Process is now open.");
                fmProcess.Process = activeProcess;
                fmProcess.BaseAddress = activeProcess.MainModule.BaseAddress.ToInt64();

                ProcessManager.fmProcess = fmProcess;
                fmProcess.VersionDescription = fmProcess.Process.MainModule.FileVersionInfo.ProductVersion;

                // Search for the current version
                logger.LogWrite("Searching for a suitable version definition...");
                foreach (var versionType in Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(IIVersion).IsAssignableFrom(t))) {
                    if (versionType.IsInterface)
                        continue;
                    var instance = (IIVersion)Activator.CreateInstance (versionType, this);

                    logger.LogWrite("Trying " + instance.Description);
                    if (instance.SupportsProcess (fmProcess, null)) {
                        Version = instance;
                        logger.LogWrite("Matched!");
                        break;
                    }
                    else {
                        logger.LogWrite("Not a match.");
                    }
                }

                fmLoaded = (Version != null);
            }
            return fmLoaded;
        }
#endif
        #endregion

        public static int TryGetPointerObjects (Int64 address, Int64 offset, FMProcess fmProcess)
        {
            return GameManager.TryGetPointerObjects (address, offset, fmProcess);
        }

        public static int TryGetPointerObjects (Int64 address, Int64 offset, FMProcess fmProcess, Int64 xor)
        {
            #region WINDOWS
#if WINDOWS
            Int64 memoryAddress = fmProcess.BaseAddress + address;
            memoryAddress = ProcessManager.ReadInt64(memoryAddress + offset);
            memoryAddress = ProcessManager.ReadInt64(memoryAddress + xor);
            int numberOfObjects = ProcessManager.ReadArrayLength(memoryAddress);
            return numberOfObjects;
#endif
            #endregion
            #region MAC
#if MAC
            Int64 memoryAddress = ProcessManager.ReadInt64 ((fmProcess.BaseAddress + address));
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + offset);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + xor);
            int numberOfObjects = ProcessManager.ReadArrayLength (memoryAddress);
            return numberOfObjects;
#endif
            #endregion
        }

        #region Events
        public delegate void ObjectEditedDelegate(object item);
        public event ObjectEditedDelegate ObjectEdited;

        internal void RaiseObjectEdited(object item) {
            this.ObjectEdited(item);
        }
        #endregion
    }
}
