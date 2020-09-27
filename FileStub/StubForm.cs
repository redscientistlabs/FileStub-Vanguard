namespace FileStub
{
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
    using RTCV.CorruptCore;
    using RTCV.NetCore;
    using RTCV.UI;
    using Vanguard;

    public partial class StubForm : Form
    {
        int originalWidth;
        public StubForm()
        {
            InitializeComponent();

            originalWidth = this.Width;

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

        public void ShrinkStubForm()
        {
            int remainder = this.Width - (btnTargetSettings.Location.X + btnTargetSettings.Width);
            int rightsideTargetType = pnTargetType.Location.X + pnTargetType.Width;
            originalWidth = this.Width;
            this.Width = rightsideTargetType + remainder;
        }

        public void ExtendStubForm()
        {
            this.Width = originalWidth;
        }

        private void StubForm_Load(object sender, EventArgs e)
        {
            ShrinkStubForm();

            cbSelectedExecution.SelectedIndex = 0;
            cbTargetType.SelectedIndex = 0;

            btnUnloadTarget.Location = btnLoadTargets.Location;
            btnUnloadTarget.Size = btnLoadTargets.Size;

            //uh oh magic numbers
            //this will break if the combobox font isnt Segoe UI, 12pt
            int magicWidth = cbTargetType.Size.Width - (411 - 387);
            int magicHeight = cbTargetType.Size.Width - (36 - 29);
            int magicX = cbTargetType.Location.X - (14 - 11);
            int magicY = cbTargetType.Location.Y - (14 - 11);

            lbTargetTypeDisplay.Font = cbTargetType.Font;
            lbTargetTypeDisplay.Size = new Size(magicX, magicY);
            lbTargetTypeDisplay.Location = new Point(magicWidth, magicHeight);
            //---------------------------------------------------------------

            Colors.SetRTCColor(Color.Plum, this);

            FileWatch.Start();
        }

        internal void RunProgressBar(string progressLabel, int maxProgress, Action<object, EventArgs> action, Action<object, EventArgs> postAction = null)
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
            string selected = cbSelectedExecution.SelectedItem.ToString();

            FileWatch.currentSession.selectedExecution = selected;

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

        public void EnableTargetInterface()
        {
            btnLoadTargets.Visible = false;

            btnUnloadTarget.Visible = true;
            cbTargetType.Visible = false;

            FileWatch.EnableInterface();

            lbExecution.Visible = true;

            lbTargetStatus.Text = FileWatch.currentSession.selectedTargetType.ToString() + " target loaded";
        }

        public void DisableTargetInterface()
        {
            btnUnloadTarget.Visible = false;
            btnLoadTargets.Visible = true;
            //lbTarget.Size = originalLbTargetSize;
            //lbTarget.Location = originalLbTargetLocation;
            cbTargetType.Visible = true;

            cbSelectedExecution.SelectedIndex = 0;

            btnRestoreBackup.Enabled = false;
            btnResetBackup.Enabled = false;
            btnClearAllBackups.Enabled = false;
            lbTarget.Text = "No target selected";
            lbTargetStatus.Text = "No target selected";
        }

        private void BtnBrowseTarget_Click(object sender, EventArgs e)
        {
            FileWatch.InsertTargets();
        }

        private void BtnUnloadTarget_Click(object sender, EventArgs e)
        {
            if (!FileWatch.CloseActiveTargets())
                return;
            DisableTargetInterface();
        }

        private void CbTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cbSelectedExecution.SelectedItem.ToString())
            FileWatch.currentSession.selectedTargetType = cbTargetType.SelectedItem.ToString();
            lbTargetTypeDisplay.Text = FileWatch.currentSession.selectedTargetType;
        }

        private void BtnKillProcess_Click(object sender, EventArgs e)
        {
            FileWatch.KillProcess();
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            FileWatch.currentSession.targetInterface?.CloseStream();
            FileWatch.currentSession.targetInterface?.RestoreBackup();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
@"This resets the backup of the current target by using the current data from it.
If you override a clean backup using a corrupted file,
you won't be able to restore the original file using it.

Are you sure you want to reset the current target's backup?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FileWatch.currentSession.targetInterface?.ResetBackup(true);
        }

        private void BtnClearAllBackups_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear ALL THE BACKUPS\n from FileStub's cache?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FileWatch.currentSession.targetInterface?.RestoreBackup();

            foreach (string file in Directory.GetFiles(Path.Combine(FileWatch.currentDir, "FILEBACKUPS")))
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
            FileWatch.currentSession.targetInterface?.ResetBackup(false);
            FileInterface.SaveCompositeFilenameDico();
            MessageBox.Show("All the backups were cleared.");
        }

        private void BtnTargetSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control c = (Control)sender;
                Point locate = new Point(c.Location.X + e.Location.X, ((Control)sender).Location.Y + e.Location.Y);

                ContextMenuStrip columnsMenu = new ContextMenuStrip();

                ((ToolStripMenuItem)columnsMenu.Items.Add("Big endian", null, new EventHandler((ob, ev) => {
                    FileWatch.currentSession.bigEndian = !FileStub.FileWatch.currentSession.bigEndian;

                    if (VanguardCore.vanguardConnected)
                        FileWatch.UpdateDomains();
                }))).Checked = FileWatch.currentSession.bigEndian;

                ((ToolStripMenuItem)columnsMenu.Items.Add("Auto-Uncorrupt", null, new EventHandler((ob, ev) => {
                    FileWatch.currentSession.autoUncorrupt = !FileWatch.currentSession.autoUncorrupt;
                }))).Checked = FileWatch.currentSession.autoUncorrupt;

                ((ToolStripMenuItem)columnsMenu.Items.Add("Use Caching + Multithreading", null, new EventHandler((ob, ev) => {
                    FileWatch.currentSession.useCacheAndMultithread = !FileWatch.currentSession.useCacheAndMultithread;
                }))).Checked = FileWatch.currentSession.useCacheAndMultithread;

                columnsMenu.Show(this, locate);
            }
        }

        private void StubForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FileWatch.CloseActiveTargets(false))
                e.Cancel = true;
        }

        private void btnLoadTargets_Click(object sender, EventArgs e)
        {
            if (!FileWatch.LoadTargets())
                return;

            if (!VanguardCore.vanguardConnected)
                VanguardCore.Start();

            EnableTargetInterface();
        }

        private void btnClearTargets_Click(object sender, EventArgs e)
        {
            lbTargets.Items.Clear();
        }

        private void lbTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTargets.SelectedIndex == -1)
                return;

            var target = (FileTarget)lbTargets.SelectedItem;

            nmHeaderPadding.Value = target.PaddingHeader;
            nmFooterPadding.Value = target.PaddingFooter;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lbTargets.SelectedIndex == -1)
                return;

            var target = (FileTarget)lbTargets.SelectedItem;

            target.PaddingHeader = Convert.ToInt64(nmHeaderPadding.Value);
            target.PaddingFooter = Convert.ToInt64(nmFooterPadding.Value);
        }

        private void lbDragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            lbTargets.Items.Clear();
            lbTargets_DragDrop(sender, e);

            if (btnUnloadTarget.Visible)
                BtnUnloadTarget_Click(sender, e);

            btnLoadTargets_Click(sender, e);
        }

        private void lbDragAndDrop_DragEnter(object sender, DragEventArgs e) => lbTargets_DragEnter(sender, e);

        private void lbTargets_DragDrop(object sender, DragEventArgs e)
        {
            var formats = e.Data.GetFormats();
            e.Effect = DragDropEffects.Move;

            string[] fd = (string[])e.Data.GetData(DataFormats.FileDrop); //file drop

            foreach (var file in fd)
            {
                if (Directory.Exists(file))
                {
                    var files = SelectMultipleForm.DirSearch(file);

                    var targets = files.Select(it => Vault.RequestFileTarget(it));

                    if (targets.Count() > 1 && FileWatch.currentSession.selectedTargetType == TargetType.SINGLE_FILE)
                        cbTargetType.SelectedItem = cbTargetType.Items.Cast<object>().FirstOrDefault(iterator => iterator.ToString() == TargetType.MULTIPLE_FILE_MULTIDOMAIN);

                    lbTargets.Items.AddRange(targets.ToArray());
                }
                else
                {
                    var targets = fd.Select(it => Vault.RequestFileTarget(it));

                    if (targets.Count() > 1 && FileWatch.currentSession.selectedTargetType == TargetType.SINGLE_FILE)
                        cbTargetType.SelectedItem = cbTargetType.Items.Cast<object>().FirstOrDefault(iterator => iterator.ToString() == TargetType.MULTIPLE_FILE_MULTIDOMAIN);

                    lbTargets.Items.AddRange(targets.ToArray());
                }
            }
        }

        private void lbTargets_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void btnExtendPanel_Click(object sender, EventArgs e)
        {
            if (btnExtendPanel.Text == "Advanced Options")
            {
                btnExtendPanel.Text = "Basic Options";
                ExtendStubForm();
            }
            else
            {
                btnExtendPanel.Text = "Advanced Options";
                ShrinkStubForm();
            }
        }

        private void btnSetBaseDir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Doesn't do anything right now");
        }
    }
}
