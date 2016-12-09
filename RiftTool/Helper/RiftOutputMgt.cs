using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RiftTool
{
    public class RiftOutputMgt : IDisposable
    {
        public readonly static RiftOutputMgt Instance = new RiftOutputMgt();

        private RiftExportBase fileExporter = null;

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

        public void Output(RiftAccountItem userItem, DetectionParamsItem paramsItem)
        {
            if ((null == userItem) || (null == paramsItem))
            {
                RiftLogManager.Instance.ErrorWithCallback("FileExportManager.Output error, parameters has null");
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
            this.FailedCount = RiftDBHelper.GetHistoryCount(LoginState.LoginFalied,LoginState.NetworkFailure);
            this.UnknownCount = RiftDBHelper.GetHistoryCount(LoginState.Unknown);
            this.SucceedCount = RiftDBHelper.GetHistoryCount(LoginState.LoginSucceed,LoginState.RegistrationInvalid);
        }

        private void CreateFileExporter(DetectionParamsItem paramsItem)
        {
            lock (this)
            {
                if (null == this.fileExporter)
                {
                    this.fileExporter = new RiftExportBase(paramsItem.DataFilePath, paramsItem.IsAppended);
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
            RiftDBHelper.SaveCacheData();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Save();
        }

        #endregion
    }
}
