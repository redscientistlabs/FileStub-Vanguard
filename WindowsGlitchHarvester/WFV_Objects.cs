using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using Ceras;


namespace WindowsFilesVanguard
{

    public class LabelPassthrough : Label
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, this.Text.ToString(), this.Font, ClientRectangle, ForeColor);
        }

    }

    public class RefreshingListBox : ListBox
    {
        public void RefreshItemsReal()
        {
            base.RefreshItems();
        }
    }

    public class MenuButton : Button
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        public void SetMenu(ContextMenuStrip _menu)
        {
            Menu = _menu;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Menu != null && mevent.Button == MouseButtons.Left)
            {
                Menu.Show(this, mevent.Location);
            }
        }

    }


    interface ICachable
    {


    }


    [Serializable()]
    public abstract class MemoryInterface
    {
        public abstract void getMemoryDump();
        public abstract void wipeMemoryDump();
        public abstract bool dolphinSavestateVersion();
        public abstract byte[][] lastMemoryDump { get; set; }
        public abstract bool cacheEnabled { get; }

        public abstract long getMemorySize();
        public abstract long? lastMemorySize { get; set; }

        public abstract void PokeByte(long address, byte data);
        public abstract void PokeBytes(long address, byte[] data);
        public abstract byte? PeekByte(long address);
        public abstract byte[] PeekBytes(long address, int length);

        public abstract void SetBackup();
        public abstract void ResetBackup(bool askConfirmation = true);
        public abstract void RestoreBackup(bool announce = true);
        public abstract void ResetWorkingFile();
        public abstract void ApplyWorkingFile();

        public volatile System.IO.Stream stream = null;
    }

    [Serializable()]
    public class FileInterface : MemoryInterface
    {
        public string Filename;
        public string ShortFilename;

        public override byte[][] lastMemoryDump { get; set; } = null;
        public override bool cacheEnabled { get { return lastMemoryDump != null; } }

        //lastMemorySize gets rounded up to a multiplier of 4 to make the vector engine work on multiple files
        //lastRealMemorySize is used in peek/poke to cancel out non-existing adresses
        public override long? lastMemorySize { get; set; }
        public long? lastRealMemorySize { get; set; }

        public long MultiFilePosition = 0;
        public long MultiFilePositionCeiling = 0;


        public FileInterface(string _targetId)
        {
            try
            {
                string[] targetId = _targetId.Split('|');
                Filename = targetId[1];
                ShortFilename = Filename.Substring(Filename.LastIndexOf("\\") + 1, Filename.Length - (Filename.LastIndexOf("\\") + 1));

                FileInfo info = new System.IO.FileInfo(Filename);

                if (info.IsReadOnly)
                {
                    throw new Exception("The file " + Filename + " is read - only! Cancelling load");
                }
                try
                {
                    using (Stream stream = new FileStream(Filename, FileMode.Open))
                    {
                        //Dummy code
                        Console.Write(stream.Length);
                    }
                }
                catch(IOException ex)
                {
                    if (ex is FileLoadException)
                    {
                        throw new Exception($"FileInterface failed to load something because the file is (probably) in use \n" + "Culprit file: " + Filename + "\n" + ex.Message);
                    }
                    if (ex is PathTooLongException)
                    {
                        throw new Exception($"FileInterface failed to load something because the path is too long. Try moving it closer to root \n" + "Culprit file: " + Filename + "\n" + ex.Message);
                    }
                }


                SetBackup();

                //getMemoryDump();
                getMemorySize();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"FileInterface failed to load something \n\n" + "Culprit file: " + Filename + "\n\n" + ex.ToString());

                if (WFV_Core.ghForm.rbTargetMultipleFiles.Checked)
                    throw;
            }
        }

        public string getCompositeFilename(string prefix)
        {
            if (WFV_Core.CompositeFilenameDico.ContainsKey(Filename))
            {
                return WFV_Core.CompositeFilenameDico[Filename];
            }
            //Add it to the dico
            string name = (WFV_Core.CompositeFilenameDico.Keys.Count + 1).ToString();
            WFV_Core.CompositeFilenameDico[Filename] = name;
            //Flush to disk
            WFV_Core.SaveCompositeFilenameDico();
            return name;
        }

        public string getCorruptFilename(bool overrideWriteCopyMode = false)
        {
            if (overrideWriteCopyMode || WFV_Core.writeCopyMode)
                return WFV_Core.currentDir + "\\TEMP\\" + getCompositeFilename("CORRUPT");
            else
                return Filename;
        }
        public string getBackupFilename()
        {
            return WFV_Core.currentDir + "\\TEMP\\" + getCompositeFilename("BACKUP");
        }

        public override void ResetWorkingFile()
        {

            try
            {
                if (File.Exists(getCorruptFilename()))
                    File.Delete(getCorruptFilename());
            }
            catch
            {
                MessageBox.Show($"Could not get access to {getCorruptFilename()}\n\nClose the file then try whatever you were doing again", "WARNING");
            }


            SetWorkingFile();
        }

        public string SetWorkingFile()
        {
            if (!File.Exists(getCorruptFilename()))
                File.Copy(getBackupFilename(), getCorruptFilename(), true);

            return getCorruptFilename();
        }

        public override void ApplyWorkingFile()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (CemuWatch.writeCopyMode)
            {

                try
                {
                    if (File.Exists(Filename))
                        File.Delete(Filename);

                    if (File.Exists(getCorruptFilename()))
                        File.Move(getCorruptFilename(), Filename);
                }
                catch
                {
                    MessageBox.Show($"Could not get access to {Filename} because some other program is probably using it. \n\nClose the file then press OK to try again", "WARNING");
                }

            }
        }

        public override void SetBackup()
        {
            if (!File.Exists(getBackupFilename()))
                File.Copy(Filename, getBackupFilename(), true);
        }

        public override void ResetBackup(bool askConfirmation = true)
        {
            if (askConfirmation && MessageBox.Show("Are you sure you want to reset the backup using the target file?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (File.Exists(getBackupFilename()))
                File.Delete(getBackupFilename());

            SetBackup();

        }

        public override void RestoreBackup(bool announce = true)
        {

            if (File.Exists(getBackupFilename()))
            {
                File.Delete(Filename);
                File.Copy(getBackupFilename(), Filename, true);

                if (announce)
                    MessageBox.Show("Backup of " + ShortFilename + " was restored");
            }
            else
            {
                MessageBox.Show("Couldn't find backup of " + ShortFilename);
            }
        }

        public override void wipeMemoryDump()
        {
            lastMemoryDump = null;
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }

        public override void getMemoryDump()
        {
            lastMemoryDump = WFV_MemoryBanks.ReadFile(getBackupFilename());

            /*
            //lastMemoryDump = File.ReadAllBytes(getBackupFilename());

            lastMemoryDump = new byte[lastMemorySize.Value];

            using (Stream stream = File.Open(getBackupFilename(), FileMode.Open))
            {

                //byte[] readBytes = new byte[bankSize];
                stream.Position = 0;
                stream.Read(lastMemoryDump, 0, Convert.ToInt32(lastRealMemorySize.Value));
            }

            //return lastMemoryDump;
            */
        }

        public override long getMemorySize()
        {
            if (lastMemorySize != null)
                return (long)lastMemorySize;

            lastRealMemorySize = new FileInfo(Filename).Length;

            long Alignment32bitReminder = lastRealMemorySize.Value % 4;

            if (Alignment32bitReminder != 0)
            {
                lastMemorySize = lastRealMemorySize.Value + (4 - Alignment32bitReminder);
            }
            else
            {
                lastMemorySize = lastRealMemorySize;
            }




            return (long)lastMemorySize;

        }

        public override bool dolphinSavestateVersion()
        {
            /*
             * 0x20-0x32 = "Dolphin Narry's Mod"
             * 0x35-0x39 = "X.Y.Z" - This is the version number
             */

            string a = "Dolphin Narry's Mod";
            string b = Encoding.Default.GetString(PeekBytes(32, 19));

            if (a == b)
            {
                //Change this if there's a new version that breaks things!!!
                string earliestSupportedVersion = "0.1.3";
                string savestateVersion = Encoding.Default.GetString(PeekBytes(53, 5));
                string earliestSupportedVersionTrimmed = earliestSupportedVersion.Replace(".", "");
                string savestateVersionTrimmed = savestateVersion.Replace(".", "");

                if (Convert.ToInt32(savestateVersionTrimmed) >= Convert.ToInt32(earliestSupportedVersionTrimmed))
                    return true;
                else
                {
                    MessageBox.Show("You are trying to load a savestate from an old version of Dolphin Narry's Mod. The earliest supported version is version " + earliestSupportedVersion + ". This will not work properly.");
                    return false;
                }

            }
            else
            {
                MessageBox.Show("The currently loaded file is not a Dolphin Narry's Mod savestate");
                return false;
            }

        }

        public override void PokeBytes(long address, byte[] data)
        {
            if (address + data.Length >= lastRealMemorySize)
                return;

            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            stream.Position = address;
            stream.Write(data, 0, data.Length);

            if (cacheEnabled)
                WFV_MemoryBanks.PokeBytes(lastMemoryDump, address, data); 

            /*
            if (lastMemoryDump != null)
                for (int i = 0; i < data.Length; i++)
                    lastMemoryDump[address + i] = data[i];
            */

        }

        public override void PokeByte(long address, byte data)
        {
            if (address >= lastRealMemorySize)
                return;

            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            stream.Position = address;
            stream.WriteByte(data);

            if (cacheEnabled)
                WFV_MemoryBanks.PokeByte(lastMemoryDump, address, data);
                //lastMemoryDump[address] = data;
        }

        public override byte? PeekByte(long address)
        {
            if (address >= lastRealMemorySize)
                return 0;


            if (cacheEnabled)
                return WFV_MemoryBanks.PeekByte(lastMemoryDump, address);
                //return lastMemoryDump[address];

            byte[] readBytes = new byte[1];

            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);


            stream.Position = address;
            stream.Read(readBytes, 0, 1);

            //fs.Close();

            return readBytes[0];

        }

        public override byte[] PeekBytes(long address, int length)
        {
            if (address + length >= lastRealMemorySize)
                return new byte[length];

            if (cacheEnabled)
                return WFV_MemoryBanks.PeekBytes(lastMemoryDump, address, length);
                //return lastMemoryDump.SubArray(address, range);

            byte[] readBytes = new byte[length];


            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);


            stream.Position = address;
            stream.Read(readBytes, 0, length);

            //fs.Close();

            return readBytes;

        }

    }


    [Serializable()]
    public class MultipleFileInterface : MemoryInterface
    {
        public string Filename;
        public string ShortFilename;

        public List<FileInterface> FileInterfaces = new List<FileInterface>();

        public MultipleFileInterface(string _targetId)
        {
            try
            {
                string[] targetId = _targetId.Split('|');

                for (int i = 0; i < targetId.Length; i++)
                    FileInterfaces.Add(new FileInterface("File|" + targetId[i]));

                Filename = "MultipleFiles";
                ShortFilename = "MultipleFiles";

                //SetBackup();

                //getMemoryDump();
                getMemorySize();

                setFilePositions();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"MultipleFileInterface failed to load something \n\n" + ex.ToString());
            }
        }

        public string getCompositeFilename(string prefix)
        {
            return string.Join("|", FileInterfaces.Select(it => it.getCompositeFilename(prefix)));
        }

        public string getCorruptFilename(bool overrideWriteCopyMode = false)
        {
            return string.Join("|", FileInterfaces.Select(it => it.getCorruptFilename(overrideWriteCopyMode)));

        }

        public string getBackupFilename()
        {
            return string.Join("|", FileInterfaces.Select(it => it.getBackupFilename()));
        }

        public override void ResetWorkingFile()
        {
            foreach (var fi in FileInterfaces)
                fi.ResetWorkingFile();

        }

        public string SetWorkingFile()
        {
            return string.Join("|", FileInterfaces.Select(it => it.SetWorkingFile()));

        }

        public override void ApplyWorkingFile()
        {
            foreach (var fi in FileInterfaces)
                fi.ApplyWorkingFile();

        }

        public override void SetBackup()
        {
            foreach (var fi in FileInterfaces)
                fi.SetBackup();

        }

        public override void ResetBackup(bool askConfirmation = true)
        {
            if (askConfirmation && MessageBox.Show("Are you sure you want to reset the backup using the target files?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            foreach (var fi in FileInterfaces)
                fi.ResetBackup(false);

        }

        public override void RestoreBackup(bool announce = true)
        {

            foreach (var fi in FileInterfaces)
                fi.RestoreBackup(false);

            if (announce)
                MessageBox.Show("Backups of " + string.Join(",", FileInterfaces.Select(it => (it as FileInterface).ShortFilename)) + " were restored");

        }

        public void setFilePositions()
        {

            long addressPad = 0;

            //find which fileInterface contains the file we want
            foreach (var fi in FileInterfaces)
            {
                fi.MultiFilePosition = addressPad;
                addressPad += fi.getMemorySize();
                fi.MultiFilePositionCeiling = addressPad;
                
            }

        }

        public override void wipeMemoryDump()
        {
            for (int i = 0; i < FileInterfaces.Count; i++)
            {
                FileInterfaces[i].wipeMemoryDump();
                //GC.Collect();
                //GC.WaitForFullGCComplete();
            }
        }

        public override void getMemoryDump()
        {
            long totalDumpSize = 0;

            for (int i = 0; i < FileInterfaces.Count; i++)
            {
                totalDumpSize += FileInterfaces[i].lastMemorySize.Value;
                FileInterfaces[i].getMemoryDump();
            }

            //lastMemoryDump = new byte[totalDumpSize];

            //long targetAddress = 0;

            //for (int i = 0; i < FileInterfaces.Count; i++)
            //{


            //Removed copying of the memory in a local big file because
            //it's smarter to actually use the FileInterfaces themselves
            /*
            long targetLength = FileInterfaces[i].lastMemorySize.Value;
            Array.Copy(FileInterfaces[i].lastMemoryDump, 0, lastMemoryDump, targetAddress, targetLength);
            targetAddress += targetLength;
            FileInterfaces[i].lastMemoryDump = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            */
            //}
            /*
            List<byte> allBytes = new List<byte>();

            foreach (var fi in FileInterfaces)
            {
                allBytes.AddRange(fi.getMemoryDump());
                fi.lastMemoryDump = null;
            }

        lastMemoryDump = allBytes.ToArray();
        */

            //return lastMemoryDump;

        }
        public override byte[][] lastMemoryDump
        {
            get { throw new Exception("FORBIDDEN USE OF LASTMEMORYDUMP ON MULTIPLEFILEINTERFACE"); }
            set { throw new Exception("FORBIDDEN USE OF LASTMEMORYDUMP ON MULTIPLEFILEINTERFACE"); }
        }

        public override bool cacheEnabled
        {
            get { return FileInterfaces.Count > 0 && FileInterfaces[0].lastMemoryDump != null; }
        }

        public override long getMemorySize()
        {
            long size = 0;

            foreach (var fi in FileInterfaces)
                size += fi.getMemorySize();

            lastMemorySize = size;
            return (long)lastMemorySize;

        }

        public override bool dolphinSavestateVersion()
        {
            //Not supported for multiple files
            return false;
        }

        public override long? lastMemorySize { get; set; }

        public override void PokeBytes(long address, byte[] data)
        {

            //find which fileInterface contains the file we want
            for (int i = 0; i < FileInterfaces.Count; i++)
            {
                var fi = FileInterfaces[i];

                if (fi.MultiFilePositionCeiling > address)
                {
                    fi.PokeBytes(address - fi.MultiFilePosition, data);
                    return;
                }
            }

        }

        public override void PokeByte(long address, byte data)
        {

            //find which fileInterface contains the file we want
            for (int i = 0; i < FileInterfaces.Count; i++)
            {
                var fi = FileInterfaces[i];

                if (fi.MultiFilePositionCeiling > address)
                {
                    fi.PokeByte(address - fi.MultiFilePosition, data);
                    return;
                }
            }

        }

        public override byte? PeekByte(long address)
        {

            //find which fileInterface contains the file we want
            for(int i = 0;i< FileInterfaces.Count;i++)
            {
                var fi = FileInterfaces[i];

                if (fi.MultiFilePositionCeiling > address)
                    return fi.PeekByte(address - fi.MultiFilePosition);
            }

            //if wasn't found
            return null;


        }

        public override byte[] PeekBytes(long address, int range)
        {

            //find which fileInterface contains the file we want
            for (int i = 0; i < FileInterfaces.Count; i++)
            {
                var fi = FileInterfaces[i];

                if (fi.MultiFilePositionCeiling > address)
                    return fi.PeekBytes(address - fi.MultiFilePosition, range);
            }

            //if wasn't found
            return null;

        }

    }


    //Since we don't need to corrupt in place for dolphin, we can corrupt a copied file
    //This also means that copy mode doesn't need to work
    //Because of this, we're using the code intended for copy mode to provide the working file which'll be corrupted
    //Then, the original file will never be touched
    [Serializable()]
    public class DolphinInterface : MemoryInterface
    {
        public string Filename;
        public string ShortFilename;
        public string OriginalFilename;
        public string OriginalShortFilename;

        public DolphinInterface(string _targetId)
        {
            try
            {
                string[] targetId = _targetId.Split('|');
                Filename = targetId[1];
                ShortFilename = Filename.Substring(Filename.LastIndexOf("\\") + 1, Filename.Length - (Filename.LastIndexOf("\\") + 1));

                OriginalFilename = Filename;
                OriginalShortFilename = ShortFilename;

                SetBackup();

                //getMemoryDump();
                getMemorySize();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"DolphinInterface failed to load something \n\n" + "Culprit file: " + Filename + "\n\n" + ex.ToString());

                if (WFV_Core.ghForm.rbTargetMultipleFiles.Checked)
                    throw;
            }
        }

        public string getCompositeFilename(string prefix)
        {
            if (WFV_Core.CompositeFilenameDico.ContainsKey(Filename))
            {
                return WFV_Core.CompositeFilenameDico[Filename];
            }
            //Add it to the dico
            string name = (WFV_Core.CompositeFilenameDico.Keys.Count + 1).ToString();
            WFV_Core.CompositeFilenameDico[Filename] = name;
            //Flush to disk
            WFV_Core.SaveCompositeFilenameDico();
            return name;
        }

        public string getCorruptFilename(bool overrideWriteCopyMode = false)
        {
            return WFV_Core.currentDir + "\\TEMP\\" + getCompositeFilename("CORRUPT");
        }
        //We don't actually use the backup at all for Target Dolphin
        public string getBackupFilename()
        {
            return Filename;
        }

        public override void ResetWorkingFile()
        {
            try
            {
                if (File.Exists(getCorruptFilename()))
                    File.Delete(getCorruptFilename());
            }
            catch
            {
                MessageBox.Show($"Could not get access to {getCorruptFilename()}", "WARNING");
            }

            SetWorkingFile();
        }

        public string SetWorkingFile()
        {
            File.Copy(Filename, getCorruptFilename(), true);
            return getCorruptFilename();
        }

        public override void ApplyWorkingFile()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            //Copy mode wont work here as we're always working on a copied file
        }

        public override void SetBackup()
        {
            SetWorkingFile();
        }

        public override void ResetBackup(bool askConfirmation = true)
        {
            MessageBox.Show("Backups aren't used for Target Dolphin so you can't reset the backup!");
        }

        public override void RestoreBackup(bool announce = true)
        {
            if (File.Exists(Filename))
            {
                File.Delete(getCorruptFilename());
                SetWorkingFile();

                if (announce)
                    MessageBox.Show("Backup of " + ShortFilename + " was restored");
            }
            else
            {
                MessageBox.Show("Couldn't find backup of " + ShortFilename);
            }
        }


        public override void wipeMemoryDump()
        {
            lastMemoryDump = null;
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }

        public override void getMemoryDump()
        {
            //lastMemoryDump = File.ReadAllBytes(getBackupFilename());
            lastMemoryDump = WFV_MemoryBanks.ReadFile(getBackupFilename());
            //return lastMemoryDump;
        }


        public override byte[][] lastMemoryDump { get; set; } = null;

        public override bool cacheEnabled { get { return lastMemoryDump != null; } }

        public override long getMemorySize()
        {
            if (lastMemorySize != null)
                return (long)lastMemorySize;

            lastMemorySize = new FileInfo(Filename).Length;
            return (long)lastMemorySize;

        }


        public override bool dolphinSavestateVersion()
        {
            /*
             * 0x20-0x32 = "Dolphin Narry's Mod"
             * 0x35-0x39 = "X.Y.Z" - This is the version number
             */

            string a = "Dolphin Narry's Mod";
            string b = Encoding.Default.GetString(PeekBytes(32, 19));

            if (a == b)
            {
                //Change this if there's a new version that breaks things!!!
                string earliestSupportedVersion = "0.1.3";
                string savestateVersion = Encoding.Default.GetString(PeekBytes(53, 5));

                string earliestSupportedVersionTrimmed = earliestSupportedVersion.Replace(".", "");
                string savestateVersionTrimmed = savestateVersion.Replace(".", "");

                if (Convert.ToInt32(savestateVersionTrimmed) >= Convert.ToInt32(earliestSupportedVersionTrimmed))
                    return true;
                else
                {
                    MessageBox.Show("You are trying to load a savestate from an old version of Dolphin Narry's Mod. The earliest supported version is version " + earliestSupportedVersion + ". This will not work properly.");
                    return false;
                }

            }
            else
            {
                MessageBox.Show("The currently loaded file is not a Dolphin Narry's Mod savestate");
                return false;
            }

        }

        public override long? lastMemorySize { get; set; }

        public override void PokeBytes(long address, byte[] data)
        {
            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            stream.Position = address;
            stream.Write(data, 0, data.Length);


            if (cacheEnabled)
                WFV_MemoryBanks.PokeBytes(lastMemoryDump, address, data);

            /*
            if (lastMemoryDump != null)
                for (int i = 0; i < data.Length; i++)
                    lastMemoryDump[address + i] = data[i];
            */
        }

        public override void PokeByte(long address, byte data)
        {
            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            stream.Position = address;
            stream.WriteByte(data);

            if (cacheEnabled)
                WFV_MemoryBanks.PokeByte(lastMemoryDump, address, data);
                //lastMemoryDump[address] = data;
        }

        public override byte? PeekByte(long address)
        {

            if (cacheEnabled)
                return WFV_MemoryBanks.PeekByte(lastMemoryDump, address);
                //return lastMemoryDump[address];

            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            byte[] readBytes = new byte[1];
            stream.Position = address;
            stream.Read(readBytes, 0, 1);

            //fs.Close();

            return readBytes[0];

        }

        public override byte[] PeekBytes(long address, int length)
        {

            if (cacheEnabled)
                return WFV_MemoryBanks.PeekBytes(lastMemoryDump, address, length);
                //return lastMemoryDump.SubArray(address, range);

            if (stream == null)
                stream = File.Open(SetWorkingFile(), FileMode.Open);

            byte[] readBytes = new byte[length];
            stream.Position = address;
            stream.Read(readBytes, 0, length);

            //fs.Close();

            return readBytes;

        }

    }
    //Actual dolphinInterface saved just in case
    /*
    [Serializable()]
    public class DolphinInterface : MemoryInterface , ICachable
    {
        public string ProcessName;
        ProcessHijacker Hijack = null;

        public bool Hooked;
        public bool UseCaching = false;

        public DolphinInterface(string _processName)
        {
            getMemorySize();
        }

        public override byte[] getMemoryDump()
        {
            lastMemoryDump = null;
            return lastMemoryDump;
        }
        public override byte[] lastMemoryDump { get; set; } = null;

        public override long getMemorySize()
        {
            lastMemorySize = WFV_SavestateInfoForm.getMemorySize();
            return WFV_SavestateInfoForm.getMemorySize();
        }

        public override bool isDolphinSavestate()
        {
            return false;
        }

        public override long? lastMemorySize { get; set; }

        public override void PokeBytes(long address, byte[] data)
        {
            WFV_Core.ssForm.PokeBytes(address, data);
        }

        public override void PokeByte(long address, byte data)
        {
            WFV_Core.ssForm.PokeByte(address, data);
        }

        public override byte? PeekByte(long address)
        {
            return WFV_Core.ssForm.PeekByte(address);
        }

        public override byte[] PeekBytes(long address, int range)
        {
            return WFV_Core.ssForm.PeekBytes(address, range);
        }

        public override void SetBackup()
        {
            //CAN'T DO THAT WITH PROCESSES
        }

        public override void ResetBackup(bool askConfirmation = true)
        {
            //CAN'T DO THAT WITH PROCESSES
        }

        public override void RestoreBackup(bool announce = true)
        {
            //CAN'T DO THAT WITH PROCESSES
        }

        public override void ResetWorkingFile()
        {
            //CAN'T DO THAT WITH PROCESSES
        }

        public override void ApplyWorkingFile()
        {
            //CAN'T DO THAT WITH PROCESSES
        }

        public void RefreshSize()
        {
            getMemorySize();
        }*/
}
