//namespace FileStub.Templates
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel;
//    using System.Data;
//    using System.Diagnostics;
//    using System.Drawing;
//    using System.IO;
//    using System.Linq;
//    using System.Runtime.InteropServices;
//    using System.Runtime.Remoting.Messaging;
//    using System.Security.Cryptography.X509Certificates;
//    using System.Text;
//    using System.Threading.Tasks;
//    using System.Windows.Forms;
//    using Newtonsoft.Json;
//    using RTCV.Common;
//    using RTCV.CorruptCore;
//    using RTCV.UI;

//    public partial class FileStubTemplateRPCS3 : Form, IFileStubTemplate
//    {

//        const string RPCS3STUB_EBOOT = "RPCS3 : PS3 game EBOOT.BIN";
//        const string RPCS3STUB_ALLSELFSSPRXS = "RPCS3 : All PS3 game SELFS and SPRXs";


//        public string rpcs3SpecDir = Path.Combine(FileStub.FileWatch.currentDir, "RPCS3");
//        public string rpcs3EmuDir = Path.Combine(FileStub.FileWatch.currentDir, "EMUS", "RPCS3");
//        public string rpcs3ExePath = Path.Combine(FileStub.FileWatch.currentDir, "EMUS", "RPCS3", "rpcs3.exe");
//        Process rpcs3Process = new Process();

//        private RPCS3State _state = RPCS3State.OFF;

//        private RPCS3State state
//        {
//            get => _state;
//            set
//            {
//                Console.WriteLine($"Setting state to {value}");
//                _state = value;
//            }
//        }

//        public Dictionary<string, RPCS3StubSession> knownGamesDico = new Dictionary<string, RPCS3StubSession>();
//        public RPCS3StubSession currentSession = new RPCS3StubSession();

//        public bool DontSelectGame = false;

//        string currentSelectedTemplate = null;
//        public string[] TemplateNames { get => new string[] {
//            RPCS3STUB_EBOOT,
//            RPCS3STUB_ALLSELFSSPRXS
//        }; }

//        public bool DisplayDragAndDrop => true;
//        public bool DisplayBrowseTarget => true;


//        public FileStubTemplateRPCS3()
//        {
//            InitializeComponent();

//            //ensure rpcs3 folders exist
//            if (!Directory.Exists(rpcs3SpecDir))
//                Directory.CreateDirectory(rpcs3SpecDir);

//            string rpcs3ParamsDir = Path.Combine(rpcs3SpecDir, "PARAMS");

//            if (!Directory.Exists(rpcs3ParamsDir))
//                Directory.CreateDirectory(rpcs3ParamsDir);
//            rpcs3Process.StartInfo.FileName = rpcs3ExePath;
//        }

//        public FileTarget[] GetTargets()
//        {
//            string targetFolder = lbGameFolder.Text;

//            if (targetFolder == "")
//            {
//                MessageBox.Show("No target loaded");
//                return null;
//            }
//            string ebootpath = Path.Combine(targetFolder, "PS3_GAME", "USRDIR", "EBOOT.BIN");
//            List<FileTarget> targets = new List<FileTarget>();

//            var exeFileInfo = new FileInfo(targetExe);
//            var exeFolder = exeFileInfo.Directory.FullName;

//            var baseFolder = exeFileInfo.Directory;

//            if (cbParentExeDir.Checked)
//                baseFolder = baseFolder.Parent;

//            List<FileInfo> allFiles = SelectMultipleForm.DirSearch(baseFolder);

//            string baseless(string path) => path.Replace(exeFolder, "");

//            var exeTarget = Vault.RequestFileTarget(baseless(exeFileInfo.FullName), baseFolder.FullName);

//            var allDlls = allFiles.Where(it => it.Extension == ".dll");

//            var allKnownDlls = allDlls.Where(it =>
//                    it.Name.ToUpper().Contains("PHYSICS") ||
//                    it.Name.ToUpper().Contains("CLOTH") ||
//                    it.Name.ToUpper().Contains("ANIMATION") ||
//                    it.Name.ToUpper().Contains("PARTICLE") ||
//                    it.Name.ToUpper().Contains("TERRAIN") ||
//                    it.Name.ToUpper().Contains("VEHICLES") ||
//                    it.Name.ToUpper().Contains("UNITYENGINE.DLL")
//                    ).ToArray();

//            var allUnityEngine = allDlls.Where(it =>
//                    it.Name.ToUpper().Contains("UNITYENGINE.DLL")
//                    ).ToArray();


