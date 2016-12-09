using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using DotRas;

namespace PublicUtilities
{
    public class VPNManager : IReconnectManager
    {
        private ManualResetEvent vpnManualReset = new ManualResetEvent(false);
        private IList<VPNItem> vpnList = null;

        public VPNItem CurrentVpnItem { get; set; }
        public LogManagerBase LogManager { get; set; }

        public VPNManager(LogManagerBase logManager)
        {
            this.LogManager = logManager;
        }

        private void GetVpnItem(DetectionParamsItem detectionItem)
        {
            if ((null == detectionItem) || (null == detectionItem.VpnList)) return;
            if (vpnList != detectionItem.VpnList)
            {
                this.vpnList = detectionItem.VpnList;
            }

            if ((null == this.CurrentVpnItem) && (this.vpnList.Count > 0))
            {
                this.CurrentVpnItem = this.vpnList[0];
                return;
            }

            if (null != this.CurrentVpnItem)
            {
                if (this.vpnList.Contains(this.CurrentVpnItem))
                {
                    int index = this.vpnList.IndexOf(this.CurrentVpnItem);
                    if (index < (this.vpnList.Count - 2))
                    {
                        this.CurrentVpnItem = this.vpnList[++index];
                        return;
                    }
                }

                //if ((this.vpnList.Count > 0))
                //{
                //    this.CurrentVpnItem = this.vpnList[0];
                //}
            }

            this.CurrentVpnItem = null;
        }

        public bool Reconnect(DetectionParamsItem detectionItem)
        {
            if ((null == detectionItem) || (null == detectionItem.VpnList))
            {
                LogManager.Error("VPN parameters can't be empty");
                return false;
            }

            bool isConnected = false;
            int i = 0;
            do
            {
                this.GetVpnItem(detectionItem);
                if (null == this.CurrentVpnItem)
                {
                    LogManager.Error("VPN服务器参数获取出错！");
                    return false;
                }
                LogManager.InfoWithCallback(string.Format("-> 正在开始对VPN: 名称＝{0}，IP＝{1} 进行网络重连测试，请稍等......", CurrentVpnItem.EntryName, CurrentVpnItem.IP));

                isConnected = Reconnect(this.CurrentVpnItem);
                if (isConnected)
                {
                    LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连成功，网络恢复正常", CurrentVpnItem.EntryName, CurrentVpnItem.IP));
                }
                else
                {
                    LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连失败，请检查IP，用户名或者密码是否正确！", CurrentVpnItem.EntryName, CurrentVpnItem.IP));
                }

            } while ((i++ < 10) && !isConnected);
            return isConnected;
        }

        public bool Reconnect(VPNItem item)
        {
            if ((null == item))
            {
                LogManager.Error("VPN服务器参数获取出错！");
                return false;
            }

            if (this.CurrentVpnItem != item)
            {
                this.CurrentVpnItem = item;
            }

            if (string.IsNullOrEmpty(this.CurrentVpnItem.EntryName) ||
                string.IsNullOrEmpty(this.CurrentVpnItem.IP) ||
                string.IsNullOrEmpty(this.CurrentVpnItem.User) ||
                string.IsNullOrEmpty(this.CurrentVpnItem.Password))
            {
                LogManager.Error("VPN 拨号参数不能为空");
                return false;
            }
            if (!this.Reconnect())
            {
                return false;
            }
            ReconnectManager.Sleep();
            //bool isConnected = CmdHelper.Ping(this.CurrentVpnItem.IP);
            //if (!isConnected)
            {
                // The connection attempt has completed, attempt to find the connection in the active connections.
                foreach (RasConnection connection in RasConnection.GetActiveConnections())
                {
                    if (connection.EntryName == this.CurrentVpnItem.EntryName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Reconnect()
        {
            VPNDialer vpndialer = null;
            Thread t = new Thread(new ThreadStart(delegate()
            {
                vpndialer = new VPNDialer(CurrentVpnItem, this.vpnManualReset, this.LogManager);
                vpndialer.Dial();
            }));
            t.Name = "VPN thread";
            t.Start();

            this.vpnManualReset.WaitOne();
            this.vpnManualReset.Reset();
            return vpndialer.IsDialSucceed;
        }
    }
}
