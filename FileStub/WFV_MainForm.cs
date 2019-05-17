using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileStub
{
    public partial class WFV_MainForm : Form
    {

        public bool DontLoadSelectedStash = false;
        public bool DontLoadSelectedStockpile = false;

        public WFV_MainForm()
        {
            InitializeComponent();

            WFV_Core.Start(this);
        }

        public void RunProgressBar(string progressLabel, int maxProgress, Action<object, EventArgs> action, Action<object, EventArgs> postAction = null)
        {

            if (WFV_Core.progressForm != null)
            {
                WFV_Core.progressForm.Close();
                this.Controls.Remove(WFV_Core.progressForm);
                WFV_Core.progressForm = null;
            }

            WFV_Core.progressForm = new ProgressForm( progressLabel, maxProgress, action, postAction);
            WFV_Core.progressForm.Run();

        }

        private void TerminateIfNeeded()
        {
            if (rbExecuteOtherProgram.Checked || rbExecuteWith.Checked || rbExecuteCorruptedFile.Checked)
                if (WFV_Core.currentTargetType == "File" && cbTerminateOnReExec.Checked && Executor.otherProgram != null)
                {
                    string otherProgramShortFilename = Executor.otherProgram.Substring(Executor.otherProgram.LastIndexOf(@"\") + 1);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "taskkill";
                    startInfo.Arguments = $"/IM \"{otherProgramShortFilename}\"";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    Process processTemp = new Process();
                    processTemp.StartInfo = startInfo;
                    processTemp.EnableRaisingEvents = true;
                    try
                    {
                        processTemp.Start();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Thread.Sleep(300);
                }
        }

        private void btnRestoreFileBackup_Click(object sender, EventArgs e)
        {
            if (WFV_Core.currentMemoryInterface != null)
                WFV_Core.currentMemoryInterface.RestoreBackup(true);

        }

        private void WFV_MainForm_Load(object sender, EventArgs e)
        {

            this.Text = "Vanguard for Windows Files " + WFV_Core.WghVersion;

        }

        private void btnResetBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
@"This resets the backup of the current target by using the current data from it.
If you override a clean backup using a corrupted file,
you won't be able to restore the original file using it.

Are you sure you want to reset the current target's backup?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (WFV_Core.currentMemoryInterface != null)
                WFV_Core.currentMemoryInterface.ResetBackup(true);

        }

        private void btnClearAllBackups_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear ALL THE BACKUPS\n from the Glitch Harvester's cache?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            if (WFV_Core.currentMemoryInterface != null && WFV_Core.currentTargetType == "File")
                WFV_Core.currentMemoryInterface.RestoreBackup();

            foreach (string file in Directory.GetFiles(WFV_Core.currentDir + "\\TEMP"))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    MessageBox.Show($"Could not delete file {file}");
                }
            }

            WFV_Core.CompositeFilenameDico = new Dictionary<string, string>();
            if (WFV_Core.currentMemoryInterface != null && (WFV_Core.currentTargetType == "File" || WFV_Core.currentTargetType == "MultipleFiles"))
                WFV_Core.currentMemoryInterface.ResetBackup(false);
            WFV_Core.SaveCompositeFilenameDico();
            MessageBox.Show("All the backups were cleared.");
        }

        private void rbTargetFile_CheckedChanged(object sender, EventArgs e)
        {
            btnResetBackup.Enabled = true;
            btnRestoreFileBackup.Enabled = true;
            cbWriteCopyMode.Enabled = true;

            rbExecuteCorruptedFile.Enabled = true;
            rbExecuteWith.Enabled = true;
            rbExecuteOtherProgram.Enabled = true;
        }

        private void rbTargetMultipleFiles_CheckedChanged(object sender, EventArgs e)
        {
            btnResetBackup.Enabled = true;
            btnRestoreFileBackup.Enabled = true;
            cbWriteCopyMode.Enabled = true;

            if (rbExecuteCorruptedFile.Checked)
                rbNoExecution.Checked = true;
            rbExecuteCorruptedFile.Enabled = false;

            if (rbExecuteWith.Checked)
                rbNoExecution.Checked = true;

            rbExecuteWith.Enabled = false;
            rbExecuteOtherProgram.Enabled = false;
        }

        private void WFV_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WFV_Core.currentMemoryInterface != null)
                WFV_Core.RestoreTarget();
        }

        private void cbWriteCopyMode_CheckedChanged(object sender, EventArgs e)
        {
            WFV_Core.writeCopyMode = cbWriteCopyMode.Checked;
        }

        private void btnEditExec_Click(object sender, EventArgs e)
        {
            Executor.EditExec();
        }

        private void rbNoExecution_CheckedChanged(object sender, EventArgs e)
        {
            Executor.RefreshLabel();
        }

        private void rbExecuteCorruptedFile_CheckedChanged(object sender, EventArgs e)
        {
            Executor.RefreshLabel();
        }

        private void rbExecuteOtherProgram_CheckedChanged(object sender, EventArgs e)
        {
            Executor.RefreshLabel();
        }

        private void rbExecuteScript_CheckedChanged(object sender, EventArgs e)
        {
            Executor.RefreshLabel();
        }

        public DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public void btnEnableCaching_Click(object sender, EventArgs e)
        {
            if (WFV_Core.currentMemoryInterface != null)
                if (btnEnableCaching.Text == "Enable Cache + Multithread")
                {
                    WFV_Core.currentMemoryInterface.getMemoryDump();

                    /*
                    if (WFV_Core.currentMemoryInterface is ProcessInterface)
                        (WFV_Core.currentMemoryInterface as ProcessInterface).UseCaching = true;
                    */

                    btnEnableCaching.Text = "Disable Cache + Multithread";
                }
                else
                {
                    WFV_Core.currentMemoryInterface.wipeMemoryDump();

                    /*
                    if (WFV_Core.currentMemoryInterface is ProcessInterface)
                        (WFV_Core.currentMemoryInterface as ProcessInterface).UseCaching = false;
                    */

                    btnEnableCaching.Text = "Enable Cache + Multithread";
                }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void btnKillProcess_Click(object sender, EventArgs e)
        {
            TerminateIfNeeded();
        }

    }
}
