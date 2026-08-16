using FM.Pitchside.Core.Defines.Offsets.PersonTypes.Sub_Entities;
using FM.Pitchside.Core.Defines.Versions;
using FM.Pitchside.Core.Entities.Ingame.Interfaces;
using FM.Pitchside.Core.VirtualMemory.Managers;

namespace FM.Pitchside.Core.Entities.Ingame.Persons.Sub_Entities
{
    public class PersonAttributes : BaseObject, IPersonAttributes
    {
        public PersonAttributes(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        { }
        public PersonAttributes(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        { }

        public void Save()
        {
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Adaptability, OriginalBytes, MemoryAddress, DatabaseMode, Adaptability);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Ambition, OriginalBytes, MemoryAddress, DatabaseMode, Ambition);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Loyalty, OriginalBytes, MemoryAddress, DatabaseMode, Loyalty);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Pressure, OriginalBytes, MemoryAddress, DatabaseMode, Pressure);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Professionalism, OriginalBytes, MemoryAddress, DatabaseMode, Professionalism);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Sportsmanship, OriginalBytes, MemoryAddress, DatabaseMode, Sportsmanship);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Temperament, OriginalBytes, MemoryAddress, DatabaseMode, Temperament);
            PropertyInvoker.Set<byte>(PersonAttributeOffsets.Controversy, OriginalBytes, MemoryAddress, DatabaseMode, Controversy);

            _isDirty = false;
        }

        private bool _isDirty = false;
        public bool isDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                if (value)
                {
                    Version.gameManager.RaiseObjectEdited(this);
                }
                _isDirty = value;
            }
        }

        private byte _adaptability = 0;
        public byte Adaptability
        {
            get
            {
                if (_adaptability == 0)
                {
                    _adaptability = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Adaptability, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _adaptability;
            }
            set
            {
                if (_adaptability != value)
                {
                    isDirty = true;
                    _adaptability = value;
                }
            }
        }

        private byte _ambition = 0;
        public byte Ambition
        {
            get
            {
                if (_ambition == 0)
                {
                    _ambition = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Ambition, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _ambition;
            }
            set
            {
                if (_ambition != value)
                {
                    isDirty = true;
                    _ambition = value;
                }
            }
        }

        private byte _loyalty = 0;
        public byte Loyalty
        {
            get
            {
                if (_loyalty == 0)
                {
                    _loyalty = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Loyalty, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _loyalty;
            }
            set
            {
                if (_loyalty != value)
                {
                    isDirty = true;
                    _loyalty = value;
                }
            }
        }

        private byte _pressure = 0;
        public byte Pressure
        {
            get
            {
                if (_pressure == 0)
                {
                    _pressure = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Pressure, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _pressure;
            }
            set
            {
                if (_pressure != value)
                {
                    isDirty = true;
                    _pressure = value;
                }
            }
        }

        private byte _professionalism = 0;
        public byte Professionalism
        {
            get
            {
                if (_professionalism == 0)
                {
                    _professionalism = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Professionalism, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _professionalism;
            }
            set
            {
                if (_professionalism != value)
                {
                    isDirty = true;
                    _professionalism = value;
                }
            }
        }

        private byte _sportsmanship = 0;
        public byte Sportsmanship
        {
            get
            {
                if (_sportsmanship == 0)
                {
                    _sportsmanship = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Sportsmanship, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _sportsmanship;
            }
            set
            {
                if (_sportsmanship != value)
                {
                    isDirty = true;
                    _sportsmanship = value;
                }
            }
        }

        private byte _temperament = 0;
        public byte Temperament
        {
            get
            {
                if (_temperament == 0)
                {
                    _temperament = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Temperament, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _temperament;
            }
            set
            {
                if (_temperament != value)
                {
                    isDirty = true;
                    _temperament = value;
                }
            }
        }

        private byte _controversy = 0;
        public byte Controversy
        {
            get
            {
                if (_controversy == 0)
                {
                    _controversy = PropertyInvoker.Get<byte>(PersonAttributeOffsets.Controversy, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _controversy;
            }
            set
            {
                if (_controversy != value)
                {
                    isDirty = true;
                    _controversy = value;
                }
            }
        }
    }
}