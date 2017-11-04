using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Attributes;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Utilities;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Person : BaseObject, IPerson {
        public PersonOffsets PersonOffsets;
        public Person(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version) {
            this.PersonOffsets = new PersonOffsets(version);
        }
        public Person(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) {
            this.PersonOffsets = new PersonOffsets(version);
        }

        public int UID {
            get {
                return PropertyInvoker.Get<int>(PersonOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public int RowID {
            get {
                return PropertyInvoker.Get<int>(PersonOffsets.RowID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        public UInt64 DBUID {
            get {
                return PropertyInvoker.Get<UInt64>(PersonOffsets.UID, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }
    }
}