using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashFormat
{
    public class HashMerge : HashBase
    {
        public HashMerge(string selectedFolder)
            : base(selectedFolder)
        {

        }

        public override void Run()
        {
            RaisStateChange(ProcessState.Started, string.Empty);

            DirectoryInfo sourceDir = new DirectoryInfo(SelectedFolder);
            DirectoryInfo destDir = sourceDir.Parent.CreateSubdirectory(string.Format("{0}合并去重", sourceDir.Name));

            GetFiles(sourceDir);
            string resultFile = string.Format(@"{0}\{1}", destDir.FullName, "hash.txt");
            using (StreamWriter sw = new StreamWriter(resultFile, false, Encoding.Default))
            {
                foreach (string f in fileList)
                {
                    RaisStateChange(ProcessState.Processing, f);
                }
            }

            RaisStateChange(ProcessState.Completed, string.Empty);
        }

        private void MergeToHash(string sourceFile, StreamWriter destSW)
        {
            using (StreamReader sr = new StreamReader(sourceFile, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // destSW.WriteLine(string.Format("{0}{1}", line, line));
                }
            }
        }

        IList<string> fileList = new List<string>();
        private void GetFiles(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo f in files)
            {
                fileList.Add(f.FullName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                GetFiles(d);
            }
        }
    }
}