//            switch (currentSelectedTemplate)
//            {
//                case UNITYSTUB_EXE_KNOWN_DLL:
//                    {
//                        targets.Add(exeTarget);
//                        targets.AddRange(allDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
//                    }
//                    break;
//                case UNITYSTUB_EXE_ALL_DLL:
//                    {
//                        targets.Add(exeTarget);
//                        targets.AddRange(allKnownDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
//                    }
//                    break;
//                case UNITYSTUB_EXE:
//                    {
//                        targets.Add(exeTarget);
//                    }
//                    break;
//                case UNITYSTUB_UNITYDLL:
//                    {
//                        targets.AddRange(allUnityEngine.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
//                    }
//                    break;
//            }

//            //Prepare filestub for execution
//            var sf = S.GET<StubForm>();
//            FileWatch.currentSession.selectedExecution = ExecutionType.EXECUTE_OTHER_PROGRAM;
//            Executor.otherProgram = targetExe;
//            sf.tbArgs.Text = $"";
//            return targets.ToArray();
//        }

//        public void GetSegments(FileInterface exeInterface)
//        {
//            ELFHelper rpx = new ELFHelper(exeInterface);
//            string exePath = exeInterface.Filename;
//            var rpxInfo = new FileInfo(exePath);
//            int i = 0;
//            //List<FileInterface> segmentInterfaces = new List<FileInterface>();
//            //List<MemoryDomainProxy> memoryDomainProxies = new List<MemoryDomainProxy>();
//            while (i < rpx.sht_entries)
//            {
//                i++;
//                long[] range = new long[2];
//                range[0] =  rpx.ss_offsets[i] ;
//                range[1] = rpx.ss_offsets[i]+rpx.ss_sizes[i];
//                string vmdnametext = rpxInfo.Name + "|Section" + i;
//                if (range[0] >= range[1])
//                {
//                    return;
//                }

//                List<long[]> ranges = new List<long[]>();
//                ranges.Add(range);
//                VmdPrototype vmdPrototype = new VmdPrototype();
//                vmdPrototype.GenDomain = exeInterface.ToString();
//                vmdPrototype.BigEndian = exeInterface.BigEndian;
//                vmdPrototype.AddRanges = ranges;
//                vmdPrototype.WordSize = exeInterface.WordSize;
//                vmdPrototype.VmdName = vmdnametext;
//                vmdPrototype.PointerSpacer = 1;
//                if(range[1] < exeInterface.Size)
//                {
//                    RTCV.NetCore.LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.DomainVMDAdd, (object)vmdPrototype, true);
//                }
//                S.GET<VmdPoolForm>().RefreshVMDs();
//                S.GET<MemoryDomainsForm>().RefreshDomains();
//            }
//        }
//        public Form GetTemplateForm(string name)
//        {
//            this.SummonTemplate(name);
//            return this;
//        }

//        private void SummonTemplate(string name)
//        {
//            currentSelectedTemplate = name;

//            lbTemplateDescription.Text =
//$@"== Corrupt Wii U RPX files ==
//Load a game in RPCS3 and after it has loaded, click on Load targets into RTCV.
//";
//        }

//        bool IFileStubTemplate.DragDrop(string[] fd)
//        {
//            if (fd.Length > 1 || fd[0].EndsWith("\\") || !fd[0].ToUpper().EndsWith(".SFB"))
//            {
//                MessageBox.Show("Please only drop the game's disc file");
//                lbGameFolder.Text = "";
//                return false;
//            }
//            string sfb = fd[0];
//            FileInfo sfbfile = new FileInfo(sfb);
//            lbGameFolder.Text = sfbfile.DirectoryName;
//            currentSession.gameFolderPath = lbGameFolder.Text;
//            currentSession.gameinfopath = Path.Combine(currentSession.gameFolderPath, "gameinfo.txt");
//            //here is where we grab the game's gameinfo from rpcs3
//            rpcs3Process.StartInfo.Arguments = $"--gameinfo \"{currentSession.gameFolderPath}\"";
//            rpcs3Process.Start();
//            File.OpenRead()
//            currentSession.gameUSRDIRPath = Path.Combine(currentSession.gameFolderPath, "PS3_GAME", "USRDIR");
//            currentSession.ebootFilePath = Path.Combine(currentSession.gameUSRDIRPath, "EBOOT.BIN");
//            return true;
//        }

