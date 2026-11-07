#nullable enable
using System.Buffers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Pitchside.Core.VirtualMemory.Rtti
{
    /// <summary>
    /// Whole-process memory helpers for the RTTI-based versions: find every object of a class by
    /// scanning for its vtable pointer, and read the game's length-prefixed strings.
    /// </summary>
    public static class ProcessHeap
    {
        /// <summary>
        /// Scans every committed, readable region of the attached process for 8-byte-aligned qwords
        /// equal to one of <paramref name="values"/> and reports each hit's address. An object's first
        /// qword is its vtable pointer, so scanning for a vtable's absolute address finds its instances.
        /// </summary>
        public static void ScanQwords(ICollection<long> values, Action<long, long> onHit)
        {
#if WINDOWS
            if (values.Count == 0)
            {
                return;
            }

            long low = values.Min();
            long high = values.Max();
            var lookup = new HashSet<long>(values);
            const int chunkSize = 16 * 1024 * 1024;

            // MEM_PRIVATE-only (skips every loaded module's image and file mappings - object
            // instances never live there) cuts the bytes to read.
            //
            // Flatten every region into (address, length) chunk descriptors up front and parallelize
            // over THOSE, not over regions: region sizes vary hugely (a handful can dwarf the rest),
            // so parallelizing per-region leaves threads idle once they've drained the small regions
            // while one thread grinds through a giant one. Chunks are all the same size, so splitting
            // on them balances load evenly across cores instead.
            var chunks = new List<(long Address, int Length)>();
            foreach (var region in ProcessManager.EnumerateReadableRegions(privateOnly: true))
            {
                for (long offset = 0; offset < region.Size; offset += chunkSize)
                {
                    int length = (int)Math.Min(chunkSize, region.Size - offset);
                    chunks.Add((region.BaseAddress + offset, length));
                }
            }

            var onHitLock = new object();

            // One pooled buffer per worker thread, reused across all the chunks that thread handles
            // (rather than a fresh ~16MB array per chunk read, which otherwise lands on the Large
            // Object Heap hundreds of times over a multi-GB scan) and returned to the pool when that
            // thread's share of the work is done.
            Parallel.ForEach(chunks,
                () => ArrayPool<byte>.Shared.Rent(chunkSize),
                (chunk, loopState, buffer) =>
                {
                    if (ProcessManager.ReadProcessMemoryInto(chunk.Address, buffer, chunk.Length, out _))
                    {
                        // Chunks are page-aligned, so qwords never straddle a chunk boundary.
                        for (int i = 0; i + 8 <= chunk.Length; i += 8)
                        {
                            long value = BitConverter.ToInt64(buffer, i);
                            if (value >= low && value <= high && lookup.Contains(value))
                            {
                                lock (onHitLock)
                                {
                                    onHit(chunk.Address + i, value);
                                }
                            }
                        }
                    }

                    return buffer;
                },
                buffer => ArrayPool<byte>.Shared.Return(buffer));
#else
            throw new PlatformNotSupportedException("Heap scanning is only implemented for Windows.");
#endif
        }

        /// <summary>Reads exactly <paramref name="length"/> bytes, or returns null if any of them are unreadable.</summary>
        public static byte[]? TryRead(long address, int length)
        {
            if (address <= 0 || length <= 0)
            {
                return null;
            }

            byte[] buffer = ProcessManager.ReadProcessMemory(address, (uint)length, out long bytesRead);
            return bytesRead >= length ? buffer : null;
        }

        public static long ReadPointer(long address)
        {
            byte[]? bytes = TryRead(address, 8);
            return bytes == null ? 0 : BitConverter.ToInt64(bytes, 0);
        }

        public static int? ReadInt32(long address)
        {
            byte[]? bytes = TryRead(address, 4);
            return bytes == null ? (int?)null : BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads the game's string layout at <paramref name="pointer"/>: Int32 byte length followed by
        /// UTF-8 bytes. Returns null for a null/unreadable pointer or an implausible length.
        /// </summary>
        public static string? ReadLengthPrefixedString(long pointer, int maxLength = 199)
        {
            if (pointer == 0)
            {
                return null;
            }

            int? length = ReadInt32(pointer);
            if (length == null || length <= 0 || length > maxLength)
            {
                return null;
            }

            byte[]? bytes = TryRead(pointer + 4, length.Value);
            if (bytes == null || Array.IndexOf(bytes, (byte)0) >= 0)
            {
                return null;
            }

            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>A name-table entry is a pointer to a pointer to a length-prefixed string.</summary>
        public static string? ReadNameEntry(long entryPointer)
        {
            return entryPointer == 0 ? null : ReadLengthPrefixedString(ReadPointer(entryPointer));
        }
    }
}
