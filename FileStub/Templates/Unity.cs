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

    public partial class FileStubTemplateUnity : Form, IFileStubTemplate
    {
        
        const string UNITYSTUB_EXE_KNOWN_DLL = "Unity Engine : EXE and known DLLs";
        const string UNITYSTUB_EXE_ALL_DLL = "Unity Engine : EXE and all DLLs";
        const string UNITYSTUB_EXE = "Unity Engine : Main EXE";
        const string UNITYSTUB_UNITYDLL = "Unity Engine : UnityEngine.dll";

        string currentSelectedTemplate = null;
        public string[] TemplateNames { get => new string[] {
            UNITYSTUB_EXE_KNOWN_DLL,
            UNITYSTUB_EXE_ALL_DLL,
            UNITYSTUB_EXE,
            UNITYSTUB_UNITYDLL,
        }; }

        public FileStubTemplateUnity()
        {
            InitializeComponent();
        }

        public FileTarget[] GetTargets()
        {
            string targetExe = lbExeTarget.Text;

            if(targetExe == "")
            {
                MessageBox.Show("No target loaded");
                return null;
            }

            List<FileTarget> targets = new List<FileTarget>();

            var exeFileInfo = new FileInfo(targetExe);
            var exeFolder = exeFileInfo.Directory.FullName;

            var baseFolder = exeFileInfo.Directory;

            if(cbParentExeDir.Checked)
                baseFolder = baseFolder.Parent;

            List<FileInfo> allFiles = SelectMultipleForm.DirSearch(baseFolder);

            string baseless(string path) => path.Replace(exeFolder, "");

            var exeTarget = Vault.RequestFileTarget(baseless(exeFileInfo.FullName), baseFolder.FullName);

            var allDlls = allFiles.Where(it => it.Extension == ".dll");

            var allKnownDlls = allDlls.Where(it =>
                    it.Name.ToUpper().Contains("PHYSICS") ||
                    it.Name.ToUpper().Contains("CLOTH") ||
                    it.Name.ToUpper().Contains("ANIMATION") ||
                    it.Name.ToUpper().Contains("PARTICLE") ||
                    it.Name.ToUpper().Contains("TERRAIN") ||
                    it.Name.ToUpper().Contains("VEHICLES") ||
                    it.Name.ToUpper().Contains("UNITYENGINE.DLL")
                    ).ToArray();

            var allUnityEngine = allDlls.Where(it =>
                    it.Name.ToUpper().Contains("UNITYENGINE.DLL")
                    ).ToArray();


            switch (currentSelectedTemplate)
            {
                case UNITYSTUB_EXE_KNOWN_DLL:
                    {
                        targets.Add(exeTarget);
                        targets.AddRange(allDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
                case UNITYSTUB_EXE_ALL_DLL:
                    {
                        targets.Add(exeTarget);
                        targets.AddRange(allKnownDlls.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
                case UNITYSTUB_EXE:
                    {
                        targets.Add(exeTarget);
                    }
                    break;
                case UNITYSTUB_UNITYDLL:
                    {
                        targets.AddRange(allUnityEngine.Select(it => Vault.RequestFileTarget(baseless(it.FullName), baseFolder.FullName)));
                    }
                    break;
            }

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
$@"Unity Engine Template
{name}

Requires: Unity Game EXE file
";
        }

        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if(fd.Length > 1 || fd[0].EndsWith("\\") || !fd[0].ToUpper().EndsWith(".EXE"))
            {
                MessageBox.Show("Please only drop the game's main EXE");
                lbExeTarget.Text = "";
                return false;
            }

            lbExeTarget.Text = fd[0];
            return true;
        }

        public void BrowseFiles()
        {
            string filename;

            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.Title = "Open Exe File";
            OpenFileDialog1.Filter = "files|*.exe";
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FileName.ToString().Contains('^'))
                {
                    MessageBox.Show("You can't use a file that contains the character ^ ");
                    lbExeTarget.Text = "";
                    return;
                }

                filename = OpenFileDialog1.FileName;
            }
            else
            {
                lbExeTarget.Text = "";
                return;
            }

            lbExeTarget.Text = filename;
        }
    }
}
