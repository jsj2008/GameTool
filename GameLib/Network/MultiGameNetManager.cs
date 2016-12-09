using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PublicUtilities
{
    public static class MultiGameNetManager
    {
        private static IList<IHttpManagerReconnect> httpManagerList = new List<IHttpManagerReconnect>();
        public static void Register(IHttpManagerReconnect httpManager)
        {
            lock (lockObj)
            {
                if (!httpManagerList.Contains(httpManager))
                {
                    httpManagerList.Add(httpManager);
                    GameLogManager.Instance.Info(string.Format("Register:{0}", httpManager.GetType()));
                }
            }
        }

        private static Dictionary<IHttpManagerReconnect, AutoResetEvent> reconnectDic = new Dictionary<IHttpManagerReconnect, AutoResetEvent>();

        /// <summary>
        /// Is all http manager trigger network reconnect
        /// </summary>
        /// <param name="httpManager"></param>
        /// <returns></returns>
        public static bool CheckAll(IHttpManagerReconnect httpManager, AutoResetEvent autoEvent)
        {
            lock (lockObj)
            {
                if (!reconnectDic.ContainsKey(httpManager))
                {
                    reconnectDic.Add(httpManager, autoEvent);
                }

                return (reconnectDic.Count == httpManagerList.Count);
            }
        }

        public static void ClearAll()
        {
            foreach (AutoResetEvent autoEvent in reconnectDic.Values)
            {
                autoEvent.Set();
            }
            reconnectDic.Clear();
        }

        public static bool IsConnected { get; private set; }
        private static DetectionParamsItem lastNetItem = null;
        public static bool Reconnect(DetectionParamsItem detectionItem)
        {
            lock (lockObj)
            {
                try
                {
                    lastNetItem = detectionItem;
                    IsConnected = false;
                    IsConnected = ReconnectManager.Instance.Reconnect(detectionItem);
                }
                finally
                {
                    ClearAll();
                }

                return IsConnected;
            }
        }

        private static object lockObj = new object();
        public static void UnRegister(IHttpManagerReconnect httpManager)
        {
            lock (lockObj)
            {
                if (httpManagerList.Contains(httpManager))
                {
                    httpManagerList.Remove(httpManager);
                    GameLogManager.Instance.Info(string.Format("UnRegister:{0}", httpManager.GetType()));
                }

                if ((null != lastNetItem) && (reconnectDic.Count > 0))
                {
                    Reconnect(lastNetItem);
                }
            }
        }
    }
}
