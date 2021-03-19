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
    class NSOHelper
    {//NSOs, not to be confused with Nintendo Switch Online, are Nintendo Switch executables. Usually they have no extension and are just named "main" (the main executable), "sdk" (library used by main), or "subsdkN" (extra libraries used by main, with N being the library's number). There's "rtld" but that's just the game's bootloader and isn't important
        public long codeoffset; //offset in the file where the code segment starts. The value that equals this offset in hex is at 0x10
        public long codesize; //size of the code segment. value is at 0x18
        public long rodataoffset; //offset where read-only data starts. value equalling this in hex is at 0x20
        public long rodatasize; //size of read-only data segment. at 0x28
        public long rwdataoffset; //read-writable data segment start offset. found at 0x30
        public long rwdatasize; //rwdata segment size. at 0x38
        public NSOHelper(FileInterface nsoInterface)
        {
            codeoffset = GetCodeOffset(nsoInterface);
            codesize = GetCodeSize(nsoInterface);
            rodataoffset = GetRODataOffset(nsoInterface);
            rodatasize = GetRODataSize(nsoInterface);
            rwdataoffset = GetRWDataOffset(nsoInterface);
            rwdatasize = GetRWDataSize(nsoInterface);
        }
        long GetCodeOffset(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x10, 4), 0);
        }
        long GetCodeSize(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x1C, 4), 0);
        }
        long GetRODataOffset(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x20, 4), 0);
        }
        long GetRODataSize(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x2C, 4), 0);
        }
        long GetRWDataOffset(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x30, 4), 0);
        }
        long GetRWDataSize(FileInterface NSO)
        {
            return BitConverter.ToInt32(NSO.PeekBytes(0x3C, 4), 0);
        }
    }
}
