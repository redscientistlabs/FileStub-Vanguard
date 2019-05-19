using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using Ceras;


namespace FileStub
{

    public class CemuGameInfo
    {
        public FileInfo gameRpxFileInfo = null;
        public FileInfo cemuExeFile = null;
        public FileInfo[] updateCodeFiles = null;
        public DirectoryInfo gameSaveFolder = null;
        public string rpxFile = null;
        public string gameRpxPath = null;
        public string updateRpxPath = null;
        public string updateCodePath = null;
        public string updateMetaPath = null;
        public string updateRpxLocation = null;
        public string updateRpxCompressed = null;
        public string updateRpxBackup = null;
        public string FirstID = null;
        public string SecondID = null;
        public string fileInterfaceTargetId = null;
        public string gameName = "Autodetect";
        public string updateRpxUncompressedToken = null;

        public override string ToString()
        {
            return gameName;
        }
    }

    public static class ExecutionType
    {
        public const string EXECUTE_CORRUPTED_FILE = "Execute corrupted file";
        public const string EXECUTE_WITH = "Execute with";
        public const string EXECUTE_OTHER_PROGRAM = "Execute other program";
        public const string NO_EXECUTION = "No execution";
        public const string SCRIPT = "Script";
    }

    public static class TargetType
    {
        public const string SINGLE_FILE = "Single File";
        public const string MULTIPLE_FILE_SINGLEDOMAIN = "Multiple files as one domain";
        public const string MULTIPLE_FILE_MULTIDOMAIN = "Multiple files as many domains";
    }
    public class LabelPassthrough : Label
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, this.Text.ToString(), this.Font, ClientRectangle, ForeColor);
        }

    }

    public class RefreshingListBox : ListBox
    {
        public void RefreshItemsReal()
        {
            base.RefreshItems();
        }
    }

    public class MenuButton : Button
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        public void SetMenu(ContextMenuStrip _menu)
        {
            Menu = _menu;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Menu != null && mevent.Button == MouseButtons.Left)
            {
                Menu.Show(this, mevent.Location);
            }
        }

    }


}
