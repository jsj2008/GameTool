using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PublicUtilities
{

    ///创建PPPOE，并选上 允许其他人使用此连接,否则下面的文件没法创建
    ///完成PPPOE创建后检查这个文件是否存在 "C:\ProgramData\Microsoft\Network\Connections\Pbk\rasphone.pbk"
    ///用程序中PPPOE测试来连接ADSL，再开始程序扫号
    public partial class UcAdsl : UserControl
    {
        ADSLItem adslItem = null;
        public LogManagerBase LogManager { get; set; }
        public UcAdsl(ADSLItem adsl, LogManagerBase logManager)
        {
            InitializeComponent();
            this.Load += UcAdsl_Load;
            this.adslItem = adsl;
            this.LogManager = logManager;
        }

        void UcAdsl_Load(object sender, EventArgs e)
        {
            if (null != adslItem)
            {
                this.txtADSLName.Text = adslItem.EntryName;
                this.txtADSLUser.Text = adslItem.User;
                this.txtADSLPwd.Text = adslItem.Password;
            }
        }

        public ADSLItem GetADSLItem()
        {
            #region  Router

            ADSLItem adslItem = new ADSLItem();
            StringBuilder sb = new StringBuilder();
            int i = 1;

            string adslName = this.txtADSLName.Text.Trim();
            if (!string.IsNullOrEmpty(adslName))
            {
                adslItem.EntryName = adslName;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的ADSL拨号连接名称！", i++));
            }

            string adslUser = this.txtADSLUser.Text.Trim();
            if (!string.IsNullOrEmpty(adslUser))
            {
                adslItem.User = adslUser;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的ADSL拨号用户名！", i++));
            }

            string adslPwd = this.txtADSLPwd.Text.Trim();
            if (!string.IsNullOrEmpty(adslPwd))
            {
                adslItem.Password = adslPwd;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的ADSL拨号密码！！", i++));
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return null;

            }
            return adslItem;

            #endregion
        }

        public void Dial()
        {
            ADSLManager adslMgr = new ADSLManager(this.LogManager);
            DetectionParamsItem paramsItem = new DetectionParamsItem();
            ADSLItem adsl = this.GetADSLItem();
            if (null != adsl)
            {
                paramsItem.ReconnectType = ReconnectType.ADSL;
                paramsItem.ADSL = adsl;
                this.OnTestChanged(true);
                Thread t = new Thread(new ThreadStart(delegate()
                {
                    LogManager.InfoWithCallback(string.Format("-> 正在开始对PPPOE: 名称＝'{0}' 进行网络重连测试，请稍等......", adsl.EntryName));
                    bool isConnected = adslMgr.Reconnect(paramsItem);
                    if (isConnected)
                    {
                        LogManager.InfoWithCallback(string.Format("-> 对PPPOE: 名称＝'{0}' 网络重连成功，网络恢复正常", adsl.EntryName));
                    }
                    else
                    {
                        LogManager.InfoWithCallback(string.Format("-> 对PPPOE: 名称＝'{0}' 网络重连失败，请检查名称，用户名或者密码是否正确！", adsl.EntryName));
                    }
                    this.OnTestChanged(false);
                }));
                t.Start();
            }
        }

        private void btnTestAdsl_Click(object sender, EventArgs e)
        {
            Dial();
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
