
namespace FileStub.Templates
{
    partial class FileStubTemplateRPCS3
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
            this.lbTemplateDescription = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbGameFolder = new System.Windows.Forms.Label();
            this.lbTargetedGameRpx = new System.Windows.Forms.Label();
            this.lbTargetedGameId = new System.Windows.Forms.Label();
            this.cbSelectedGame = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbGameInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTemplateDescription
            // 
            this.lbTemplateDescription.AllowDrop = true;
            this.lbTemplateDescription.BackColor = System.Drawing.Color.Transparent;
            this.lbTemplateDescription.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTemplateDescription.ForeColor = System.Drawing.Color.White;
            this.lbTemplateDescription.Location = new System.Drawing.Point(-1, 0);
            this.lbTemplateDescription.Name = "lbTemplateDescription";
            this.lbTemplateDescription.Padding = new System.Windows.Forms.Padding(8);
            this.lbTemplateDescription.Size = new System.Drawing.Size(275, 78);
            this.lbTemplateDescription.TabIndex = 38;
            this.lbTemplateDescription.Tag = "";
            this.lbTemplateDescription.Text = "_";
            this.lbTemplateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbTemplateDescription);
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 78);
            this.panel1.TabIndex = 40;
            this.panel1.Tag = "color:normal";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel2.Controls.Add(this.lbGameInfo);
            this.panel2.Controls.Add(this.lbGameFolder);
            this.panel2.Controls.Add(this.lbTargetedGameRpx);
            this.panel2.Controls.Add(this.lbTargetedGameId);
            this.panel2.Controls.Add(this.cbSelectedGame);
            this.panel2.Location = new System.Drawing.Point(14, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 136);
            this.panel2.TabIndex = 42;
            this.panel2.Tag = "color:normal";
            // 
            // lbGameFolder
            // 
            this.lbGameFolder.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbGameFolder.ForeColor = System.Drawing.Color.White;
            this.lbGameFolder.Location = new System.Drawing.Point(14, 56);
            this.lbGameFolder.Name = "lbGameFolder";
            this.lbGameFolder.Size = new System.Drawing.Size(159, 40);
            this.lbGameFolder.TabIndex = 9;
            this.lbGameFolder.Visible = false;
            // 
            // lbTargetedGameRpx
            // 
            this.lbTargetedGameRpx.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbTargetedGameRpx.ForeColor = System.Drawing.Color.White;
            this.lbTargetedGameRpx.Location = new System.Drawing.Point(14, 40);
            this.lbTargetedGameRpx.Name = "lbTargetedGameRpx";
            this.lbTargetedGameRpx.Size = new System.Drawing.Size(87, 16);
            this.lbTargetedGameRpx.TabIndex = 9;
            this.lbTargetedGameRpx.Text = "No game selected.";
            // 
            // lbTargetedGameId
            // 
            this.lbTargetedGameId.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbTargetedGameId.ForeColor = System.Drawing.Color.White;
            this.lbTargetedGameId.Location = new System.Drawing.Point(9, 41);
            this.lbTargetedGameId.Name = "lbTargetedGameId";
            this.lbTargetedGameId.Size = new System.Drawing.Size(164, 15);
            this.lbTargetedGameId.TabIndex = 140;
            // 
            // cbSelectedGame
            // 
            this.cbSelectedGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbSelectedGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedGame.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectedGame.ForeColor = System.Drawing.Color.White;
            this.cbSelectedGame.FormattingEnabled = true;
            this.cbSelectedGame.Items.AddRange(new object[] {
            "Autodetect"});
            this.cbSelectedGame.Location = new System.Drawing.Point(14, 17);
            this.cbSelectedGame.Name = "cbSelectedGame";
            this.cbSelectedGame.Size = new System.Drawing.Size(159, 21);
            this.cbSelectedGame.TabIndex = 139;
            this.cbSelectedGame.Tag = "color:normal";
            this.cbSelectedGame.SelectedIndexChanged += new System.EventHandler(this.cbSelectedGame_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(18, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 41;
            this.label5.Text = "Targeted Game";
            // 
            // lbGameInfo
            // 
            this.lbGameInfo.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbGameInfo.ForeColor = System.Drawing.Color.White;
            this.lbGameInfo.Location = new System.Drawing.Point(2, 56);
            this.lbGameInfo.Name = "lbGameInfo";
            this.lbGameInfo.Size = new System.Drawing.Size(269, 80);
            this.lbGameInfo.TabIndex = 9;
            // 
            // FileStubTemplateRPCS3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileStubTemplateRPCS3";
            this.Tag = "color:dark1";
            this.Text = "Cemu";
            this.Load += new System.EventHandler(this.FileStubTemplateRPCS3_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbTemplateDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbTargetedGameRpx;
        public System.Windows.Forms.Label lbTargetedGameId;
        public System.Windows.Forms.ComboBox cbSelectedGame;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lbGameFolder;
        public System.Windows.Forms.Label lbGameInfo;
    }
}
