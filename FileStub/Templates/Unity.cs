namespace FileStub.Templates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.CorruptCore;

    public partial class FileStubTemplateUnity : Form, IFileStubTemplate
    {
        public FileStubTemplateUnity(string[] args)
        {
            InitializeComponent();
        }

        public FileTarget GetTargets()
        {
            throw new NotImplementedException();
        }

        public Form GetTemplateForm(string[] args)
        {
            return new FileStubTemplateUnity(args);
        }

        public static (string text, string value) GetTemplates()
        {
            return (null,null);
        }

    }
}
