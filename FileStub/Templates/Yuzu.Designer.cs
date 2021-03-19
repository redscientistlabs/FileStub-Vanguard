
namespace FileStub.Templates
{
    partial class FileStubTemplateYuzu
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
            this.pnTarget = new System.Windows.Forms.Panel();
            this.cbSelectedGame = new System.Windows.Forms.ComboBox();
            this.lbGameName = new System.Windows.Forms.Label();
            this.btnEditExec = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnGetSegments = new System.Windows.Forms.Button();
            this.lbNSOTarget = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnTarget.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTemplateDescription
            // 
            this.lbTemplateDescription.AllowDrop = true;
            this.lbTemplateDescription.BackColor = System.Drawing.Color.Transparent;
            this.lbTemplateDescription.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbTemplateDescription.ForeColor = System.Drawing.Color.White;
            this.lbTemplateDescription.Location = new System.Drawing.Point(-2, 0);
            this.lbTemplateDescription.Name = "lbTemplateDescription";
            this.lbTemplateDescription.Padding = new System.Windows.Forms.Padding(16);
            this.lbTemplateDescription.Size = new System.Drawing.Size(276, 103);
            this.lbTemplateDescription.TabIndex = 38;
            this.lbTemplateDescription.Tag = "";
            this.lbTemplateDescription.Text = "_";
            this.lbTemplateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnTarget
            // 
            this.pnTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.pnTarget.Controls.Add(this.cbSelectedGame);
            this.pnTarget.Controls.Add(this.lbGameName);
            this.pnTarget.Controls.Add(this.btnEditExec);
            this.pnTarget.Controls.Add(this.btnDecompress);
            this.pnTarget.Controls.Add(this.btnGetSegments);
            this.pnTarget.Controls.Add(this.lbNSOTarget);
            this.pnTarget.Location = new System.Drawing.Point(14, 126);
            this.pnTarget.Name = "pnTarget";
            this.pnTarget.Size = new System.Drawing.Size(274, 140);
            this.pnTarget.TabIndex = 39;
            this.pnTarget.Tag = "color:normal";
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
            "None"});
            this.cbSelectedGame.Location = new System.Drawing.Point(90, 116);
            this.cbSelectedGame.Name = "cbSelectedGame";
            this.cbSelectedGame.Size = new System.Drawing.Size(181, 21);
            this.cbSelectedGame.TabIndex = 140;
            this.cbSelectedGame.Tag = "color:normal";
            // 
            // lbGameName
            // 
            this.lbGameName.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbGameName.ForeColor = System.Drawing.Color.White;
            this.lbGameName.Location = new System.Drawing.Point(90, 93);
            this.lbGameName.Name = "lbGameName";
            this.lbGameName.Size = new System.Drawing.Size(181, 20);
            this.lbGameName.TabIndex = 194;
            this.lbGameName.Text = "No game selected.";
            // 
            // btnEditExec
            // 
            this.btnEditExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditExec.BackColor = System.Drawing.Color.Gray;
            this.btnEditExec.FlatAppearance.BorderSize = 0;
            this.btnEditExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditExec.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnEditExec.ForeColor = System.Drawing.Color.White;
            this.btnEditExec.Location = new System.Drawing.Point(3, 105);
            this.btnEditExec.Name = "btnEditExec";
            this.btnEditExec.Size = new System.Drawing.Size(81, 32);
            this.btnEditExec.TabIndex = 193;
            this.btnEditExec.TabStop = false;
            this.btnEditExec.Tag = "color:light1";
            this.btnEditExec.Text = "Select Yuzu";
            this.btnEditExec.UseVisualStyleBackColor = false;
            this.btnEditExec.Click += new System.EventHandler(this.btnEditExec_Click);
            // 
            // btnDecompress
            // 
            this.btnDecompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDecompress.FlatAppearance.BorderSize = 0;
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.Location = new System.Drawing.Point(139, 41);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(132, 34);
            this.btnDecompress.TabIndex = 192;
            this.btnDecompress.TabStop = false;
            this.btnDecompress.Tag = "color:dark1";
            this.btnDecompress.Text = "Decompress Executable";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnGetSegments
            // 
            this.btnGetSegments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetSegments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGetSegments.FlatAppearance.BorderSize = 0;
            this.btnGetSegments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSegments.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGetSegments.ForeColor = System.Drawing.Color.White;
            this.btnGetSegments.Location = new System.Drawing.Point(139, 3);
            this.btnGetSegments.Name = "btnGetSegments";
            this.btnGetSegments.Size = new System.Drawing.Size(132, 32);
            this.btnGetSegments.TabIndex = 192;
            this.btnGetSegments.TabStop = false;
            this.btnGetSegments.Tag = "color:dark1";
            this.btnGetSegments.Text = "Get Segments";
            this.btnGetSegments.UseVisualStyleBackColor = false;
            this.btnGetSegments.Click += new System.EventHandler(this.btnGetSegments_Click);
            // 
            // lbNSOTarget
            // 
            this.lbNSOTarget.AllowDrop = true;
            this.lbNSOTarget.BackColor = System.Drawing.Color.Transparent;
            this.lbNSOTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbNSOTarget.ForeColor = System.Drawing.Color.White;
            this.lbNSOTarget.Location = new System.Drawing.Point(3, 3);
            this.lbNSOTarget.Name = "lbNSOTarget";
            this.lbNSOTarget.Padding = new System.Windows.Forms.Padding(8);
            this.lbNSOTarget.Size = new System.Drawing.Size(130, 32);
            this.lbNSOTarget.TabIndex = 39;
            this.lbNSOTarget.Tag = "";
            this.lbNSOTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbTemplateDescription);
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 103);
            this.panel1.TabIndex = 40;
            this.panel1.Tag = "color:normal";
            // 
            // FileStubTemplateYuzu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileStubTemplateYuzu";
            this.Tag = "color:dark1";
            this.Text = "Unity";
            this.pnTarget.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbTemplateDescription;
        private System.Windows.Forms.Panel pnTarget;
        public System.Windows.Forms.Label lbNSOTarget;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGetSegments;
        private System.Windows.Forms.Button btnEditExec;
        private System.Windows.Forms.Button btnDecompress;
        public System.Windows.Forms.Label lbGameName;
        public System.Windows.Forms.ComboBox cbSelectedGame;
    }
}
