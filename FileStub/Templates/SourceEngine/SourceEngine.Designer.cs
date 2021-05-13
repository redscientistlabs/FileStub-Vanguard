
namespace FileStub.Templates
{
    partial class FileStubTemplaceSource
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
            this.lbGameTarget = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.commandLineBox = new System.Windows.Forms.TextBox();
            this.commandlineargsdesc = new System.Windows.Forms.Label();
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
            this.lbTemplateDescription.Size = new System.Drawing.Size(276, 141);
            this.lbTemplateDescription.TabIndex = 38;
            this.lbTemplateDescription.Tag = "";
            this.lbTemplateDescription.Text = "_";
            this.lbTemplateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnTarget
            // 
            this.pnTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.pnTarget.Controls.Add(this.commandlineargsdesc);
            this.pnTarget.Controls.Add(this.commandLineBox);
            this.pnTarget.Controls.Add(this.lbGameTarget);
            this.pnTarget.Location = new System.Drawing.Point(14, 164);
            this.pnTarget.Name = "pnTarget";
            this.pnTarget.Size = new System.Drawing.Size(274, 102);
            this.pnTarget.TabIndex = 39;
            this.pnTarget.Tag = "color:normal";
            // 
            // lbGameTarget
            // 
            this.lbGameTarget.AllowDrop = true;
            this.lbGameTarget.BackColor = System.Drawing.Color.Transparent;
            this.lbGameTarget.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbGameTarget.ForeColor = System.Drawing.Color.White;
            this.lbGameTarget.Location = new System.Drawing.Point(12, 9);
            this.lbGameTarget.Name = "lbGameTarget";
            this.lbGameTarget.Padding = new System.Windows.Forms.Padding(8);
            this.lbGameTarget.Size = new System.Drawing.Size(121, 82);
            this.lbGameTarget.TabIndex = 39;
            this.lbGameTarget.Tag = "";
            this.lbGameTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.panel1.Controls.Add(this.lbTemplateDescription);
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 141);
            this.panel1.TabIndex = 40;
            this.panel1.Tag = "color:normal";
            // 
            // commandLineBox
            // 
            this.commandLineBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.commandLineBox.ForeColor = System.Drawing.SystemColors.Window;
            this.commandLineBox.Location = new System.Drawing.Point(139, 9);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(132, 20);
            this.commandLineBox.TabIndex = 40;
            // 
            // commandlineargsdesc
            // 
            this.commandlineargsdesc.AllowDrop = true;
            this.commandlineargsdesc.BackColor = System.Drawing.Color.Transparent;
            this.commandlineargsdesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.commandlineargsdesc.ForeColor = System.Drawing.Color.White;
            this.commandlineargsdesc.Location = new System.Drawing.Point(139, 32);
            this.commandlineargsdesc.Name = "commandlineargsdesc";
            this.commandlineargsdesc.Padding = new System.Windows.Forms.Padding(16);
            this.commandlineargsdesc.Size = new System.Drawing.Size(132, 59);
            this.commandlineargsdesc.TabIndex = 39;
            this.commandlineargsdesc.Tag = "";
            this.commandlineargsdesc.Text = "Command Line Args";
            this.commandlineargsdesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FileStubTemplaceSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileStubTemplaceSource";
            this.Tag = "color:dark1";
            this.Text = "Unity";
            this.pnTarget.ResumeLayout(false);
            this.pnTarget.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbTemplateDescription;
        private System.Windows.Forms.Panel pnTarget;
        public System.Windows.Forms.Label lbGameTarget;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label commandlineargsdesc;
        private System.Windows.Forms.TextBox commandLineBox;
    }
}