//        private Process getRPCS3Process()
//        {
//            if (rpcs3Process == null)
//            {
//                RefreshRPCS3Process();
//            }
//            //Get a new process object from then pid we have.
//            try
//            {
//                if (rpcs3Process?.Id != null)
//                    rpcs3Process = Process.GetProcessById(rpcs3Process.Id);
//            }
//            catch (Exception e)
//            {
//                rpcs3Process = null;
//                Console.WriteLine($"Couldn't get process from pid {rpcs3Process?.Id ?? -1}\n {e}");
//            }
//            //If the title is still expectedRPCS3Title, we know something else didn't eat the pid
//            if (!(rpcs3Process?.MainWindowTitle.Contains(expectedRPCS3Title) ?? false))
//                RefreshRPCS3Process();

//            return rpcs3Process;
//        }

//        public void RefreshRPCS3Process(Process p = null)
//        {
//            if (p == null)
//            {
//                try
//                {
//                    p = Process.GetProcessesByName("RPCS3")
//                        .FirstOrDefault(it => it?.MainWindowTitle?.Contains(expectedRPCS3Title) ?? false);
//                }
//                catch (InvalidOperationException e)
//                {
//                    Console.WriteLine($"Failed to get process!\n{e.Message}");
//                    rpcs3Process = null;
//                    return;
//                }
//            }


//            rpcs3Process = p;

//            if (rpcs3Process != null)
//            {
//                rpcs3Process.EnableRaisingEvents = true;
//                rpcs3Process.Exited += (o, e) =>
//                {
//                    rpcs3Process = null;
//                };
//            }
//        }

//        private void ScanRPCS3()
//        {
//            Process p = getRPCS3Process();

//            if (state == RPCS3State.UNFOUND && p != null)
//            {
//                state = RPCS3State.RUNNING;
//            }
//            else if(
//                state != RPCS3State.UNFOUND &&
//                state != RPCS3State.GAMELOADED &&
//                state != RPCS3State.READY &&
//                p == null)
//            {
//                state = RPCS3State.UNFOUND;
//                //DisableInterface();
//            }

//        }

//        private bool FetchBaseInfoFromRPCS3Process()
//        {
//            ///
//            ///Fetching Game info from rpcs3 process window title
//            ///

//            string windowTitle = rpcs3Process.MainWindowTitle;

//            if (windowTitle.Contains("[Online]"))
//            {
//                MessageBox.Show("RPCS3 is in online mode. Cancelling load to prevent any potential bans.\nDisable online mode to use the RPCS3 template");
//                return false;
//            }


//            string TitleIdPart = windowTitle.Split('[').FirstOrDefault(it => it.Contains("TitleId:"));
//            string TitleNumberPartLong = TitleIdPart.Split(':')[1];
//            string TitleNumberPart = TitleNumberPartLong.Split(']')[0];
//            string TitleGameNamePart = TitleNumberPartLong.Split(']')[1];

//            currentSession.FirstID = TitleNumberPart.Split('-')[0].Trim();
//            currentSession.SecondID = TitleNumberPart.Split('-')[1].Trim();
//            currentSession.rpcs3ExeFile = new FileInfo(rpcs3Process.MainModule.FileName);

//            currentSession.gameName = TitleGameNamePart.Trim();
//            return true;
//        }

//        [DllImport("user32.dll")]
//        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

//        private const int WM_CLOSE = 0x0010;
//        private const int WM_DESTROY = 0x0011;
//        private const int WM_QUIT = 0x0012;
//        internal void KillRPCS3Process(bool graceful)
//        {
//            if (graceful)
//            {
//                var rpcs3s = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(currentSession.rpcs3ExeFile.FullName));
//                MessageBox.Show("Closing RPCS3 to configure the loaded game for FileStub.\n\n" +
//                                "IF YOU OPENED ANY MENUS WHILE THE GAME WAS LOADING, AN ERROR MAY OCCUR. If an error occurs, try again. If it keeps occurring, poke the RTC devs.\n\n" +
//                                "If RPCS3 doesn't close, quit it yourself to continue.",
//                        "Registering Game for FileStub",
//                        MessageBoxButtons.OK,
//                        MessageBoxIcon.Information,
//                        MessageBoxDefaultButton.Button1,
//                        MessageBoxOptions.DefaultDesktopOnly);
//                foreach (var p in rpcs3s)
//                {
//                    try
//                    {
//                        var children = WindowHandleInfo.GetAllChildHandles(p.MainWindowHandle);
//                        if (children != null)
//                        {
//                            foreach (var h in children)
//                            {
//                                SendMessage(h, WM_CLOSE, new IntPtr(0), new IntPtr(0));
//                            }
//                        }
//                        SendMessage(p.MainWindowHandle, WM_CLOSE, new IntPtr(0), new IntPtr(0));
//                        p.CloseMainWindow();
//                        p.WaitForExit();
//                    }
//                    catch (Exception e)
//                    {
//                        Console.WriteLine(e);
//                    }
//                }
//            }
//            else
//            {
//                var p = rpcs3Process;
//                {
//                    ProcessStartInfo psi = new ProcessStartInfo();
//                    psi.FileName = "taskkill";
//                    psi.Arguments = $"/F /IM {currentSession.rpcs3ExeFile.Name} /T";
//                    psi.RedirectStandardOutput = true;
//                    psi.RedirectStandardError = true;
//                    psi.UseShellExecute = false;
//                    psi.CreateNoWindow = true;

