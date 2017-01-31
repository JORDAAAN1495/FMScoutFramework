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

            PersonMemoryAddressesWrapper staffAddresses = SortPersonMemoryAddresses();

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
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown1);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown2);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown3);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown4);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown5);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown6);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown7);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown8);
            //RetrieveObjects<Award>(GameManager.Version.MemoryAddresses.Unknown9);

            //#region People
            ObjectStore.Add (typeof (Player), RetrieveObjects<Player> (staffAddresses.PlayerAddresses));
            //ObjectStore.Add (typeof (Staff), RetrieveObjects<Staff> (staffAddresses.StaffAddresses));
            //ObjectStore.Add (typeof (PlayerStaff), RetrieveObjects<PlayerStaff> (staffAddresses.PlayerStaffAddresses));
            //#endregion
        }

        private PersonMemoryAddressesWrapper SortPersonMemoryAddresses() {
            var memoryAddresses = new PersonMemoryAddressesWrapper();
            memoryAddresses.PlayerAddresses = new List<Int64>();
            memoryAddresses.StaffAddresses = new List<Int64>();
            memoryAddresses.PlayerStaffAddresses = new List<Int64>();
            memoryAddresses.HumanManagerAddresses = new List<Int64>();

            List<Int64> addresses = GetMemoryAddresses<Person>(GameManager.Version.MemoryAddresses.Person);
            List<Int64> unknownaddresses = new List<Int64>();

            foreach(Int64 personAddress in addresses) {
                Int64 type = ProcessManager.ReadInt64(personAddress);
                if (type == GameManager.Version.PersonEnum.Player) {
                    memoryAddresses.PlayerAddresses.Add(personAddress + GameManager.Version.PersonOffsets.Player);
                }
                else if (type == GameManager.Version.PersonEnum.Staff) {
                    memoryAddresses.StaffAddresses.Add(personAddress + GameManager.Version.PersonOffsets.Staff);
                }
                else if (type == GameManager.Version.PersonEnum.PlayerStaff) {
                    memoryAddresses.PlayerStaffAddresses.Add(personAddress + GameManager.Version.PersonOffsets.PlayerStaff);
                }
                else if (type == GameManager.Version.PersonEnum.HumanManager) {
                    memoryAddresses.HumanManagerAddresses.Add(personAddress + GameManager.Version.PersonOffsets.HumanManager);
                }
                else {
                    // Handle unknown person types
                    if (unknownaddresses.IndexOf(type) < 0) {
                        unknownaddresses.Add(type);
                        short personIDOffset = 0x8;
                        if (IntPtr.Size == 8) {
                            personIDOffset = 0xC;
                        }
                        int personID = ProcessManager.ReadInt32(personAddress + personIDOffset);

                        Console.WriteLine("Unknown Person Type: 0x" + type.ToString("X") + " Address: 0x" + personAddress.ToString("X") + " UID: " + personID.ToString());
                    }
                }
            }

            return memoryAddresses;
        }

        Dictionary<Int64, T> RetrieveObjects<T>(List<Int64> addressesCollection) {
            return RetrieveObjects<T>(-1, addressesCollection);
        }

        Dictionary<Int64, T> RetrieveObjects<T>(Int64 offset) {
            return RetrieveObjects<T>(offset, new List<Int64>());
        }

        Dictionary<Int64, T> RetrieveObjects<T>(Int64 offset, List<Int64> addressesCollection)
        {
            List<Int64> memoryAddresses = offset > -1 ? GetMemoryAddresses<T>(offset) : addressesCollection;

            #region CreateConstructorDelegate
            ConstructorInfo constructor = typeof(T).GetConstructor(new[] { typeof(Int64), typeof(IVersion) });
            ParameterExpression expPointer = Expression.Parameter(typeof(Int64), "memoryAddress");
            ParameterExpression vPointer = Expression.Parameter(typeof(IVersion), "version");
            Expression createNew = Expression.New(constructor, expPointer, vPointer);
            LambdaExpression lambda = Expression.Lambda(createNew, new[] { expPointer, vPointer });
            Func<Int64, IVersion, T> constructInvoker = (Func<Int64, IVersion, T>)lambda.Compile();
            #endregion

            var outputList = new Dictionary<Int64, T>(memoryAddresses.Count);
            foreach(Int64 address in memoryAddresses) {
                var obj = constructInvoker.Invoke(address, GameManager.Version);
                if (obj != null) {
                    try {
                        outputList.Add(address, obj);
                    }
                    catch { }
                }
                else {
                    Console.WriteLine("WTF!");
                }
            }

            return outputList;
        }

        List<Int64> GetMemoryAddresses<T> (Int64 offset)
        {
#if MAC
            Int64 memoryAddress = ProcessManager.ReadInt64 (ProcessManager.fmProcess.BaseAddress + GameManager.Version.MemoryAddresses.MainAddress);
#elif WINDOWS
            Int64 memoryAddress = ProcessManager.fmProcess.BaseAddress + GameManager.Version.MemoryAddresses.MainAddress;
#endif
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + offset);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress + GameManager.Version.MemoryAddresses.XorDistance);
            int objectCount = ProcessManager.ReadArrayLength (memoryAddress);
            memoryAddress = ProcessManager.ReadInt64 (memoryAddress);

            FMCore.logger.LogWrite("Loading " + objectCount.ToString() + " " + typeof(T).Name + " pointers");
            List<Int64> pointers = GetMemoryAddressList (memoryAddress, objectCount);
            return pointers;
        }

        List<Int64> GetMemoryAddressList (Int64 memoryAddress, int length)
        {
            List<Int64> pointers = new List<Int64> (length);
            
            for (Int64 i = memoryAddress; i < (memoryAddress + (length * 0x8)); i += 0x8) {
                Int64 pointer = ProcessManager.ReadInt64(i);
                pointers.Add(pointer);
            }

            return pointers;
        }
    }

    public class PersonMemoryAddressesWrapper {
        public List<Int64> PlayerAddresses { get; set; }
        public List<Int64> StaffAddresses { get; set; }
        public List<Int64> PlayerStaffAddresses { get; set; }
        public List<Int64> HumanManagerAddresses { get; set; }
    }
}
