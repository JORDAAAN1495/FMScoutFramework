using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FMScoutFramework.Core.Entities;
using FMScoutFramework.Core.Entities.GameVersions;
using System.Drawing;

namespace FMScoutFramework.Core.Managers
{
    internal static class PropertyInvoker
    {
        public static T Get<T> (Int64 offset, ArraySegment<byte> baseObject, Int64 memoryAddress, DatabaseModeEnum databaseMode)
        {
            Int64 offsetToFind = memoryAddress + offset;

            if (typeof(Int16) == typeof(T))
                return (T)(object)ProcessManager.ReadInt16(offsetToFind);
            else if (typeof(Byte) == typeof(T))
                return (T)(object)ProcessManager.ReadByte(offsetToFind);
            else if (typeof(DateTime) == typeof(T))
                return (T)(object)ProcessManager.ReadDateTime(offsetToFind);
            else if (typeof(Int32) == typeof(T))
                return (T)(object)ProcessManager.ReadInt32(offsetToFind);
            else if (typeof(SByte) == typeof(T))
                return (T)(object)ProcessManager.ReadSByte(offsetToFind);
            else if (typeof(float) == typeof(T))
                return (T)(object)ProcessManager.ReadFloat(offsetToFind);
            else if (typeof(UInt32) == typeof(T))
                return (T)(object)ProcessManager.ReadUInt32(offsetToFind);
            else if (typeof(ushort) == typeof(T))
                return (T)(object)ProcessManager.ReadUInt16(offsetToFind);
            else if (typeof(Int64) == typeof(T))
                return (T)(object)ProcessManager.ReadInt64(offsetToFind);
            else if (typeof(UInt64) == typeof(T))
                return (T)(object)ProcessManager.ReadUInt64(offsetToFind);
            else if (typeof(Color) == typeof(T))
                return (T)(object)ProcessManager.ReadColour(offsetToFind);
            else
                return default(T);
        }

        public static void Set<T> (Int64 offset, ArraySegment<byte> baseObject, Int64 memoryAddress, DatabaseModeEnum databaseMode, T value)
        {
            Int64 offsetToFind = memoryAddress + offset;

            if (typeof(Int16) == typeof(T))
                ProcessManager.WriteInt16((short)(object)value, offsetToFind);
            else if (typeof(Byte) == typeof(T))
                ProcessManager.WriteByte((byte)(object)value, offsetToFind);
            else if (typeof(DateTime) == typeof(T))
                ProcessManager.WriteDateTime((DateTime)(object)value, offsetToFind);
            else if (typeof(Int32) == typeof(T))
                ProcessManager.WriteInt32((int)(object)value, offsetToFind);
            else if (typeof(SByte) == typeof(T))
                ProcessManager.WriteSByte((sbyte)(object)value, offsetToFind);
            else if (typeof(float) == typeof(T))
                ProcessManager.WriteFloat((float)(object)value, offsetToFind);
            else if (typeof(UInt32) == typeof(T))
                ProcessManager.WriteInt32((int)(object)value, offsetToFind);
            else if (typeof(ushort) == typeof(T))
                ProcessManager.WriteInt16((ushort)(object)value, offsetToFind);
            else if (typeof(Color) == typeof(T))
                ProcessManager.WriteColour((Color)(object)value, offsetToFind);
        }


        public static string GetString (Int64 offset, Int64 additionalStringOffset, ArraySegment<byte> baseObject, Int64 memoryAddress, DatabaseModeEnum databaseMode)
        {
            return ProcessManager.ReadString (memoryAddress + offset, additionalStringOffset);
        }

        private static Dictionary<Type, Func<Int64, IVersion, object>> pointerDelegateDictionary = new Dictionary<Type, Func<Int64, IVersion, object>> ();
        public static T GetPointer<T> (int offset, ArraySegment<byte> baseObject, int memoryAddress, DatabaseModeEnum databaseMode, IVersion version)
            where T : class
        {
            return GetPointer<T> ((Int64)offset, baseObject, (Int64)memoryAddress, databaseMode, version);
        }
        public static T GetPointer<T> (Int64 offset, ArraySegment<byte> baseObject, Int64 memoryAddress, DatabaseModeEnum databaseMode, IVersion version)
            where T : class
        {
            Int64 memAddress = memoryAddress + offset;

            if (!pointerDelegateDictionary.ContainsKey (typeof (T))) {
                System.Reflection.ConstructorInfo ci =
                    typeof (T).GetConstructor (new [] { typeof (int), typeof (IVersion) });

                ParameterExpression memAddressParam = Expression.Parameter (typeof (Int64), "memAdd");
                ParameterExpression versionParam = Expression.Parameter (typeof (IVersion), "version");

                LambdaExpression lambda = Expression.Lambda (
                    Expression.Convert (Expression.New (ci, memAddressParam, versionParam), typeof (object))
                    , memAddressParam, versionParam);

                lock (pointerDelegateLock) {
                    if (!pointerDelegateDictionary.ContainsKey (typeof (T))) {
                        pointerDelegateDictionary.Add (typeof (T),
                            (Func<Int64, IVersion, object>)lambda.Compile ());
                    }
                }
            }

            if (IntPtr.Size == 4) {
                return (T)pointerDelegateDictionary [typeof (T)].Invoke (ProcessManager.ReadInt32 (memAddress), version);
            } 
            else {
                return (T)pointerDelegateDictionary [typeof (T)].Invoke (ProcessManager.ReadInt64 (memAddress), version);
            }
        }

        private static object pointerDelegateLock = new object ();
    }
}
