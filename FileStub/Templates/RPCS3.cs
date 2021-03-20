namespace FileStub.Templates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting.Messaging;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.UI;

    public partial class FileStubTemplateRPCS3 : Form, IFileStubTemplate
    {

        const string RPCS3STUB_EBOOT = "RPCS3 : PS3 game EBOOT.BIN";
        const string RPCS3STUB_ALLSELFSALLSPRXS = "RPCS3 : All PS3 game SELFS and SPRXs";


        public string rpcs3SpecDir = Path.Combine(FileStub.FileWatch.currentDir, "RPCS3");
        public string rpcs3EmuDir = Path.Combine(FileStub.FileWatch.currentDir, "EMUS", "RPCS3");
        public string rpcs3ExePath = Path.Combine(FileStub.FileWatch.currentDir, "EMUS", "RPCS3", "rpcs3.exe");
        Process rpcs3Process = new Process();
        public GameInfo gameInfo = null;
        private RPCS3State _state = RPCS3State.OFF;

        private RPCS3State state
        {
            get => _state;
            set
            {
                Console.WriteLine($"Setting state to {value}");
                _state = value;
            }
        }

        public Dictionary<string, RPCS3StubSession> knownGamesDico = new Dictionary<string, RPCS3StubSession>();
        public RPCS3StubSession currentSession = new RPCS3StubSession();

        public bool DontSelectGame = false;

        string currentSelectedTemplate = null;
        public string[] TemplateNames
        {
            get => new string[] {
            RPCS3STUB_EBOOT,
            RPCS3STUB_ALLSELFSALLSPRXS
        };
        }

        public bool DisplayDragAndDrop => true;
        public bool DisplayBrowseTarget => true;


        public FileStubTemplateRPCS3()
        {
            InitializeComponent();

            //ensure rpcs3 folders exist
            if (!Directory.Exists(rpcs3SpecDir))
                Directory.CreateDirectory(rpcs3SpecDir);

            string rpcs3ParamsDir = Path.Combine(rpcs3SpecDir, "PARAMS");

            if (!Directory.Exists(rpcs3ParamsDir))
                Directory.CreateDirectory(rpcs3ParamsDir);
            rpcs3Process.StartInfo.FileName = rpcs3ExePath;
        }

        public FileTarget[] GetTargets()
        {
            if (DecryptAll() == false)
            {
                MessageBox.Show("Decryption failed!");
                Application.Exit();
            }
            string targetFolder = lbGameFolder.Text;

            if (targetFolder == "")
            {
                MessageBox.Show("No target loaded");
                return null;
            }
            string ebootpath = currentSession.ebootFilePath;
            List<FileTarget> targets = new List<FileTarget>();

            var ebootFileInfo = new FileInfo(ebootpath);
            var ebootFolder = ebootFileInfo.Directory.FullName;

            var baseFolder = ebootFileInfo.Directory;

            string baseless(string path) => path.Replace(ebootFolder, "");

            var ebootTarget = Vault.RequestFileTarget(baseless(ebootFileInfo.FullName), baseFolder.FullName);
            List<FileInfo> allselfsallsprxs = new List<FileInfo>();
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            while (a < gameInfo.SELFPATHSLOWERCASECOUNT)
            {
                allselfsallsprxs.Add(new FileInfo(gameInfo.SELFPATHSLOWERCASE[a]));
                a++;
            }
            while (b < gameInfo.SELFPATHSUPPERCASECOUNT)
            {
                allselfsallsprxs.Add(new FileInfo(gameInfo.SELFPATHSUPPERCASE[b]));
                b++;
            }
            while (c < gameInfo.SPRXPATHSLOWERCASECOUNT)
            {
                allselfsallsprxs.Add(new FileInfo(gameInfo.SPRXPATHSLOWERCASE[c]));
                c++;
            }
            while (d < gameInfo.SPRXPATHSUPPERCASECOUNT)
            {
                allselfsallsprxs.Add(new FileInfo(gameInfo.SPRXPATHSUPPERCASE[d]));
                d++;
            }

            switch (currentSelectedTemplate)
            {
                case RPCS3STUB_EBOOT:
                    {
                        targets.Add(ebootTarget);
                    }
                    break;
                case RPCS3STUB_ALLSELFSALLSPRXS:
                    {
                        targets.Add(ebootTarget);
                        targets.AddRange(allselfsallsprxs.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
            }

            knownGamesDico[currentSession.gameName] = currentSession;
            if(!cbSelectedGame.Items.Contains(currentSession.gameName))
                cbSelectedGame.Items.Add(currentSession.gameName);
            cbSelectedGame.SelectedIndex = cbSelectedGame.Items.Count - 1;
            currentSession._game = gameInfo;
            lbTargetedGameRpx.Visible = false;
            string savedata = Path.Combine(rpcs3EmuDir, "dev_hdd0", "home", "00000001", "savedata");
            if (Directory.GetDirectories(savedata, currentSession.gameSerial).Length != 0)
            {
                currentSession.possibleGameSaveFolders = new DirectoryInfo(savedata).GetDirectories(currentSession.gameSerial);
                currentSession.firstgameSaveFolder = currentSession.possibleGameSaveFolders.FirstOrDefault();
                currentSession.firstgameSaveFolderPath = currentSession.firstgameSaveFolder.FullName;
            }
            else { currentSession.possibleGameSaveFolders = null; }
            foreach (RPCS3StubSession cgi in knownGamesDico.Values)
            {

                cgi.ebootFilePath = currentSession.ebootFilePath;
                cgi.gameName = currentSession.gameName;
                cgi.gameinfopath = currentSession.gameinfopath;
                cgi.gameSerial = currentSession.gameSerial;
                cgi.gameUSRDIRPath = currentSession.gameUSRDIRPath;
                cgi.gameFolderPath = currentSession.gameFolderPath;
                cgi._game = currentSession._game;
                cgi.firstgameSaveFolder = currentSession.firstgameSaveFolder;
                cgi.firstgameSaveFolderPath = currentSession.firstgameSaveFolderPath;
                cgi.possibleGameSaveFolders = currentSession.possibleGameSaveFolders;
            }
            //SaveKnownGames(); //saving and loading jsons causes issues for some reason with this template right now so for now I'm disabling knowngames.json for this template
            //Prepare filestub for execution
            var sf = S.GET<StubForm>();
            FileWatch.currentSession.selectedExecution = ExecutionType.EXECUTE_OTHER_PROGRAM;
            Executor.otherProgram = rpcs3ExePath;
            sf.tbArgs.Text = $"\"{gameInfo.EBOOTPATH}\"";
            FileWatch.currentSession.bigEndian = true;
            lbGameInfo.Text =
$@"NAME: {gameInfo.NAME}
SERIAL: {gameInfo.SERIAL}
VERSION: {gameInfo.VERSION}
TYPE: {gameInfo.TYPE}

";
            return targets.ToArray();
        }

        public void GetSegments(FileInterface exeInterface)
        {

        }
        public Form GetTemplateForm(string name)
        {
            this.SummonTemplate(name);
            return this;
        }

        private void SummonTemplate(string name)
        {
            currentSelectedTemplate = name;

            lbTemplateDescription.Text =
$@"== Corrupt PS3 game executables ==
Load or drag and drop a PS3 game's folder.
For some reason, at the moment this template won't work unless FileStub is already connected to RTCV.
";
        }
        bool DecryptElf(string elfpath)
        {
            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            byte[] magicnumber = new byte[4];
            FileStream openread = File.OpenRead(elfpath);
            openread.Read(magicnumber, 0, 4);
            openread.Close();
            if (!System.Text.Encoding.ASCII.GetString(magicnumber).Contains("SCE"))
                return true; //assume the file's already decrypted
            if(!File.Exists(elfpath +".bak"))
                File.Copy(elfpath, elfpath + ".bak");
            rpcs3Process.StartInfo.Arguments = $"--decrypt \"{elfpath}\"";
            rpcs3Process.Start();

            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            openread = File.OpenRead(elfpath);
            openread.Read(magicnumber, 0, 4);
            openread.Close();
            if (!System.Text.Encoding.ASCII.GetString(magicnumber).Contains("SCE") || string.IsNullOrEmpty(System.Text.Encoding.ASCII.GetString(magicnumber)))
                return false;
            else return true;
        }
        public bool DecryptAll()
        {
            if (!string.IsNullOrWhiteSpace(gameInfo.EBOOTPATH))
            {
                if (DecryptElf(gameInfo.EBOOTPATH) == false) return false;
            }
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            while (a < gameInfo.SELFPATHSLOWERCASECOUNT)
            {
                if (!string.IsNullOrWhiteSpace(gameInfo.SELFPATHSLOWERCASE[a]))
                {
                    if (DecryptElf(gameInfo.SELFPATHSLOWERCASE[a]) == false) return false;
                }
                a++;
            }
            while (b < gameInfo.SELFPATHSUPPERCASECOUNT)
            {
                if (!string.IsNullOrWhiteSpace(gameInfo.SELFPATHSUPPERCASE[b]))
                {
                    if (DecryptElf(gameInfo.SELFPATHSUPPERCASE[b]) == false) return false;
                }
                b++;
            }
            while (c < gameInfo.SPRXPATHSLOWERCASECOUNT)
            {
                if (!string.IsNullOrWhiteSpace(gameInfo.SPRXPATHSLOWERCASE[c]))
                {
                    if (DecryptElf(gameInfo.SPRXPATHSLOWERCASE[c]) == false)
                        return false;
                }
                c++;
            }
            while (d < gameInfo.SPRXPATHSUPPERCASECOUNT)
            {
                if (!string.IsNullOrWhiteSpace(gameInfo.SPRXPATHSUPPERCASE[d]))
                {
                    if (DecryptElf(gameInfo.SPRXPATHSUPPERCASE[d]) == false)
                        return false;
                }
                d++;
            }
            return true;
        }
        void UpdateRpcs3ProcessInfo()
        {

            if (Process.GetProcessesByName("rpcs3").Length != 0)
            {
                _state = RPCS3State.RUNNING;
                if(!Process.GetProcessesByName("rpcs3").FirstOrDefault().Responding)
                {
                    Process.GetProcessesByName("rpcs3").FirstOrDefault().Kill();
                    _state = RPCS3State.OFF;
                }
            }
            else _state = RPCS3State.OFF;
        }
        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if (!File.Exists(Path.Combine(rpcs3EmuDir, "config.yml")))
            {
                MessageBox.Show($"Set up your RPCS3 (in \"{rpcs3EmuDir}\") first!!!");
                return false;
            }
            if (fd.Length > 1 || File.Exists(fd[0]))
            {
                MessageBox.Show("Please only drop the game's folder");
                lbGameFolder.Text = "";
                return false;
            }
            lbGameFolder.Text = fd[0];
            currentSession.gameFolderPath = lbGameFolder.Text;
            currentSession.gameinfopath = Path.Combine(currentSession.gameFolderPath, "gameinfo.txt");
            if (File.Exists(currentSession.gameinfopath))
                File.Delete(currentSession.gameinfopath);
            //here is where we grab the game's gameinfo from rpcs3
            rpcs3Process.StartInfo.Arguments = $"--getgameinfo \"{currentSession.gameFolderPath}\"";
            UpdateRpcs3ProcessInfo();
            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            rpcs3Process.Start();
            UpdateRpcs3ProcessInfo();
            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            gameInfo = new GameInfo(currentSession.gameinfopath);
            currentSession.gameUSRDIRPath = gameInfo.USRDIRPATH;
            currentSession.ebootFilePath = gameInfo.EBOOTPATH;
            currentSession.gameName = gameInfo.NAME;
            currentSession.gameSerial = gameInfo.SERIAL;
            return true;
        }

        public bool LoadKnownGames()
        {
            JsonSerializer serializer = new JsonSerializer();
            string path = Path.Combine(rpcs3SpecDir, "PARAMS", "knowngames.json");
            if (!File.Exists(path))
            {
                knownGamesDico = new Dictionary<string, RPCS3StubSession>();
                return true;
            }
            try
            {

                using (StreamReader sw = new StreamReader(path))
                using (JsonTextReader reader = new JsonTextReader(sw))
                {
                    knownGamesDico = serializer.Deserialize<Dictionary<string, RPCS3StubSession>>(reader);
                }

                foreach (var key in knownGamesDico.Keys)
                    cbSelectedGame.Items.Add(key);
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the filemap! Figure out what's locking it and then restart the WGH.\n" + e.ToString());
                return false;
            }
            return true;
        }
        public bool SaveKnownGames()
        {
            JsonSerializer serializer = new JsonSerializer();
            var path = Path.Combine(rpcs3SpecDir, "PARAMS", "knowngames.json");
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, knownGamesDico);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the known games!\n" + e.ToString());
                return false;
            }
            return true;
        }



        internal bool SelectGame(string selected = null)
        {
            if (selected != null && selected != "Autodetect")
                currentSession = knownGamesDico[selected];

            var rpcs3FullPath = currentSession.rpcs3ExeFile;
            if (!File.Exists(rpcs3ExePath))
            {
                //RPCS3 could not be found.

                string message = "RPCS3 could not be found. Did you delete it from the FileStub folder?";
                var result = MessageBox.Show(message, "Error finding rpcs3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string ebootFullPath = currentSession.ebootFilePath;
            if (!File.Exists(ebootFullPath))
            {
                string message = "RPCS3 Stub couldn't find the eboot file for this game. Would you like to remove this entry?";
                var result = MessageBox.Show(message, "Error finding game", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    FileInterface.CompositeFilenameDico.Remove(currentSession.gameName);
                    knownGamesDico.Remove(currentSession.gameName);
                    //SaveKnownGames();
                    cbSelectedGame.SelectedIndex = 0;
                    cbSelectedGame.Items.Remove(currentSession.gameName);
                }

                cbSelectedGame.SelectedIndex = 0;
                UpdateRpcs3ProcessInfo();
                return false;
            }

            //S.GET<StubForm>().lbRPCS3Status.Text = "Ready for corrupting";
            //S.GET<StubForm>().lbTargetedGameRpx.Text = currentSession.gameRpxFileInfo.FullName;
            //S.GET<StubForm>().lbTargetedGameId.Text = "Game ID: " + currentSession.FirstID + "-" + currentSession.SecondID;
            //EnableInterface();

            return true;
        }



        public void BrowseFiles()
        {
            if (!File.Exists(Path.Combine(rpcs3EmuDir, "config.yml")))
            {
                MessageBox.Show($"Set up your RPCS3 (in \"{rpcs3EmuDir}\") first!!!");
                return;
            }
            string folderpath;
            FolderBrowserDialog folderBrowserDialog;
            folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.Description = "Open PS3 Game Folder";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath.ToString().Contains('^'))
                {
                    MessageBox.Show("You can't use a folder that contains the character ^ ");
                    lbGameFolder.Text = "";
                    return;
                }
                
                folderpath = folderBrowserDialog.SelectedPath;
            }
            else
            {
                lbGameFolder.Text = "";
                return;
            }

            lbGameFolder.Text = folderpath;
            currentSession.gameFolderPath = lbGameFolder.Text;
            currentSession.gameinfopath = Path.Combine(currentSession.gameFolderPath, "gameinfo.txt");
            if (File.Exists(currentSession.gameinfopath))
                File.Delete(currentSession.gameinfopath);
            //here is where we grab the game's gameinfo from rpcs3
            UpdateRpcs3ProcessInfo();
            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            rpcs3Process.StartInfo.Arguments = $"--gameinfo \"{currentSession.gameFolderPath}\"";
            rpcs3Process.Start();
            UpdateRpcs3ProcessInfo();
            while (_state == RPCS3State.RUNNING)
            {
                UpdateRpcs3ProcessInfo();
            }
            gameInfo = new GameInfo(currentSession.gameinfopath);
            currentSession.gameUSRDIRPath = gameInfo.USRDIRPATH;
            currentSession.ebootFilePath = gameInfo.EBOOTPATH;
            currentSession.gameName = gameInfo.NAME;
            currentSession.gameSerial = gameInfo.SERIAL;
        }

        private void FileStubTemplateRPCS3_Load(object sender, EventArgs e)
        {
            cbSelectedGame.SelectedIndex = 0;
            //LoadKnownGames();
        }

        private void cbSelectedGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cbSelectedGame.SelectedItem.ToString();

            if (selected == "Autodetect")
                return;

            if (!SelectGame(selected))
            {
                cbSelectedGame.SelectedIndex = 0;
                return;
            }

            S.GET<StubForm>().btnLoadTargets_Click(null, null);

        }

        private void btnGetSegments_Click(object sender, EventArgs e)
        {

        }


    }

    enum RPCS3State
    {
        OFF,
        RUNNING,
        GAMELOADED,
        PREPARING,
        READY
    }

    public class RPCS3StubSession
    {
        public FileInfo gameEbootFileInfo = null;
        public FileInfo rpcs3ExeFile = null;
        public FileInfo[] updateCodeFiles = null;
        public DirectoryInfo firstgameSaveFolder = null;
        public DirectoryInfo[] possibleGameSaveFolders = null;
        public string firstgameSaveFolderPath = null;
        public string ebootFilePath = null;
        public string gameFolderPath = null;
        public string gameUSRDIRPath = null;
        public string gameSerial = null;
        public string gameinfopath = null;
        public string gameName = "Autodetect";
        public string ebootUncompressedToken = null;
        public GameInfo _game = null;
        public FileInterface ebootInterface = null;
        internal FileMemoryInterface fileInterface;

        public override string ToString()
        {
            return gameName;
        }
    }
    public class GameInfo
    {
        public string NAME = null;
        public string SERIAL = null;
        public string VERSION = null;
        public string TYPE = null;
        public string PATH = null;
        public string USRDIRPATH = null;
        public string EBOOTPATH = null;
        public string SFODIR = null;
        public string ICON0PATH = null;
        public string[] SELFPATHSLOWERCASE = new string[1024];
        public string[] SELFPATHSUPPERCASE = new string[1024];
        public string[] SPRXPATHSLOWERCASE = new string[1024];
        public string[] SPRXPATHSUPPERCASE = new string[1024];
        public int SELFPATHSLOWERCASECOUNT = 0;
        public int SELFPATHSUPPERCASECOUNT = 0;
        public int SPRXPATHSLOWERCASECOUNT = 0;
        public int SPRXPATHSUPPERCASECOUNT = 0;
        public DirectoryInfo USRDIRINFO;
        public GameInfo(string gameinfotxtpath)
        {
            string[] lines = File.ReadAllLines(gameinfotxtpath);
            NAME = lines[0].Replace("NAME$$", "");
            SERIAL = lines[1].Replace("SERIAL$$", "");
            VERSION = lines[2].Replace("VERSION$$", "");
            TYPE = lines[3].Replace("TYPE$$", "");
            if (TYPE == "DG") TYPE = "Disk Game";
            if (TYPE == "HG") TYPE = "HDD Game";
            if (TYPE == "GD") TYPE = "Game Data";
            PATH = lines[4].Replace("PATH$$", "");
            if (TYPE != "Disk Game")
            {
                USRDIRPATH = PATH + "/USRDIR";
            }else
            {
                USRDIRPATH = lines[5].Replace("USRDIRPATH$$", "") + "/USRDIR";
            }
            SFODIR = USRDIRPATH.Substring(0, USRDIRPATH.IndexOf("/USRDIR"));
            ICON0PATH = SFODIR + "/ICON0.PNG";
            if (TYPE != "Disk Game")
            {
                EBOOTPATH = USRDIRPATH + "/EBOOT.BIN";
            }else
            {
                EBOOTPATH = lines[6].Replace("EBOOTPATH$$", "");
            }
            USRDIRINFO = new DirectoryInfo(USRDIRPATH);
            SELFPATHSLOWERCASE = Directory.GetFiles(USRDIRPATH, "*.self", SearchOption.AllDirectories);
            SELFPATHSLOWERCASECOUNT = SELFPATHSLOWERCASE.Count();
            SELFPATHSUPPERCASE = Directory.GetFiles(USRDIRPATH, "*.ELF", SearchOption.AllDirectories);
            SELFPATHSUPPERCASECOUNT = SELFPATHSUPPERCASE.Count();
            SPRXPATHSLOWERCASE = Directory.GetFiles(USRDIRPATH, "*.sprx", SearchOption.AllDirectories);
            SPRXPATHSLOWERCASECOUNT = SPRXPATHSLOWERCASE.Count();
            SPRXPATHSUPPERCASE = Directory.GetFiles(USRDIRPATH, "*.PRX", SearchOption.AllDirectories);
            SPRXPATHSUPPERCASECOUNT = SPRXPATHSUPPERCASE.Count();
        }
    }
    public static class RPCS3WindowHandleInfo
    {
        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);

        public static List<IntPtr> GetAllChildHandles(IntPtr MainHandle)
        {
            List<IntPtr> childHandles = new List<IntPtr>();

            GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
            IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(MainHandle, childProc, pointerChildHandlesList);
            }
            finally
            {
                gcChildhandlesList.Free();
            }

            return childHandles;
        }

        private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
            childHandles.Add(hWnd);

            return true;
        }
    }
}
