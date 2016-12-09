using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;
using System.Text;

namespace RSTool
{
    public partial class FrmRS : Form
    {
        private IList<GameServer> gameServerList = new List<GameServer>();
        private RSHttperManager httpManager = null;

        public FrmRS()
        {
            InitializeComponent();
            this.Load += FrmWebDetection_Load;
            this.FormClosing += FrmMain_FormClosing;
            RSLogManager.Instance.LogEvent += LogManager_LogEvent;
        }

        UcAdsl ucAdsl = null;
        UcRouter ucRouter = null;
        UcVpnList ucVpnList = null;
        //UcVpnItem ucVpn = null;

        private void LoadUcNet()
        {
            ADSLItem adsl = new ADSLItem();
            adsl.EntryName = RSSetConfig.Instance.ADSLName;
            adsl.Password = RSSetConfig.Instance.ADSLPwd;
            adsl.User = RSSetConfig.Instance.ADSLUser;
            ucAdsl = new UcAdsl(adsl, RSLogManager.Instance);
            ucAdsl.TestChanged += ucNetwork_TestChanged;

            RouterItem router = new RouterItem();
            router.RouterType = RSSetConfig.Instance.RouterType;
            router.IP = RSSetConfig.Instance.RouterIP;
            router.Password = RSSetConfig.Instance.RouterPwd;
            router.User = RSSetConfig.Instance.RouterUser;
            ucRouter = new UcRouter(router, RSLogManager.Instance);
            ucRouter.TestChanged += ucNetwork_TestChanged;

            VPNFile vpn = new VPNFile();
            vpn.EntryName = RSSetConfig.Instance.VpnEntryName;
            vpn.File = RSSetConfig.Instance.VpnFile;
            ucVpnList = new UcVpnList(vpn, RSLogManager.Instance);
            ucVpnList.TestChanged += ucNetwork_TestChanged;

            //VPNItem vpnItem = new VPNItem();
            //vpnItem.EntryName = RSSetConfig.Instance.VpnEntryName;
            //vpnItem.IP = RSSetConfig.Instance.VpnIP;
            //vpnItem.User = RSSetConfig.Instance.VpnUser;
            //vpnItem.Password = RSSetConfig.Instance.VpnPwd;
            //ucVpn = new UcVpnItem(vpnItem, RSLogManager.Instance);
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
            this.isClosing = true;
            this.DisposeHttpManager();

            RSLogManager.Instance.LogEvent -= LogManager_LogEvent;
            ucAdsl.TestChanged -= ucNetwork_TestChanged;
            ucVpnList.TestChanged -= ucNetwork_TestChanged;
            ucRouter.TestChanged -= ucNetwork_TestChanged;
            //ucVpn.TestChanged -= ucNetwork_TestChanged;
            this.Load -= FrmWebDetection_Load;
            this.FormClosing -= FrmMain_FormClosing;

            RSOutputMgt.Instance.Save();
            RSSetConfig.Instance.Save();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DetectionParamsItem detectionItem = GetDetectionParamsItem();
            if (null != detectionItem)
            {
                this.CreateHttperManamger();
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
            RSSetConfig.Instance.IsCompleted = false;
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
                MessageBox.Show("请选择一个帐户数据文件", "（江湖）提醒");
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
                    this.radioFromStopped.Checked = true;
                    this.btnPaused.Text = "继续";
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
            this.btnClear.Enabled = isEnabled;
            this.btnPaused.Enabled = !isEnabled;
            this.btnStop.Enabled = this.btnPaused.Enabled;

            if (!isEnabled)
            {
                this.tabWebDetection.SelectedTab = pageDisplay;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CaptchaHelper.Instance.ClearCaptchaFolder();
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
                //panelNet.Controls.Add(ucVpn);
                panelNet.Controls.Add(this.ucVpnList);
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

        private void SetBtnPausedStateInReconnect(bool isEnabled)
        {
            if (btnPaused.Enabled != isEnabled)
            {
                btnPaused.Enabled = isEnabled;
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

        private void httpManager_ProcessItemChanged(object sender, ProcessEventArgs<RSAccountItem> e)
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

                                  string line = "\r\n" + e.Item.ToString();
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

                                  RSOutputMgt.Instance.Output(e.Item, httpManager.HttperParamsItem);
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
                    RSLogManager.Instance.Info("Update UI error:" + ex.Message);
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
                this.txtUnknowCount.Text = "0";
            }
            else
            {
                if (null != this.httpManager)
                {
                    this.txtTotalCount.Text = this.httpManager.TotalCount.ToString();
                    int remainCount = this.httpManager.TotalCount - RSOutputMgt.Instance.ProcessedCount;
                    this.txtRemainCount.Text = (remainCount < 0 ? 0 : remainCount).ToString();
                }
                this.txtProcessedCount.Text = RSOutputMgt.Instance.ProcessedCount.ToString();
                this.txtSuccessCount.Text = RSOutputMgt.Instance.SucceedCount.ToString();
                this.txtFailedCount.Text = RSOutputMgt.Instance.FailedCount.ToString();
                this.txtUnknowCount.Text = RSOutputMgt.Instance.UnknownCount.ToString();
            }
        }

        private void ProcessCompleted()
        {
            if (this.isClosing) { return; }

            this.BeginInvoke(new ThreadStart(delegate()
             {
                 RSOutputMgt.Instance.LoadLastStopCount();
                 RSOutputMgt.Instance.Save();
                 //this.ShowProcessCount(false);
                 this.SetEnableForButton(true);
                 this.StopSpendTimer();
                 this.DisposeHttpManager();
                 PlayProcessCompletedSound();

                 MessageBox.Show(string.Format("完成（江湖）网络对号 ！"));
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

        private void CreateHttperManamger()
        {
            this.DisposeHttpManager();
            httpManager = new RSHttperManager();
            httpManager.ProcessItemChanged += httpManager_ProcessItemChanged;
            httpManager.ProcessCompleted += httpManager_ProcessCompleted;
            httpManager.ReconnectChanged += httpManager_ReconnectChanged;
        }

        private void httpManager_ReconnectChanged(object sender, BoolEventArgs e)
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

        private void httpManager_ProcessCompleted(object sender, BoolEventArgs e)
        {
            this.ProcessCompleted();
            RSSetConfig.Instance.IsCompleted = e.IsTrue;
        }

        private void DisposeHttpManager()
        {
            if (null != httpManager)
            {
                httpManager.ProcessItemChanged -= httpManager_ProcessItemChanged;
                httpManager.ProcessCompleted -= httpManager_ProcessCompleted;
                httpManager.ReconnectChanged -= httpManager_ReconnectChanged;
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