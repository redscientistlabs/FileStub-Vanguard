namespace FileStub
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.pnBottom = new System.Windows.Forms.Panel();
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
            this.pnSideBar = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pnGlitchHarvesterOpen = new System.Windows.Forms.Panel();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.btnResetBackup = new System.Windows.Forms.Button();
            this.btnKillProcess = new System.Windows.Forms.Button();
            this.pnBottom.SuspendLayout();
            this.pnSideBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(133, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Selected Target";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(131, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 79);
            this.panel2.TabIndex = 13;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSettings.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(538, 13);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(32, 32);
            this.btnSettings.TabIndex = 172;
            this.btnSettings.TabStop = false;
            this.btnSettings.Tag = "color:light1";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnSettings_MouseDown);
            // 
            // pnBottom
            // 
            this.pnBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnBottom.Controls.Add(this.btnKillProcess);
            this.pnBottom.Controls.Add(this.lbArgs);
            this.pnBottom.Controls.Add(this.tbArgs);
            this.pnBottom.Controls.Add(this.cbTerminateOnReExec);
            this.pnBottom.Controls.Add(this.rbExecuteWith);
            this.pnBottom.Controls.Add(this.rbExecuteScript);
            this.pnBottom.Controls.Add(this.rbExecuteOtherProgram);
            this.pnBottom.Controls.Add(this.rbExecuteCorruptedFile);
            this.pnBottom.Controls.Add(this.rbNoExecution);
            this.pnBottom.Controls.Add(this.btnEditExec);
            this.pnBottom.Controls.Add(this.lbExecution);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 141);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(582, 87);
            this.pnBottom.TabIndex = 173;
            this.pnBottom.Tag = "color:darkest";
            // 
            // lbArgs
            // 
            this.lbArgs.AutoSize = true;
            this.lbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbArgs.ForeColor = System.Drawing.Color.White;
            this.lbArgs.Location = new System.Drawing.Point(220, 59);
            this.lbArgs.Name = "lbArgs";
            this.lbArgs.Size = new System.Drawing.Size(29, 13);
            this.lbArgs.TabIndex = 54;
            this.lbArgs.Text = "args";
            this.lbArgs.Visible = false;
            // 
            // tbArgs
            // 
            this.tbArgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbArgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbArgs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbArgs.Location = new System.Drawing.Point(250, 60);
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(320, 15);
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
            this.cbTerminateOnReExec.Location = new System.Drawing.Point(9, 59);
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
            this.rbExecuteWith.Location = new System.Drawing.Point(266, 8);
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
            this.rbExecuteScript.Location = new System.Drawing.Point(515, 8);
            this.rbExecuteScript.Name = "rbExecuteScript";
            this.rbExecuteScript.Size = new System.Drawing.Size(54, 17);
            this.rbExecuteScript.TabIndex = 46;
            this.rbExecuteScript.Text = "Script";
            this.rbExecuteScript.UseVisualStyleBackColor = true;
            this.rbExecuteScript.Visible = false;
            // 
            // rbExecuteOtherProgram
            // 
            this.rbExecuteOtherProgram.AutoSize = true;
            this.rbExecuteOtherProgram.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteOtherProgram.ForeColor = System.Drawing.Color.White;
            this.rbExecuteOtherProgram.Location = new System.Drawing.Point(365, 8);
            this.rbExecuteOtherProgram.Name = "rbExecuteOtherProgram";
            this.rbExecuteOtherProgram.Size = new System.Drawing.Size(142, 17);
            this.rbExecuteOtherProgram.TabIndex = 45;
            this.rbExecuteOtherProgram.Text = "Execute other program";
            this.rbExecuteOtherProgram.UseVisualStyleBackColor = true;
            // 
            // rbExecuteCorruptedFile
            // 
            this.rbExecuteCorruptedFile.AutoSize = true;
            this.rbExecuteCorruptedFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbExecuteCorruptedFile.ForeColor = System.Drawing.Color.White;
            this.rbExecuteCorruptedFile.Location = new System.Drawing.Point(119, 8);
            this.rbExecuteCorruptedFile.Name = "rbExecuteCorruptedFile";
            this.rbExecuteCorruptedFile.Size = new System.Drawing.Size(137, 17);
            this.rbExecuteCorruptedFile.TabIndex = 44;
            this.rbExecuteCorruptedFile.Text = "Execute corrupted file";
            this.rbExecuteCorruptedFile.UseVisualStyleBackColor = true;
            // 
            // rbNoExecution
            // 
            this.rbNoExecution.AutoSize = true;
            this.rbNoExecution.Checked = true;
            this.rbNoExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rbNoExecution.ForeColor = System.Drawing.Color.White;
            this.rbNoExecution.Location = new System.Drawing.Point(14, 8);
            this.rbNoExecution.Name = "rbNoExecution";
            this.rbNoExecution.Size = new System.Drawing.Size(93, 17);
            this.rbNoExecution.TabIndex = 43;
            this.rbNoExecution.TabStop = true;
            this.rbNoExecution.Text = "No execution";
            this.rbNoExecution.UseVisualStyleBackColor = true;
            // 
            // btnEditExec
            // 
            this.btnEditExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEditExec.FlatAppearance.BorderSize = 0;
            this.btnEditExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditExec.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEditExec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEditExec.Location = new System.Drawing.Point(415, 30);
            this.btnEditExec.Name = "btnEditExec";
            this.btnEditExec.Size = new System.Drawing.Size(67, 23);
            this.btnEditExec.TabIndex = 35;
            this.btnEditExec.TabStop = false;
            this.btnEditExec.Tag = "color:darker";
            this.btnEditExec.Text = "Edit Exec";
            this.btnEditExec.UseVisualStyleBackColor = false;
            // 
            // lbExecution
            // 
            this.lbExecution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbExecution.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.lbExecution.Location = new System.Drawing.Point(9, 30);
            this.lbExecution.Name = "lbExecution";
            this.lbExecution.Padding = new System.Windows.Forms.Padding(2, 5, 1, 1);
            this.lbExecution.Size = new System.Drawing.Size(400, 23);
            this.lbExecution.TabIndex = 42;
            this.lbExecution.Tag = "color:darker";
            this.lbExecution.Text = "No execution set";
            // 
            // pnSideBar
            // 
            this.pnSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnSideBar.Controls.Add(this.button1);
            this.pnSideBar.Controls.Add(this.pnGlitchHarvesterOpen);
            this.pnSideBar.Controls.Add(this.btnRestoreBackup);
            this.pnSideBar.Controls.Add(this.btnResetBackup);
            this.pnSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnSideBar.Name = "pnSideBar";
            this.pnSideBar.Size = new System.Drawing.Size(118, 141);
            this.pnSideBar.TabIndex = 174;
            this.pnSideBar.Tag = "color:dark3";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.OrangeRed;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 97);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(133, 34);
            this.button1.TabIndex = 121;
            this.button1.TabStop = false;
            this.button1.Tag = "color:dark3";
            this.button1.Text = "Clear All Backups";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // pnGlitchHarvesterOpen
            // 
            this.pnGlitchHarvesterOpen.BackColor = System.Drawing.Color.Gray;
            this.pnGlitchHarvesterOpen.Location = new System.Drawing.Point(-19, 188);
            this.pnGlitchHarvesterOpen.Name = "pnGlitchHarvesterOpen";
            this.pnGlitchHarvesterOpen.Size = new System.Drawing.Size(23, 25);
            this.pnGlitchHarvesterOpen.TabIndex = 8;
            this.pnGlitchHarvesterOpen.Tag = "color:light1";
            this.pnGlitchHarvesterOpen.Visible = false;
            // 
            // btnRestoreBackup
            // 
            this.btnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRestoreBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestoreBackup.FlatAppearance.BorderSize = 0;
            this.btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnRestoreBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRestoreBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestoreBackup.Location = new System.Drawing.Point(0, 19);
            this.btnRestoreBackup.Name = "btnRestoreBackup";
            this.btnRestoreBackup.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnRestoreBackup.Size = new System.Drawing.Size(133, 34);
            this.btnRestoreBackup.TabIndex = 119;
            this.btnRestoreBackup.TabStop = false;
            this.btnRestoreBackup.Tag = "color:dark3";
            this.btnRestoreBackup.Text = "Restore Backup";
            this.btnRestoreBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestoreBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestoreBackup.UseVisualStyleBackColor = false;
            // 
            // btnResetBackup
            // 
            this.btnResetBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnResetBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnResetBackup.FlatAppearance.BorderSize = 0;
            this.btnResetBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnResetBackup.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnResetBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetBackup.Location = new System.Drawing.Point(0, 57);
            this.btnResetBackup.Name = "btnResetBackup";
            this.btnResetBackup.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnResetBackup.Size = new System.Drawing.Size(133, 34);
            this.btnResetBackup.TabIndex = 120;
            this.btnResetBackup.TabStop = false;
            this.btnResetBackup.Tag = "color:dark3";
            this.btnResetBackup.Text = "Reset Backup";
            this.btnResetBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetBackup.UseVisualStyleBackColor = false;
            // 
            // btnKillProcess
            // 
            this.btnKillProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnKillProcess.FlatAppearance.BorderSize = 0;
            this.btnKillProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKillProcess.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnKillProcess.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnKillProcess.Location = new System.Drawing.Point(489, 30);
            this.btnKillProcess.Name = "btnKillProcess";
            this.btnKillProcess.Size = new System.Drawing.Size(81, 23);
            this.btnKillProcess.TabIndex = 55;
            this.btnKillProcess.TabStop = false;
            this.btnKillProcess.Tag = "color:darker";
            this.btnKillProcess.Text = "Kill Process";
            this.btnKillProcess.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(582, 228);
            this.Controls.Add(this.pnSideBar);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(598, 267);
            this.Name = "MainForm";
            this.Text = "File Stub ";
            this.Load += new System.EventHandler(this.CS_Core_Form_Load);
            this.pnBottom.ResumeLayout(false);
            this.pnBottom.PerformLayout();
            this.pnSideBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnSettings;
        public System.Windows.Forms.Panel pnBottom;
        public System.Windows.Forms.Label lbArgs;
        public System.Windows.Forms.TextBox tbArgs;
        private System.Windows.Forms.CheckBox cbTerminateOnReExec;
        public System.Windows.Forms.RadioButton rbExecuteWith;
        public System.Windows.Forms.RadioButton rbExecuteScript;
        public System.Windows.Forms.RadioButton rbExecuteOtherProgram;
        public System.Windows.Forms.RadioButton rbExecuteCorruptedFile;
        public System.Windows.Forms.RadioButton rbNoExecution;
        private System.Windows.Forms.Button btnEditExec;
        public System.Windows.Forms.Label lbExecution;
        private System.Windows.Forms.Panel pnSideBar;
        public System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Panel pnGlitchHarvesterOpen;
        public System.Windows.Forms.Button btnRestoreBackup;
        public System.Windows.Forms.Button btnResetBackup;
        private System.Windows.Forms.Button btnKillProcess;
    }
}

