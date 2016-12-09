using PublicUtilities;

namespace WebDetection
{
    public class WowHttperManager : HttperManagerBase<UserAccountItem, WowHttperLoginBase>
    {
        public WowHttperManager()
            : base(WowLogManager.Instance)
        {
            this.IPLimitCount = WowSetConfig.Instance.IPAcessTimes;
        }

        protected override void StartProcess()
        {
            base.StartProcess();
            for (int i = 0; i < this.HttperParamsItem.Threads; i++)
            {
                WowHttpProcessor hp = new WowHttpProcessor(this);
                hp.ProcessedItemChanged += HttpProcess_ProcessItemChanged;
                hp.ProcessCompleted += HttpProcess_ProcessCompleted;
                hp.ReconnectChanged += HttpProcess_ReconnectChanged;
                hp.Start();
                httpProcessorList.Add(hp);
            }
        }
        
        #region Get methods

        public override WowHttperLoginBase GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                WowHttperLoginBase httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.USBattle:
                        httpHelper = new USBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.EUBattle:
                        httpHelper = new EUBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.SEABattle:
                        httpHelper = new SeaBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.KRBattle:
                        httpHelper = new KRBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.TWBattle:
                        httpHelper = new TWBattleLoginHttper(this.HttperParamsItem);
                        break;
                    default:
                        break;
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
                (this.HttperParamsItem.DataFilePath != WowSetConfig.Instance.LastFile)
                )
            {
                BattleDBHelper.ClearTable(WOWTableName.QueriedAccount, WOWTableName.HistoryLists);
            }
            WowSetConfig.Instance.LastFile = this.HttperParamsItem.DataFilePath;
            WowSetConfig.Instance.Save();
            BattleOutptMgt.Instance.LoadLastStopCount();
        }

        protected override void SetDB()
        {
            this.queriedIndexList = BattleDBHelper.GetQueriedIndex();
            BattleDBHelper.SaveCacheData();
        }
    }
}