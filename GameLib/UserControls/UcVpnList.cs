using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PublicUtilities
{
    public partial class UcVpnList : UserControl
    {
        VPNFile vpnFile = null;
        public LogManagerBase LogManager { get; set; }

        public UcVpnList(VPNFile vpn, LogManagerBase logManager)
        {
            InitializeComponent();
            this.Load += new EventHandler(UcVpnList_Load);
            this.vpnFile = vpn;
            this.LogManager = logManager;
        }

        void UcVpnList_Load(object sender, EventArgs e)
        {
            this.txtVpnName.Text = vpnFile.EntryName;
            this.txtFile.Text = vpnFile.File;
        }

        public string VpnFile
        {
            get { return this.txtFile.Text; }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openDialog.FileName))
            {
                this.txtFile.Text = openDialog.FileName;
            }
            else
            {
                MessageBox.Show("请选择一个VPN数据文件");
            }
        }

        public IList<VPNItem> GetVpnList()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            if (string.IsNullOrEmpty(txtVpnName.Text.Trim()))
            {
                sb.AppendLine(string.Format("{0}、VPN名称不能为空！", ++i));
            }
            if (string.IsNullOrEmpty(txtFile.Text) || !File.Exists(txtFile.Text) || string.IsNullOrEmpty(txtVpnName.Text.Trim()))
            {
                sb.AppendLine(string.Format("{0}、VPN数据不能为空或者不存在！", ++i));
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return null;
            }

            IList<VPNItem> vpnList = new List<VPNItem>();
            using (StreamReader sr = new StreamReader(txtFile.Text))
            {
                while (!sr.EndOfStream)
                {
                    VPNItem item = GetVpn(sr.ReadLine());
                    if (null != item)
                    {
                        vpnList.Add(item);
                    }
                }
            }

            if (vpnList.Count == 0)
            {
                MessageBox.Show("当前选择的VPN列表文件非有效格式，获取失败！");
                return null;
            }
            return vpnList;
        }

        private VPNItem GetVpn(string content)
        {
            string[] items = content.Split(TextToItemHelper.SplitChars, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 3)
            {
                return new VPNItem() { EntryName = txtVpnName.Text.Trim(), IP = items[0], User = items[1], Password = items[2] };
            }
            return null;
        }

        private void btnTestVpn_Click(object sender, EventArgs e)
        {
            Test();
        }

        public void Test()
        {
            IList<VPNItem> vpnList = this.GetVpnList();
            if ((null != vpnList) && (vpnList.Count > 0))
            {
                VPNManager vpnMgr = new VPNManager(this.LogManager);
                DetectionParamsItem paramsItem = new DetectionParamsItem();
                //VPNItem vpnItem = vpnList[0];
                paramsItem.ReconnectType = ReconnectType.VPN;
                paramsItem.VpnList = vpnList;
                //paramsItem.VpnList.Add(vpnItem);
                this.OnTestChanged(true);

                Thread t = new Thread(new ThreadStart(delegate()
                {
                    //LogManager.InfoWithCallback(string.Format("-> 正在开始对VPN: 名称＝{0}，IP＝{1} 进行网络重连测试，请稍等......", vpnItem.EntryName, vpnItem.IP));

                    bool isConnected = vpnMgr.Reconnect(paramsItem);
                   
                    //if (isConnected)
                    //{
                    //    LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连成功，网络恢复正常", vpnItem.EntryName, vpnItem.IP));
                    //}
                    //else
                    //{
                    //    LogManager.InfoWithCallback(string.Format("-> 对VPN: 名称＝{0}，IP＝{1} 网络重连失败，请检查IP，用户名或者密码是否正确！", vpnItem.EntryName, vpnItem.IP));
                    //}
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
