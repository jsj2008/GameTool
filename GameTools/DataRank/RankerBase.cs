using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PublicUtilities;

namespace WowTools
{
    public abstract class RankerBase
    {
        protected DataInsertParams DataParams
        {
            get;
            set;
        }
        protected string targetFilePath = string.Empty;
        protected Dictionary<int, StreamWriter> swDic = new Dictionary<int, StreamWriter>();

        protected bool CreateOutputDirectory(string filePath)
        {
            string dir = Path.GetDirectoryName(filePath);
            string file = Path.GetFileNameWithoutExtension(filePath);
            string newDir = string.Format("{0}\\{1}", dir, file);
            this.targetFilePath = newDir;
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
                return true;
            }
            return false;
        }

        protected void CloseFileStream()
        {
            if (swDic.Values.Count > 0)
            {
                foreach (StreamWriter sw in swDic.Values)
                {
                    using (sw)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                }
            }

            this.swDic.Clear();
        }

        protected virtual void CreateFileWriter(string filePrefix)
        {
            this.CloseFileStream();
            CreateOutputDirectory(this.DataParams.RawFilePath);
            //Less than 3, more than 8, and 3 to 8
            for (int i = 3; i <= 8; i++)
            {
                string fileName = string.Format("{0}\\{1}_{2}.txt", targetFilePath, filePrefix, i);
                StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.CreateNew));
                swDic.Add(i, sw);
            }
        }

        int flushCount = 0;
        protected virtual bool WriteToFile(AccountItem account)
        {
            StreamWriter sw = null;
            if (account.OldPassword.Length <= 3)
            {
                sw = swDic[3];
            }
            else if (account.OldPassword.Length >= 8)
            {
                sw = swDic[8];
            }
            else
            {
                sw = swDic[account.OldPassword.Length];
            }

            if (null == sw)
            {
                //LogManager.Error("File writer can't be null!");
                return false;
            }
            if (null != sw)
            {
                sw.WriteLine(account.ToString());
                flushCount++;
                if (flushCount == 100)
                {
                    sw.Flush();
                    flushCount = 0;
                }
                return true;
            }

            return false;
        }

        public abstract bool ParsePassword(AccountItem account);

        #region  Add /Del chars

        protected virtual string AddOrDel(string pwd)
        {
            pwd = pwd.Trim();
            string addChars = string.Empty;
            switch (pwd.Length)
            {
                case 4:
                    addChars = this.DataParams.Add4Chars; break;
                case 5:
                    addChars = this.DataParams.Add5Chars; break;
                case 6:
                    addChars = this.DataParams.Add6Chars; break;
                case 7:
                    addChars = this.DataParams.Add7Chars; break;
                case 8:
                    addChars = this.DataParams.Add8Chars; break;
                default:
                    if (pwd.Length <= 3)
                    {
                        addChars = this.DataParams.Add3Chars;
                    }
                    else if (pwd.Length > 8)
                    {
                        addChars = this.DataParams.Add9Chars;
                    }
                    break;
            }
            pwd = this.Add(pwd, this.DataParams.AddIndex, addChars);
            pwd = this.Delete(pwd, this.DataParams.DelIndex);
            return pwd;
        }

        protected string Add(string pwd, int index, string addChars)
        {
            if (this.DataParams.IsCanAdd)
            {
                if (this.DataParams.IsAddBefore)
                {
                    return this.AddBefor(pwd, index, addChars);
                }

                if (this.DataParams.IsAddAfter)
                {
                    return this.AddAfter(pwd, index, addChars);
                }
            }
            return pwd;
        }

        protected string Delete(string pwd, int index)
        {
            if (this.DataParams.IsCanDel)
            {
                if (this.DataParams.IsDelBefore)
                {
                    return this.DelBefore(pwd, index);
                }

                if (this.DataParams.IsDelAfter)
                {
                    return this.DelAfter(pwd, index);
                }
            }

            return pwd;
        }

        private string AddBefor(string pwd, int index, string addChars)
        {
            if (index <= 0)
            {
                return addChars + pwd;
            }
            else if (index > (pwd.Length - 1))
            {
                return pwd + addChars;
            }
            else
            {
                return pwd.Insert(index, addChars);
            }
        }

        private string AddAfter(string pwd, int index, string addChars)
        {
            if (index < 0)
            {
                return addChars + pwd;
            }
            else if (index >= (pwd.Length - 1))
            {
                return pwd + addChars;
            }
            else
            {
                return pwd.Insert(index + 1, addChars);
            }
        }

        private string DelBefore(string pwd, int index)
        {
            if (pwd.Length > 1)
            {
                if ((index <= 0) || (index == 1))
                {
                    return pwd.Substring(1);
                }
                else if (index > (pwd.Length - 1))
                {
                    return pwd.Substring(0, pwd.Length - 1);
                }
                else
                {
                    return pwd.Substring(0, index - 1) + pwd.Substring(index);
                }
            }
            else
            {
                return pwd;
            }

            //if ((index <= 0) || (index == 1))
            //{
            //    return pwd.Remove(0);
            //}
            //else if (index > (pwd.Length - 1))
            //{
            //    return pwd.Remove(pwd.Length - 1);
            //}
            //else
            //{
            //    return pwd.Remove(index - 1);
            //}
        }

        private string DelAfter(string pwd, int index)
        {
            if (pwd.Length > 1)
            {
                if (index < 0)
                {
                    return pwd.Substring(1);
                }
                else if (index >= (pwd.Length - 1 - 1))
                {
                    return pwd.Substring(0, pwd.Length - 1);
                }
                else
                {
                    return pwd.Substring(0, index + 1) + pwd.Substring(index + 2);
                }
            }
            else
            {
                return pwd;
            }

            //if (index < 0)
            //{
            //    return pwd.Remove(0);
            //}
            //else if (index >= (pwd.Length - 1 - 1))
            //{
            //    return pwd.Remove(pwd.Length - 1);
            //}
            //else
            //{
            //    return pwd.Remove(index + 1);
            //}
        }

        #endregion

        public void Save()
        {
            foreach (StreamWriter sw in this.swDic.Values)
            {
                using (sw)
                {
                    sw.Flush();
                }
            }
        }
    }
}
