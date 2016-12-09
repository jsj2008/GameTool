using System;
using System.Reflection;

namespace PublicUtilities
{
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class CommentAttribute : Attribute
    {
        public string Comment
        {
            get;
            private set;
        }

        public CommentAttribute(string comment)
        {
            this.Comment = comment;
        }
    }

    public enum GameServerType
    {
        /// <summary>
        ///美服 us.battle.net
        /// </summary>
        [Comment("美服 us.battle.net")]
        USBattle,
        /// <summary>
        /// 英服 eu.battle.net
        /// </summary>
        [Comment("欧服 eu.battle.net")]
        EUBattle,
        /// <summary>
        ///东南亚 sea.battle.net
        /// </summary>
        [Comment("东南亚 sea.battle.net")]
        SEABattle,
        /// <summary>
        /// 台服tw.battle.net
        /// </summary>
        [Comment("台服 tw.battle.net")]
        TWBattle,
        /// <summary>
        /// 韩服kr.battle.net
        /// </summary>
        [Comment("韩服 kr.battle.net")]
        KRBattle,
        /// <summary>
        ///裂隙 www.riftgame.com
        /// </summary>
        [Comment("裂隙 riftgame.com")]
        USRift,
        /// <summary>
        ///江湖www.runescape.com
        /// </summary>
        [Comment("江湖 runescape.com")]
        RSServer,
        /// <summary>
        ///苹果www.apple.com
        /// </summary>
        [Comment("苹果 apple.com")]
        AppleServer,
        /// <summary>
        //XBOX www.xbox.com
        /// </summary>
        [Comment("XBOX xbox.com")]
        XBOXServer,
        /// <summary>
        //account.guildwars2.com
        /// </summary>
        [Comment("激战 guildwars2.com")]
        GuildWarsServer,
        //account.ArcheageServer.com
        /// </summary>
        [Comment("上古世纪 ArcheageServer.com")]
        ArcheageServer
    }

