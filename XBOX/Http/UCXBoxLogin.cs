using System;
using System.Windows.Forms;
using PublicUtilities;
using PublicUtilities.Helpers;
using System.Threading;

namespace XBOX.Http
{
    public partial class UCXBoxLogin : UserControl, IDisposable
    {
        public DetectionParamsItem HttperParamsItem
        { get; protected set; }

        public LogManagerBase LogManager
        {
            get;
            set;
        }

        public UCXBoxLogin(DetectionParamsItem paramsItem, LogManagerBase logManager)
        {
            InitializeComponent();

            if (null == paramsItem)
            {
                throw new ArgumentNullException("paramsItem");
            }
            if (null == logManager)
            {
                throw new ArgumentNullException("logManager");
            }

            CookieCleaner.CleanCookies("xbox");
            this.HttperParamsItem = paramsItem;
            this.LogManager = logManager;
            this.Server = this.HttperParamsItem.CurrentGameServer;
            this.webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
            this.webBrowser.AllowNavigation = true;
            this.webBrowser.ScriptErrorsSuppressed = true;
            //this.webBrowser.Document.Cookie
        }
        #region Timer

        System.Windows.Forms.Timer timer = null;

        private void StartTimer()
        {
            StopTimer();
            if (null == timer)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 30 * 1000;
                timer.Tick += new EventHandler(timer_Tick);
            }
            if(!timer.Enabled)
            {
                timer.Start();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.CurrentAccount.State = LoginState.Unknown;
            this.Stop();
            autoResetEvent.Set();
        }

        private void StopTimer()
        {
            if((null != timer)&& timer.Enabled)
            {
                timer.Stop();
            }
        }

        #endregion

        #region Browser event

        const long Timeout = 2 * 1000;
        long tick = Environment.TickCount;

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = webBrowser.Document;
            string html = doc.Body.OuterHtml;
            if (string.IsNullOrEmpty(html) || html == "\r\n<BODY onload=evt_Login_onload(event);></BODY>")
            {
                return;
            }
            else if (IsLoginTooMuch(html))
            {
                this.CurrentAccount.State = LoginState.Catpcha;
                this.Stop();
                autoResetEvent.Set();
                return;
            }

            if ((Environment.TickCount - tick) < Timeout)
            {
                return;
            }
            tick = Environment.TickCount;
            this.StartTimer();

            if (!isLogined && !isSignout)
            {
                isSignout = Signout();
            }
            // the sign in page
            bool isSucceed = false;
            if (!isLogined)
            {
                isSucceed = PostDataAndLogin();
            }

            if (isLogined)
            {
                GetLoginState();
            }
            isLogined = isSucceed;
        }

        private bool isLogined = false;
        private bool PostDataAndLogin()
        {
            HtmlDocument doc = webBrowser.Document;
            if ((null == doc) ||
                !HtmlParseHelper.IsContains(doc.Body.OuterHtml, "Sign in", "i0116", "Keep me signed in"))
            {
                return false;
            }

            //<div id="idDiv_PWD_UsernameTb" class="cssTextInput"><div style="position: relative; width: 100%; ">
            //<input type="text" name="login" id="i0116" maxlength="113" style="width: 100%; ">

            HtmlElement eleAccount = doc.GetElementById("i0116");
            if (null != eleAccount)
            {
                eleAccount.InnerText = this.CurrentAccount.User;
            }
            //<div id="idDiv_PWD_PasswordTb" class="cssTextInput">
            //<input name="passwd" type="password" id="i0118" maxlength="16" autocomplete="off" style="width: 100%; "></div>
            HtmlElement elePwd = doc.GetElementById("i0118");
            if (null != elePwd)
            {
                elePwd.InnerText = this.CurrentAccount.Password;
            }
            //<input type="checkbox" name="KMSI" id="idChkBx_PWD_KMSI0Pwd" value="1" class="cssCheckbox">
            HtmlElement eleKeep = doc.GetElementById("idChkBx_PWD_KMSI0Pwd");
            if (null != eleKeep)
            {
                eleKeep.SetAttribute("checked", "checked");
            }
            //<input type="submit" name="SI" id="idSIButton9" value="Sign in" style="height: 25px; ">
            HtmlElement btnLogin = doc.GetElementById("idSIButton9");
            if (null != btnLogin)
            {
                LogManager.Info(string.Format("正在查询{0} ,{1}", CurrentAccount.Index, CurrentAccount.EMail));
                btnLogin.InvokeMember("click");
                return true;
            }
            Navigate();
            return false;
        }

