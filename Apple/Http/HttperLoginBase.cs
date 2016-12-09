using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;
using System.Net;
using System.Web;

namespace Apple
{
    #region

    //public class HttperLoginBase : WowHttperBase
    //{
    //    enum AppleCookieStep
    //    {
    //        LoginPage,
    //        GetImagePage,
    //        PostAccountPage
    //    }

    //    // login-appleId=lzcj4%40163.com&login-password=122603Lzc!%40%23&
    //    //_a=login.sign&c=2d82076a49f9dceebba0fbf589cc6242&_fid=si&r=SCKFUHKXACXX7DYHYT9JT7JJTAPAXHFKH&s=2c3aa16188f004ce27c3f3fb77e960bc&t=SXYD4UDAPXU7P7KXF
    //    string PostFormat = "login-appleId={0}&login-password={1}&_a=login.sign&";
    //    public AccountItem Account
    //    {
    //        get;
    //        set;
    //    }
    //    AppleCookieStep currentStep = AppleCookieStep.LoginPage;

    //    public HttperLoginBase(DetectionParamsItem HttperParamsItem)
    //        : base(HttperParamsItem, AppleLogManager.Instance)
    //    {
    //    }

    //    private string postActionUrl = string.Empty;
    //    public void GetState(AccountItem account)
    //    {
    //        if (account == null) return;
    //        AppleLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));

    //        this.Account = account;
    //        GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
    //        string html = string.Empty;
    //        string postFormat = PostFormat;
    //        //postActionUrl = gameServer.LoginPostActionUrl;

    //        if (!string.IsNullOrEmpty(gameServer.LoginUrl))
    //        {
    //            this.postActionUrl = this.GetResponseUri(gameServer.LoginUrl, ref html);
    //            //html = this.ReadFromUrl(this.postActionUrl, false);
    //            currentStep = AppleCookieStep.GetImagePage;
    //            GetCookieFromImage(html);
    //            if (!string.IsNullOrEmpty(postActionUrl))
    //            {
    //                if (postActionUrl.IndexOf("?") > 0)
    //                {
    //                    string postValue = postActionUrl.Substring(postActionUrl.IndexOf("?") + 1);
    //                    if (!string.IsNullOrEmpty(postValue))
    //                    {
    //                        int index = postValue.IndexOf("&r");
    //                        if (index >= 0)
    //                        {
    //                            postValue = postValue.Insert(index, "&_fid=si");
    //                        }
    //                        if (!string.IsNullOrEmpty(postValue))
    //                        {
    //                            postFormat += postValue;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        //html = this.ReadFromUrl(postActionUrl);
    //        currentStep = AppleCookieStep.PostAccountPage;
    //        string data = string.Format(postFormat, Uri.EscapeDataString(this.Account.EMail), Uri.EscapeDataString(this.Account.Password));
    //        //string jsPostActionUrl = GetPostActionUri(html);
    //        html = this.ReadUrlContent(gameServer.LoginPostActionUrl, data);
    //        this.GetState(account, html);
    //    }

    //    private bool GetCookieFromImage(string html)
    //    {
    //        if (string.IsNullOrEmpty(html))
    //        {
    //            return false;
    //        }

    //        string image = HtmlParser.GetOuterTextFromHtml("<img width=\"1\" height=\"1\"", "/>", 1, html);
    //        image = HtmlParser.GetOutterPropertyFromHtml(image, "src");
    //        if (!string.IsNullOrEmpty(image))
    //        {
    //            this.ReadFromUrl(image, false);
    //            return true;
    //        }
    //        return false;
    //    }
    //    private string GetPostActionUri(string html)
    //    {
    //        GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
    //        if (string.IsNullOrEmpty(html))
    //        {
    //            return gameServer.LoginPostActionUrl;
    //        }
    //        string actionUrl = HtmlParser.GetSubString(html, "\"signIn\":{\"form\":\"login._forms.main\",\"url\":\"", "\"",
    //                                                   gameServer.LoginPostActionUrl);

