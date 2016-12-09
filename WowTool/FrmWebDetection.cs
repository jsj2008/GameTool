using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;
using System.IO;

namespace WebDetection
{
    public partial class FrmWebDetection : Form
    {
        private IList<GameServer> gameServerList = new List<GameServer>();
        private WowHttperManager httpManager = null;

        public FrmWebDetection()
        {
            InitializeComponent();
            this.Load += FrmWebDetection_Load;
            this.FormClosing += FrmMain_FormClosing;

            WowLogManager.Instance.LogEvent += LogManager_LogEvent;
        }

        UcAdsl ucAdsl = null;
        UcRouter ucRouter = null;
        UcVpnList ucVpnList = null;
        //UcVpnItem ucVpn = null;

        private void LoadUcNet()
        {
            ADSLItem adsl = new ADSLItem();
            adsl.EntryName = WowSetConfig.Instance.ADSLName;
            adsl.Password = WowSetConfig.Instance.ADSLPwd;
            adsl.User = WowSetConfig.Instance.ADSLUser;
            ucAdsl = new UcAdsl(adsl, WowLogManager.Instance);
            ucAdsl.TestChanged += ucNetwork_TestChanged;

            RouterItem router = new RouterItem();
            router.RouterType = WowSetConfig.Instance.RouterType;
            router.IP = WowSetConfig.Instance.RouterIP;
            router.Password = WowSetConfig.Instance.RouterPwd;
            router.User = WowSetConfig.Instance.RouterUser;
            ucRouter = new UcRouter(router, WowLogManager.Instance);
            ucRouter.TestChanged += ucNetwork_TestChanged;

            VPNFile vpn = new VPNFile();
            vpn.EntryName = WowSetConfig.Instance.VpnEntryName;
            vpn.File = WowSetConfig.Instance.VpnFile;
            ucVpnList = new UcVpnList(vpn, WowLogManager.Instance);
            ucVpnList.TestChanged += ucNetwork_TestChanged;

            //VPNItem vpnItem = new VPNItem();
            //vpnItem.EntryName = WowSetConfig.Instance.VpnEntryName;
            //vpnItem.IP = WowSetConfig.Instance.VpnIP;
            //vpnItem.User = WowSetConfig.Instance.VpnUser;
            //vpnItem.Password = WowSetConfig.Instance.VpnPwd;
            //ucVpn = new UcVpnItem(vpnItem, WowLogManager.Instance);
            //ucVpn.TestChanged += ucNetwork_TestChanged;
        }

        #region UI events

        private void FrmWebDetection_Load(object sender, EventArgs e)
        {
            this.LoadParmas();
            this.LoadUcNet();
            this.chkRestart_CheckedChanged(null, null);
            this.radioCustomRange_CheckedChanged(null, null);
        }

        private bool isClosing = false;

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucProxy1.Stop();
            this.isClosing = true;
            this.DisposeHttpManager();

            WowLogManager.Instance.LogEvent -= LogManager_LogEvent;
            ucAdsl.TestChanged -= ucNetwork_TestChanged;
            ucVpnList.TestChanged -= ucNetwork_TestChanged;
            ucRouter.TestChanged -= ucNetwork_TestChanged;
            //ucVpn.TestChanged -= ucNetwork_TestChanged;
            this.Load -= FrmWebDetection_Load;
            this.FormClosing -= FrmMain_FormClosing;

