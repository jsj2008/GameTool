using System;
using System.Collections.Generic;
using System.Text;

namespace PublicUtilities
{
    public enum GameTypes
    {
        /// <summary>
        /// 魔兽
        /// </summary>
        Wow,
        /// <summary>
        ///苹果
        /// </summary>
        Apple,
        /// <summary>
        ///微软 - Xbox
        /// </summary>
        Xbox,
        /// <summary>
        /// 裂隙
        /// </summary>
        Rift,
        /// <summary>
        ///激战
        /// </summary>
        GuildWar,
        /// <summary>
        ///江湖
        /// </summary>
        RS,
        /// <summary>
        ///上古世纪
        /// </summary>
        ArcheageServer
    }

    public class ItemBase
    {
        public int Index { get; set; }
        public string EMail { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string RawData { get; set; }

        public WowLoginStates State { get; set; }

        /// <summary>
        /// 网络出错 /验证码输入开始重启网络
        /// </summary>
        public virtual bool IsNeedRedial
        {
            get
            {
                return false;
            }
        }
    }


    /// <summary>
    /// 登录账号
    /// </summary>
    public class UserAccountItem : ItemBase
    {
        /// <summary>
        /// State is TooManyAttempt , can reconnect
        /// </summary>
        public override bool IsNeedRedial
        {
            get
            {
                return WowLoginStates.TooManyAttempt == this.State ||
                       WowLoginStates.TooMuchFailedChapcha == this.State ||
                       IsErrored;
            }
        }


        /// <summary>
        /// Is auto switch game server
        /// </summary>
        public bool IsAutoSwitchServer
        {
            get
            {
                return
                         (WowLoginStates.InvalidPassword == this.State) ||
                         (WowLoginStates.IsNotCurretServerAccount == this.State) ||
                         (WowLoginStates.IsNotExist == this.State) ||
                         (WowLoginStates.WebSiteMaintain == this.State) ||
                         (WowLoginStates.Unknown == this.State) ||
                         IsErrored;
            }
        }
        /// <summary>
        /// Is Catpcha error or not get
        /// </summary>
        public bool IsCaptchaError
        {
            get
            {
                return (WowLoginStates.IncorrectCaptcha == this.State) ||
                       (WowLoginStates.NeedCaptcha == this.State) ||
                       (WowLoginStates.TooMuchFailedChapcha == this.State);
            }
        }

        /// <summary>
        /// State is IncorrectCaptcha/ HttpError/ website error
        /// </summary>
        public bool IsErrored
        {
            get
            {
                return IsCaptchaError ||
                    (WowLoginStates.HttpError == this.State) ||
                    (WowLoginStates.WebSiteError == this.State);
            }
        }

        public bool IsCanGetDetail
        {
            get
            {
                return (WowLoginStates.SingleGameAccount == this.State) ||
                        (WowLoginStates.MultiGameAccount == this.State) ||
                        (WowLoginStates.Trial == this.State) ||
                         (WowLoginStates.LoginWithAccount == this.State) ||
                         (WowLoginStates.Active == this.State);

            }
        }

        public bool IsCanGetUnbattleCharacter
        {
            get
            {
                return (WowLoginStates.Unbattle_OK == this.State);
                //||  (LoginState.Unbattle_WLK == this.State);
            }
        }

        /// <summary>
        /// Get the comment attribute from GameAccountState
        /// </summary>
        public string StateComment
        {
            get
            {
                return CommentAttributeGetter.GetAttribute<WowLoginStates>(this.State);
            }
        }

        private IList<GameAccountItem> items = new List<GameAccountItem>();

        public IList<GameAccountItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public string SignInServer
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string AccountStateString
        {
            get
            {
                string stateString = this.StateComment;
                if ((this.State == WowLoginStates.MultiGameAccount) && (this.Items.Count > 0))
                {
                    stateString = string.Format(": {0}个子帐号；", this.Items.Count);
                }

                if (this.Items.Count > 0)
                {
                    stateString += "　{";
                    string subStateString = string.Empty;
                    foreach (GameAccountItem item in this.Items)
                    {
                        subStateString += item.GetAccountState();
                        if (!string.IsNullOrEmpty(subStateString))
                        {
                            subStateString += "|";
                        }
                    }

                    if (!string.IsNullOrEmpty(subStateString) && (subStateString.LastIndexOf("|") == (subStateString.Length - 1)))
                    {
                        subStateString = subStateString.Remove(subStateString.Length - 1);
                    }

                    if (string.IsNullOrEmpty(subStateString))
                    {
                        subStateString = "正常";
                    }
                    stateString += string.Format("{0} {1}", subStateString, "}");
                }

                return stateString;
            }
        }

