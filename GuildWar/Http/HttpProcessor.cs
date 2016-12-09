using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using PublicUtilities;

namespace GuildWar
{
    public class HttpProcessor : HttpProcessorBase<FightAccountItem, FightHttperLogin>
    {
        public HttpProcessor(HttperManagerBase<FightAccountItem, FightHttperLogin> hm)
            : base(hm, FightLogManager.Instance)
        {
        }

        int processCount = 0;
        protected override void StartDetection()
        {
            while (this.isRunning)
            {
                FightAccountItem userItem = httperManager.GetNextItem();
                FightHttperLogin httpHelper = httperManager.GetHttper();
                this.cuttentHttpHelper = httpHelper;
                if ((null == userItem) || (null == httpHelper))
                {
                    if (httperManager.IsIPLimit)
                    {
                        this.RaiseProcessedItemChanged(userItem);
                        continue;
                    }
                    break;
                }
                this.stopwatch.Start();
                FightLogManager.Instance.Info(string.Format("-----------  start detecting new account,email={0},pwd={1}", userItem.EMail, userItem.Password));

                try
                {
                    using (httpHelper)
                    {
                        httpHelper.GetState(userItem);
                        this.RaiseProcessedItemChanged(userItem);
                        this.RaiseReconnectChanged(userItem);
                    }
                    this.stopwatch.Stop();
                    FightLogManager.Instance.Info(string.Format("***********  end detecting account,email={0},pwd={1}, state={2}, total spend:{3} s",
                                                  userItem.EMail, userItem.Password, userItem.State, this.stopwatch.Elapsed.ToString()));
                    //Thread.Sleep(500);
                }
                catch (System.Exception ex)
                {
                    FightLogManager.Instance.Error(string.Format("DectectionProcess.GetAccountState() failed:{0}", ex.Message));
                }
            }
            this.RaiseProcessCompleted();
        }
    }
}
