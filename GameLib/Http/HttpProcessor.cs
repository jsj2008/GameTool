using System;
using System.Diagnostics;
using System.Threading;

namespace PublicUtilities
{
    public abstract class HttpProcessorBase<T, P>
        where T : ItemBase, new()
        where P : GameHttperBase
    {
        static int threadCount = 0;
        protected HttperManagerBase<T, P> httperManager = null;
        protected Thread thread = null;
        protected GameHttperBase cuttentHttpHelper = null;
        protected Stopwatch stopwatch = new Stopwatch();
        protected bool isRunning = true;

        public bool IsStopped
        {
            get;
            private set;
        }

        public LogManagerBase LogManager
        {
            get;
            private set;
        }

        public event EventHandler<BoolEventArgs> ProcessCompleted;
        public event EventHandler<ProcessEventArgs<T>> ProcessedItemChanged;
        public event EventHandler<ProcessEventArgs<T>> ReconnectChanged;

        public HttpProcessorBase(HttperManagerBase<T, P> hm, LogManagerBase logManager)
        {
            this.httperManager = hm;
            this.isRunning = true;
            this.LogManager = logManager;
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(this.StartDetection));
            thread.Name = string.Format("HttpProcessor thread {0}", ++threadCount);
            thread.Start();
        }

        protected abstract void StartDetection();

        public void Stop()
        {
            try
            {
                this.isRunning = false;
                if ((null != this.thread) && this.thread.IsAlive)
                {
                    if (null != this.cuttentHttpHelper)
                    {
                        using (this.cuttentHttpHelper)
                        {
                            this.cuttentHttpHelper.ForceToSave();
                        }
                    }
                    //this.thread.Join();
                    this.thread.Abort();
                    this.thread = null;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.Error(String.Format("Abort thread error:{0}", ex.Message));
            }
        }

        protected void RaiseProcessCompleted()
        {
            this.IsStopped = true;
            if (null != this.ProcessCompleted)
            {
                this.ProcessCompleted(this, new BoolEventArgs() { IsTrue = true });
            }
        }

        protected virtual void RaiseProcessedItemChanged(T item)
        {
            //if ((null != item) && (this.ProcessedItemChanged != null))
            if ((this.ProcessedItemChanged != null))
            {
                this.ProcessedItemChanged(this, new ProcessEventArgs<T> { Item = item });
            }
        }

        protected void RaiseReconnectChanged(T item)
        {
            if ((null != item) && (this.ReconnectChanged != null))
            {
                this.ReconnectChanged(this, new ProcessEventArgs<T> { Item = item });
            }
        }
    }

}