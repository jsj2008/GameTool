using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;
using System.Threading;

namespace WebDetection
{
    public class WowHttpProcessor : HttpProcessorBase<UserAccountItem, WowHttperLoginBase>
    {
        public WowHttpProcessor(HttperManagerBase<UserAccountItem, WowHttperLoginBase> hm)
            : base(hm, WowLogManager.Instance)
        {
        }

        protected override void StartDetection()
        {
            while (this.isRunning)
            {
                UserAccountItem userItem = httperManager.GetNextItem();
                if (null == userItem)
                {
                    break;
                }
                GameServer currentServer = httperManager.HttperParamsItem.CurrentGameServer;
                GetStateFromServer(currentServer, userItem, false);
                if (!userItem.IsAutoSwitchServer)
                {
                    userItem.SignInServer = currentServer.Header;
                }
                AutoSwitchServer(userItem);
                this.RaiseProcessedItemChanged(userItem);
                this.RaiseReconnectChanged(userItem);
            }

            this.RaiseProcessCompleted();
        }

        GameServer oldServer = null;
        private void AutoSwitchServer(UserAccountItem userItem)
        {
            if (httperManager.HttperParamsItem.IsAutoSwitch && userItem.IsAutoSwitchServer)
            {
                GameServer currentServer = httperManager.HttperParamsItem.CurrentGameServer;
                IList<GameServer> serverList = httperManager.HttperParamsItem.GameServerList;

                oldServer = currentServer;
                foreach (GameServer server in serverList)
                {
                    if (server == currentServer)
                    {
                        continue;
                    }

                    GetStateFromServer(server, userItem, true);
                    if (!userItem.IsAutoSwitchServer)
                    {
                        userItem.SignInServer = server.Header;
                        return;
                    }
                    oldServer = server;
                }
            }
        }

        private void GetStateFromServer(GameServer server, UserAccountItem userItem, bool isSwitch)
        {
            if (null == server || null == userItem)
            {
                throw new ArgumentNullException();
            }

            WowHttperLoginBase httpHelper = httperManager.GetHttper(server.GameServerType);
            if (null == httpHelper)
            {
                throw new InvalidOperationException();
            }

            this.cuttentHttpHelper = httpHelper;
            this.stopwatch.Start();
            WowLogManager.Instance.Info(string.Format("-----------  start detecting new account,email={0},pwd={1}", userItem.EMail, userItem.Password));
            if (isSwitch)
            {
                GameServer currentServer = httperManager.HttperParamsItem.CurrentGameServer;
                WowLogManager.Instance.InfoWithCallback(string.Format("    **>第{0} 用户：{1} 在 {2} 登录失败，切换到：{3} 上查询",
                                                                     userItem.Index, userItem.EMail, oldServer.Header, server.Header));
            }
            else
            {
                WowLogManager.Instance.InfoWithCallback(string.Format("-->第{0} 用户：{1} 在：{2} 上查询", userItem.Index, userItem.EMail, server.Header));
            }

            try
            {
                using (httpHelper)
                {
                    httpHelper.GetAccountDetail(userItem);
                    if (userItem.IsErrored)//&& !userItem.IsCaptchaError)
                    {
                        WowLogManager.Instance.Info(string.Format("----------- account error so start taking more try,email={0},pwd={1}, state={2}",
                                                userItem.EMail, userItem.Password, userItem.State));
                        httpHelper.TakeMoreTryForErrored(userItem);
                    }

                    //this.AutoSwitchEUServer(userItem);
                    //this.RaiseProcessedItemChanged(userItem);
                    //this.RaiseReconnectChanged(userItem);
                }
                this.stopwatch.Stop();
                WowLogManager.Instance.Info(string.Format("***********  end detecting account,email={0},pwd={1}, state={2}, total spend:{3} s",
                                              userItem.EMail, userItem.Password, userItem.State, this.stopwatch.Elapsed.ToString()));
                //Thread.Sleep(500);
            }
            catch (System.Exception ex)
            {
                WowLogManager.Instance.Error(string.Format("DectectionProcess.GetAccountState() failed:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 自动英美服切换
        /// </summary>
        /// <param name="userItem"></param>
        private void AutoSwitchEUServer(UserAccountItem userItem)
        {
            if (httperManager.HttperParamsItem.IsAutoSwitch && (null != userItem) && userItem.IsAutoSwitchServer)
            {
                WowHttperLoginBase httper = null;
                GameServer lastGameServer = this.httperManager.HttperParamsItem.CurrentGameServer;
                if (this.httperManager.HttperParamsItem.CurrentGameServer.GameServerType == GameServerType.USBattle)
                {
                    GameServer gs = this.httperManager.HttperParamsItem.CurrentGameServer;
                    WowLogManager.Instance.InfoWithCallback(string.Format("-->第{0} 用户：{1} 在：{2} 登录失败，自动切换到：{3} 上查询",
                                                      userItem.Index, userItem.EMail, gs.Header, GameWowServers.ENServer.Header));
                    this.httperManager.HttperParamsItem.CurrentGameServer = GameWowServers.ENServer;
                    httper = this.httperManager.GetHttper(GameServerType.EUBattle);
                }
                else if (this.httperManager.HttperParamsItem.CurrentGameServer.GameServerType == GameServerType.EUBattle)
                {
                    GameServer gs = this.httperManager.HttperParamsItem.CurrentGameServer;
                    WowLogManager.Instance.InfoWithCallback(string.Format("-->第{0} 用户：{1} 在：{2} 登录失败，自动切换到：{3} 上查询",
                                                      userItem.Index, userItem.EMail, gs.Header, GameWowServers.USServer.Header));
                    this.httperManager.HttperParamsItem.CurrentGameServer = GameWowServers.USServer;
                    httper = this.httperManager.GetHttper(GameServerType.USBattle);
                }

                if (null == httper) return;
                using (httper)
                {
                    httper.GetAccountDetail(userItem);
                    if (userItem.IsErrored)
                    {
                        WowLogManager.Instance.Info(string.Format("----------- account error so start taking more try,email={0},pwd={1}, state={2}",
                                                userItem.EMail, userItem.Password, userItem.State));
                        httper.TakeMoreTryForErrored(userItem);
                    }
                }

                //Server switch should not cause reconnect
                if (userItem.IsCaptchaError || userItem.IsNeedRedial)
                {
                    userItem.State = WowLoginStates.AutoSwitchFailed;
                    WowLogManager.Instance.Error(string.Format("--> Account:{0} login server :{0} caused reconnect ,it has be suppressed!",
                                             userItem.EMail, this.httperManager.HttperParamsItem.CurrentGameServer.Header));
                }

                this.httperManager.HttperParamsItem.CurrentGameServer = lastGameServer;
            }
        }
    }
}
