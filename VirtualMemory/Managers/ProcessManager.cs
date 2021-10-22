using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FMScoutFramework.Extensions;

namespace FMScoutFramework.Core.Managers
{
  public static class ProcessManager
  {
    public static FMProcess fmProcess = null;
    public static string FMVersion = null;
    public static string FMVersionShort = null;

    public static FMProcess FMProcess {
      get { return fmProcess; }
      set { fmProcess = value; }
    }

#if WINDOWS
    public static Int64 GetProcessEndPoint(IntPtr process) {
      Int64 bytesRead = 0;
      Int64 memoryAddress = 0x7fffffff;
      Int64 num3 = 0x1000000;
      for (int i = 1; i <= 7; i++) {
        ReadProcessMemory(process, memoryAddress, 1, out bytesRead);
        while (bytesRead == 0) {
          memoryAddress -= num3;
          ReadProcessMemory(process, memoryAddress, 1, out bytesRead);
        }
        memoryAddress += num3;
        num3 /= 0x10;
      }
      return memoryAddress;
    }
#endif
#if MAC
        public static int GetProcessEndPoint (uint pid)
        {
            int memoryAddress = 0x7FFFFFFF;
            int num3 = 0x1000000;
            bool readable = false;
            for (int i = 1; i <= 7; i++) {
                readable = ProcessMemoryAPI.CanReadAtAddress (pid, (UInt64)memoryAddress, 1);
                while (!readable) {
                    memoryAddress -= num3;
                    readable = ProcessMemoryAPI.CanReadAtAddress (pid, (UInt64)memoryAddress, 1);
                }
                memoryAddress += num3;
                num3 /= 0x10;
            }
            return memoryAddress;
        }

        public static Int64 GetASLROffset (uint pid)
        {
            IntPtr ptr = ProcessMemoryAPI.ZGGetASLROffset (pid);
            if (IntPtr.Size == 8) {
                return ptr.ToInt64 ();
            } else {
                return (Int64)ptr.ToInt32 ();
            }
        }
#endif
    #region ReadProcessMemoryExtensions

#if WINDOWS
    private static byte[] ReadProcessMemory(IntPtr process, Int64 memoryAddress, uint bytesToRead, out Int64 bytesRead) {
      IntPtr ptr;
      byte[] buffer = new byte[bytesToRead];
      ProcessMemoryAPI.ReadProcessMemory(process, (IntPtr)memoryAddress, buffer, bytesToRead, out ptr);
      if (IntPtr.Size == 4) {
        bytesRead = ptr.ToInt32();
      }
      else {
        bytesRead = ptr.ToInt64();
      }

      return buffer;
    }

    public static byte[] ReadProcessMemory(Int64 memoryAddress, uint bytesToRead) {
      if (memoryAddress > 0) {
        Int64 num;
        if (bytesToRead <= (32 * 1024 * 1024))
          return ReadProcessMemory(memoryAddress, bytesToRead, out num);
      }
      return new byte[8];
    }

    public static byte[] ReadProcessMemory(Int64 memoryAddress, uint bytesToRead, out Int64 bytesRead) {
      IntPtr ptr;
      byte[] buffer = new byte[bytesToRead];
      ProcessMemoryAPI.ReadProcessMemory(FMProcess.Pointer, (IntPtr)memoryAddress, buffer, bytesToRead, out ptr);
      if (IntPtr.Size == 4) {
        bytesRead = ptr.ToInt32();
      }
      else {
        bytesRead = ptr.ToInt64();
      }

      return buffer;
    }