//                    Process _p = new Process();
//                    _p.OutputDataReceived += (sender, args) => Console.WriteLine("received output: {0}", args.Data);
//                    _p.ErrorDataReceived += (sender, args) => Console.WriteLine("received error: {0}", args.Data);
//                    _p.StartInfo = psi;
//                    _p.Start();
//                    _p.BeginOutputReadLine();
//                }
//                if (p == null)
//                    System.Threading.Thread.Sleep(300); //Sleep for 300ms in case there's a rpcs3 process we don't have a handle to
//                else
//                {
//                    p.WaitForExit();
//                }
//            }
//        }
//        private bool LoadDataFromRPCS3FilesXml()
//        {
//            ///
//            ///gathering data from log.txt and settings.xml files
//            ///

//            string[] logTxt = File.ReadAllLines(Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "log.txt"));
//            string[] settingsXml =
//                File.ReadAllLines(Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "settings.xml"));

//            //getting rpx filename from log.txt
//            string logLoadingLine = logTxt.FirstOrDefault(it => it.Contains("Loading") && it.Contains(".rpx"));
//            string[] logLoadingLineParts = logLoadingLine.Split(' ');
//            currentSession.rpxFile = logLoadingLineParts[logLoadingLineParts.Length - 1];

//            //getting full rpx path from settings.xml
//            string settingsXmlRpxLine = settingsXml.FirstOrDefault(it => it.Contains(currentSession.rpxFile));
//            string[] settingsXmlRpxLineParts = settingsXmlRpxLine.Split('>')[1].Split('<');

//            //gameRpxPath =
//            //gameRpxFileInfo = new FileInfo(gameRpxPath);
//            //updateRpxPath = Path.Combine(rpcs3ExeFile.DirectoryName, "mlc01", "usr", "title", FirstID, SecondID);

//            //updateCodePath = Path.Combine(updateRpxPath, "code");
//            //updateMetaPath = Path.Combine(updateRpxPath, "meta");



//            //updateRpxLocation = Path.Combine(updateCodePath, rpxFile);
//            //updateRpxCompressed = Path.Combine(updateCodePath, "compressed_" + rpxFile);
//            //updateRpxBackup = Path.Combine(updateCodePath, "backup_" + rpxFile);


//            currentSession.gameRpxPath = settingsXmlRpxLineParts[0];
//            currentSession.gameRpxFileInfo = new FileInfo(currentSession.gameRpxPath);
//            currentSession.updateRpxPath = Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "mlc01", "usr",
//                "title", currentSession.FirstID, currentSession.SecondID);

//            currentSession.updateCodePath = Path.Combine(currentSession.updateRpxPath, "code");
//            currentSession.updateMetaPath = Path.Combine(currentSession.updateRpxPath, "meta");

//            currentSession.gameSaveFolder = new DirectoryInfo(Path.Combine(
//                currentSession.rpcs3ExeFile.DirectoryName, "mlc01", "usr", "save", currentSession.FirstID,
//                currentSession.SecondID));



//            currentSession.updateRpxLocation =
//                Path.Combine(currentSession.updateCodePath, currentSession.rpxFile);
//            currentSession.updateRpxCompressed = Path.Combine(currentSession.updateCodePath,
//                "compressed_" + currentSession.rpxFile);
//            currentSession.updateRpxBackup =
//                Path.Combine(currentSession.updateCodePath, "backup_" + currentSession.rpxFile);
//            currentSession.updateRpxUncompressedToken =
//                Path.Combine(currentSession.updateCodePath, "UNCOMPRESSED.txt");

//            return true;
//        }

