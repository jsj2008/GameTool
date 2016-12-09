using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace PublicUtilities
{
    public interface IHttpManagerReconnect
    {
        void Reconnect();
    }

    public abstract class HttperManagerBase<T, P> : IHttpManagerReconnect, IDisposable
        where T : ItemBase, new()
        where P : GameHttperBase
    {
        //线程倍数
        protected const int STARTRATE = 5;

        protected AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        protected List<T> accountList = new List<T>();
        protected IList<HttpProcessorBase<T, P>> httpProcessorList = new List<HttpProcessorBase<T, P>>();

        protected object readLockObject = new object();
        protected object httpLockObject = new object();
        protected object eventLockObject = new object();

        public event EventHandler<ProcessEventArgs<T>> ProcessItemChanged;
        public event EventHandler<BoolEventArgs> ProcessCompleted;
        public event EventHandler<BoolEventArgs> ReconnectChanged;

        protected IList<int> queriedIndexList = new List<int>();

        #region Properties

        public DetectionParamsItem HttperParamsItem
        { get; protected set; }

        public int TotalCount
        { get; protected set; }

        protected bool isFinished = false;

        public bool IsFinished
        {
            get { return this.isFinished; }
            protected set
            {
                if (this.isFinished != value)
                {
                    this.isFinished = value;
                    if (this.isFinished)
                    {
                        this.RaiseProcessCompleted();
                    }
                }
            }
        }

        public int CurrentIndex
        {
            get;
            protected set;
        }

        public bool IsPaused
        {
            get;
            protected set;
        }

        /// <summary>
        /// 每IP可以访问的次数
        /// </summary>
        public int IPLimitCount
        {
            get;
            set;
        }

        /// <summary>
        /// 到达IP访问限止
        /// </summary>
        public bool IsIPLimit
        {
            get { return HttperParamsItem.IsIpAccessLimit && (IPLimitCount > 0) && ipAccessCount >= IPLimitCount; }
        }

        public LogManagerBase LogManager
        {
            get;
            set;
        }

        #endregion Properties

        public HttperManagerBase(LogManagerBase logManager)
        {
            this.LogManager = logManager;
            MultiGameNetManager.Register(this);
        }

        #region virtual Metholds

        StringBuilder sb = new StringBuilder();
        int queriedCount = 0;
        int ipAccessCount = -1;

        public virtual T GetNextItem()
        {
            try
            {
                Interlocked.Increment(ref ipAccessCount);
                if (IsIPLimit)
                {
                    //ipAccessCount = -1;
                    LogManager.InfoWithCallback(string.Format("当前每IP只允许访问 {0} 个帐号，准备重新连接网络！", IPLimitCount));
                    return null;
                }

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

                    if (this.CurrentIndex == 0) { queriedCount = 0; }

                    if ((this.accountList.Count > 0) && (this.CurrentIndex >= 0) && (this.CurrentIndex < this.accountList.Count))
                    {
                        T item = this.accountList[this.CurrentIndex++];
                        while ((null != item) && queriedIndexList.Contains(item.Index))
                        {
                            if (this.CurrentIndex < this.accountList.Count)
                            {
                                if (queriedCount++ == 0)
                                {
                                    sb.Append(string.Format("Below accounts has been queried:\n"));
                                }

                                sb.Append(string.Format("{0}->:{1}; ", item.Index, item.EMail));
                                if (queriedCount % 100 == 0)
                                {
                                    LogManager.Info(sb.ToString());
                                    sb = new StringBuilder();
                                    queriedCount = 0;
                                }
                                item = this.accountList[this.CurrentIndex++];
                            }
                            else
                            {
                                return null;
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

        public virtual P GetHttper()
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

        public abstract P GetHttper(GameServerType serverType);

        protected virtual void LoadFile()
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
                    string user = string.Empty;
                    string pwd = string.Empty;
                    string email = string.Empty;

                    while (!sr.EndOfStream && !isDisposed)
                    {
                        string line = sr.ReadLine();
                        if (TextToItemHelper.GetLoginAccountItem(line, ref user, ref pwd, ref email,
                            this.HttperParamsItem.DataFormat, this.LogManager))
                        {
                            T item = new T()
                            {
                                RawData = line,
                                User = user,
                                Password = pwd,
                                EMail = email,
                                Index = ++this.TotalCount
                            };
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
                    LogManager.InfoWithCallback("-> 请检查数据文件的格式是否正确，当前格式无法解析帐号、EMAIL、密码!!!!");
                    this.IsFinished = true;
                }
                LogManager.InfoWithCallback("-> 完成数据从文件提取,正在开始网络查询！");
                this.SetAutoResetEvent(true);
            }
        }

        #endregion virtual Metholds

        #region Control

        /// <summary>
        /// Stop all running threads
        /// </summary>
        public virtual void Stop()
        {
            this.IsFinished = true;
            this.PauseProcess();
        }

        /// <summary>
        /// Start all threads for get account info
        /// </summary>
        public virtual void Start(DetectionParamsItem detectionItem)
        {
            if (null == detectionItem)
            {
                return;
            }

            this.HttperParamsItem = detectionItem;
            this.CurrentIndex = 0;
            this.isSetAutoResetEvent = false;

            LogManager.InfoWithCallback(string.Format("-> 正在开始初始化对文件:{0} 在服务器:{1} 上的查询，请稍等.....\r\n",
                                                      detectionItem.DataFilePath, detectionItem.CurrentGameServer.Header));
            Application.DoEvents();
            Thread thread = new Thread(new ThreadStart(this.LoadFile));
            thread.Name = "Load data thread";
            thread.Start();
            this.WaitOneAutoResetEvent();

            this.SetProcessByCustomRange();
            this.ContinueFromLastStopped();
            this.StartProcess();
        }

        public virtual void RestartProcess()
        {
            if (this.IsPaused && !this.IsReconnecting)
            {
                LogManager.Info("Detection manager restart process ");
                this.StartProcess();
            }
        }

        /// <summary>
        /// Start http content detection
        /// </summary>
        protected virtual void StartProcess()
        {
            this.PauseProcess();
            this.IsPaused = false;
            this.isFinished = false;
            ipAccessCount = -1;
        }

        protected abstract void SetDB();

        /// <summary>
        /// Just pause process
        /// </summary>
        public virtual void PauseProcess()
        {
            lock (this)
            {
                try
                {
                    while (httpProcessorList.Count > 0)
                    {
                        HttpProcessorBase<T, P> hp = httpProcessorList[0];
                        hp.ProcessedItemChanged -= HttpProcess_ProcessItemChanged;
                        hp.ProcessCompleted -= HttpProcess_ProcessCompleted;
                        hp.ReconnectChanged -= HttpProcess_ReconnectChanged;
                        hp.Stop();
                        httpProcessorList.Remove(hp);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("Pause http process failed:{0}", ex.Message));
                }

                this.IsPaused = true;
                //Restart from 0, just check in DB
                this.CurrentIndex = 0;
                this.SetDB();
            }
        }

        #region Process events

        protected virtual void HttpProcess_ProcessItemChanged(object sender, ProcessEventArgs<T> e)
        {
            lock (eventLockObject)
            {
                UserAccountItem wowItem = e.Item as UserAccountItem;
                if ((null != wowItem) && wowItem.IsCaptchaError)
                {
                    if (!queriedIndexList.Contains(e.Item.Index))
                    {
                        queriedIndexList.Add(e.Item.Index);
                    }
                }

                if (IsReconnecting) return;

                if ((null != wowItem) && !queriedIndexList.Contains(wowItem.Index))
                {
                    queriedIndexList.Add(wowItem.Index);
                }

                if (null != this.ProcessItemChanged)
                {
                    this.ProcessItemChanged(sender, e);
                }

                if (IsIPLimit)
                {
                    LogManager.InfoWithCallback(string.Format("当前每IP只允许访问 {0} 个帐号，已经到达限止，重新网络！", IPLimitCount));
                    this.Reconnect();
                }
            }
        }

        protected void HttpProcess_ProcessCompleted(object sender, BoolEventArgs e)
        {
            lock (this)
            {
                foreach (HttpProcessorBase<T, P> hp in httpProcessorList)
                {
                    if (!hp.IsStopped)
                    {
                        return;
                    }
                }

                if (!HttperParamsItem.IsIpAccessLimit || !IsIPLimit)
                {
                    this.IsFinished = true;
                }
            }
        }

        protected virtual void HttpProcess_ReconnectChanged(object sender, ProcessEventArgs<T> e)
        {
            lock (this)
            {
                T userItem = e.Item;
                if (null == userItem) return;

                if (this.IsReconnecting)
                {
                    if (userItem.IsNeedRedial)
                    {
                        LogManager.InfoWithCallback(string.Format(" -->已经在网络重拨，当前用户：" + userItem.ToString()));
                    }
                    return;
                }

                if (userItem.IsNeedRedial)
                {
                    UserAccountItem wowItem = userItem as UserAccountItem;
                    if ((null != wowItem) && wowItem.IsCaptchaError)
                    {
                        BattleOutptMgt.Instance.Output(wowItem, this.HttperParamsItem);
                    }

                    if (userItem.State == WowLoginStates.TooMuchFailedChapcha)
                    {
                        LogManager.InfoWithCallback(string.Format(" -->验证码识别出错:{0} 次，网络重启。", this.HttperParamsItem.ErrorRepeatCount));
                    }

                    LogManager.InfoWithCallback(string.Format("-> 游戏网站:{0} 开始拒绝查询，准备重拨网络 ，帐户：{1}",
                                                          this.HttperParamsItem.CurrentGameServer.Header, userItem.ToString()));
                    this.Reconnect();
                }
            }
        }

        #endregion Process events

        /// <summary>
        /// Start from stopped or closed app
        /// </summary>
        protected virtual void ContinueFromLastStopped()
        {
            throw new System.NotImplementedException();
        }

        protected void SetProcessByCustomRange()
        {
            if (null == this.HttperParamsItem)
            {
                return;
            }

            if (this.HttperParamsItem.IsSupportedCustomRange)
            {
                this.CurrentIndex = this.HttperParamsItem.RangeLower;
            }
        }

        #endregion Control

        #region signal methods

        protected void WaitOneAutoResetEvent()
        {
            if (null == this.autoResetEvent)
            {
                this.autoResetEvent = new AutoResetEvent(false);
            }

            this.autoResetEvent.WaitOne();
        }

        protected bool isSetAutoResetEvent = false;

        /// <summary>
        /// Set autoresetevent , and let UI thread continue
        /// </summary>
        /// <param name="isLoaded">is all data loaded from file</param>
        protected void SetAutoResetEvent(bool isLoaded)
        {
            if (this.isSetAutoResetEvent)
            {
                return;
            }

            if (!isLoaded)
            {
                if (null != this.HttperParamsItem)
                {
                    int threadCount = this.HttperParamsItem.Threads;
                    if (this.TotalCount > (threadCount * STARTRATE))
                    {
                        this.autoResetEvent.Set();
                        this.isSetAutoResetEvent = true;
                    }
                }
            }
            else
            {
                this.autoResetEvent.Set();
                this.isSetAutoResetEvent = true;
            }
        }

        #endregion signal methods

        #region Events

        protected void RaiseProcessCompleted()
        {
            if (null != this.ProcessCompleted)
            {
                bool isCompleted = false;
                if (null != this.HttperParamsItem)
                {
                    if (this.CurrentIndex >= (this.TotalCount - 1))
                    {
                        LogManager.InfoWithCallback(string.Format("-> 完成对数据文件:{0} 在网站:{1}上的查询\r\n",
                        this.HttperParamsItem.DataFilePath, this.HttperParamsItem.CurrentGameServer.Header));
                        isCompleted = true;
                    }
                    else
                    {
                        LogManager.InfoWithCallback(string.Format("？？？？未有效完成对数据文件:{0} 在网站:{1}上的查询，请查实是否网络出错，手动停止或者其他原因？？？\r\n",
                          this.HttperParamsItem.DataFilePath, this.HttperParamsItem.CurrentGameServer.Header));
                    }
                }
                this.ProcessCompleted(this, new BoolEventArgs() { IsTrue = isCompleted });
            }
        }

        protected void OnReconnectChanged(bool isStarted)
        {
            if (null != this.ReconnectChanged)
            {
                this.ReconnectChanged(this, new BoolEventArgs() { IsTrue = isStarted });
            }
        }

        #endregion Events

        protected bool IsReconnecting
        {
            get { return lockCount > 0; }
        }

        private object lockRestartObject = new object();
        int lockCount = 0;

        public void Reconnect()
        {
            lock (lockRestartObject)
            {
                if (this.IsReconnecting)
                {
                    LogManager.InfoWithCallback(string.Format("->当前网络已经在重拨，请等待…………"));
                    return;
                }

                if (!this.HttperParamsItem.IsSupportedReconnect)
                {
                    LogManager.InfoWithCallback(string.Format("-> 当前没有设置网络重联功能，请检查！"));
                    this.Stop();
                    return;
                }

                SoundPlayer.PlayAlter();
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object obj)
                {
                    if (Interlocked.Increment(ref lockCount) > 1)
                    {
                        LogManager.InfoWithCallback(string.Format("->当前网络已经在重拨,当前网络重拨计数:{0}", lockCount));
                        return;
                    }

                    this.PauseProcess();
                    this.OnReconnectChanged(true);
                    LogManager.InfoWithCallback(string.Format("-> 游戏网站:{0} 开始拒绝查询，正在进行网络重连，请稍等............",
                                                              this.HttperParamsItem.CurrentGameServer.Header));
                    //AutoResetEvent reconnectEvent = null;
                    try
                    {
                        //reconnectEvent = new AutoResetEvent(false);
                        //bool isAllReconnect = MultiGameNetManager.CheckAll(this, reconnectEvent);
                        //if (!isAllReconnect && (null != reconnectEvent))
                        //{
                        //    reconnectEvent.WaitOne();
                        //}
                        //else
                        //{
                        ReconnectManager.Instance.ResetLogManager(GameLogManager.Instance);
                        MultiGameNetManager.Reconnect(this.HttperParamsItem);
                        //}

                        //if (null != reconnectEvent)
                        //{
                        //    reconnectEvent.Close();
                        //}
                    }
                    catch (Exception ex)
                    {
                        this.LogManager.Error(string.Format("reconnectEvent close error:{0}", ex.Message));
                    }
                    finally
                    {
                        //Interlocked.Decrement(ref lockCount);
                        Interlocked.Exchange(ref lockCount, 0);
                    }

                    if (MultiGameNetManager.IsConnected)
                    {
                        if (this.IsFinished || this.isDisposed) return;
                        LogManager.InfoWithCallback(string.Format("-> 网络重连完成，等待程序就绪！"));
                        this.RestartProcess();
                        LogManager.InfoWithCallback(string.Format("-> 程序就绪，正在重新启动查询功能,请稍等..........."));
                    }
                    else
                    {
                        LogManager.InfoWithCallback(string.Format("-> 网络重连失败，请检查原因！"));
                        this.Stop();
                    }
                    SoundPlayer.StopWarn();
                    this.OnReconnectChanged(false);
                }));
            }
        }
        #region IDisposable Members

        protected bool isDisposed = false;

        protected virtual void OnDispose()
        {
            this.Stop();
            this.SetDB();
            isDisposed = true;
        }

        public void Dispose()
        {
            MultiGameNetManager.UnRegister(this);
            OnDispose();
            isDisposed = true;
        }

        #endregion IDisposable Members
    }

    public class ProcessEventArgs<T> : EventArgs
        where T : ItemBase
    {
        public T Item
        {
            get;
            set;
        }
    }

    public enum QueryType
    {
        /// <summary>
        /// 从头开始扫描
        /// </summary>
        FromFrist,
        /// <summary>
        /// 从自定义范围开始扫描
        /// </summary>
        FromCustomRange,
        /// <summary>
        /// 从上次停止处开始扫描
        /// </summary>
        FromStopped
    }
}