    //        if (actionUrl != gameServer.LoginPostActionUrl)
    //        {
    //            actionUrl = gameServer.LoginPostActionUrl + actionUrl;
    //        }
    //        return actionUrl;
    //    }

    //    protected override void SetHttpRequestPostHeader(System.Net.HttpWebRequest httpRequest)
    //    {
    //        base.SetHttpRequestPostHeader(httpRequest);
    //        httpRequest.Accept = "*/*";
    //        httpRequest.UserAgent = @"User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET4.0C; .NET4.0E)";

    //        httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
    //        httpRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
    //        if ((null != httpRequest) && (!string.IsNullOrEmpty(this.postActionUrl)))
    //        {
    //            httpRequest.Referer = this.postActionUrl;
    //        }
    //    }

    //    protected override void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
    //    {
    //        base.SetHttpRequestGetHeader(httpRequest);
    //        httpRequest.UserAgent = @"User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET4.0C; .NET4.0E)";
    //        httpRequest.Accept = @"application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/xaml+xml, application/x-ms-xbap, application/x-ms-application, */*";
    //        if ((null != httpRequest) && (!string.IsNullOrEmpty(this.postActionUrl)))
    //        {
    //            httpRequest.Referer = this.postActionUrl;
    //        }
    //    }

    //    protected override void SetCookieToHttpRequestHeader(System.Net.HttpWebRequest httpRequest)
    //    {
    //        for (int i = 0; i < cookieList.Count; i++)
    //        {
    //            if (i == 0 && cookieList.Count > 1)
    //            {
    //                continue;
    //            }
    //            string item = cookieList[i];
    //            string[] splitItems = item.Split(';');
    //            if (splitItems.Length > 0)
    //            {
    //                httpRequest.Headers.Add(HttpRequestHeader.Cookie, splitItems[0]);
    //            }
    //        }

