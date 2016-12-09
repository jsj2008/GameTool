using System.IO;
using PublicUtilities;

namespace Apple
{
    public class ExportBase : FileExportBase
    {
        protected const string Succeed = "密码正确账号";
        protected const string Failed = "登录失败账号";
        protected const string Unknown = "状态未知账号";
        protected const string InvalidRegister = "注册不完全";

        public ExportBase(string filePath, bool isAppended)
            : base(filePath, isAppended, AppleLogManager.Instance)
        {
            this.CreateFileStream();
        }

        private void CreateFileStream()
        {
            if (string.IsNullOrEmpty(this.rawFilePath))
            {
                AppleLogManager.Instance.Error("Raw data file cann't be null");
                return;
            }
            if (this.streamWriterDic.Count == 0)
            {
                //正常可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, Succeed);
                //不完全
                this.CreateFileStream(this.rawFilePath, this.IsAppended, InvalidRegister);
                //不正常
                this.CreateFileStream(this.rawFilePath, this.IsAppended, Failed, Unknown, InvalidRegister);
            }
        }

        object output = new object();
        public virtual void Output(AccountItem userItem)
        {
            lock (output)
            {
                if ((null != userItem))
                {
                    DBHelper.InsertHistory(userItem);
                    DBHelper.InsertQueriedItems(userItem);

                    StreamWriter sw = GetStreamWriter(userItem);
                    string content = userItem.ToString();
                    this.Output(content, sw);
                }
            }
        }


        protected StreamWriter GetStreamWriter(AccountItem userItem)
        {
            switch (userItem.State)
            {
                case LoginState.LoginSucceed:
                case LoginState.RegistrationInvalid:
                case LoginState.EnterGame:
                case LoginState.LoginSucceedAndVerified:
                    OutputMgt.Instance.SucceedCount++;
                    return this.GetStream(Succeed);

                case LoginState.Locked:
                case LoginState.Unactive:
                case LoginState.VerifyMail:
                    OutputMgt.Instance.SucceedCount++;
                    return this.GetStream(InvalidRegister);

                case LoginState.LoginFalied:
                    OutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);

                case LoginState.Unknown:
                    OutputMgt.Instance.UnknownCount++;
                    return this.GetStream(Unknown);

                default:
                    OutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);
            }
        }
    }
}
