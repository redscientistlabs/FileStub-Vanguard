namespace FileStub
{
    partial class SelectMultipleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMultipleForm));
            this.lbMultipleFiles = new System.Windows.Forms.ListBox();
            this.btnSendList = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.cbLoadAnything = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbMultipleFiles
            // 
            this.lbMultipleFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMultipleFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbMultipleFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbMultipleFiles.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbMultipleFiles.ForeColor = System.Drawing.Color.White;
            this.lbMultipleFiles.FormattingEnabled = true;
            this.lbMultipleFiles.Location = new System.Drawing.Point(12, 12);
            this.lbMultipleFiles.Name = "lbMultipleFiles";
            this.lbMultipleFiles.ScrollAlwaysVisible = true;
            this.lbMultipleFiles.Size = new System.Drawing.Size(290, 429);
            this.lbMultipleFiles.TabIndex = 0;
            // 
            // btnSendList
            // 
            this.btnSendList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnSendList.FlatAppearance.BorderSize = 0;
            this.btnSendList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSendList.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnSendList.Location = new System.Drawing.Point(105, 509);
            this.btnSendList.Name = "btnSendList";
            this.btnSendList.Size = new System.Drawing.Size(197, 23);
            this.btnSendList.TabIndex = 1;
            this.btnSendList.Text = "Load Multiple Files in FileStub";
            this.btnSendList.UseVisualStyleBackColor = false;
            this.btnSendList.Click += new System.EventHandler(this.btnSendList_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 509);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.BackColor = System.Drawing.Color.Gray;
            this.btnAddFiles.FlatAppearance.BorderSize = 0;
            this.btnAddFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFiles.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAddFiles.ForeColor = System.Drawing.Color.White;
            this.btnAddFiles.Location = new System.Drawing.Point(12, 451);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(66, 23);
            this.btnAddFiles.TabIndex = 3;
            this.btnAddFiles.Text = "Add File(s)";
            this.btnAddFiles.UseVisualStyleBackColor = false;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.BackColor = System.Drawing.Color.Gray;
            this.btnRemoveSelected.FlatAppearance.BorderSize = 0;
            this.btnRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelected.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRemoveSelected.ForeColor = System.Drawing.Color.White;
            this.btnRemoveSelected.Location = new System.Drawing.Point(157, 451);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(101, 23);
            this.btnRemoveSelected.TabIndex = 4;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = false;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.BackColor = System.Drawing.Color.Gray;
            this.btnClearList.FlatAppearance.BorderSize = 0;
            this.btnClearList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearList.ForeColor = System.Drawing.Color.White;
            this.btnClearList.Location = new System.Drawing.Point(260, 451);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(42, 23);
            this.btnClearList.TabIndex = 5;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = false;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.BackColor = System.Drawing.Color.Gray;
            this.btnLoadFile.FlatAppearance.BorderSize = 0;
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLoadFile.ForeColor = System.Drawing.Color.White;
            this.btnLoadFile.Location = new System.Drawing.Point(12, 480);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(143, 23);
            this.btnLoadFile.TabIndex = 6;
            this.btnLoadFile.Text = "Load list from File";
            this.btnLoadFile.UseVisualStyleBackColor = false;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.BackColor = System.Drawing.Color.Gray;
            this.btnSaveFile.FlatAppearance.BorderSize = 0;
            this.btnSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSaveFile.ForeColor = System.Drawing.Color.White;
            this.btnSaveFile.Location = new System.Drawing.Point(157, 480);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(145, 23);
            this.btnSaveFile.TabIndex = 7;
            this.btnSaveFile.Text = "Save list to File";
            this.btnSaveFile.UseVisualStyleBackColor = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.BackColor = System.Drawing.Color.Gray;
            this.btnAddFolder.FlatAppearance.BorderSize = 0;
            this.btnAddFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFolder.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAddFolder.ForeColor = System.Drawing.Color.White;
            this.btnAddFolder.Location = new System.Drawing.Point(81, 451);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(74, 23);
            this.btnAddFolder.TabIndex = 8;
            this.btnAddFolder.Text = "Add Folder";
            this.btnAddFolder.UseVisualStyleBackColor = false;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // cbLoadAnything
            // 
            this.cbLoadAnything.AutoSize = true;
            this.cbLoadAnything.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbLoadAnything.ForeColor = System.Drawing.Color.White;
            this.cbLoadAnything.Location = new System.Drawing.Point(12, 543);
            this.cbLoadAnything.Name = "cbLoadAnything";
            this.cbLoadAnything.Size = new System.Drawing.Size(219, 17);
            this.cbLoadAnything.TabIndex = 9;
            this.cbLoadAnything.Text = "Ignore loading errors (Load anything)";
            this.cbLoadAnything.UseVisualStyleBackColor = true;
            this.cbLoadAnything.CheckedChanged += new System.EventHandler(this.CbLoadAnything_CheckedChanged);
            // 
            // SelectMultipleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(314, 571);
            this.Controls.Add(this.cbLoadAnything);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendList);
            this.Controls.Add(this.lbMultipleFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectMultipleForm";
            this.Text = "Select Multiple Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFV_SelectMultiple_FormClosing);
            this.Load += new System.EventHandler(this.WFV_SelectMultiple_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMultipleFiles;
        private System.Windows.Forms.Button btnSendList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.CheckBox cbLoadAnything;
    }
}
