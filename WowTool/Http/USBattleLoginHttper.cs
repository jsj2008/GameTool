using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using PublicUtilities;

namespace WebDetection
{
    public partial class USBattleLoginHttper : WowHttperLoginBase
    {
        private const string POSTCONTENTFORMAT = "accountName={0}&password={1}&persistLogin=on";
        private const string POSTCONTENTWITHCAPTCHAFORMAT = "accountName={0}&password={1}&securityAnswer={2}";
        private const string POSTCHARACTERCONTENTFORMAT = "_target0=&selectedRealm={0}";
        private Stopwatch stopwatch = new Stopwatch();

        public USBattleLoginHttper(DetectionParamsItem paramsItem)
            : base(paramsItem, WowLogManager.Instance)
        {
            this.PostContentFormat = USBattleLoginHttper.POSTCONTENTFORMAT;
            this.PostContentWithCaptchaFormat = USBattleLoginHttper.POSTCONTENTWITHCAPTCHAFORMAT;
            this.PostCharacterContentFormat = USBattleLoginHttper.POSTCHARACTERCONTENTFORMAT;
        }

        private int captchaErrorCount = 0;
        public int CaptchaErrorCount
        {
            get { return this.captchaErrorCount; }
        }

        public override UserAccountItem GetAccountDetail(UserAccountItem accountItem)
        {
            this.curretnAccountItem = accountItem;
            string httpContent = this.GetHttpLoginContent(accountItem);
            accountItem.State = this.GetAccountState(httpContent);
            if (!accountItem.IsErrored)
            {
                if (!this.DetectionParamsItem.IsGetDetail)
                {
                    accountItem.Items = this.GetGameAccountItemList(httpContent);
                    this.SetAccountCount(accountItem);
                    return accountItem;
                }

                this.GetSubGameAccountDetail(accountItem);
                this.GetCharacters(accountItem);
            }
            else
            {
                captchaErrorCount++;
            }

            return accountItem;
        }

        /// <summary>
        /// if input incorrect captcha /httperror/website error,repeat again
        /// </summary>
        /// <param name="accountItem"></param>
        public override UserAccountItem TakeMoreTryForErrored(UserAccountItem accountItem)
        {
            if (accountItem.IsErrored)
            {
                for (int i = 0; i < this.DetectionParamsItem.ErrorRepeatCount; i++)
                {
                    WowLogManager.Instance.InfoWithCallback(string.Format("重试第 {0} 次前状态，{1}", i + 1, accountItem.UserDetail));
                    string httpContent = this.GetHttpLoginContent(accountItem);
                    accountItem.State = this.GetAccountState(httpContent);

                    // cpatcha code is correct
                    if (!accountItem.IsErrored)
                    {
                        if (!this.DetectionParamsItem.IsGetDetail)
                        {
                            accountItem.Items = GetGameAccountItemList(httpContent);
                            SetAccountCount(accountItem);
                            return accountItem;
                        }

                        this.GetSubGameAccountDetail(accountItem);
                        this.GetCharacters(accountItem);
                        WowLogManager.Instance.InfoWithCallback(string.Format("第 {0} 次重试，{1}", i + 1, accountItem.UserDetail));
                        break;
                    }
                    else
                    {
                        captchaErrorCount++;
                        WowLogManager.Instance.InfoWithCallback(string.Format("第 {0} 次重试，{1}", i + 1, accountItem.UserDetail));
                    }
                }

                if (this.captchaErrorCount > this.DetectionParamsItem.CaptchaErrorCount)
                {
                    accountItem.State = WowLoginStates.TooMuchFailedChapcha;
                }
            }

            return accountItem;
        }

