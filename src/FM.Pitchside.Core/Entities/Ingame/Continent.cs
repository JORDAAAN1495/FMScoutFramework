using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using System;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Continent : BaseObject, IContinent
    {
        public Continent(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Continent(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}