using Newtonsoft.Json;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.NetCore.StaticTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;
using FileStub;

namespace FileStub
{
    public static class FileWatch
    {
        static Timer watch = null;
        public static string FileStubVersion = "0.01";
        public static string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static FileStubFileInfo currentFileInfo = new FileStubFileInfo();

        public static bool stubInterfaceEnabled = false;
        public static ProgressForm progressForm;




        public static void Start()
        {
            if (watch != null)
            {
                watch.Stop();
                watch = null;
            }

            if (VanguardCore.vanguardConnected)
                RemoveDomains();

            FileWatch.currentFileInfo = new FileStubFileInfo();

            DisableInterface();
            //state = TargetType.UNFOUND;


            string tempPath = Path.Combine(FileWatch.currentDir, "TEMP");
            string temp2Path = Path.Combine(FileWatch.currentDir, "TEMP2");
            string paramsPath = Path.Combine(FileWatch.currentDir, "PARAMS");

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            if (!Directory.Exists(temp2Path))
                Directory.CreateDirectory(temp2Path);

            if (!Directory.Exists(paramsPath))
                Directory.CreateDirectory(paramsPath);

            string disclaimerPath = Path.Combine(currentDir, "LICENSES", "DISCLAIMER.TXT");
            string disclaimerReadPath = Path.Combine(currentDir, "PARAMS", "DISCLAIMERREAD");

            if (File.Exists(disclaimerPath) && !File.Exists(disclaimerReadPath))
            {
                MessageBox.Show(File.ReadAllText(disclaimerPath).Replace("[ver]", FileWatch.FileStubVersion), "Cemu Stub", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Create(disclaimerReadPath);
            }

            //If we can't load the dictionary, quit the wgh to prevent the loss of backups
            if (!LoadCompositeFilenameDico())
                Application.Exit();

        }

        private static void RemoveDomains()
        {
            if(currentFileInfo.targetInterface != null)
            {
                currentFileInfo.targetInterface.CloseStream();
                currentFileInfo.targetInterface = null;
            }

            UpdateDomains();
        }

        public static void RestoreTarget()
        {
            if (FileWatch.currentFileInfo.AutoUncorrupt)
            {
                if (FileWatch.currentFileInfo.lastBlastLayerBackup != null)
                    FileWatch.currentFileInfo.lastBlastLayerBackup.Apply(false);
                else
                {
                    //CHECK CRC WITH BACKUP HERE AND SKIP BACKUP IF WORKING FILE = BACKUP FILE
                    FileWatch.currentFileInfo.targetInterface.ResetWorkingFile();
                }
            }
            else
            {
                FileWatch.currentFileInfo.targetInterface.ResetWorkingFile();
            }
        }

        internal static bool LoadTarget()
        {
            if(currentFileInfo.selectedTargetType == TargetType.SINGLE_FILE)
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

                string targetId = "File|" + filename;

                if (currentFileInfo.targetInterface != null)
                {
                    RestoreTarget();
                    currentFileInfo.targetInterface.CloseStream();
                }

                //Disable caching of the previously loaded file if it was enabled
                /*
                if (ghForm.btnEnableCaching.Text.Contains("Disable"))
                    ghForm.btnEnableCaching.PerformClick();

                if (currentMemoryInterface != null && (currentTargetType == "Dolphin" || currentTargetType == "File" || currentTargetType == "MultipleFiles"))
                {
                    WGH_Core.RestoreTarget();
                    currentMemoryInterface.stream?.Dispose();
                }
                */

                FileInterface fi = null;

                Action<object, EventArgs> action = (ob, ea) =>
                {
                    fi = new FileInterface(targetId);
                };

                Action<object, EventArgs> postAction = (ob, ea) =>
                {
                    if (fi == null || fi.lastMemorySize == null)
                    {
                        MessageBox.Show("Failed to load target");
                        S.GET<MainForm>().DisableInterface();
                        return;
                    }

                    FileWatch.currentFileInfo.targetShortName = fi.ShortFilename;
                    FileWatch.currentFileInfo.targetFullName = fi.Filename;

                    FileWatch.currentFileInfo.targetInterface = fi;
                    S.GET<MainForm>().lbTarget.Text = targetId + "|MemorySize:" + fi.lastMemorySize.ToString();

                    if(VanguardCore.vanguardConnected)
                        UpdateDomains();

                    //Refresh the UI
                    //RefreshUIPostLoad();
                };

                S.GET<MainForm>().RunProgressBar($"Loading target...", 0, action, postAction);

            }
            else //MULTIPLE_FILE
            {
                switch(currentFileInfo.selectedTargetType)
                {
                    case TargetType.MULTIPLE_FILE_SINGLEDOMAIN:
                        FileInterface.identity = FileInterfaceIdentity.SELF_DESCRIBE;
                            break;
                    case TargetType.MULTIPLE_FILE_MULTIDOMAIN:
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




                var mfi = (MultipleFileInterface)FileWatch.currentFileInfo.targetInterface;
                //currentTargetName = mfi.ShortFilename;
                S.GET<MainForm>().lbTarget.Text = mfi.ShortFilename + "|MemorySize:" + mfi.lastMemorySize.ToString();

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
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //Thread.Sleep(300);
                }
        }

        internal static void CloseTarget()
        {
            currentFileInfo.targetInterface?.CloseStream();
            currentFileInfo.targetInterface = null;
            UpdateDomains();
        }

        public static int IndexOf<T>(this T[] haystack, T[] needle)
        {
            if ((needle != null) && (haystack.Length >= needle.Length))
            {
                for (int l = 0; l < haystack.Length - needle.Length + 1; l++)
                {
                    if (!needle.Where((data, index) => !haystack[l + index].Equals(data)).Any())
                    {
                        return l;
                    }
                }
            }

            return -1;
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
                gameDone[VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS] = new string[0];
                gameDone[VSPEC.MEMORYDOMAINS_INTERFACES] = GetInterfaces();
                gameDone[VSPEC.CORE_DISKBASED] = false;
                AllSpec.VanguardSpec.Update(gameDone);

                //This is local. If the domains changed it propgates over netcore
                LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_EVENT_DOMAINSUPDATED, true, true);

                //Asks RTC to restrict any features unsupported by the stub
                LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_EVENT_RESTRICTFEATURES, true, true);

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
                    return new MemoryDomainProxy[] { };
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


