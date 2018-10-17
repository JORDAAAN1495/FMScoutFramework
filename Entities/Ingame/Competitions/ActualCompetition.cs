using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Defines.Offsets;
using FMScoutFramework.Core.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;

namespace FMScoutFramework.Core.Entities.InGame
{
    public class ActualCompetition : BaseObject, IActualCompetition
    {
        public ActualCompetitionOffsets Offsets;
        public ActualCompetition(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version){
            this.Offsets = new ActualCompetitionOffsets(Version);
        }
        public ActualCompetition(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version){
            this.Offsets = new ActualCompetitionOffsets(Version);
        }

        public void Save() {
            
            isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty {
            get {
                return _isDirty;
            }
            set {
                if (value) {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        private List<LeagueStage> _leagueStages = new List<LeagueStage>();
        public List<LeagueStage> LeagueStages {
            get {
                if (_leagueStages.Count == 0) {
                    bool readStagesAtOne = false;
                    bool readStagesAtTwo = false;
                    Int64 countOne = ProcessManager.ReadArrayLength((MemoryAddress + Offsets.StagesOne));
                    Int64 countTwo = ProcessManager.ReadArrayLength((MemoryAddress + Offsets.StagesTwo));
                    if (countOne > 0) {
                        readStagesAtOne = true;
                    }
                    if (countTwo > 0) {
                        readStagesAtTwo = true;
                    }

                    int masterCount = 1;
                    if (readStagesAtOne) {
                        Int64 startAddress = PropertyInvoker.Get<Int64>(Offsets.StagesOne, OriginalBytes, MemoryAddress, DatabaseMode);
                        if (startAddress > 0x0) {
                            for (Int64 i = 0; i < countOne; i++) {
                                LeagueStage ls = PropertyInvoker.GetPointer<LeagueStage>((i * 0x8), OriginalBytes, startAddress, DatabaseMode, Version);
                                ls.Name = "Stage " + masterCount;
                                _leagueStages.Add(ls);
                                masterCount++;
                            }
                        }
                    }
                    if (readStagesAtTwo) {
                        Int64 startAddress = PropertyInvoker.Get<Int64>(Offsets.StagesTwo, OriginalBytes, MemoryAddress, DatabaseMode);
                        if (startAddress > 0x0) {
                            // for (Int64 i = 0; i < countTwo; i++) {
                                LeagueStage ls = PropertyInvoker.GetPointer<LeagueStage>(0x0, OriginalBytes, startAddress, DatabaseMode, Version);
                                ls.Name = "Stage " + masterCount;
                                _leagueStages.Add(ls);
                                masterCount++;
                            // }
                        }
                    }
                }

                return _leagueStages;
            }
        }
    }
}
