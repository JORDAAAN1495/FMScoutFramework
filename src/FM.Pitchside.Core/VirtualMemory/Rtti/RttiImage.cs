#nullable enable
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace FM.Pitchside.Core.VirtualMemory.Rtti
{
    // MSVC x64 RTTI reader.
    //
    // FM26/FM27's game_plugin.dll is a native MSVC x64 module that still carries RTTI, so every
    // polymorphic game class (TEAM@db, CLUB_HUMAN_MANAGER_DETAILS, ...) can be located *by name*
    // instead of by brute-forcing data tables. The layout used here is the standard, publicly
    // documented MSVC x64 one:
    //
    //   vtable[-1]                  -> 8-byte absolute pointer to the Complete Object Locator (COL)
    //   COL  +0  u32 signature      -> 1 on x64
    //        +4  i32 offset         -> byte offset of this vtable's sub-object inside the complete
    //                                  object (non-zero for the 2nd+ base of a class with multiple
    //                                  inheritance)
    //        +8  i32 cdOffset       -> constructor displacement
    //        +12 u32 pTypeDescriptor-> RVA
    //        +16 u32 pClassHierarchy-> RVA
    //        +20 u32 pSelf          -> RVA of this COL (x64 sanity check)
    //   TypeDescriptor +0 ptr vftable, +8 spare (0), +16 decorated name ".?AV<Name>@<Scope>@@\0"
    //   ClassHierarchy +0 sig, +4 attrs, +8 u32 numBases, +12 u32 RVA of base-descriptor RVA array
    //   BaseClassDescriptor +0 u32 pTypeDescriptor, +4 numContainedBases, +8 PMD{mdisp,pdisp,vdisp}
    //
    // The image is held in *virtual layout* (byte index == RVA), either read from a live process or
    // built from a DLL on disk, so the same code serves both modes.

    /// <summary>One COL: a vtable "part" of a class.</summary>
    public readonly struct ObjectLocator
    {
        public ObjectLocator(long rva, int subObjectOffset, int constructorDisplacement, long typeDescriptorRva, long classHierarchyRva)
        {
            Rva = rva;
            SubObjectOffset = subObjectOffset;
            ConstructorDisplacement = constructorDisplacement;
            TypeDescriptorRva = typeDescriptorRva;
            ClassHierarchyRva = classHierarchyRva;
        }

        public long Rva { get; }

        /// <summary>Byte offset of this vtable's sub-object inside the complete object.</summary>
        public int SubObjectOffset { get; }

        public int ConstructorDisplacement { get; }
        public long TypeDescriptorRva { get; }
        public long ClassHierarchyRva { get; }
    }

    public sealed class BaseClassInfo
    {
        public BaseClassInfo(string name, int memberDisplacement)
        {
            Name = name;
            MemberDisplacement = memberDisplacement;
        }

        public string Name { get; }
        public int MemberDisplacement { get; }
    }

    public sealed class VtableInfo
    {
        public VtableInfo(long vtableRva, ObjectLocator locator, string rawName, IReadOnlyList<BaseClassInfo> bases)
        {
            VtableRva = vtableRva;
            Locator = locator;
            RawName = rawName;
            Bases = bases;
        }

        public long VtableRva { get; }
        public ObjectLocator Locator { get; }
        public string RawName { get; }
        public IReadOnlyList<BaseClassInfo> Bases { get; }
    }

    public sealed class RttiImage
    {
        private static readonly byte[] DecoratedPrefix = { (byte)'.', (byte)'?', (byte)'A' };

        public byte[] Bytes { get; }
        public long ImageBase { get; }

        public RttiImage(byte[] bytes, long imageBase)
        {
            Bytes = bytes;
            ImageBase = imageBase;
        }

        /// <summary>Reads a loaded module out of the attached FM process. Unreadable pages stay zero.</summary>
        public static RttiImage FromLiveModule(long moduleBase, long moduleSize)
        {
            if (moduleSize <= 0 || moduleSize > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(moduleSize));
            }

            var buffer = new byte[moduleSize];
            const int chunk = 16 * 1024 * 1024;
            for (long offset = 0; offset < moduleSize; offset += chunk)
            {
                int length = (int)Math.Min(chunk, moduleSize - offset);
                byte[] part = ProcessManager.ReadProcessMemory(moduleBase + offset, (uint)length, out _);
                if (part != null)
                {
                    Buffer.BlockCopy(part, 0, buffer, (int)offset, Math.Min(length, part.Length));
                }
            }

            return new RttiImage(buffer, moduleBase);
        }

        /// <summary>Maps a PE32+ DLL/EXE on disk into virtual layout (what the loader would produce, minus relocations).</summary>
        public static RttiImage FromFile(string path)
        {
            byte[] file = File.ReadAllBytes(path);
            if (file.Length < 0x100 || file[0] != (byte)'M' || file[1] != (byte)'Z')
            {
                throw new InvalidDataException("Not a PE file (missing MZ header).");
            }

            int pe = BinaryPrimitives.ReadInt32LittleEndian(file.AsSpan(0x3C, 4));
            if (pe < 0 || pe + 0x108 > file.Length || BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(pe, 4)) != 0x00004550)
            {
                throw new InvalidDataException("Missing PE signature.");
            }

            ushort machine = BinaryPrimitives.ReadUInt16LittleEndian(file.AsSpan(pe + 4, 2));
            ushort magic = BinaryPrimitives.ReadUInt16LittleEndian(file.AsSpan(pe + 24, 2));
            if (machine != 0x8664 || magic != 0x20B)
            {
                throw new InvalidDataException("Only x64 (PE32+) images are supported.");
            }

            int sectionCount = BinaryPrimitives.ReadUInt16LittleEndian(file.AsSpan(pe + 6, 2));
            int optionalSize = BinaryPrimitives.ReadUInt16LittleEndian(file.AsSpan(pe + 20, 2));
            long imageBase = BinaryPrimitives.ReadInt64LittleEndian(file.AsSpan(pe + 24 + 24, 8));
            uint sizeOfImage = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(pe + 24 + 56, 4));
            uint sizeOfHeaders = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(pe + 24 + 60, 4));
            if (sizeOfImage == 0 || sizeOfImage > int.MaxValue)
            {
                throw new InvalidDataException("Unsupported SizeOfImage.");
            }

            var image = new byte[sizeOfImage];
            Buffer.BlockCopy(file, 0, image, 0, (int)Math.Min(Math.Min(sizeOfHeaders, (uint)file.Length), sizeOfImage));

            for (int i = 0; i < sectionCount; i++)
            {
                int header = pe + 24 + optionalSize + 40 * i;
                if (header + 40 > file.Length)
                {
                    throw new InvalidDataException("Truncated section table.");
                }

                uint virtualSize = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(header + 8, 4));
                uint virtualAddress = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(header + 12, 4));
                uint rawSize = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(header + 16, 4));
                uint rawPointer = BinaryPrimitives.ReadUInt32LittleEndian(file.AsSpan(header + 20, 4));

                long copy = virtualSize == 0 ? rawSize : Math.Min(rawSize, virtualSize);
                copy = Math.Min(copy, file.Length - (long)rawPointer);
                copy = Math.Min(copy, image.Length - (long)virtualAddress);
                if (copy > 0)
                {
                    Buffer.BlockCopy(file, (int)rawPointer, image, (int)virtualAddress, (int)copy);
                }
            }

            return new RttiImage(image, imageBase);
        }

        public bool TryU32(long rva, out uint value)
        {
            if (rva < 0 || rva + 4 > Bytes.Length)
            {
                value = 0;
                return false;
            }

            value = BinaryPrimitives.ReadUInt32LittleEndian(Bytes.AsSpan((int)rva, 4));
            return true;
        }

        public bool TryI32(long rva, out int value)
        {
            bool ok = TryU32(rva, out uint raw);
            value = unchecked((int)raw);
            return ok;
        }

        public bool TryU64(long rva, out ulong value)
        {
            if (rva < 0 || rva + 8 > Bytes.Length)
            {
                value = 0;
                return false;
            }

            value = BinaryPrimitives.ReadUInt64LittleEndian(Bytes.AsSpan((int)rva, 8));
            return true;
        }

        /// <summary>Decorated name stored in a TypeDescriptor (e.g. ".?AVTEAM@db@@"), or null if it isn't one.</summary>
        public string? TypeDescriptorName(long typeDescriptorRva)
        {
            long start = typeDescriptorRva + 16;
            if (typeDescriptorRva < 0 || start + 4 > Bytes.Length)
            {
                return null;
            }

            var span = Bytes.AsSpan((int)start, (int)Math.Min(512, Bytes.Length - start));
            int end = span.IndexOf((byte)0);
            if (end < 6)
            {
                return null;
            }

            string name = Encoding.ASCII.GetString(span.Slice(0, end).ToArray());
            return name.StartsWith(".?A", StringComparison.Ordinal) && name.EndsWith("@@", StringComparison.Ordinal)
                ? name
                : null;
        }

        private bool LooksLikeTypeDescriptor(long typeDescriptorRva) =>
            TryU64(typeDescriptorRva + 8, out ulong spare) && spare == 0 && TypeDescriptorName(typeDescriptorRva) != null;

        /// <summary>Every TypeDescriptor in the image whose decorated name contains <paramref name="filter"/> (null = all).</summary>
        public List<(long TypeDescriptorRva, string Raw)> FindTypeDescriptors(string? filter = null)
        {
            var results = new List<(long, string)>();
            int pos = 0;
            while (pos < Bytes.Length - 8)
            {
                int idx = Bytes.AsSpan(pos).IndexOf(DecoratedPrefix);
                if (idx < 0)
                {
                    break;
                }

                int at = pos + idx;
                pos = at + 1;
                if (at < 16 || at + 4 > Bytes.Length || (Bytes[at + 3] != (byte)'V' && Bytes[at + 3] != (byte)'U'))
                {
                    continue;
                }

                long td = at - 16;
                if (!LooksLikeTypeDescriptor(td))
                {
                    continue;
                }

                string raw = TypeDescriptorName(td)!;
                if (filter == null
                    || raw.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0
                    || Pretty(raw).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add((td, raw));
                }
            }

            return results;
        }

        /// <summary>TypeDescriptors whose decorated name is exactly <paramref name="raw"/>.</summary>
        public List<long> FindExactTypeDescriptors(string raw)
        {
            var results = new List<long>();
            byte[] needle = Encoding.ASCII.GetBytes(raw + "\0");
            int pos = 0;
            while (pos < Bytes.Length)
            {
                int idx = Bytes.AsSpan(pos).IndexOf(needle);
                if (idx < 0)
                {
                    break;
                }

                int at = pos + idx;
                pos = at + 1;
                if (at >= 16 && LooksLikeTypeDescriptor(at - 16))
                {
                    results.Add(at - 16);
                }
            }

            return results;
        }

        /// <summary>Complete Object Locators that reference the given TypeDescriptor (one per vtable "part").</summary>
        public List<ObjectLocator> LocatorsFor(long typeDescriptorRva)
        {
            var results = new List<ObjectLocator>();
            byte[] needle = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(needle, (uint)typeDescriptorRva);

            int pos = 0;
            while (pos < Bytes.Length)
            {
                int idx = Bytes.AsSpan(pos).IndexOf(needle);
                if (idx < 0)
                {
                    break;
                }

                int at = pos + idx;
                pos = at + 1;
                if (at % 4 != 0)
                {
                    continue;
                }

                if (TryReadLocator(at - 12, out ObjectLocator locator) && locator.TypeDescriptorRva == typeDescriptorRva)
                {
                    results.Add(locator);
                }
            }

            return results;
        }

        private bool TryReadLocator(long colRva, out ObjectLocator locator)
        {
            locator = default;
            if (colRva < 0 || colRva % 4 != 0 || colRva + 24 > Bytes.Length)
            {
                return false;
            }

            if (!TryU32(colRva, out uint signature) || signature != 1
                || !TryU32(colRva + 20, out uint self) || self != colRva
                || !TryI32(colRva + 4, out int offset)
                || !TryI32(colRva + 8, out int cdOffset)
                || !TryU32(colRva + 12, out uint typeDescriptor)
                || !TryU32(colRva + 16, out uint hierarchy))
            {
                return false;
            }

            locator = new ObjectLocator(colRva, offset, cdOffset, typeDescriptor, hierarchy);
            return true;
        }

        /// <summary>RVAs of vtables whose [-1] slot points at the given COL.</summary>
        public List<long> VtablesFor(long colRva)
        {
            var results = new List<long>();
            byte[] needle = BitConverter.GetBytes((ulong)(ImageBase + colRva));
            int pos = 0;
            while (pos < Bytes.Length)
            {
                int idx = Bytes.AsSpan(pos).IndexOf(needle);
                if (idx < 0)
                {
                    break;
                }

                int at = pos + idx;
                pos = at + 1;
                if (at % 8 == 0)
                {
                    results.Add(at + 8);
                }
            }

            return results;
        }

        /// <summary>
        /// Every vtable of the named class ("TEAM@db", "db::TEAM" or ".?AVTEAM@db@@"), one per
        /// sub-object, ordered by sub-object offset. Empty if the class isn't in this image.
        /// </summary>
        public List<(long VtableRva, int SubObjectOffset)> FindVtables(string className)
        {
            var results = new List<(long, int)>();
            foreach (string raw in RawCandidates(className))
            {
                foreach (long td in FindExactTypeDescriptors(raw))
                {
                    foreach (ObjectLocator col in LocatorsFor(td))
                    {
                        foreach (long vtable in VtablesFor(col.Rva))
                        {
                            results.Add((vtable, col.SubObjectOffset));
                        }
                    }
                }
            }

            return results.OrderBy(r => r.Item2).ThenBy(r => r.Item1).ToList();
        }

        /// <summary>Names the class that owns the vtable starting at <paramref name="vtableRva"/>, or null if [-1] isn't a valid COL.</summary>
        public VtableInfo? Describe(long vtableRva)
        {
            if (!TryU64(vtableRva - 8, out ulong colPointer))
            {
                return null;
            }

            long colRva = unchecked((long)colPointer) - ImageBase;
            if (!TryReadLocator(colRva, out ObjectLocator locator))
            {
                return null;
            }

            string? raw = TypeDescriptorName(locator.TypeDescriptorRva);
            if (raw == null)
            {
                return null;
            }

            var bases = new List<BaseClassInfo>();
            long hierarchy = locator.ClassHierarchyRva;
            if (TryU32(hierarchy + 8, out uint baseCount) && baseCount <= 256
                && TryU32(hierarchy + 12, out uint arrayRva))
            {
                for (uint i = 0; i < baseCount; i++)
                {
                    if (!TryU32(arrayRva + 4L * i, out uint descriptor)
                        || !TryU32(descriptor, out uint baseTypeDescriptor)
                        || !TryI32(descriptor + 8L, out int memberDisplacement))
                    {
                        continue;
                    }

                    string? baseName = TypeDescriptorName(baseTypeDescriptor);
                    if (baseName != null)
                    {
                        bases.Add(new BaseClassInfo(baseName, memberDisplacement));
                    }
                }
            }

            return new VtableInfo(vtableRva, locator, raw, bases);
        }

        /// <summary>".?AVName@Scope@@" -> "Scope::Name". Templates (?$) are left decorated.</summary>
        public static string Pretty(string raw)
        {
            if (raw.Length < 7 || raw.IndexOf("?$", StringComparison.Ordinal) >= 0)
            {
                return raw;
            }

            string[] parts = raw.Substring(4, raw.Length - 6).Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(parts);
            return string.Join("::", parts);
        }

        /// <summary>Accepts "TEAM@db", "db::TEAM" or a full ".?AVTEAM@db@@" and returns the decorated names to try.</summary>
        public static IEnumerable<string> RawCandidates(string input)
        {
            string body = input.IndexOf("::", StringComparison.Ordinal) >= 0
                ? string.Join("@", Enumerable.Reverse(input.Split(new[] { "::" }, StringSplitOptions.None)))
                : input;

            if (body.StartsWith(".?A", StringComparison.Ordinal))
            {
                yield return body;
                yield break;
            }

            yield return ".?AV" + body + "@@";
            yield return ".?AU" + body + "@@";
        }
    }
}
