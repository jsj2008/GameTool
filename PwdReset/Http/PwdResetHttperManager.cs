using System.IO;
using System.Windows.Forms;
using PwdReset;
using System.Text;

namespace PublicUtilities
{
    public class PwdResetHttperManager : HttperManagerBase<PwdResetItem, PwdResetHttperBase>
    {
        public PwdResetHttperManager()
            : base(WowLogManager.Instance)
        {
        }

        protected override void StartProcess()
        {
            base.StartProcess();
            for (int i = 0; i < this.HttperParamsItem.Threads; i++)
            {
                PwdResetHttpProcessor hp = new PwdResetHttpProcessor(this);
                hp.ProcessedItemChanged += HttpProcess_ProcessItemChanged;
                hp.ProcessCompleted += HttpProcess_ProcessCompleted;
                hp.ReconnectChanged += HttpProcess_ReconnectChanged;
                hp.Start();
                httpProcessorList.Add(hp);
            }
        }

        #region Get methods

        public override PwdResetHttperBase GetHttper()
        {
            lock (this)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }
                return GetHttper(this.HttperParamsItem.CurrentGameServer.GameServerType);
            }
        }

        public override PwdResetHttperBase GetHttper(GameServerType gsType)
        {
            lock (httpLockObject)
            {
                if (null == this.HttperParamsItem)
                {
                    return null;
                }

                PwdResetHttperBase httpHelper = null;
                switch (gsType)
                {
                    case GameServerType.USBattle:
                        httpHelper = new USPwdResetHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.EUBattle:
                        httpHelper = new ENPwdResetHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.SEABattle:
                        //httpHelper = new SeaBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.KRBattle:
                        ///httpHelper = new KRBattleLoginHttper(this.HttperParamsItem);
                        break;
                    case GameServerType.TWBattle:
                        //httpHelper = new TWBattleLoginHttper(this.HttperParamsItem);
                        break;
                    default:
                        break;
                }
                return httpHelper;
            }
        }

        StringBuilder sb = new StringBuilder();
        int queriedCount = 0;
        public override PwdResetItem GetNextItem()
        {
            try
            {
                lock (this.readLockObject)
                {
                    // if process by custom range ,check it ,if out of range then  stop process
                    if ((this.HttperParamsItem.IsSupportedCustomRange))
                    {
                        if (this.CurrentIndex < this.HttperParamsItem.RangeLower)
                        {
                            this.CurrentIndex = this.HttperParamsItem.RangeLower;
                            if (this.CurrentIndex < 0) this.CurrentIndex = 0;
                        }
                        if (this.CurrentIndex > this.HttperParamsItem.RangeUpper)
                        {
                            return null;
                        }
                    }

                    if ((this.accountList.Count > 0) && (this.CurrentIndex >= 0) && (this.CurrentIndex < this.accountList.Count))
                    {
                        PwdResetItem item = this.accountList[this.CurrentIndex++];
                        while ((null != item) && queriedIndexList.Contains(item.Index))
                        {
                            if (this.CurrentIndex < this.accountList.Count)
                            {
                                if (queriedCount++ == 0)
                                {
                                    sb.Append(string.Format("Below accounts has been queried:\n"));
                                }

                                sb.Append(string.Format("Index:{0},Email:{1}\t", item.Index, item.EMail));
                                if (queriedCount % 20 == 0)
                                {
                                    LogManager.Info(sb.ToString());
                                    sb = new StringBuilder();
                                    queriedCount = 0;
                                }
                                item = this.accountList[this.CurrentIndex++];
                            }
                        }
                        return item;
                    }
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("HttperManager.GetNextItem() failed:{0}", ex.Message));
            }
            return null;
        }

        protected override void LoadFile()
        {
            try
            {
                if ((null == this.HttperParamsItem) || (string.IsNullOrEmpty(this.HttperParamsItem.DataFilePath)))
                {
                    return;
                }

                this.accountList.Clear();
                this.TotalCount = 0;
                using (StreamReader sr = new StreamReader(this.HttperParamsItem.DataFilePath))
                {
                    while (!sr.EndOfStream && !isDisposed)
                    {
                        PwdResetItem item = TextToItemHelper.GetPwdResetItem(sr.ReadLine(), 
                            this.HttperParamsItem.DataFormat,WowLogManager.Instance);
                        if (null != item)
                        {
                            item.Index = ++this.TotalCount;
                            this.accountList.Add(item);
                        }

                        if (this.TotalCount % 10 == 0)
                        {
                            this.SetAutoResetEvent(false);
                            Application.DoEvents();
                        }
                    }
                }
            }
            finally
            {
                if (accountList.Count == 0)
                {
                    LogManager.InfoWithCallback("-> 请检查数据文件的格式是否正确，当前格式无法解析EMAIL、姓名!!!!");
                    this.IsFinished = true;
                }
                BattleDBHelper.SaveCacheData();
                LogManager.InfoWithCallback("-> 完成数据从文件提取,正在开始网络查询！");
                this.SetAutoResetEvent(true);
            }
        }

        #endregion Get methods

        protected override void ContinueFromLastStopped()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetDB()
        {
            throw new System.NotImplementedException();
        }
    }
}