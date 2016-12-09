using System;
using System.Collections.Generic;
using System.IO;
using PublicUtilities;

namespace WowTools
{  
    public class RankerFactory : FactoryBase
    {
        private DataFormat Format
        {
            get;
            set;
        }

        private Dictionary<DataType, DataInsertParams> ParamsDic
        {
            get;
            set;
        }

        private string SplitChar
        {
            get
            {
                if (this.ParamsDic.Count > 0)
                {
                    return this.ParamsDic[DataType.Num].RawSplitChars;
                }
                return string.Empty;
            }
        }

        public RankerFactory(string filePath, DataFormat format, Dictionary<DataType, DataInsertParams> paramDic)
        {
            this.Format = format;
            this.FilePath = filePath;
            this.ParamsDic = paramDic;
            this.LoadRankerList();
        }

        protected override void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            AccountItem account = new AccountItem(line, this.Format, this.SplitChar);
            if (account.IsValidate)
            {
                StartRank(account);
            }
        }

        private IList<RankerBase> rankerList = new List<RankerBase>();
        private void LoadRankerList()
        {
            rankerList.Clear();
            this.DeleteTargetDir(this.FilePath);
            rankerList.Add(new NumberRanker(this.ParamsDic[DataType.Num]));
            rankerList.Add(new CharRanker(this.ParamsDic[DataType.Char]));
            //SpecialCharsRanker priority to CompositeRanker
            rankerList.Add(new SpecialCharsRanker(this.ParamsDic[DataType.Special]));
            rankerList.Add(new CompositeRanker(this.ParamsDic[DataType.Composit]));
        }

        private bool DeleteTargetDir(string filePath)
        {
            string dir = Path.GetDirectoryName(filePath);
            string file = Path.GetFileNameWithoutExtension(filePath);
            string newDir = string.Format("{0}\\{1}", dir, file);
            if (Directory.Exists(newDir))
            {
                Directory.Delete(newDir, true);
                return true;
            }
            return false;
        }

        private void StartRank(AccountItem account)
        {
            foreach (RankerBase ranker in this.rankerList)
            {
                if (ranker.ParsePassword(account))
                {
                    break;
                }
            }
        }

        public override void Stop()
        {
            base.Stop();

            foreach (RankerBase ranker in this.rankerList)
            {
                ranker.Save();
            }
        }
    }


    public class AccountItem
    {
        string[] SpaceChars = { " ", "\t", "\r", "\n" };
        public string User
        {
            get;
            set;
        }

        /// <summary>
        /// Old Password usefull output to a length file
        /// </summary>
        public string OldPassword
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Mail
        {
            get;
            set;
        }

        public string SplitChars
        {
            get;
            set;
        }

        public DataFormat Format
        {
            get;
            private set;
        }

        public bool IsValidate
        {
            get;
            private set;
        }

        public AccountItem(string rawLine, DataFormat format, string splitChar)
        {
            this.Format = format;

            if (!string.IsNullOrEmpty(splitChar.Trim()))
            {
                SpaceChars = new string[] { splitChar };
            }
            string[] items = rawLine.Split(SpaceChars, StringSplitOptions.RemoveEmptyEntries);

            if ((null != items) && items.Length > 0)
            {
                switch (this.Format)
                {
                    case DataFormat.AccountPasswordMail:
                        LoadAccountMailPassword(items); break;
                    case DataFormat.AccountMailPassword:
                        LoadAccountMailPassword(items); break;
                    case DataFormat.MailPassword:
                        LoadMailPassword(items); break;
                    default:
                        break;
                }
            }
        }

        private void LoadAccountMailPassword(string[] items)
        {
            if (items.Length == 3)
            {
                this.User = items[0].Trim();
                this.Mail = items[1].Trim();
                this.Password = items[2].Trim();
                this.OldPassword = this.Password;
                this.IsValidate = true;
            }
            else
            {
                this.IsValidate = false;
            }
        }

        private void LoadAccountPasswordMail(string[] items)
        {
            if (items.Length == 3)
            {
                this.User = items[0].Trim();
                this.Password = items[1].Trim();
                this.Mail = items[2].Trim();
                this.OldPassword = this.Password;
                this.IsValidate = true;
            }
            else
            {
                this.IsValidate = false;
            }
        }


        private void LoadMailPassword(string[] items)
        {
            if (items.Length == 2)
            {

                this.Mail = items[0].Trim();
                this.Password = items[1].Trim();
                this.OldPassword = this.Password;
                this.IsValidate = true;
            }
            else
            {
                this.IsValidate = false;
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.User))
            {
                return string.Format("{1}{0}{2}", this.SplitChars, this.Mail, this.Password);
            }

            return string.Format("{1}{0}{2}{0}{3}", this.SplitChars, this.User, this.Mail, this.Password);
        }
    }
}