//        private bool LoadDataFromRPCS3FilesBin()
//        {
//            ///
//            ///gathering data from log.txt and settings.xml files
//            ///

//            string[] logTxt = File.ReadAllLines(Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "log.txt"));
//            //string[] settingsXml = File.ReadAllLines(Path.Combine(rpcs3ExeFile.DirectoryName, "settings.xml"));
//            byte[] settingsBin = File.ReadAllBytes(Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "settings.bin"));

//            //getting rpx filename from log.txt
//            string logLoadingLine = logTxt.FirstOrDefault(it => it.Contains("Loading") && it.Contains(".rpx"));

//            if (String.IsNullOrWhiteSpace(logLoadingLine))
//            {
//                MessageBox.Show(
//                    "Could not find an rpx file to corrupt.\n\n" +
//                    "If the game you are trying to corrupt is in Wud format, you must extract it for it to be corruptible\n\n" +
//                    "Loading aborted.", "Error finding game");
//                state = RPCS3State.UNFOUND;
//                return false;
//            }

//            string[] logLoadingLineParts = logLoadingLine.Split(' ');
//            currentSession.rpxFile = logLoadingLineParts[logLoadingLineParts.Length - 1];

//            //Getting rpx path from settings.bin
//            byte[] rpx = { 0x2E, 0x00, 0x72, 0x00, 0x70, 0x00, 0x78, 0x00 }; //".rpx" encoded as utf-16
//            int startOffset = 0xB7;
//            var endOffset = Array.IndexOf(settingsBin, rpx) + rpx.Length;



//            byte[] tmp = new byte[endOffset - startOffset];
//            Array.Copy(settingsBin, startOffset, tmp, 0, endOffset - startOffset);
//            var gamePath = Encoding.Unicode.GetString(tmp);

//            try
//            {
//                if (File.Exists(gamePath))
//                {
//                    Console.WriteLine("Found game " + gamePath);
//                }
//                else
//                {
//                    throw new Exception("Couldn't find RPX");
//                }
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show("Something went wrong when locating the RPX of the running game.\nYou can probably fix this by going to your RPCS3 folder and deleting settings.bin, then trying again.\nIf this doesn't fix it, poke the devs.\n\nCouldn't find: " + gamePath);
//                state = RPCS3State.UNFOUND;
//                return false;
//            }


//            currentSession.gameRpxPath = gamePath;
//            currentSession.gameRpxFileInfo = new FileInfo(currentSession.gameRpxPath);
//            currentSession.updateRpxPath = Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "mlc01", "usr", "title", currentSession.FirstID, currentSession.SecondID);

//            currentSession.updateCodePath = Path.Combine(currentSession.updateRpxPath, "code");
//            currentSession.updateMetaPath = Path.Combine(currentSession.updateRpxPath, "meta");

//            currentSession.gameSaveFolder = new DirectoryInfo(Path.Combine(currentSession.rpcs3ExeFile.DirectoryName, "mlc01", "usr", "save", currentSession.FirstID, currentSession.SecondID));



//            currentSession.updateRpxLocation = Path.Combine(currentSession.updateCodePath, currentSession.rpxFile);
//            currentSession.updateRpxCompressed = Path.Combine(currentSession.updateCodePath, "compressed_" + currentSession.rpxFile);
//            currentSession.updateRpxBackup = Path.Combine(currentSession.updateCodePath, "backup_" + currentSession.rpxFile);
//            currentSession.updateRpxUncompressedToken = Path.Combine(currentSession.updateCodePath, "UNCOMPRESSED.txt");

//            return true;
//        }


//        public bool LoadKnownGames()
//        {
//            JsonSerializer serializer = new JsonSerializer();
//            string path = Path.Combine(rpcs3Dir, "PARAMS", "knowngames.json");
//            if (!File.Exists(path))
//            {
//                knownGamesDico = new Dictionary<string, RPCS3StubSession>();
//                return true;
//            }
//            try
//            {

//                using (StreamReader sw = new StreamReader(path))
//                using (JsonTextReader reader = new JsonTextReader(sw))
//                {
//                    knownGamesDico = serializer.Deserialize<Dictionary<string, RPCS3StubSession>>(reader);
//                }

