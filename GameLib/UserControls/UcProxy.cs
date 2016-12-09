using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PublicUtilities.UserControls
{
    public partial class UcProxy : UserControl
    {
        public UcProxy()
        {
            InitializeComponent();
            this.Load += new EventHandler(UcProxy_Load);
        }
        public static bool IsDesignMode()
        {
            bool returnFlag = false;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                returnFlag = true;
            }

            return returnFlag;
        }

        void UcProxy_Load(object sender, EventArgs e)
        {
            if (!IsDesignMode())
            {
                this.txtProxyIP.Text = WowSetConfig.Instance.ProxyIP;
                this.txtProxyIP.Enabled = WowSetConfig.Instance.IsProxyEnabled;
                this.txtProxyPort.Text = WowSetConfig.Instance.ProxyPort;
                this.txtProxyPort.Enabled = WowSetConfig.Instance.IsProxyEnabled;

                this.txtProxyFile.Text = WowSetConfig.Instance.ProxyFile;
            }
        }

        public string IP
        {
            get { return this.txtProxyIP.Text; }
            set { this.txtProxyIP.Text = value; }
        }
        public string Port
        {
            get { return this.txtProxyPort.Text; }
            set { this.txtProxyPort.Text = value; }
        }
        public string ProxyFile
        {
            get { return this.txtProxyFile.Text; }
            set { this.txtProxyFile.Text = value; }
        }

        public GameServer Server { get; set; }
        public bool IsSupportProxy
        {
            get { return this.radioAutoProxy.Checked || this.radioMannualProxy.Checked; }
        }
        public IList<ProxyItem> ProxyList
        {
            get
            {
                IList<ProxyItem> proxyList = new List<ProxyItem>();
                if (this.radioMannualProxy.Checked)
                {
                    proxyList.Add(new ProxyItem(this.txtProxyIP.Text, TextHelper.StringToInt(txtProxyPort.Text.Trim())));
                }

                if (this.radioAutoProxy.Checked)
                {
                    proxyList = GetProxyList(this.txtProxyFile.Text);
                }
                return proxyList;
            }
        }

        private void radioAutoProxy_CheckedChanged(object sender, EventArgs e)
        {
            btnProxyFile.Enabled = radioAutoProxy.Checked;
            txtProxyFile.Enabled = radioAutoProxy.Checked;
            if (radioAutoProxy.Checked && !string.IsNullOrEmpty(this.txtProxyFile.Text) &&
                File.Exists(this.txtProxyFile.Text))
            {
                btnProxySpeed.Visible = true;
                btnStopProxy.Visible = true;
                btnStopProxy.Enabled = false;
            }
            else
            {
                btnProxySpeed.Visible = false;
                btnStopProxy.Visible = false;
            }
        }

        private void radioMannualProxy_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyIP.Enabled = radioMannualProxy.Checked;
            txtProxyPort.Enabled = radioMannualProxy.Checked;
        }

        private void btnProxyFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openDialog.FileName))
            {
                this.txtProxyFile.Text = openDialog.FileName;
                btnProxySpeed.Visible = true;
            }
            else
            {
                MessageBox.Show("请选择一个有效代理列表文件", "提醒");
            }
        }

        ProxySpeedTester proxyTester = null;
        private void btnProxySpeed_Click(object sender, EventArgs e)
        {
            string proxyFile = this.txtProxyFile.Text;
            if (string.IsNullOrEmpty(proxyFile) || !File.Exists(proxyFile))
            {
                MessageBox.Show("请选择一个有效代理列表文件", "（WOW魔兽）提醒");
                return;
            }

            if (null == Server) return;

            IList<ProxyItem> proxyList = GetProxyList(proxyFile);
            proxyTester = new ProxySpeedTester(Server, WowLogManager.Instance, proxyList, proxyFile);
            proxyTester.Start();

            btnStopProxy.Visible = true;
            btnStopProxy.Enabled = true;
            btnProxySpeed.Enabled = false;
        }

        private void btnStopProxy_Click(object sender, EventArgs e)
        {
            if (null != proxyTester)
            {
                proxyTester.Dispose();
            }
            btnStopProxy.Enabled = false;
            btnProxySpeed.Enabled = true;
        }

        private IList<ProxyItem> GetProxyList(string proxyFile)
        {
            IList<ProxyItem> proxyList = new List<ProxyItem>();
            if (string.IsNullOrEmpty(proxyFile) || !File.Exists(proxyFile))
            {
                return proxyList;
            }

            using (StreamReader sr = new StreamReader(proxyFile))
            {
                string ip = string.Empty;
                int port = 0;
                int index = 1;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (TextToItemHelper.GetProxy(line, ref ip, ref port))
                    {
                        ProxyItem item = new ProxyItem(ip, port) { ID = index++ };
                        proxyList.Add(item);
                    }
                }
            }
            return proxyList;
        }

        public void Stop()
        {
            btnStopProxy_Click(null, null);
        }
    }
}
