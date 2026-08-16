using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Entities.InGame.Interfaces;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using System;
using System.ComponentModel;
using System.Drawing;

namespace FMScoutFramework.Core.Entities.InGame
{

    public enum KitType
    {
        [Description("Home")]
        KTHome = 0,
        [Description("Away")]
        KTAway = 1,
        [Description("Third")]
        KTThird = 2
    }

    public enum KitRecordType
    {
        [Description("Shirt")]
        KRTShirt = 1,
        [Description("Icon")]
        KRTIcon = 2,
        [Description("Text")]
        KRTText = 3,
        [Description("Shorts")]
        KRTShorts = 4,
        [Description("Socks")]
        KRTSocks = 5,
        [Description("Shirt")]
        KRTShirtAlt = 33
    }

    public class Kit : BaseObject, IKit
    {
        private KitOffsets KitOffsets;
        public Kit(Int64 memoryAddress, IVersion version)
            : base(memoryAddress, version)
        {
            KitOffsets = new KitOffsets(version);
        }
        public Kit(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
            : base(memoryAddress, originalBytes, version)
        {
            KitOffsets = new KitOffsets(version);
        }

        public void Save()
        {
            PropertyInvoker.Set<Color>(KitOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode, ForegroundColour);
            PropertyInvoker.Set<Color>(KitOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode, BackgroundColour);
            PropertyInvoker.Set<Color>(KitOffsets.OutlineColour, OriginalBytes, MemoryAddress, DatabaseMode, OutlineColour);
            PropertyInvoker.Set<Color>(KitOffsets.NumberColour, OriginalBytes, MemoryAddress, DatabaseMode, NumberColour);
            PropertyInvoker.Set<Color>(KitOffsets.OutlineNumberColour, OriginalBytes, MemoryAddress, DatabaseMode, OutlineNumberColour);
            PropertyInvoker.Set<byte>(KitOffsets.Outfield, OriginalBytes, MemoryAddress, DatabaseMode, OutfieldPlayer);
            PropertyInvoker.Set<byte>(KitOffsets.Style, OriginalBytes, MemoryAddress, DatabaseMode, Style);
            PropertyInvoker.Set<byte>(KitOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode, Type);
            PropertyInvoker.Set<byte>(KitOffsets.RecordType, OriginalBytes, MemoryAddress, DatabaseMode, RecordType);
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

        private Color _foregroundColour;
        public Color ForegroundColour
        {
            get
            {
                if (_foregroundColour.IsEmpty)
                {
                    _foregroundColour = PropertyInvoker.Get<Color>(KitOffsets.ForegroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _foregroundColour;
            }
            set
            {
                if (_foregroundColour != value)
                {
                    _foregroundColour = value;
                    isDirty = true;
                }
            }
        }

        private Color _backgroundColour;
        public Color BackgroundColour
        {
            get
            {
                if (_backgroundColour.IsEmpty)
                {
                    _backgroundColour = PropertyInvoker.Get<Color>(KitOffsets.BackgroundColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _backgroundColour;
            }
            set
            {
                if (_backgroundColour != value)
                {
                    _backgroundColour = value;
                    isDirty = true;
                }
            }
        }

        private Color _outlineColour;
        public Color OutlineColour
        {
            get
            {
                if (_outlineColour.IsEmpty)
                {
                    _outlineColour = PropertyInvoker.Get<Color>(KitOffsets.OutlineColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _outlineColour;
            }
            set
            {
                if (_outlineColour != value)
                {
                    _outlineColour = value;
                }
            }
        }

        private Color _numberColour;
        public Color NumberColour
        {
            get
            {
                if (_numberColour.IsEmpty)
                {
                    _numberColour = PropertyInvoker.Get<Color>(KitOffsets.NumberColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _numberColour;
            }
            set
            {
                if (_numberColour != value)
                {
                    _numberColour = value;
                    isDirty = true;
                }
            }
        }

        private Color _outlineNumberColour;
        public Color OutlineNumberColour
        {
            get
            {
                if (_outlineNumberColour.IsEmpty)
                {
                    _outlineNumberColour = PropertyInvoker.Get<Color>(KitOffsets.OutlineNumberColour, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _outlineNumberColour;
            }
            set
            {
                if (_outlineNumberColour != value)
                {
                    _outlineNumberColour = value;
                    isDirty = true;
                }
            }
        }

        private byte _outfieldPlayer = 0;
        public byte OutfieldPlayer
        {
            get
            {
                if (_outfieldPlayer == 0)
                {
                    _outfieldPlayer = PropertyInvoker.Get<byte>(KitOffsets.Outfield, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _outfieldPlayer;
            }
            set
            {
                if (_outfieldPlayer != value)
                {
                    _outfieldPlayer = value;
                    isDirty = true;
                }
            }
        }

        private byte _style = 0;
        public byte Style
        {
            get
            {
                if (_style == 0)
                {
                    _style = PropertyInvoker.Get<byte>(KitOffsets.Style, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _style;
            }
            set
            {
                if (_style != value)
                {
                    _style = value;
                    isDirty = true;
                }
            }
        }

        private byte _type = 0;
        public byte Type
        {
            get
            {
                if (_type == 0)
                {
                    _type = PropertyInvoker.Get<byte>(KitOffsets.Type, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    isDirty = true;
                }
            }
        }

        private byte _recordType = 0;
        public byte RecordType
        {
            get
            {
                if (_recordType == 0)
                {
                    _recordType = PropertyInvoker.Get<byte>(KitOffsets.RecordType, OriginalBytes, MemoryAddress, DatabaseMode);
                }
                return _recordType;
            }
            set
            {
                if (_recordType != value)
                {
                    _recordType = value;
                    isDirty = true;
                }
            }
        }


    }
}