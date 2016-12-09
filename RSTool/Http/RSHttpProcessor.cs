using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using PublicUtilities;

namespace RSTool
{
    public class RSHttpProcessor : HttpProcessorBase<RSAccountItem, RSHttperLoginBase>
    {
        public RSHttpProcessor(HttperManagerBase<RSAccountItem, RSHttperLoginBase> hm)
            : base(hm,RSLogManager.Instance)
        {
        }

        protected override void StartDetection()
        {
            while (this.isRunning)
            {
                RSAccountItem userItem = httperManager.GetNextItem();
                RSHttperLoginBase httpHelper = httperManager.GetHttper();
                this.cuttentHttpHelper = httpHelper;
                if ((null == userItem) || (null == httpHelper))
                {
                    break;
                }
                this.stopwatch.Start();
                RSLogManager.Instance.Info(string.Format("-----------  start detecting new account,email={0},pwd={1}", userItem.EMail, userItem.Password));

                try
                {
                    using (httpHelper)
                    {
                        httpHelper.GetState(userItem);
                        this.RaiseProcessedItemChanged(userItem);
                        this.RaiseReconnectChanged(userItem);
                    }
                    this.stopwatch.Stop();
                    RSLogManager.Instance.Info(string.Format("***********  end detecting account,email={0},pwd={1}, state={2}, total spend:{3} s",
                                                  userItem.EMail, userItem.Password, userItem.State, this.stopwatch.Elapsed.ToString()));
                    //Thread.Sleep(500);
                }
                catch (System.Exception ex)
                {
                    RSLogManager.Instance.Error(string.Format("DectectionProcess.GetAccountState() failed:{0}", ex.Message));
                }
            }
            this.RaiseProcessCompleted();
        }
    }
}