                return interfaces.ToArray();
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex, true) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();

                return new MemoryDomainProxy[] { };
            }

        }


        public static bool LoadCompositeFilenameDico()
        {
            JsonSerializer serializer = new JsonSerializer();
            string filemapPath = Path.Combine(FileWatch.currentDir, "TEMP", "filemap.json");
            if (!File.Exists(filemapPath))
            {
                FileInterface.CompositeFilenameDico = new Dictionary<string, string>();
                return true;
            }
            try
            {

                using (StreamReader sw = new StreamReader(filemapPath))
                using (JsonTextReader reader = new JsonTextReader(sw))
                {
                    FileInterface.CompositeFilenameDico = serializer.Deserialize<Dictionary<string, string>>(reader);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the filemap! Figure out what's locking it and then restart the WGH.\n" + e.ToString());
                return false;
            }
            return true;
        }


        public static void EnableInterface()
        {
            S.GET<MainForm>().btnResetBackup.Enabled = true;
            S.GET<MainForm>().btnRestoreBackup.Enabled = true;
            stubInterfaceEnabled = true;
        }

        public static void DisableInterface()
        {
            S.GET<MainForm>().btnResetBackup.Enabled = false;
            S.GET<MainForm>().btnRestoreBackup.Enabled = false;
            stubInterfaceEnabled = false;
        }

    }


}
