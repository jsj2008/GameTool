using System.IO;
using System.Threading;
using System;

namespace PublicUtilities
{
    /// <summary>
    /// 非/战网数据输出
    /// </summary>
    public class BattleOutptMgt : IDisposable
    {
        public readonly static BattleOutptMgt Instance = new BattleOutptMgt();
        protected LogManagerBase LogManager = WowLogManager.Instance;

        private BattleExportBase fileExporter = null;

        public int SucceedCount { get; set; }
        public int RetryCount { get; set; }
        public int FailedCount { get; set; }
        public int UselessCount { get; set; }
        public int LevelDetailCount { get; set; }
       
        public int ProcessedCount
        {
            get
            {
                return SucceedCount + RetryCount + FailedCount + UselessCount;
            }
        }

        public void Output(UserAccountItem userItem, DetectionParamsItem paramsItem)
        {
            if ((null == userItem) || (null == paramsItem))
            {
                LogManager.ErrorWithCallback("FileExportManager.Output error, parameters has null");
                return;
            }

            this.CreateFileExporter(paramsItem);
            this.fileExporter.Output(userItem);
        }

        public void OuputCharacter(string content, CharacterType charType, DetectionParamsItem paramsItem)
        {
            lock (this)
            {
                if (string.IsNullOrEmpty(content)) return;
                this.CreateFileExporter(paramsItem);
                fileExporter.OutputChars(charType, content);
            }
        }

        public void Save()
        {
            this.DisposeLastFileExporter();
        }

        public void LoadLastStopCount()
        {
            this.FailedCount = BattleDBHelper.GetHistoryCount(WowLoginStates.Unknown,
                                                         WowLoginStates.InvalidPassword,
                                                         WowLoginStates.NeedCaptcha,
                                                         WowLoginStates.MissAccount,
                                                         WowLoginStates.IsNotExist,
                                                         WowLoginStates.IsNotCurretServerAccount);

            this.RetryCount = BattleDBHelper.GetHistoryCount(WowLoginStates.IncorrectCaptcha,
                                                         WowLoginStates.TooManyAttempt);

            this.SucceedCount = BattleDBHelper.GetHistoryCount(WowLoginStates.SingleGameAccount,
                                                         WowLoginStates.MultiGameAccount,
                                                         WowLoginStates.Unbattle_TCB,
                                                         WowLoginStates.Unbattle_WLK,
                                                         WowLoginStates.Unbattle_OK,
                                                         WowLoginStates.Trial,
                                                         WowLoginStates.LoginWithNoGameAccount);

            this.UselessCount = BattleDBHelper.GetHistoryCount(WowLoginStates.HttpError,
                                                         WowLoginStates.SucceedUnknown,
                                                         WowLoginStates.WebSiteError,
                                                         WowLoginStates.TimeOut,
                                                         WowLoginStates.Locked,
                                                         WowLoginStates.LoginWithEmptyResponse,
                                                         WowLoginStates.TempDisabled,
                                                         WowLoginStates.TestAccountOutOfExpire,
                                                         WowLoginStates.PermanentDisabled,
                                                         WowLoginStates.AuthenticatorCode,
                                                         WowLoginStates.Frozen);
        }

        private void CreateFileExporter(DetectionParamsItem paramsItem)
        {
            lock (this)
            {
                //this.DisposeLastFileExporter();
                if (null == this.fileExporter)
                {
                    this.fileExporter = new BattleExportBase(paramsItem.DataFilePath, paramsItem.IsAppended);
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
            BattleDBHelper.SaveCacheData();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Save();
        }

        #endregion
    }

    /// <summary>
    /// 角色类型
    /// </summary>
    public enum CharacterType
    {
        /// <summary>
        /// 非战
        /// </summary>
        Unbattle,
        /// <summary>
        /// 战网
        /// </summary>
        Battle
    }
}