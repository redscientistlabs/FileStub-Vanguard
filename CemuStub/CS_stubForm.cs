using RTCV.CorruptCore;
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
using Vanguard;

namespace FileStub
{
    public partial class CS_Core_Form : Form
    {

        public CS_Core_Form()
        {
            InitializeComponent();
            Text += FileWatch.CemuStubVersion;
        }

        private void BtnRestartStub_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            FileWatch.RestoreBackup();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Resetting the backup will take the current rpx and promote it to backup. Do you want to continue?", "Reset Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                FileWatch.ResetBackup();

            FileInterface.CompositeFilenameDico = new Dictionary<string, string>();
            FileInterface.SaveCompositeFilenameDico();
        }


        private void CS_Core_Form_Load(object sender, EventArgs e)
        {
            cbSelectedGame.SelectedIndex = 0;
            FileWatch.LoadKnownGames();
        }

        private void CbSelectedGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FileWatch.DontSelectGame)
                return;

            string selected = cbSelectedGame.SelectedItem.ToString();
            if (selected == "Autodetect")
            { 


                FileWatch.Start();
                return;
            }

            if(!FileWatch.SelectGame(selected))
            {
                cbSelectedGame.SelectedIndex = 0;
                return;
            }

            if(!VanguardCore.vanguardStarted)
                VanguardCore.Start();
            else if (VanguardCore.vanguardConnected)
                FileWatch.UpdateDomains();

        }

        private void BtnSettings_MouseDown(object sender, MouseEventArgs e)
        {

            Point locate = new Point(((Control)sender).Location.X + e.Location.X, ((Control)sender).Location.Y + e.Location.Y);

            FileInfo cemuExeFile = null;

            if (FileWatch.currentGameInfo != null && FileWatch.currentGameInfo.gameName != "Autodetect")
                cemuExeFile = FileWatch.currentGameInfo.cemuExeFile;
            else if (FileWatch.knownGamesDico.Values.Count > 0)
                cemuExeFile = FileWatch.knownGamesDico.Values.First().cemuExeFile;

            ContextMenuStrip loadMenuItems = new ContextMenuStrip();

            loadMenuItems.Items.Add("Start Cemu", null, new EventHandler((ob, ev) =>
            {
                FileWatch.StartCemu();
            })).Visible = (cemuExeFile != null);

            var startRpxItem = loadMenuItems.Items.Add("Manually start Rpx", null, new EventHandler((ob, ev) =>
            {
                FileWatch.StartRpx();
            }));

            startRpxItem.Visible = (cemuExeFile != null);
            startRpxItem.Enabled = FileWatch.InterfaceEnabled;


            loadMenuItems.Items.Add("Reconstruct fake update", null, new EventHandler((ob, ev) =>
            {
                FileWatch.PrepareUpdateFolder(true);
            })).Enabled = FileWatch.InterfaceEnabled;

            loadMenuItems.Items.Add("Change Cemu location", null, new EventHandler((ob, ev) =>
            {
                FileWatch.ChangeCemuLocation();
            })).Enabled = FileWatch.InterfaceEnabled;

            loadMenuItems.Items.Add(new ToolStripSeparator());

            loadMenuItems.Items.Add("Unmod selected Game", null, new EventHandler((ob, ev) =>
            {
                FileWatch.UnmodGame();
            })).Enabled = FileWatch.InterfaceEnabled;

            loadMenuItems.Show(this, locate);
        }
    }
}
