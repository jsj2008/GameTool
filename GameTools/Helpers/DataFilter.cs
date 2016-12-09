using System.IO;
using System.Text;
using PublicUtilities;

namespace WowTools
{
    public class DataFilter : FactoryBase
    {
        private DataFilterParam CurrentFilterParam
        {
            get;
            set;
        }
        private StreamWriter resultSW = null;
        public DataFilter(DataFilterParam dataFilterParam)
        {
            this.CurrentFilterParam = dataFilterParam;
            this.FilePath = this.CurrentFilterParam.FilePath;
        }

        protected override void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;
            AccountItem account = new AccountItem(line, DataFormat.MailPassword, "");
            if (account.IsValidate)
            {
                account.SplitChars = "  ";
                if (this.CurrentFilterParam.IsFilterChar)
                {
                    account.Password = this.RemoveChar(account.Password);
                }

                if (this.CurrentFilterParam.IsFilterNum)
                {
                    account.Password = this.RemoveNum(account.Password);
                }

                this.WriteLine(account.ToString());
            }
        }     

        private string RemoveChar(string pwd)
        {
            char[] pwdList = pwd.ToCharArray();
            for (int i = 0; i < pwdList.Length; i++)
            {
                if (TextHelper.IsChar(pwdList[i]))
                {
                    pwdList[i] = '0';
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (char item in pwdList)
            {
                if(item !='0')
                {
                    sb.Append(item);
                }
                
            }
            return sb.ToString();
        }

        private string RemoveNum(string pwd)
        {
            char[] pwdList = pwd.ToCharArray();
            for (int i = 0; i < pwdList.Length; i++)
            {
                if (TextHelper.IsNumber(pwdList[i]))
                {
                    pwdList[i] = '0';
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (char item in pwdList)
            {
                if (item != '0')
                {
                    sb.Append(item);
                }

            }
            return sb.ToString();
        }

        private void WriteLine(string line)
        {
           this.CreateResultFile(this.FilePath);
            if (!string.IsNullOrEmpty(line) && (null != resultSW))
            {
                resultSW.WriteLine(line);
            }
        }

        private void CreateResultFile(string filePath)
        {
            if (null == this.resultSW)
            {
                string dir = Path.GetDirectoryName(filePath);
                string file = Path.GetFileNameWithoutExtension(filePath);
                string newFile = string.Format("{0}\\{1}_result.txt", dir, file);
                if (File.Exists(newFile))
                {
                    File.Delete(newFile);
                }
                FileStream fs = File.Create(newFile);
                resultSW = new StreamWriter(fs, Encoding.UTF8);
                resultSW.AutoFlush = true;
            }
        }

        public override void Stop()
        {
            base.Stop();
            if (null != resultSW)
            {
                using (resultSW)
                {
                    resultSW.Flush();
                    resultSW.Close();
                }
            }
        }
    }

    public class DataFilterParam
    {
        public string FilePath
        { get; set; }

        public bool IsFilterChar
        { get; set; }

        public bool IsFilterNum
        { get; set; }
    }
}
