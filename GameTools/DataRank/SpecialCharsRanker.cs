using System.IO;
using System.Text.RegularExpressions;
using PublicUtilities;

namespace WowTools
{
    public class SpecialCharsRanker : RankerBase
    {
        public SpecialCharsRanker(DataInsertParams param)
        {
            this.DataParams = param;
            this.CreateOutputDirectory(this.DataParams.RawFilePath);
            this.CreateFileWriter("特殊字符");
        }

        public override bool ParsePassword(AccountItem account)
        {
            account.SplitChars = this.DataParams.TargetSplitChars;
            if (IsContainSpecialChar(account.Password))
            {
                account.Password = this.AddOrDel(account.Password);
                return this.WriteToFile(account);
            }

            return false;
        }

        protected override string AddOrDel(string pwd)
        {            
            pwd = this.Add(pwd, this.DataParams.AddIndex, this.DataParams.AddSpecialChars);
            pwd = this.Delete(pwd, this.DataParams.DelIndex);
            return pwd;
        }

        /// <summary>
        /// Contain special char and len large and equal 8
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsContainSpecialChar(string line)
        {
            bool b = (line.Length >= 8) && Regex.IsMatch(line, @"[^A-Za-z\d]+");
            return b;
        }

        protected override void CreateFileWriter(string filePrefix)
        {
            this.CloseFileStream();
            //more than 8
            for (int i = 8; i <= 8; i++)
            {
                string fileName = string.Format("{0}\\{1}_{2}.txt", targetFilePath, filePrefix, i);
                StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.CreateNew));
                swDic.Add(i, sw);
            }
        }

        
    }
}
