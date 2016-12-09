using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using PublicUtilities;

namespace WowTools
{
    public abstract class FactoryBase
    {
        protected string FilePath
        {
            get;
            set;
        }

        public long CurrentCount
        {
            get;
            set;
        }

        /// <summary>
        /// Bytes
        /// </summary>
        public long TotalSize
        {
            get;
            set;
        }

        /// <summary>
        /// Bytes
        /// </summary>
        public long CurrentSize
        {
            get;
            set;
        }

        protected bool IsRunning
        {
            get;
            set;
        }

        public event EventHandler<ProgressEventArgs> CurrentLineChanged;
        public event EventHandler ProcessFinished;
        private Thread processThread;
        protected const int REPORTCOUNT = 100;

        /// <summary>
        /// Start a thread to read line and handle it
        /// </summary>
        public void Start()
        {
            this.IsRunning = true;
            processThread = new Thread(new ThreadStart(this.StartProcess));
            processThread.Start();
        }

        /// <summary>
        /// Stop thread
        /// </summary>
        public virtual void Stop()
        {
            if (!this.IsRunning)
            {
                return;
            }

            this.IsRunning = false;
            if ((null != this.processThread) && this.processThread.IsAlive)
            {
                processThread.Join(1000);
                processThread = null;
            }
        }

        /// <summary>
        /// Read line from raw  data file and handle it 
        /// </summary>
        protected virtual void StartProcess()
        {
            if (!string.IsNullOrEmpty(this.FilePath) && File.Exists(this.FilePath))
            {
                this.InitialRawFileParams();
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line = string.Empty;
                        while (this.IsRunning && (null != (line = sr.ReadLine())))
                        {
                            this.CurrentCount++;
                            this.CurrentSize += line.Length;
                            this.ProcessLine(line);
                            if (this.CurrentCount % REPORTCOUNT == 0)
                            {
                                this.RaiseCurrentLineChanged();
                                Thread.Sleep(1);
                            }
                        }
                    }
                }

                this.RaiseCurrentLineChanged();
                this.RaiseProcessFinished();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0}　文件不存在！", this.FilePath));
            }
        }

        /// <summary>
        /// Parse the current lint by rule
        /// </summary>
        /// <param name="line"></param>
        protected abstract void ProcessLine(string line);

        protected void InitialRawFileParams()
        {
            FileInfo fi = new FileInfo(this.FilePath);
            this.TotalSize = fi.Length;

            this.CurrentSize = 0;
            this.CurrentCount = 0;
        }

        protected void RaiseCurrentLineChanged()
        {
            if (null != this.CurrentLineChanged)
            {
                this.CurrentLineChanged(this, new ProgressEventArgs(this.CurrentCount, this.CurrentSize, this.TotalSize));
            }
        }

        protected void RaiseProcessFinished()
        {
            if (null != this.ProcessFinished)
            {
                this.ProcessFinished(this, new EventArgs());
            }
        }
    }
}
