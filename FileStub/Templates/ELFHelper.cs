//a helper class for targeting elf files
namespace FileStub.Templates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.Common;
    using RTCV.CorruptCore;

    public class ELFHelper
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        public string pathtoelf;
        public FileTarget elfTarget;
        public int bitwidth;
        public bool BigEndian;
        public bool IsRPX;
        public long pht_offset;
        public long sht_offset;
        public int pht_entries;
        public int sht_entries;
        public long[] ps_offsets = new long[1024];
        public long[] ps_sizes = new long[1024];
        public string[] ss_names = new string[1024];
        public long[] ss_offsets = new long[1024];
        public long[] ss_sizes = new long[1024];
#pragma warning restore CA1051 // Do not declare visible instance fields
        public ELFHelper(FileInterface elfInterface)
        {
            string elfpath = elfInterface?.Filename;
            pathtoelf = elfpath;
            if (elfpath.Contains("rpx")) IsRPX = true;
            else { IsRPX = false; }
            var elfpathInfo = new FileInfo(elfpath);
            bitwidth = GetElfBitWidth(elfInterface);
            BigEndian = IsElfBigEndian(elfInterface);
            if (!IsRPX) //RPXs are ELF files but don't have program headers.
            {
                pht_offset = GetProgramHeaderTableOffset(elfInterface);
                pht_entries = GetProgramHeaderEntryNum(elfInterface);
                ps_offsets = new long[pht_entries];
                ps_sizes = new long[pht_entries];
            }
            sht_offset = GetSectionHeaderTableOffset(elfInterface);
            sht_entries = GetSectionHeaderEntryNum(elfInterface);
            ss_names = new string[sht_entries];
            ss_offsets = new long[sht_entries];
            ss_sizes = new long[sht_entries];
            if (!IsRPX)
            {
                int ph_iterator = 0;
                while (ph_iterator < pht_entries)
                {
                    ph_iterator++;
                    ps_offsets[ph_iterator] = GetProgramSegmentOffset(elfInterface, ph_iterator);
                    ps_sizes[ph_iterator] = GetProgramSegmentSize(elfInterface, ph_iterator);
                }
            }
            int sh_iterator = 0;
            while (sh_iterator < sht_entries)
            {
                sh_iterator++;
                ss_names[sh_iterator] = GetSectionSegmentName(elfInterface, sh_iterator);
                ss_offsets[sh_iterator] = GetSectionSegmentOffset(elfInterface, sh_iterator);
                ss_sizes[sh_iterator] = GetSectionSegmentSize(elfInterface, sh_iterator);
            }
        }
        static int GetElfBitWidth(FileInterface elf)
        {
            if (elf.PeekByte(0x04) == 0x1)
                return 32;
            if (elf.PeekByte(0x04) == 0x2)
                return 64;
            return 32;
        }
        static bool IsElfBigEndian(FileInterface elf)
        {
            if (elf.PeekByte(0x5) == 0x1)
                return false;
            else
            {
                return true;
            }
        }
        static long GetProgramHeaderTableOffset(FileInterface elf)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                return BitConverter.ToUInt32(elf.PeekBytes(0x1C, 4).Reverse().ToArray(), 0);
            }
            else //assume it's 64-bit
                return (long)BitConverter.ToUInt64(elf.PeekBytes(0x20, 8).Reverse().ToArray(), 0);
        }
        static long GetSectionHeaderTableOffset(FileInterface elf)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                return BitConverter.ToUInt32(elf.PeekBytes(0x20, 4).Reverse().ToArray(), 0);
            }
            else //assume it's 64-bit
                return (long)BitConverter.ToUInt64(elf.PeekBytes(0x28, 8).Reverse().ToArray(), 0);
        }
        static int GetProgramHeaderEntryNum(FileInterface elf)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                return BitConverter.ToUInt16(elf.PeekBytes(0x2C, 2).Reverse().ToArray(), 0);
            }
            else //assume it's 64-bit
                return BitConverter.ToUInt16(elf.PeekBytes(0x38, 2).Reverse().ToArray(), 0);
        }
        static int GetSectionHeaderEntryNum(FileInterface elf)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                return BitConverter.ToUInt16(elf.PeekBytes(0x30, 2).Reverse().ToArray(), 0);
            }
            else //assume it's 64-bit
                return BitConverter.ToUInt16(elf.PeekBytes(0x3C, 2).Reverse().ToArray(), 0);
        }
        static long GetProgramSegmentOffset(FileInterface elf, int segment)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                long headeroffset = GetProgramHeaderTableOffset(elf) + (0x20 * Convert.ToInt64(segment));
                return BitConverter.ToUInt32(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x04, 4).Reverse().ToArray(), 0);
            }
            else
            {
                long headeroffset = GetProgramHeaderTableOffset(elf) + (0x38 * Convert.ToInt64(segment));
                return (long)BitConverter.ToUInt64(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x08, 8).Reverse().ToArray(), 0);
            }
        }
        static long GetProgramSegmentSize(FileInterface elf, int segment)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                long headeroffset = GetProgramHeaderTableOffset(elf) + (0x20 * Convert.ToInt64(segment));
                return BitConverter.ToUInt32(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x10, 4).Reverse().ToArray(), 0);
            }
            else
            {
                long headeroffset = GetProgramHeaderTableOffset(elf) + (0x38 * Convert.ToInt64(segment));
                return (long)BitConverter.ToUInt64(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x20, 8).Reverse().ToArray(), 0);
            }
        }
        static long GetShstrOffset(FileInterface elf)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                int segmentindex = BitConverter.ToUInt16(elf.PeekBytes(0x32, 2).Reverse().ToArray(), 0);
                return BitConverter.ToUInt32(elf.PeekBytes(GetSectionHeaderTableOffset(elf) + (0x28 * Convert.ToInt64(segmentindex)) + 10, 4).Reverse().ToArray(), 0);
            }
            else
            {
                int segmentindex = BitConverter.ToUInt16(elf.PeekBytes(0x3E, 2).Reverse().ToArray(), 0);
                return (long)BitConverter.ToUInt64(elf.PeekBytes(GetSectionHeaderTableOffset(elf) + (0x40 * Convert.ToInt64(segmentindex)) + 10, 8).Reverse().ToArray(), 0);
            }
        }
        static string GetSectionSegmentName(FileInterface elf, int segment)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                long headeroffset = GetSectionHeaderTableOffset(elf) + (0x28 * Convert.ToInt64(segment));
                long stroffset = BitConverter.ToUInt32(elf.PeekBytes(headeroffset, 4).Reverse().ToArray(), 0);
                if (GetShstrOffset(elf) == 0) return " ";
                return System.Text.Encoding.ASCII.GetString(elf.PeekBytes(GetShstrOffset(elf) + stroffset, 8));
            }
            else
            {
                long headeroffset = GetSectionHeaderTableOffset(elf) + (0x40 * Convert.ToInt64(segment));
                long stroffset = (long)BitConverter.ToUInt64(elf.PeekBytes(headeroffset, 8).Reverse().ToArray(), 0);
                if (GetShstrOffset(elf) == 0) return " ";
                return System.Text.Encoding.ASCII.GetString(elf.PeekBytes(GetShstrOffset(elf) + stroffset, 8));
            }
        }
        long GetSectionSegmentOffset(FileInterface elf, int segment)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                long headeroffset = sht_offset + (0x28 * Convert.ToInt64(segment));
                return BitConverter.ToUInt32(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x10, 4).Reverse().ToArray(), 0);
            }
            else
            {
                long headeroffset = sht_offset + (0x40 * Convert.ToInt64(segment));
                return (long)BitConverter.ToUInt64(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x18, 8).Reverse().ToArray(), 0);
            }
        }
        static long GetSectionSegmentSize(FileInterface elf, int segment)
        {
            if (GetElfBitWidth(elf) == 32)
            {
                long headeroffset = GetSectionHeaderTableOffset(elf) + (0x28 * Convert.ToInt64(segment));
                return BitConverter.ToUInt32(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x14, 4).Reverse().ToArray(), 0);
            }
            else
            {
                long headeroffset = GetSectionHeaderTableOffset(elf) + (0x40 * Convert.ToInt64(segment));
                return (long)BitConverter.ToUInt64(elf.PeekBytes(Convert.ToInt64(headeroffset) + 0x20, 8).Reverse().ToArray(), 0);
            }
        }
    }
}
