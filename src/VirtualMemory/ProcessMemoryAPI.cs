using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FMScoutFramework
{
    [Flags]
    public enum AllocationType {
        Commit      = 0x1000,
        Reserve     = 0x2000,
        Decommit    = 0x4000,
        Release     = 0x8000,
        Reset       = 0x80000,
        Physical    = 0x400000,
        TopDown     = 0x100000,
        WriteWatch  = 0x200000,
        LargePages  = 0x20000000
    }

    [Flags]
    public enum MemoryProtection {
        Execute                     = 0x10,
        ExecuteRead                 = 0x20,
        ExecuteReadWrite            = 0x40,
        ExecuteWriteCopy            = 0x80,
        NoAccess                    = 0x01,
        ReadOnly                    = 0x02,
        ReadWrite                   = 0x04,
        WriteCopy                   = 0x08,
        GuardModifierFlag           = 0x100,
        NoCacheModifierFlag         = 0x200,
        WriteCombineModifierFlag    = 0x400
    }

    public enum ProcessorArchitecture {
        X86     = 0,
        x64     = 9,
        @Arm    = -1,
        Itanium = 6,
        Unknown = 0xFFFF
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo {
        public ProcessorArchitecture ProcessorArchitecture;  // WORD
        public uint PageSize; // DWORD
        public IntPtr MinimumApplicationAddress;
        public IntPtr MaximumApplicationAddress;
        public IntPtr ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public ushort ProcessorLevel;
        public ushort ProcessorRevision;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SYSTEM_INFO_UNION {
        [FieldOffset(0)]
        public UInt32 OemId;
        [FieldOffset(0)]
        public UInt16 ProcessorArchitecture;
        [FieldOffset(2)]
        public UInt16 Reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SYSTEM_INFO {
        public UInt32 PageSize;
        public UInt32 MinimumApplicationAddress;
        public UInt32 MaximumApplicationAddress;
        public UInt32 ActiveProcessorMask;
        public UInt32 NumberOfProcessors;
        public UInt32 ProcessorType;
        public UInt32 AllocationGranularity;
        public UInt16 ProcessorLevel;
        public UInt16 ProcessorRevision;
    }

    internal sealed class ProcessMemoryAPI
    {
#if MAC
        [DllImport("libprocessmemoryapi.dylib")]
        public static extern IntPtr ReadProcessBytes (uint ptask, UInt64 address, int size);

        [DllImport("libprocessmemoryapi.dylib")]
        public static extern bool CanReadAtAddress(uint ptask, UInt64 address, int size);

        [DllImport ("libprocessmemoryapi.dylib")]
        public static extern IntPtr ZGGetASLROffset (uint ptask);

        [DllImport ("libprocessmemoryapi.dylib")]
        public static extern uint GetProcessTaskForPID (int pid);

        [DllImport("libprocessmemoryapi.dylib")]
        public static extern IntPtr AllocateProcessBytes(uint ptask, int size);

        [DllImport("libprocessmemoryapi.dylib")]
        public static extern bool WriteProcessMemory(uint ptask, UInt64 address, [In, Out] byte[] buffer, uint size);
#endif
#if WINDOWS

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo(out SYSTEM_INFO Info);
#endif
    }
}
