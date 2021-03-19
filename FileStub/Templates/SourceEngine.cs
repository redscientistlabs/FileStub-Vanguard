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
    using System.Runtime.Remoting.Messaging;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.Common;
    using RTCV.CorruptCore;

    public partial class FileStubTemplaceSource : Form, IFileStubTemplate
    {
        
        const string SOURCESTUB_EXE_KNOWN_DLL = "Source Engine : EXE and known DLLs";
        //const string SOURCESTUB_EXE_ALL_DLL = "Source Engine : EXE and all DLLs"; //targeting all dlls for a source engine game is a bad idea
        const string SOURCESTUB_EXE = "Source Engine : Engine EXE";
        const string SOURCESTUB_SOURCEDLL = "Source Engine : engine.dll";
        string exePath = null;
        string gameFolerName = null;
        string currentSelectedTemplate = null;
        public string[] TemplateNames { get => new string[] {
            SOURCESTUB_EXE_KNOWN_DLL,
            //SOURCESTUB_EXE_ALL_DLL,
            SOURCESTUB_EXE,
            SOURCESTUB_SOURCEDLL,
        }; }

        public bool DisplayDragAndDrop => true;
        public bool DisplayBrowseTarget => true;


        public FileStubTemplaceSource()
        {
            InitializeComponent();
        }

        public FileTarget[] GetTargets()
        {
            string targetGame = lbGameTarget.Text;
            if(targetGame == "" || targetGame == " ")
            {
                MessageBox.Show("No target loaded");
                return null;
            }
            List<FileTarget> targets = new List<FileTarget>();

            var gameDirectoryInfo = new DirectoryInfo(targetGame);
            string targetExe = "";
            if (gameDirectoryInfo.Parent.GetFiles("hl2.exe").FirstOrDefault().Exists)
            {
                targetExe = gameDirectoryInfo.Parent.GetFiles("hl2.exe").FirstOrDefault().FullName;
            }
            else if (gameDirectoryInfo.Parent.GetFiles("bms.exe").FirstOrDefault().Exists)
            {
                targetExe = gameDirectoryInfo.Parent.GetFiles("bms.exe").FirstOrDefault().FullName;
            }
            else if (gameDirectoryInfo.Parent.GetFiles("portal2.exe").FirstOrDefault().Exists)
            {
                targetExe = gameDirectoryInfo.Parent.GetFiles("portal2.exe").FirstOrDefault().FullName;
            }
            exePath = targetExe;
            var exeFileInfo = new FileInfo(targetExe);
            var exeFolder = exeFileInfo.Directory.FullName;
            var GameFolder = gameDirectoryInfo.FullName;
            var baseFolder = exeFileInfo.Directory;
            var enginebinfoldername = Path.Combine(exeFolder, "bin");
            var enginebinfolder = new DirectoryInfo(enginebinfoldername);
            var gamebinfoldername = Path.Combine(GameFolder, "bin");
            var gamebinfolder = new DirectoryInfo(gamebinfoldername);

            //if (cbParentExeDir.Checked)
            //    baseFolder = baseFolder.Parent;

            List<FileInfo> allengineFiles = SelectMultipleForm.DirSearch(enginebinfolder);
            List<FileInfo> allgamebinFiles = SelectMultipleForm.DirSearch(gamebinfolder);

            string baseless(string path) => path.Replace(exeFolder, "");

            var exeTarget = Vault.RequestFileTarget(baseless(exeFileInfo.FullName), baseFolder.FullName);

            var allEngineDlls = allengineFiles.Where(it => it.Extension == ".dll");

            var allKnownEngineDlls = allEngineDlls.Where(it =>
                    it.Name.ToUpper().Contains("PHYS") ||
                    it.Name.ToUpper().Contains("STUDIORENDER.DLL") ||
                    it.Name.ToUpper().Contains("CLIENT.DLL") ||
                    it.Name.ToUpper().Contains("ENGINE.DLL")
                    ).ToArray();

            var allSourceEngine = allEngineDlls.Where(it =>
                    it.Name.ToUpper().Contains("ENGINE.DLL")
                    ).ToArray();

            var allGameDlls = allgamebinFiles.Where(it => it.Extension == ".dll");

            var allKnownGameDlls = allGameDlls.Where(it =>
                    it.Name.ToUpper().Contains("CLIENT.DLL")
                    ).ToArray();


            switch (currentSelectedTemplate)
            {
                case SOURCESTUB_EXE_KNOWN_DLL:
                    {
                        targets.Add(exeTarget);
                        targets.AddRange(allKnownEngineDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                        targets.AddRange(allKnownGameDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
                case SOURCESTUB_EXE:
                    {
                        targets.Add(exeTarget);
                    }
                    break;
                case SOURCESTUB_SOURCEDLL:
                    {
                        targets.AddRange(allSourceEngine.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
            }


            var sf = S.GET<StubForm>();
            FileWatch.currentSession.selectedExecution = ExecutionType.EXECUTE_OTHER_PROGRAM;
            Executor.otherProgram = targetExe;
            sf.tbArgs.Text = $"-game {gameFolerName} -insecure";
            return targets.ToArray();
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
$@"== Corrupt Source Engine ==
Click on Browse Target and select the gameinfo.txt of the game you want to corrupt, or drag and drop it into the box.
Don't be stupid. Don't corrupt online games like TF2 or CounterStrike.
";
        }

        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if (fd.Length > 1 || fd[0].EndsWith("\\") || !fd[0].ToUpper().EndsWith(".TXT"))
            {
                MessageBox.Show("Please only drop the game's gameinfo.txt");
                lbGameTarget.Text = "";
                return false;
            }

            string filename = fd[0];
            var gameinfofileinfo = new FileInfo(filename);
            lbGameTarget.Text = gameinfofileinfo.Directory.FullName;
            gameFolerName = gameinfofileinfo.Directory.Name;
            return true;
        }

        public void BrowseFiles()
        {
            string filename;

            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.Title = "Open Game's gameinfo.txt";
            OpenFileDialog1.Filter = "gameinfo|gameinfo.txt";
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FileName.ToString().Contains('^'))
                {
                    MessageBox.Show("You can't use a file that contains the character ^ ");
                    lbGameTarget.Text = "";
                    return;
                }

                filename = OpenFileDialog1.FileName;
            }
            else
            {
                lbGameTarget.Text = "";
                return;
            }

            var gameinfofileinfo = new FileInfo(filename);
            lbGameTarget.Text = gameinfofileinfo.Directory.FullName;
            gameFolerName = gameinfofileinfo.Directory.Name;
        }

        public void GetSegments(FileInterface exeInterface)
        {
            throw new NotImplementedException();
        }
    }
}
