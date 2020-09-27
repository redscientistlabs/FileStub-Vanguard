namespace Vanguard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FileStub;
    using RTCV;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.NetCore;

    public static class VanguardImplementation
    {
        internal static RTCV.Vanguard.VanguardConnector connector = null;

        public static void StartClient()
        {
            try
            {
                ConsoleEx.WriteLine("Starting Vanguard Client");
                Thread.Sleep(500); //When starting in Multiple Startup Project, the first try will be uncessful since
                                   //the server takes a bit more time to start then the client.

                var spec = new NetCoreReceiver();
                spec.Attached = VanguardCore.attached;
                spec.MessageReceived += OnMessageReceived;

                connector = new RTCV.Vanguard.VanguardConnector(spec);
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex, true) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();
            }
        }

        public static void RestartClient()
        {
            connector?.Kill();
            connector = null;
            StartClient();
        }

        private static void OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            try
            {
                // This is where you implement interaction.
                // Warning: Any error thrown in here will be caught by NetCore and handled by being displayed in the console.

                var message = e.message;
                var simpleMessage = message as NetCoreSimpleMessage;
                var advancedMessage = message as NetCoreAdvancedMessage;

                ConsoleEx.WriteLine(message.Type);
                switch (message.Type) //Handle received messages here
                {
                    case RTCV.NetCore.Commands.Remote.AllSpecSent:
                        {
                            //We still need to set the emulator's path
                            AllSpec.VanguardSpec.Update(VSPEC.EMUDIR, FileWatch.currentDir);
                            SyncObjectSingleton.FormExecute(() =>
                            {
                                FileWatch.UpdateDomains();
                            });
                        }
                        break;
                    case RTCV.NetCore.Commands.Basic.SaveSavestate:
                        e.setReturnValue("");
                        break;

                    case RTCV.NetCore.Commands.Basic.LoadSavestate:
                        e.setReturnValue(true);
                        break;

                    case RTCV.NetCore.Commands.Remote.PreCorruptAction:
                        FileWatch.KillProcess();
                        FileWatch.currentSession.targetInterface.CloseStream();
                        FileWatch.RestoreTarget();
                        break;

                    case RTCV.NetCore.Commands.Remote.PostCorruptAction:
                        //var fileName = advancedMessage.objectValue as String;
                        FileWatch.currentSession.targetInterface.CloseStream();
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            Executor.Execute();
                        });
                        break;

                    case RTCV.NetCore.Commands.Remote.CloseGame:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            FileWatch.KillProcess();
                        });
                        break;

                    case RTCV.NetCore.Commands.Remote.DomainGetDomains:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            e.setReturnValue(FileWatch.GetInterfaces());
                        });
                        break;

                    case RTCV.NetCore.Commands.Remote.EventEmuMainFormClose:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            Environment.Exit(0);
                        });
                        break;
                    case RTCV.NetCore.Commands.Remote.IsNormalAdvance:
                        e.setReturnValue(true);
                        break;

                    case RTCV.NetCore.Commands.Remote.EventCloseEmulator:
                        Environment.Exit(-1);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex, true) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();
            }
        }
    }
}
