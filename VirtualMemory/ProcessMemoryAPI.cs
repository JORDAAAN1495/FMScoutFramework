using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FMScoutFramework
{
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
#endif
    }
}
