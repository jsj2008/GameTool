using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashFormat
{
    public class ColumnExtractor : HashBase
    {
        public string[] Columns { get; set; }
        private char[] columnNamesSplitChars = { ',', ';', '，', '；', ' ', '\t' };
        private char[] columnValuesSplitChars = { ' ', '\t' };
        private const int columnLen = 20;

        public ColumnExtractor(string selectedFolder, string columns)
            : base(selectedFolder)
        {
            if (string.IsNullOrEmpty(columns))
            {
                throw new ArgumentNullException("columns");
            }
            this.Columns = columns.Split(columnNamesSplitChars, StringSplitOptions.RemoveEmptyEntries);
        }

        public override void Run()
        {
            RaisStateChange(ProcessState.Started, string.Empty);

            DirectoryInfo sourceDir = new DirectoryInfo(SelectedFolder);
            DirectoryInfo destDir = sourceDir.Parent.CreateSubdirectory(string.Format("{0}分列", sourceDir.Name));
            fileDic.Clear();
            CreateSubDir(sourceDir, destDir);
            foreach (KeyValuePair<string, string> item in fileDic)
            {
                RaisStateChange(ProcessState.Processing, item.Key);
                CreateColumeFilter(item.Key, item.Value);
            }
            RaisStateChange(ProcessState.Completed, string.Empty);
        }

        private void CreateColumeFilter(string sourceFile, string destFile)
        {
            using (StreamReader sr = new StreamReader(sourceFile, Encoding.Default))
            {
                using (StreamWriter sw = new StreamWriter(destFile, false, Encoding.Default))
                {
                    bool isFirstRow = true;
                    IList<int> indexList = null;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        byte[] bytes = Encoding.UTF8.GetBytes(line);
                        line = Encoding.UTF8.GetString(bytes);
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        if (isFirstRow)
                        {
                            indexList = GetColumnIndex(line);
                            StringBuilder sb = new StringBuilder();
                            foreach (string item in Columns)
                            {
                                sb.Append(item.PadRight(columnLen, ' '));
                            }
                            sw.WriteLine(sb.ToString());
                            isFirstRow = false;
                        }
                        else
                        {
                            string[] columnsValue = line.Split(columnValuesSplitChars, StringSplitOptions.RemoveEmptyEntries);
                            StringBuilder sb = new StringBuilder();
                            foreach (int index in indexList)
                            {
                                sb.Append(columnsValue[index].PadRight(columnLen, ' '));
                            }
                            sw.WriteLine(sb.ToString());
                        }
                    }
                }
            }
        }

        private IList<int> GetColumnIndex(string row)
        {
            if (string.IsNullOrEmpty(row))
            {
                throw new ArgumentNullException("row");
            }

            IList<int> indexList = new List<int>();
            string[] titles = row.Split(columnValuesSplitChars, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < titles.Length; i++)
            {
                if (indexList.Count == Columns.Length)
                {
                    break;
                }
                foreach (string item in Columns)
                {
                    if (titles[i].Trim().Equals(item.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        indexList.Add(i);
                        break;
                    }
                }
            }

            if (indexList.Count != Columns.Length)
            {
                throw new InvalidOperationException("筛选列名和文件实际列名不匹配");
            }
            return indexList;
        }
    }
}