    public static Int64 AllocateProcessBytes(int memorySize) {
      if (memorySize > 0) {
        IntPtr alloc = ProcessMemoryAPI.VirtualAllocEx(FMProcess.Pointer, IntPtr.Zero, (IntPtr)memorySize, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
        if (alloc != null) {
          if (IntPtr.Size == 4) {
            return alloc.ToInt32();
          }
          else {
            return alloc.ToInt64();
          }
        }
      }

      return 0;
    }
#endif
#if MAC
        public static byte [] ReadProcessMemory (int pid, int address, int length)
        {
            byte [] buffer = new byte [length];
            if (address > fmProcess.BaseAddress) {
                IntPtr result = ProcessMemoryAPI.ReadProcessBytes (fmProcess.ProcessTask, (UInt64)address, length);
                Marshal.Copy (result, buffer, 0, length);
            }
            return buffer;
        }

        public static byte [] ReadProcessMemory (int address, int length)
        {
            return ReadProcessMemory ((Int64)address, length);
        }

        public static byte [] ReadProcessMemory (Int64 address, int length)
        {
            byte [] buffer = new byte [length];
            if (address > fmProcess.BaseAddress) {
                IntPtr result = ProcessMemoryAPI.ReadProcessBytes (fmProcess.ProcessTask, (UInt64)address, length);
                Marshal.Copy (result, buffer, 0, length);
            }
            return buffer;
        }

        public static Int64 AllocateProcessBytes(int memorySize) {
            if (memorySize > 0) {
                IntPtr alloc = ProcessMemoryAPI.AllocateProcessBytes(FMProcess.ProcessTask, memorySize);
                if (alloc != null) {
                    if (IntPtr.Size == 4) {
                        return alloc.ToInt32();
                    }
                    else {
                        return alloc.ToInt64();
                    }
                }
            }

            return 0;
        }
#endif

    public static byte ReadByte(int address) {
      return ReadProcessMemory(address, 1)[0];
    }

    public static byte ReadByte(Int64 address) {
      return ReadProcessMemory(address, 1)[0];
    }

    public static bool ReadBool(Int64 address) {
      return (ReadProcessMemory(address, 1)[0] == 1) ? true : false;
    }

    public static sbyte ReadSByte(int address) {
      return (sbyte)ReadProcessMemory(address, 1)[0];
    }

    public static sbyte ReadSByte(Int64 address) {
      return (sbyte)ReadProcessMemory(address, 1)[0];
    }

    public static Int16 ReadInt16(int address) {
      return ReadInt16((Int64)address);
    }

    public static Int16 ReadInt16(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 2);
      return ReadInt16(buffer, 0);
    }

    public static float ReadFloat(int address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadFloat(buffer, 0);
    }

    public static float ReadFloat(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadFloat(buffer, 0);
    }

    public static double ReadDouble(int address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadDouble(buffer, 0);
    }

    public static double ReadDouble(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadDouble(buffer, 0);
    }

    public static Int64 ReadInt64(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 8);
      return ReadInt64(buffer, 0);
    }

