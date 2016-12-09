using System;
using System.Collections.Generic;

namespace PublicUtilities
{
    /// <summary>
    /// 帐户查询参数信息
    /// </summary>
    public class DetectionParamsItem
    {
        public DetectionParamsItem()
        {
            this.Router = new RouterItem();
            this.ADSL = new ADSLItem();
            this.VpnList = new List<VPNItem>();
            this.ReconnectTimeout = 30;
            this.IsGetDetail = true;
        }

        /// <summary>
        /// 当前选中游戏网站
        /// </summary>
        public GameServer CurrentGameServer { get; set; }

        public IList<GameServer> GameServerList { get; set; }

        public IList<PwdFormatOptions> PwdFormatOptions { get; set; }

        /// <summary>
        /// 原始数据格式
        /// </summary>
        public DataFormat DataFormat { get; set; }

        /// <summary>
        /// 原始数据路径
        /// </summary>
        public string DataFilePath { get; set; }

        /// <summary>
        /// Http网络连接超时
        /// </summary>
        public int HttpTimeout { get; set; }


        /// <summary>
        /// 扫多少用户后重启
        /// </summary>
        public int RedialCount { get; set; }

        /// <summary>
        /// 是否支持自定义数据行范围查询
        /// </summary>
        public bool IsSupportedCustomRange
        {
            get
            {
                return this.QueryType == QueryType.FromCustomRange;
            }
        }

        /// <summary>
        /// 自定义数据下标
        /// </summary>
        public int RangeLower { get; set; }

        /// <summary>
        ///自定义数据上标
        /// </summary>
        public int RangeUpper { get; set; }

        /// <summary>
        ///  支持线程数
        /// </summary>
        public int Threads { get; set; }

        /// <summary>
        ///  网络重连超时，必须大于30
        /// </summary>
        public int ReconnectTimeout { get; set; }

        /// <summary>
        /// 是否支持网络重连
        /// </summary>
        public bool IsSupportedReconnect { get; set; }

        /// <summary>
        /// 出错后自动重连次数
        /// </summary>
        public int ErrorRepeatCount { get; set; }

        /// <summary>
        /// 验证码识别出错重启数
        /// </summary>
        public int CaptchaErrorCount { get; set; }

        /// <summary>
        /// 是否以追加方式，继续从上次停止处开始查询
        /// </summary>
        public bool IsAppended
        {
            get
            {
                return this.QueryType == QueryType.FromStopped;
            }
        }

        /// <summary>
        /// 是否自动清除验证码图片
        /// </summary>
        public bool IsDeleteCaptcha { get; set; }

        /// <summary>
        /// 是否非战等级查询功能
        /// </summary>
        public bool IsGetUnbattleChar { get; set; }

        /// <summary>
        /// 是否查询子帐号明细
        /// </summary>
        public bool IsGetDetail { get; set; }

        /// <summary>
        /// 是否战网等级查询功能
        /// </summary>
        public bool IsGetBattleChar { get; set; }

        /// <summary>
        /// 是否查G值 
        /// </summary>
        public bool IsGetGValue { get; set; }

        /// <summary>
        /// 当扫到有效等级时直接结束
        /// </summary>
        public bool IsStopByLevel { get; set; }

        private int availableCharacterLevel = 70;
        public int AvailableCharacterLevel
        {
            get { return availableCharacterLevel; }
            set
            {
                availableCharacterLevel = value;
            }
        }

        /// <summary>
        /// 战网自动切换
        /// </summary>
        public bool IsAutoSwitch { get; set; }

        /// <summary>
        /// 每IP访问次数限止
        /// </summary>
        public bool IsIpAccessLimit{get;set;}

        /// <summary>
        /// 数据查询方式(从头开始/自定义范围/从上次结束开始)
        /// </summary>
        public QueryType QueryType { get; set; }

        /// <summary>
        /// 网络重连方式
        /// </summary>
        public ReconnectType ReconnectType { get; set; }

        public RouterItem Router { get; set; }

        public ADSLItem ADSL { get; set; }

        private IList<VPNItem> vpnList = null;

        public IList<VPNItem> VpnList
        {
            get { return vpnList ?? new List<VPNItem>(); }
            set
            {
                if (this.vpnList != value)
                {
                    this.vpnList = value;
                }
            }
        }

        public bool IsSupportProxy { get; set; }

        private IList<ProxyItem> proxyList = null;
        public IList<ProxyItem> ProxyList
        {
            get { return proxyList ?? new List<ProxyItem>(); }
            set
            {
                if (this.proxyList != value)
                {
                    this.proxyList = value;
                }
            }
        }

        public ProxyItem GetAvaiableProxy()
        {
            if (!this.IsSupportProxy)
            {
                return null;
            }

            lock (ProxyList)
            {
                foreach (ProxyItem item in ProxyList)
                {
                    if (item.IsAccessable)
                    {
                        item.UsedCount++;
                        return item;
                    }
                }

                //If all proxy used , start from the first again
                foreach (ProxyItem item in ProxyList)
                {
                    item.UsedCount = 0;
                }

                if (ProxyList.Count > 0)
                {
                    return ProxyList[0];
                }
                return null;
            }
        }
    }