            BattleOutptMgt.Instance.Save();
            WowSetConfig.Instance.Save();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DetectionParamsItem detectionItem = GetDetectionParamsItem();
            if (null != detectionItem)
            {
                this.CreateHttperManager();
                this.ClearLog();
                this.SetEnableForButton(false);
                this.ShowProcessCount(true);
                this.StartSpendTimer();
                if (this.radioVpn.Checked)
                {
                    this.ucVpnList.Test();
                }
                httpManager.Start(detectionItem);
            }
            SoundPlayer.PlayAlter();
            WowSetConfig.Instance.IsCompleted = false;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openDialog.FileName))
            {
                this.txtFilePath.Text = openDialog.FileName;
            }
            else
            {
                MessageBox.Show("请选择一个帐户数据文件", "（WOW魔兽）提醒");
            }
        }

        private bool isManualStopped = false;

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.isManualStopped = true;
            this.DisposeHttpManager();
            this.SetEnableForButton(true);
            SoundPlayer.PlayAlter();
        }

        private void btnPaused_Click(object sender, EventArgs e)
        {
            if (null != this.httpManager)
            {
                if (!this.httpManager.IsPaused)
                {
                    this.httpManager.PauseProcess();
                    this.btnPaused.Text = "继续";
                    this.radioFromStopped.Checked = true;
                    SoundPlayer.PlayAlter();
                    this.StopSpendTimer();
                }
                else
                {
                    this.httpManager.RestartProcess();
                    this.btnPaused.Text = "暂停";
                    this.StartSpendTimer();
                }
            }
        }

        private void ClearLog()
        {
            itemCount = 0;
            txtLog.Text = string.Empty;
            progressProcess.Value = 0;
        }

        /// <summary>
        /// If btnStart is true ,btnStop is false
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetEnableForButton(bool isEnabled)
        {
            this.btnStart.Enabled = isEnabled;
            this.btnPaused.Enabled = !isEnabled;
            this.btnStop.Enabled = this.btnPaused.Enabled;

            if (!isEnabled)
            {
                this.tabWebDetection.SelectedTab = pageDisplay;
            }
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckNetworkPanel();
        }

        private void ucNetwork_TestChanged(object sender, BoolEventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                if (e.IsTrue)
                {
                    this.tabWebDetection.SelectTab(pageDisplay);
                    txtLog.Clear();
                    this.StartReconnectTimer();
                }
                else
                {
                    this.StopReconnectTimer();
                }
            }));
        }

        private void CheckNetworkPanel()
        {
            if (radioADSL.Checked)
            {
                panelNet.Controls.Clear();
                panelNet.Controls.Add(ucAdsl);
            }

            if (radioRouter.Checked)
            {
                panelNet.Controls.Clear();
                panelNet.Controls.Add(ucRouter);
            }

            if (radioVpn.Checked)
            {
                panelNet.Controls.Clear();
                // panelNet.Controls.Add(ucVpn);
                panelNet.Controls.Add(ucVpnList);
            }
        }

        private void chkRestart_CheckedChanged(object sender, EventArgs e)
        {
            groupRestartMode.Enabled = chkReconnect.Checked;
            if (!chkReconnect.Checked)
            {
                radioADSL.Checked = chkReconnect.Checked;
                radioRouter.Checked = chkReconnect.Checked;
                radioVpn.Checked = chkReconnect.Checked;
                panelNet.Controls.Clear();
            }

            this.CheckNetworkPanel();
        }

        private void radioCustomRange_CheckedChanged(object sender, EventArgs e)
        {
            this.txtLower.Enabled = this.radioCustomRange.Checked;
            this.txtUpper.Enabled = this.radioCustomRange.Checked;
        }

        private void btnStopSound_Click(object sender, EventArgs e)
        {
            SoundPlayer.StopWarn();
        }

        private void chkCharacter_CheckedChanged(object sender, EventArgs e)
        {
            chkGValue.Enabled = chkBattleChar.Checked;
            if (!chkBattleChar.Checked)
            {
                chkGValue.Checked = false;
            }
            txtCharacterNum.Enabled = chkUnbattleChar.Checked;
            chkStopByLevel.Enabled = chkUnbattleChar.Checked;

            if (chkUnbattleChar.Checked)
            {
                SoundPlayer.PlayAlter();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("你当前打开了查等级功能,以下是注意问题：");
                sb.AppendLine("1、请不要和对号功能一起用，以免网络重启丢失数据！");
                sb.AppendLine("2、请使用对号生成的非战正式用户！");
                sb.AppendLine("3、请控制线程数目，尽量使用默认值 ,暂还不确认服务是否会拒绝访问！");
                MessageBox.Show(sb.ToString(), "WOW魔兽）严重警告！");
            }
        }

        private void SetBtnPausedStateInReconnect(bool isEnabled)
        {
            if (btnPaused.Enabled != isEnabled)
            {
                btnPaused.Enabled = isEnabled;
            }
        }

        private string lastThreads = string.Empty;
        private void chkSwitchIP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIPLimit.Checked)
            {
                lastThreads = txtThreads.Text;
                txtThreads.Text = WowSetConfig.Instance.IPAcessTimes.ToString();
                txtThreads.Enabled = false;
            }
            else
            {
                txtThreads.Text = string.IsNullOrEmpty(lastThreads) ? WowSetConfig.Instance.Threads.ToString() : lastThreads;
                txtThreads.Enabled = true;
            }
        }

        private void comboServer_SelectedValueChanged(object sender, EventArgs e)
        {
            GameServer gameServer = comboServer.SelectedItem as GameServer;
            if (null != gameServer)
            {
                this.ucProxy1.Server = gameServer;
            }
        }

        #endregion UI events

        #region Business events

        private void LogManager_LogEvent(string log)
        {
            if (this.isClosing) { return; }

            this.BeginInvoke(new ThreadStart(delegate()
                            {
                                txtLog.AppendText("\r\n" + log);
                                SetUIByLog(log);
                            }));
        }

        private void SetUIByLog(string log)
        {
            if (TextHelper.IsContains(log, "角色"))
            {
                this.ShowProcessCount(false);
            }
            else if (TextHelper.IsContains(log, "正在进行网络重连"))
            {
                SetBtnPausedStateInReconnect(false);
            }
            else if (TextHelper.IsContains(log, "程序就绪，正在重新启动查询功能"))
            {
                SetBtnPausedStateInReconnect(true);
            }
        }

        private const int MaxLine = 200;
        private int itemCount = 0;

        private void detectionManager_ProcessItemChanged(object sender, ProcessEventArgs<UserAccountItem> e)
        {
            lock (this)
            {
                if (this.isClosing) { return; }

                try
                {
                    this.BeginInvoke(new ThreadStart(delegate()
                              {
                                  if (this.httpManager.TotalCount != this.progressProcess.Maximum)
                                  {
                                      this.progressProcess.Maximum = this.httpManager.TotalCount;
                                      this.progressProcess.Minimum = 0;
                                  }

                                  string line = "\r\n" + e.Item.UserDetail;
                                  txtLog.AppendText(line);
                                  //if IsTooManyAttempt,don't export to file and update progress
                                  if (e.Item.IsNeedRedial)
                                  {
                                      return;
                                  }

                                  if (itemCount < this.httpManager.TotalCount)
                                  {
                                      this.progressProcess.Value = ++itemCount;
                                  }

                                  BattleOutptMgt.Instance.Output(e.Item, httpManager.HttperParamsItem);
                                  this.ShowProcessCount(false);

                                  if (txtLog.Lines.Length >= MaxLine)
                                  {
                                      txtLog.Clear();
                                  }
                                  else
                                  {
                                      //txtLog.SelectedText = line;
                                      txtLog.ScrollToCaret();
                                  }
                              }));
                }
                catch (Exception ex)
                {
                    WowLogManager.Instance.Info("Update UI error:" + ex.Message);
                }
            }
        }

        private void ShowProcessCount(bool isRestart)
        {
            if (isRestart)
            {
                this.txtTotalCount.Text = "0";
                this.txtRemainCount.Text = "0";
                this.txtProcessedCount.Text = "0";
                this.txtSuccessCount.Text = "0";
                this.txtFailedCount.Text = "0";
                this.txtRetryCount.Text = "0";
                this.txtUselessCount.Text = "0";
                this.lblLeveDetail.Text = "0";
                BattleOutptMgt.Instance.LevelDetailCount = 0;
            }
            else
            {
                if (null != this.httpManager)
                {
                    this.txtTotalCount.Text = this.httpManager.TotalCount.ToString();
                    int remainCount = this.httpManager.TotalCount - BattleOutptMgt.Instance.ProcessedCount;
                    this.txtRemainCount.Text = (remainCount < 0 ? 0 : remainCount).ToString();
                }
                this.txtProcessedCount.Text = BattleOutptMgt.Instance.ProcessedCount.ToString();
                this.txtSuccessCount.Text = BattleOutptMgt.Instance.SucceedCount.ToString();
                this.txtFailedCount.Text = BattleOutptMgt.Instance.FailedCount.ToString();
                this.txtRetryCount.Text = BattleOutptMgt.Instance.RetryCount.ToString();
                this.txtUselessCount.Text = BattleOutptMgt.Instance.UselessCount.ToString();
                this.lblLeveDetail.Text = BattleOutptMgt.Instance.LevelDetailCount.ToString();
            }
        }

        private void ProcessCompleted()
        {
            if (this.isClosing) { return; }

            this.BeginInvoke(new ThreadStart(delegate()
             {
                 BattleOutptMgt.Instance.LoadLastStopCount();
                 BattleOutptMgt.Instance.Save();
                 //this.ShowProcessCount(false);
                 this.SetEnableForButton(true);
                 this.StopSpendTimer();
                 this.DisposeHttpManager();
                 PlayProcessCompletedSound();
                 MessageBox.Show(string.Format("完成（WOW魔兽）网络对号！"), "（WOW魔兽）提醒");
                 SoundPlayer.StopWarn();
             }));
        }

        private void PlayProcessCompletedSound()
        {
            if (!this.isManualStopped)
            {
                SoundPlayer.PlayWarn();
            }
            isManualStopped = false;
        }

        private void CreateHttperManager()
        {
            this.DisposeHttpManager();
            httpManager = new WowHttperManager();
            httpManager.ProcessItemChanged += detectionManager_ProcessItemChanged;
            httpManager.ProcessCompleted += detectionManager_ProcessCompleted;
            httpManager.ReconnectChanged += detectionManager_ReconnectChanged;
        }

        private void detectionManager_ReconnectChanged(object sender, BoolEventArgs e)
        {
            if (this.isClosing) { return; }

            this.BeginInvoke(new ThreadStart(delegate()
            {
                if (e.IsTrue)
                {
                    SoundPlayer.PlayWarn();
                    this.StartReconnectTimer();
                }
                else
                {
                    this.StopReconnectTimer();
                }
            }));
        }

        private void detectionManager_ProcessCompleted(object sender, BoolEventArgs e)
        {
            this.ProcessCompleted();
            WowSetConfig.Instance.IsCompleted = e.IsTrue;
        }

        private void DisposeHttpManager()
        {
            if (null != httpManager)
            {
                httpManager.ProcessItemChanged -= detectionManager_ProcessItemChanged;
                httpManager.ProcessCompleted -= detectionManager_ProcessCompleted;
                httpManager.ReconnectChanged -= detectionManager_ReconnectChanged;
                httpManager.Dispose();
                httpManager = null;
            }
            this.StopReconnectTimer();
            this.StopSpendTimer();
        }

        #endregion Business events

        #region Net reconnect Timer

        System.Windows.Forms.Timer reconnectTimer = null;
        int timerCount = 0;

        private void StartReconnectTimer()
        {
            if (null == this.reconnectTimer)
            {
                this.reconnectTimer = new System.Windows.Forms.Timer();
                // 1 second
                this.reconnectTimer.Interval = 1 * 1000;
                this.reconnectTimer.Tick += reconnectTimer_Tick;
            }
            this.StopReconnectTimer();
            this.reconnectTimer.Start();
        }

        private void reconnectTimer_Tick(object sender, EventArgs e)
        {
            this.labSeconds.Text = (++timerCount).ToString();
        }

        private void StopReconnectTimer()
        {
            if ((null != this.reconnectTimer) && this.reconnectTimer.Enabled)
            {
                this.reconnectTimer.Stop();
            }
            timerCount = 0;
            this.labSeconds.Text = timerCount.ToString();
        }

        System.Windows.Forms.Timer spendTimer = null;
        long spendCount = 0;

        private void StartSpendTimer()
        {
            if (null == this.spendTimer)
            {
                this.spendTimer = new System.Windows.Forms.Timer();
                // 1 second
                this.spendTimer.Interval = 1 * 1000;
                this.spendTimer.Tick += spendTimer_Tick;
            }
            this.StopSpendTimer();
            spendCount = 0;
            this.spendTimer.Start();
        }

        private void spendTimer_Tick(object sender, EventArgs e)
        {
            spendCount++;
            this.txtSpend.Text = TimeSpan.FromSeconds(spendCount).ToString();
        }

        private void StopSpendTimer()
        {
            if ((null != this.spendTimer) && this.spendTimer.Enabled)
            {
                this.spendTimer.Stop();
            }
            this.txtSpend.Text = TimeSpan.FromSeconds(spendCount).ToString();
        }

        #endregion Net reconnect Timer

    }
}