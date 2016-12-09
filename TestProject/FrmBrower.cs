using System.Windows.Forms;
using System.Net;

namespace TestProject
{
    public partial class FrmBrower : Form
    {
        public FrmBrower()
        {
            InitializeComponent();
            this.webBrower.DocumentCompleted += webBrower_DocumentCompleted;
            this.webBrower.AllowNavigation = true;

            this.LoadWebPage();
        }

       // bool isLogin = false;

        //private void webBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (isLogin) return;
        //    HtmlElement loginBtn = null;
        //    HtmlDocument doc = webBrower.Document;
        //    for (int i = 0; i < doc.All.Count; i++)
        //    {
        //        //this.tbShowInformation.Text = doc.All[i];
        //        if (doc.All[i].TagName.ToUpper().Equals("INPUT"))
        //        {
        //            switch (doc.All[i].Name)
        //            {
        //                case "accountName":
        //                    doc.All[i].InnerText = "speedrhonda@gmail.com";
        //                    break;
        //                case "password":
        //                    doc.All[i].InnerText = "person12";
        //                    break;
        //            }
        //        }

        //        if (doc.All[i].TagName.ToUpper().Equals("A"))
        //        {
        //            if (doc.All[i].InnerText == "Log In")
        //            {
        //                loginBtn = doc.All[i];
        //            }
        //        }
        //    }

        //    //doc.InvokeScript("Form.submit(this)");
        //    loginBtn.InvokeMember("click");  //执行按扭操作
        //    isLogin = true;
        //}

        int count = 0;
        private void webBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //count++;
            //HtmlDocument doc = webBrower.Document;
            //if(count==1)
            //{
            //    HtmlElementCollection eleCollection = doc.GetElementsByTagName("A");
            //    HtmlElement logIn = null;
            //    foreach (HtmlElement item in eleCollection)
            //    {
            //        if (item.InnerText == "管理您的帐户")
            //        {
            //            logIn = item;
            //        }
            //    }
            //    if (null != logIn)
            //    {
            //        logIn.InvokeMember("click");
            //    }
            //}
            //else if(count==2)
            //{
            //    doc.GetElementById("accountname").InnerText = "langzc@gmail.com";
            //    doc.GetElementById("accountpassword").InnerText = "122603Lzc<>?";
            //    HtmlElement btnLogin = doc.GetElementById("signInHyperLink");
            //    if (null != btnLogin)
            //    {
            //        btnLogin.InvokeMember("click");
            //    }
            //}

            if (++count != 1) return;

            HtmlDocument doc = webBrower.Document;
            doc.GetElementById("login-appleId").InnerText = "lzcj4@163.com";
            doc.GetElementById("login-password").InnerText = "122603Lzc!@#";

            HtmlElement formLogin = doc.Forms["sign-in-form"];
            HtmlElement btnLogin = doc.GetElementById("sign-in");

            if (null != btnLogin)
            {
                btnLogin.InvokeMember("click");
            }
        }

        private void LoadWebPage()
        {
            // string url = @"https://us.battle.net/login/en/login.xml?ref=https%3A%2F%2Fwww.worldofwarcraft.com%2Faccount%2F&app=wam&cr=true&rhtml=true";

            //string PostContentFormat = "accountName=speedrhonda@gmail.com&password=person12&persistLogin=on";
            //this.webBrower.Navigate("www.baidu.com");
            string url = @"https://secure1.store.apple.com/us/order/list";
            //string appleLoginUrl = @"https://appleid.apple.com/cgi-bin/WebObjects/MyInfo.woa";
            //HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            //if (null != httpRequest)
            //{
            //    httpRequest.Proxy = new WebProxy("192.168.80.222", 3128);
            //}
            //HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;
            //// httpResponse.ResponseUri;

            //this.webBrower.Navigate(httpResponse.ResponseUri);
            this.webBrower.Navigate(url);
            this.webBrower.ScriptErrorsSuppressed = true;

            // this.webBrower.Navigate(url, "", Encoding.ASCII.GetBytes(PostContentFormat), "Content-Type: application/x-www-form-urlencoded");
        }
    }
}