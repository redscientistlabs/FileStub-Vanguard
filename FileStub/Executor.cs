namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using RTCV.Common;
    using RTCV.CorruptCore;

    static class Executor
    {
        public static string otherProgram = null;
        public static string script = null;

        public static void EditExec()
        {
            if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM ||
                FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_WITH)
            {
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
                        return;
                    }

                    otherProgram = OpenFileDialog1.FileName.ToString();
                }
                else
                    return;
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.SCRIPT)
            {
                MessageBox.Show("UNIMPLEMENTED");
            }

            RefreshLabel();
        }

        public static void Execute()
        {
            string args = S.GET<StubForm>().tbArgs.Text;

            //Hijack no execution for the Netcore executor
            if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.NO_EXECUTION)
            {
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_CORRUPTED_FILE)
            {
                if (FileWatch.currentFileInfo.selectedTargetType == TargetType.SINGLE_FILE)
                {
                    var fi = (FileInterface)FileWatch.currentFileInfo.targetInterface;
                    //Process.Start(fi.filename);

                    string fullPath = fi.Filename;
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = Path.GetFileName(fullPath);
                    psi.WorkingDirectory = Path.GetDirectoryName(fullPath);

                    if (!string.IsNullOrWhiteSpace(args))
                        psi.Arguments = args;

                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show("Execution of multiple individual files currently unsupported. Use Execute other program.");
                    return;
                }
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_WITH)
            {
                if (otherProgram != null)
                {
                    if (FileWatch.currentFileInfo.selectedTargetType == TargetType.SINGLE_FILE)
                    {
                        var fi = (FileInterface)FileWatch.currentFileInfo.targetInterface;
                        //Process.Start(otherProgram, "\"" + fi.filename + "\"");

                        string fullPath = otherProgram;
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = Path.GetFileName(fullPath);
                        psi.WorkingDirectory = Path.GetDirectoryName(fullPath);
                        psi.Arguments = "\"" + fi.Filename + "\"";

                        if (!string.IsNullOrWhiteSpace(args))
                            psi.Arguments = " " + args;

                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Execution of multiple individual files currently unsupported. Use Execute other program.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("You need to specify a file to execute with the Edit Exec button.");
                    return;
                }
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM)
            {
                if (otherProgram != null)
                {
                    string fullPath = otherProgram;
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = Path.GetFileName(fullPath);
                    psi.WorkingDirectory = Path.GetDirectoryName(fullPath);

                    if (!string.IsNullOrWhiteSpace(args))
                        psi.Arguments = args;

                    Process.Start(psi);
                }
                else
                    MessageBox.Show("You need to specify a file to execute with the Edit Exec button.");
                return;
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.SCRIPT)
            {
                MessageBox.Show("UNIMPLEMENTED");
            }
        }

        public static void RefreshLabel()
        {
            var gh = S.GET<StubForm>();

            gh.lbArgs.Visible = false;
            gh.tbArgs.Visible = false;

            if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.NO_EXECUTION)
                gh.lbExecution.Text = "No execution set";
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_CORRUPTED_FILE)
                gh.lbExecution.Text = "The target file will be executed";
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_WITH)
            {
                if (otherProgram == null)
                {
                    gh.lbExecution.Text = "No program selected for execution";
                }
                else
                {
                    gh.lbExecution.Text = "Target will be executed using " + otherProgram.Substring(otherProgram.LastIndexOf('\\') + 1);
                }
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM)
            {
                gh.lbArgs.Visible = true;
                gh.tbArgs.Visible = true;

                if (otherProgram == null)
                {
                    gh.lbExecution.Text = "No program selected for execution";
                }
                else
                {
                    gh.lbExecution.Text = otherProgram.Substring(otherProgram.LastIndexOf('\\') + 1) + " will be executed after corruption";
                }
            }
            else if (FileWatch.currentFileInfo.selectedExecution == ExecutionType.SCRIPT)
            {
                if (otherProgram == null)
                {
                    gh.lbExecution.Text = "No script loaded";
                }
                else
                {
                    gh.lbExecution.Text = script;
                }
            }
        }
    }
}
