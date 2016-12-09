using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicUtilities;
using System.Net;
using System.IO;

namespace GuildWar
{
    public class FightHttperLogin : GameHttperBase
    {
        const string PostFormat = "email={0}&password={1}";
        HtmlParseHelper htmlParseHelper = new HtmlParseHelper();

        public FightAccountItem Account { get; private set; }

        public FightHttperLogin(DetectionParamsItem HttperParamsItem)
            : base(HttperParamsItem, FightLogManager.Instance)
        {
        }

        #region Http headers

        private string refererUrl = string.Empty;

        bool isRedirected = false;
        protected override void SetHttpRequestPostHeader(System.Net.HttpWebRequest httpRequest)
        {
            httpRequest.Method = HttpHelperBase.MethodPost;
            httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
            httpRequest.KeepAlive = true;
            if (!isRedirected)
            {
                httpRequest.AllowAutoRedirect = false;
                //isRedirected = true;
            }
            else
            {
                httpRequest.AllowAutoRedirect = true;
            }

            httpRequest.CookieContainer = this.cookieContainer;
            httpRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpRequest.UserAgent = @"Mozilla/5.0 (Windows NT 5.1) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.47 Safari/536.11";
            httpRequest.ContentType = @"application/x-www-form-urlencoded";
            httpRequest.ProtocolVersion = new Version("1.1");

            //httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
            httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            httpRequest.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");//"no-cache"            
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

        private string GetCookieValue(HttpWebResponse httpResponse)
        {
            string repCookie = httpResponse.Headers.Get("Set-Cookie");
            if (!string.IsNullOrEmpty(repCookie) && repCookie.Contains("s="))
            {
                string[] cookiesArray = repCookie.Split(';');
                this.cookie = cookiesArray[0];
            }
            if (!string.IsNullOrEmpty(this.cookie))
            {
                string cookieValue = this.cookie.Replace("s=", "");
                return cookieValue;
            }
            return string.Empty;
        }

        protected override void SaveCookie(HttpWebResponse httpResponse)
        {
            string cookieValue = GetCookieValue(httpResponse);
            if (!string.IsNullOrEmpty(cookieValue))
            {
                //s=864A4971-104C-400C-80B0-AF218552165E; Expires=Sun, 09 Mar 2014 10:32:36 GMT; Domain=guildwars2.com; Path=/; Version=1
                Cookie c = new Cookie("s", cookieValue, "/", "guildwars2.com");
                this.cookieContainer.Add(c);
            }
            /* save cookies */
            foreach (Cookie item in httpResponse.Cookies)
            {
                this.cookieContainer.Add(item);
            }
        }

        protected override void SetCookieToHttpRequestHeader(HttpWebRequest httpRequest)
        {
            if (!string.IsNullOrEmpty(this.cookie))
            {
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, this.cookie);
            }
        }

        #endregion

        public void GetState(FightAccountItem account)
        {
            if (account == null) return;
            FightLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));

            this.Account = account;
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            string postData = string.Format(PostFormat, account.EMail.Replace("@", "%40"), account.Password);
            string html = string.Empty;
            this.refererUrl = gameServer.LoginPostActionUrl;
            html = this.PostAndResponseUriContent(gameServer.LoginPostActionUrl, postData);
            if (html != loginTooMuch)
            {
                html = this.ReadFromUrl(gameServer.AccountDetailUrl);
            }

