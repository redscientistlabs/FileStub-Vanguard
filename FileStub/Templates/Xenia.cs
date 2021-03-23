namespace FileStub.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.UI;

    public partial class FileStubTemplateXenia : Form, IFileStubTemplate
    {

        const string XENIASTUB_MAIN = "Xenia : XBOX 360 Executables - default.xex";
        const string XENIASTUB_ALL = "Xenia : XBOX 360 Executables - Any XEX File";
        public string XeniaDir = Path.Combine(FileStub.FileWatch.currentDir, "XENIA");
        public string XEXTOOLPATH;
        public string XeniaExePath;
        XeniaTemplateSession currentXeniaSession;
        public Dictionary<string, XeniaTemplateSession> knownGamesDico = new Dictionary<string, XeniaTemplateSession>();
        string currentSelectedTemplate = null;
        Process XeniaProcess = null;
        string gamepath = null;
        public string[] TemplateNames
        {
            get => new string[] {
            XENIASTUB_MAIN,
            XENIASTUB_ALL,
        };
        }

        public bool DisplayDragAndDrop => true;
        public bool DisplayBrowseTarget => true;


        public FileStubTemplateXenia()
        {
            InitializeComponent();
            if (!Directory.Exists(XeniaDir))
                Directory.CreateDirectory(XeniaDir);

            string XeniaParamsDir = Path.Combine(XeniaDir, "PARAMS");

            if (!Directory.Exists(XeniaParamsDir))
                Directory.CreateDirectory(XeniaParamsDir);
            lbNSOTarget.Visible = false;
            XEXTOOLPATH = Path.Combine(XeniaDir, "xextool.exe");
            currentXeniaSession = new XeniaTemplateSession();
            if (File.Exists(Path.Combine(XeniaParamsDir, "XENIALOCATION")))
            {

                XeniaExePath = File.ReadAllText(Path.Combine(XeniaParamsDir, "XENIALOCATION"));
                currentXeniaSession.XeniaExePath = File.ReadAllText(Path.Combine(XeniaParamsDir, "XENIALOCATION"));
                if(!File.Exists(XeniaExePath))
                {
                    MessageBox.Show("FileStub can't find Xenia. Did you move or delete Xenia? Please redefine the location of your Xenia install by clicking \"Select Xenia\".");
                    XeniaExePath = null;
                    currentXeniaSession.XeniaExePath = null;
                }
            }
        }
        public FileTarget[] GetTargets()
        {
            string targetExe = lbNSOTarget.Text;
            lbGameName.Visible = false;
            if (targetExe == "")
            {
                MessageBox.Show("No target loaded");
                return null;
            }

            List<FileTarget> targets = new List<FileTarget>();

            var exeFileInfo = new FileInfo(targetExe);
            var exeFolder = exeFileInfo.Directory.FullName;

            var baseFolder = exeFileInfo.Directory;


            List<FileInfo> allFiles = SelectMultipleForm.DirSearch(baseFolder);

            string baseless(string path) => path.Replace(exeFolder, "");

            var exeTarget = Vault.RequestFileTarget(baseless(exeFileInfo.FullName), baseFolder.FullName);

            //var allDlls = allFiles.Where(it => it.Extension == ".dll");

            var allExecutables = allFiles.Where(it =>
                    it.Name.ToUpper().EndsWith(".XEX")).ToArray();

            var allMain = allExecutables.Where(it =>
                    it.Name.ToUpper().Contains("DEFAULT")
                    ).ToArray();


            switch (currentSelectedTemplate)
            {
                case XENIASTUB_MAIN:
                    {
                        targets.Add(exeTarget);
                    }
                    break;
                case XENIASTUB_ALL:
                    {
                        targets.AddRange(allExecutables.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
            }
            gamepath = targetExe;
            currentXeniaSession.gameName = exeFileInfo.Directory.Name;
            lbGameName.Visible = false;
            knownGamesDico[currentXeniaSession.gameName] = currentXeniaSession;
            cbSelectedGame.Items.Add(currentXeniaSession.gameName);
            cbSelectedGame.SelectedIndex = cbSelectedGame.Items.Count - 1;
            currentXeniaSession.gameMainExePath = gamepath;
            foreach (XeniaTemplateSession cgi in knownGamesDico.Values)
            {

                cgi.XeniaExePath = currentXeniaSession.XeniaExePath;
                cgi.gameMainExePath = currentXeniaSession.gameMainExePath;
            }
            SaveKnownGames();

            //Prepare filestub for execution
            var sf = S.GET<StubForm>();
            FileWatch.currentSession.selectedExecution = ExecutionType.EXECUTE_OTHER_PROGRAM;
            Executor.otherProgram = currentXeniaSession.XeniaExePath;
            sf.tbArgs.Text = $"\"{currentXeniaSession.gameMainExePath}\"";
            FileWatch.currentSession.bigEndian = true;
            return targets.ToArray();
        }

        public Form GetTemplateForm(string name)
        {
            this.SummonTemplate(name);
            return this;
        }

        public bool LoadKnownGames()
        {
            JsonSerializer serializer = new JsonSerializer();
            string path = Path.Combine(XeniaDir, "PARAMS", "knowngames.json");
            if (!File.Exists(path))
            {
                knownGamesDico = new Dictionary<string, XeniaTemplateSession>();
                return true;
            }
            try
            {

                using (StreamReader sw = new StreamReader(path))
                using (JsonTextReader reader = new JsonTextReader(sw))
                {
                    knownGamesDico = serializer.Deserialize<Dictionary<string, XeniaTemplateSession>>(reader);
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
            var path = Path.Combine(XeniaDir, "PARAMS", "knowngames.json");
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
        private void SummonTemplate(string name)
        {
            currentSelectedTemplate = name;

            lbTemplateDescription.Text =
$@"== Corrupt XBOX 360 Games ==
Click on Select Xenia and select the location of your version of Xenia you wish to use, then...
Decompress all XEX files, then...
Load or drag and drop default.xex.
";
        }

        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if (currentXeniaSession.XeniaExePath == null)
            {
                MessageBox.Show("You need to specify Xenia's location first.");
                lbNSOTarget.Text = "";
                return false;

            }
            if (fd.Length > 1 || fd[0].EndsWith("\\") || !fd[0].ToUpper().Contains("DEFAULT.XEX"))
            {
                MessageBox.Show("Please only drop the game's main executable");
                lbNSOTarget.Text = "";
                return false;
            }

            lbNSOTarget.Text = fd[0];
            return true;
        }

        public void BrowseFiles()
        {
            if (currentXeniaSession.XeniaExePath == null)
            {
                MessageBox.Show("You need to specify Xenia's location first.");
                lbNSOTarget.Text = "";
                return;

            }
            string filename;

            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.Title = "Open Main Executable";
            OpenFileDialog1.Filter = "default.xex|default.xex";
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FileName.ToString().Contains('^'))
                {
                    MessageBox.Show("You can't use a file that contains the character ^ ");
                    lbNSOTarget.Text = "";
                    return;
                }

                filename = OpenFileDialog1.FileName;
            }
            else
            {
                lbNSOTarget.Text = "";
                return;
            }
            lbNSOTarget.Text = filename;
        }

        public void GetSegments(FileInterface exeInterface)
        {
        }

        private void btnEditExec_Click(object sender, EventArgs e)
        {

            string filename = "";

            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.Title = "Open Xenia";
            OpenFileDialog1.Filter = "Xenia|xenia.exe";
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FileName.ToString().Contains('^'))
                {
                    MessageBox.Show("You can't use a file that contains the character ^ ");
                    //lbNSOTarget.Text = "";
                    return;
                }

                filename = OpenFileDialog1.FileName;
            }
            XeniaExePath = filename;
            currentXeniaSession.XeniaExePath = filename;
            if (File.Exists(Path.Combine(XeniaDir, "PARAMS", "XENIALOCATION")))
                File.Delete(Path.Combine(XeniaDir, "PARAMS", "XENIALOCATION"));
            File.WriteAllText(Path.Combine(XeniaDir, "PARAMS", "XENIALOCATION"), filename);
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.Title = "Open XBOX 360 Executable";
            OpenFileDialog1.Filter = "XEX File|*.xex";
            OpenFileDialog1.RestoreDirectory = true;
            string args;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(OpenFileDialog1.FileName, OpenFileDialog1.FileName + ".bak");
                args = "-c u -e u \"" + OpenFileDialog1.FileName + "\"";
                Process.Start(XEXTOOLPATH, args);
            }
        }

        private void FileStubTemplateXenia_Load(object sender, EventArgs e)
        {
            cbSelectedGame.SelectedIndex = 0;
            //LoadKnownGames();
        }

        private void btnGetSegments_Click(object sender, EventArgs e)
        {
        }

        internal bool SelectGame(string selected = null)
        {
            if (selected != null && selected != "None")
                currentXeniaSession = knownGamesDico[selected];

            

            string mainFullPath = currentXeniaSession.gameMainExePath;
            if (!File.Exists(mainFullPath))
            {
                string message = "File Stub couldn't find the \"default.xex\" file for this game. Would you like to remove this entry?";
                var result = MessageBox.Show(message, "Error finding game", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    UnmodGame();

                cbSelectedGame.SelectedIndex = 0;
                return false;
            }

            //if (!LoadRpxFileInterface())
            //    return false;



            //load target here




            //S.GET<StubForm>().lbCemuStatus.Text = "Ready for corrupting";
            //S.GET<StubForm>().lbTargetedGameRpx.Text = currentSession.gameRpxFileInfo.FullName;
            //S.GET<StubForm>().lbTargetedGameId.Text = "Game ID: " + currentSession.FirstID + "-" + currentSession.SecondID;
            //EnableInterface();

            return true;
        }

        private void UnmodGame()
        {
            var lastRef = currentXeniaSession;

            FileInterface.CompositeFilenameDico.Remove(lastRef.gameName);
            knownGamesDico.Remove(lastRef.gameName);
            SaveKnownGames();
            cbSelectedGame.SelectedIndex = 0;
            cbSelectedGame.Items.Remove(lastRef.gameName);
        }

        private void cbSelectedGame_SelectedIndexChanged(object sender, EventArgs e)
        {

            //var selected = cbSelectedGame.SelectedItem.ToString();

            //if (selected == "None")
            //    return;

            //if (!SelectGame(selected))
            //{
            //    cbSelectedGame.SelectedIndex = 0;
            //    return;
            //}

            //S.GET<StubForm>().btnLoadTargets_Click(null, null);

        }
    }
    public class XeniaTemplateSession
    {
        public FileInfo gameMainExeFileInfo = null;
        public FileInfo XeniaExeFile = null;
        public string XeniaExePath = null;
        public DirectoryInfo gameSaveFolder = null;
        public string mainExeFile = null;
        public string gameMainExePath = null;
        public string FirstID = null;
        public string SecondID = null;
        public string fileInterfaceTargetId = null;
        public string gameName = "Autodetect";
        public string mainUncompressedToken = null;
        internal FileMemoryInterface fileInterface;

        public override string ToString()
        {
            return gameName;
        }
    }
}
