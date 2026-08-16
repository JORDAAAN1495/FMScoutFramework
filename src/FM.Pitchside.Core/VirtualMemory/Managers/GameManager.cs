using FM.Pitchside.Core.Defines;
using FM.Pitchside.Core.Defines.Versions;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FM.Pitchside.Core.VirtualMemory.Managers
{
    public class GameManager
    {
        private bool fmLoaded;
        private bool fmLoading;
        public LogWriter logger { get; set; }

        public GameManager()
        {
            this.fmLoaded = false;
            this.fmLoading = false;
        }

        public bool FMLoaded
        {
            get { return fmLoaded; }
        }

        public bool FMLoading
        {
            get { return fmLoading; }
            set { fmLoading = value; }
        }

        public static string LastErrorMessage { get; set; }

        public IVersion Version { get; private set; }

        #region MAC

#if MAC
    public bool findFMProcess() {
      FMProcess fmProcess = new FMProcess();
      Process[] fmProcesses = Process.GetProcessesByName("fm");
      if (fmProcesses.Length > 0) {
        logger.LogWrite("Found > 0 FM Processes");
        Process activeProcess = fmProcesses[0];
        // Try to get the pTask
        uint ptask = ProcessMemoryAPI.GetProcessTaskForPID(activeProcess.Id);
        fmProcess.Process = activeProcess;
        fmProcess.ProcessTask = ptask;
        fmProcess.BaseAddress = ProcessManager.GetASLROffset(fmProcess.ProcessTask);
        // fmProcess.EndPoint = ProcessManager.GetProcessEndPoint (activeProcess.Id);

        ProcessManager.fmProcess = fmProcess;

        // Search for the current version
        foreach (var versionType in Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(IIVersion).IsAssignableFrom(t))) {
          if (versionType.IsInterface)
            continue;
          var instance = (IIVersion)Activator.CreateInstance(versionType, this);

          if (instance.SupportsProcess(fmProcess, null)) {
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
        public bool findFMProcess()
        {
            FMProcess fmProcess = new FMProcess();
            Process[] fmProcesses = Process.GetProcessesByName("fm");

            if (fmProcesses.Length > 0)
            {
                logger.LogWrite("Found > 0 FM Processes.");
                Process activeProcess = fmProcesses[0];

                logger.LogWrite("Opening Process for r/w.");
                fmProcess.Pointer = ProcessMemoryAPI.OpenProcess(0x001F0FFF, 1, (uint)activeProcess.Id);
                // fmProcess.EndPoint = ProcessManager.GetProcessEndPoint (fmProcess.Pointer);
                logger.LogWrite("Process is now open.");
                fmProcess.Process = activeProcess;
                fmProcess.BaseAddress = activeProcess.MainModule.BaseAddress.ToInt64();

                // Newer (Unity/IL2CPP) builds keep the actual simulation state in a native
                // plugin module rather than the main executable - address all offsets relative
                // to that module when it's loaded. Older native builds have no such module, so
                // this leaves fmProcess.BaseAddress pointing at the main exe as before.
                var pluginModule = activeProcess.Modules.Cast<ProcessModule>()
                    .FirstOrDefault(m => string.Equals(m.ModuleName, "game_plugin.dll", StringComparison.OrdinalIgnoreCase));
                if (pluginModule != null)
                {
                    fmProcess.BaseAddress = pluginModule.BaseAddress.ToInt64();
                }

                ProcessManager.fmProcess = fmProcess;
                fmProcess.VersionDescription = fmProcess.Process.MainModule.FileVersionInfo.ProductVersion;

                // Search for the current version
                logger.LogWrite("Searching for a suitable version definition...");
                var types = Assembly.GetCallingAssembly().GetTypes()
                    .Where(t => typeof(IIVersion).IsAssignableFrom(t));

                foreach (var versionType in types)
                {
                    if (versionType.IsInterface)
                    {
                        continue;
                    }

                    var instance = (IIVersion)Activator.CreateInstance(versionType, this);

                    logger.LogWrite("Trying " + instance.Description);
                    if (instance.SupportsProcess(fmProcess, null))
                    {
                        Version = instance;
                        logger.LogWrite("Matched!");
                        break;
                    }
                    else
                    {
                        logger.LogWrite("Not a match.");
                    }
                }

                fmLoaded = (Version != null);
            }

            if (!fmLoaded)
            {
                LastErrorMessage = "Could not find a compatible Football Manager version.";
            }

            return fmLoaded;
        }
#endif

        #endregion

        public static void detectNewMainAddress()
        {
            // Find the FM process
            FMProcess fmProcess = new FMProcess();
            Process[] fmProcesses = Process.GetProcessesByName("fm");
            Process activeProcess = fmProcesses[0];

            fmProcess.Pointer = ProcessMemoryAPI.OpenProcess(0x001F0FF, 1, (uint)activeProcess.Id);
            fmProcess.Process = activeProcess;
            fmProcess.BaseAddress = activeProcess.MainModule.BaseAddress.ToInt64();

            ProcessManager.fmProcess = fmProcess;
            fmProcess.VersionDescription = fmProcess.Process.MainModule.FileVersionInfo.ProductVersion;

            // Start searching for the main address in reverse. Find "Albpetrol Patos", the first club in the clubs table
        }

        public static int TryGetPointerObjects(Int64 address, Int64 offset, FMProcess fmProcess)
        {
            return GameManager.TryGetPointerObjects(address, offset, fmProcess);
        }

        public static int TryGetPointerObjects(Int64 address, Int64 offset, FMProcess fmProcess, Int64 xor)
        {
            Int64 memoryAddress = fmProcess.BaseAddress + address;
            memoryAddress = ProcessManager.ReadInt64(memoryAddress + offset);
            memoryAddress = ProcessManager.ReadInt64(memoryAddress + xor);
            int numberOfObjects = ProcessManager.ReadArrayLength(memoryAddress);
            return numberOfObjects;
        }

        #region Events

        public delegate void ObjectEditedDelegate(object item);

        public event ObjectEditedDelegate ObjectEdited;

        internal void RaiseObjectEdited(object item)
        {
            if (this.ObjectEdited != null)
            {
                this.ObjectEdited(item);
            }
        }

        #endregion
    }
}