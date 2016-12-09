using System.Windows.Forms;
using QQTest.cn.com.webxml.www;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QQTest
{
    public partial class FrmQQOnline : Form
    {
        public FrmQQOnline()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            LoadQQ();
            StartCheckOnline();
        }

        private void StartCheckOnline()
        {
            qqOnlineWebService qqOnline = new qqOnlineWebService();

            foreach (QQItem qq in qqList)
            {
                qq.CheckState(qqOnline);
            }
            qqView.DataSource = null;
            qqView.DataSource = qqList;
        }

        private void StartChat(QQItem qq)
        {
            const string qqChatUrl = "http://wpasig.qq.com/msgrd?v=1&uin={0}&site=microsoft&menu=yes";
            string qqChat = string.Format(qqChatUrl, qq.QQ);
            Process.Start(qqChat);
        }

        private List<QQItem> qqList = new List<QQItem>();
        private void LoadQQ()
        {
            string qqString = txtQQ.Text;
            char[] qqSplit = { ',', ';' };
            string[] qqs = qqString.Split(qqSplit, StringSplitOptions.RemoveEmptyEntries);
            qqList.Clear();
            foreach (string item in qqs)
            {
                qqList.Add(new QQItem(item));
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            QQItem qq = new QQItem("59007337");
            StartChat(qq);
        }
    }

    public class QQItem
    {
        //获得腾讯QQ在线状态
        //输入参数：QQ号码 String，默认QQ号码：8698053。
        //返回数据：String，Y = 在线；N = 离线；E = QQ号码错误；A = 商业用户验证失败；V = 免费用户超过数量
        public string QQ
        {
            get;
            set;
        }

        private QQState state = QQState.Unknown;
        public string State
        {
            get
            {
                switch (this.state)
                {
                    case QQState.Unknown:
                        return "未知状态";
                    case QQState.Online:
                        return "在线";
                    case QQState.Offline:
                        return "离线";
                    case QQState.ErrorQQ:
                        return "QQ号码错误";
                    case QQState.InvalidCertificate:
                        return "商业用户验证失败";
                    case QQState.OutOfFreeNum:
                        //return "免费用户超过数量";
                        return "完了，腾讯不让免费查了，要收米啊";
                    default:
                        return "未知状态";
                }
            }
        }

        public QQItem(string qqCode)
        {
            this.QQ = qqCode;
        }

        public void CheckState(qqOnlineWebService qqOnline)
        {
            if (!string.IsNullOrEmpty(this.QQ) && (null != qqOnline))
            {
                string state = qqOnline.qqCheckOnline(this.QQ);
                if (state.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.state = QQState.Online;
                }
                else if (state.Equals("N", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.state = QQState.Offline;
                }
                else if (state.Equals("E", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.state = QQState.ErrorQQ;
                }
                else if (state.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.state = QQState.InvalidCertificate;
                }
                else if (state.Equals("V", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.state = QQState.OutOfFreeNum;
                }
            }
        }
    }

    public enum QQState
    {
        Unknown,
        /// <summary>
        /// 在线
        /// </summary>
        Online,
        /// <summary>
        /// 离线
        /// </summary>
        Offline,
        /// <summary>
        ///  QQ号码错误
        /// </summary>
        ErrorQQ,
        /// <summary>
        /// 商业用户验证失败
        /// </summary>
        InvalidCertificate,
        /// <summary>
        /// 免费用户超过数量
        /// </summary>
        OutOfFreeNum
    }

}
