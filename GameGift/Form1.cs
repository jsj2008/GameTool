using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;
using System.Threading;
using System.IO;

namespace GameGift
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        bool isClosing = false;
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            autoResetEvent.Set();
            autoResetEvent.Reset();
            try
            {
                httpThread.Abort();
            }
            catch (System.Exception ex)
            {

            }
        }

        UcAdsl ucAdsl = null;
        void Form1_Load(object sender, EventArgs e)
        {
            ADSLItem adsl = new ADSLItem();
            adsl.EntryName = WowSetConfig.Instance.ADSLName;
            adsl.Password = WowSetConfig.Instance.ADSLPwd;
            adsl.User = WowSetConfig.Instance.ADSLUser;
            ucAdsl = new UcAdsl(adsl, WowLogManager.Instance);
            ucAdsl.TestChanged += ucNetwork_TestChanged;
            panel1.Controls.Add(ucAdsl);

            WowLogManager.Instance.LogEvent += LogManager_LogEvent;
        }

        private void LogManager_LogEvent(string log)
        {
            if (isClosing)
            {
                return;
            }
            Dial(log);
            this.BeginInvoke(new ThreadStart(delegate()
            {
                txtLog.AppendText("\r\n" + log);
            }));
        }

        private bool isDialing = false;
        private void Dial(string log)
        {
            if (TextHelper.IsContains(log, "jQuery(-4)"))
            {
                Thread.Sleep(15 * 1000);
                this.ucAdsl.Dial();
                isDialing = true;
            }
            else if (TextHelper.IsContains(log, "网络重连成功"))
            {
                isDialing = false;
                Thread.Sleep(5 * 1000);
                autoResetEvent.Set();
            }
        }

        private void ucNetwork_TestChanged(object sender, BoolEventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                //if (e.IsTrue)
                //{
                //    this.tabWebDetection.SelectTab(pageDisplay);
                //    txtLog.Clear();
                //    this.StartReconnectTimer();
                //}
                //else
                //{
                //    this.StopReconnectTimer();
                //}
            }));
        }

        string filePath = string.Empty;
        private void btnStart_Click(object sender, EventArgs e)
        {
            filePath = this.ucOpenFile1.FilePath;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择有效数据文件 ");
                return;
            }
            StartRequest();
        }

        Thread httpThread = null;
        protected AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private void StartRequest()
        {
            httpThread = new Thread(new ThreadStart(delegate()
           {
               IList<ItemBase> userList = LoadUser();
               StreamWriter writer = GetOutput();
               using (writer)
               {
                   int total = userList.Count;
                   this.BeginInvoke(new ThreadStart(delegate()
                   {
                       ucProgress1.StartTimer();
                   }));

                   for (int i = 0; i < userList.Count; i++)
                   {
                       if (isClosing)
                       {
                           return;
                       }

                       ItemBase item = userList[i];
                       if (item == null)
                       {
                           continue;
                       }
                       string result = HttpHelper.Get(item);
                       if (string.IsNullOrEmpty(result))
                       {
                           continue;
                       }

                       if (!TextHelper.IsContains(result, "jQuery(-4)"))
                       {
                           writer.WriteLine(result);
                       }

                       int lineIndex = i + 1;
                       WowLogManager.Instance.InfoWithCallback(string.Format("第{0}个：{1}，结果：{2}", lineIndex, item.User, result));

                       Application.DoEvents();
                       ucProgress1.SetCount(new ProgressEventArgs(lineIndex, lineIndex, total));
                       if (i % 5 == 0)
                       {
                           writer.Flush();
                       }

                       if (TextHelper.IsContains(result, "jQuery(-4)") || isDialing)
                       {
                           i = i - 2;
                           autoResetEvent.WaitOne();
                       }
                   }
                   writer.Flush();
               }

               this.BeginInvoke(new ThreadStart(delegate()
               {
                   ucProgress1.StopTimer();
                   MessageBox.Show("处理完成");
               }));
           }));
            httpThread.Start();
        }

        private IList<ItemBase> LoadUser()
        {
            IList<ItemBase> list = new List<ItemBase>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string user = string.Empty;
                string pwd = string.Empty;
                string email = string.Empty;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (TextToItemHelper.GetLoginAccountItem(line, ref user, ref pwd, ref email,
                        DataFormat.AccountPassword, WowLogManager.Instance))
                    {
                        ItemBase item = new ItemBase()
                        {
                            RawData = line,
                            User = user,
                            Password = pwd
                        };
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        private StreamWriter GetOutput()
        {
            string name = Path.GetFileNameWithoutExtension(this.filePath) + "结果";
            string outPath = string.Format(@"{0}\{1}{2}", Path.GetDirectoryName(filePath), name, Path.GetExtension(filePath));
            StreamWriter sw = new StreamWriter(outPath, true, Encoding.Default);
            return sw;
        }
    }
}
