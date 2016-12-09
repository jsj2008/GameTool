using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PublicUtilities
{
    public class RouterManager : IReconnectManager
    {
        private IDictionary<RouterType, RouterBase> routerDic = new Dictionary<RouterType, RouterBase>();
        public LogManagerBase LogManager
        {
            get;
            set;
        }

        public RouterManager(LogManagerBase logManager)
        {
            this.LogManager = logManager;
            routerDic.Add(RouterType.TL_R402, new TL_R402Router(this.LogManager));
        }

        private RouterBase GetRouter(RouterType routerType)
        {
            RouterBase router = this.routerDic[routerType];
            Trace.WriteLine(router != null, "Selected router can't be null!");
            return router;
        }

        public bool Reconnect(DetectionParamsItem detectionItem)
        {
            if ((null != detectionItem) && (detectionItem.ReconnectType == ReconnectType.Router))
            {
                RouterItem routerItem = detectionItem.Router;
                LogManager.Info(string.Format("start restart router:{0}", routerItem.IP));
                RouterBase router = this.GetRouter(routerItem.RouterType);
                router.Reconnect(routerItem);
                ReconnectManager.Sleep();

                int i = 0;
                while (i++ < ReconnectManager.TRYCOUNT)
                {
                    if (CmdHelper.PingBaidu() || CmdHelper.Ping163())
                    {
                        return true;
                    }
                    Thread.Sleep(2 * 1000);
                }
                return false;
            }

            return false;
        }
    }
}