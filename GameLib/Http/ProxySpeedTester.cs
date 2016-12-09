using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace PublicUtilities
{
    public class ProxySpeedTester : IDisposable
    {
        readonly int threadCount = WowSetConfig.Instance.ProxyThreads;
        readonly int chunkCount = WowSetConfig.Instance.ProxyTestChunck;

        private IEnumerator<ProxyItem> proxyEnumerator = null;
        private int finishCount = 0;
        private bool isRunnig = false;
        private SortedList<double, ProxyItem> sortedProxyList = new SortedList<double, ProxyItem>(new ProxyCompare());
        private SortedList<double, ProxyItem> sortedSucceedProxyList = new SortedList<double, ProxyItem>(new ProxyCompare());

        private GameServer Server { get; set; }
        private LogManagerBase LogManager { get; set; }
        private IList<ProxyItem> ProxyList { get; set; }
        private string ProxyFile { get; set; }

        public ProxySpeedTester(GameServer server, LogManagerBase logManager, IList<ProxyItem> proxyList, string proxyFile)
        {
            if (null == server)
            {
                throw new ArgumentNullException("server");
            }
            if (null == logManager)
            {
                throw new ArgumentNullException("logManager");
            }
            if (null == proxyList)
            {
                throw new ArgumentNullException("proxyList");
            }

            if (string.IsNullOrEmpty(proxyFile))
            {
                throw new ArgumentNullException("proxyFile");
            }

            this.Server = server;
            this.LogManager = logManager;
            this.ProxyList = proxyList;
            this.ProxyFile = proxyFile;
        }

        protected void SaveSpeedProxy()
        {
            string fileName = Path.GetFileName(this.ProxyFile);
            string extensionName = Path.GetExtension(this.ProxyFile);
            fileName = fileName.Replace(extensionName, "");
            string newPath = string.Format("{0}\\{1}_测速.txt", Path.GetDirectoryName(this.ProxyFile), fileName); ;

            using (StreamWriter sw = new StreamWriter(newPath, true))
            {
                foreach (ProxyItem item in sortedProxyList.Values)
                {
                    sw.WriteLine(string.Format("{0} {1} ,{2:f2} 秒", item.IP, item.Port, item.AccessTime));
                }
            }
            SoundPlayer.PlayAlter();
            LogManager.InfoWithCallback(string.Format("所有代理完成对 {0} 的测速 ", this.Server.Header));

        }

        protected void SaveAllResult()
        {
            using (StreamWriter sw = GetResultFile())
            {
                foreach (ProxyItem item in sortedProxyList.Values)
                {
                    sw.WriteLine(string.Format("{0} {1} ,{2:f2} 秒", item.IP, item.Port, item.AccessTime));
                }
            }

            LogManager.InfoWithCallback(string.Format("所有代理完成对 {0} 的测速 ", this.Server.Header));
        }

        StreamWriter resultFileStream = null;
        StreamWriter succeedFileStream = null;
        int chunkNum = 1;
        protected void SaveChunk(bool isLastChunk)
        {
            if (null == resultFileStream)
            {
                resultFileStream = GetResultFile();
            }
            if (null == succeedFileStream)
            {
                succeedFileStream = GetSuccedResultFile();
            }
            if (!isLastChunk)
            {
                resultFileStream.WriteLine(string.Format("开始第 {0} 块测试输出", chunkNum++));
            }

            foreach (ProxyItem item in sortedProxyList.Values)
            {
                resultFileStream.WriteLine(string.Format("{0} {1} ,{2:f2} 秒", item.IP, item.Port, item.AccessTime));
            }

            foreach (ProxyItem item in sortedSucceedProxyList.Values)
            {
                succeedFileStream.WriteLine(string.Format("{0} {1} ,{2:f2} 秒", item.IP, item.Port, item.AccessTime));
            }

            resultFileStream.Flush();
            sortedProxyList.Clear();

            succeedFileStream.Flush();
            sortedSucceedProxyList.Clear();
            SoundPlayer.PlayAlter();

            if (isLastChunk)
            {
                resultFileStream.Dispose();
                succeedFileStream.Dispose();
                resultFileStream = null;
                LogManager.InfoWithCallback(string.Format("所有代理完成对 {0} 的测速 ", this.Server.Header));
            }
        }

        private StreamWriter GetResultFile()
        {
            string fileName = Path.GetFileName(this.ProxyFile);
            string extensionName = Path.GetExtension(this.ProxyFile);
            fileName = fileName.Replace(extensionName, "");
            string newPath = string.Format("{0}\\{1}_测速.txt", Path.GetDirectoryName(this.ProxyFile), fileName); ;

            StreamWriter stream = new StreamWriter(newPath, true);
            return stream;
        }

        private StreamWriter GetSuccedResultFile()
        {
            string fileName = Path.GetFileName(this.ProxyFile);
            string extensionName = Path.GetExtension(this.ProxyFile);
            fileName = fileName.Replace(extensionName, "");
            string newPath = string.Format("{0}\\{1}_有效代理.txt", Path.GetDirectoryName(this.ProxyFile), fileName); ;

            StreamWriter stream = new StreamWriter(newPath, true);
            return stream;
        }


        object resultObj = new object();
        public void Start()
        {
            SoundPlayer.PlayAlter();
            proxyEnumerator = ProxyList.GetEnumerator();
            isRunnig = true;
            LogManager.InfoWithCallback(string.Format("开始对:{0} 文件进行代理测速 ", ProxyFile, this.Server.Header));
            for (int i = 0; i < threadCount; i++)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    while (true)
                    {
                        ProxyItem proxy = GetCurrentProxy();
                        if (null == proxy || this.isDisposing || !isRunnig)
                        {
                            if (Interlocked.Increment(ref finishCount) == threadCount)
                            {
                                SaveChunk(true);
                                //SaveSpeedProxy();
                            }
                            return;
                        }
                        using (ProxyHttper httper = new ProxyHttper(proxy))
                        {
                            httper.GetHttpTime(this.Server.LoginPostActionUrl);
                        }

                        lock (resultObj)
                        {
                            sortedProxyList.Add(proxy.AccessTime, proxy);
                            if (proxy.AccessTime > 0 & !proxy.IsNeedCaptcha)
                            {
                                sortedSucceedProxyList.Add(proxy.AccessTime, proxy);
                            }
                            if (sortedProxyList.Count >= chunkCount)
                            {
                                SaveChunk(false);
                            }
                        }

                        LogManager.InfoWithCallback(string.Format("{0}、 代理:{1} 访问时间:{2:f2} 秒,{3} {4} ",
                            proxy.ID, proxy.IP, proxy.AccessTime, proxy.IsNeedCaptcha ? " ,需要验证码" : "",
                            proxy.AccessTime < 0 ? " ,代理无法访问" : ""));
                    }
                });
            }
        }

        public void Stop()
        {
            isRunnig = false;
        }

        object lockObj = new object();
        private ProxyItem GetCurrentProxy()
        {
            lock (this)
            {
                if (null != proxyEnumerator)
                {
                    if (proxyEnumerator.MoveNext())
                    {
                        return proxyEnumerator.Current;
                    }
                }
                return null;
            }
        }

        class ProxyCompare : IComparer<double>
        {

            #region IComparer<int> Members

            public int Compare(double x, double y)
            {
                return -1;
                //if (x > y) return 1;
                //if (x == y) return 0;
                //if (x < y) return -1;
                //return 0;
            }

            #endregion
        }

        #region IDisposable Members

        bool isDisposing = false;
        public void Dispose()
        {
            Stop();
            isDisposing = true;
        }

        #endregion
    }


    public class ProxyHttper : HttpHelperBase
    {
        public ProxyItem Proxy { get; set; }

        public ProxyHttper(ProxyItem proxyItem)
            : base(null)
        {
            if (null == proxyItem || string.IsNullOrEmpty(proxyItem.IP) || proxyItem.Port <= 0)
            {
                throw new ArgumentNullException("proxyItem");
            }
            Proxy = proxyItem;
        }

        protected override void SetProxy(System.Net.HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Proxy = new WebProxy(Proxy.IP, Proxy.Port);
            }
        }

        /// <summary>
        /// Get ms for the http access spend
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public long GetHttpTime(string url)
        {
            int startTick = Environment.TickCount;
            string html = this.ReadFromUrl(url);
            if (IsContains(html, "loginForm", "accountName", "password", "Log In", "persistLogin", "/login/captcha.jpg", "securityAnswer"))
            {
                int usedTick = Environment.TickCount - startTick;
                if (IsContains(html, "/login/captcha.jpg?random=", "For security reasons, enter the characters you see in the image. This is not your password.",
                    "securityAnswer"))
                {
                    this.Proxy.IsNeedCaptcha = true;
                }
                this.Proxy.AccessTime = usedTick / 1000.0;
                return usedTick;
            }
            this.Proxy.AccessTime = -1;
            return -1;
        }
    }
}
