using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using PublicUtilities;

namespace WebDetection
{
    public class EUBattleLoginHttper : SeaBattleLoginHttper
    {
        public EUBattleLoginHttper(DetectionParamsItem paramsItem)
            : base(paramsItem)
        {
            //this.GetGameAccountItemList(null);
        }

        protected override void GetBattleCharacter(UserAccountItem userItem)
        {
            if (this.IsCanGetBattleChar)
            {
                this.battleCharList.Clear();
                this.curretnAccountItem = userItem;
                //Get all battle characters
                EUBattleCharGetter bg = new EUBattleCharGetter(0, this.DetectionParamsItem, this.LogManager, this.cookie, this.cookieContainer);
                this.battleCharList = bg.GetCharacter();
                this.SaveBattleChar();
            }
        }

        #region Removed

        //protected override void GetSubGameAccountDetail(UserAccountItem accountItem)
        //{
        //    if (accountItem.IsCanGetDetail)
        //    {
        //        IList<GameAccountItem> accountList = this.GetGameAccountItemList(this.ReadFromResponseContent(this.DetectionParamsItem.GameServer.SelectAccountUrl));
        //        if (accountList.Count > 0)
        //        {
        //            foreach (GameAccountItem account in accountList)
        //            {
        //                account.Detail = this.GetGameAccountDetail(account);
        //            }
        //        }

        //        accountItem.Items = accountList;
        //    }
        //}

        //protected IList<GameAccountItem> GetGameAccountItemList(string content)
        //{
        //    // content = new StreamReader(@"F:\MyProjects\Wow\SourceCode\Wow数据\Eu_multiHTML.txt").ReadToEnd();
        //    IList<GameAccountItem> gameAccountList = new List<GameAccountItem>();
        //    string gameListContent = HtmlParser.GetOuterTextFromHtml("<ul id=\"game-list-wow\">", "</ul>", 1, content);
        //    if (!string.IsNullOrEmpty(gameListContent))
        //    {
        //        while (gameListContent.IndexOf("<li class=\"border-4\"") > 0)
        //        {
        //            string gameContent = HtmlParser.GetOuterTextFromHtml("<li class=\"border-4\"", "</li>", 1, gameListContent);
        //            if (!string.IsNullOrEmpty(gameContent))
        //            {
        //                GameAccountItem item = this.GetAccountItemFromHtml(gameContent);
        //                if (null != item)
        //                {
        //                    gameAccountList.Add(item);
        //                }
        //                gameListContent = gameListContent.Replace(gameContent, "");
        //            }
        //        }
        //    }

        //    return gameAccountList;
        //}

        //private GameAccountItem GetAccountItemFromHtml(string accountHtml)
        //{
        //    if (!string.IsNullOrEmpty(accountHtml))
        //    {
        //        string url = HtmlParser.GetOutterPropertyFromHtml(accountHtml, "href");
        //        if (url.Contains("amp;"))
        //        {
        //            url = url.Replace("amp;", "");
        //        }
        //        string id = HtmlParser.GetInnerTextFromHtml(HtmlParser.GetOuterTextFromHtml("<span class=\"account-id\">", "</span>", 1, accountHtml));
        //        if (id.StartsWith("["))
        //        {
        //            id = id.Remove(0, 1);
        //        }

        //        if (id.EndsWith("]"))
        //        {
        //            id = id.Remove(id.Length - 1, 1);
        //        }

        //        GameAccountItem gameItem = new GameAccountItem() { Name = id, Detail = url };
        //        return gameItem;
        //    }

        //    return null;
        //}

