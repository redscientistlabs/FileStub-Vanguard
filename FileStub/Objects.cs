namespace FileStub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using Ceras;
    using RTCV.CorruptCore;

    public class FileStubFileInfo
    {
        internal string targetShortName = "No target";
        internal string selectedExecution = null;
        internal bool writeCopyMode = false;
        internal string targetFullName = "No target";
        internal FileMemoryInterface targetInterface;
        internal string selectedTargetType = TargetType.SINGLE_FILE;
        internal bool autoUncorrupt = true;
        internal bool TerminateBeforeExecution = true;
        internal bool useAutomaticBackups = true;
        internal bool bigEndian = false;
        internal bool useCacheAndMultithread = true;

        public override string ToString()
        {
            return targetShortName;
        }
    }

    public static class ExecutionType
    {
        public const string EXECUTE_CORRUPTED_FILE = "Execute corrupted file";
        public const string EXECUTE_WITH = "Execute with";
        public const string EXECUTE_OTHER_PROGRAM = "Execute other program";
        public const string NO_EXECUTION = "No execution";
        public const string SCRIPT = "Script";
    }

    public static class TargetType
    {
        public const string SINGLE_FILE = "Single File";
        public const string MULTIPLE_FILE_SINGLEDOMAIN = "Multiple files (One domain)";
        public const string MULTIPLE_FILE_MULTIDOMAIN = "Multiple files (Many domains)";
        public const string MULTIPLE_FILE_MULTIDOMAIN_FULLPATH = "Multiple files (Many domains + Full path)";
    }
}
