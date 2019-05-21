namespace FileStub
{
    partial class StubForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StubForm));
            this.label5 = new System.Windows.Forms.Label();
            this.pnTarget = new System.Windows.Forms.Panel();
            this.cbTargetType = new System.Windows.Forms.ComboBox();
            this.btnUnloadTarget = new System.Windows.Forms.Button();
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.lbTarget = new System.Windows.Forms.Label();
            this.btnTargetSettings = new System.Windows.Forms.Button();
            this.btnKillProcess = new System.Windows.Forms.Button();
            this.lbArgs = new System.Windows.Forms.Label();
            this.tbArgs = new System.Windows.Forms.TextBox();
            this.lbExecution = new System.Windows.Forms.Label();
            this.pnSideBar = new System.Windows.Forms.Panel();
            this.lbTargetStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearAllBackups = new System.Windows.Forms.Button();
            this.pnGlitchHarvesterOpen = new System.Windows.Forms.Panel();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.btnResetBackup = new System.Windows.Forms.Button();
            this.cbSelectedExecution = new System.Windows.Forms.ComboBox();
            this.pnTargetExecution = new System.Windows.Forms.Panel();
            this.btnEditExec = new System.Windows.Forms.Button();
            this.lbTargetExecution = new System.Windows.Forms.Label();
            this.pnTarget.SuspendLayout();
            this.pnSideBar.SuspendLayout();
            this.pnTargetExecution.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(131, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Selected Target";
            // 
            // pnTarget
            // 
            this.pnTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnTarget.Controls.Add(this.cbTargetType);
            this.pnTarget.Controls.Add(this.btnUnloadTarget);
            this.pnTarget.Controls.Add(this.btnBrowseTarget);
            this.pnTarget.Controls.Add(this.lbTarget);
            this.pnTarget.Location = new System.Drawing.Point(129, 49);
            this.pnTarget.Name = "pnTarget";
            this.pnTarget.Size = new System.Drawing.Size(359, 122);
            this.pnTarget.TabIndex = 13;
            // 
            // cbTargetType
            // 
            this.cbTargetType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTargetType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTargetType.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbTargetType.ForeColor = System.Drawing.Color.White;
            this.cbTargetType.FormattingEnabled = true;
            this.cbTargetType.Location = new System.Drawing.Point(12, 14);
            this.cbTargetType.Name = "cbTargetType";
            this.cbTargetType.Size = new System.Drawing.Size(248, 21);
            this.cbTargetType.TabIndex = 118;
            this.cbTargetType.TabStop = false;
            this.cbTargetType.Tag = "color:normal";
            this.cbTargetType.SelectedIndexChanged += new System.EventHandler(this.CbTargetType_SelectedIndexChanged);
            // 
            // btnUnloadTarget
            // 
            this.btnUnloadTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnloadTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnUnloadTarget.FlatAppearance.BorderSize = 0;
            this.btnUnloadTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnloadTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnUnloadTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUnloadTarget.Location = new System.Drawing.Point(263, 12);
            this.btnUnloadTarget.Name = "btnUnloadTarget";
            this.btnUnloadTarget.Size = new System.Drawing.Size(84, 23);
            this.btnUnloadTarget.TabIndex = 42;
            this.btnUnloadTarget.TabStop = false;
            this.btnUnloadTarget.Tag = "color:darker";
            this.btnUnloadTarget.Text = "Unload";
            this.btnUnloadTarget.UseVisualStyleBackColor = false;
            this.btnUnloadTarget.Visible = false;
            this.btnUnloadTarget.Click += new System.EventHandler(this.BtnReleaseTarget_Click);
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnBrowseTarget.FlatAppearance.BorderSize = 0;
            this.btnBrowseTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnBrowseTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBrowseTarget.Location = new System.Drawing.Point(12, 39);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(72, 23);
            this.btnBrowseTarget.TabIndex = 35;
            this.btnBrowseTarget.TabStop = false;
            this.btnBrowseTarget.Tag = "color:darker";
            this.btnBrowseTarget.Text = "Browse";
            this.btnBrowseTarget.UseVisualStyleBackColor = false;
            this.btnBrowseTarget.Click += new System.EventHandler(this.BtnBrowseTarget_Click);
            // 
            // lbTarget
            // 
            this.lbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTarget.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.lbTarget.Location = new System.Drawing.Point(87, 39);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Padding = new System.Windows.Forms.Padding(3, 6, 1, 1);
            this.lbTarget.Size = new System.Drawing.Size(260, 72);
            this.lbTarget.TabIndex = 36;
            this.lbTarget.Tag = "color:darker";
            this.lbTarget.Text = "No target selected";
            this.lbTarget.Visible = false;
            // 
            // btnTargetSettings
            // 
            this.btnTargetSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTargetSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTargetSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTargetSettings.FlatAppearance.BorderSize = 0;
            this.btnTargetSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTargetSettings.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnTargetSettings.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnTargetSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnTargetSettings.Image")));
            this.btnTargetSettings.Location = new System.Drawing.Point(456, 13);
            this.btnTargetSettings.Name = "btnTargetSettings";
            this.btnTargetSettings.Size = new System.Drawing.Size(32, 32);
            this.btnTargetSettings.TabIndex = 172;
            this.btnTargetSettings.TabStop = false;
            this.btnTargetSettings.Tag = "color:light1";
            this.btnTargetSettings.UseVisualStyleBackColor = false;
            this.btnTargetSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnTargetSettings_MouseDown);
            // 
            // btnKillProcess
            // 
            this.btnKillProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKillProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnKillProcess.FlatAppearance.BorderSize = 0;
            this.btnKillProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKillProcess.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnKillProcess.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnKillProcess.Location = new System.Drawing.Point(263, 38);
            this.btnKillProcess.Name = "btnKillProcess";
            this.btnKillProcess.Size = new System.Drawing.Size(81, 21);
            this.btnKillProcess.TabIndex = 55;
            this.btnKillProcess.TabStop = false;
            this.btnKillProcess.Tag = "color:darker";
            this.btnKillProcess.Text = "Kill Process";
            this.btnKillProcess.UseVisualStyleBackColor = false;
            this.btnKillProcess.Click += new System.EventHandler(this.BtnKillProcess_Click);
            // 
            // lbArgs
            // 
            this.lbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbArgs.AutoSize = true;
            this.lbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbArgs.ForeColor = System.Drawing.Color.White;
            this.lbArgs.Location = new System.Drawing.Point(12, 61);
            this.lbArgs.Name = "lbArgs";
            this.lbArgs.Size = new System.Drawing.Size(29, 13);
            this.lbArgs.TabIndex = 54;
            this.lbArgs.Text = "args";
            this.lbArgs.Visible = false;
            // 
            // tbArgs
            // 
            this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tbArgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbArgs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbArgs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbArgs.Location = new System.Drawing.Point(41, 62);
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(303, 15);
            this.tbArgs.TabIndex = 53;
            this.tbArgs.Tag = "color:darker";
            this.tbArgs.Visible = false;
            // 
            // lbExecution
            // 
            this.lbExecution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbExecution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbExecution.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.lbExecution.Location = new System.Drawing.Point(14, 38);
            this.lbExecution.Margin = new System.Windows.Forms.Padding(0);
            this.lbExecution.Name = "lbExecution";
            this.lbExecution.Padding = new System.Windows.Forms.Padding(2, 2, 1, 1);
            this.lbExecution.Size = new System.Drawing.Size(246, 21);
            this.lbExecution.TabIndex = 42;
            this.lbExecution.Tag = "color:darker";
            this.lbExecution.Text = "No execution set";
            this.lbExecution.Visible = false;
            // 
            // pnSideBar
            // 
            this.pnSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnSideBar.Controls.Add(this.lbTargetStatus);
            this.pnSideBar.Controls.Add(this.label2);
            this.pnSideBar.Controls.Add(this.btnClearAllBackups);
            this.pnSideBar.Controls.Add(this.pnGlitchHarvesterOpen);
            this.pnSideBar.Controls.Add(this.btnRestoreBackup);
            this.pnSideBar.Controls.Add(this.btnResetBackup);
            this.pnSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnSideBar.Name = "pnSideBar";
            this.pnSideBar.Size = new System.Drawing.Size(118, 313);
            this.pnSideBar.TabIndex = 174;
            this.pnSideBar.Tag = "color:dark3";
            // 
            // lbTargetStatus
            // 
            this.lbTargetStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTargetStatus.ForeColor = System.Drawing.Color.White;
            this.lbTargetStatus.Location = new System.Drawing.Point(9, 37);
            this.lbTargetStatus.Name = "lbTargetStatus";
            this.lbTargetStatus.Size = new System.Drawing.Size(110, 44);
            this.lbTargetStatus.TabIndex = 123;
            this.lbTargetStatus.Text = "No target selected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 122;
            this.label2.Text = "Status";
            // 
            // btnClearAllBackups
            // 
            this.btnClearAllBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnClearAllBackups.Enabled = false;
            this.btnClearAllBackups.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnClearAllBackups.FlatAppearance.BorderSize = 0;
            this.btnClearAllBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllBackups.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnClearAllBackups.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnClearAllBackups.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearAllBackups.Location = new System.Drawing.Point(0, 166);
            this.btnClearAllBackups.Name = "btnClearAllBackups";
            this.btnClearAllBackups.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClearAllBackups.Size = new System.Drawing.Size(133, 34);
            this.btnClearAllBackups.TabIndex = 121;
            this.btnClearAllBackups.TabStop = false;
            this.btnClearAllBackups.Tag = "color:dark3";
            this.btnClearAllBackups.Text = "Clear All Backups";
            this.btnClearAllBackups.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearAllBackups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearAllBackups.UseVisualStyleBackColor = false;
            this.btnClearAllBackups.Visible = false;
            this.btnClearAllBackups.Click += new System.EventHandler(this.BtnClearAllBackups_Click);
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
            this.btnRestoreBackup.Enabled = false;
            this.btnRestoreBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestoreBackup.FlatAppearance.BorderSize = 0;
            this.btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnRestoreBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRestoreBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestoreBackup.Location = new System.Drawing.Point(0, 90);
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
            this.btnRestoreBackup.Click += new System.EventHandler(this.BtnRestoreBackup_Click);
            // 
            // btnResetBackup
            // 
            this.btnResetBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnResetBackup.Enabled = false;
            this.btnResetBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnResetBackup.FlatAppearance.BorderSize = 0;
            this.btnResetBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnResetBackup.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnResetBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetBackup.Location = new System.Drawing.Point(0, 128);
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
            this.btnResetBackup.Click += new System.EventHandler(this.BtnResetBackup_Click);
            // 
            // cbSelectedExecution
            // 
            this.cbSelectedExecution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelectedExecution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbSelectedExecution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedExecution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedExecution.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectedExecution.ForeColor = System.Drawing.Color.White;
            this.cbSelectedExecution.FormattingEnabled = true;
            this.cbSelectedExecution.Location = new System.Drawing.Point(14, 14);
            this.cbSelectedExecution.Name = "cbSelectedExecution";
            this.cbSelectedExecution.Size = new System.Drawing.Size(246, 21);
            this.cbSelectedExecution.TabIndex = 117;
            this.cbSelectedExecution.TabStop = false;
            this.cbSelectedExecution.Tag = "color:normal";
            this.cbSelectedExecution.Visible = false;
            this.cbSelectedExecution.SelectedIndexChanged += new System.EventHandler(this.CbSelectedExecution_SelectedIndexChanged);
            // 
            // pnTargetExecution
            // 
            this.pnTargetExecution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTargetExecution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnTargetExecution.Controls.Add(this.btnEditExec);
            this.pnTargetExecution.Controls.Add(this.cbSelectedExecution);
            this.pnTargetExecution.Controls.Add(this.lbExecution);
            this.pnTargetExecution.Controls.Add(this.btnKillProcess);
            this.pnTargetExecution.Controls.Add(this.tbArgs);
            this.pnTargetExecution.Controls.Add(this.lbArgs);
            this.pnTargetExecution.Location = new System.Drawing.Point(129, 204);
            this.pnTargetExecution.Name = "pnTargetExecution";
            this.pnTargetExecution.Size = new System.Drawing.Size(359, 92);
            this.pnTargetExecution.TabIndex = 176;
            // 
            // btnEditExec
            // 
            this.btnEditExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnEditExec.FlatAppearance.BorderSize = 0;
            this.btnEditExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditExec.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEditExec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEditExec.Location = new System.Drawing.Point(263, 14);
            this.btnEditExec.Name = "btnEditExec";
            this.btnEditExec.Size = new System.Drawing.Size(81, 21);
            this.btnEditExec.TabIndex = 118;
            this.btnEditExec.TabStop = false;
            this.btnEditExec.Tag = "color:darker";
            this.btnEditExec.Text = "Edit Exec";
            this.btnEditExec.UseVisualStyleBackColor = false;
            this.btnEditExec.Click += new System.EventHandler(this.BtnEditExec_Click);
            // 
            // lbTargetExecution
            // 
            this.lbTargetExecution.AutoSize = true;
            this.lbTargetExecution.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lbTargetExecution.ForeColor = System.Drawing.Color.White;
            this.lbTargetExecution.Location = new System.Drawing.Point(131, 182);
            this.lbTargetExecution.Name = "lbTargetExecution";
            this.lbTargetExecution.Size = new System.Drawing.Size(95, 15);
            this.lbTargetExecution.TabIndex = 175;
            this.lbTargetExecution.Text = "Target execution";
            // 
            // StubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(500, 313);
            this.Controls.Add(this.pnTargetExecution);
            this.Controls.Add(this.lbTargetExecution);
            this.Controls.Add(this.pnSideBar);
            this.Controls.Add(this.btnTargetSettings);
            this.Controls.Add(this.pnTarget);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(516, 352);
            this.Name = "StubForm";
            this.Text = "File Stub ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StubForm_FormClosing);
            this.Load += new System.EventHandler(this.StubForm_Load);
            this.pnTarget.ResumeLayout(false);
            this.pnSideBar.ResumeLayout(false);
            this.pnSideBar.PerformLayout();
            this.pnTargetExecution.ResumeLayout(false);
            this.pnTargetExecution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnTarget;
        public System.Windows.Forms.Button btnTargetSettings;
        public System.Windows.Forms.Label lbArgs;
        public System.Windows.Forms.TextBox tbArgs;
        public System.Windows.Forms.Label lbExecution;
        private System.Windows.Forms.Panel pnSideBar;
        public System.Windows.Forms.Button btnClearAllBackups;
        internal System.Windows.Forms.Panel pnGlitchHarvesterOpen;
        public System.Windows.Forms.Button btnRestoreBackup;
        public System.Windows.Forms.Button btnResetBackup;
        private System.Windows.Forms.Button btnKillProcess;
        public System.Windows.Forms.ComboBox cbSelectedExecution;
        private System.Windows.Forms.Panel pnTargetExecution;
        public System.Windows.Forms.Label lbTargetExecution;
        private System.Windows.Forms.Button btnBrowseTarget;
        public System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.Button btnEditExec;
        private System.Windows.Forms.Button btnUnloadTarget;
        public System.Windows.Forms.Label lbTargetStatus;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cbTargetType;
    }
}

