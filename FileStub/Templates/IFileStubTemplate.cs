namespace FileStub.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.CorruptCore;

    interface IFileStubTemplate
    {
        Form GetTemplateForm(string[] args);
        FileTarget GetTargets();
    }
}
