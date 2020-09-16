using RTCV.NetCore;
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
using RTCV;
using RTCV.CorruptCore;
using RTCV.Common;
using FileStub;

namespace Vanguard
{
    public static class VanguardImplementation
    {
        public static RTCV.Vanguard.VanguardConnector connector = null;


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

                    case REMOTE_ALLSPECSSENT:
                        {
                            //We still need to set the emulator's path
                            AllSpec.VanguardSpec.Update(VSPEC.EMUDIR, FileWatch.currentDir);
                            SyncObjectSingleton.FormExecute(() =>
                            {
                                FileWatch.UpdateDomains();
                            });
                        }
                        break;
                    case SAVESAVESTATE:
                        e.setReturnValue("");
                        break;

                    case LOADSAVESTATE:
                        e.setReturnValue(true);
                        break;

                    case REMOTE_PRECORRUPTACTION:
                        FileWatch.KillProcess();
                        FileWatch.currentFileInfo.targetInterface.CloseStream();
                        FileWatch.RestoreTarget();
                        break;

                    case REMOTE_POSTCORRUPTACTION:
                        //var fileName = advancedMessage.objectValue as String;
                        FileWatch.currentFileInfo.targetInterface.CloseStream();
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            Executor.Execute();
                        });
                        break;

                    case REMOTE_CLOSEGAME:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            FileWatch.KillProcess();
                        });
                        break;

                    case REMOTE_DOMAIN_GETDOMAINS:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            e.setReturnValue(FileWatch.GetInterfaces());
                        });
                        break;

                    case REMOTE_EVENT_EMU_MAINFORM_CLOSE:
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            Environment.Exit(0);
                        });
                        break;
                    case REMOTE_ISNORMALADVANCE:
                        e.setReturnValue(true);
                        break;

                    case REMOTE_EVENT_CLOSEEMULATOR:
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
