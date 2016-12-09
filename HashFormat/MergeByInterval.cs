
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HashFormat
{
    public class MergeByInterval
    {
        public string FilePath { get; set; }
        public int CurrentItems { get; set; }
        private StreamReader Reader { get; set; }

        public MergeByInterval(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(filePath);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            Reader = new StreamReader(filePath, Encoding.Default);
        }

        public string ReaderItem()
        {
            if (!Reader.EndOfStream)
            {
                return string.Empty;
            }

            ///[无心·津芹]
            ///Account=wayeh282827jr
            ///Psd=qagx6qqdr2
            ///ServerName=超级女生
            ///Remark=1，强化完成级:50
            ///Remark2=门派:金刚轩辕,绑银:130353活:7

            int i = 5;
            int j = 0;
            StringBuilder sb = new StringBuilder();
            while (!Reader.EndOfStream)
            {
                string line = Reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("["))
                {
                    sb.AppendLine(line);
                    j = 1;
                    continue;
                }

                if (j < i)
                {
                    sb.AppendLine(line);
                }
                else if (j == i)
                {
                    sb.AppendLine(line);
                    break;
                }
            }
            CurrentItems++;
            string result = sb.ToString();
            return result;
        }
    }
}
