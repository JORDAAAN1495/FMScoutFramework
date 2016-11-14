using System;
using FMScoutFramework.Core.Attributes;

namespace FMScoutFramework.Core.Entities.GameVersions
{
    public interface IVersionMemoryAddresses
    {
        Int64 MainAddress { get; }
        Int64 MainOffset { get; }
        Int64 XorDistance { get; }
        Int64 StringOffset { get; }

        [MemoryAddress (CountLength = 4, BytesToSkip = 0x28)]
        Int64 City { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Club { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Continent { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Nation { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Competition { get; }

        /*
        [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x28)]
        int Language { get; } */

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Stadium { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Team { get; }

        [MemoryAddressAttribute (CountLength = 4, BytesToSkip = 0x28)]
        Int64 Person { get; }

        Int64 CurrentDateTime { get; }
        Int64 ActiveObject { get; }
    }
}