        /// <summary>
        /// 选择子帐号，并再查子帐号明细
        /// </summary>
        /// <param name="accountItem"></param>
        protected virtual void GetSubGameAccountDetail(UserAccountItem accountItem)
        {
            if (accountItem.IsCanGetDetail)
            {
                IList<GameAccountItem> accountList = this.GetGameAccountItem();
                if (accountList.Count > 0)
                {
                    foreach (GameAccountItem account in accountList)
                    {
                        account.Detail = this.GetGameAccountDetail(account);
                    }
                }

                accountItem.Items = accountList;
                SetAccountCount(accountItem);
            }
        }

        protected void SetAccountCount(UserAccountItem accountItem)
        {
            if ((null == accountItem) || (accountItem.Items == null)) return;
            if (accountItem.Items.Count == 0)
            {
                accountItem.State = WowLoginStates.LoginWithNoGameAccount;
            }
            else if (accountItem.Items.Count == 1)
            {
                accountItem.State = WowLoginStates.SingleGameAccount;
            }
            else if (accountItem.Items.Count > 1)
            {
                accountItem.State = WowLoginStates.MultiGameAccount;
            }
        }

        protected virtual string GetHttpLoginContent(UserAccountItem accountItem)
        {
            string postData = string.Format(PostContentFormat, accountItem.EMail, accountItem.Password);
            string postCaptchaData = string.Format(PostContentWithCaptchaFormat, accountItem.EMail, accountItem.Password, "{0}");
            string content = string.Empty;
            //New query need cookie be empty;
            this.ClearCookie();

            WowLogManager.Instance.Info(string.Format("-> post data:第{0}个 {1} to {2}", accountItem.Index, accountItem.EMail, this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl));
            this.cookie = string.Empty;
            bool isHasCaptcha = this.PostDataAndCheckCaptcha(this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl,
                                                             this.DetectionParamsItem.CurrentGameServer.DomainUrl, postData,
                                                             ref postCaptchaData, ref content);
            //if (!isHasCaptcha)
            {
                return content;
            }

            //WowLogManager.Instance.Info(string.Format("-> post data with captcha:第{0}个 {1} to {2}", accountItem.Index, accountItem.EMail, this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl));
            //HttpWebRequest httpRequest = this.WriteToHttpWebRequest(this.DetectionParamsItem.CurrentGameServer.LoginPostActionUrl, postCaptchaData);
            //try
            //{
            //    if (null != httpRequest)
            //    {
            //        // TODO:use the same session id
            //        using (HttpWebResponse httpReponse = httpRequest.GetResponse() as HttpWebResponse)
            //        {
            //            if (null != httpReponse)
            //            {
            //                using (StreamReader sr = new StreamReader(GetGzipStream(httpReponse), Encoding.UTF8))
            //                {
            //                    return sr.ReadToEnd();
            //                }
            //            }
            //        }
            //    }
            //    return HttpHelperBase.HTTPERROR;
            //}
            //catch (Exception ex)
            //{
            //    WowLogManager.Instance.Error(string.Format("USBattleHttpHelper.GetHttpLoginContent() error:{0}", ex.Message));
            //}
            //finally
            //{
            //    this.DisposeHttpRequest(httpRequest);
            //}
            //return content;
        }

        /// <summary>=
        /// If a user account need accept agreement
        /// </summary>
        private bool AcceptAgreement(string content, DetectionParamsItem serverItem)
        {
            #region HtmlSampleCode

            //<label for="agree1">
            //<input type="checkbox" id="agree1" name="agree1" disabled="disabled"
            //class="legalCheckbox" style="visibility:hidden" onclick="login.scrollAccept.accept(this)" />
            //I accept the Terms of Use applicable to my country of residence.<span class="important">&#42;</span>
            //</label>

            //Post data:agree1=on

            #endregion HtmlSampleCode

            string result = string.Empty;

            if (this.IsContains(content, "legalAgreement", "<label for=\"agree1\">"))
            {
                string postContent = "agree1=on";
                HttpWebRequest httpRequest = this.WriteToHttpWebRequest(serverItem.CurrentGameServer.LoginPostActionUrl, postContent);

                try
                {
                    if (null != httpRequest)
                    {
                        result = this.ReadFromHttpWebResponse(httpRequest);
                        this.DisposeHttpRequest(httpRequest);
                    }
                }
                catch (Exception ex)
                {
                    WowLogManager.Instance.Error(string.Format("USBattleHttpHelper.AcceptAgreement() error:{0}", ex.Message));
                }

                return true;
            }

            return false;
        }