    /// <summary>
    /// 帐户登录状态
    /// </summary>
    public enum WowLoginStates
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Comment("未知状态")]
        Unknown,
        /// <summary>
        /// 登录成功,状态未知
        /// </summary>
        [Comment("登录成功,状态未知")]
        SucceedUnknown,
        /// <summary>
        /// 非战——登录成功,状态未知
        /// </summary>
        [Comment("非战——登录成功,状态未知")]
        UnbattleSucceedUnknown,
        /// <summary>
        /// 密码错误
        /// </summary>
        [Comment("密码错误")]
        InvalidPassword,
        /// <summary>
        /// 网站维护中
        /// </summary>
        [Comment("网站维护中")]
        WebSiteMaintain,
        /// <summary>
        /// 可点TBC
        /// </summary>
        [Comment("非战_TBC可点十天")]
        Unbattle_TCB,
        /// <summary>
        /// 非战: 可点WLK
        /// </summary>
        [Comment("非战_WLK可点十天")]
        Unbattle_WLK,
        /// <summary>
        /// 非战_WLK正式
        /// </summary>
        [Comment("非战_WLK正式")]
        Unbattle_WLK_Formal,
        /// <summary>
        /// 非战_正式
        /// </summary>
        [Comment("非战_正式")]
        Unbattle_OK,
        /// <summary>
        /// 需要输入验证码
        /// </summary>
        [Comment("需要输入验证码")]
        NeedCaptcha,
        /// <summary>
        /// 验证码输入错误
        /// </summary>
        [Comment("验证码输入错误")]
        IncorrectCaptcha,
        /// <summary>
        /// 登录太频繁，需要重连换IP
        /// </summary>
        [Comment("登录太频繁，需要重连换IP")]
        TooManyAttempt,
        /// <summary>
        /// 登录用户名缺失
        /// </summary>
        [Comment("登录用户名缺失")]
        MissAccount,
        /// <summary>
        /// 时间到期
        /// </summary>
        [Comment("时间到期")]
        TimeOut,
        /// <summary>
        /// 锁号
        /// </summary>
        [Comment("锁号")]
        Locked,
        /// <summary>
        /// 登录太多，帐号被锁
        /// </summary>
        [Comment("登录太多，帐号被锁")]
        TryTooMuchLocked,
        /// <summary>
        /// 网络超时，不可访问等错误
        /// </summary>
        [Comment("网络超时，不可访问等错误")]
        HttpError,
        /// <summary>
        /// 网站出错
        /// </summary>
        [Comment("网站出错，可能不能登录或者崩溃")]
        WebSiteError,
        /// <summary>
        /// 冷号
        /// </summary>
        [Comment("冷号")]
        Frozen,
        /// <summary>
        /// 登录时网站没有返回数据
        /// </summary>
        [Comment("登录时网站没有返回数据")]
        LoginWithEmptyResponse,
        /// <summary>
        /// 暂时卦号
        /// </summary>
        [Comment("暂时卦号")]
        TempDisabled,
        /// <summary>
        /// 永久卦号
        /// </summary>
        [Comment("永久卦号")]
        PermanentDisabled,
        /// <summary>
        /// 密保（受权码）
        /// </summary>
        [Comment("密保（受权码）")]
        AuthenticatorCode,
        /// <summary>
        /// 多个游戏帐号，一拖多
        /// </summary>
        [Comment("多个游戏帐号")]
        MultiGameAccount,
        /// <summary>
        /// 测试账号过期
        /// </summary>
        [Comment("测试账号过期")]
        TestAccountOutOfExpire,
        /// <summary>
        /// 试玩帐号
        /// </summary>
        [Comment("试玩帐号")]
        Trial,
        /// <summary>
        /// 帐号不存在（空号）
        /// </summary>
        [Comment("空号或非当前服务器帐号")]
        IsNotExist,
        /// <summary>
        /// 存在有效游戏帐号
        /// </summary>
        [Comment("存在有效游戏帐号")]
        LoginWithAccount,
        /// <summary>
        /// 单游戏帐号，已登录
        /// </summary>
        [Comment("单游戏帐号")]
        SingleGameAccount,
        /// <summary>
        /// 成功登录，但不存在游戏帐号
        /// </summary>
        [Comment("成功登录，但不存在游戏帐号")]
        LoginWithNoGameAccount,
        /// <summary>
        /// 不是当前登录战网游戏账号
        /// </summary>
        [Comment("不是当前登录战网游戏账号")]
        IsNotCurretServerAccount,
        /// <summary>
        /// 热号
        /// </summary>
        [Comment("热号")]
        Active,
        /// <summary>
        ///自动服务器切换失败
        /// </summary>
        [Comment("自动服务器切换失败")]
        AutoSwitchFailed,
        /// <summary>
        ///没有角色
        /// </summary>
        [Comment("没有角色")]
        NoCharacter,
        /// <summary>
        ///验证码识别太多
        /// </summary>
        [Comment("验证码识别太多")]
        TooMuchFailedChapcha,
        /// <summary>
        ///无效帐号
        /// </summary>
        [Comment("无效帐号")]
         InvalidAccount
    }

    /// <summary>
    /// Data format
    /// </summary>
    public enum DataFormat
    {
        [Comment("Account/Password/Mail")]
        AccountPasswordMail,
        [Comment("Account/Mail/Password")]
        AccountMailPassword,
        [Comment("Mail/Password")]
        MailPassword,
        [Comment("Account/Password")]
        AccountPassword,
        [Comment("Mail/FirstName/SecondName")]
        MailFstNSecN,
        [Comment("Mail/SecondName/FirstName")]
        MailSecNFstN
    }

    public enum PwdFormatOptions
    {
        [Comment("全部选项")]
        All,
        [Comment("首字母大小写反转")]
        FirstCharReversal,
        [Comment("密码结尾数字+1")]
        AddOneAfterEndWithNum,
        [Comment("密码结尾字母+a")]
        AddAAfterEndWithChar,
        [Comment("全数字+a")]
        AddAAllNum,
        [Comment("全字母+1")]
        AddOneAllChar,
        [Comment("原始密码+首字母大小写反转")]
        RawAddFirstCharReversal
    }
    /// <summary>
    /// The third GPU application type
    /// </summary>
    public enum GPUAppType
    {
        Ighashgpu,
        Egb
    }

    public enum DataType
    {
        Num,
        Char,
        Composit,
        Special
    }

    public enum RouterType
    {
        /// <summary>
        ///TL-R402系列 SOHO宽带路由器
        /// </summary>
        [Comment("TL-R402系列 SOHO宽带路由器")]
        TL_R402,
        /// <summary>
        /// DLink
        /// </summary>
        [Comment("D_Link")]
        D_Link
    }

    public enum ReconnectType
    {
        /// <summary>
        ///通过路由器重启
        /// </summary>
        [Comment("通过路由器重启")]
        Router,
        /// <summary>
        /// ADSL重拨
        /// </summary>
        [Comment("ADSL重拨")]
        ADSL,
        /// <summary>
        /// VPN重连
        /// </summary>
        [Comment("VPN重连")]
        VPN
    }

    /// <summary>
    /// 重新找回密码状态
    /// </summary>
    public enum PwdResetState
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Comment("未知状态")]
        Unknown,
        /// <summary>
        ///网络超时，不可访问等错误
        /// </summary>
        [Comment("网络超时，不可访问等错误")]
        HTTPERROR,
        /// <summary>
        ///网站错误
        /// </summary>
        [Comment("网站错误")]
        WebSiteERROR,
        /// <summary>
        /// Email或者名称不存在
        /// </summary>
        [Comment("Email或者名称不存在")]
        EmailOrNameNotExisted,
        /// <summary>
        /// 安全提示问题
        /// </summary>
        [Comment("安全提示问题")]
        SecretQuestion,
        /// <summary>
        ///需要验证码
        /// </summary>
        [Comment("需要验证码")]
        NeedCatcha,
    }

    public static class CommentAttributeGetter
    {
        public static string GetAttribute<T>(T value)
        {
            Type type = typeof(T);
            FieldInfo field = type.GetField(value.ToString());
            if (null != field)
            {
                object[] attributes = field.GetCustomAttributes(true);
                foreach (object obj in attributes)
                {
                    if (obj is CommentAttribute)
                    {
                        return (obj as CommentAttribute).Comment;
                    }
                }
            }
            return string.Empty;
        }
    }
}