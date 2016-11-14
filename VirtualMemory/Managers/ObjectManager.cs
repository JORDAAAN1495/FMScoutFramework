using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Entities;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame;
using System.Threading.Tasks;

namespace FMScoutFramework.Core.Managers
{
    public static class ObjectManagerWrapper
    {
        public static Dictionary<DatabaseModeEnum, ObjectManager> ObjectManagers =
            new Dictionary<DatabaseModeEnum, ObjectManager> ();

        // public static StaffMemoryAddressesWrapper StaffMemoryCache;
    }

    public class ObjectManager
    {
        public Dictionary<Type, object> ObjectStore = new Dictionary<Type, object> ();
        public readonly DatabaseModeEnum DatabaseMode;
        public readonly GameManager GameManager;

        public ObjectManager (GameManager gameManager, DatabaseModeEnum databaseMode = DatabaseModeEnum.Realtime)
        {
            DatabaseMode = databaseMode;
            GameManager = gameManager;
        }

        public void Load (bool refreshPersonCache)
        {
            ObjectStore.Clear ();

            ObjectStore.Add (typeof (Continent), RetrieveObjects<Continent> (GameManager.Version.MemoryAddresses.Continent));
            ObjectStore.Add (typeof (Nation), RetrieveObjects<Nation> (GameManager.Version.MemoryAddresses.Nation));
            ObjectStore.Add (typeof (City), RetrieveObjects<City> (GameManager.Version.MemoryAddresses.City));
            ObjectStore.Add (typeof (Club), RetrieveObjects<Club> (GameManager.Version.MemoryAddresses.Club));

            //#region People
            //ObjectStore.Add (typeof (Player), RetrieveObjects<Player> (staffAddresses.PlayerAddresses));
            //ObjectStore.Add (typeof (Staff), RetrieveObjects<Staff> (staffAddresses.StaffAddresses));
            //ObjectStore.Add (typeof (PlayerStaff), RetrieveObjects<PlayerStaff> (staffAddresses.PlayerStaffAddresses));
            //#endregion
        }

        List<Int64> RetrieveObjects<T> (Int64 offset)
        {
            Int64 memoryAddress = ProcessManager.ReadInt64 (ProcessManager.fmProcess.BaseAddress + GameManager.Version.MemoryAddresses.MainAddress);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + offset);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + GameManager.Version.MemoryAddresses.XorDistance);
            int objectCount = ProcessManager.ReadArrayLength (memoryAddress);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress);

            if (typeof (T) == typeof (Person)) {
                return null;
            }

            List<Int64> pointers = GetMemoryAddresses (memoryAddress, objectCount);
            return pointers;
        }

        List<Int64> GetMemoryAddresses (Int64 memoryAddress, int length)
        {
            List<Int64> res = new List<Int64> (length);
            List<Int64> pointers = new List<Int64> (length);
            for (Int64 i = memoryAddress; i < (memoryAddress + (length * 0x8)); i += 0x8) {
                pointers.Add (i);
            }

            Parallel.ForEach (pointers, (i) => {
                Int64 pointer = ProcessManager.ReadInt64 (i);
                res.Add (pointer);
            });

            return res;
        }
    }
}
