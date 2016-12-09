using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System;

namespace RSTool
{
    public class RSHttperManager : HttperManagerBase<RSAccountItem, RSHttperLoginBase>
    {
        public RSHttperManager()
            : base(RSLogManager.Instance)
        {
        }

        protected override void StartProcess()
        {
            base.StartProcess();
            for (int i = 0; i < this.HttperParamsItem.Threads; i++)
            {
                RSHttpProcessor hp = new RSHttpProcessor(this);
                hp.ProcessedItemChanged += HttpProcess_ProcessItemChanged;
                hp.ProcessCompleted += HttpProcess_ProcessCompleted;
                hp.ReconnectChanged += HttpProcess_ReconnectChanged;
                hp.Start();
                httpProcessorList.Add(hp);
            }
        }

        #region Get methods      

        public override RSHttperLoginBase GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                RSHttperLoginBase httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.RSServer:
                        httpHelper = new RSHttperLoginBase(this.HttperParamsItem);
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
                (this.HttperParamsItem.DataFilePath != RSSetConfig.Instance.LastFile)
                )
            {
                RSDBHelper.ClearTable(RSTableName.QueriedRS, RSTableName.RSResult);
            }
            RSSetConfig.Instance.LastFile = this.HttperParamsItem.DataFilePath;
            RSSetConfig.Instance.Save();
            RSOutputMgt.Instance.LoadLastStopCount();
        }

        protected override void SetDB()
        {
            this.queriedIndexList = RSDBHelper.GetQueriedIndex();
            RSDBHelper.SaveCacheData();
        }
    }
}