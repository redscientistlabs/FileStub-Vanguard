namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.Common;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Make sure we resolve our dlls
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Initialize();
        }

        static void Initialize()
        {
            var frm = new StubForm();
            S.SET<StubForm>(frm);
            Application.Run(frm);
        }

        //Lifted from Bizhawk
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                string requested = args.Name;
                lock (AppDomain.CurrentDomain)
                {
                    var asms = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var asm in asms)
                        if (asm.FullName == requested)
                        {
                            return asm;
                        }

                    //load missing assemblies by trying to find them in the dll directory
                    string dllname = new AssemblyName(requested).Name + ".dll";
                    string directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "RTCV");
                    string simpleName = new AssemblyName(requested).Name;
                    string fname = Path.Combine(directory, dllname);
                    if (!File.Exists(fname))
                    {
                        return null;
                    }

                    //it is important that we use LoadFile here and not load from a byte array; otherwise mixed (managed/unamanged) assemblies can't load
                    return Assembly.UnsafeLoadFrom(fname);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went really wrong in AssemblyResolve. Send this to the devs\n" + e);
                return null;
            }
        }
    }
}
