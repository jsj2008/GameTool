using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashFormat
{
    public class HashAdder : HashBase
    {
        public string HashValue { get; set; }
        public HashAdder(string selectedFolder, string hashValue)
            : base(selectedFolder)
        {
            if (string.IsNullOrEmpty(hashValue))
            {
                throw new ArgumentNullException("hashValue");
            }
            this.HashValue = hashValue;
        }

        public override void Run()
        {
            RaisStateChange(ProcessState.Started, string.Empty);

            DirectoryInfo sourceDir = new DirectoryInfo(SelectedFolder);
            DirectoryInfo destDir = sourceDir.Parent.CreateSubdirectory(string.Format("{0}+{1}", sourceDir.Name, HashValue));
            fileDic.Clear();
            CreateSubDir(sourceDir, destDir);
            foreach (KeyValuePair<string, string> item in fileDic)
            {
                RaisStateChange(ProcessState.Processing, item.Key);
                CreateFileWithHash(item.Key, item.Value);
            }
            RaisStateChange(ProcessState.Completed, string.Empty);
        }

        private void CreateFileWithHash(string sourceFile, string destFile)
        {
            using (StreamReader sr = new StreamReader(sourceFile, Encoding.Default))
            {
                using (StreamWriter sw = new StreamWriter(destFile, false, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        sw.WriteLine(string.Format("{0}{1}", HashValue, line));
                    }
                }
            }
        }
    }
}
