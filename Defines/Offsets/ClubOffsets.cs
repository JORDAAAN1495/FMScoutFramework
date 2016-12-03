using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets
{
    public sealed class ClubOffsets
    {
        public IVersion Version;

        public ClubOffsets (IVersion version)
        {
            this.Version = version;
        }

        // Consts are the same for every version
        public const short RowID = 0x8;
        public const short UID = 0xC;
        public const short Teams = 0x18;
        public const short ClubInfoOne = 0x98;

        public short Name {
            get { return 0xA0; }
        }

        public short ShortName {
            get { return 0xA8; }
        }

        //public short ShortName {
        //    get { return 0xB0; }
        //}

        public short Nation {
            get { return 0xB8; }
        }

        public short BasedNation {
            get { return 0xD0; }
        }

        public short City {
            get { return 0xD8; }
        }

        public short ClubInfoTwo {
            get { return 0xE0; }
        }

        public short ClubSponshorshipDeals {
            get { return 0x110; }
        }

        public short ClubFinances {
            get { return 0x130; }
        }
    }
}