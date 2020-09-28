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
        public static Color ProgramColor = Color.Plum;
        int originalWidth;
        Dictionary<string, IFileStubTemplate> templateDico = new Dictionary<string, IFileStubTemplate>();

        IFileStubTemplate selectedTemplate = null;
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

            var templates = GetAssemblyTemplates();
            foreach (var template in templates)
            {
                foreach (var name in template.TemplateNames)
                    templateDico[name] = template;

                this.cbTargetType.Items.AddRange(template.TemplateNames);
            }

        }

        private IFileStubTemplate[] GetAssemblyTemplates()
        {
            var type = typeof(IFileStubTemplate);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            return types
                .Where(it => it != typeof(IFileStubTemplate))
                .Select(it => (IFileStubTemplate)Activator.CreateInstance(it))
                .ToArray();
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
            //lbTargetTypeDisplay.Size = new Size(magicX, magicY);
            //lbTargetTypeDisplay.Location = new Point(magicWidth, magicHeight);
            lbTargetTypeDisplay.Visible = true;
            //---------------------------------------------------------------

            Colors.SetRTCColor(ProgramColor, this);

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
            cbTargetType.Visible = true;

            cbSelectedExecution.SelectedIndex = 0;

            btnRestoreTargets.Enabled = false;
            btnResetBackups.Enabled = false;
            btnClearVaultData.Enabled = false;
            lbTarget.Text = "No target selected";
            lbTargetStatus.Text = "No target selected";
        }

        private void BtnBrowseTarget_Click(object sender, EventArgs e)
        {
            if(selectedTemplate != null)
            {
                selectedTemplate.BrowseFiles();
            }
            else
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
            FileWatch.currentSession.selectedTargetType = cbTargetType.SelectedItem.ToString();
            lbTargetTypeDisplay.Text = FileWatch.currentSession.selectedTargetType;
            lbTargets.Items.Clear();

            if(templateDico.TryGetValue(FileWatch.currentSession.selectedTargetType, out IFileStubTemplate template))
            {
                if(selectedTemplate != null)
                {
                    var st = (selectedTemplate as Form);
                    if (st.Parent != null)
                        st.Parent.Controls.Remove(st);
                }

                var tf = template.GetTemplateForm(FileWatch.currentSession.selectedTargetType);
                tf.TopLevel = false;
                Colors.SetRTCColor(ProgramColor, tf);
                pnFileLoading.Controls.Add(tf);
                tf.BringToFront();
                tf.Show();

                selectedTemplate = template;
                btnSetBaseDir.Visible = false;

                if (btnExtendPanel.Text != "Advanced Options")
                    btnExtendPanel_Click(null, null);

                btnExtendPanel.Visible = false;
            }
            else
            {
                if (selectedTemplate != null)
                {
                    var st = (selectedTemplate as Form);
                    if(st.Parent != null)
                        st.Parent.Controls.Remove(st);
                }
                selectedTemplate = null;
                btnSetBaseDir.Visible = true;

                btnExtendPanel.Visible = true;
            }
        }

        private void BtnKillProcess_Click(object sender, EventArgs e)
        {
            FileWatch.KillProcess();
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            FileWatch.currentSession.fileInterface?.CloseStream();
            FileWatch.currentSession.fileInterface?.SendBackupToReal();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
@"This resets the backup of the current target by using the current data from it.
If you override a clean backup using a corrupted file,
you won't be able to restore the original file using it.

Are you sure you want to reset the current target's backup?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FileWatch.currentSession.fileInterface?.SendRealToBackup(true);
        }

        private void BtnClearAllBackups_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the vault?\n This will delete any remaining artifacts.", "WARNING", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (btnUnloadTarget.Visible)
                BtnUnloadTarget_Click(sender, e);

            FileWatch.CloseActiveTargets();

            Vault.ResetVault();

            lbTargets.Items.Clear();

            MessageBox.Show("The vault was reset.");
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
            FileWatch.CloseActiveTargets(false);

            int nbDirtyFiles = Vault.GetDirtyTargets().Count;
            if (nbDirtyFiles > 0)
            {
                var answer = MessageBox.Show($"There are still {nbDirtyFiles} dirty files, would you like to restore them?", "Warning: Quitting FileStub with Dirty Files", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                switch (answer)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                    case DialogResult.Yes:
                        FileWatch.RestoreDirty();
                        return;
                    case DialogResult.No:
                    default:
                        return;
                }
            }
        }

        private void btnLoadTargets_Click(object sender, EventArgs e)
        {
            FileTarget[] overrideTargets = null;
            if(selectedTemplate != null)
            {
                lbTargets.Items.Clear();
                overrideTargets = selectedTemplate.GetTargets();
                //if (targets != null)
                //    lbTargets.Items.AddRange(targets);

                //overrideTargets.
            }

            if (!FileWatch.LoadTargets(overrideTargets))
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

            lbBaseDir.Text = (string.IsNullOrWhiteSpace(target.BaseDir) ? "(unset)" : target.BaseDir);
        }

        private void btnSaveTargetPadding_Click(object sender, EventArgs e)
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

            if (!targetDragDrop(sender, e))
                return;

            if (btnUnloadTarget.Visible)
                BtnUnloadTarget_Click(sender, e);

            btnLoadTargets_Click(sender, e);
        }

        private void lbDragAndDrop_DragEnter(object sender, DragEventArgs e) => lbTargets_DragEnter(sender, e);

        private void lbTargets_DragDrop(object sender, DragEventArgs e) => targetDragDrop(sender, e);
        bool targetDragDrop(object sender, DragEventArgs e)
        {
            var formats = e.Data.GetFormats();
            e.Effect = DragDropEffects.Move;

            string[] fd = (string[])e.Data.GetData(DataFormats.FileDrop); //file drop
            if (selectedTemplate != null)
            {
                return selectedTemplate.DragDrop(fd);

            }
            else
            {
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

                return true;
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
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if(result == DialogResult.OK)
            {
                var baseDir = fbd.SelectedPath;

                FileTarget invalid = lbTargets.Items.Cast<FileTarget>()
                    .FirstOrDefault(it => string.IsNullOrWhiteSpace(it.BaseDir) && !it.FilePath.Contains(baseDir));

                if(invalid != null)
                {
                    MessageBox.Show("Could not match all files with basedir");
                    return;
                }

                foreach (FileTarget target in lbTargets.Items)
                {
                    target.SetBaseDir(baseDir);
                }
            }



        }

        private void btnRestoreDirty_Click(object sender, EventArgs e)
        {
            FileWatch.RestoreDirty();
        }

        private void btnBakeAllDirty_Click(object sender, EventArgs e)
        {
            FileWatch.BakeDirty();
        }
    }
}
