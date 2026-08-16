using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using System;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class Weather : BaseObject, IWeather
    {
        public Weather(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public Weather(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }
    }
}