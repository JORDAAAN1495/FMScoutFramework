using System.Buffers.Binary;
using System.Text;
using FM.Pitchside.Core.VirtualMemory.Rtti;

// rtti-* OffsetFinder commands. The RTTI reader itself (RttiImage and the MSVC layout notes) lives in
// FM.Pitchside.Core.VirtualMemory.Rtti.

internal static class RttiCommands
{
    /// <param name="read">Live-memory reader (address, length) -> bytes, or null if unreadable. Only rtti-object needs it.</param>
    public static void Run(string command, string[] args, RttiImage image, Func<long, int, byte[]?>? read = null)
    {
        Console.WriteLine($"RTTI image: 0x{image.Bytes.Length:X} bytes, base 0x{image.ImageBase:X}");
        Console.WriteLine();

        switch (command)
        {
            case "rtti-object":
                if (read == null)
                {
                    Console.WriteLine("rtti-object needs a live process (it can't run with --file).");
                    break;
                }

                WalkObject(args, image, read);
                break;
            case "rtti-name":
                Name(args, image);
                break;
            case "rtti-vtable":
                Vtable(args, image);
                break;
            case "rtti-list":
                List(args, image);
                break;
            default:
                Console.WriteLine($"Unknown RTTI command '{command}'.");
                break;
        }
    }

    // Walks the qwords of a live object. A qword that points at another object whose first qword is a
    // module vtable is labelled with that object's RTTI class, so a struct's links to other game
    // objects (person, club, ...) show up by name instead of as anonymous pointers.
    private static void WalkObject(string[] args, RttiImage image, Func<long, int, byte[]?> read)
    {
        if (args.Length < 1 || args[0].StartsWith("--", StringComparison.Ordinal))
        {
            Console.WriteLine("Usage: rtti-object <addressHex> [--count 48]   (live process, qword slots)");
            return;
        }

        string hex = args[0].Trim();
        long address = Convert.ToInt64(hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? hex[2..] : hex, 16);
        int count = Option(args, "--count") is { } c ? (int)ParseNumber(c) : 48;

        byte[]? block = read(address, count * 8);
        if (block == null)
        {
            Console.WriteLine($"Could not read {count * 8} bytes at 0x{address:X}.");
            return;
        }

        string Label(long moduleRelative)
        {
            VtableInfo? info = image.Describe(moduleRelative);
            return info == null ? "" : $"vtable of {RttiImage.Pretty(info.RawName)} (sub-object +{info.Locator.SubObjectOffset})";
        }

        for (int i = 0; i < count; i++)
        {
            ulong value = BinaryPrimitives.ReadUInt64LittleEndian(block.AsSpan(i * 8, 8));
            string note = "";
            long rel = unchecked((long)value) - image.ImageBase;

            if (rel > 0 && rel < image.Bytes.Length)
            {
                note = Label(rel);
                if (note == "")
                {
                    note = $"module RVA 0x{rel:X}";
                }
            }
            else if (value > 0x10000 && value < 0x7FFFFFFFFFFF)
            {
                // Heap-looking pointer: peek at the target's first qword.
                byte[]? head = read(unchecked((long)value), 8);
                if (head != null)
                {
                    long targetRel = unchecked((long)BinaryPrimitives.ReadUInt64LittleEndian(head)) - image.ImageBase;
                    if (targetRel > 0 && targetRel < image.Bytes.Length)
                    {
                        string label = Label(targetRel);
                        note = label == "" ? $"-> object, first qword = module RVA 0x{targetRel:X}" : $"-> object: {label}";
                    }
                }
            }

            if (note == "")
            {
                // Inline text or small values are common in these structs; show them when they look meaningful.
                var raw = block.AsSpan(i * 8, 8);
                int printable = 0;
                foreach (byte b in raw)
                {
                    if (b >= 0x20 && b < 0x7F)
                    {
                        printable++;
                    }
                }

                if (value != 0 && printable == 8)
                {
                    note = $"ascii \"{Encoding.ASCII.GetString(raw)}\"";
                }
                else if (value != 0 && value < 0x100000000UL)
                {
                    note = $"int32 {(int)(uint)value}";
                }
            }

            Console.WriteLine($"+0x{i * 8:X3}  0x{value:X16}  {note}");
        }
    }