    public static UInt64 ReadUInt64(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 8);
      return ReadUInt64(buffer, 0);
    }

    public static Int32 ReadInt32(int address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadInt32(buffer, 0);
    }

    public static Int32 ReadInt32(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadInt32(buffer, 0);
    }

    public static UInt32 ReadUInt32(int address) {
      byte[] buffer = ReadProcessMemory((int)address, 4);
      return ReadUInt32(buffer, 0);
    }

    public static UInt32 ReadUInt32(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 4);
      return ReadUInt32(buffer, 0);
    }

    public static ushort ReadUInt16(int address) {
      byte[] buffer = ReadProcessMemory(address, 2);
      return ReadUInt16(buffer, 0);
    }

    public static ushort ReadUInt16(Int64 address) {
      byte[] buffer = ReadProcessMemory(address, 2);
      return ReadUInt16(buffer, 0);
    }

    public static DateTime ReadDateTime(int address) {
      return ReadDateTime((Int64)address);
    }

    public static DateTime ReadDateTime(Int64 address) {
      int days = (ReadInt16(address) & 0x1FF);
      int years = ReadInt16(address + 0x2);
      if (days > 0 && days <= 366 && years > 1900 && years < 2150) {
        return FMScoutFramework.Core.Converters.DateConverter.FromFmDateTime((days - 1), years);
      }
      return new DateTime(1900, 1, 1);
    }

    public static Color ReadColour(Int64 address) {
      byte[] buffer = ProcessManager.ReadProcessMemory(address, 0x4);

      if (buffer == null) {
        return Color.FromArgb(0, 0, 0, 0);
      }

      byte alpha = buffer[3];
      byte blue = buffer[2];
      byte green = buffer[1];
      byte red = buffer[0];

      Color colour = Color.FromArgb(alpha, red, green, blue);
      return colour;
    }

    public static string ReadString(int currentAddress, int? addBufferIndex, bool isRead) {
      return ReadString((Int64)currentAddress, (Int64)addBufferIndex, 0, isRead);
    }

    public static string ReadString(int currentAddress, int? addBufferIndex) {
      return ReadString((Int64)currentAddress, (Int64)addBufferIndex, 0, false);
    }

    public static string ReadString(Int64 currentAddress, Int64? addBufferIndex) {
      return ReadString(currentAddress, addBufferIndex, 0, false);
    }

    private static Dictionary<string, string> readStringCache = new Dictionary<string, string>();
    public static string ReadString(Int64 currentAddress, Int64? addBufferIndex, Int64 offset, bool isRead) {
      //string cacheKey = string.Format ("{0}.{1}.{2}.{3}", currentAddress, addBufferIndex ?? -1, offset, isRead);
      //if (!readStringCache.ContainsKey (cacheKey)) {
      if (!isRead) {
        if (IntPtr.Size == 4) {
          currentAddress = ProcessManager.ReadInt32(currentAddress);
        }
        else {
          currentAddress = ProcessManager.ReadInt64(currentAddress);
        }
      }


      if (addBufferIndex > -1) {
        if (IntPtr.Size == 4) {
          currentAddress = ProcessManager.ReadInt32(currentAddress + (int)addBufferIndex);
        }
        else {
          currentAddress = ProcessManager.ReadInt64(currentAddress + (int)addBufferIndex);
        }
      }

      string str = "";

      // Get the string Length
      int length = (int)ProcessManager.ReadInt32(currentAddress);
      if (length <= 0) {
        return "-";
      }
      currentAddress += 0x4;

#if WINDOWS
      byte[] buffer = ProcessManager.ReadProcessMemory(currentAddress, (uint)length);
#endif

#if MAC
                byte [] buffer = ProcessManager.ReadProcessMemory (currentAddress, length);
#endif
      if (buffer.Length < length) {
        return "";
      }
      str = UnicodeEncoding.UTF8.GetString(buffer);

      // readStringCache.Add (cacheKey, str);
      //}
      // return readStringCache [cacheKey];
      return str;
    }

    public static byte[] GetFMStringBytes(string text) {
      List<byte> result = new List<byte>();
      if (text.Length > 0) {
        int length = text.Length;

        // Let's add the length
        result.AddRange(length.GetFMBytes());

        // Now add the string bytes to the array
        result.AddRange(UnicodeEncoding.UTF8.GetBytes(text));
      }

      return result.ToArray();
    }

    public static int ReadArrayLength(Int64 currentAddress) {
      return (int)ReadArrayLength(currentAddress, 0x8);
    }

    public static Int64 ReadArrayLength(Int64 currentAddress, int objectLength) {
      Int64 addressOne = ProcessManager.ReadInt64(currentAddress);
      Int64 addressTwo = ProcessManager.ReadInt64(currentAddress + 0x8);

      return ((addressTwo - addressOne) / objectLength);
    }

    public static int ReadArrayLength(int currentAddress) {
      return ReadArrayLength(currentAddress, 0x4);
    }

    public static int ReadArrayLength(int currentAddress, int objectLength) {
      int addressOne = ProcessManager.ReadInt32(currentAddress);
      int addressTwo = ProcessManager.ReadInt32(currentAddress + 0x4);

      return ((addressTwo - addressOne) / objectLength);
    }
    #endregion

    #region ReadFromBuffer
    private static Dictionary<int, string> stringCache = new Dictionary<int, string>();
    public static string ReadString(ArraySegment<byte> buffer, int offset, int additionalStringOffset) {
      int stringPointer = ReadInt32(buffer.Array, offset + buffer.Offset);
      if (!stringCache.ContainsKey(stringPointer))
        stringCache.Add(stringPointer, ReadString(stringPointer, -1, additionalStringOffset, true));

      return stringCache[stringPointer];
    }

    public static short ReadInt16(byte[] buffer, int offset) {
      return BitConverter.ToInt16(buffer, offset);
    }

    public static Int32 ReadInt32(byte[] buffer, int offset) {
      return BitConverter.ToInt32(buffer, offset);
    }

    public static Int64 ReadInt64(byte[] buffer, int offset) {
      return BitConverter.ToInt64(buffer, offset);
    }

    public static UInt64 ReadUInt64(byte[] buffer, int offset) {
      return BitConverter.ToUInt64(buffer, offset);
    }

    public static ushort ReadUInt16(byte[] buffer, int offset) {
      return BitConverter.ToUInt16(buffer, offset);
    }

    public static UInt32 ReadUInt32(byte[] buffer, int offset) {
      return BitConverter.ToUInt32(buffer, offset);
    }

    public static double ReadDouble(byte[] buffer, int offset) {
      return BitConverter.ToDouble(buffer, offset);
    }

    public static float ReadFloat(byte[] buffer, int offset) {
      return BitConverter.ToSingle(buffer, offset);
    }

    public static int GetAddress(byte[] buffer, int index) {
      int num = 0;
      try {
        num += buffer[index];
        num += buffer[index + 1] * 0x100;
        num += buffer[index + 2] * 0x10000;
        num += buffer[index + 3] * 0x1000000;
      }
      catch {
        return 0;
      }
      return num;
    }
    #endregion

    #region WriteProcessMemory
