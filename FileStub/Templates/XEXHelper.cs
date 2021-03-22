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
namespace FileStub.Templates
{
    class XEXHelper
    {//XEX files are the XBOX 360 equivalent of a Windows EXE file. In fact, when unencrypted and decompressed, a XEX file can contain a Windows PE file, just with code in PPC asm instead of x86 asm, and other stuff related to the XBOX 360's inner workings.

        long FileHeaderSize = 24;
        int optheadercount;
        long peoffset;
        public long[] optheader_offsets = new long[1024];
        public long[] optheader_sizes = new long[1024];
        public XEXHelper(FileInterface xexInterface)
        {
            peoffset = GetPEOffset(xexInterface);
            optheadercount = GetOptHeaderCount(xexInterface);
        }
        int GetOptHeaderCount(FileInterface XEX)
        {
            return BitConverter.ToInt32(XEX.PeekBytes(0x14, 4), 0);
        }
        long GetPEOffset(FileInterface XEX)
        {
            long mzoffset = BitConverter.ToInt32(XEX.PeekBytes(0x8, 4), 0);
            return mzoffset + BitConverter.ToInt32(XEX.PeekBytes(mzoffset + 0x3C, 4).Reverse().ToArray(), 0);
        }

    }
}
