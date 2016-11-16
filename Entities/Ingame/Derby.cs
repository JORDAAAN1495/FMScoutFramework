using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Derby : BaseObject, IDerby
    {
        public Derby(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Derby(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}
