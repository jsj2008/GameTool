using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System;

namespace GuildWar
{
    public class HttperManager : HttperManagerBase<FightAccountItem, FightHttperLogin>
    {
        public HttperManager()
            : base(FightLogManager.Instance)
        {
        }

        protected override void StartProcess()
        {
            this.IPLimitCount = this.HttperParamsItem.RedialCount;
            this.HttperParamsItem.IsIpAccessLimit = true;

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

        public override FightHttperLogin GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                FightHttperLogin httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.GuildWarsServer:
                        //httpHelper = new HttperLoginBase(this.HttperParamsItem);  
                        httpHelper = new FightHttperLogin(this.HttperParamsItem);
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
                DBHelper.ClearTable(FightTableName.QueriedFight, FightTableName.FightResult);
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