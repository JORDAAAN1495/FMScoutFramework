using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Staff : Person, IStaff
    {
        private StaffOffsets StaffOffsets;
        public Staff (int memoryAddress, IVersion version)
            : base (memoryAddress, version)
        {
            this.StaffOffsets = new StaffOffsets (version);
        }
        public Staff (int memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base (memoryAddress, originalBytes, version)
        {
            this.StaffOffsets = new StaffOffsets (version);
        }

        
    }
}
