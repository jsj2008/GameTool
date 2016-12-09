using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace WebDetection
{
    public class SeaBattleLoginHttper : USBattleLoginHttper
    {
        //private const string ContentFormat = "accountName={0}+&password={1}+";
        private const string POSTCONTENTFORMAT = "accountName={0}&password={1}&persistLogin=on";
        private const string POSTCONTENTWITHCAPTCHAFORMAT = "accountName={0}&password={1}&securityAnswer={2}";


        public SeaBattleLoginHttper(DetectionParamsItem paramsItem)
            : base(paramsItem)
        {
            this.PostContentFormat = SeaBattleLoginHttper.POSTCONTENTFORMAT;
            this.PostContentWithCaptchaFormat = SeaBattleLoginHttper.POSTCONTENTWITHCAPTCHAFORMAT;
        }

        protected override void GetSubGameAccountDetail(UserAccountItem accountItem)
        {
            if (accountItem.IsCanGetDetail)
            {
                IList<GameAccountItem> accountList = this.GetGameAccountItemList(this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.SelectAccountUrl));
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

        protected override string GetGameAccountDetail(GameAccountItem accountItem)
        {
            if ((null != accountItem) && !string.IsNullOrEmpty(accountItem.DetailUrl))
            {
                string content = this.ReadFromUrl(accountItem.DetailUrl);
                if (!string.IsNullOrEmpty(content))
                {
                    return ParseGameAccountDetail(content).Trim();
                }
            }
            return string.Empty;
        }

        protected override string GetCaptchaUrl(string htmlContent)
        {
            // <p><img src="/login/captcha.jpg?random=8055856583243930139" alt=""/></p>
            string outerHtml = HtmlParser.GetOuterTextFromHtml("<img src=\"/login/captcha.jpg", @"<img", @"/>", 1, htmlContent);
            string captchaImage = HtmlParser.GetOutterPropertyFromHtml(outerHtml, "src");
            return captchaImage;
        }

        protected override WowLoginStates GetAccountState(string httpContent)
        {
            if (string.IsNullOrEmpty(httpContent))
                return WowLoginStates.LoginWithEmptyResponse;
            //网站维护
            else if (this.IsContains(httpContent, "maintenancelogo", "This service is not available", "Service Temporarily Unavailable"))
                return WowLoginStates.WebSiteMaintain;
            //OK
            else if (IsContains(httpContent, "<span class='status-active'>", "action=\"change-account.html\"",
                                              "Account Management", "Welcome", "Accountdetails", "Willkommen"))
                return GetLoginStates(httpContent);
            //验证码输入错误
            else if (IsContains(httpContent, "Wrong characters entered. Please try again"))
                return WowLoginStates.IncorrectCaptcha;
            //密码无效
            else if (IsContains(httpContent, "Password invalid", "Invalid account name",
                                             "The username or password is incorrect.", "incorrect",
                                             "Please try again", "無效的密碼", "使用者名稱或密碼錯誤。請重試一次"))
                return WowLoginStates.InvalidPassword;
            //锁号
            else if (IsContains(httpContent, "this account has been locked", "Due to suspicious activity, this account has been locked."))
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
            else if (IsContains(httpContent, "Authenticator Code", "Authenticator Detected", "<input id=\"authValue\" name=\"authValue\""
                , "已偵測到驗證器"))
                return WowLoginStates.AuthenticatorCode;
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

        protected override WowLoginStates GetLoginStates(string httpContent)
        {
            //选择游戏帐号
            if (IsContains(httpContent, "Select a Game Account to Manage"))
                return WowLoginStates.MultiGameAccount;
            //登录无帐号
            else if (IsContains(httpContent, "沒有聯絡資料"))
                return WowLoginStates.LoginWithNoGameAccount;
            //暂封
            else if (IsContains(httpContent, "This account has been temporarily disabled"))
                return WowLoginStates.TempDisabled;
            //永封
            else if (IsContains(httpContent, "This account has been permanently disabled"))
                return WowLoginStates.PermanentDisabled;
            else if (IsContains(httpContent, "<span class=\"flag trial\"", "（體驗帳號）", "（已到期）", "(試玩)", "試玩到期"))
                return WowLoginStates.Trial;
            //测试账号过期
            else if (IsContains(httpContent, "Your trial has expired"))
                return WowLoginStates.TestAccountOutOfExpire;
            //试用
            else if (IsContains(httpContent, "Your trial has", "Trial expires", "(TESTE)", "Expirado"))
                return WowLoginStates.Trial;
            //帐户不存在（空号）
            else if (IsContains(httpContent, "No World of Warcraft account detected",
                "account does not have any World of Warcraft accounts associated with it",
                "account currently does not have any World of Warcraft accounts associated with it"))
                return WowLoginStates.IsNotExist;
            //正常登录，可以获取明细
            else if (IsContains(httpContent, "Welcome", "Willkommen"))
                return WowLoginStates.LoginWithAccount;
            //时间到
            else if (IsContains(httpContent, "Freeze", "已凍結"))
                return WowLoginStates.TimeOut;
            //受权码
            else if (IsContains(httpContent, "Authenticator Code"))
                return WowLoginStates.AuthenticatorCode;
            //非战TBC可点10
            else if (IsContains(httpContent, "banner-tbc-trial.jpg", "hp_button-payupgradebc-wow.jpg", "hp_button-payupgradebc-upgrade.jpg"))
                return WowLoginStates.Unbattle_TCB;
            //非战: 可点WLK
            else if (IsContains(httpContent, "banner-classic.jpg", "banner-tbc.jpg", "hp_button-payupgrade_wrath-upgrade.jpg"))
                return WowLoginStates.Unbattle_WLK;
            //非战_WLK正式
            else if (IsContains(httpContent, "banner-wrath.jpg"))
                return WowLoginStates.Unbattle_WLK_Formal;
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
    }
}
