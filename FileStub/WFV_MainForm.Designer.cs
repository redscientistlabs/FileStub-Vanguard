namespace FileStub
{
    public partial class WFV_MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFV_MainForm));
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.lbTarget = new System.Windows.Forms.Label();
            this.btnRestoreFileBackup = new System.Windows.Forms.Button();
            this.rbTargetFile = new System.Windows.Forms.RadioButton();
            this.pnTargetPanel = new System.Windows.Forms.Panel();
            this.rbTargetMultipleFiles = new System.Windows.Forms.RadioButton();
            this.btnClearAllBackups = new System.Windows.Forms.Button();
            this.btnResetBackup = new System.Windows.Forms.Button();
            this.cbWriteCopyMode = new System.Windows.Forms.CheckBox();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.btnKillProcess = new System.Windows.Forms.Button();
            this.lbArgs = new System.Windows.Forms.Label();
            this.tbArgs = new System.Windows.Forms.TextBox();
            this.cbTerminateOnReExec = new System.Windows.Forms.CheckBox();
            this.rbExecuteWith = new System.Windows.Forms.RadioButton();
            this.rbExecuteScript = new System.Windows.Forms.RadioButton();
            this.rbExecuteOtherProgram = new System.Windows.Forms.RadioButton();
            this.rbExecuteCorruptedFile = new System.Windows.Forms.RadioButton();
            this.rbNoExecution = new System.Windows.Forms.RadioButton();
            this.btnEditExec = new System.Windows.Forms.Button();
            this.lbExecution = new System.Windows.Forms.Label();
            this.cbInjectOnSelect = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnEnableCaching = new System.Windows.Forms.Button();
            this.btnDisableAutoUncorrupt = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnTargetPanel.SuspendLayout();
            this.pnBottom.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnBrowseTarget.FlatAppearance.BorderSize = 0;
            this.btnBrowseTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnBrowseTarget.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.btnBrowseTarget.Location = new System.Drawing.Point(13, 38);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(71, 23);
            this.btnBrowseTarget.TabIndex = 1;
            this.btnBrowseTarget.TabStop = false;
            this.btnBrowseTarget.Tag = "color:darker";
            this.btnBrowseTarget.Text = "Browse";
            this.btnBrowseTarget.UseVisualStyleBackColor = false;
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTarget.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.lbTarget.Location = new System.Drawing.Point(88, 38);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Padding = new System.Windows.Forms.Padding(3, 6, 1, 1);
            this.lbTarget.Size = new System.Drawing.Size(416, 23);
            this.lbTarget.TabIndex = 12;
            this.lbTarget.Tag = "color:darker";
            this.lbTarget.Text = "No target selected";
            // 
            // btnRestoreFileBackup
            // 
            this.btnRestoreFileBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestoreFileBackup.FlatAppearance.BorderSize = 0;
            this.btnRestoreFileBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreFileBackup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRestoreFileBackup.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnRestoreFileBackup.Location = new System.Drawing.Point(397, 11);
            this.btnRestoreFileBackup.Name = "btnRestoreFileBackup";
            this.btnRestoreFileBackup.Size = new System.Drawing.Size(107, 23);
            this.btnRestoreFileBackup.TabIndex = 27;
            this.btnRestoreFileBackup.TabStop = false;
            this.btnRestoreFileBackup.Tag = "color:darker";
            this.btnRestoreFileBackup.Text = "Restore Backup";
            this.btnRestoreFileBackup.UseVisualStyleBackColor = false;
            this.btnRestoreFileBackup.Click += new System.EventHandler(this.btnRestoreFileBackup_Click);
            // 
            // rbTargetFile
            // 
            this.rbTargetFile.AutoSize = true;
            this.rbTargetFile.Checked = true;
            this.rbTargetFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbTargetFile.ForeColor = System.Drawing.Color.White;
            this.rbTargetFile.Location = new System.Drawing.Point(17, 14);
            this.rbTargetFile.Name = "rbTargetFile";
            this.rbTargetFile.Size = new System.Drawing.Size(77, 17);
            this.rbTargetFile.TabIndex = 30;
            this.rbTargetFile.TabStop = true;
            this.rbTargetFile.Text = "Target File";
            this.rbTargetFile.UseVisualStyleBackColor = true;
            this.rbTargetFile.CheckedChanged += new System.EventHandler(this.rbTargetFile_CheckedChanged);
            // 
            // pnTargetPanel
            // 
            this.pnTargetPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.pnTargetPanel.Controls.Add(this.rbTargetMultipleFiles);
            this.pnTargetPanel.Controls.Add(this.btnClearAllBackups);
            this.pnTargetPanel.Controls.Add(this.btnResetBackup);
            this.pnTargetPanel.Controls.Add(this.rbTargetFile);
            this.pnTargetPanel.Controls.Add(this.btnBrowseTarget);
            this.pnTargetPanel.Controls.Add(this.lbTarget);
            this.pnTargetPanel.Controls.Add(this.btnRestoreFileBackup);
            this.pnTargetPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTargetPanel.Location = new System.Drawing.Point(0, 0);
            this.pnTargetPanel.Name = "pnTargetPanel";
            this.pnTargetPanel.Size = new System.Drawing.Size(765, 73);
            this.pnTargetPanel.TabIndex = 39;
            this.pnTargetPanel.Tag = "color:darkest";
            // 
            // rbTargetMultipleFiles
            // 
            this.rbTargetMultipleFiles.AutoSize = true;
            this.rbTargetMultipleFiles.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbTargetMultipleFiles.ForeColor = System.Drawing.Color.White;
            this.rbTargetMultipleFiles.Location = new System.Drawing.Point(98, 14);
            this.rbTargetMultipleFiles.Name = "rbTargetMultipleFiles";
            this.rbTargetMultipleFiles.Size = new System.Drawing.Size(128, 17);
            this.rbTargetMultipleFiles.TabIndex = 34;
            this.rbTargetMultipleFiles.Text = "Target Multiple Files";
            this.rbTargetMultipleFiles.UseVisualStyleBackColor = true;
            this.rbTargetMultipleFiles.CheckedChanged += new System.EventHandler(this.rbTargetMultipleFiles_CheckedChanged);
            // 
            // btnClearAllBackups
            // 
            this.btnClearAllBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnClearAllBackups.FlatAppearance.BorderSize = 0;
            this.btnClearAllBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllBackups.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearAllBackups.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnClearAllBackups.Location = new System.Drawing.Point(509, 38);
            this.btnClearAllBackups.Name = "btnClearAllBackups";
            this.btnClearAllBackups.Size = new System.Drawing.Size(108, 23);
            this.btnClearAllBackups.TabIndex = 33;
            this.btnClearAllBackups.TabStop = false;
            this.btnClearAllBackups.Tag = "color:darker";
            this.btnClearAllBackups.Text = "Clear all Backups";
            this.btnClearAllBackups.UseVisualStyleBackColor = false;
            this.btnClearAllBackups.Click += new System.EventHandler(this.btnClearAllBackups_Click);
            // 
            // btnResetBackup
            // 
            this.btnResetBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnResetBackup.FlatAppearance.BorderSize = 0;
            this.btnResetBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBackup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnResetBackup.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnResetBackup.Location = new System.Drawing.Point(509, 11);
            this.btnResetBackup.Name = "btnResetBackup";
            this.btnResetBackup.Size = new System.Drawing.Size(108, 23);
            this.btnResetBackup.TabIndex = 32;
            this.btnResetBackup.TabStop = false;
            this.btnResetBackup.Tag = "color:darker";
            this.btnResetBackup.Text = "Reset Backup";
            this.btnResetBackup.UseVisualStyleBackColor = false;
            this.btnResetBackup.Click += new System.EventHandler(this.btnResetBackup_Click);
            // 
            // cbWriteCopyMode
            // 
            this.cbWriteCopyMode.AutoSize = true;
            this.cbWriteCopyMode.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbWriteCopyMode.ForeColor = System.Drawing.Color.White;
            this.cbWriteCopyMode.Location = new System.Drawing.Point(114, 58);
            this.cbWriteCopyMode.Name = "cbWriteCopyMode";
            this.cbWriteCopyMode.Size = new System.Drawing.Size(126, 17);
            this.cbWriteCopyMode.TabIndex = 34;
            this.cbWriteCopyMode.TabStop = false;
            this.cbWriteCopyMode.Text = "Write in copy mode";
            this.cbWriteCopyMode.UseVisualStyleBackColor = true;
            this.cbWriteCopyMode.CheckedChanged += new System.EventHandler(this.cbWriteCopyMode_CheckedChanged);
            // 
            // pnBottom
            // 
            this.pnBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.pnBottom.Controls.Add(this.btnKillProcess);
            this.pnBottom.Controls.Add(this.lbArgs);
            this.pnBottom.Controls.Add(this.tbArgs);
            this.pnBottom.Controls.Add(this.cbTerminateOnReExec);
            this.pnBottom.Controls.Add(this.cbWriteCopyMode);
            this.pnBottom.Controls.Add(this.rbExecuteWith);
            this.pnBottom.Controls.Add(this.rbExecuteScript);
            this.pnBottom.Controls.Add(this.rbExecuteOtherProgram);
            this.pnBottom.Controls.Add(this.rbExecuteCorruptedFile);
            this.pnBottom.Controls.Add(this.rbNoExecution);
            this.pnBottom.Controls.Add(this.btnEditExec);
            this.pnBottom.Controls.Add(this.lbExecution);
            this.pnBottom.Controls.Add(this.cbInjectOnSelect);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 368);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(765, 92);
            this.pnBottom.TabIndex = 40;
            this.pnBottom.Tag = "color:darkest";
            // 
            // btnKillProcess
            // 
            this.btnKillProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnKillProcess.FlatAppearance.BorderSize = 0;
            this.btnKillProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKillProcess.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnKillProcess.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnKillProcess.Location = new System.Drawing.Point(528, 30);
            this.btnKillProcess.Name = "btnKillProcess";
            this.btnKillProcess.Size = new System.Drawing.Size(87, 23);
            this.btnKillProcess.TabIndex = 55;
            this.btnKillProcess.TabStop = false;
            this.btnKillProcess.Tag = "color:darker";
            this.btnKillProcess.Text = "Kill Process";
            this.btnKillProcess.UseVisualStyleBackColor = false;
            this.btnKillProcess.Click += new System.EventHandler(this.btnKillProcess_Click);
            // 
            // lbArgs
            // 
            this.lbArgs.AutoSize = true;
            this.lbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbArgs.ForeColor = System.Drawing.Color.White;
            this.lbArgs.Location = new System.Drawing.Point(454, 58);
            this.lbArgs.Name = "lbArgs";
            this.lbArgs.Size = new System.Drawing.Size(29, 13);
            this.lbArgs.TabIndex = 54;
            this.lbArgs.Text = "args";
            this.lbArgs.Visible = false;
            // 
            // tbArgs
            // 
            this.tbArgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbArgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbArgs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbArgs.Location = new System.Drawing.Point(484, 59);
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(131, 15);
            this.tbArgs.TabIndex = 53;
            this.tbArgs.Tag = "color:darker";
            this.tbArgs.Visible = false;
            // 
            // cbTerminateOnReExec
            // 
            this.cbTerminateOnReExec.AutoSize = true;
            this.cbTerminateOnReExec.Checked = true;
            this.cbTerminateOnReExec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTerminateOnReExec.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbTerminateOnReExec.ForeColor = System.Drawing.Color.White;
            this.cbTerminateOnReExec.Location = new System.Drawing.Point(247, 58);
            this.cbTerminateOnReExec.Name = "cbTerminateOnReExec";
            this.cbTerminateOnReExec.Size = new System.Drawing.Size(196, 17);
            this.cbTerminateOnReExec.TabIndex = 48;
            this.cbTerminateOnReExec.TabStop = false;
            this.cbTerminateOnReExec.Text = "Terminate Target on Re-Execution";
            this.cbTerminateOnReExec.UseVisualStyleBackColor = true;
            // 
            // rbExecuteWith
            // 
            this.rbExecuteWith.AutoSize = true;
            this.rbExecuteWith.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteWith.ForeColor = System.Drawing.Color.White;
            this.rbExecuteWith.Location = new System.Drawing.Point(258, 8);
            this.rbExecuteWith.Name = "rbExecuteWith";
            this.rbExecuteWith.Size = new System.Drawing.Size(90, 17);
            this.rbExecuteWith.TabIndex = 47;
            this.rbExecuteWith.Text = "Execute with";
            this.rbExecuteWith.UseVisualStyleBackColor = true;
            // 
            // rbExecuteScript
            // 
            this.rbExecuteScript.AutoSize = true;
            this.rbExecuteScript.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteScript.ForeColor = System.Drawing.Color.White;
            this.rbExecuteScript.Location = new System.Drawing.Point(501, 8);
            this.rbExecuteScript.Name = "rbExecuteScript";
            this.rbExecuteScript.Size = new System.Drawing.Size(54, 17);
            this.rbExecuteScript.TabIndex = 46;
            this.rbExecuteScript.Text = "Script";
            this.rbExecuteScript.UseVisualStyleBackColor = true;
            this.rbExecuteScript.Visible = false;
            this.rbExecuteScript.CheckedChanged += new System.EventHandler(this.rbExecuteScript_CheckedChanged);
            // 
            // rbExecuteOtherProgram
            // 
            this.rbExecuteOtherProgram.AutoSize = true;
            this.rbExecuteOtherProgram.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteOtherProgram.ForeColor = System.Drawing.Color.White;
            this.rbExecuteOtherProgram.Location = new System.Drawing.Point(354, 8);
            this.rbExecuteOtherProgram.Name = "rbExecuteOtherProgram";
            this.rbExecuteOtherProgram.Size = new System.Drawing.Size(142, 17);
            this.rbExecuteOtherProgram.TabIndex = 45;
            this.rbExecuteOtherProgram.Text = "Execute other program";
            this.rbExecuteOtherProgram.UseVisualStyleBackColor = true;
            this.rbExecuteOtherProgram.CheckedChanged += new System.EventHandler(this.rbExecuteOtherProgram_CheckedChanged);
            // 
            // rbExecuteCorruptedFile
            // 
            this.rbExecuteCorruptedFile.AutoSize = true;
            this.rbExecuteCorruptedFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteCorruptedFile.ForeColor = System.Drawing.Color.White;
            this.rbExecuteCorruptedFile.Location = new System.Drawing.Point(114, 8);
            this.rbExecuteCorruptedFile.Name = "rbExecuteCorruptedFile";
            this.rbExecuteCorruptedFile.Size = new System.Drawing.Size(137, 17);
            this.rbExecuteCorruptedFile.TabIndex = 44;
            this.rbExecuteCorruptedFile.Text = "Execute corrupted file";
            this.rbExecuteCorruptedFile.UseVisualStyleBackColor = true;
            this.rbExecuteCorruptedFile.CheckedChanged += new System.EventHandler(this.rbExecuteCorruptedFile_CheckedChanged);
            // 
            // rbNoExecution
            // 
            this.rbNoExecution.AutoSize = true;
            this.rbNoExecution.Checked = true;
            this.rbNoExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbNoExecution.ForeColor = System.Drawing.Color.White;
            this.rbNoExecution.Location = new System.Drawing.Point(9, 8);
            this.rbNoExecution.Name = "rbNoExecution";
            this.rbNoExecution.Size = new System.Drawing.Size(93, 17);
            this.rbNoExecution.TabIndex = 43;
            this.rbNoExecution.TabStop = true;
            this.rbNoExecution.Text = "No execution";
            this.rbNoExecution.UseVisualStyleBackColor = true;
            this.rbNoExecution.CheckedChanged += new System.EventHandler(this.rbNoExecution_CheckedChanged);
            // 
            // btnEditExec
            // 
            this.btnEditExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnEditExec.FlatAppearance.BorderSize = 0;
            this.btnEditExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditExec.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEditExec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEditExec.Location = new System.Drawing.Point(454, 30);
            this.btnEditExec.Name = "btnEditExec";
            this.btnEditExec.Size = new System.Drawing.Size(67, 23);
            this.btnEditExec.TabIndex = 35;
            this.btnEditExec.TabStop = false;
            this.btnEditExec.Tag = "color:darker";
            this.btnEditExec.Text = "Edit Exec";
            this.btnEditExec.UseVisualStyleBackColor = false;
            this.btnEditExec.Click += new System.EventHandler(this.btnEditExec_Click);
            // 
            // lbExecution
            // 
            this.lbExecution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbExecution.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.lbExecution.Location = new System.Drawing.Point(6, 30);
            this.lbExecution.Name = "lbExecution";
            this.lbExecution.Padding = new System.Windows.Forms.Padding(2, 5, 1, 1);
            this.lbExecution.Size = new System.Drawing.Size(440, 23);
            this.lbExecution.TabIndex = 42;
            this.lbExecution.Tag = "color:darker";
            this.lbExecution.Text = "No execution set";
            // 
            // cbInjectOnSelect
            // 
            this.cbInjectOnSelect.AutoSize = true;
            this.cbInjectOnSelect.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbInjectOnSelect.ForeColor = System.Drawing.Color.White;
            this.cbInjectOnSelect.Location = new System.Drawing.Point(7, 58);
            this.cbInjectOnSelect.Name = "cbInjectOnSelect";
            this.cbInjectOnSelect.Size = new System.Drawing.Size(103, 17);
            this.cbInjectOnSelect.TabIndex = 41;
            this.cbInjectOnSelect.TabStop = false;
            this.cbInjectOnSelect.Text = "Inject on select";
            this.cbInjectOnSelect.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(688, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 17);
            this.label9.TabIndex = 54;
            this.label9.Text = "args";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnEnableCaching
            // 
            this.btnEnableCaching.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnEnableCaching.FlatAppearance.BorderSize = 0;
            this.btnEnableCaching.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnableCaching.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEnableCaching.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnEnableCaching.Location = new System.Drawing.Point(220, 10);
            this.btnEnableCaching.Name = "btnEnableCaching";
            this.btnEnableCaching.Size = new System.Drawing.Size(209, 21);
            this.btnEnableCaching.TabIndex = 35;
            this.btnEnableCaching.TabStop = false;
            this.btnEnableCaching.Tag = "color:darker";
            this.btnEnableCaching.Text = "Enable Cache + Multithread";
            this.btnEnableCaching.UseVisualStyleBackColor = false;
            this.btnEnableCaching.Click += new System.EventHandler(this.btnEnableCaching_Click);
            // 
            // btnDisableAutoUncorrupt
            // 
            this.btnDisableAutoUncorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnDisableAutoUncorrupt.FlatAppearance.BorderSize = 0;
            this.btnDisableAutoUncorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableAutoUncorrupt.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnDisableAutoUncorrupt.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnDisableAutoUncorrupt.Location = new System.Drawing.Point(70, 10);
            this.btnDisableAutoUncorrupt.Name = "btnDisableAutoUncorrupt";
            this.btnDisableAutoUncorrupt.Size = new System.Drawing.Size(142, 22);
            this.btnDisableAutoUncorrupt.TabIndex = 36;
            this.btnDisableAutoUncorrupt.TabStop = false;
            this.btnDisableAutoUncorrupt.Tag = "color:darker";
            this.btnDisableAutoUncorrupt.Text = "Disable Auto-Uncorrupt";
            this.btnDisableAutoUncorrupt.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(17, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 17);
            this.label8.TabIndex = 37;
            this.label8.Text = "Debug";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.btnDisableAutoUncorrupt);
            this.panel3.Controls.Add(this.btnEnableCaching);
            this.panel3.Location = new System.Drawing.Point(12, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(450, 42);
            this.panel3.TabIndex = 115;
            this.panel3.Tag = "color:darkest";
            // 
            // WFV_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(765, 460);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pnTargetPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WFV_MainForm";
            this.Tag = "color:dark";
            this.Text = "Vanguard for Windows Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFV_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.WFV_MainForm_Load);
            this.pnTargetPanel.ResumeLayout(false);
            this.pnTargetPanel.PerformLayout();
            this.pnBottom.ResumeLayout(false);
            this.pnBottom.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.Button btnRestoreFileBackup;
        public System.Windows.Forms.RadioButton rbTargetFile;
        private System.Windows.Forms.Panel pnTargetPanel;
        public System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.Button btnClearAllBackups;
        private System.Windows.Forms.Button btnResetBackup;
        private System.Windows.Forms.CheckBox cbWriteCopyMode;
        public System.Windows.Forms.RadioButton rbExecuteOtherProgram;
        public System.Windows.Forms.RadioButton rbExecuteCorruptedFile;
        public System.Windows.Forms.RadioButton rbNoExecution;
        private System.Windows.Forms.Button btnEditExec;
        public System.Windows.Forms.Label lbExecution;
        public System.Windows.Forms.RadioButton rbExecuteScript;
        public System.Windows.Forms.RadioButton rbExecuteWith;
        private System.Windows.Forms.CheckBox cbTerminateOnReExec;
        public System.Windows.Forms.RadioButton rbTargetMultipleFiles;
        public System.Windows.Forms.Panel pnBottom;
        public System.Windows.Forms.TextBox tbArgs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnKillProcess;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label lbArgs;
        private System.Windows.Forms.CheckBox cbInjectOnSelect;
        public System.Windows.Forms.Button btnEnableCaching;
        private System.Windows.Forms.Button btnDisableAutoUncorrupt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
    }
}

