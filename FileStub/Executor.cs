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

    public static class Executor
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static string otherProgram = null;
        public static string script = null;
#pragma warning restore CA2211 // Non-constant fields should not be visible
#pragma warning restore CA1051 // Do not declare visible instance fields
        public static void EditExec()
        {
            if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM ||
                FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_WITH)
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
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.SCRIPT)
            {
                MessageBox.Show("UNIMPLEMENTED");
            }

            RefreshLabel();
        }

        public static void Execute()
        {
            string args = S.GET<StubForm>().tbArgs.Text;

            //Hijack no execution for the Netcore executor
            if (FileWatch.currentSession.selectedExecution == ExecutionType.NO_EXECUTION)
            {
            }
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_CORRUPTED_FILE)
            {
                if (FileWatch.currentSession.selectedTargetType == TargetType.SINGLE_FILE)
                {
                    var fi = (FileInterface)FileWatch.currentSession.fileInterface;
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
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_WITH)
            {
                if (otherProgram != null)
                {
                    if (FileWatch.currentSession.selectedTargetType == TargetType.SINGLE_FILE)
                    {
                        var fi = (FileInterface)FileWatch.currentSession.fileInterface;
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
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM)
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
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.SCRIPT)
            {
                MessageBox.Show("UNIMPLEMENTED");
            }
        }

        public static void RefreshLabel()
        {
            var stubForm = S.GET<StubForm>();

            stubForm.lbArgs.Visible = false;
            stubForm.tbArgs.Visible = false;

            if (FileWatch.currentSession.selectedExecution == ExecutionType.NO_EXECUTION)
                stubForm.lbExecution.Text = "No execution set";
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_CORRUPTED_FILE)
                stubForm.lbExecution.Text = "The target file will be executed";
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_WITH)
            {
                if (otherProgram == null)
                {
                    stubForm.lbExecution.Text = "No program selected for execution";
                }
                else
                {
                    stubForm.lbExecution.Text = "Target will be executed using " + otherProgram.Substring(otherProgram.LastIndexOf('\\') + 1);
                }
            }
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.EXECUTE_OTHER_PROGRAM)
            {
                stubForm.lbArgs.Visible = true;
                stubForm.tbArgs.Visible = true;

                if (otherProgram == null)
                {
                    stubForm.lbExecution.Text = "No program selected for execution";
                }
                else
                {
                    stubForm.lbExecution.Text = otherProgram.Substring(otherProgram.LastIndexOf('\\') + 1) + " will be executed after corruption";
                }
            }
            else if (FileWatch.currentSession.selectedExecution == ExecutionType.SCRIPT)
            {
                if (otherProgram == null)
                {
                    stubForm.lbExecution.Text = "No script loaded";
                }
                else
                {
                    stubForm.lbExecution.Text = script;
                }
            }
        }
    }
}
