using System;
using System.Collections.Generic;
using System.IO;

namespace HashFormat
{
    public enum ProcessState
    {
        Started,
        Processing,
        Completed
    }

    public class ProcessStateEventArgs : EventArgs
    {
        public string CurrentFile { get; set; }
        public ProcessState CurrentState { get; set; }
    }

    public abstract class HashBase
    {
        public string SelectedFolder { get; set; }

        public event EventHandler<ProcessStateEventArgs> ProcessStateChanged = null;

        public HashBase(string selectedFolder)
        {
            if (string.IsNullOrEmpty(selectedFolder))
            {
                throw new ArgumentNullException("selectedFolder");
            }
            this.SelectedFolder = selectedFolder;
        }

        protected void RaisStateChange(ProcessState state, string filePath)
        {
            if (null != ProcessStateChanged)
            {
                this.ProcessStateChanged(this, new ProcessStateEventArgs() { CurrentState = state, CurrentFile = filePath });
            }
        }

        public abstract void Run();

        protected void CreateSubDir(DirectoryInfo sourceDir, DirectoryInfo destDir)
        {
            if (sourceDir == null || destDir == null)
            {
                return;
            }

            CreateFiles(sourceDir, destDir);

            DirectoryInfo[] subDirs = sourceDir.GetDirectories();
            if (subDirs == null || subDirs.Length == 0)
            {
                return;
            }

            foreach (DirectoryInfo item in subDirs)
            {
                DirectoryInfo newItem = destDir.CreateSubdirectory(item.Name);
                CreateSubDir(item, newItem);
            }
        }

        protected IDictionary<string, string> fileDic = new Dictionary<string, string>();
        protected void CreateFiles(DirectoryInfo sourceDir, DirectoryInfo destDir)
        {
            if (sourceDir == null || destDir == null)
            {
                return;
            }
            FileInfo[] files = sourceDir.GetFiles();
            if (files == null || files.Length == 0)
            {
                return;
            }

            foreach (FileInfo item in files)
            {
                string destFile = string.Format(@"{0}\{1}", destDir.FullName, Path.GetFileName(item.FullName));
                using (FileStream fs = File.Create(destFile)) { }

                if (!fileDic.ContainsKey(item.FullName))
                {
                    fileDic.Add(item.FullName, destFile);
                }
            }
        }

    }
}
