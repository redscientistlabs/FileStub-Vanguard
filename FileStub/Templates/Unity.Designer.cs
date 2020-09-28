
namespace FileStub.Templates
{
    partial class FileStubTemplateUnity
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
            this.lbExeTarget = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbParentExeDir = new System.Windows.Forms.CheckBox();
            this.pnTarget.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTemplateDescription
            // 
            this.lbTemplateDescription.AllowDrop = true;
            this.lbTemplateDescription.BackColor = System.Drawing.Color.Transparent;
            this.lbTemplateDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbTemplateDescription.ForeColor = System.Drawing.Color.White;
            this.lbTemplateDescription.Location = new System.Drawing.Point(12, 12);
            this.lbTemplateDescription.Name = "lbTemplateDescription";
            this.lbTemplateDescription.Padding = new System.Windows.Forms.Padding(16);
            this.lbTemplateDescription.Size = new System.Drawing.Size(178, 90);
            this.lbTemplateDescription.TabIndex = 38;
            this.lbTemplateDescription.Tag = "";
            this.lbTemplateDescription.Text = "_";
            this.lbTemplateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnTarget
            // 
            this.pnTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.pnTarget.Controls.Add(this.lbExeTarget);
            this.pnTarget.Location = new System.Drawing.Point(14, 164);
            this.pnTarget.Name = "pnTarget";
            this.pnTarget.Size = new System.Drawing.Size(203, 102);
            this.pnTarget.TabIndex = 39;
            this.pnTarget.Tag = "color:normal";
            // 
            // lbExeTarget
            // 
            this.lbExeTarget.AllowDrop = true;
            this.lbExeTarget.BackColor = System.Drawing.Color.Transparent;
            this.lbExeTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbExeTarget.ForeColor = System.Drawing.Color.White;
            this.lbExeTarget.Location = new System.Drawing.Point(12, 9);
            this.lbExeTarget.Name = "lbExeTarget";
            this.lbExeTarget.Padding = new System.Windows.Forms.Padding(8);
            this.lbExeTarget.Size = new System.Drawing.Size(178, 82);
            this.lbExeTarget.TabIndex = 39;
            this.lbExeTarget.Tag = "";
            this.lbExeTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbTemplateDescription);
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 114);
            this.panel1.TabIndex = 40;
            this.panel1.Tag = "color:normal";
            // 
            // cbParentExeDir
            // 
            this.cbParentExeDir.AutoSize = true;
            this.cbParentExeDir.ForeColor = System.Drawing.Color.White;
            this.cbParentExeDir.Location = new System.Drawing.Point(14, 141);
            this.cbParentExeDir.Name = "cbParentExeDir";
            this.cbParentExeDir.Size = new System.Drawing.Size(193, 17);
            this.cbParentExeDir.TabIndex = 146;
            this.cbParentExeDir.Text = "Use Exe\'s Parent Directory as Base";
            this.cbParentExeDir.UseVisualStyleBackColor = true;
            // 
            // FileStubTemplateUnity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.cbParentExeDir);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileStubTemplateUnity";
            this.Tag = "color:dark1";
            this.Text = "Unity";
            this.pnTarget.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbTemplateDescription;
        private System.Windows.Forms.Panel pnTarget;
        public System.Windows.Forms.Label lbExeTarget;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbParentExeDir;
    }
}