//                foreach (var key in knownGamesDico.Keys)
//                    cbSelectedGame.Items.Add(key);
//            }
//            catch (IOException e)
//            {
//                MessageBox.Show("Unable to access the filemap! Figure out what's locking it and then restart the WGH.\n" + e.ToString());
//                return false;
//            }
//            return true;
//        }
//        public bool SaveKnownGames()
//        {
//            JsonSerializer serializer = new JsonSerializer();
//            var path = Path.Combine(rpcs3Dir, "PARAMS", "knowngames.json");
//            try
//            {
//                using (StreamWriter sw = new StreamWriter(path))
//                using (JsonWriter writer = new JsonTextWriter(sw))
//                {
//                    serializer.Serialize(writer, knownGamesDico);
//                }
//            }
//            catch (IOException e)
//            {
//                MessageBox.Show("Unable to access the known games!\n" + e.ToString());
//                return false;
//            }
//            return true;
//        }

//        //private static bool LoadRpxFileInterface()
//        //{
//        //    try
//        //    {
//        //        currentSession.fileInterfaceTargetId = "File|" + currentSession.updateRpxLocation;

//        //        var ft = new FileTarget(currentSession.gameRpxPath, null);

//        //        rpxInterface = new FileInterface(ft);
//        //        rpxInterface.getMemoryDump();
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex);

//        //        if (ex is FileNotFoundException && knownGamesDico.ContainsKey(currentSession.gameName))
//        //        {
//        //            object selectedItem = cbSelectedGame.SelectedItem;
//        //            cbSelectedGame.SelectedIndex = 0;

//        //            if (MessageBox.Show($"Do you want to remove the entry for {selectedItem}?", "Error lading rpx file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//        //            {
//        //                cbSelectedGame.Items.Remove(selectedItem);
//        //                knownGamesDico.Remove(selectedItem.ToString());
//        //                SaveKnownGames();
//        //            }

//        //        }
//        //        else
//        //        {
//        //            cbSelectedGame.SelectedIndex = 0;
//        //        }
//        //        return false;

//        //    }
//        //}

//        internal string PrepareUpdateFolder(bool overwrite = false)
//        {
//            if (overwrite)
//                if (Directory.Exists(currentSession.updateRpxPath))
//                    Directory.Delete(currentSession.updateRpxPath, true);


//            //Creating fake update if update doesn't already exist
//            if (!Directory.Exists(currentSession.updateRpxPath) || !File.Exists(currentSession.updateRpxLocation))
//            {
//                Directory.CreateDirectory(currentSession.updateRpxPath);
//                Directory.CreateDirectory(currentSession.updateCodePath);
//                Directory.CreateDirectory(currentSession.updateMetaPath);

//                foreach (var file in currentSession.gameRpxFileInfo.Directory.GetFiles())
//                    File.Copy(file.FullName, Path.Combine(currentSession.updateCodePath, file.Name), true);

//                DirectoryInfo gameDirectoryInfo = currentSession.gameRpxFileInfo.Directory.Parent;
//                DirectoryInfo metaDirectoryInfo = new DirectoryInfo(currentSession.updateMetaPath);

//                foreach (var file in metaDirectoryInfo.GetFiles())
//                    File.Copy(file.FullName, currentSession.updateMetaPath);

//            }

//            //Uncompress update rpx if it isn't already

//            DirectoryInfo updateCodeDirectoryInfo = new DirectoryInfo(currentSession.updateCodePath);
//            currentSession.updateCodeFiles = updateCodeDirectoryInfo.GetFiles();

//            if (!File.Exists(currentSession.updateRpxUncompressedToken))
//            {
//                if (File.Exists(currentSession.updateRpxCompressed))
//                    File.Delete(currentSession.updateRpxLocation);
//                else
//                    File.Move(currentSession.updateRpxLocation, currentSession.updateRpxCompressed);

//                ProcessStartInfo psi = new ProcessStartInfo();
//                psi.FileName = Path.Combine(rpcs3Dir, "wiiurpxtool.exe");
//                psi.WorkingDirectory = rpcs3Dir;
//                psi.Arguments = $"-d \"{currentSession.updateRpxCompressed}\" \"{currentSession.updateRpxLocation}\"";
//                psi.RedirectStandardOutput = true;
//                psi.RedirectStandardError = true;
//                psi.UseShellExecute = false;
//                psi.CreateNoWindow = true;
//                var p = Process.Start(psi);

//                p.WaitForExit();

//                File.WriteAllText(currentSession.updateRpxUncompressedToken, "DONE");
//            }

//            return currentSession.updateRpxLocation;
//        }

//        internal void UnmodGame()
//        {
//            KillRPCS3Process(false);

//            //remove item from known games and go back to autodetect
//            var lastRef = currentSession;

