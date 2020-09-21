namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using Vanguard;
    using static RTCV.CorruptCore.FileInterface;

    public partial class SelectMultipleForm : Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Dispose method is located in the designer file")]
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public SelectMultipleForm()
        {
            InitializeComponent();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (string file in openFileDialog1.FileNames)
                {
                    lbMultipleFiles.Items.Add(file);
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            //thx http://stackoverflow.com/questions/11624298/how-to-use-openfiledialog-to-select-a-folder-how-to-reuse-rc-file-from-mfc-in

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            List<string> files = DirSearch(fbd.SelectedPath);

            lbMultipleFiles.Items.AddRange(files.ToArray());
        }

        public static List<string> DirSearch(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }

        private void WFV_SelectMultiple_Load(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();
        }

        private void InitializeOpenFileDialog()
        {
            //thanks: http://stackoverflow.com/questions/1311578/opening-multiple-files-openfiledialog-c

            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "All files (*.*)|*.*";

            //  Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
            //                   ^  ^  ^  ^  ^  ^  ^

            this.openFileDialog1.Title = "Add File(s)";
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lbMultipleFiles.SelectedIndex != -1)
                lbMultipleFiles.Items.RemoveAt(lbMultipleFiles.SelectedIndex);
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lbMultipleFiles.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lbMultipleFiles.Items.Clear();
            this.Close();
        }

        private void btnSendList_Click(object sender, EventArgs e)
        {
            if (lbMultipleFiles.Items.Count == 0)
            {
                MessageBox.Show("No files are selected");
                return;
            }

            //FileWatch.CloseTarget(false);

            List<string> allFiles = new List<string>();

            for (int i = 0; i < lbMultipleFiles.Items.Count; i++)
                allFiles.Add(lbMultipleFiles.Items[i].ToString());

            allFiles.Sort();

            /*
            string multipleFiles = "";

            for (int i = 0; i < allFiles.Count; i++)
            {
                multipleFiles += allFiles[i];

                if (i < allFiles.Count - 1)
                    multipleFiles += "|";
            }
            */

            var targets = allFiles.Select(it => new TargetLoader(it, false));

            S.GET<StubForm>().lbTargets.Items.AddRange(targets.ToArray());

            /*

            var mfi = new MultipleFileInterface(multipleFiles, FileWatch.currentFileInfo.bigEndian, FileWatch.currentFileInfo.useAutomaticBackups);

            if (FileWatch.currentFileInfo.useCacheAndMultithread)
                mfi.getMemoryDump();

            FileWatch.currentFileInfo.targetInterface = mfi;

            if (VanguardCore.vanguardConnected)
                FileWatch.UpdateDomains();

            */

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void WFV_SelectMultiple_FormClosing(object sender, FormClosingEventArgs e)
        {
            lbMultipleFiles.Items.Clear();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1;
            OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.DefaultExt = "txt";
            OpenFileDialog1.Title = "Open File list";
            OpenFileDialog1.Filter = "TXT files|*.txt";
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] files = File.ReadAllLines(OpenFileDialog1.FileName);
                    lbMultipleFiles.Items.Clear();
                    lbMultipleFiles.Items.AddRange(files);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong while loading file list \n\n" + ex.ToString());
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Title = "Save File list";
            saveFileDialog1.Filter = "TXT files|*.txt";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<string> allLines = new List<string>();

                    foreach (var item in lbMultipleFiles.Items)
                        allLines.Add(item.ToString());

                    File.WriteAllLines(saveFileDialog1.FileName, allLines.ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong while saving file list \n\n" + ex.ToString());
                }
            }
        }

        private void CbLoadAnything_CheckedChanged(object sender, EventArgs e)
        {
            MultipleFileInterface.LoadAnything = (sender as CheckBox).Checked;
        }
    }
}