        protected virtual string GetGameAccountDetail(GameAccountItem accountItem)
        {
            #region HtmlSampleCode

            //https://us.battle.net/account/management/wow/dashboard.html?region=US&accountName=COMRVF&ST=US-575779-Kpe1pnm5TCCvZfGYu6PeLZbEv2pWzbdRJCf

            #endregion HtmlSampleCode

            HttpWebRequest httpRequest = this.GetHttpWebRequest(accountItem.DetailUrl, true);
            if (null != httpRequest)
            {
                try
                {
                    string content = this.ReadFromHttpWebResponse(httpRequest);
                    return ParseGameAccountDetail(content).Trim();
                }
                finally
                {
                    this.DisposeHttpRequest(httpRequest);
                }
            }
            return string.Empty;
        }

        protected virtual string ParseGameAccountDetail(string content)
        {
            StringBuilder sb = new StringBuilder();
            if (this.IsContains(content, "This account has been temporarily disabled",
                "Account Banned", "您無法登入此頁面，因為此協議已被禁止、鎖住或停用"))
            {
                return CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.TempDisabled);
            }
            if (this.IsContains(content, "section account-details"))
            {
                string outterHtml = HtmlParser.GetOuterTextFromHtml("section account-details", "<dl>", "</dl>", 1, content);
                if (!string.IsNullOrEmpty(outterHtml))
                {
                    sb.AppendLine(HtmlParser.GetInnerTextFromHtml(outterHtml));
                }
            }

            if (this.IsContains(content, "subscriptionData"))
            {
                string outterHtml = HtmlParser.GetOuterTextFromHtml("subscriptionData", "<table>", "</table>", 1, content);
                if (!string.IsNullOrEmpty(outterHtml))
                {
                    sb.AppendLine(HtmlParser.GetInnerTextFromHtml(outterHtml));
                }
            }

            return sb.ToString();
        }

        #region Get game account item

