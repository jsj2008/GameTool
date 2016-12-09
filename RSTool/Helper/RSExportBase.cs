using System.IO;
using PublicUtilities;

namespace RSTool
{
    public class RSExportBase : FileExportBase
    {
        protected const string Succeed = "密码正确账号";
        protected const string Failed = "登录失败账号";
        protected const string Unknown = "状态未知账号";
        protected const string EnterGame = "正式登入";

        public RSExportBase(string filePath, bool isAppended)
            : base(filePath, isAppended,RSLogManager.Instance)
        {
            this.CreateFileStream();
        }

        private void CreateFileStream()
        {
            if (string.IsNullOrEmpty(this.rawFilePath))
            {
                RSLogManager.Instance.Error("Raw data file cann't be null");
                return;
            }
            if (this.streamWriterDic.Count == 0)
            {
                //正常可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, Succeed, Failed, Unknown, EnterGame);
            }
        }

        object output = new object();
        public virtual void Output(RSAccountItem userItem)
        {
            lock (output)
            {
                if ((null != userItem))
                {
                    RSDBHelper.InsertHistory(userItem);
                    RSDBHelper.InsertQueriedItems(userItem);

                    StreamWriter sw = GetStreamWriter(userItem);
                    string content = userItem.ToString();
                    this.Output(content, sw);
                }
            }
        }


        protected StreamWriter GetStreamWriter(RSAccountItem userItem)
        {
            switch (userItem.State)
            {
                case LoginState.LoginSucceed:
                case LoginState.RegistrationInvalid:
                    RSOutputMgt.Instance.SucceedCount++;
                    return this.GetStream(Succeed);
                case LoginState.EnterGame:
                    RSOutputMgt.Instance.SucceedCount++;
                    return this.GetStream(EnterGame);
                case LoginState.LoginFalied:
                    RSOutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);

                case LoginState.Unknown:
                    RSOutputMgt.Instance.UnknownCount++;
                    return this.GetStream(Unknown);

                default:
                    RSOutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);
            }
        }
    }
}
