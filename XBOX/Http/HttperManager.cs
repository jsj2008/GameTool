using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System;

namespace XBOX
{
    public class HttperManager : HttperManagerBase<AccountItem, XBoxHttperLogin>
    {
        public HttperManager()
            : base(XBOXLogManager.Instance)
        {
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

        public override XBoxHttperLogin GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                XBoxHttperLogin httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.XBOXServer:
                        httpHelper = new XBoxHttperLogin(this.HttperParamsItem);
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
                DBHelper.ClearTable(XBOXTableName.QueriedXBOX, XBOXTableName.XBOXResult);
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