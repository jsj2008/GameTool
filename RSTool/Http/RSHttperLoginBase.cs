using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RSTool
{
    public class RSHttperLoginBase : GameHttperBase
    {
        //username=ttt@184.com&password=111111111&mod=www&ssl=0&dest=
        //username=lzcj4@163.com&password=123456lzc&recaptcha_challenge_field=03AHJ_Vut_coBrWjlgS5LGZiB06-sFQnnEAxKr-RlXouPKM8DlkKuuydQxq1qkRh3wsOeLHrhke7plbOcG94B9ceHaZbaY1XcQW2J56HOhD3Qqsm9zDxpyzfRJkijaiV2j9QujkBv35OKcoa8Fx2e5cdnrTcHPxQNGNg
        //&recaptcha_response_field=tordo+Microbiol.&rem=1&mod=www&ssl=0&dest=
        string PostFormat = "username={0}&password={1}&mod=www&ssl=0&dest=";
        public RSAccountItem Account
        {
            get;
            set;
        }
        public RSHttperLoginBase(DetectionParamsItem HttperParamsItem)
            : base(HttperParamsItem,RSLogManager.Instance)
        {
        }

        public void GetState(RSAccountItem account)
        {
            if (account == null) return;
            RSLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));
            this.Account = account;
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            string html = string.Empty;

            if (!string.IsNullOrEmpty(gameServer.LoginUrl))
            {
                html = this.ReadFromUrl(gameServer.LoginUrl);
                if (!string.IsNullOrEmpty(html))
                {
                    //If need Captcha
                    this.GetState(account, html);
                    if (account.IsNeedRedial)
                    {
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(gameServer.LoginPostActionUrl)) return;
            string data = string.Format(PostFormat, this.Account.EMail, this.Account.Password);
            html = this.ReadUrlContent(gameServer.LoginPostActionUrl, data);
            this.GetState(account, html);

            //if (account.IsLoginSucceed)
            //{
            //    GetAccountSummary(account, html);
            //}
        }

        private void GetAccountSummary(RSAccountItem account, string html)
        {
            string summary = string.Empty;
            //<span class="bold">Country:</span> United States
            string s = HtmlParser.GetOuterTextFromHtml("Country:</span>", "</div>", 1, html);
            if (!string.IsNullOrEmpty(s))
            {
                account.AccountSummary = HtmlParser.GetInnerTextFromHtml(s).Replace("Country:", "");
            }
        }

        private void GetState(RSAccountItem account, string html)
        {
            if ((null == account)) return;
            if (this.IsContains(html, "Login Successful", "Your login was successful", "You will shortly be redirected to your destination", "logout.ws"))
            {
                account.State = LoginState.LoginSucceed;
                this.GetLoginState(account);
            }
            else if (this.IsContains(html, "Login Failed", "Invalid Login or Password", "The username, email or password you entered was incorrect"))
            {
                account.State = LoginState.LoginFalied;
            }
            else if (this.IsContains(html, "The requested URL could not be retrieved", "The remote host or network may be down", HttpHelperBase.HTTPERROR))
            {
                account.State = LoginState.NetworkFailure;
            }
            else if (this.IsContains(html, "<div class=\"section_form\" id=\"recaptcha\">", "<div id=\"recaptcha_image\">"))
            {
                account.State = LoginState.Catpcha;
            }
            else if (string.IsNullOrEmpty(html) || this.IsContains(html, HttpHelperBase.HTTPERROR))
            {
                account.State = LoginState.LoginTooMuch;
            }
            else
            {
                account.State = LoginState.Unknown;
            }
        }

        private void GetLoginState(RSAccountItem account)
        {
            if ((null == account)) return;

            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            if (!string.IsNullOrEmpty((gameServer.AccountDetailUrl)))
            {
            }
        }
    }
}
