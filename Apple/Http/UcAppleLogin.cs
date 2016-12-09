using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace Apple.Http
{
    public partial class UcAppleLogin : UserControl, IWebsetSignIn
    {
        public UcAppleLogin()
        {
            InitializeComponent();
            this.Load += UcAppleLogin_Load;
            this.webBrowser.DocumentCompleted += webBrower_DocumentCompleted;
            this.webBrowser.AllowNavigation = true;
            this.webBrowser.ScriptErrorsSuppressed = true;
        }

        int count = 0;
        void webBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            count++;
            // the sign in page
            if (count == 1)
            {
                HtmlDocument doc = webBrowser.Document;
                HtmlElement eleAccount = doc.GetElementById("login-appleId");
                if (null != eleAccount)
                {
                    eleAccount.InnerText = this.Account;
                }
                HtmlElement elePwd = doc.GetElementById("login-password");
                if (null != elePwd)
                {
                    elePwd.InnerText = this.Password;
                }

                HtmlElement formLogin = doc.Forms["sign-in-form"];
                HtmlElement btnLogin = doc.GetElementById("sign-in");
                if (null != btnLogin)
                {
                    btnLogin.InvokeMember("click");
                }
            }
            //Check account info
            else if (count == 2)
            {

            }
        }

        void UcAppleLogin_Load(object sender, EventArgs e)
        {
            this.webBrowser.Document.Cookie = string.Empty;
            this.webBrowser.Navigate(this.Server.LoginUrl);
        }

        public UcAppleLogin(GameServer server, string user, string password)
            : this()
        {
            SignIn(server, user, password);
        }

        #region  Properties

        public GameServer Server { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        #endregion

        public void SignIn(GameServer server, string user, string password)
        {
            if (null == server)
            {
                throw new ArgumentNullException("server");
            }
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            count = 0;
            this.Server = server;
            this.Account = user;
            this.Password = password;
            ClearCookie();
            this.webBrowser.Navigate(Server.LoginUrl);
        }

        private void ClearCookie()
        {
            HtmlDocument doc = this.webBrowser.Document;
            if (null != doc)
            {
                doc.Cookie = string.Empty;
            }
        }
        public void Stop()
        {
            if (this.webBrowser.IsBusy)
            {
                this.webBrowser.Stop();
            }
        }
    }
}
