using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace PublicUtilities
{
    public partial class UcVpnItem : UserControl
    {
        VPNItem vpnItem = null;
        public LogManagerBase LogManager { get; set; }
        public UcVpnItem(VPNItem item, LogManagerBase logManager)
        {
            InitializeComponent();
            this.vpnItem = item;
            this.LogManager = logManager;
            this.Load += UcVpnItem_Load;
        }

        void UcVpnItem_Load(object sender, EventArgs e)
        {
            if (null != this.vpnItem)
            {
                this.txtVpnName.Text = vpnItem.EntryName;
                this.txtVpnUser.Text = vpnItem.User;
                this.txtVpnPwd.Text = vpnItem.Password;
                this.txtVpnIP.Text = vpnItem.IP;
            }
        }

        public VPNItem GetVpnParams()
        {
            #region  VPN

            VPNItem vpnItem = new VPNItem();
            StringBuilder sb = new StringBuilder();
            int i = 1;

            string vpnName = this.txtVpnName.Text.Trim();
            if (!string.IsNullOrEmpty(vpnName))
            {
                vpnItem.EntryName = vpnName;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的VPN连接名称！", i++));
            }

            string vpnIP = this.txtVpnIP.Text.Trim();
            if (!string.IsNullOrEmpty(vpnIP) && TextHelper.IsIP(vpnIP))
            {
                vpnItem.IP = vpnIP;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、您录入的不是有效的VPN IP地址！", i++));
            }

            string vpnUser = this.txtVpnUser.Text.Trim();
            if (!string.IsNullOrEmpty(vpnUser))
            {
                vpnItem.User = vpnUser;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的VPN登录用户名！", i++));
            }

            string vpnPwd = this.txtVpnPwd.Text.Trim();
            if (!string.IsNullOrEmpty(vpnPwd))
            {
                vpnItem.Password = vpnPwd;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的VPN登录密码！", i++));
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return null;
            }

            return vpnItem;

            #endregion
        }

        private void btnTestVpn_Click(object sender, EventArgs e)
        {
            VPNManager vpnMgr = new VPNManager(this.LogManager);
            VPNItem vpnItem = this.GetVpnParams();
            if (null != vpnItem)
            {
                this.OnTestChanged(true);
                Thread t = new Thread(new ThreadStart(delegate()
                {
                    LogManager.InfoWithCallback(string.Format("-> 正在开始对VPN: 名称＝{0}，IP＝{1} 进行网络重连测试，请稍等......", vpnItem.EntryName, vpnItem.IP));

                    bool isConnected = vpnMgr.Reconnect(vpnItem);
                    if (isConnected)
                    {
                        LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连成功，网络恢复正常", vpnItem.EntryName, vpnItem.IP));
                    }
                    else
                    {
                        LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连失败，请检查IP，用户名或者密码是否正确！", vpnItem.EntryName, vpnItem.IP));
                    }
                    this.OnTestChanged(false);
                }));
                t.Start();
            }
        }

        public event EventHandler<BoolEventArgs> TestChanged;
        private void OnTestChanged(bool isStarted)
        {
            if (null != this.TestChanged)
            {
                this.TestChanged(this, new BoolEventArgs() { IsTrue = isStarted });
            }
        }
    }
}
