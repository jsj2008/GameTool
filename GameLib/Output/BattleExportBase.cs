using System;
using System.Collections.Generic;
using System.IO;

namespace PublicUtilities
{
    public class BattleExportBase : FileExportBase
    {
        protected const string SUCCEED = "有价值账号";
        protected const string SUCCEED_US = "有价值账号_US";
        protected const string SUCCEED_EU = "有价值账号_EU";
        protected const string RETRY = "查询出错";
        protected const string FAILED = "无法登录";
        protected const string USELESS = "账户不可用";
        protected const string REUSEABLE = "可重用数据";
        protected const string LoginWithNoAccount = "成功登录无游戏帐号";
        protected const string IsNotCurrentServerAccount = "非当前服务器帐号";
        protected const string TestOrExpire = "试完或测试到期";

        protected const string QueryChar = "非战可查角色帐号";
        protected const string QueryUnbattleChar = "非战角色明细";
        protected const string QueryBattleChar = "战网角色明细";

        protected const string InvalidCaptcha = "验证码出错";

        public BattleExportBase(string filePath, bool isAppended)
            : base(filePath, isAppended, WowLogManager.Instance)
        {
            this.CreateFileStream();
        }

        private void CreateFileStream()
        {
            if (string.IsNullOrEmpty(this.rawFilePath))
            {
                LogManager.Error("Raw data file cann't be null");
                return;
            }
            if (this.streamWriterDic.Count == 0)
            {
                //正常可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, SUCCEED, SUCCEED_US, SUCCEED_EU);
                //可以登录，但暂时不可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, USELESS);
                //访问频繁，拒绝登录，需重启路由
                this.CreateFileStream(this.rawFilePath, this.IsAppended, RETRY);
                //密码出错等不正常登录
                this.CreateFileStream(this.rawFilePath, this.IsAppended, FAILED);
                //验证码录入错误重用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, REUSEABLE);
                //成功登录不存在游戏帐号
                this.CreateFileStream(this.rawFilePath, this.IsAppended, LoginWithNoAccount, IsNotCurrentServerAccount, TestOrExpire);
                //非战，战网角色
                this.CreateFileStream(this.rawFilePath, this.IsAppended, QueryChar, QueryUnbattleChar, QueryBattleChar);

                this.CreateFileStream(this.rawFilePath, this.IsAppended, InvalidCaptcha);
            }
        }

        object output = new object();
        public virtual void Output(UserAccountItem userItem)
        {
            lock (output)
            {
                if ((null != userItem))
                {
                    BattleDBHelper.InsertHistory(userItem);
                    BattleDBHelper.InsertQueriedItems(userItem);

                    StreamWriter sw = GetStreamWriter(userItem);
                    string content = userItem.ToString();
                    this.Output(content, sw);

                    this.WriteRawData(userItem);
                    this.OutputByRegion(userItem);
                }
            }
        }

        object region = new object();
        protected void OutputByRegion(UserAccountItem userItem)
        {
            lock (region)
            {
                string s = userItem.ToString();
                if ((null != userItem))
                {
                    StreamWriter sw = null;
                    if (TextHelper.IsContains(s, "(US)"))
                    {
                        sw = this.GetStream(SUCCEED_US);
                    }
                    else if (TextHelper.IsContains(s, "(EU)"))
                    {
                        sw = this.GetStream(SUCCEED_EU);
                    }

                    if (sw == null)
                    {
                        LogManager.Error("Account output US/EU account failed,that's other country account!");
                        return;
                    }

                    this.Output(s, sw);
                }
            }
        }

        protected void WriteRawData(UserAccountItem userItem)
        {
            StreamWriter sw = null;
            switch (userItem.State)
            {
                case WowLoginStates.HttpError:
                case WowLoginStates.WebSiteError:
                case WowLoginStates.TimeOut:
                case WowLoginStates.IncorrectCaptcha:
                case WowLoginStates.TooManyAttempt:
                    sw = this.GetStream(REUSEABLE);
                    break;
                case WowLoginStates.LoginWithNoGameAccount:
                    sw = this.GetStream(LoginWithNoAccount);
                    break;
                case WowLoginStates.IsNotCurretServerAccount:
                    sw = this.GetStream(IsNotCurrentServerAccount);
                    break;
                case WowLoginStates.Unbattle_OK:
                    //case LoginState.Unbattle_WLK:
                    sw = this.GetStream(QueryChar);
                    break;
                default:
                    break;
            }

            if ((null != sw) && !string.IsNullOrEmpty(userItem.RawData))
            {
                this.Output(userItem.RawData, sw);
            }
        }

        public void OutputChars(CharacterType charType, string content)
        {
            StreamWriter sw = null;
            switch (charType)
            {
                case CharacterType.Unbattle:
                    sw = this.GetStream(QueryUnbattleChar); break;
                case CharacterType.Battle:
                    sw = this.GetStream(QueryBattleChar); break;
                default: break;
            }

            if (sw == null)
            {
                LogManager.Error("Account output characters failed!");
                return;
            }

            if ((null != sw) && !string.IsNullOrEmpty(content))
            {
                this.Output(content, sw);
            }
        }

        protected StreamWriter GetStreamWriter(UserAccountItem userItem)
        {
            switch (userItem.State)
            {
                case WowLoginStates.SingleGameAccount:
                case WowLoginStates.MultiGameAccount:
                case WowLoginStates.Unbattle_TCB:
                case WowLoginStates.Unbattle_WLK:
                case WowLoginStates.Unbattle_OK:
                    BattleOutptMgt.Instance.SucceedCount++;
                    return this.GetStream(SUCCEED);

                case WowLoginStates.NeedCaptcha:
                case WowLoginStates.IncorrectCaptcha:
                case WowLoginStates.TooMuchFailedChapcha:
                    BattleOutptMgt.Instance.RetryCount++;
                    return this.GetStream(InvalidCaptcha);

                case WowLoginStates.HttpError:
                case WowLoginStates.SucceedUnknown:
                case WowLoginStates.WebSiteError:
                case WowLoginStates.TimeOut:
                case WowLoginStates.Locked:
                case WowLoginStates.LoginWithEmptyResponse:
                case WowLoginStates.TempDisabled:
                case WowLoginStates.PermanentDisabled:
                case WowLoginStates.AuthenticatorCode:
                case WowLoginStates.Frozen:
                    BattleOutptMgt.Instance.UselessCount++;
                    return this.GetStream(USELESS);

                case WowLoginStates.TestAccountOutOfExpire:
                    BattleOutptMgt.Instance.UselessCount++;
                    return this.GetStream(TestOrExpire);

                case WowLoginStates.Trial:
                    BattleOutptMgt.Instance.SucceedCount++;
                    return this.GetStream(TestOrExpire);

                case WowLoginStates.Unknown:
                case WowLoginStates.InvalidPassword:

                case WowLoginStates.MissAccount:
                case WowLoginStates.IsNotExist:
                case WowLoginStates.LoginWithNoGameAccount:
                    BattleOutptMgt.Instance.FailedCount++;
                    return this.GetStream(FAILED);

                case WowLoginStates.TooManyAttempt:
                    BattleOutptMgt.Instance.RetryCount++;
                    return this.GetStream(RETRY);

                default:
                    BattleOutptMgt.Instance.FailedCount++;
                    return this.GetStream(FAILED);
            }
        }
    }
}