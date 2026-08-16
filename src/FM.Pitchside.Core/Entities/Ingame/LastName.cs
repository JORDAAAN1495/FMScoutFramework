using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class LastName : BaseObject, ILastName
    {
        public LastName(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public LastName(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}
