using System;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Attributes;

namespace FMScoutFramework.Core.Entities.GameVersions {
  internal class Steam_19_0_3_Touch_Windows : IIVersion {
    public IVersionMemoryAddresses MemoryAddresses { get; private set; }
    public IVersionPersonEnumPointers PersonEnum { get; private set; }
    public IPersonVersionOffsets PersonOffsets { get; private set; }
    public GameManager gameManager { get; set; }

    public Steam_19_0_3_Touch_Windows(GameManager gm) {
      MemoryAddresses = new VersionMemoryAddresses();
      PersonEnum = new VersionPersonEnumPointers();
      PersonOffsets = new PersonVersionOffsets();
      gameManager = gm;
    }

    public string Description {
      get {
        return "19.0.3 Steam (Windows)";
      }
    }

    public bool SupportsProcess(FMProcess process, byte[] context) {
      #region WINDOWS
#if WINDOWS
      FMCore.logger.LogWrite("Getting Continents count...");
      int numberOfObjects = GameManager.TryGetPointerObjects(MemoryAddresses.MainAddress, MemoryAddresses.Continent, ProcessManager.fmProcess, MemoryAddresses.XorDistance);
      if (numberOfObjects != 7) {
        FMCore.logger.LogWrite("Continents Count is wrong, returning false.");
        GameManager.LastErrorMessage = "Could not find Base Object offsets.";
        return false;
      }
      FMCore.logger.LogWrite("Continent Count Match!");

      FMCore.logger.LogWrite("Getting in-game date...");
      DateTime dt = ProcessManager.ReadDateTime(process.BaseAddress + MemoryAddresses.CurrentDateTime);
      if (dt.Year < 2016 || dt.Year > 2300) {
        FMCore.logger.LogWrite("In-game date is invalid.");
        GameManager.LastErrorMessage = "Invalid main date at offset.";
        return false;
      }

      FMCore.logger.LogWrite("In-game date correct! Version is a match.");
      if (!string.IsNullOrEmpty(process.VersionDescription)) {
        if (process.VersionDescription != "19.0.3b1154609") {
          return false;
        }
      }
      else {
        process.VersionDescription = "19.0.3b1154609";
      }
      return true;
#endif
      #endregion
      #region MAC
#if MAC
            return false;
#endif
      #endregion
    }

    public class VersionMemoryAddresses : IVersionMemoryAddresses {
      // Statics
      public Int64 MainAddress { get { return 0x6975B90; } }
      public Int64 MainOffset { get { return 0x0; } }
      public Int64 XorDistance { get { return 0x88; } }
      public Int64 StringOffset { get { return 0x0; } }
      public Int64 CurrentDateTime { get { return 0x68784F8; } } // At BaseAddress + offset
      public Int64 ActiveObject { get { return 0x6A6C768; } } // BaseAddress + offset (ID: 5640119 / B7 0F 56 00)
      public Int64 TransferManager { get { return 0x5C2F210; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x10)]
      public Int64 Award { get { return 0x10; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x18)]
      public Int64 City { get { return 0x18; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x20)]
      public Int64 Club { get { return 0x20; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x28)]
      public Int64 Competition { get { return 0x28; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x30)]
      public Int64 Continent { get { return 0x30; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x38)]
      public Int64 Currency { get { return 0x38; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x40)]
      public Int64 Unknown1 { get { return 0x40; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x48)]
      public Int64 Injury { get { return 0x48; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x50)]
      public Int64 MediaSource { get { return 0x50; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x58)]
      public Int64 Language { get { return 0x58; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x60)]
      public Int64 LocalRegion { get { return 0x60; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x68)]
      public Int64 Nation { get { return 0x68; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x70)]
      public Int64 Person { get { return 0x70; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x78)]
      public Int64 Unknown2 { get { return 0x78; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x80)]
      public Int64 Unknown3 { get { return 0x80; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x88)]
      public Int64 Stadium { get { return 0x88; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x90)]
      public Int64 Unknown4 { get { return 0x90; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0x98)]
      public Int64 Unknown5 { get { return 0x98; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xA0)]
      public Int64 Team { get { return 0xA0; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xA8)]
      public Int64 Weather { get { return 0xA8; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xB0)]
      public Int64 Unknown6 { get { return 0xB0; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xB8)]
      public Int64 Derby { get { return 0xB8; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xC0)]
      public Int64 Agreement { get { return 0xC0; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xC8)]
      public Int64 FirstName { get { return 0xC8; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xD0)]
      public Int64 LastName { get { return 0xD0; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xD8)]
      public Int64 CommonName { get { return 0xD8; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xE0)]
      public Int64 Unknown7 { get { return 0xE0; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xE8)]
      public Int64 Unknown8 { get { return 0xE8; } }

      [MemoryAddressAttribute(CountLength = 4, BytesToSkip = 0xF0)]
      public Int64 Unknown9 { get { return 0xF0; } }
    }

    public class VersionPersonEnumPointers : IVersionPersonEnumPointers {
      public Int64 Player { get { return 0x62D6330; } }         // UID: 11133
      public Int64 Staff { get { return 0x62C6038; } }          // UID: 354
      public Int64 PlayerStaff { get { return 0x62ED7A8; } }    // UID: 104062
      public Int64 HumanManager { get { return 0x62C5628; } }     // UID: User Manager's
      public Int64 Official { get { return 0x0; } }         // NSY
      public Int64 NonPlayer { get { return 0x0; } }        // NSY
      public Int64 Retired { get { return 0x0; } }          // NSY
      public Int64 Spokesperson { get { return 0x0; } }     // NSY
      public Int64 AgentType { get { return 0x0; } }        // NSY
      public Int64 Journalist { get { return 0x0; } }       // NSY
    }

    public class PersonVersionOffsets : IPersonVersionOffsets {
      public Int64 Person { get { return -0xC4; } }
      public Int64 Player { get { return -0x1C0; } }
      public Int64 Staff { get { return -0xC8; } }
      public Int64 NonPlayer { get { return 0x0; } }
      public Int64 PlayerStaff { get { return -0x3B8; } }
      public Int64 Official { get { return 0x0; } }
      public Int64 Retired { get { return 0x0; } }
      public Int64 Spokesperson { get { return 0x0; } }
      public Int64 Agent { get { return 0x0; } }
      public Int64 Journalist { get { return 0x0; } }
      public Int64 HumanManager { get { return -0x458; } }
    }
  }
}