    public class RouterItem
    {
        public RouterType RouterType { get; set; }

        public string IP { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    public class VPNItem : ADSLItem
    {
        private int port = 80;

        public int Port
        {
            get { return this.port; }
            set
            {
                if (port != value)
                {
                    this.port = value;
                }
            }
        }

        public string IP { get; set; }
    }


    public class ProxyItem
    {
        readonly int MaxCount = WowSetConfig.Instance.ProxyAcessTimes;

        public ProxyItem(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
        }

        public int ID { get; set; }
        public int Port { get; set; }
        public string IP { get; set; }

        /// <summary>
        /// get Second
        /// </summary>
        private double acctimeTime = 0;
        public double AccessTime
        {
            get { return acctimeTime; }
            set { acctimeTime = value; }
        }

        public bool IsNeedCaptcha { get; set; }

        public bool IsAccessable { get; set; }

        public int UsedCount { get; set; }

        public bool IsAvaiable
        {
            get
            {
                return IsAccessable && UsedCount < MaxCount;
            }
        }
    }

    public class VPNFile : ADSLItem
    {
        public string File { get; set; }
    }

    public class ADSLItem
    {
        public ADSLItem()
        {
            this.EntryName = "宽带连接";
        }
        /// <summary>
        ///  name
        /// </summary>
        public string EntryName { get; set; }

        /// <summary>
        /// login  account
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// login  password
        /// </summary>
        public string Password { get; set; }
    }

    public class GameServer
    {
        public string Header { get; set; }

        public GameServerType GameServerType { get; set; }

        /// <summary>
        /// 游戏帐号选择网址
        /// </summary>
        public string SelectAccountUrl { get; set; }

        /// <summary>
        /// 游戏帐号详细信息网址
        /// </summary>
        public string AccountDetailUrl { get; set; }

        /// <summary>
        /// 网站首页网址
        /// </summary>
        public string DomainUrl { get; set; }

        /// <summary>
        /// 帐号登录网址(先进入这个网页，取得SESSION之类再POST)
        /// </summary>
        public string LoginUrl { get; set; }
        /// <summary>
        /// 帐号登录POST网址，先进入帐号登录网址
        /// </summary>
        public string LoginPostActionUrl { get; set; }

        /// <summary>
        /// 忘记密码重新验证
        /// </summary>
        public string PasswordResetUrl { get; set; }

        /// <summary>
        /// 非战角色查询
        /// </summary>
        public string UnBattleCharacterUrl { get; set; }

        public string UnBattleLoginUrl { get; set; }

        /// <summary>
        /// 战网角色查询
        /// </summary>
        public string BattleCharacterUrl { get; set; }
        /// <summary>
        /// 战网G选择角色查询
        /// </summary>
        public string BattleGSelectCharUrl { get; set; }
        /// <summary>
        /// 战网G查询
        /// </summary>
        public string BattleGetGUrl { get; set; }
        /// <summary>
        /// 战网G查询Money
        /// </summary>
        public string BattleGetGUrlMoney { get; set; }

        public int UsedCount { get; set; }
        public bool IsAvaiable
        {
            get
            {
                return UsedCount < WowSetConfig.Instance.IPAcessTimes;
            }
        }
    }

    public class RouterComboxItem
    {
        public RouterType RouterType { get; set; }

        public string Header
        {
            get { return CommentAttributeGetter.GetAttribute<RouterType>(this.RouterType); }
        }
    }

    public class ProgressEventArgs : EventArgs
    {
        public long CurrentSize { get; set; }

        public long TotalSize { get; set; }

        public long CurrentLine { get; set; }

        public int CurrentPercent
        {
            get { return (int)((1.0 * CurrentSize / TotalSize) * 100); }
        }

        public ProgressEventArgs(long currentCount, long currentSize, long totalSize)
        {
            this.CurrentLine = currentCount;
            this.CurrentSize = currentSize;
            this.TotalSize = totalSize;
        }
    }

    public class GPUAppItem
    {
        public string Title { get; set; }

        public GPUAppType GpuApp { get; set; }
    }

    public class DataTypeItem
    {
        public string Text { get; set; }

        public DataFormat Type { get; set; }
    }

    public class DataInsertParams
    {
        public string RawFilePath { get; set; }

        public DataType Type { get; set; }

        public DataFormat Format { get; set; }

        public string TargetSplitChars { get; set; }

        public string RawSplitChars { get; set; }

        public bool IsCanAdd { get; set; }

        public bool IsAddBefore { get; set; }

        public bool IsAddAfter { get; set; }

        public int AddIndex { get; set; }

        public bool IsCanDel { get; set; }

        public bool IsDelBefore { get; set; }

        public bool IsDelAfter { get; set; }

        public int DelIndex { get; set; }

        public string Add3Chars { get; set; }

        public string Add4Chars { get; set; }

        public string Add5Chars { get; set; }

        public string Add6Chars { get; set; }

        public string Add7Chars { get; set; }

        public string Add8Chars { get; set; }

        public string Add9Chars { get; set; }

        public string AddSpecialChars { get; set; }
    }
}