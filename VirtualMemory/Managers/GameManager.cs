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
                Process activeProcess = fmProcesses [0];

                fmProcess.Pointer = ProcessMemoryAPI.OpenProcess (0x001F0FFF, 1, (uint)activeProcess.Id);
                fmProcess.EndPoint = ProcessManager.GetProcessEndPoint (fmProcess.Pointer);
                fmProcess.Process = activeProcess;
                fmProcess.BaseAddress = activeProcess.MainModule.BaseAddress.ToInt32();

                ProcessManager.fmProcess = fmProcess;
                fmProcess.VersionDescription = fmProcess.Process.MainModule.FileVersionInfo.ProductVersion;

                // Search for the current version
                foreach (var versionType in Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(IIVersion).IsAssignableFrom(t))) {
                    if (versionType.IsInterface)
                        continue;
                    var instance = (IIVersion)Activator.CreateInstance (versionType);

                    if (instance.SupportsProcess (fmProcess, null)) {
                        Version = instance;
                        break;
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
            int memoryAddress = ProcessManager.ReadInt32(address);
            Debug.WriteLine("Base 0x{0:X} -> 0x{1:X}", address, memoryAddress);
            if (memoryAddress > fmProcess.BaseAddress && memoryAddress < fmProcess.EndPoint)
            {
                memoryAddress = ProcessManager.ReadInt32(memoryAddress);
                if (memoryAddress < fmProcess.BaseAddress || memoryAddress > fmProcess.EndPoint)
                    return 0;

                string[] splitVersion = fmProcess.VersionDescription.Split('.');
                if (splitVersion[0] == "14")
                {
                    int xorValueOne = ProcessManager.ReadInt32(memoryAddress + offset + 0x4);
                    int xorValueTwo = ProcessManager.ReadInt32(memoryAddress + offset);
                    memoryAddress = xorValueTwo ^ xorValueOne;
                    if (memoryAddress < fmProcess.BaseAddress || memoryAddress > fmProcess.EndPoint)
                        return 0;
                    memoryAddress = ProcessManager.ReadInt32(memoryAddress + 0x54);
                }
                else
                {
                    memoryAddress = ProcessManager.ReadInt32(memoryAddress + offset);
                    if (memoryAddress < fmProcess.BaseAddress || memoryAddress > fmProcess.EndPoint)
                        return 0;
                    memoryAddress = ProcessManager.ReadInt32(memoryAddress + 0x40);
                }
                
                if (memoryAddress < fmProcess.BaseAddress || memoryAddress > fmProcess.EndPoint)
                    return 0;

                int numberOfObjects = ProcessManager.ReadArrayLength(memoryAddress);
                return numberOfObjects;
            }
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
    }
}
