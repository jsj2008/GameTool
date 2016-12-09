using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PublicUtilities
{
    public partial class UcRouter : UserControl
    {
        RouterItem routerItem = null;
        public LogManagerBase LogManager { get; set; }
        public UcRouter(RouterItem router, LogManagerBase logManager)
        {
            InitializeComponent();
            this.Load += UcRouter_Load;
            this.LogManager = logManager;
        }

        void UcRouter_Load(object sender, EventArgs e)
        {
            List<RouterComboxItem> routerList = new List<RouterComboxItem>();
            routerList.Add(new RouterComboxItem() { RouterType = RouterType.TL_R402 });
            // routerList.Add(new RouterItem() { RouterType = RouterType.D_Link });
            comboRouter.DataSource = routerList;

            if (null != routerItem)
            {
                this.comboRouter.SelectedValue = routerItem.RouterType;
                this.txtRounterIp.Text = routerItem.IP;
                this.txtRouterUser.Text = routerItem.User;
                this.txtRouterPwd.Text = routerItem.Password;
            }
        }

        public RouterItem GetRouterItem()
        {
            #region  Router

            RouterItem routerItem = new RouterItem();
            StringBuilder sb = new StringBuilder();
            int i = 1;

            RouterType routerType = (RouterType)this.comboRouter.SelectedValue;
            routerItem.RouterType = routerType;

            string routerIp = this.txtRounterIp.Text.Trim();
            if (!string.IsNullOrEmpty(routerIp) && TextHelper.IsIP(routerIp))
            {
                routerItem.IP = routerIp;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、您录入的不是有效的路由器IP地址！", i++));
            }

            string routerUser = this.txtRouterUser.Text.Trim();
            if (!string.IsNullOrEmpty(routerUser))
            {
                routerItem.User = routerUser;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的路由器登录用户名！", i++));
            }

            string routerPwd = this.txtRouterPwd.Text.Trim();
            if (!string.IsNullOrEmpty(routerPwd))
            {
                routerItem.Password = routerPwd;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的路由器登录密码！", i++));
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return null;
            }

            return routerItem;

            #endregion
        }

        private void btnTestRouter_Click(object sender, EventArgs e)
        {
            RouterManager routerMgr = new RouterManager(this.LogManager);
            DetectionParamsItem paramsItem = new DetectionParamsItem();
            RouterItem router = this.GetRouterItem();
            if (null != router)
            {
                paramsItem.ReconnectType = ReconnectType.Router;
                paramsItem.Router = router;

                this.OnTestChanged(true);
                LogManager.InfoWithCallback(string.Format("-> 正在开始对：{0} 进行网络重连测试，请稍等......", CommentAttributeGetter.GetAttribute<RouterType>(router.RouterType)));
                Thread t = new Thread(new ThreadStart(delegate()
                    {
                        bool isConnected = routerMgr.Reconnect(paramsItem);

                        if (isConnected)
                        {
                            LogManager.InfoWithCallback(string.Format("-> 对：{0} 网络重连测试成功，网络恢复正常", CommentAttributeGetter.GetAttribute<RouterType>(router.RouterType)));
                        }
                        else
                        {
                            LogManager.InfoWithCallback(string.Format("-> 对：{0} 网络重连测试失败，请确保路由器可以正常访问！", CommentAttributeGetter.GetAttribute<RouterType>(router.RouterType)));
                        }
                        this.OnTestChanged(false);
                    }));
                t.Start();
            }
        }

        public event EventHandler<BoolEventArgs> TestChanged;
        private void OnTestChanged(bool isStarted)
        {
            if (null != this.TestChanged)
            {
                this.TestChanged(this, new BoolEventArgs() { IsTrue = isStarted });
            }
        }
    }
}
