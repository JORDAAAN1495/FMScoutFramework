using FM.Pitchside.Core.Defines.Versions;
using System.ComponentModel;

namespace FM.Pitchside.Core.Entities.Ingame
{
    public enum Formation
    {
        [Description("Not Set")]
        NotSet = 0,
        [Description("4-4-2")]
        F442 = 3,
        [Description("3-5-2")]
        F352 = 6,
        [Description("4-4-2 Diamond Narrow")]
        F442DiamondNarrow = 7,
        [Description("3-4-3")]
        F343 = 9,
        [Description("3-4-1-2")]
        F3412 = 10,
        [Description("4-2-3-1 Wide")]
        F4231Wide = 21,
        [Description("4-2-2-2 DM Narrow")]
        F4222DMNarrow = 24,
        [Description("4-4-2 Diamond Wide")]
        F442DiamondWide = 25,
        [Description("4-1-2-3 DM Wide")]
        F4123DMWide = 28,
        [Description("4-2-4 Wide")]
        F424Wide = 34,
        [Description("4-1-2-3 DM Narrow")]
        F4123DMNarrow = 51
    }

    public class BaseObject
    {
        public Int64 MemoryAddress;
        public ArraySegment<byte> OriginalBytes;
        public IVersion Version;
        public DatabaseModeEnum DatabaseMode;

        public BaseObject(Int64 memoryAddress, IVersion version)
        {
            MemoryAddress = memoryAddress;
            Version = version;
            DatabaseMode = DatabaseModeEnum.Realtime;
        }

        public BaseObject(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
        {
            MemoryAddress = memoryAddress;
            OriginalBytes = originalBytes;
            DatabaseMode = DatabaseModeEnum.Cached;
            Version = version;
        }

        public static bool operator ==(BaseObject a, BaseObject b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                if ((object)a == null && (object)b == null)
                    return true;
                else
                    return false;

            if (a.MemoryAddress == b.MemoryAddress)
                return true;
            else
                return false;
        }

        public static bool operator !=(BaseObject a, BaseObject b)
        {
            if (!System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                if ((object)a == null && (object)b == null)
                    return false;
                else
                    return true;

            if (a.MemoryAddress != b.MemoryAddress)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.MemoryAddress.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.GetHashCode().Equals(obj.GetHashCode());
        }
    }
}