//            //remove fake update from game
//            if (File.Exists(lastRef.updateRpxCompressed))
//            {
//                if (File.Exists(lastRef.updateRpxLocation))
//                    File.Delete(lastRef.updateRpxLocation);

//                if (File.Exists(lastRef.updateRpxCompressed))
//                {
//                    File.Copy(lastRef.updateRpxCompressed, lastRef.updateRpxLocation);
//                    File.Delete(lastRef.updateRpxCompressed);
//                }

//                if (File.Exists(lastRef.updateRpxBackup))
//                    File.Delete(lastRef.updateRpxBackup);

//                if (File.Exists(lastRef.updateRpxUncompressedToken))
//                    File.Delete(lastRef.updateRpxUncompressedToken);
//            }
//            else if (Directory.Exists(lastRef.updateRpxPath))
//                Directory.Delete(lastRef.updateRpxPath, true);

//            FileInterface.CompositeFilenameDico.Remove(lastRef.gameName);
//            knownGamesDico.Remove(lastRef.gameName);
//            SaveKnownGames();
//            cbSelectedGame.SelectedIndex = 0;
//            cbSelectedGame.Items.Remove(lastRef.gameName);
//        }


//        internal bool SelectGame(string selected = null)
//        {
//            if (selected != null && selected != "Autodetect")
//                currentSession = knownGamesDico[selected];

//            var rpcs3FullPath = currentSession.rpcs3ExeFile;
//            if (!File.Exists(rpcs3FullPath.FullName))
//            {
//                //RPCS3 could not be found. Prompt a message for replacement, a browse box, and replace all refs for the known games

//                string message = "FileStub couldn't find RPCS3 emulator. Would you like to specify a new location?";
//                var result = MessageBox.Show(message, "Error finding rpcs3", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

//                string rpcs3Location = null;
//                if (result == DialogResult.Yes)
//                {
//                    OpenFileDialog ofd = new OpenFileDialog
//                    {
//                        DefaultExt = "exe",
//                        Title = "Open RPCS3 Emulator",
//                        Filter = "RPCS3 Emulator|*.exe",
//                        RestoreDirectory = true
//                    };
//                    if (ofd.ShowDialog() == DialogResult.OK)
//                    {
//                        rpcs3Location = ofd.FileName;
//                    }
//                    else
//                    {
//                        cbSelectedGame.SelectedIndex = 0;
//                        return false;
//                    }

//                    currentSession.rpcs3ExeFile = new FileInfo(rpcs3Location);
//                    foreach (RPCS3StubSession cgi in knownGamesDico.Values)
//                        cgi.rpcs3ExeFile = currentSession.rpcs3ExeFile;
//                    SaveKnownGames();

//                }
//                else
//                {
//                    cbSelectedGame.SelectedIndex = 0;
//                    return false;
//                }
//            }

//            string rpxFullPath = currentSession.gameRpxFileInfo.FullName;
//            if (!File.Exists(rpxFullPath))
//            {
//                string message = "RPCS3 Stub couldn't find the Rpx file for this game. Would you like to remove this entry?";
//                var result = MessageBox.Show(message, "Error finding game", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

//                if (result == DialogResult.Yes)
//                    UnmodGame();

//                cbSelectedGame.SelectedIndex = 0;
//                return false;
//            }

//            //if (!LoadRpxFileInterface())
//            //    return false;



//            //load target here




//            state = RPCS3State.READY;
//            //S.GET<StubForm>().lbRPCS3Status.Text = "Ready for corrupting";
//            //S.GET<StubForm>().lbTargetedGameRpx.Text = currentSession.gameRpxFileInfo.FullName;
//            //S.GET<StubForm>().lbTargetedGameId.Text = "Game ID: " + currentSession.FirstID + "-" + currentSession.SecondID;
//            //EnableInterface();

//            return true;
//        }


//        private string SearchForRPCS3Instance()
//        {

//            ScanRPCS3();

//            if (state == RPCS3State.RUNNING && rpcs3Process.MainWindowTitle.Contains("[TitleId:"))
//                state = RPCS3State.GAMELOADED;

//            if (state == RPCS3State.GAMELOADED)
//            {
//                state = RPCS3State.PREPARING; // this prevents the ticker to call this method again

//                //Game is loaded in rpcs3, let's gather all the info we need


//                if (!FetchBaseInfoFromRPCS3Process())
//                {
//                    return null; //Couldn't fetch the correct info, or they were in online mode
//                }


//                KillRPCS3Process(true);

