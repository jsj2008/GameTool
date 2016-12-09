using System.Collections.Generic;
using System.Threading;

namespace PublicUtilities
{
    public class ADSLManager : IReconnectManager
    {
        public LogManagerBase LogManager = null;
        public ADSLManager(LogManagerBase logManager)
        {
            this.LogManager = logManager;
        }

        private object lockObj = new object();
        public bool Reconnect(DetectionParamsItem detectionItem)
        {
            lock (lockObj)
            {
                if ((null == detectionItem) || (null == detectionItem.ADSL))
                {
                    LogManager.Error("ADSL parameters can't be empty");
                    return false;
                }

                for (int i = 0; i < 3; i++)
                {
                    using (AdslDialer dialer = new AdslDialer(detectionItem.ADSL, this.LogManager))
                    {
                        if (dialer.Dial())
                        {
                            break;
                        }
                    }
                    Thread.Sleep(3 * 1000);
                }

                //this.Reconnect(detectionItem.ADSL);
                //ReconnectManager.Sleep();
                Thread.Sleep(2 * 1000);
                int t = 0;
                while (t++ < ReconnectManager.TRYCOUNT)
                {
                    if (CmdHelper.PingBaidu() || CmdHelper.Ping163())
                    {
                        return true;
                    }
                    Thread.Sleep(2 * 1000);
                }
                return false;
            }
        }

        private void Reconnect(ADSLItem adslItem)
        {
            if ((null == adslItem) ||
                string.IsNullOrEmpty(adslItem.EntryName) ||
                string.IsNullOrEmpty(adslItem.User) ||
                string.IsNullOrEmpty(adslItem.Password))
            {
                LogManager.Error("PPPOE 拨号参数不能为空");
                return;
            }
            IList<string> cmdList = new List<string>();

            // string disconnectCmd = string.Format(" rasdial.exe \"{0}\" /DISCONNECT ", adslItem.EntryName);
            string disconnectCmd = string.Format(" rasdial.exe {0} /DISCONNECT ", adslItem.EntryName);
            cmdList.Add(disconnectCmd);
            LogManager.Info(disconnectCmd);
            LogManager.InfoWithCallback(string.Format("-> 断开ASDL: {0}", adslItem.EntryName));

            //string conCmd = string.Format(" rasdial.exe \"{0}\"  {1} {2} ", adslItem.EntryName, adslItem.User, adslItem.Password);
            string conCmd = string.Format(" rasdial.exe {0} {1} {2} ", adslItem.EntryName, adslItem.User, adslItem.Password);
            cmdList.Add(conCmd);
            LogManager.Info(conCmd);
            LogManager.InfoWithCallback(string.Format("-> 重连ASDL:{0}", adslItem.EntryName));

            CmdHelper.RunCmd(cmdList, this.LogManager);
        }
    }
}