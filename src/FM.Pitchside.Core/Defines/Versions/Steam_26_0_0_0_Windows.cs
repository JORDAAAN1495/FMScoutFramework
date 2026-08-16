using FM.Pitchside.Core.Attributes;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Defines.Versions
{
    // FM26 moved to Unity/IL2CPP. The main "fm.exe" module is a thin Unity shell - the
    // simulation state this class reads about lives in a native plugin module
    // ("game_plugin.dll"), which GameManager.findFMProcess() now detects and uses as
    // fmProcess.BaseAddress instead of the main exe when present.
    //
    // Verified (via FM.Pitchside.OffsetFinder against a live FM26 process):
    //   - MainAddress / XorDistance: confirmed - the Continent slot resolves to exactly 7.
    //   - CurrentDateTime: confirmed - decodes to 2025-07-07, matching the live in-game
    //     date, and sits 0x920 bytes from MainAddress (same nearby global game-state
    //     struct). With this and Continent both verified, SupportsProcess() now succeeds
    //     end-to-end against a live process (confirmed via the demo app).
    //   - ProductVersion string: confirmed from the live process (this is a Unity engine
    //     build string, not an FM version number like FM22's "22.4.1+1662587").
    //
    // NOT verified - placeholders only:
    //   - Every table slot other than Continent. A full 0x10-0xC8 sweep at this
    //     MainAddress/XorDistance returns plausible non-garbage counts for every slot
    //     (so the table shape/stride is intact), but FM22's slot order does not carry
    //     over cleanly - e.g. the old "Nation" slot (0x68) now reads 2527 (too high for
    //     ~200 real nations) and the old "Person" slot (0x70) reads 502 (far too low for
    //     a player+staff database). The slots below keep FM22's offsets/names as a
    //     starting hypothesis only - confirm each against real in-game counts before
    //     trusting it, and never write through one of these until it's confirmed.
    //   - ActiveObject: not located yet.
    //   - PersonEnum / PersonOffsets: not located, and FM26 doesn't appear to use FM17-22's
    //     scheme at all. ~135,000 players were confirmed in the live game, but that count
    //     doesn't appear anywhere in the 0x10-0x1E0 table sweep, and a live capture (see
    //     below) shows type discrimination now goes through vtable pointers, not
    //     UID-tagged enum pointers. IVersionPersonEnumPointers/IPersonVersionOffsets below
    //     are left as unused 0x0 placeholders to satisfy the interface until the library
    //     grows a matching concept for the new scheme - see KnownVtables/KnownFieldOffsets.
    //
    // Live-captured facts (via a custom Cheat Engine injection at game_plugin.dll's
    // "select object" hook - byte pattern 89 D6 48 89 CF 49 8B 00 48 8D 54 - which puts the
    // selected object's pointer in r8; see KnownVtables/KnownFieldOffsets below for what
    // this has confirmed so far):
    //   - Concrete type is identified by comparing [object+0x0] (the object's vtable
    //     pointer) against a per-class constant, e.g. vtbPlayer/vtbClub/vtbNation - not by
    //     a UID-tagged pointer table like FM17-22.
    //   - String fields are pointers to {Int32 length}{UTF8 bytes} buffers, same encoding
    //     FM17-22 used (see ProcessManager.ReadString) - the underlying native engine's
    //     data types are unchanged, only how you reach an object has changed.
    internal class Steam_26_0_0_0_Windows : IIVersion
    {
        public IVersionMemoryAddresses MemoryAddresses { get; private set; }
        public IVersionPersonEnumPointers PersonEnum { get; private set; }
        public IPersonVersionOffsets PersonOffsets { get; private set; }
        public GameManager gameManager { get; set; }
        public bool isTouch { get; set; }

        public Steam_26_0_0_0_Windows(GameManager gm)
        {
            MemoryAddresses = new VersionMemoryAddresses();
            PersonEnum = new VersionPersonEnumPointers();
            PersonOffsets = new PersonVersionOffsets();
            gameManager = gm;
            isTouch = false;
        }

        public string Description
        {
            get
            {
                return "26.0 (Windows Steam, game_plugin.dll)";
            }
        }

        public bool SupportsProcess(FMProcess process, byte[] context)
        {
            FMCore.logger.LogWrite("Getting Continents count...");
            int numberOfObjects = GameManager.TryGetPointerObjects(MemoryAddresses.MainAddress, MemoryAddresses.Continent, ProcessManager.fmProcess, MemoryAddresses.XorDistance);
            if (numberOfObjects != 7)
            {
                FMCore.logger.LogWrite("Continents Count is wrong, returning false.");
                GameManager.LastErrorMessage = "Could not find Base Object offsets.";
                return false;
            }
            FMCore.logger.LogWrite("Continent Count Match!");

            FMCore.logger.LogWrite("Getting in-game date...");
            DateTime dt = ProcessManager.ReadDateTime(process.BaseAddress + MemoryAddresses.CurrentDateTime);
            if (dt.Year < 2018 || dt.Year > 2300)
            {
                FMCore.logger.LogWrite("In-game date is invalid.");
                GameManager.LastErrorMessage = "Invalid main date at offset.";
                return false;
            }

            FMCore.logger.LogWrite("In-game date correct! Version is a match.");
            if (!string.IsNullOrEmpty(process.VersionDescription))
            {
                if (process.VersionDescription != "6000.0.52f1-fm26-05f1 (87a0370e9917)")
                {
                    return false;
                }
            }
            else
            {
                process.VersionDescription = "6000.0.52f1-fm26-05f1 (87a0370e9917)";
            }
            return true;
        }

        public class VersionMemoryAddresses : IVersionMemoryAddresses
        {
            // Verified.
            public Int64 MainAddress { get { return 0x4E49568; } }
            public Int64 MainOffset { get { return 0x0; } }
            public Int64 XorDistance { get { return 0x80; } }
            public Int64 StringOffset { get { return 0x0; } }

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x30)]
            public Int64 Continent { get { return 0x30; } } // Verified - resolves to 7.

            // Verified - decodes to 2025-07-07, matching the live in-game date, and sits
            // 0x920 bytes from MainAddress (same nearby global game-state struct, matching
            // the FM17-22 pattern of MainAddress/CurrentDateTime being close together).
            public Int64 CurrentDateTime { get { return 0x4E49E88; } }

            // Not yet located.
            public Int64 ActiveObject { get { return 0x0; } } // TODO

            // Unverified - FM22 slot order kept as a starting hypothesis. See class comment.
            [MemoryAddress(CountLength = 4, BytesToSkip = 0x10)]
            public Int64 Award { get { return 0x10; } } // sweep count: 3884

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x18)]
            public Int64 City { get { return 0x18; } } // sweep count: 58349

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x20)]
            public Int64 Club { get { return 0x20; } } // sweep count: 19386

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x28)]
            public Int64 Competition { get { return 0x28; } } // sweep count: 8102

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x38)]
            public Int64 Currency { get { return 0x38; } } // sweep count: 251

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x40)]
            public Int64 Unknown1 { get { return 0x40; } } // sweep count: 173

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x48)]
            public Int64 Injury { get { return 0x48; } } // sweep count: 3104

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x50)]
            public Int64 MediaSource { get { return 0x50; } } // sweep count: 136

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x58)]
            public Int64 Language { get { return 0x58; } } // sweep count: 270

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x60)]
            public Int64 LocalRegion { get { return 0x60; } } // sweep count: 126

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x68)]
            public Int64 Nation { get { return 0x68; } } // sweep count: 2527 - too high for ~200 real nations, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x70)]
            public Int64 Person { get { return 0x70; } } // sweep count: 502 - far too low for the ~135,000 players confirmed live, definitely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x78)]
            public Int64 Unknown2 { get { return 0x78; } } // sweep count: 44868

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x80)]
            public Int64 Unknown3 { get { return 0x80; } } // sweep count: 924

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x88)]
            public Int64 Stadium { get { return 0x88; } } // sweep count: 28 - too low for Stadium, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x90)]
            public Int64 Unknown4 { get { return 0x90; } } // sweep count: 16230

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x98)]
            public Int64 Unknown5 { get { return 0x98; } } // sweep count: 436

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xA0)]
            public Int64 Team { get { return 0xA0; } } // sweep count: 286 - low for Team, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xA8)]
            public Int64 Weather { get { return 0xA8; } } // sweep count: 43794 - far too high for Weather, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xB0)]
            public Int64 Unknown6 { get { return 0xB0; } } // sweep count: 373

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xB8)]
            public Int64 Derby { get { return 0xB8; } } // sweep count: 77433 - far too high for Derby, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xC0)]
            public Int64 Agreement { get { return 0xC0; } } // sweep count: 1683

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xC8)]
            public Int64 FirstName { get { return 0xC8; } } // sweep count: 6 - far too low for FirstName, likely wrong slot.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xD0)]
            public Int64 LastName { get { return 0xD0; } } // sweep count: 0 - dead slot, likely wrong.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xD8)]
            public Int64 CommonName { get { return 0xD8; } } // sweep count: garbage - beyond the live table.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xE0)]
            public Int64 Unknown7 { get { return 0xE0; } } // sweep count: garbage - beyond the live table.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xE8)]
            public Int64 Unknown8 { get { return 0xE8; } } // sweep count: garbage - beyond the live table.

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xF0)]
            public Int64 Unknown9 { get { return 0xF0; } } // sweep count: 5
        }

        public class VersionPersonEnumPointers : IVersionPersonEnumPointers
        {
            // Not yet located - see class comment.
            public Int64 Player { get { return 0x0; } } // TODO
            public Int64 Staff { get { return 0x0; } } // TODO
            public Int64 PlayerStaff { get { return 0x0; } } // TODO
            public Int64 HumanManager { get { return 0x0; } } // TODO
            public Int64 Official { get { return 0x0; } } // TODO
        }

        public class PersonVersionOffsets : IPersonVersionOffsets
        {
            // Not yet located - see class comment.
            public Int64 Person { get { return 0x0; } } // TODO
            public Int64 Player { get { return 0x0; } } // TODO
            public Int64 Staff { get { return 0x0; } } // TODO
            public Int64 HumanManager { get { return 0x0; } } // TODO
            public Int64 PlayerStaff { get { return 0x0; } } // TODO
            public Int64 Official { get { return 0x0; } } // TODO
        }

        // Raw facts captured live from a running FM26 process, ahead of there being a real
        // reading pipeline for them (see class comment for how they were captured). Not
        // wired into IVersion/PropertyInvoker yet - this is just where verified numbers get
        // written down as they're found, so they don't get lost between sessions.
        public static class KnownVtables
        {
            // RVA relative to game_plugin.dll's base. [object+0x0] == base+this RVA
            // identifies the object's concrete type.
            // Verified against a live Player object (Leny Yoro, Manchester United).
            public const long Player = 0x4509D68;

            // Verified against a live Club object (Manchester United).
            public const long Club = 0x44C13A8;

            // Verified against a live Staff (non-playing backroom, e.g. Performance
            // Analyst) object - Eduardo Rosalino. Distinct from Player, confirming the
            // hook's [object+0x0] discrimination and Person's shared field layout both
            // hold across subtypes (PersonFullName below resolved correctly for both).
            public const long Staff = 0x4565018;

            // Verified against the live human-controlled manager object (the player's own
            // save - resolved full name "Jordan Kennedy" matched their manager profile).
            public const long HumanManager = 0x44A5238;
        }

        public static class KnownFieldOffsets
        {
            // Byte offset from a Person object's own address. Confirmed stable across at
            // least Player and Staff subtypes (base-class field layout).
            // Verified: [object+0x40] is a pointer to an FM-encoded string
            // (Int32 length + UTF8 bytes) holding the full name, e.g. "Leny Jean-Luc Yoro",
            // "Eduardo José Monteiro Rosalino".
            public const long PersonFullName = 0x40;

            // Club's full name is two hops away: [club+0x30] is a pointer to a sub-object
            // (identity/purpose not yet confirmed - possibly the same "team" sub-object
            // GameManager-era engines kept alongside Club), and [subObject+0xC0] is the
            // FM-encoded full name string pointer, e.g. "Manchester United".
            public const long ClubSubObject = 0x30;
            public const long ClubSubObjectFullName = 0xC0;

            // Squad enumeration chain, verified end-to-end against a live Club (Manchester
            // United): [club+0x88] is a pointer to a second, different sub-object (not the
            // same one ClubSubObject above reaches - Club has multiple distinct
            // sub-components), and [thatSubObject+0x78] is an array-bounds pair (same
            // {startPtr,endPtr} shape find-continents/scan-arrays use) - EXCEPT the element
            // stride here is 0x10, not 0x8: each entry is {Int64 personPointer, Int64
            // metadata (unidentified, looks like packed flags/status - not a pointer)}.
            // personPointer's own [0x0] is a Person-hierarchy vtable (confirmed Player;
            // very likely mixed with Staff too, not confirmed yet), and its [PersonFullName]
            // resolved to real live squad members (Bernardo Silva, Juan Mata, Gelson
            // Martins). One element had a null FullName pointer despite a valid Player
            // vtable - likely a regen without a cached full-name string; harmless, just
            // don't assume FullName is always non-null.
            public const long ClubSquadSubObject = 0x88;
            public const long SquadArrayField = 0x78;
            public const long SquadEntryStride = 0x10;
        }
    }
}
