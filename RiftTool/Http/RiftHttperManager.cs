using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System;

namespace RiftTool
{
    public class RiftHttperManager : HttperManagerBase<RiftAccountItem, USRiftHttperLoginBase>
    {
        public RiftHttperManager()
            : base(RiftLogManager.Instance)
        {
        }

        protected override void StartProcess()
        {
            base.StartProcess();
            for (int i = 0; i < this.HttperParamsItem.Threads; i++)
            {
                RiftHttpProcessor hp = new RiftHttpProcessor(this);
                hp.ProcessedItemChanged += HttpProcess_ProcessItemChanged;
                hp.ProcessCompleted += HttpProcess_ProcessCompleted;
                hp.ReconnectChanged += HttpProcess_ReconnectChanged;
                hp.Start();
                httpProcessorList.Add(hp);
            }
        }

        #region Get methods      

        public override USRiftHttperLoginBase GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                USRiftHttperLoginBase httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.USRift:
                        httpHelper = new USRiftHttperLoginBase(this.HttperParamsItem);
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
                (this.HttperParamsItem.DataFilePath != RiftSetConfig.Instance.LastFile)
                )
            {
                RiftDBHelper.ClearTable(RiftTableName.QueriedRift, RiftTableName.RiftResult);
            }
            RiftSetConfig.Instance.LastFile = this.HttperParamsItem.DataFilePath;
            RiftSetConfig.Instance.Save();
            RiftOutputMgt.Instance.LoadLastStopCount();
        }

        protected override void SetDB()
        {
            this.queriedIndexList = RiftDBHelper.GetQueriedIndex();
            RiftDBHelper.SaveCacheData();
        }
    }
}