    //        if (currentStep == AppleCookieStep.GetImagePage)
    //        {
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "pxro=1");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_cc=true");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_ppv=AOS%253A%2520OLSS%253A%2520OS%253A%2520Login%2520Order%2520Lookup");
    //        }
    //        else if (currentStep == AppleCookieStep.PostAccountPage)
    //        {
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "asmetrics=%257B%2522store%2522%253A%257B%2522sid%2522%253A%2522wHF2F2PHCCCX72KDY%2522%257D%257D");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "dc=nwk");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "pxro=1");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_cc=true");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_ppv=AOS%253A%2520OLSS%253A%2520OS%253A%2520Login%2520Order%2520Lookup%2C67%2C67%2C574");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_sq=%5B%5BB%5D%5D");
    //            httpRequest.Headers.Add(HttpRequestHeader.Cookie, "sfa=us");
    //        }
    //    }

    //    private void GetAccountSummary(AccountItem account, string html)
    //    {
    //        string summary = string.Empty;
    //        //<span class="bold">Country:</span> United States
    //        string s = HtmlParser.GetOuterTextFromHtml("Country:</span>", "</div>", 1, html);
    //        if (!string.IsNullOrEmpty(s))
    //        {
    //            account.AccountSummary = HtmlParser.GetInnerTextFromHtml(s).Replace("Country:", "");
    //        }
    //    }

    //    private void GetState(AccountItem account, string html)
    //    {
    //        if ((null == account)) return;
    //        if (this.IsContains(html, "Login Successful", "Your login was successful", "You will shortly be redirected to your destination", "logout.ws"))
    //        {
    //            account.State = LoginState.LoginSucceed;
    //            this.GetLoginState(account);
    //        }
    //        else if (this.IsContains(html, "Login Failed", "Invalid Login or Password", "The username, email or password you entered was incorrect"))
    //        {
    //            account.State = LoginState.LoginFalied;
    //        }
    //        else if (this.IsContains(html, "The requested URL could not be retrieved", "The remote host or network may be down", HttpHelperBase.HTTPERROR))
    //        {
    //            account.State = LoginState.NetworkFailure;
    //        }
    //        else if (this.IsContains(html, "<div class=\"section_form\" id=\"recaptcha\">", "<div id=\"recaptcha_image\">"))
    //        {
    //            account.State = LoginState.Catpcha;
    //        }
    //        else if (string.IsNullOrEmpty(html) || this.IsContains(html, HttpHelperBase.HTTPERROR))
    //        {
    //            account.State = LoginState.LoginTooMuch;
    //        }
    //        else
    //        {
    //            account.State = LoginState.Unknown;
    //        }
    //    }

    //    private void GetLoginState(AccountItem account)
    //    {
    //        if ((null == account)) return;

    //        GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
    //        if (!string.IsNullOrEmpty((gameServer.AccountDetailUrl)))
    //        {
    //        }
    //    }
    //}

    #endregion

    public class HttperLoginBase : GameHttperBase
    {
        string PostFormat = "{0}=&theAccountName={1}&theAccountPW={2}&signInHyperLink=%E7%99%BB%E5%BD%95&" +
                            "theTypeValue=&wosid={3}&Nojive=&wosid={3}";

        public AccountItem Account
        {
            get;
            set;
        }

        public HttperLoginBase(DetectionParamsItem HttperParamsItem)
            : base(HttperParamsItem, AppleLogManager.Instance)
        {
        }

        #region Http headers

        private bool isMultiPart = false;
        protected override void SetHttpRequestPostHeader(System.Net.HttpWebRequest httpRequest)
        {
            if (!isMultiPart)
            {
                base.SetHttpRequestPostHeader(httpRequest);
                if ((null != httpRequest) && (!string.IsNullOrEmpty(this.postActionUrl)))
                {
                    httpRequest.Referer = this.postActionUrl;
                }
            }
            else
            {
                httpRequest.Method = HttpHelperBase.MethodPost;
                httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.CookieContainer = this.cookieContainer;

                this.SetCookieToHttpRequestHeader(httpRequest);
                this.SetProxy(httpRequest);
                httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                httpRequest.ContentType = "multipart/form-data; boundary=245419563ABA00BD2D9478CCB70DF28C";
                httpRequest.UserAgent = "iTunes/10.6.3 (Windows; Microsoft Windows XP Professional Service Pack 3 (Build 2600)) AppleWebKit/534.57.2";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn, zh;q=0.75, en-us;q=0.50, en;q=0.25");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
                httpRequest.Headers.Add("X-Apple-Store-Front", "143441-1,12");
                httpRequest.Headers.Add("X-Apple-Tz", "28800");

                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "z_user_info_version=0");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "mzf_in=331114");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "Pod=33");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_invisit_n2_us=0");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_membership=1%3Ait10");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_pathLength=itunes.welcomescreen%3D1%2C");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_pv=itunes%2010%20-%20welcome%20screen%20(us)");
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, "s_vnum_n2_us=0%7C1");
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

            if ((null != httpRequest) && (!string.IsNullOrEmpty(this.postActionUrl)))
            {
                httpRequest.Referer = this.postActionUrl;
            }
        }

        #endregion

        private string postActionUrl = string.Empty;
        public void GetState(AccountItem account)
        {
            if (account == null) return;
            AppleLogManager.Instance.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));

            this.Account = account;
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            string html = string.Empty;
            string postFormat = PostFormat;

            //Get login uri
            if (!string.IsNullOrEmpty(gameServer.LoginUrl))
            {
                html = this.ReadFromUrl(gameServer.LoginUrl);
                if (IsReadSucceedFromUrl(html))
                {
                    string loginUrl = HtmlParser.GetPropertyFromHtml("<a class=\"more\" href=\"/cgi-bin/WebObjects/MyAppleId.woa", "\">", "href", html);
                    if (IsReadSucceedFromUrl(loginUrl))
                    {
                        html = this.ReadFromUrl(CreateAbUrlByRel(gameServer.DomainUrl, loginUrl));
                    }
                }
            }

            if (!IsReadSucceedFromUrl(html))
            {
                html = this.ReadFromUrl("https://appleid.apple.com/cgi-bin/WebObjects/MyAppleId.woa/101/wa/directToSignIn?localang=zh_Cn");
            }
            //<form method="POST" id="signIn" name="signIn" action="/cgi-bin/WebObjects/MyAppleId.woa/101/wo/4lsbAuXWx4SntoC8xGeLtw/0.0.35.145.1">
            string postActionUrl = GetPostActionUri(html);
            if (string.IsNullOrEmpty(postActionUrl))
            {
                postActionUrl = HTTPERROR;
            }
            string[] splitItems = postActionUrl.Split('/');
            if (splitItems.Length > 2)
            {
                string id = splitItems[splitItems.Length - 1];
                string random = splitItems[splitItems.Length - 2];
                string data = string.Format(postFormat, id, Uri.EscapeDataString(this.Account.EMail), Uri.EscapeDataString(this.Account.Password).Replace("!", "%21"), random);
                html = this.ReadUrlContent(postActionUrl, data);
            }

            this.GetState(account, html);
            if (account.IsLoginSucceed)
            {
                GetLocation(account, html);
                //GetDetail(account);
            }
        }


        private bool IsReadSucceedFromUrl(string html)
        {
            return !string.IsNullOrEmpty(html) && html != HTTPERROR;
        }

        private string GetPostActionUri(string html)
        {
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            if (string.IsNullOrEmpty(html))
            {
                return gameServer.LoginPostActionUrl;
            }
            string actionUrl = HtmlParser.GetSubString(html, "<form method=\"POST\" id=\"signIn\" name=\"signIn\" action=\"", "\">",
                                                       gameServer.LoginPostActionUrl);

            if (actionUrl != gameServer.LoginPostActionUrl)
            {
                actionUrl = CreateAbUrlByRel(gameServer.DomainUrl, actionUrl);
            }
            return actionUrl;
        }

        private string CreateAbUrlByRel(string domainUrl, string relUri)
        {
            string absUrl = string.Empty;
            if (domainUrl.EndsWith("/"))
            {
                if (relUri.StartsWith("/"))
                {
                    absUrl = domainUrl + relUri.Substring(1);
                }
                else
                {
                    absUrl = domainUrl + relUri;
                }
            }
            else
            {
                if (relUri.StartsWith("/"))
                {
                    absUrl = domainUrl + relUri;
                }
                else
                {
                    absUrl = domainUrl + "/" + relUri;
                }
            }
            return absUrl;
        }

        private void GetState(AccountItem account, string html)
        {
            if ((null == account)) return;
            if (this.IsContains(html, "<span id = \"primary-email\" class = \"email\">", "欢迎", "已验证", "您可以随时更改您的 Apple ID", "储存更改") &&
                   this.IsContains(html, "注销"))
            {
                account.State = LoginState.LoginSucceed;

                if (this.IsContains(html, "已验证", "<span class=\"verified\">"))
                {
                    account.State = LoginState.LoginSucceedAndVerified;
                }
            }
            else if (this.IsContains(html, "21607", HTTPERROR))
            {
                account.State = LoginState.NetworkFailure;
            }
            else if (this.IsContains(html, "您输入的 Apple ID 或密码不正确", " 密码错误"))
            {
                account.State = LoginState.LoginFalied;
            }
            else if (this.IsContains(html, "此 Apple ID 已被锁"))
            {
                account.State = LoginState.Locked;
            }
            else if (this.IsContains(html, "<span class=\"input-msg red show\">这个人不在激活状态。</span>"))
            {
                account.State = LoginState.Unactive;
            }
            else if (this.IsContains(html, "验证您的电子邮件地址"))
            {
                account.State = LoginState.VerifyMail;
            }
            //密保
            //else if (this.IsContains(html, "验证您的电子邮件地址"))
            //{
            //    account.State = LoginState.VerifyMail;
            //}
            else
            {
                account.State = LoginState.Unknown;
            }
        }

        private void GetLocation(AccountItem account, string html)
        {
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            string addrUrl = HtmlParser.GetPropertyFromHtml("id=\"addressLink\"", "</a>", "href=", html);
            if (!string.IsNullOrEmpty(addrUrl))
            {
                addrUrl = CreateAbUrlByRel(gameServer.DomainUrl, addrUrl);
                string addrHtml = ReadFromUrl(addrUrl);
                if (!string.IsNullOrEmpty(addrHtml))
                {
                    string addrDetail = HtmlParser.GetSubString(addrHtml, "<div class=\"address-info\">", "</div>");
                    if (!string.IsNullOrEmpty(addrDetail))
                    {
                        IList<string> addrList = HtmlParser.GetInnerTextListFromHtml("<span>", "</span>", addrDetail);
                        string location = string.Empty;
                        foreach (string item in addrList)
                        {
                            location += item + "- ";
                        }
                        account.Location = location;
                    }
                }
            }
        }
        private bool GetDetail(AccountItem account)
        {
            GameServer gameServer = this.DetectionParamsItem.CurrentGameServer;
            if (null == account || string.IsNullOrEmpty(gameServer.AccountDetailUrl))
            { return false; }

            string accountSummaryUrl = "https://buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/accountSummary";
            string html = this.ReadFromUrl(accountSummaryUrl);


            string metricsUrl = "http://metrics.apple.com/b/ss/applesuperglobal/1/G.6--NS?pageName=BrowserRedirect&pccr=true&ch=BrowserRedirect&h5=appleitmsna%2Cappleitmsus";
            html = this.ReadFromUrl(metricsUrl);
            //string initUrl = "http://ax.init.itunes.apple.com./bag.xml?ix=4";
            //html = this.ReadFromUrl(initUrl);
            //html = this.ReadFromUrl(initUrl);
            isMultiPart = true;
            string buyUrl = "https://buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/accountSummary?ign-mscache=1";
            html = this.ReadFromUrl(buyUrl, true);
            //html = this.ReadFromUrl(initUrl);

            string boundary = "--245419563ABA00BD2D9478CCB70DF28C";
            string newLine = Environment.NewLine;
            string postFormat = boundary + newLine +
                                "Content-Disposition: form-data; name=\"machineName\"" + newLine + newLine +
                                Environment.MachineName + newLine +
                                boundary + newLine +
                                "Content-Disposition: form-data; name=\"attempt\"" + newLine + newLine +
                                "1" + newLine +
                                boundary + newLine +
                                "Content-Disposition: form-data; name=\"why\"" + newLine + newLine +
                                "serverDialog" + newLine +
                                 boundary + newLine +
                                "Content-Disposition: form-data; name=\"guid\"" + newLine + newLine +
                                "1334361A.FE82C235.3EFDECB0.D47B6924.14DF9387.775CDDA0.3EE73B7E" + newLine +
                                 boundary + newLine +
                                "Content-Disposition: form-data; name=\"password\"" + newLine + newLine +
                                "{0}" + newLine +
                                  boundary + newLine +
                                "Content-Disposition: form-data; name=\"appleId\"" + newLine + newLine +
                                "{1}" + newLine +
                                 boundary + newLine +
                                "Content-Disposition: form-data; name=\"createSession\"" + newLine + newLine +
                                "true" + newLine +
                                 boundary + "--" + newLine + newLine;

            html = this.ReadUrlContent(gameServer.AccountDetailUrl, string.Format(postFormat, account.Password, account.User));
            isMultiPart = false;
            return html.Length > 0;
        }
    }

}