        private IList<GameAccountItem> GetGameAccountItem()
        {
            #region script code

            //<div class="acctsel1">
            //   <label><input type="radio" id="accountName" name="accountName" value="COMRVF"
            //     checked />
            //    <strong>COMRVF</strong> (US)
            //    </label>
            //</div>

            #endregion script code

            //accountName=COMRVF&x=85&y=7
            //accountName=WoW2&x=34&y=6

            string content = this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.SelectAccountUrl);
            //string content = this.ReadUrlContent(this.RedirectLocation);
            return GetGameAccountItemList(content);
            //return GetGameAccountItem(content);
        }

        /// <summary>
        /// 列出有多少个子帐号，不再进一步查明细
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected virtual IList<GameAccountItem> GetGameAccountItemList(string content)
        {
            //<ul id="game-list-wow">
            IList<GameAccountItem> gameAccountList = new List<GameAccountItem>();

            //Get D3 accounts
            string d3ListContent = HtmlParser.GetOuterTextFromHtml("<ul id=\"game-list-d3\">", "</ul>", 1, content);
            if (!string.IsNullOrEmpty(d3ListContent))
            {
                while ((d3ListContent.IndexOf("<li class=\"border-4") > 0) ||
                    (d3ListContent.IndexOf("<li class=\"disabled unloaded border-4") > 0))
                {
                    string gameContent = HtmlParser.GetOuterTextFromHtml("<li class=\"border-4", "</li>", 1, d3ListContent);
                    if (string.IsNullOrEmpty(gameContent))
                    {
                        gameContent = HtmlParser.GetOuterTextFromHtml("<li class=\"disabled unloaded border-4", "</li>", 1, d3ListContent);
                    }
                    
                    if (!string.IsNullOrEmpty(gameContent))
                    {
                        GameAccountItem item = this.GetD3AccountItemFromHtml(gameContent);
                        if (null != item)
                        {
                            gameAccountList.Add(item);
                        }
                        d3ListContent = d3ListContent.Replace(gameContent, "");
                    }
                }
            }

            //Get wow accounts
            string wowListContent = HtmlParser.GetOuterTextFromHtml("<ul id=\"game-list-wow", "</ul>", 1, content);
            if (!string.IsNullOrEmpty(wowListContent))
            {
                while ((wowListContent.IndexOf("<li class=\"border-4") > 0) ||
                    (wowListContent.IndexOf("<li class=\"disabled unloaded border-4") > 0))
                {
                    string gameContent = HtmlParser.GetOuterTextFromHtml("<li class=\"border-4", "</li>", 1, wowListContent);
                    if (string.IsNullOrEmpty(gameContent))
                    {
                        gameContent = HtmlParser.GetOuterTextFromHtml("<li class=\"disabled unloaded border-4", "</li>", 1, wowListContent);
                    }
                    if (!string.IsNullOrEmpty(gameContent))
                    {
                        GameAccountItem item = this.GetWowAccountItemFromHtml(gameContent);
                        if (null != item)
                        {
                            gameAccountList.Add(item);
                        }
                        wowListContent = wowListContent.Replace(gameContent, "");
                    }
                }
            }

            return gameAccountList;
        }

        protected virtual GameAccountItem GetD3AccountItemFromHtml(string accountHtml)
        {
            //<span class="account-link">
            //<strong>
            //<a href="/account/management/d3/dashboard.html">
            //《暗黑破壞神?III》
            //</a>
            //</strong>
            //<span class="account-id"><span class="account-edition">標準版</span></span>
            //</span>

            //<span class="account-link">
            //<strong>
            //<a href="/account/management/digital-purchase.html?product=D3">
            //立即購買《暗黑破壞神?III》！
            //</a>
            //</strong>
            //</span>


            if (!string.IsNullOrEmpty(accountHtml))
            {
                string linkStr = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<a href=\"/account/management/", "</a>", 1, accountHtml)).Trim();
                string edition = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-edition", "</span>", 1, accountHtml)).Trim();
                GameAccountItem gameItem = new GameAccountItem() { Detail = string.Format("{0} - {1}", linkStr, string.IsNullOrEmpty(edition)?"无效":edition) };
                return gameItem;
            }

            return null;
        }
        protected virtual GameAccountItem GetWowAccountItemFromHtml(string accountHtml)
        {
            if (!string.IsNullOrEmpty(accountHtml))
            {
                string url = HtmlParser.GetOutterPropertyFromHtml(accountHtml, "href");
                if (url.Contains("amp;"))
                {
                    url = url.Replace("amp;", "");
                }
                //May be contains <span > in account_id <span>
                string id = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-id>", " <", 1, accountHtml)).Trim();

                if (string.IsNullOrEmpty(id))
                {
                    id = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-id", "</span>", 1, accountHtml)).Trim();
                }
                string edition = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-edition", "</span>", 1, accountHtml)).Trim();
                string region = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-region", "</span>", 1, accountHtml)).Trim();
                id.Replace("[", "");
                id.Replace("]", "");

                GameAccountItem gameItem = new GameAccountItem() { Name = id, Edition = edition, Region = region, DetailUrl = url };
                return gameItem;
            }

            return null;
        }

        #region old get subaccount methods

        //private IList<GameAccountItem> GetGameAccountItem(string content)
        //{
        //    IList<string> accountList = GetRadioAccount(content);
        //    IList<GameAccountItem> gameAccountList = new List<GameAccountItem>();
        //    if (accountList.Count > 0)
        //    {
        //        foreach (string account in accountList)
        //        {
        //            GameAccountItem item = ParseGameAccountItem(account);
        //            if (null != item)
        //            {
        //                gameAccountList.Add(item);
        //            }
        //        }
        //    }

        //    return gameAccountList;
        //}

        //private GameAccountItem ParseGameAccountItem(string account)
        //{
        //    const string accountPref = "value";

        //    if (string.IsNullOrEmpty(account))
        //    {
        //        return null;
        //    }

        //    string[] values = account.Replace("\"", "").Split(HttpHelperBase.BlankChars, StringSplitOptions.RemoveEmptyEntries);
        //    for (int i = values.Length - 1; i >= 0; i--)
        //    {
        //        string item = values[i];
        //        if (item.StartsWith(accountPref, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            string[] subItems = item.Split('=');
        //            if (subItems.Length == 2)
        //            {
        //                return new GameAccountItem() { Name = subItems[1] };
        //            }
        //        }
        //    }

        //    return null;
        //}

        //private IList<string> GetRadioAccount(string content)
        //{
        //    const string radioAccount = "type=\"radio\" id=\"accountName\"";
        //    const int radioLength = 80;
        //    IList<string> accountList = new List<string>();
        //    while (content.IndexOf(radioAccount) != -1)
        //    {
        //        string account = content.Substring(content.IndexOf(radioAccount), radioLength);
        //        content = content.Replace(account, "");
        //        if (!string.IsNullOrEmpty(content))
        //        {
        //            accountList.Add(account);
        //        }
        //    }

        //    return accountList;
        //}

        #endregion
        #endregion

        #region  Get Account State

        protected virtual WowLoginStates GetAccountState(string httpContent)
        {
            if (string.IsNullOrEmpty(httpContent))
                return WowLoginStates.LoginWithEmptyResponse;
            //网站维护
            else if (this.IsContains(httpContent, "maintenancelogo", "Service Temporarily Unavailable"))
                return WowLoginStates.WebSiteMaintain;
            //OK
            else if (IsContains(httpContent, "<span class='status-active'>", "action=\"change-account.html\"", "Account Management", "Welcome"))
                return GetLoginStates(httpContent);
            //密码无效
            else if (IsContains(httpContent, "Password invalid", "Invalid account name", "The username or password is incorrect.",
                "無效的密碼", "使用者名稱或密碼錯誤。請重試一次"))
                return WowLoginStates.InvalidPassword;
            //锁号
            else if (IsContains(httpContent, "this account has been locked"))
                return WowLoginStates.Locked;
            //登录太多锁号
            else if (IsContains(httpContent, "Due to suspicious activity, this account has been locked",
                "A message has been sent to this account’s email address"))
                return WowLoginStates.TryTooMuchLocked;
            //网络出错
            else if (IsContains(httpContent, HttpHelperBase.HTTPERROR))
                return WowLoginStates.HttpError;
            //网站出错
            else if (IsContains(httpContent, "An error has occurred."))
                return WowLoginStates.WebSiteError;
            //暂封
            else if (IsContains(httpContent, "This account has been temporarily disabled", "<span class=\"brownText\">"))
                return WowLoginStates.TempDisabled;
            //永封
            else if (IsContains(httpContent, "This account has been permanently disabled"))
                return WowLoginStates.PermanentDisabled;
            //帐户不存在（空号）
            else if (IsContains(httpContent, "No World of Warcraft account detected",
                "account does not have any World of Warcraft accounts associated with it",
                "account currently does not have any World of Warcraft accounts associated with it"))
                return WowLoginStates.IsNotExist;
            //登录太频繁，可能需要重启或换IP
            else if (IsContains(httpContent, "Too many attempt", "嘗試次數過多"))
                return WowLoginStates.TooManyAttempt;
            //密保（受权码）
            else if (IsContains(httpContent, "Authenticator Code", "class=\"authMessage\"", "name=\"authValue\"", "已偵測到驗證器"))
                return WowLoginStates.AuthenticatorCode;
            //验证码输入错误
            else if (IsContains(httpContent, "Wrong characters entered. Please try again"))
                return WowLoginStates.IncorrectCaptcha;
            //登录名缺失
            else if (IsContains(httpContent, "Account name required"))
                return WowLoginStates.MissAccount;
            //到期卦号
            else if (IsContains(httpContent, "Freeze"))
                return WowLoginStates.TimeOut;
            //非战时间到
            else if (IsContains(httpContent, "#acctdetails", "Frozen"))
                return WowLoginStates.Unbattle_TCB;
            //不是当前登录战网游戏账号
            else if (IsContains(httpContent, "No World of Warcraft account detected",
                "Please log in with your associated Battle.net account"))
                return WowLoginStates.IsNotCurretServerAccount;
            //验证码
            else if (IsContains(httpContent, "captcha.jpg"))
                return WowLoginStates.NeedCaptcha;
            else
                return WowLoginStates.Unknown;
        }

        protected virtual WowLoginStates GetLoginStates(string httpContent)
        {
            //选择游戏帐号
            if (IsContains(httpContent, "Select a Game Account to Manage"))
                return WowLoginStates.MultiGameAccount;
            //登录无帐号
            else if (IsContains(httpContent, "沒有聯絡資料"))
                return WowLoginStates.LoginWithNoGameAccount;
            //暂封
            else if (IsContains(httpContent, "This account has been temporarily disabled"))//, "Game Time Expires", "<span class=\"brownText\">"
                return WowLoginStates.TempDisabled;
            //永封
            else if (IsContains(httpContent, "This account has been permanently disabled"))
                return WowLoginStates.PermanentDisabled;
            //测试账号过期
            else if (IsContains(httpContent, "Your trial has expired"))
                return WowLoginStates.TestAccountOutOfExpire;
            //试用
            else if (IsContains(httpContent, "Your trial has", "Trial expires", "(TESTE)", "Expirado"
                , "（體驗帳號）", "（已到期）", "(試玩)", "試玩到期"))
                return WowLoginStates.Trial;
            //帐户不存在（空号）
            else if (IsContains(httpContent, "No World of Warcraft account detected",
                "account does not have any World of Warcraft accounts associated with it",
                "account currently does not have any World of Warcraft accounts associated with it"))
                return WowLoginStates.IsNotExist;
            //正常登录，可以获取明细
            else if (IsContains(httpContent, "Welcome"))
                return WowLoginStates.LoginWithAccount;
            //时间到
            else if (IsContains(httpContent, "Freeze", "已凍結"))
                return WowLoginStates.TimeOut;
            //受权码
            else if (IsContains(httpContent, "Authenticator Code"))
                return WowLoginStates.AuthenticatorCode;
            //非战TBC可点10
            else if (IsContains(httpContent, "hp_button-payupgradebc-wow.jpg", "hp_button-payupgradebc-upgrade.jpg"))
                return WowLoginStates.Unbattle_TCB;
            //非战: 可点WLK
            else if (IsContains(httpContent, "hp_button-payupgrade_wrath-bc.jpg", "hp_button-payupgrade_wrath-upgrade.jpg"))
                return WowLoginStates.Unbattle_WLK;
            //非战: 可点正式
            else if (IsContains(httpContent, "hp_button-renewsubs.jpg"))
                return WowLoginStates.Unbattle_OK;
            //非战——登录成功,状态未知
            else if ((null != this.curretnAccountItem) && (!TextHelper.IsMail(this.curretnAccountItem.EMail)))
            {
                return WowLoginStates.UnbattleSucceedUnknown;
            }
            else
                return WowLoginStates.SucceedUnknown;
        }

        #endregion
    }
}