using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;
using System.Threading;

namespace PwdReset
{
    public class PwdResetHttpProcessor : HttpProcessorBase<PwdResetItem, PwdResetHttperBase>
    {
        public PwdResetHttpProcessor(HttperManagerBase<PwdResetItem, PwdResetHttperBase> hm)
            : base(hm,WowLogManager.Instance)
        {
        }

        protected override void StartDetection()
        {
            while (this.isRunning)
            {
                PwdResetItem userItem = httperManager.GetNextItem();
                PwdResetHttperBase httpHelper = httperManager.GetHttper();
                this.cuttentHttpHelper = httpHelper;
                if ((null == userItem) || (null == httpHelper))
                {
                    break;
                }
                this.stopwatch.Start();
                LogManager.Info(string.Format("-----------  start passwrod reset new account,email={0}", userItem.EMail));

                try
                {
                    using (httpHelper)
                    {
                        httpHelper.GetItemState(userItem);
                        //if (!userItem.IsValidName)
                        //{
                        //    LogManager.Info(string.Format("----------- account error so start taking more try,email={0}, state={1}",
                        //                            userItem.EMail,  userItem.State));
                        //}

                        this.RaiseProcessedItemChanged(userItem);
                        this.RaiseReconnectChanged(userItem);
                    }
                    this.stopwatch.Stop();
                    LogManager.Info(string.Format("***********  end passwrod reset ,email={0}, state={1}, total spend:{2} s",
                                                  userItem.EMail, userItem.State, this.stopwatch.Elapsed.ToString()));
                    Thread.Sleep(500);
                }
                catch (System.Exception ex)
                {
                    LogManager.Error(string.Format("httpHelper.GetAccountState() failed:{0}", ex.Message));
                }
            }

            this.RaiseProcessCompleted();
        }
    }
}
