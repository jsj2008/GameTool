using System;
using PublicUtilities;

namespace RSTool
{
    public class RSOutputMgt : IDisposable
    {
        public readonly static RSOutputMgt Instance = new RSOutputMgt();

        private RSExportBase fileExporter = null;

        public int SucceedCount { get; set; }
        public int FailedCount { get; set; }
        public int UnknownCount { get; set; }
        public int ProcessedCount
        {
            get
            {
                return SucceedCount + UnknownCount + FailedCount;
            }
        }

        public void Output(RSAccountItem userItem, DetectionParamsItem paramsItem)
        {
            if ((null == userItem) || (null == paramsItem))
            {
                RSLogManager.Instance.ErrorWithCallback("FileExportManager.Output error, parameters has null");
                return;
            }

            this.CreateFileExporter(paramsItem);
            this.fileExporter.Output(userItem);
        }

        public void Save()
        {
            this.DisposeLastFileExporter();
        }

        public void LoadLastStopCount()
        {
            this.FailedCount = RSDBHelper.GetHistoryCount(LoginState.LoginFalied, LoginState.NetworkFailure);
            this.UnknownCount = RSDBHelper.GetHistoryCount(LoginState.Unknown);
            this.SucceedCount = RSDBHelper.GetHistoryCount(LoginState.LoginSucceed, LoginState.RegistrationInvalid);
        }

        private void CreateFileExporter(DetectionParamsItem paramsItem)
        {
            lock (this)
            {
                if (null == this.fileExporter)
                {
                    this.fileExporter = new RSExportBase(paramsItem.DataFilePath, paramsItem.IsAppended);
                    this.LoadLastStopCount();
                }
            }
        }

        private void DisposeLastFileExporter()
        {
            if (null != this.fileExporter)
            {
                this.fileExporter.Dispose();
                this.fileExporter = null;
            }
            RSDBHelper.SaveCacheData();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Save();
        }

        #endregion
    }
}
