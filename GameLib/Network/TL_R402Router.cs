using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;

namespace PublicUtilities
{
    public abstract class RouterBase : HttpHelperBase
    {
        public RouterBase(LogManagerBase logManager)
            : base(logManager)
        {

        }
        protected bool IsServerExisted(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                LogManager.Error("Router ip can't be empty");
                return false;
            }

            bool isPinged = CmdHelper.Ping(ip);
            if (isPinged)
            {
                LogManager.Info(string.Format("Ping reply from router:{0}", ip));
            }
            else
            {
                LogManager.Info(string.Format("Can't ping reply from router:{0}", ip));
            }
            return isPinged;
        }

        protected string GetBase64String(string user, string password)
        {
            string raw = string.Format("{0}:{1}", user, password);
            byte[] rawBytes = ASCIIEncoding.ASCII.GetBytes(raw);
            string result = Convert.ToBase64String(rawBytes);
            return result;
        }

        public abstract bool Reconnect(RouterItem routerItem);
    }

    /// <summary>
    /// TL-R402系列 SOHO宽带路由器
    /// </summary>
    public class TL_R402Router : RouterBase
    {
        private RouterItem RouterItem
        { get; set; }

        public TL_R402Router(LogManagerBase logManager)
            : base(logManager)
        {
        }

        protected override void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
        {
            base.SetHttpRequestGetHeader(httpRequest);
            if (null != this.RouterItem)
            {
                string authString = string.Format("Basic {0}", this.GetBase64String(this.RouterItem.User, this.RouterItem.Password));
                httpRequest.Headers.Add(HttpRequestHeader.Authorization, authString);

                //Default user and password ( admin / admin)
                //httpRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic YWRtaW46YWRtaW4=");
            }
        }

        /// <summary>
        /// Is restart rounter ok
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public override bool Reconnect(RouterItem routerItem)
        {
            if (null == routerItem)
            {
                LogManager.Error("Router params can't be null!");
                return false;
            }

            this.RouterItem = routerItem;
            if (this.IsServerExisted(this.RouterItem.IP))
            {
                LogManager.InfoWithCallback("-> 开始重启 TL-R402系列 SOHO宽带路由器！");
                string content = string.Empty;
                string url = "http://{0}/userRpm/SysRebootRpm.htm?Reboot=%D6%D8%C6%F4%C2%B7%D3%C9%C6%F7 ";
                content = this.ReadFromUrl(string.Format(url, this.RouterItem.IP));
                bool isRestarted = this.IsContains(content, "LoadMain()");
                return isRestarted;
            }

            return false;
        }
    }
}
