using System;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;

namespace TestProject
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private string GetPath()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openDialog.FileName))
            {
                return openDialog.FileName;
            }
            else
            {
                MessageBox.Show("请选择一个帐户数据文件", "（江湖）提醒");
                return string.Empty;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string url = this.txtUrl.Text;
            if (!string.IsNullOrEmpty(url))
            {
                HtmlRequest hr = new HtmlRequest();

                string s = hr.ReadFromResponseContent(url);
                s = hr.AsyncRead(url);
            }
        }

        void p_ProcessItemChanged()
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                //txtLog.AppendText("\r\n" + e.Item.ToString());
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(delegate
            {
                ADSLItem adsl = new ADSLItem() { EntryName = "宽带连接", User = "jh6798028", Password = "976005" };
                int i = 0;
                while (i++ < 20)
                {
                    Console.WriteLine(string.Format("----》第{0}次重启网络开始", i));
                    using (AdslDialer dialer = new AdslDialer(adsl, WowLogManager.Instance))
                    {
                       bool b= dialer.Dial();
                       if (!b)
                       {
                         Console.WriteLine(string.Format("*****》第{0}次重启网络出错", i));  
                       }
                    }
                    Thread.Sleep(2000);
                    Console.WriteLine(string.Format("######》第{0}次重启网络结束", i));

                }
            }));
            t.Start();
        }
    }
}
