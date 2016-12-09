using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RiftTool
{
    public class USRiftHttperLoginBase : GameHttperBase
    {
        //http://www.riftgame.com/en/
        //username=350394089@qq.com&password=350394089&lt=e1s1&_eventId=submit
        string ltValue = "e1s1";
        string PostFormat = "username={0}&password={1}&lt={2}&_eventId=submit";
        public RiftAccountItem Account
        {
            get;
            set;
        }
        public USRiftHttperLoginBase(DetectionParamsItem HttperParamsItem)
            : base(HttperParamsItem,RiftLogManager.Instance)
        {
        }

        public void GetState(RiftAccountItem account)
        {
            if (account == null) return;
            RiftLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));
            this.Account = account;
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            string html = string.Empty;

            if (!string.IsNullOrEmpty(gameServer.LoginUrl))
            {
                html = this.ReadFromUrl(gameServer.LoginUrl);
                string ltStr = HtmlParser.GetOuterTextFromHtml("<input type=\"hidden\" name=\"lt\"", "/>", 1, html);
                ltStr = HtmlParser.GetOutterPropertyFromHtml(ltStr, "value");
                if (!string.IsNullOrEmpty(ltStr))
                {
                    ltValue = ltStr;
                }
            }

            if (string.IsNullOrEmpty(gameServer.LoginPostActionUrl)) return;
            string data = string.Format(PostFormat, this.Account.EMail, this.Account.Password, ltValue);
            html = this.ReadUrlContent(gameServer.LoginPostActionUrl, data);
            this.GetState(account, html);

            if (account.IsLoginSucceed && this.DetectionParamsItem.IsGetDetail)
            {
                GetAccountSummary(account, html);
            }
        }

        private void GetAccountSummary(RiftAccountItem account, string html)
        {
            string summary = string.Empty;
            //<span class="bold">Country:</span> United States
            string s = HtmlParser.GetOuterTextFromHtml("Country:</span>", "</div>", 1, html);
            if (!string.IsNullOrEmpty(s))
            {
                account.AccountSummary = HtmlParser.GetInnerTextFromHtml(s).Replace("Country:", "");
            }
        }
        private void GetState(RiftAccountItem account, string html)
        {
            if ((null == account)) return;
            if (this.IsContains(html, "Hello,", "Account summary"))
            {
                this.Account.State = LoginState.LoginSucceed;
                this.GetLoginState(account);
            }
            else if (this.IsContains(html, "您提供的凭证有误", "The email address or password supplied is not correct."))
            {
                this.Account.State = LoginState.LoginFalied;
            }
            else if (this.IsContains(html, "The requested URL could not be retrieved", "The remote host or network may be down"))
            {
                this.Account.State = LoginState.NetworkFailure;
            }
            else if (this.IsContains(html, "The account you are attempting to use has been temporarily blocked", "temporarily blocked"))
            {
                this.Account.State = LoginState.TempBlock;
            }
            else if (string.IsNullOrEmpty(html) || this.IsContains(html, HttpHelperBase.HTTPERROR))
            {
                this.Account.State = LoginState.LoginTooMuch;
            }
            else if (this.IsContains(html, "Confirm your email address ", "To complete registration"))
            {
                this.Account.State = LoginState.RegistrationInvalid;
            }
            else if (this.IsContains(html, "Additional information required", "We need to collect some additional information from you to fully activate your account"))
            {
                this.Account.State = LoginState.AdditionalQuestion;
            }
            else
            {
                this.Account.State = LoginState.Unknown;
            }
        }

        private void GetLoginState(RiftAccountItem account)
        {
            if ((null == account)) return;

            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            if (!string.IsNullOrEmpty((gameServer.AccountDetailUrl)))
            {
                string content = this.ReadFromUrl(gameServer.AccountDetailUrl);
                if (string.IsNullOrEmpty(content) || content == HttpHelperBase.HTTPERROR)
                {
                    account.State = LoginState.NetworkFailure;
                }
                if (this.IsContains(content, @"RIFT Digital Collector's Edition", "RIFT Digital Standard Edition"))
                {
                    if (!this.IsContains(content, "You do not have an active subscription for this game"))
                    {
                        account.State = LoginState.EnterGame;
                    }
                }
            }

        }
    }
}