#if WINDOWS
    public static int WriteProcessMemory(Int64 memoryaddress, byte[] buffer, uint bytesToWrite) {
      IntPtr ptr;
      ProcessMemoryAPI.WriteProcessMemory(FMProcess.Pointer, (IntPtr)memoryaddress, buffer, bytesToWrite, out ptr);

      return ptr.ToInt32();
    }
#endif
#if MAC
        public static void WriteProcessMemory (Int64 memoryaddress, byte [] buffer, uint bytesToWrite)
        {
            ProcessMemoryAPI.WriteProcessMemory(FMProcess.ProcessTask, (UInt64)memoryaddress, buffer, bytesToWrite);
        }
#endif

    public static void WriteByte(byte value, Int64 address) {
      byte[] buffer = new byte[] { value };
      WriteProcessMemory(address, buffer, 1);
    }

    public static void WriteDateTime(DateTime value, Int64 address) {
      WriteInt16(value.DayOfYear, address);
      WriteInt16(value.Year, address + 2);
    }

    public static void WriteInt16(int value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 2);
    }

    public static void WriteUInt16(ushort value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 2);
    }

    public static void WriteInt32(int value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 4);
    }

    public static void WriteUInt32(uint value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 4);
    }

    public static void WriteInt64(Int64 value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 8);
    }

    public static void WriteFloat(float value, Int64 address) {
      byte[] buffer = BitConverter.GetBytes(value);
      WriteProcessMemory(address, buffer, 4);
    }

    public static void WriteSByte(sbyte value, Int64 address) {
      byte[] buffer = new byte[] { (byte)value };
      WriteProcessMemory(address, buffer, 1);
    }

    public static void WriteBool(bool value, Int64 address) {
      byte[] buffer = new byte[] { (value == true ? (byte)1 : (byte)0) };
      WriteProcessMemory(address, buffer, 1);
    }

    public static void WriteString(byte[] value, Int64 address) {
      WriteProcessMemory(address, value, 4);
    }

    public static void ResizeArray(int currentAddress, int newLength) {
      ResizeArray(currentAddress, 0x4, newLength);
    }

    public static void WriteColour(Color newColour, Int64 address) {
      byte[] buffer = new byte[4];

      buffer[0] = newColour.R;
      buffer[1] = newColour.G;
      buffer[2] = newColour.B;
      buffer[3] = newColour.A;

      WriteProcessMemory(address, buffer, 4);
    }

    public static void ResizeArray(int currentAddress, int objectLength, int newLength) {
      int addressOne = ProcessManager.ReadInt32(currentAddress);
      ProcessManager.WriteInt32(addressOne + objectLength * newLength, currentAddress + 0x4);
    }
    #endregion
  }
}
