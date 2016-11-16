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

            ObjectStore.Add(typeof(Award), RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Award));
            ObjectStore.Add(typeof(City), RetrieveObjects<City>(GameManager.Version.MemoryAddresses.City));
            ObjectStore.Add(typeof(Club), RetrieveObjects<Club>(GameManager.Version.MemoryAddresses.Club));
            ObjectStore.Add(typeof(Competition), RetrieveObjects<Competition>(GameManager.Version.MemoryAddresses.Competition));
            ObjectStore.Add(typeof(Continent), RetrieveObjects<Continent>(GameManager.Version.MemoryAddresses.Continent));
            ObjectStore.Add(typeof(Currency), RetrieveObjects<Currency>(GameManager.Version.MemoryAddresses.Currency));
            // Unknown 1
            ObjectStore.Add(typeof(Injury), RetrieveObjects<Injury>(GameManager.Version.MemoryAddresses.Injury));
            ObjectStore.Add(typeof(MediaSource), RetrieveObjects<MediaSource>(GameManager.Version.MemoryAddresses.MediaSource));
            ObjectStore.Add(typeof(Language), RetrieveObjects<Language>(GameManager.Version.MemoryAddresses.Language));
            ObjectStore.Add(typeof(LocalRegion), RetrieveObjects<LocalRegion>(GameManager.Version.MemoryAddresses.LocalRegion));
            ObjectStore.Add(typeof(Nation), RetrieveObjects<Nation>(GameManager.Version.MemoryAddresses.Nation));
            // Persons, let's load that later
            // Unknown 2
            // Unknown 3
            ObjectStore.Add(typeof(Stadium), RetrieveObjects<Stadium>(GameManager.Version.MemoryAddresses.Stadium));
            // Unknown 4
            // Unknown 5
            ObjectStore.Add(typeof(Team), RetrieveObjects<Team>(GameManager.Version.MemoryAddresses.Team));
            ObjectStore.Add(typeof(Weather), RetrieveObjects<Weather>(GameManager.Version.MemoryAddresses.Weather));
            // Unknown 6
            ObjectStore.Add(typeof(Derby), RetrieveObjects<Derby>(GameManager.Version.MemoryAddresses.Derby));
            ObjectStore.Add(typeof(Agreement), RetrieveObjects<Agreement>(GameManager.Version.MemoryAddresses.Agreement));
            // ObjectStore.Add(typeof(FirstName), RetrieveObjects<FirstName>(GameManager.Version.MemoryAddresses.FirstName));
            // ObjectStore.Add(typeof(LastName), RetrieveObjects<LastName>(GameManager.Version.MemoryAddresses.LastName));
            // ObjectStore.Add(typeof(CommonName), RetrieveObjects<CommonName>(GameManager.Version.MemoryAddresses.CommonName));
            // Unknown 7
            // Unknown 8
            // Unknown 9

            // Debug some main objects
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown1);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown2);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown3);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown4);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown5);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown6);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown7);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown8);
            RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown9);

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
