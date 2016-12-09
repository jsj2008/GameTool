using System;
using System.Diagnostics;

namespace PublicUtilities
{
    /// <summary>
    /// 非/战配置参数
    /// </summary>
    public class WowSetConfig
    {
        private readonly static string BattleXML = "战网对号.Xml";
        public readonly static WowSetConfig Instance = new WowSetConfig();

        protected XmlIniFile XmlFile = null;
        protected LogManagerBase LogManager = WowLogManager.Instance;

        public WowSetConfig()
        {
            XmlFile = new XmlIniFile(WowSetConfig.BattleXML);
        }

        public void Save()
        {
            LogManager.Info("Start write params to params file");
            try
            {
                this.XmlFile.SaveFile();
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("Save params file error:{0}", ex.Message));
            }
        }

        /// <summary>
        /// Is completed
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsCompleted", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsCompleted", value);
            }
        }


        #region  非/战配置参数

        /// <summary>
        /// 上次操作的数据文件
        /// </summary>
        public string LastFile
        {
            get { return this.XmlFile.ReadString("Httper", "LastFile", ""); }
            set { this.XmlFile.WriteString("Httper", "LastFile", value); }
        }

        /// <summary>
        /// 线程数
        /// </summary>
        public int Threads
        {
            get { return this.XmlFile.ReadInt("Httper", "Threads", 5); }
            set { this.XmlFile.WriteInt("Httper", "Threads", value); }
        }

        /// <summary>
        /// 连接超时（单位：秒）
        /// </summary>
        public int HttpTimeout
        {
            get { return this.XmlFile.ReadInt("Httper", "HttpTimeout", 90); }
            set { this.XmlFile.WriteInt("Httper", "HttpTimeout", value); }
        }

        /// <summary>
        /// 验证码出错重启数
        /// </summary>
        public int CaptchCount
        {
            get { return this.XmlFile.ReadInt("Httper", "CaptchCount", 5); }
            set { this.XmlFile.WriteInt("Httper", "CaptchCount", value); }
        }

        /// <summary>
        /// 游戏服务器编号
        /// </summary>
        public int GameServerNo
        {
            get { return this.XmlFile.ReadInt("Httper", "GameServerNo", 0); }
            set { this.XmlFile.WriteInt("Httper", "GameServerNo", value); }
        }

        public DataFormat DataFormat
        {
            get
            {
                string value = this.XmlFile.ReadString("Httper", "DataFormat", DataFormat.MailPassword.ToString());
                return (DataFormat)Enum.Parse(typeof(DataFormat), value);
            }
            set
            {
                this.XmlFile.WriteString("Httper", "DataFormat", value.ToString());
            }
        }

        public QueryType QuertyType
        {
            get
            {
                string value = this.XmlFile.ReadString("Httper", "QuertyType", QueryType.FromFrist.ToString());
                return (QueryType)Enum.Parse(typeof(QueryType), value);
            }
            set
            {
                this.XmlFile.WriteString("Httper", "QuertyType", value.ToString());
            }
        }


        public ReconnectType ReconnectType
        {
            get
            {
                string value = this.XmlFile.ReadString("Httper", "ReconnectType", ReconnectType.ADSL.ToString());
                return (ReconnectType)Enum.Parse(typeof(ReconnectType), value);
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ReconnectType", value.ToString());
            }
        }

        public RouterType RouterType
        {
            get
            {
                string result = this.XmlFile.ReadString("Httper", "RouterType", RouterType.TL_R402.ToString());
                return (RouterType)Enum.Parse(typeof(RouterType), result);
            }
            set
            {
                this.XmlFile.WriteString("Httper", "RouterType", value.ToString());
            }
        }

        public string RouterIP
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "RouterIP", "192.168.1.1");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "RouterIP", value);
            }
        }

        public string RouterUser
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "RouterUser", "admin");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "RouterUser", value);
            }
        }

        public string RouterPwd
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "RouterPwd", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "RouterPwd", value);
            }
        }

        /// <summary>
        /// 是否支持网络重连
        /// </summary>
        public bool IsSupportedReconnect
        {
            get
            {
                return this.XmlFile.ReadBool("Httper", "IsSupportedReconnect", false);
            }
            set
            {
                this.XmlFile.WriteBool("Httper", "IsSupportedReconnect", value);
            }
        }

        public string ADSLName
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ADSLName", "宽带连接");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ADSLName", value);
            }
        }

        public string ADSLUser
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ADSLUser", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ADSLUser", value);
            }
        }

        public string ADSLPwd
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ADSLPwd", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ADSLPwd", value);
            }
        }

        public string VpnEntryName
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "VpnEntryName", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "VpnEntryName", value);
            }
        }

        public string VpnFile
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "VpnFile", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "VpnFile", value);
            }
        }

        public string VpnUser
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "VpnUser", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "VpnUser", value);
            }
        }

        public string VpnPwd
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "VpnPwd", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "VpnPwd", value);
            }
        }

        public string VpnIP
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "VpnIP", "127.0.0.1");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "VpnIP", value);
            }
        }

        /// <summary>
        /// 出错重复次数
        /// </summary>
        public int ErrorRepeatCount
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "ErrorRepeatCount", 0);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "ErrorRepeatCount", value);
            }
        }

        public int RangeLower
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "RangeLower", 0);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "RangeLower", value);
            }
        }

        public int RedialCount
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "RedialCount",3);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "RedialCount", value);
            }
        }

        public int RangeUpper
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "RangeUpper", 0);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "RangeUpper", value);
            }
        }

        public bool IsDeleteCaptcha
        {
            get
            {
                return this.XmlFile.ReadBool("Httper", "IsDeleteCaptcha", true);
            }
            set
            {
                this.XmlFile.WriteBool("Httper", "IsDeleteCaptcha", value);
            }
        }

        public bool IsProxyEnabled
        {
            get
            {
                return this.XmlFile.ReadBool("Httper", "IsProxyEnabled", false);
            }
            set
            {
                this.XmlFile.WriteBool("Httper", "IsProxyEnabled", value);
            }
        }

        public string ProxyIP
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ProxyIP", "127.0.0.1");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ProxyIP", value);
            }
        }

        public string ProxyPort
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ProxyPort", "80");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ProxyPort", value);
            }
        }

        public string ProxyFile
        {
            get
            {
                return this.XmlFile.ReadString("Httper", "ProxyFile", "");
            }
            set
            {
                this.XmlFile.WriteString("Httper", "ProxyFile", value);
            }
        }

        public int ProxyThreads
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "ProxyThreads", 10);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "ProxyThreads", value);
            }
        }

        public int ProxyTestChunck
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "ProxyTestChunck", 30);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "ProxyTestChunck", value);
            }
        }

        /// <summary>
        /// 每个代理使用次数
        /// </summary>
        public int ProxyAcessTimes
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "ProxyAcessTimes", 2);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "ProxyAcessTimes", value);
            }
        }

        /// <summary>
        /// 每个IP使用次数
        /// </summary>
        public int IPAcessTimes
        {
            get
            {
                return this.XmlFile.ReadInt("Httper", "IPAcessTimes", 2);
            }
            set
            {
                this.XmlFile.WriteInt("Httper", "IPAcessTimes", value);
            }
        }

        #endregion

    }
}