            this.GetState(account, html);
        }

        protected string PostAndResponseUriContent(string url, string postData)
        {
            HttpWebRequest httpRequest = this.WriteToHttpWebRequest(url, postData);
            try
            {
                if (null != httpRequest)
                {
                    return this.ReadFromResponseUri(httpRequest, postData);
                }
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.GetHttpWebRequest() error:{0}", ex.Message));
            }
            finally
            {
                this.DisposeHttpRequest(httpRequest);
            }
            return string.Empty;
        }

        private string responseUrl = string.Empty;
        private int repeatCount = 0;
        protected string ReadFromResponseUri(HttpWebRequest httpRequest, string postData)
        {
            string content = HttpHelperBase.HTTPERROR;
            try
            {
                if (null != httpRequest)
                {
                    //Thread.Sleep(500);
                    HttpWebResponse httpResponse = null;
                    try
                    {
                        httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                    }
                    catch (WebException we)
                    {
                        if (null != LogManager)
                            LogManager.Error(string.Format("ReadFromHttpWebResponse error:{0}", we.Message));
                        httpResponse = we.Response as HttpWebResponse;
                    }

                    // TODO:use the same session id
                    if (null != httpResponse)
                    {
                        //if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                        //{
                        //    string cookieValue = GetCookieValue(httpResponse);
                        //    if (string.IsNullOrEmpty(cookieValue))
                        //    {
                        //        return loginTooMuch;
                        //    }
                        //}
                        while (httpResponse.StatusCode == HttpStatusCode.Found ||
                            httpResponse.StatusCode == HttpStatusCode.SeeOther)
                        {
                            this.SaveCookie(httpResponse);
                            string locationUri = httpResponse.Headers["location"];
                            if (!string.IsNullOrEmpty(locationUri))
                            {
                                locationUri = string.Format("{0}{1}", this.DetectionParamsItem.CurrentGameServer.DomainUrl, locationUri);
                                //responseUrl = locationUri;
                                if (this.IsEquals(locationUri, "https://account.guildwars2.com/account"))
                                {
                                    break;
                                }
                                else if (this.IsEquals(locationUri, "https://account.guildwars2.com/login",
                                    "https://account.guildwars2.com/login?redirect_uri=/account"))
                                {
                                    //content = PostAndResponseUriContent(this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl, postData);
                                }
                                else if (
                                    this.IsEquals(locationUri,
                                    "https://account.guildwars2.com/login?redirect_uri=%2Faccount&error=true&reason=",
                                    "https://account.guildwars2.com/login?error=true&reason="))
                                {
                                    //if (repeatCount++ < 3)
                                    //{
                                    //    content = PostAndResponseUriContent(this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl, postData);
                                    //}
                                    //else
                                    //{
                                    //    content = loginTooMuch;
                                    //}                                   
                                }
                                //using (httpResponse)
                                //{
                                //}
                                //return content;

                                using (httpResponse)
                                {
                                }
                                HttpWebRequest request = GetHttpWebRequest(locationUri, false);
                                httpResponse = request.GetResponse() as HttpWebResponse;
                            }
                        }

                        using (httpResponse)
                        {
                            this.SaveCookie(httpResponse);
                            using (StreamReader sr = new StreamReader(GetGzipStream(httpResponse), Encoding.UTF8))
                            {
                                content = sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.ReadFromHttpWebResponse() error:{0}", ex.Message));
                content = HttpHelperBase.HTTPERROR;
            }

            return content;
        }

        const string loginTooMuch = "loginTooMuch";
        private void GetState(FightAccountItem account, string html)
        {
            if ((null == account)) return;

            if (this.IsContains(html, "Personal Info", "Change Account Name", "Change Password", "LOGOUT", "<a href=\" / \">ACCOUNT</a>"))
            {
                account.State = LoginState.LoginSucceed;
            }
            else if (this.IsContains(html, "<input type=\"text\" id=\"email\" name=\"email\" value=\"\" placeholder=\"E-mail / Account Name\" autofocus=\"\" required=\"\">",
                                           "The account name or password you entered is invalid. Please check your information and try again.",
                                           "<a class=\"login-reset yui3-u\" href=\"/recovery\">", "Forgot your password?"))
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
            else if (string.IsNullOrEmpty(html) || this.IsContains(html, HttpHelperBase.HTTPERROR, loginTooMuch))
            {

                account.State = LoginState.LoginTooMuch;
            }
            else
            {
                account.State = LoginState.Unknown;
            }
        }
    }
}
