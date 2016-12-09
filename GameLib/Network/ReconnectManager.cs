using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using DotRas;

namespace PublicUtilities
{
    public interface IReconnectManager
    {
        bool Reconnect(DetectionParamsItem detectionItem);
    }

    public class ReconnectManager : IReconnectManager
    {
        public const int TRYCOUNT = 100;

        public static void Sleep()
        {
            Thread.Sleep(8 * 1000);
        }

        public RouterManager routerManager { get; private set; }
        public ADSLManager adslManager { get; private set; }
        public VPNManager VPNManager { get; private set; }
        public LogManagerBase LogManager { get; private set; }

        public static readonly ReconnectManager Instance = new ReconnectManager(WowLogManager.Instance);
        private IDictionary<ReconnectType, IReconnectManager> reconnectFactories = new Dictionary<ReconnectType, IReconnectManager>();

        private ReconnectManager(LogManagerBase logManager)
        {
            this.routerManager = new RouterManager(logManager);
            reconnectFactories.Add(ReconnectType.Router, this.routerManager);
            this.adslManager = new ADSLManager(logManager);
            reconnectFactories.Add(ReconnectType.ADSL, this.adslManager);
            this.VPNManager = new VPNManager(logManager);
            reconnectFactories.Add(ReconnectType.VPN, this.VPNManager);

            //NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            //NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            //SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        public void ResetLogManager(LogManagerBase logManager)
        {
            if (this.LogManager == logManager) return;
            this.LogManager = logManager;
            this.routerManager.LogManager = logManager;
            this.adslManager.LogManager = logManager;
            this.VPNManager.LogManager = logManager;
        }

        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            LogManager.Error(string.Format("SystemEvents_PowerModeChanged() called,e.Mode={0}", e.Mode));
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            LogManager.Error(string.Format("NetworkChange_NetworkAvailabilityChanged() called,e.IsAvailable={0}", e.IsAvailable));
        }

        void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            LogManager.Error("NetworkChange_NetworkAddressChanged() called");
        }

        private IReconnectManager GetManager(ReconnectType connType)
        {
            if (!this.reconnectFactories.ContainsKey(connType))
            {
                LogManager.Error(string.Format(" Don't have a ReconnectType :{0}", connType));
                return null;
            }
            return this.reconnectFactories[connType];
        }

        public bool Reconnect(DetectionParamsItem detectionItem)
        {
            if (null == detectionItem)
            {
                LogManager.Error("重连参数不能为空！");
                return false;
            }
            try
            {
                if (detectionItem.IsSupportedReconnect)
                {
                    IReconnectManager reconMgr = GetManager(detectionItem.ReconnectType);
                    //LogManager.InfoWithCallback(string.Format("-> 开始重连，连接方式：{0}", CommentAttributeGetter.GetAttribute<ReconnectType>(detectionItem.ReconnectType)));
                    bool isConnected = false;
                    ManualResetEvent reconnectManualReset = new ManualResetEvent(false);
                    Thread t = new Thread(new ThreadStart(delegate()
                        {
                            isConnected = reconMgr.Reconnect(detectionItem);
                            reconnectManualReset.Set();
                        }));
                    t.Start();
                    LogManager.InfoWithCallback(string.Format("-> 开始重连，连接方式：{0},请等待", CommentAttributeGetter.GetAttribute<ReconnectType>(detectionItem.ReconnectType)));
                    reconnectManualReset.WaitOne();
                    reconnectManualReset.Reset();
                    return isConnected;
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("ReconnectManager.Reconnect() error:{0}", ex.Message));
            }

            LogManager.InfoWithCallback("-> 当前网络重连不能正常运行，是否选择网络重连或者参数是否异常，请仔细检查！");
            return false;
        }

        public string VpnIp
        {
            get
            {
                IPHostEntry hosts = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in hosts.AddressList)
                {
                    if (TextHelper.IsIP(ip.ToString()))
                    {
                        Trace.WriteLine(ip.ToString());
                        return ip.ToString();
                    }
                }

                LogManager.Error("本地IP地址不能为空！");
                return string.Empty;
            }
        }
    }
}
