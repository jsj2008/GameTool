using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicUtilities;
using System.Net;

namespace XBOX
{
    public class XBoxHttperLogin : GameHttperBase
    {
        const string httpPostValue = "&KMSI=1&SI=%E7%99%BB%E5%BD%95&mest=0&type=11&PPFT={2}&PPSX=Passp&idsbho=1&sso=0&NewUser=1&LoginOptions=1&i1=0&i2=1&i3=20262&i4=0&i7=0&i12=1&i13=1&i14=1844&i15=2408&i16=3252&i17=0&i18=__Login_Strings%7C1%2C__Login_Core%7C1%2C";
        string PostFormat = "login={0}&passwd={1}" + httpPostValue;
        HtmlParseHelper htmlParseHelper = new HtmlParseHelper();

        public AccountItem Account { get; private set; }

        public XBoxHttperLogin(DetectionParamsItem HttperParamsItem)
            : base(HttperParamsItem, XBOXLogManager.Instance)
        {
        }

        #region Http headers

        private string refererUrl = string.Empty;

        protected override void SetHttpRequestPostHeader(System.Net.HttpWebRequest httpRequest)
        {
            httpRequest.Method = HttpHelperBase.MethodPost;
            httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
            httpRequest.KeepAlive = true;
            httpRequest.AllowAutoRedirect = true;
            httpRequest.CookieContainer = this.cookieContainer;
            httpRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpRequest.UserAgent = @"Mozilla/5.0 (Windows NT 5.1) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.47 Safari/536.11";
            httpRequest.ContentType = @"application/x-www-form-urlencoded";
            httpRequest.ProtocolVersion = new Version("1.1");

            //httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
            httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            httpRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
            httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
            httpRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8;q=0.7,*;q=0.3");
            this.SetCookieToHttpRequestHeader(httpRequest);
            this.SetProxy(httpRequest);
            httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            if ((null != httpRequest) && (!string.IsNullOrEmpty(this.refererUrl)))
            {
                httpRequest.Referer = this.refererUrl;
            }
        }

        protected override void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HttpHelperBase.MethodGet;
                httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
                httpRequest.CookieContainer = this.cookieContainer;
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.47 Safari/536.11";
                httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8;q=0.7,*;q=0.3");
                httpRequest.ProtocolVersion = new Version("1.1");
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.MaximumAutomaticRedirections = 50;
                this.SetCookieToHttpRequestHeader(httpRequest);
                this.SetProxy(httpRequest);
                httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }

            if ((null != httpRequest) && (!string.IsNullOrEmpty(this.refererUrl)))
            {
                httpRequest.Referer = this.refererUrl;
            }
        }

        #endregion

        public void GetState(AccountItem account)
        {
            if (account == null) return;
            XBOXLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));
            this.Account = account;
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;

            string msnLoginUrl = "https://login.live.com/pp1300/";
            string html = ReadFromUrl(msnLoginUrl);

            string actionUrl = GetLoginPostActionUri(html);
            if (!string.IsNullOrEmpty(actionUrl))
            {
                gameServer.LoginPostActionUrl = actionUrl;
                this.refererUrl = msnLoginUrl;
            }

            if (!string.IsNullOrEmpty(gameServer.LoginPostActionUrl))
            {
                string ppftValue = GetPPFT(html);
                if (!string.IsNullOrEmpty(ppftValue))
                {
                    string data = string.Format(PostFormat, Uri.EscapeDataString(this.Account.EMail), Uri.EscapeDataString(this.Account.Password).Replace("!", "%21"), ppftValue.Replace("!", "%21"));
                    html = this.ReadUrlContent(gameServer.LoginPostActionUrl, data);
                }
            }

            this.GetState(account, html);

            //if (account.IsLoginSucceed)
            //{
            //    GetAccountSummary(account, html);
            //}
        }


        private string GetLoginPostActionUri(string html)
        {
            const string postLabel = "srf_uPost";
            if (html.Contains(postLabel))
            {
                string actionUri = htmlParseHelper.GetOutterPropertyFromHtml(html, postLabel, "'");
                return actionUri;
            }
            return string.Empty;
        }

        private string GetPPFT(string html)
        {
            if (html.Contains("name=\"PPFT\""))
            {
                string subStr = htmlParseHelper.GetSubString(html, "<input type=\"hidden\" name=\"PPFT\"", "/>", string.Empty);
                string propValue = htmlParseHelper.GetOutterPropertyFromHtml(subStr, "value");
                return propValue;
            }
            return string.Empty;
        }


        private void GetState(AccountItem account, string html)
        {
            if ((null == account)) return;
            if (this.IsContains(html, "idProfileInformation", "idUniqueIdLb", "idWinLiveIdValue", "idFullNameValue"))
            {
                account.State = LoginState.LoginSucceed;
               
            }
            else if (this.IsContains(html, "Login Failed", "Invalid Login or Password"))
            {
                account.State = LoginState.LoginFalied;
            }
            else if (string.IsNullOrEmpty(html) || this.IsContains(html, HttpHelperBase.HTTPERROR))
            {
                account.State = LoginState.NetworkFailure;
            }
            else
            {
                account.State = LoginState.Unknown;
            }
        }
    }
}