//                if (!LoadDataFromRPCS3FilesXml())
//                {
//                    MessageBox.Show("Failed to get RPX file location from RPCS3.\nIf you continue to see this error, let the RTC Devs know.");
//                    return null; //Could not get the rpx file location
//                }

//                // Prepare fake update and backup
//                var rpxTargetFile = PrepareUpdateFolder();

//                knownGamesDico[currentSession.gameName] = currentSession;

//                if (!SelectGame())
//                    return null;

//                DontSelectGame = true;
//                cbSelectedGame.Items.Add(currentSession.gameName);
//                cbSelectedGame.SelectedIndex = cbSelectedGame.Items.Count - 1;
//                DontSelectGame = false;

//                foreach (RPCS3StubSession cgi in knownGamesDico.Values)
//                    cgi.rpcs3ExeFile = currentSession.rpcs3ExeFile;

//                SaveKnownGames();

//                return rpxTargetFile;

//            }

//            return null;

//        }


//        public void BrowseFiles()
//        {
//            string folderpath;

//            OpenFileDialog OpenFileDialog1;
//            OpenFileDialog1 = new OpenFileDialog();

//            OpenFileDialog1.Title = "Open Game's PS3_DISC.SFB";
//            OpenFileDialog1.Filter = "DISC SFB|PS3_DISC.SFB";
//            OpenFileDialog1.RestoreDirectory = true;
//            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
//            {
//                if (OpenFileDialog1.FileName.ToString().Contains('^'))
//                {
//                    MessageBox.Show("You can't use a file that contains the character ^ ");
//                    lbGameFolder.Text = "";
//                    return;
//                }

//                folderpath = new FileInfo(OpenFileDialog1.FileName).DirectoryName;
//            }
//            else
//            {
//                lbGameFolder.Text = "";
//                return;
//            }

//            lbGameFolder.Text = folderpath;
//        }

//        private void FileStubTemplateRPCS3_Load(object sender, EventArgs e)
//        {
//            cbSelectedGame.SelectedIndex = 0;
//            LoadKnownGames();
//        }

//        private void cbSelectedGame_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            var selected = cbSelectedGame.SelectedItem.ToString();

//            if (selected == "Autodetect")
//                return;

//            if (!SelectGame(selected))
//            {
//                cbSelectedGame.SelectedIndex = 0;
//                return;
//            }

//            S.GET<StubForm>().btnLoadTargets_Click(null, null);

//        }

//        private void btnGetSegments_Click(object sender, EventArgs e)
//        {
//            foreach (var fi in (FileWatch.currentSession.fileInterface as MultipleFileInterface).FileInterfaces)
//                GetSegments(fi);
//        }

//        public void ExecuteGame()
//        {
//            rpcs3Process.StartInfo.FileName = currentSession.rpcs3ExeFile.FullName;
//            rpcs3Process.StartInfo.Arguments = $"-g \"{currentSession.gameRpxFileInfo.FullName}\"";
//            rpcs3Process.Start();
//        }

//    }

//    enum RPCS3State
//    {
//        OFF,
//        RUNNING,
//        GAMELOADED,
//        PREPARING,
//        READY
//    }

//    public class RPCS3StubSession
//    {
//        public FileInfo gameEbootFileInfo = null;
//        public FileInfo rpcs3ExeFile = null;
//        public FileInfo[] updateCodeFiles = null;
//        public DirectoryInfo gameSaveFolder = null;
//        public string ebootFilePath = null;
//        public string gameFolderPath = null;
//        public string gameUSRDIRPath = null;
//        public string gameSerial = null;
//        public string gameinfopath = null;
//        public string gameName = "Autodetect";
//        public string ebootUncompressedToken = null;
//        public FileInterface ebootInterface = null;
//        internal FileMemoryInterface fileInterface;

//        public override string ToString()
//        {
//            return gameName;
//        }
//    }

//    public static class RPCS3WindowHandleInfo
//    {
//        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

//        [DllImport("user32")]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);

//        public static List<IntPtr> GetAllChildHandles(IntPtr MainHandle)
//        {
//            List<IntPtr> childHandles = new List<IntPtr>();

//            GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
//            IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

//            try
//            {
//                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
//                EnumChildWindows(MainHandle, childProc, pointerChildHandlesList);
//            }
//            finally
//            {
//                gcChildhandlesList.Free();
//            }

//            return childHandles;
//        }

//        private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
//        {
//            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

//            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
//            {
//                return false;
//            }

//            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
//            childHandles.Add(hWnd);

//            return true;
//        }
//    }
//}
