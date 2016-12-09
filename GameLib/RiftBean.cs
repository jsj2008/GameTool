using System;
using System.Collections.Generic;
using System.Text;

namespace PublicUtilities
{
    public enum LoginState
    {   /// <summary>
        /// 未知
        /// </summary>
        [Comment("未知状态")]
        Unknown,
        /// <summary>
        /// 密码正确
        /// </summary>
        [Comment("密码正确")]
        LoginSucceed,
        /// <summary>
        /// 密码正确
        /// </summary>
        [Comment("登录成功，邮箱已验证")]
        LoginSucceedAndVerified,
        /// <summary>
        /// 登录失败
        /// </summary>
        [Comment("登录失败")]
        LoginFalied,
        /// <summary>
        /// 登录太多要网络重联
        /// </summary>
        [Comment("登录太多要网络重联")]
        LoginTooMuch,
        /// <summary>
        /// 帐号不存在
        /// </summary>
        [Comment("帐号不存在")]
        NotExisted,
        /// <summary>
        /// 网络访问出错/拒绝
        /// </summary>
        [Comment("网络访问出错/拒绝")]
        NetworkFailure,
        /// <summary>
        /// 暂时访问限制
        /// </summary>
        [Comment("暂时访问限制")]
        TempBlock,
        /// <summary>
        /// 注册未完成
        /// </summary>
        [Comment("注册未完成")]
        RegistrationInvalid,
        /// <summary>
        /// 正式登入
        /// </summary>
        [Comment("正式登入")]
        EnterGame,
        /// <summary>
        /// 需要验证码
        /// </summary>
        [Comment("需要验证码")]
        Catpcha,
        /// <summary>
        /// 密码正确但要回答问题
        /// </summary>
        [Comment("密码正确但要回答问题")]
        AdditionalQuestion,
         /// <summary>
        /// 额外安全设置
        /// </summary>
        [Comment("额外安全设置")]
        AdditionalSecurity,
        /// <summary>
        /// 账户被锁定
        /// </summary>
        [Comment("账户被锁定")]
        Locked,
        /// <summary>
        ///  账户未激活
        /// </summary>
        [Comment(" 账户未激活")]
        Unactive,
        /// <summary>
        ///  未验证电子邮件地址
        /// </summary>
        [Comment("未验证电子邮件地址")]
        VerifyMail
    }

    /// <summary>
    /// 裂隙帐号
    /// </summary>
    public class RiftAccountItem : ItemBase
    {
        public new LoginState State { get; set; }

        public string AccountSummary { get; set; }

        public virtual string StateComment
        {
            get
            {
                return CommentAttributeGetter.GetAttribute<LoginState>(this.State);
            }
        }

        public override bool IsNeedRedial
        {
            get
            {
                return LoginState.LoginTooMuch == this.State
                    || LoginState.NetworkFailure == this.State;
            }
        }

        public virtual bool IsLoginSucceed
        {
            get
            {
                return LoginState.LoginSucceed == this.State ||
                       LoginState.EnterGame == this.State ||
                       LoginState.AdditionalQuestion == this.State;
            }
        }

        public virtual string AccountDetail
        {
            get
            {
                if (IsLoginSucceed)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("序号:{0}, EMAIL:{1}，密码:{2}，状态:{3}", Index, EMail, Password, StateComment);
                    if (!string.IsNullOrEmpty(AccountSummary))
                    {
                        sb.AppendFormat("，状态:{0}", AccountSummary);
                    }
                    return sb.ToString();
                }
                else
                {
                    return string.Format("序号:{0}, EMAIL:{1}，密码:{2}，状态:{3}", Index, EMail, Password, StateComment);
                }
            }
        }

        public override string ToString()
        {
            return AccountDetail;
        }
    }
}
