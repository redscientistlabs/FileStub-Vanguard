namespace UnityTemplate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FileStub.Templates.PluginHost;

    [Export(typeof(IFileStubPlugin))]
    public class Plugin : IFileStubPlugin
    {
        public string Name => "Unity Engine Template";
        public string Description => "Targets Unity game dlls in FileStub";
        public string Author => "ChrisNonyminus and Ircluzar";
        public Version Version => new Version(0, 0, 1);
        public bool Start()
        {
            return true;
        }
    }
}
