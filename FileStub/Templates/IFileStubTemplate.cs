namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.CorruptCore;

    public interface IFileStubTemplate
    {
        Form GetTemplateForm(string templateName);
        FileTarget[] GetTargets();
        void GetSegments(FileInterface exeInterface);
        string[] TemplateNames { get; }

        bool DragDrop(string[] fd);
        bool DisplayBrowseTarget { get; }
        bool DisplayDragAndDrop { get; }
        void BrowseFiles();

    }
}