    private static void Name(string[] args, RttiImage image)
    {
        if (args.Length < 1 || args[0].StartsWith("--", StringComparison.Ordinal))
        {
            Console.WriteLine("Usage: rtti-name <vtableRvaHex>   (RVA relative to the module base, e.g. 0x4B05678)");
            return;
        }

        // The RVA is always hex (matches how vtable RVAs are quoted everywhere else), with or without 0x.
        string hex = args[0].Trim();
        long rva = Convert.ToInt64(hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? hex[2..] : hex, 16);
        VtableInfo? info = image.Describe(rva);
        if (info == null)
        {
            Console.WriteLine($"No valid RTTI Complete Object Locator at vtable[-1] for RVA 0x{rva:X}.");
            Console.WriteLine("Either this isn't the start of a vtable (try a slightly lower/higher RVA), the class has no RTTI,");
            Console.WriteLine("or the address isn't inside the selected module.");
            return;
        }

        Console.WriteLine($"Vtable RVA 0x{info.VtableRva:X} (absolute 0x{image.ImageBase + info.VtableRva:X})");
        Console.WriteLine($"  Class         : {RttiImage.Pretty(info.RawName)}   [{info.RawName}]");
        Console.WriteLine($"  COL RVA       : 0x{info.Locator.Rva:X}");
        Console.WriteLine($"  Sub-object at : +{info.Locator.SubObjectOffset} (0x{info.Locator.SubObjectOffset:X}) inside the complete object");
        Console.WriteLine($"  cdOffset      : {info.Locator.ConstructorDisplacement}");

        if (info.Bases.Count > 0)
        {
            Console.WriteLine("  Hierarchy     :");
            foreach (BaseClassInfo b in info.Bases)
            {
                Console.WriteLine($"    {RttiImage.Pretty(b.Name),-48} member displacement {b.MemberDisplacement} (0x{b.MemberDisplacement:X})");
            }
        }

        Console.Write("  First slots   :");
        for (int i = 0; i < 4; i++)
        {
            if (!image.TryU64(info.VtableRva + 8L * i, out ulong slot))
            {
                break;
            }

            long slotRva = unchecked((long)slot) - image.ImageBase;
            if (slotRva <= 0 || slotRva >= image.Bytes.Length)
            {
                // Past the end of the vtable (a class with few virtuals runs into unrelated data).
                Console.Write(i == 0 ? " (slot 0 is not a pointer into the module)" : " [end of vtable]");
                break;
            }

            Console.Write($" 0x{slotRva:X}");
        }

        Console.WriteLine();
    }

    private static void Vtable(string[] args, RttiImage image)
    {
        if (args.Length < 1 || args[0].StartsWith("--", StringComparison.Ordinal))
        {
            Console.WriteLine("Usage: rtti-vtable <ClassName> [--part <subObjectOffset>]");
            Console.WriteLine("  ClassName: TEAM@db  |  db::TEAM  |  .?AVTEAM@db@@");
            return;
        }

        long? part = Option(args, "--part") is { } p ? ParseNumber(p) : null;
        int found = 0;

        foreach (string raw in RttiImage.RawCandidates(args[0]))
        {
            foreach (long td in image.FindExactTypeDescriptors(raw))
            {
                foreach (ObjectLocator col in image.LocatorsFor(td).OrderBy(c => c.SubObjectOffset))
                {
                    if (part.HasValue && col.SubObjectOffset != part.Value)
                    {
                        continue;
                    }

                    foreach (long vt in image.VtablesFor(col.Rva))
                    {
                        found++;
                        Console.WriteLine($"{RttiImage.Pretty(raw)}  vtable RVA 0x{vt:X}  sub-object +{col.SubObjectOffset} (0x{col.SubObjectOffset:X})  [COL 0x{col.Rva:X}]");
                    }
                }
            }
        }

        Console.WriteLine(found == 0
            ? "No vtable found (class not in this module, no RTTI, or --part didn't match)."
            : $"{found} vtable(s).");
    }

    private static void List(string[] args, RttiImage image)
    {
        string? filter = args.Length > 0 && !args[0].StartsWith("--", StringComparison.Ordinal) ? args[0] : null;
        int max = Option(args, "--max") is { } m ? (int)ParseNumber(m) : 200;

        var matches = image.FindTypeDescriptors(filter);
        Console.WriteLine($"{matches.Count} type descriptor(s){(filter == null ? "" : $" matching '{filter}'")}.");

        bool showVtables = matches.Count <= 25;
        foreach (var (td, raw) in matches.Take(max))
        {
            Console.WriteLine($"  {RttiImage.Pretty(raw),-56} [{raw}]");
            if (!showVtables)
            {
                continue;
            }

            foreach (ObjectLocator col in image.LocatorsFor(td).OrderBy(c => c.SubObjectOffset))
            {
                foreach (long vt in image.VtablesFor(col.Rva))
                {
                    Console.WriteLine($"      vtable RVA 0x{vt:X}  sub-object +{col.SubObjectOffset} (0x{col.SubObjectOffset:X})");
                }
            }
        }

        if (matches.Count > max)
        {
            Console.WriteLine($"  ... {matches.Count - max} more (raise --max).");
        }

        if (!showVtables && matches.Count > 0)
        {
            Console.WriteLine("(Narrow the filter to 25 or fewer matches to also list vtables.)");
        }
    }

    private static string? Option(string[] args, string name)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (string.Equals(args[i], name, StringComparison.OrdinalIgnoreCase))
            {
                return args[i + 1];
            }
        }

        return null;
    }

    private static long ParseNumber(string text)
    {
        text = text.Trim();
        return text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            ? Convert.ToInt64(text[2..], 16)
            : long.Parse(text);
    }
}
