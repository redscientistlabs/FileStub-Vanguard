namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FileStub;
    using Newtonsoft.Json;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.NetCore;
    using Vanguard;

    public static class FileWatch
    {
        internal static string FileStubVersion = "0.1.9";
        internal static string currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        internal static FileStubFileInfo currentFileInfo = new FileStubFileInfo();

        internal static ProgressForm progressForm;

        public static void Start()
        {
            RTCV.Common.Logging.StartLogging(VanguardCore.logPath);
            if (VanguardCore.vanguardConnected)
                RemoveDomains();

            //FileWatch.currentFileInfo = new FileStubFileInfo();

            DisableInterface();
            //state = TargetType.UNFOUND;

            RtcCore.EmuDirOverride = true; //allows the use of this value before vanguard is connected

            string backupPath = Path.Combine(FileWatch.currentDir, "FILEBACKUPS");
            string paramsPath = Path.Combine(FileWatch.currentDir, "PARAMS");

            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);

            if (!Directory.Exists(paramsPath))
                Directory.CreateDirectory(paramsPath);

            string disclaimerPath = Path.Combine(currentDir, "LICENSES", "DISCLAIMER.TXT");
            string disclaimerReadPath = Path.Combine(currentDir, "PARAMS", "DISCLAIMERREAD");

            if (File.Exists(disclaimerPath) && !File.Exists(disclaimerReadPath))
            {
                MessageBox.Show(File.ReadAllText(disclaimerPath).Replace("[ver]", FileWatch.FileStubVersion), "File Stub", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Create(disclaimerReadPath);
            }

            //If we can't load the dictionary, quit the wgh to prevent the loss of backups
            if (!FileInterface.LoadCompositeFilenameDico(FileWatch.currentDir))
                Application.Exit();
        }

        private static void RemoveDomains()
        {
            if (currentFileInfo.targetInterface != null)
            {
                currentFileInfo.targetInterface.CloseStream();
                currentFileInfo.targetInterface = null;
            }

            UpdateDomains();
        }

        public static bool RestoreTarget()
        {
            bool success = false;
            if (currentFileInfo.autoUncorrupt)
            {
                if (StockpileManagerEmuSide.UnCorruptBL != null)
                {
                    StockpileManagerEmuSide.UnCorruptBL.Apply(false);
                    success = true;
                }
                else
                {
                    //CHECK CRC WITH BACKUP HERE AND SKIP BACKUP IF WORKING FILE = BACKUP FILE
                   success = currentFileInfo.targetInterface.ResetWorkingFile();
                }
            }
            else
            {
                success = currentFileInfo.targetInterface.ResetWorkingFile();
            }
            return success;
        }

        internal static bool LoadTargets()
        {
            var targets = S.GET<StubForm>().lbTargets;
            if (targets.Items.Count == 0)
            {
                MessageBox.Show("No targets scheduled for loading. Aborting loading.");
                return false;
            }


            if (currentFileInfo.selectedTargetType == TargetType.SINGLE_FILE)
            {
                FileInterface.identity = FileInterfaceIdentity.SELF_DESCRIBE;

                var target = (FileTarget)targets.Items[0];
                string filename = targets.Items[0].ToString();



                string targetId = "File|" + filename;

                CloseTarget(false);

                FileInterface fi = null;

                Action<object, EventArgs> action = (ob, ea) =>
                {
                    fi = new FileInterface(
                        targetId,
                        bigEndian: FileWatch.currentFileInfo.bigEndian,
                        useAutomaticFileBackups: true,
                        startPadding: target.PaddingHeader,
                        endPadding: target.PaddingFooter);


                    if (FileWatch.currentFileInfo.useCacheAndMultithread)
                        fi.getMemoryDump();
                };

                Action<object, EventArgs> postAction = (ob, ea) =>
                {
                    if (fi == null || fi.lastMemorySize == null)
                    {
                        MessageBox.Show("Failed to load target");
                        S.GET<StubForm>().DisableTargetInterface();
                        return;
                    }

                    FileWatch.currentFileInfo.targetShortName = fi.ShortFilename;
                    FileWatch.currentFileInfo.targetFullName = fi.Filename;

                    FileWatch.currentFileInfo.targetInterface = fi;
                    S.GET<StubForm>().lbTarget.Text = targetId + "|MemorySize:" + fi.lastMemorySize.ToString();

                    if (VanguardCore.vanguardConnected)
                        UpdateDomains();

                    //Refresh the UI
                    //RefreshUIPostLoad();
                };

                S.GET<StubForm>().RunProgressBar($"Loading target...", 0, action, postAction);
            }
            else //MULTIPLE_FILE
            {
                switch (currentFileInfo.selectedTargetType)
                {
                    case TargetType.MULTIPLE_FILE_SINGLEDOMAIN:
                        FileInterface.identity = FileInterfaceIdentity.SELF_DESCRIBE;
                        break;
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN:
                    default:
                        FileInterface.identity = FileInterfaceIdentity.HASHED_PREFIX;
                        break;
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN_FULLPATH:
                        FileInterface.identity = FileInterfaceIdentity.FULL_PATH;
                        break;
                }

                var targetLoaders = targets.Items.Cast<FileTarget>().ToArray();

                var mfi = new MultipleFileInterface(targetLoaders, FileWatch.currentFileInfo.bigEndian, FileWatch.currentFileInfo.useAutomaticBackups);

                if (FileWatch.currentFileInfo.useCacheAndMultithread)
                    mfi.getMemoryDump();

                FileWatch.currentFileInfo.targetInterface = mfi;

                if (VanguardCore.vanguardConnected)
                    FileWatch.UpdateDomains();


                //currentTargetName = mfi.ShortFilename;
                S.GET<StubForm>().lbTarget.Text = mfi.ShortFilename + "|MemorySize:" + mfi.lastMemorySize.ToString();
                StockpileManagerEmuSide.UnCorruptBL = null;
            }

            return true;
        }

        internal static bool InsertTargets()
        {
            if (currentFileInfo.selectedTargetType == TargetType.SINGLE_FILE)
            {
                FileInterface.identity = FileInterfaceIdentity.SELF_DESCRIBE;

                string filename = null;

                OpenFileDialog OpenFileDialog1;
                OpenFileDialog1 = new OpenFileDialog();

                OpenFileDialog1.Title = "Open File";
                OpenFileDialog1.Filter = "files|*.*";
                OpenFileDialog1.RestoreDirectory = true;
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (OpenFileDialog1.FileName.ToString().Contains('^'))
                    {
                        MessageBox.Show("You can't use a file that contains the character ^ ");
                        return false;
                    }

                    filename = OpenFileDialog1.FileName;
                }
                else
                    return false;

                var target = Vault.RequestFileTarget(filename);
                target.IsMain = true;
                //var target = new TargetLoader(filename, true);

                //here we make target objects
                S.GET<StubForm>().lbTargets.Items.Clear();
                S.GET<StubForm>().lbTargets.Items.Add(target);

            }
            else //MULTIPLE_FILE
            {
                switch (currentFileInfo.selectedTargetType)
                {
                    case TargetType.MULTIPLE_FILE_SINGLEDOMAIN:
                        FileInterface.identity = FileInterfaceIdentity.SELF_DESCRIBE;
                        break;
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN:
                    default:
                        FileInterface.identity = FileInterfaceIdentity.HASHED_PREFIX;
                        break;
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN_FULLPATH:
                        FileInterface.identity = FileInterfaceIdentity.FULL_PATH;
                        break;
                }

                S.GET<SelectMultipleForm>().Close();
                var smForm = new SelectMultipleForm();
                S.SET(smForm);

                if (smForm.ShowDialog() != DialogResult.OK)
                    return false;

                //var mfi = (MultipleFileInterface)FileWatch.currentFileInfo.targetInterface;
                ////currentTargetName = mfi.ShortFilename;
                //S.GET<StubForm>().lbTarget.Text = mfi.ShortFilename + "|MemorySize:" + mfi.lastMemorySize.ToString();
                //StockpileManagerEmuSide.UnCorruptBL = null;
            }

            return true;
        }


        internal static void KillProcess()
        {
            if (currentFileInfo.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM ||
                currentFileInfo.selectedExecution == ExecutionType.EXECUTE_WITH ||
                currentFileInfo.selectedExecution == ExecutionType.EXECUTE_CORRUPTED_FILE)
                if (currentFileInfo.TerminateBeforeExecution && Executor.otherProgram != null)
                {
                    string otherProgramShortFilename = Path.GetFileName(Executor.otherProgram);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "taskkill";
                    startInfo.Arguments = $"/IM \"{otherProgramShortFilename}\"";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    Process processTemp = new Process();
                    processTemp.StartInfo = startInfo;
                    processTemp.EnableRaisingEvents = true;
                    try
                    {
                        processTemp.Start();
                        processTemp.WaitForExit();
                        Thread.Sleep(500); //Add an artificial delay as sometimes handles don't release right away
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    //Thread.Sleep(300);
                }
        }

        internal static bool CloseTarget(bool updateDomains = true)
        {
            if (FileWatch.currentFileInfo.targetInterface != null)
            {
                if (!FileWatch.RestoreTarget())
                {
                    MessageBox.Show("Unable to restore the backup. Aborting!");
                    return false;
                }
                FileWatch.currentFileInfo.targetInterface.CloseStream();
                FileWatch.currentFileInfo.targetInterface = null;
            }

            if (updateDomains)
                UpdateDomains();
            return true;
        }

        public static void UpdateDomains()
        {
            try
            {
                PartialSpec gameDone = new PartialSpec("VanguardSpec");
                gameDone[VSPEC.SYSTEM] = "FileSystem";
                gameDone[VSPEC.GAMENAME] = FileWatch.currentFileInfo.targetShortName;
                gameDone[VSPEC.SYSTEMPREFIX] = "FileStub";
                gameDone[VSPEC.SYSTEMCORE] = "FileStub";
                //gameDone[VSPEC.SYNCSETTINGS] = BIZHAWK_GETSET_SYNCSETTINGS;
                gameDone[VSPEC.OPENROMFILENAME] = currentFileInfo.targetFullName;
                gameDone[VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS] = Array.Empty<string>();
                gameDone[VSPEC.MEMORYDOMAINS_INTERFACES] = GetInterfaces();
                gameDone[VSPEC.CORE_DISKBASED] = false;
                AllSpec.VanguardSpec.Update(gameDone);

                //This is local. If the domains changed it propgates over netcore
                LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.EventDomainsUpdated, true, true);

                //Asks RTC to restrict any features unsupported by the stub
                LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.EventRestrictFeatures, true, true);
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();
            }
        }

        public static MemoryDomainProxy[] GetInterfaces()
        {
            try
            {
                Console.WriteLine($" getInterfaces()");
                if (currentFileInfo.targetInterface == null)
                {
                    Console.WriteLine($"rpxInterface was null!");
                    return Array.Empty<MemoryDomainProxy>();
                }

                List<MemoryDomainProxy> interfaces = new List<MemoryDomainProxy>();

                switch (currentFileInfo.selectedTargetType)
                {   //Checking if the FileInterface/MultiFileInterface is split in sub FileInterfaces
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN:
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN_FULLPATH:
                        foreach (var fi in (currentFileInfo.targetInterface as MultipleFileInterface).FileInterfaces)
                            interfaces.Add(new MemoryDomainProxy(fi));
                        break;
                    case TargetType.SINGLE_FILE:
                    case TargetType.MULTIPLE_FILE_SINGLEDOMAIN:
                    default:
                        interfaces.Add(new MemoryDomainProxy(currentFileInfo.targetInterface));
                        break;
                }

                foreach (MemoryDomainProxy mdp in interfaces)
                    mdp.BigEndian = currentFileInfo.bigEndian;

                return interfaces.ToArray();
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex, true) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();

                return Array.Empty<MemoryDomainProxy>();
            }
        }

        public static void EnableInterface()
        {
            S.GET<StubForm>().btnResetBackup.Enabled = true;
            S.GET<StubForm>().btnRestoreBackup.Enabled = true;
        }

        public static void DisableInterface()
        {
            S.GET<StubForm>().btnResetBackup.Enabled = false;
            S.GET<StubForm>().btnRestoreBackup.Enabled = false;
        }
    }
}