        public string UserDetail
        {
            get
            {
                string detail = string.Empty;
                if (string.IsNullOrEmpty(this.User) || (this.User == this.EMail))
                {
                    detail = string.Format("序号：{0}, 邮箱：{1}, 密码：{2}, 状态：{3}", this.Index, this.EMail, this.Password, this.AccountStateString);
                }
                else
                {
                    detail = string.Format("序号：{0},用户名：{1}， 邮箱：{2}, 密码：{3}, 状态：{4} ", this.Index, this.User, this.EMail, this.Password, this.AccountStateString, SignInServer);
                }
                if (!string.IsNullOrEmpty(SignInServer))
                {
                    detail = string.Format("{0}, 服务器：{1}", detail, SignInServer);
                }
                return detail;
            }
        }

        public string ToEmailPasswordString()
        {
            return string.Format("{0}  {1}  {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), this.EMail, this.Password);
        }

        public string SubAccountDetailString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (GameAccountItem item in this.Items)
                {
                    sb.AppendLine(item.ToString());
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0}  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), this.UserDetail));
            sb.AppendLine(SubAccountDetailString);
            return sb.ToString();
        }
    }

    /// <summary>
    /// 具体游戏帐号
    /// </summary>
    public class GameAccountItem
    {
        //Eu server need,get
        public string DetailUrl
        { get; set; }

        public string Name
        { get; set; }

        public string Edition
        { get; set; }

        public string Region
        { get; set; }

        public string Detail
        { get; set; }

        public override string ToString()
        {
            string s = string.Format("\t游戏帐号:{0}; 状态:{1};", Name, GetAccountState());
            if (!string.IsNullOrEmpty(Detail))
            {
                s = string.Format("{0}; 明细:{1};", s, Detail);
            }
            return s;
        }

        public string GetAccountState()
        {
            string str = "正常";
            if (string.IsNullOrEmpty(this.Detail))
            {
                if (TextHelper.IsContains(this.Edition, "Trial", "體驗帳號", "試玩", "已到期", "測試伺服器", "public test realm"))
                {
                    str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Trial));
                }
                else if (TextHelper.IsContains(this.Edition, "Locked", "Banned", "Cerrada", "Account Banned", "暂时卦号"))
                {
                    str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.TempDisabled));
                }
                else if (TextHelper.IsContains(this.Edition, "EINGEFROREN", "FROZEN", "已凍結"))
                {
                    str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Frozen));
                }
                else if (TextHelper.IsContains(this.Edition, "Active", "已取消", "啟動"))
                {
                    str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Active));
                }

                if (TextHelper.IsContains(this.Region, "Americas", "Oceania (US)", "(US)", "美國"))
                {
                    str += "(US)";
                }
                else if (TextHelper.IsContains(this.Region, "Europe (EU)", "Europe", "(EU)"))
                {
                    str += "(EU)";
                }
                else if (TextHelper.IsContains(this.Region, "Россия и СНГ (RU)", "(RU)"))
                {
                    str += "(RU)";
                }
                return str;
            }

            if (TextHelper.IsContains(this.Detail, "(  Trial  )", "（體驗帳號）", "(試玩)", "（已到期）",
                "測試伺服器", "public test realm", "新手入門版"))
            {
                str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Trial));
            }
            else if (TextHelper.IsContains(this.Detail, "Locked", "Banned", "Cerrada", "Account Banned", "暂时卦号"))
            {
                str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.TempDisabled));
            }
            else if (TextHelper.IsContains(this.Detail, "EINGEFROREN", "FROZEN", "已凍結"))
            {
                str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Frozen));
            }
            else if (TextHelper.IsContains(this.Detail, "Active", "已取消", "啟動"))
            {
                str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.Active));
            }
            else if (TextHelper.IsContains(this.Detail, "立即購買《暗黑破壞神"))
            {
                str = string.Format(" {0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.InvalidAccount));
            }

            if (TextHelper.IsContains(this.Detail, "Americas", "Oceania (US)", "(US)", "美國"))
            {
                str += "(US)";
            }
            else if (TextHelper.IsContains(this.Detail, "Europe (EU)", "Europe", "(EU)"))
            {
                str += "(EU)";
            }
            else if (TextHelper.IsContains(this.Detail, "Россия и СНГ (RU)", "(RU)"))
            {
                str += "(RU)";
            }

            return str;
        }
    }

    /// <summary>
    /// 破宝
    /// </summary>
    public class PwdResetItem : ItemBase
    {
        public string FirstName
        { get; set; }

        public string SecondName
        { get; set; }

        /// <summary>
        ///  安全设置答案
        /// </summary>
        public string SecutiryAnswer
        { get; set; }

        public new PwdResetState State
        { get; set; }

        public bool IsValidName
        {
            get { return this.State == PwdResetState.SecretQuestion; }
        }

        public bool IsNeedCaptcha
        {
            get { return this.State == PwdResetState.NeedCatcha; }
        }
    }
}