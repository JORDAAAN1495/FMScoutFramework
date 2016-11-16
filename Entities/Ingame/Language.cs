using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Language : BaseObject, ILanguage
    {
        public Language(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Language(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}
