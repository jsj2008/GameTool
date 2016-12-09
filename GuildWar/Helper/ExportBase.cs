using System.IO;
using PublicUtilities;

namespace GuildWar
{
    public class ExportBase : FileExportBase
    {
        protected const string Succeed = "密码正确账号";
        protected const string Failed = "登录失败账号";
        protected const string Unknown = "状态未知账号";
        protected const string EnterGame = "正式登入";

        public ExportBase(string filePath, bool isAppended)
            : base(filePath, isAppended,FightLogManager.Instance)
        {
            this.CreateFileStream();
        }

        private void CreateFileStream()
        {
            if (string.IsNullOrEmpty(this.rawFilePath))
            {
                FightLogManager.Instance.Error("Raw data file cann't be null");
                return;
            }
            if (this.streamWriterDic.Count == 0)
            {
                //正常可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, Succeed, Failed, Unknown, EnterGame);
            }
        }

        object output = new object();
        public virtual void Output(FightAccountItem userItem)
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


        protected StreamWriter GetStreamWriter(FightAccountItem userItem)
        {
            switch (userItem.State)
            {
                case LoginState.LoginSucceed:
                case LoginState.RegistrationInvalid:
                    OutputMgt.Instance.SucceedCount++;
                    return this.GetStream(Succeed);
                case LoginState.EnterGame:
                    OutputMgt.Instance.SucceedCount++;
                    return this.GetStream(EnterGame);
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
