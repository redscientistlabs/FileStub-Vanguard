using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStub.Templates.PluginHost;

namespace Yuzu
{
    [Export(typeof(IFileStubPlugin))]
    public class Plugin : IFileStubPlugin
    {
        public String Name => "Yuzu Template";
        public String Description => "Targets Nintendo Switch executable files in FileStub";
        public string Author => "ChrisNonyminus";
        public Version Version => new Version(0, 0, 1);
        public bool Start()
        {

            return true;
        }

    }
}
