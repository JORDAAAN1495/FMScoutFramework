using FM.Pitchside.Core.Attributes;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Defines.Versions
{
    // FM27 (Unity 6000.3, native game_plugin.dll). Unlike FM17-FM26 this version does NOT use the flat
    // MainAddress/XorDistance master table: it is located entirely through MSVC RTTI (see Fm27Game).
    //
    // How it works:
    //   - game_plugin.dll is a native MSVC x64 module that still carries RTTI, so every polymorphic
    //     game class can be found by name. VirtualMemory/Rtti/RttiImage.cs walks type descriptor ->
    //     Complete Object Locator -> vtable for a class name such as CLUB_HUMAN_MANAGER_DETAILS.
    //   - Live objects are then found by scanning the process heap for that vtable's address (an
    //     object's first qword is its vtable pointer) - ProcessHeap.ScanQwords.
    //   - Nothing is an absolute address or a per-build RVA, so a patch that only moves code does not
    //     break it; the class names and the field layouts in Fm27Game.Layout are what matter.
    //
    // Verified live (FM27, "6000.3.18f1-custom (96cdb67fb884)", Manchester United save):
    //   - Process/module detection: "fm" process and "game_plugin.dll" work exactly as for FM26.
    //   - CLUB_HUMAN_MANAGER_DETAILS: one instance per human manager. +0x40 team, team+0x30 club,
    //     club+0xC0 -> length-prefixed club name.
    //   - db::HUMAN_NON_PLAYER (lowest sub-object offset): the manager's person record. It is tied to
    //     the details object by record+0x4E8 (contract) -> contract+0x10 == team. Names sit at
    //     +0x480 (full), +0x490/+0x498/+0x4A0 (first/second/common name-table entries).
    //   - Players: a player is db::PLAYING_DATA (sub-object +0) + db::ACTUAL_PLAYER at sub-object +648
    //     (the "id" vtable, +320 from the block start) and +688 ("person", +360). Instances are found by
    //     scanning for the +648 vtable and confirming the other two sit at their fixed distances. The
    //     legacy Player entity's base is that +648 sub-object. Checked against real players: Leny Yoro
    //     (b. 2005-11-13, 190 cm, France) and Bruno Fernandes (b. 1994-09-08, 179 cm, Portugal) decode
    //     exactly; over ~124k players CA/PA/height/birth date are 100% in range.
    //
    // History worth keeping: the earlier lead at RVA 0x4B05678 turned out to be
    // game_plugin::GAME_UI_HANDLER (a UI event handler), not a manager record, and the flat-table
    // brute force (Continent count == 7) found no contiguous table for FM27. Both dead ends are why
    // this version moved to RTTI.
    //
    // Scope: SupportsProcess() succeeds when the engine line and the RTTI classes resolve.
    //   - Game.FindHumanManagers() reads the human manager(s); Game.FindPlayers() reads every player
    //     (uid, names, nation, birth date, height, CA, PA, the 15 position proficiencies and the stored top 3+3 roles); both share one heap scan (Game.Refresh()).
    //   - Each player also carries owner club, current team, squad (First team / U21 / ...) and a loan flag;
    //     Game.FindSquads() groups players by team. Checked against famous players' real clubs and the human
    //     manager's own squad (Manchester United: First team 30, U21 26, U18 24). A loan and a B-team
    //     registration are indistinguishable in the contract, so IsOnLoan is a documented heuristic.
    //   - core.Players is populated with legacy Player entities so identity/name reads work, but the
    //     rest of that entity's offsets are still FM22-era and are NOT valid for FM27 - use FindPlayers().
    //   - Staff, clubs, teams and the other legacy tables are empty for this version. Mapping them means
    //     adding more RTTI-keyed readers like the ones above.
    internal class Steam_27_0_0_0_Windows : IIVersion, IFm27Version
    {
        public Fm27Game Game { get; private set; }
        public IVersionMemoryAddresses MemoryAddresses { get; private set; }
        public IVersionPersonEnumPointers PersonEnum { get; private set; }
        public IPersonVersionOffsets PersonOffsets { get; private set; }
        public GameManager gameManager { get; set; }
        public bool isTouch { get; set; }

        public Steam_27_0_0_0_Windows(GameManager gm)
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
                return "27.0 (Windows, game_plugin.dll, RTTI-based)";
            }
        }

        public bool SupportsProcess(FMProcess process, byte[] context)
        {
            FMCore.logger.LogWrite("Resolving FM27 classes through RTTI...");
            if (!Fm27Game.TryCreate(process, out Fm27Game game, out string error))
            {
                FMCore.logger.LogWrite("Not FM27: " + error);
                GameManager.LastErrorMessage = error;
                return false;
            }

            Game = game;
            FMCore.logger.LogWrite("FM27 RTTI match: manager details vtable RVA 0x" + game.HumanManagerDetailsVtableRva.ToString("X"));
            return true;
        }

        public class VersionMemoryAddresses : IVersionMemoryAddresses
        {
            // Not used: FM27 is located through RTTI (see Fm27Game), so the legacy master-table
            // addresses stay 0x0. They exist only to satisfy IVersionMemoryAddresses.
            public Int64 MainAddress { get { return 0x0; } } // TODO - not found.
            public Int64 MainOffset { get { return 0x0; } }
            public Int64 XorDistance { get { return 0x0; } } // TODO - not found.
            public Int64 StringOffset { get { return 0x0; } }

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x30)]
            public Int64 Continent { get { return 0x0; } } // TODO - not found.

            public Int64 CurrentDateTime { get { return 0x0; } } // TODO - not found.

            public Int64 ActiveObject { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x10)]
            public Int64 Award { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x18)]
            public Int64 City { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x20)]
            public Int64 Club { get { return 0x0; } } // TODO - see class comment (ID 680 lead was a dead end).

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x28)]
            public Int64 Competition { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x38)]
            public Int64 Currency { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x40)]
            public Int64 Unknown1 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x48)]
            public Int64 Injury { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x50)]
            public Int64 MediaSource { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x58)]
            public Int64 Language { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x60)]
            public Int64 LocalRegion { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x68)]
            public Int64 Nation { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x78)]
            public Int64 Person { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x80)]
            public Int64 Unknown2 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x88)]
            public Int64 Unknown3 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x90)]
            public Int64 Stadium { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0x98)]
            public Int64 Unknown4 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xA0)]
            public Int64 Unknown5 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xA8)]
            public Int64 Team { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xB0)]
            public Int64 Weather { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xB8)]
            public Int64 Unknown6 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xC0)]
            public Int64 Derby { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xC8)]
            public Int64 Agreement { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xD0)]
            public Int64 FirstName { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xD8)]
            public Int64 LastName { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xE0)]
            public Int64 CommonName { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xE8)]
            public Int64 Unknown7 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xF0)]
            public Int64 Unknown8 { get { return 0x0; } } // TODO

            [MemoryAddress(CountLength = 4, BytesToSkip = 0xF8)]
            public Int64 Unknown9 { get { return 0x0; } } // TODO
        }

        public class VersionPersonEnumPointers : IVersionPersonEnumPointers
        {
            // Not used: person types are resolved by RTTI class, not by these legacy RVAs.
            public Int64 Player { get { return 0x0; } }
            public Int64 Staff { get { return 0x0; } }
            public Int64 PlayerStaff { get { return 0x0; } }
            public Int64 HumanManager { get { return 0x0; } }
            public Int64 Official { get { return 0x0; } }
        }

        public class PersonVersionOffsets : IPersonVersionOffsets
        {
            // TODO - not determined yet either way (unlike FM26, where all-0x0 was confirmed
            // correct; here it's simply unverified).
            public Int64 Person { get { return 0x0; } }
            public Int64 Player { get { return 0x0; } }
            public Int64 Staff { get { return 0x0; } }
            public Int64 HumanManager { get { return 0x0; } }
            public Int64 PlayerStaff { get { return 0x0; } }
            public Int64 Official { get { return 0x0; } }
        }
    }
}
