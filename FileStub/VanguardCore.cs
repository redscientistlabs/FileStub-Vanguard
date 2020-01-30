using FileStub;
using RTCV.CorruptCore;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.Vanguard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Vanguard
{
    public static class VanguardCore
    {
        public static string[] args;
        public static bool vanguardStarted = false;
        public static bool vanguardConnected => (VanguardImplementation.connector != null ? VanguardImplementation.connector.netcoreStatus == NetworkStatus.CONNECTED : false);

        internal static DialogResult ShowErrorDialog(Exception exception, bool canContinue = false)
        {
            return new RTCV.NetCore.CloudDebug(exception, canContinue).Start();
        }


        /// <summary>
        /// Global exceptions in Non User Interfarce(other thread) antipicated error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Form error = new RTCV.NetCore.CloudDebug(ex);
            var result = error.ShowDialog();

        }

        /// <summary>
        /// Global exceptions in User Interfarce antipicated error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Form error = new RTCV.NetCore.CloudDebug(ex);
            var result = error.ShowDialog();

            Form loaderObject = (sender as Form);

            if (result == DialogResult.Abort)
            {
                if (loaderObject != null)
                    RTCV.NetCore.SyncObjectSingleton.SyncObjectExecute(loaderObject, (o, ea) =>
                    {
                        loaderObject.Close();
                    });
            }
        }

        public static bool attached = false;

        public static string System
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.SYSTEM];
            set => AllSpec.VanguardSpec.Update(VSPEC.SYSTEM, value);
        }
        public static string GameName
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.GAMENAME];
            set => AllSpec.VanguardSpec.Update(VSPEC.GAMENAME, value);
        }
        public static string SystemPrefix
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.SYSTEMPREFIX];
            set => AllSpec.VanguardSpec.Update(VSPEC.SYSTEMPREFIX, value);
        }
        public static string SystemCore
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.SYSTEMCORE];
            set => AllSpec.VanguardSpec.Update(VSPEC.SYSTEMCORE, value);
        }
        public static string SyncSettings
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.SYNCSETTINGS];
            set => AllSpec.VanguardSpec.Update(VSPEC.SYNCSETTINGS, value);
        }
        public static string OpenRomFilename
        {
            get => (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
            set => AllSpec.VanguardSpec.Update(VSPEC.OPENROMFILENAME, value);
        }
        public static int LastLoaderRom
        {
            get => (int)AllSpec.VanguardSpec[VSPEC.CORE_LASTLOADERROM];
            set => AllSpec.VanguardSpec.Update(VSPEC.CORE_LASTLOADERROM, value);
        }
        public static string[] BlacklistedDomains
        {
            get => (string[])AllSpec.VanguardSpec[VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS];
            set => AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS, value);
        }
        public static MemoryDomainProxy[] MemoryInterfacees
        {
            get => (MemoryDomainProxy[])AllSpec.VanguardSpec[VSPEC.MEMORYDOMAINS_INTERFACES];
            set => AllSpec.VanguardSpec.Update(VSPEC.MEMORYDOMAINS_INTERFACES, value);
        }

        public static string emuDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string logPath = Path.Combine(emuDir, "EMU_LOG.txt");


        public static PartialSpec getDefaultPartial()
        {
            var partial = new PartialSpec("VanguardSpec");

            partial[VSPEC.NAME] = "FileStub";
            partial[VSPEC.SYSTEM] = "FileSystem";
            partial[VSPEC.GAMENAME] = String.Empty;
            partial[VSPEC.SYSTEMPREFIX] = String.Empty;
            partial[VSPEC.OPENROMFILENAME] = String.Empty;
            partial[VSPEC.SYNCSETTINGS] = String.Empty;
            partial[VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS] = new string[] { };
            partial[VSPEC.MEMORYDOMAINS_INTERFACES] = new MemoryDomainProxy[] { };
            partial[VSPEC.CORE_LASTLOADERROM] = -1;
            partial[VSPEC.SUPPORTS_RENDERING] = false;
            partial[VSPEC.SUPPORTS_CONFIG_MANAGEMENT] = false;
            partial[VSPEC.SUPPORTS_CONFIG_HANDOFF] = false;
            partial[VSPEC.SUPPORTS_SAVESTATES] = false;
            partial[VSPEC.SUPPORTS_REFERENCES] = false;
            partial[VSPEC.OVERRIDE_DEFAULTMAXINTENSITY] = 655350;
            partial[VSPEC.SUPPORTS_GAMEPROTECTION] = false;
            partial[VSPEC.SUPPORTS_REALTIME] = false;
            partial[VSPEC.SUPPORTS_KILLSWITCH] = false;
            partial[VSPEC.SUPPORTS_MIXED_STOCKPILE] = false;
            partial[VSPEC.REPLACE_MANUALBLAST_WITH_GHCORRUPT] = true;
            partial[VSPEC.EMUDIR] = emuDir;

            if(FileWatch.currentFileInfo.useCacheAndMultithread)
                partial[VSPEC.SUPPORTS_MULTITHREAD] = true;

            //partial[VSPEC.CONFIG_PATHS] = new[] { Path.Combine(emuDir, "config.ini") };

            return partial;
        }

        public static void RegisterVanguardSpec()
        {
            PartialSpec emuSpecTemplate = new PartialSpec("VanguardSpec");

            emuSpecTemplate.Insert(VanguardCore.getDefaultPartial());

            AllSpec.VanguardSpec = new FullSpec(emuSpecTemplate, !RtcCore.Attached); //You have to feed a partial spec as a template

            if (VanguardCore.attached)
                RTCV.Vanguard.VanguardConnector.PushVanguardSpecRef(AllSpec.VanguardSpec);

            LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_PUSHVANGUARDSPEC, emuSpecTemplate, true);
            LocalNetCoreRouter.Route(NetcoreCommands.UI, NetcoreCommands.REMOTE_PUSHVANGUARDSPEC, emuSpecTemplate, true);


            AllSpec.VanguardSpec.SpecUpdated += (o, e) =>
            {
                PartialSpec partial = e.partialSpec;


                LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_PUSHVANGUARDSPECUPDATE, partial, true);
                LocalNetCoreRouter.Route(NetcoreCommands.UI, NetcoreCommands.REMOTE_PUSHVANGUARDSPECUPDATE, partial, true);
            };
        }

        //This is the entry point of RTC. Without this method, nothing will load.
        
        public static void Start()
        {

            vanguardStarted = true;

            //Grab an object on the main thread to use for netcore invokes
            SyncObjectSingleton.SyncObject = S.GET<StubForm>();
            SyncObjectSingleton.EmuThreadIsMainThread = true;

            //Start everything
            VanguardImplementation.StartClient();
            VanguardCore.RegisterVanguardSpec();
            RtcCore.StartEmuSide();

            //Refocus on Bizhawk
            S.GET<StubForm>().Focus();

            //If it's attached, lie to vanguard
            if (VanguardCore.attached)
                VanguardConnector.ImplyClientConnected();
        }


    }
}
