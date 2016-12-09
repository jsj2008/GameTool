using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System;

namespace Apple
{
    public class HttperManager : HttperManagerBase<AccountItem, HttperLoginBase>
    {
        public HttperManager()
            : base(AppleLogManager.Instance)
        {
        }

        public override AccountItem GetNextItem()
        {
            AccountItem account = base.GetNextItem();
            if ((null == account) || string.IsNullOrEmpty(account.Password))
            { return account; }
            else
            {
                return FormatPwd(account);
            }
        }

        private AccountItem FormatPwd(AccountItem account)
        {
            const string AddChar = "a";
            const int AddNum = 1;
            string oldPwd = account.Password;

            foreach (PwdFormatOptions item in this.HttperParamsItem.PwdFormatOptions)
            {
                switch (item)
                {
                    case PwdFormatOptions.FirstCharReversal:
                        char c = TextHelper.CharReversal(oldPwd[0]);
                        if (c != account.Password[0])
                        {
                            account.Password = string.Format("{0}{1}", c, oldPwd.Substring(1));
                        }
                        break;
                    case PwdFormatOptions.AddOneAfterEndWithNum:
                        bool isNum = TextHelper.IsNumber(oldPwd[oldPwd.Length - 1]);
                        if (isNum)
                        {
                            account.Password = string.Format("{0}{1}", account.Password, AddNum);
                        }
                        break;
                    case PwdFormatOptions.AddAAfterEndWithChar:
                        bool isChar = TextHelper.IsChar(oldPwd[oldPwd.Length - 1]);
                        if (isChar)
                        {
                            account.Password = string.Format("{0}{1}", account.Password, AddChar);
                        }
                        break;
                    case PwdFormatOptions.AddAAllNum:
                        bool isAllNum = TextHelper.IsNumber(oldPwd);
                        if (isAllNum)
                        {
                            account.Password = string.Format("{0}{1}", account.Password, AddChar);
                        }
                        break;
                    case PwdFormatOptions.AddOneAllChar:
                        bool isAllChar = TextHelper.IsAllChar(oldPwd);
                        if (isAllChar)
                        {
                            account.Password = string.Format("{0}{1}", account.Password, AddNum);
                        }
                        break;
                    case PwdFormatOptions.RawAddFirstCharReversal:
                        char firstChar = TextHelper.CharReversal(oldPwd[0]);
                        account.Password = string.Format("{0}{1}", account.Password, firstChar);
                        break;
                    default:
                        break;
                }
            }
            return account;
        }
        protected override void StartProcess()
        {
            base.StartProcess();
            for (int i = 0; i < this.HttperParamsItem.Threads; i++)
            {
                HttpProcessor hp = new HttpProcessor(this);
                hp.ProcessedItemChanged += HttpProcess_ProcessItemChanged;
                hp.ProcessCompleted += HttpProcess_ProcessCompleted;
                hp.ReconnectChanged += HttpProcess_ReconnectChanged;
                hp.Start();
                httpProcessorList.Add(hp);
            }
        }

        #region Get methods

        public override HttperLoginBase GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                HttperLoginBase httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.AppleServer:
                        httpHelper = new HttperLoginBase(this.HttperParamsItem);
                        break;
                    default:
                        throw new NotImplementedException();
                    //break;
                }
                return httpHelper;
            }
        }

        #endregion Get methods

        protected override void ContinueFromLastStopped()
        {
            if (null == this.HttperParamsItem) return;
            this.CurrentIndex = 0;

            if (!this.HttperParamsItem.IsAppended ||
                (this.HttperParamsItem.DataFilePath != SetConfig.Instance.LastFile)
                )
            {
                DBHelper.ClearTable(AppleTableName.QueriedApple, AppleTableName.AppleResult);
            }
            SetConfig.Instance.LastFile = this.HttperParamsItem.DataFilePath;
            SetConfig.Instance.Save();
            OutputMgt.Instance.LoadLastStopCount();
        }

        protected override void SetDB()
        {
            this.queriedIndexList = DBHelper.GetQueriedIndex();
            DBHelper.SaveCacheData();
        }
    }
}