        bool isSignout = false;
        private bool Signout()
        {
            HtmlDocument doc = webBrowser.Document;
            if ((null == doc) ||
                !HtmlParseHelper.IsContains(doc.Body.OuterHtml, "Sign Out", "https://live.xbox.com/Account/Signout"))
            {
                return false;
            }

            if (doc.Body.OuterHtml.Contains("Sign Out"))
            {
                HtmlElement btnSignout = doc.GetElementById("RpsSignInLink");
                if (null != btnSignout)
                {
                    LogManager.Info(string.Format("正在退出{0}", Server.LoginUrl));
                    btnSignout.InvokeMember("click");
                    return true;
                }
            }
            return false;
        }

        private void Navigate()
        {
            this.webBrowser.Navigate(Server.LoginUrl);
        }

        #endregion

        #region Businesss

        private void GetLoginState()
        {
            HtmlDocument doc = webBrowser.Document;
            if ((null == doc) || (null == doc.Body))
            {
                this.CurrentAccount.State = LoginState.Unknown;
            }

            string html = doc.Body.OuterHtml;

            // <a name="RpsSignInLink" href="https://live.xbox.com/Account/Signout">Sign Out</a>
            // <span class="spread"><a href="https://live.xbox.com/en-US/Account?xr=mebarnav" >My Account</a></span>
            if (HtmlParseHelper.IsContains(html, "<a name=\"RpsSignInLink\" href=\"https://live.xbox.com/Account/Signout\">Sign Out</a>",
                "https://live.xbox.com/Account/Signout", "Sign Out", "https://live.xbox.com/en-US/Account?xr=mebarnav", "My Account"))
            {

                this.CurrentAccount.State = LoginState.LoginSucceed;
            }
            else if (HtmlParseHelper.IsContains(html, "The email address or password is incorrect",
                                            "idTd_Tile_ErrorMsg_Login", "<td class=\"cssError\" id=\"idTd_Tile_ErrorMsg_Login\">"))
            {
                this.CurrentAccount.State = LoginState.LoginFalied;
            }
            else if (HtmlParseHelper.IsContains(html, "That Windows Live ID doesn't exist", "Enter a different ID or get a new one."))
            {
                this.CurrentAccount.State = LoginState.NotExisted;
            }
            else if (HtmlParseHelper.IsContains(html, "We need you to add some security info for your account", "Enter your contact information"))
            {
                this.CurrentAccount.State = LoginState.AdditionalSecurity;
            }
            else if (string.IsNullOrEmpty(html) || HtmlParseHelper.IsContains(html, HttpHelperBase.HTTPERROR,
                "You've tried to sign in too many times with an incorrect email address or password", "Enter the characters you see",
                "recaptcha"))
            {
                this.CurrentAccount.State = LoginState.Catpcha;
            }
            else
            {
                this.CurrentAccount.State = LoginState.Unknown;
            }
            autoResetEvent.Set();
        }

        private bool IsLoginTooMuch(string html)
        {
            return string.IsNullOrEmpty(html) || HtmlParseHelper.IsContains(html,
                 "You've tried to sign in too many times with an incorrect email address or password",
                 "Enter the characters you see", "recaptcha");
        }

        #endregion

        #region  Properties

        public AccountItem CurrentAccount { get; set; }
        public GameServer Server { get; set; }
        private AutoResetEvent autoResetEvent = null;

        #endregion

        #region IWebsetSignIn Members

        public void GetState(AccountItem account, AutoResetEvent resetEvent)
        {
            if (null == account)
            {
                return;
            }
            LogManager.Info(string.Format("正在查询{0} ,{1}", account.Index, account.EMail));
            this.CurrentAccount = account;
            autoResetEvent = resetEvent;
            Navigate();
        }

        public void Stop()
        {
            if (this.webBrowser.IsBusy)
            {
                this.webBrowser.Stop();
            }

            Signout();
            this.StopTimer();
            this.isLogined = false;
            this.isSignout = false;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Stop();
            this.webBrowser.Dispose();
        }

        #endregion
    }
}
