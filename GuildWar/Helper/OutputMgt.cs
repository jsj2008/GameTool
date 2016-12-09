using System;
using PublicUtilities;

namespace GuildWar
{
    public class OutputMgt : IDisposable
    {
        public readonly static OutputMgt Instance = new OutputMgt();

        private ExportBase fileExporter = null;

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

        public void Output(FightAccountItem userItem, DetectionParamsItem paramsItem)
        {
            if ((null == userItem) || (null == paramsItem))
            {
                FightLogManager.Instance.ErrorWithCallback("FileExportManager.Output error, parameters has null");
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
            this.FailedCount = DBHelper.GetHistoryCount(LoginState.LoginFalied, LoginState.NetworkFailure);
            this.UnknownCount = DBHelper.GetHistoryCount(LoginState.Unknown);
            this.SucceedCount = DBHelper.GetHistoryCount(LoginState.LoginSucceed, LoginState.RegistrationInvalid);
        }

        private void CreateFileExporter(DetectionParamsItem paramsItem)
        {
            lock (this)
            {
                if (null == this.fileExporter)
                {
                    this.fileExporter = new ExportBase(paramsItem.DataFilePath, paramsItem.IsAppended);
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
            DBHelper.SaveCacheData();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Save();
        }

        #endregion
    }
}
