using System;
using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System.Collections.Generic;

namespace FMScoutFramework.Core.Entities.InGame {
    public class ClubInfoTwo : BaseObject, IClubInfoTwo {
        private ClubInfoTwoOffsets ClubInfoTwoOffsets;
        public ClubInfoTwo(Int64 memoryAddress, IVersion version)
            :base(memoryAddress, version) {
            ClubInfoTwoOffsets = new ClubInfoTwoOffsets(version);
        }
        public ClubInfoTwo(Int64 memoryAddress, ArraySegment<byte>originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version) {
            ClubInfoTwoOffsets = new ClubInfoTwoOffsets(version);
        }

        public void Save() {
            PropertyInvoker.Set<short>(ClubInfoTwoOffsets.YearFounded, OriginalBytes, MemoryAddress, DatabaseMode, YearFounded);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.YouthImportance, OriginalBytes, MemoryAddress, DatabaseMode, YouthImportance);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.YouthFacilities, OriginalBytes, MemoryAddress, DatabaseMode, YouthFacilities);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.YouthRecruitment, OriginalBytes, MemoryAddress, DatabaseMode, YouthRecruitment);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.YouthAcademy, OriginalBytes, MemoryAddress, DatabaseMode, YouthAcademy);
            PropertyInvoker.Set<short>(ClubInfoTwoOffsets.ChairmanStatus, OriginalBytes, MemoryAddress, DatabaseMode, ChairmanStatus);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.TrainingFacilities, OriginalBytes, MemoryAddress, DatabaseMode, TrainingFacilities);
            PropertyInvoker.Set<byte>(ClubInfoTwoOffsets.Morale, OriginalBytes, MemoryAddress, DatabaseMode, Morale);
            _isDirty = false;
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

        public string SixLetterName {
            get {
                return PropertyInvoker.GetString(ClubInfoTwoOffsets.SixLetterName, -1, OriginalBytes, MemoryAddress, DatabaseMode);
            }
        }

        private short _yearFounded = 0;
        public short YearFounded {
            get {
                if (_yearFounded == 0) {
                    _yearFounded = PropertyInvoker.Get<short>(ClubInfoTwoOffsets.YearFounded, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _yearFounded;
            }
            set {
                if (_yearFounded != value) {
                    _yearFounded = value;
                    isDirty = true;
                }
            }
        }

        private byte _youthImportance = 0;
        public byte YouthImportance {
            get {
                if (_youthImportance == 0) {
                    _youthImportance = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.YouthImportance, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _youthImportance;
            }
            set {
                if (_youthImportance != value) {
                    _youthImportance = value;
                    isDirty = true;
                }
            }
        }

        private byte _youthFacilities = 0;
        public byte YouthFacilities {
            get {
                if (_youthFacilities == 0) {
                    _youthFacilities = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.YouthFacilities, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _youthFacilities;
            }
            set {
                if (_youthFacilities != value) {
                    _youthFacilities = value;
                    isDirty = true;
                }
            }
        }

        private byte _youthRecruitment = 0;
        public byte YouthRecruitment {
            get {
                if (_youthRecruitment == 0) {
                    _youthRecruitment = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.YouthRecruitment, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _youthRecruitment;
            }
            set {
                if (_youthRecruitment != value) {
                    _youthRecruitment = value;
                    isDirty = true;
                }
            }
        }

        private byte _youthAcademy = 0;
        public byte YouthAcademy {
            get {
                if (_youthAcademy == 0) {
                    _youthAcademy = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.YouthAcademy, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _youthAcademy;
            }
            set {
                if (_youthAcademy != value) {
                    _youthAcademy = value;
                    isDirty = true;
                }
            }
        }

        private short _chairmanStatus = 0;
        public short ChairmanStatus {
            get {
                if (_chairmanStatus == 0) {
                    _chairmanStatus = PropertyInvoker.Get<short>(ClubInfoTwoOffsets.ChairmanStatus, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _chairmanStatus;
            }
            set {
                if (_chairmanStatus != value) {
                    _chairmanStatus = value;
                    isDirty = true;
                }
            }
        }

        private byte _trainingFacilities = 0;
        public byte TrainingFacilities {
            get {
                if (_trainingFacilities == 0) {
                    _trainingFacilities = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.TrainingFacilities, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _trainingFacilities;
            }
            set {
                if (_trainingFacilities != value) {
                    _trainingFacilities = value;
                    isDirty = true;
                }
            }
        }

        private byte _morale = 0;
        public byte Morale {
            get {
                if (_morale == 0) {
                    _morale = PropertyInvoker.Get<byte>(ClubInfoTwoOffsets.Morale, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _morale;
            }
            set {
                if (_morale != value) {
                    _morale = value;
                    isDirty = true;
                }
            }
        }

        private List<ClubDebt> _debts = new List<ClubDebt>();
        public List<ClubDebt> Debts {
            get {
                int debtsCount = ProcessManager.ReadArrayLength(MemoryAddress + ClubInfoTwoOffsets.ClubDebts);
                if (debtsCount > 0) {
                    Int64 debtsArrayAddress = PropertyInvoker.Get<Int64>(ClubInfoTwoOffsets.ClubDebts, OriginalBytes, MemoryAddress, DatabaseMode);
                    for (int i = 0; i < debtsCount; i++) {
                        _debts.Add(PropertyInvoker.GetPointer<ClubDebt>(0x0, OriginalBytes, (debtsArrayAddress + (i * 0x8)), DatabaseMode, Version));
                    }
                }

                return _debts;
            }
        }
    }
}
