using FM.Pitchside.Core.Attributes;

namespace FM.Pitchside.Core.Defines.Versions
{
    public interface IVersionMemoryAddresses
    {
        Int64 MainAddress { get; }
        Int64 MainOffset { get; }
        Int64 XorDistance { get; }
        Int64 StringOffset { get; }
        Int64 CurrentDateTime { get; }
        Int64 ActiveObject { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x10)]
        Int64 Award { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x18)]
        Int64 City { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x20)]
        Int64 Club { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x28)]
        Int64 Competition { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x30)]
        Int64 Continent { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x38)]
        Int64 Currency { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x40)]
        Int64 Unknown1 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x48)]
        Int64 Injury { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x50)]
        Int64 MediaSource { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x58)]
        Int64 Language { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x60)]
        Int64 LocalRegion { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x68)]
        Int64 Nation { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x70)]
        Int64 Person { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x78)]
        Int64 Unknown2 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x80)]
        Int64 Unknown3 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x88)]
        Int64 Stadium { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x90)]
        Int64 Unknown4 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0x98)]
        Int64 Unknown5 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xA0)]
        Int64 Team { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xA8)]
        Int64 Weather { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xB0)]
        Int64 Unknown6 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xB8)]
        Int64 Derby { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xC0)]
        Int64 Agreement { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xC8)]
        Int64 FirstName { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xD0)]
        Int64 LastName { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xD8)]
        Int64 CommonName { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xE0)]
        Int64 Unknown7 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xE8)]
        Int64 Unknown8 { get; }

        [MemoryAddress(CountLength = 4, BytesToSkip = 0xF0)]
        Int64 Unknown9 { get; }
    }
}