        //protected override GameAccountState GetAccountState(string httpContent)
        //{
        //    if (string.IsNullOrEmpty(httpContent))
        //        return GameAccountState.LoginWithEmptyResponse;
        //    //网站维护
        //    else if (this.IsContains(httpContent, "maintenancelogo"))
        //        return GameAccountState.WebSiteMaintain;
        //    //OK
        //    else if (IsContains(httpContent, "<span class='status-active'>", "action=\"change-account.html\"", "Account Management", "Welcome"))
        //        return GetLoginStates(httpContent);
        //    //密码无效
        //    else if (IsContains(httpContent, "Password invalid", "Invalid account name", "The username or password is incorrect."))
        //        return GameAccountState.InvalidPassword;
        //    //锁号
        //    else if (IsContains(httpContent, "this account has been locked"))
        //        return GameAccountState.Locked;
        //    //网络出错
        //    else if (IsContains(httpContent, HttpHelperBase.HTTPERROR))
        //        return GameAccountState.HttpError;
        //    //网站出错
        //    else if (IsContains(httpContent, "An error has occurred."))
        //        return GameAccountState.WebSiteError;
        //    //暂封
        //    else if (IsContains(httpContent, "This account has been temporarily disabled", "<span class=\"brownText\">"))
        //        return GameAccountState.TempDisabled;
        //    //永封
        //    else if (IsContains(httpContent, "This account has been permanently disabled"))
        //        return GameAccountState.PermanentDisabled;
        //    //帐户不存在（空号）
        //    else if (IsContains(httpContent, "account does not have any World of Warcraft accounts associated with it"))
        //        return GameAccountState.IsNotExist;
        //    //登录太频繁，可能需要重启或换IP
        //    else if (IsContains(httpContent, "Too many attempt"))
        //        return GameAccountState.TooManyAttempt;
        //    //密保（受权码）
        //    else if (IsContains(httpContent, "Authenticator Code"))
        //        return GameAccountState.AuthenticatorCode;
        //    //验证码输入错误
        //    else if (IsContains(httpContent, "Wrong characters entered. Please try again"))
        //        return GameAccountState.IncorrectCaptcha;
        //    //验证码
        //    else if (IsContains(httpContent, "captcha.jpg"))
        //        return GameAccountState.NeedCaptcha;
        //    //登录名缺失
        //    else if (IsContains(httpContent, "Account name required"))
        //        return GameAccountState.MissAccount;
        //    //到期卦号
        //    else if (IsContains(httpContent, "Freeze"))
        //        return GameAccountState.TimeOut;
        //    //非战时间到
        //    else if (IsContains(httpContent, "#acctdetails", "Frozen"))
        //        return GameAccountState.OutOfBattleTime;
        //    else
        //        return GameAccountState.Unknown;
        //}

        //protected override GameAccountState GetLoginStates(string httpContent)
        //{
        //    //选择游戏帐号
        //    if (IsContains(httpContent, "Select a Game Account to Manage"))
        //        return GameAccountState.MultiGameAccount;
        //    //暂封
        //    else if (IsContains(httpContent, "This account has been temporarily disabled", "<span class=\"brownText\">"))
        //        return GameAccountState.TempDisabled;
        //    //永封
        //    else if (IsContains(httpContent, "This account has been permanently disabled"))
        //        return GameAccountState.PermanentDisabled;
        //    //测试账号过期
        //    else if (IsContains(httpContent, "Your trial has expired"))
        //        return GameAccountState.TestAccountOutOfExpire;
        //    //帐户不存在（空号）
        //    else if (IsContains(httpContent, "account does not have any World of Warcraft accounts associated with it"))
        //        return GameAccountState.IsNotExist;
        //    //正常登录，可以获取明细
        //    else if (IsContains(httpContent, "Welcome"))
        //        return GameAccountState.SingleGameAccount;
        //    //时间到
        //    else if (IsContains(httpContent, "Freeze"))
        //        return GameAccountState.TimeOut;
        //    //受权码
        //    else if (IsContains(httpContent, "Authenticator Code"))
        //        return GameAccountState.AuthenticatorCode;
        //    else
        //        return GameAccountState.SucceedUnknown;
        //}

        #endregion
    }
}
