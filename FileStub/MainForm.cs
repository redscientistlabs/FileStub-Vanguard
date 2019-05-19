using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;

namespace FileStub
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            SyncObjectSingleton.SyncObject = this;


        Text += FileWatch.FileStubVersion;

            this.cbSelectedExecution.Items.AddRange(new object[] {
                ExecutionType.NO_EXECUTION,
                ExecutionType.EXECUTE_CORRUPTED_FILE,
                ExecutionType.EXECUTE_WITH,
                ExecutionType.EXECUTE_OTHER_PROGRAM,
                ExecutionType.SCRIPT,
            });

            this.cbTargetType.Items.AddRange(new object[] {
                TargetType.SINGLE_FILE,
                TargetType.MULTIPLE_FILE_SINGLEDOMAIN,
                TargetType.MULTIPLE_FILE_MULTIDOMAIN,
                TargetType.MULTIPLE_FILE_MULTIDOMAIN_FULLPATH,
            });

        }

        private void BtnRestartStub_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        private void CS_Core_Form_Load(object sender, EventArgs e)
        {
            cbSelectedExecution.SelectedIndex = 0;
            cbTargetType.SelectedIndex = 0;
        }

        public void RunProgressBar(string progressLabel, int maxProgress, Action<object, EventArgs> action, Action<object, EventArgs> postAction = null)
        {

            if (FileWatch.progressForm != null)
            {
                FileWatch.progressForm.Close();
                this.Controls.Remove(FileWatch.progressForm);
                FileWatch.progressForm = null;
            }

            FileWatch.progressForm = new ProgressForm(progressLabel, maxProgress, action, postAction);
            FileWatch.progressForm.Run();

        }



        private void CbSelectedExecution_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cbSelectedExecution.SelectedItem.ToString())
            string selected = cbSelectedExecution.SelectedItem.ToString();

            FileWatch.currentFileInfo.selectedExecution = selected;

            switch (selected)
            {
                case ExecutionType.EXECUTE_CORRUPTED_FILE:
                case ExecutionType.EXECUTE_WITH:
                case ExecutionType.EXECUTE_OTHER_PROGRAM:
                    lbArgs.Visible = true;
                    tbArgs.Visible = true;
                    btnKillProcess.Visible = true;
                    btnEditExec.Visible = true;
                    btnEditExec.Text = "Edit Exec";
                    break;

                    
                case ExecutionType.NO_EXECUTION:
                    lbArgs.Visible = false;
                    tbArgs.Visible = false;
                    btnKillProcess.Visible = false;
                    btnEditExec.Visible = false;
                    btnEditExec.Text = "Edit Exec";
                    break;

                case ExecutionType.SCRIPT:
                default:
                    lbArgs.Visible = false;
                    tbArgs.Visible = false;
                    btnKillProcess.Visible = true;
                    btnEditExec.Visible = true;
                    btnEditExec.Text = "Edit Script";
                    break;
            }

            Executor.RefreshLabel();

        }

        private void BtnEditExec_Click(object sender, EventArgs e)
        {
            Executor.EditExec();
        }


        Size originalLbTargetSize;
        Point originalLbTargetLocation;
        public void EnableInterface()
        {
            var diff = lbTarget.Location.X - btnBrowseTarget.Location.X;
            originalLbTargetLocation = lbTarget.Location;
            lbTarget.Location = btnBrowseTarget.Location;
            lbTarget.Visible = true;

            btnBrowseTarget.Visible = false;
            originalLbTargetSize = lbTarget.Size;
            lbTarget.Size = new Size(lbTarget.Size.Width + diff, lbTarget.Size.Height);
            btnUnloadTarget.Visible = true;
            cbTargetType.Enabled = false;

            lbTargetExecution.Enabled = true;
            pnTargetExecution.Enabled = true;
            btnExecutionSettings.Visible = true;

            btnRestoreBackup.Enabled = true;
            btnResetBackup.Enabled = true;
            btnClearAllBackups.Enabled = true;

            lbTargetStatus.Text = FileWatch.currentFileInfo.selectedTargetType.ToString() + " target loaded";
        }

        public void DisableInterface()
        {
            btnUnloadTarget.Visible = false;
            btnBrowseTarget.Visible = true;
            lbTarget.Size = originalLbTargetSize;
            lbTarget.Location = originalLbTargetLocation;
            lbTarget.Visible = false;
            cbTargetType.Enabled = true;

            cbSelectedExecution.SelectedIndex = 0;
            lbTargetExecution.Enabled = false;
            pnTargetExecution.Enabled = false;
            btnExecutionSettings.Visible = false;

            btnRestoreBackup.Enabled = false;
            btnResetBackup.Enabled = false;
            btnClearAllBackups.Enabled = false;
            lbTarget.Text = "No target selected";
            lbTargetStatus.Text = "No target selected";
        }

        private void BtnBrowseTarget_Click(object sender, EventArgs e)
        {
            if (!FileWatch.LoadTarget())
                return;

            if (!VanguardCore.vanguardConnected)
                VanguardCore.Start();

            EnableInterface();

        }

        private void BtnReleaseTarget_Click(object sender, EventArgs e)
        {
            FileWatch.CloseTarget();
            DisableInterface();
        }

        private void CbTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cbSelectedExecution.SelectedItem.ToString())
            FileWatch.currentFileInfo.selectedTargetType = cbTargetType.SelectedItem.ToString();

        }

        private void BtnKillProcess_Click(object sender, EventArgs e)
        {
            FileWatch.KillProcess();
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            FileWatch.currentFileInfo.targetInterface?.RestoreBackup();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
@"This resets the backup of the current target by using the current data from it.
If you override a clean backup using a corrupted file,
you won't be able to restore the original file using it.

Are you sure you want to reset the current target's backup?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FileWatch.currentFileInfo.targetInterface?.ResetBackup(true);

        }

        private void BtnClearAllBackups_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear ALL THE BACKUPS\n from FileStub's cache?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            FileWatch.currentFileInfo.targetInterface?.RestoreBackup();

            foreach (string file in Directory.GetFiles(FileWatch.currentDir + "\\TEMP"))
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

            FileInterface.CompositeFilenameDico = new Dictionary<string, string>();
            FileWatch.currentFileInfo.targetInterface?.ResetBackup(false);
            FileInterface.SaveCompositeFilenameDico();
            MessageBox.Show("All the backups were cleared.");
        }
    }
}
