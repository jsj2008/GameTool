using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PublicUtilities;

namespace RiftTool
{
    public class RiftExportBase : FileExportBase
    {
        protected const string Succeed = "密码正确账号";
        protected const string Failed = "登录失败账号";
        protected const string Unknown = "状态未知账号";
        protected const string EnterGame = "正式登入";

        public RiftExportBase(string filePath, bool isAppended)
            : base(filePath, isAppended,RiftLogManager.Instance)
        {
            this.CreateFileStream();
        }

        private void CreateFileStream()
        {
            if (string.IsNullOrEmpty(this.rawFilePath))
            {
                RiftLogManager.Instance.Error("Raw data file cann't be null");
                return;
            }
            if (this.streamWriterDic.Count == 0)
            {
                //正常可用
                this.CreateFileStream(this.rawFilePath, this.IsAppended, Succeed, Failed, Unknown, EnterGame);
            }
        }

        object output = new object();
        public virtual void Output(RiftAccountItem userItem)
        {
            lock (output)
            {
                if ((null != userItem))
                {
                    RiftDBHelper.InsertHistory(userItem);
                    RiftDBHelper.InsertQueriedItems(userItem);

                    StreamWriter sw = GetStreamWriter(userItem);
                    string content = userItem.ToString();
                    this.Output(content, sw);
                }
            }
        }


        protected StreamWriter GetStreamWriter(RiftAccountItem userItem)
        {
            switch (userItem.State)
            {
                case LoginState.LoginSucceed:
                case LoginState.RegistrationInvalid:
                    RiftOutputMgt.Instance.SucceedCount++;
                    return this.GetStream(Succeed);
                case LoginState.EnterGame:
                    RiftOutputMgt.Instance.SucceedCount++;
                    return this.GetStream(EnterGame);
                case LoginState.LoginFalied:
                    RiftOutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);

                case LoginState.Unknown:
                    RiftOutputMgt.Instance.UnknownCount++;
                    return this.GetStream(Unknown);

                default:
                    RiftOutputMgt.Instance.FailedCount++;
                    return this.GetStream(Failed);
            }
        }
    }
}
