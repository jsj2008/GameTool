using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace WebDetection
{
    public partial class FrmWebDetection
    {
        #region Load params / set params

        private void LoadParmas()
        {
            #region game server

            gameServerList.Clear();

            gameServerList.Add(GameWowServers.USServer);
            gameServerList.Add(GameWowServers.ENServer);
            gameServerList.Add(GameWowServers.SEAServer);
            gameServerList.Add(GameWowServers.TWServer);
            gameServerList.Add(GameWowServers.KRServer);

            comboServer.DataSource = gameServerList;
            comboServer.SelectedIndex = WowSetConfig.Instance.GameServerNo;

            if (!string.IsNullOrEmpty(WowSetConfig.Instance.LastFile) && !WowSetConfig.Instance.IsCompleted)
                this.txtFilePath.Text = WowSetConfig.Instance.LastFile;

            #endregion game server

            #region raw data type

            List<DataTypeItem> list = new List<DataTypeItem>();
            list.Add(new DataTypeItem() { Text = "帐号|密码|邮箱", Type = DataFormat.AccountPasswordMail });
            list.Add(new DataTypeItem() { Text = "帐号|邮箱|密码", Type = DataFormat.AccountMailPassword });
            list.Add(new DataTypeItem() { Text = "邮箱|密码", Type = DataFormat.MailPassword });
            list.Add(new DataTypeItem() { Text = "帐号|密码", Type = DataFormat.AccountPassword });
            comboDataType.DataSource = list;
            this.comboDataType.SelectedValue = WowSetConfig.Instance.DataFormat;

            #endregion raw data type

            #region Query Type

            QueryType qType = WowSetConfig.Instance.QuertyType;
            radioFromFirst.Checked = qType == QueryType.FromFrist;
            radioCustomRange.Checked = qType == QueryType.FromCustomRange;
            radioFromStopped.Checked = qType == QueryType.FromStopped;

            if (!WowSetConfig.Instance.IsCompleted)
            {
                radioFromStopped.Checked = true;
            }

            #endregion Query Type

            #region Restart Mode

            this.chkReconnect.Checked = WowSetConfig.Instance.IsSupportedReconnect;
            if (this.chkReconnect.Checked)
            {
                switch (WowSetConfig.Instance.ReconnectType)
                {
                    case ReconnectType.ADSL:
                        radioADSL.Checked = true; break;
                    case ReconnectType.Router:
                        radioRouter.Checked = true; break;
                    case ReconnectType.VPN:
                        radioVpn.Checked = true; break;
                    default:
                        break;
                }
            }

            #endregion Restart Mode

            this.txtThreads.Text = WowSetConfig.Instance.Threads.ToString();
            this.txtRepeatCount.Text = WowSetConfig.Instance.ErrorRepeatCount.ToString();

            this.txtLower.Text = WowSetConfig.Instance.RangeLower.ToString();
            this.txtUpper.Text = WowSetConfig.Instance.RangeUpper.ToString();

            this.chkDelCapatch.Checked = WowSetConfig.Instance.IsDeleteCaptcha;
            this.txtCaptchCount.Text = WowSetConfig.Instance.CaptchCount.ToString();

            this.ucProxy1.IP = WowSetConfig.Instance.ProxyIP;
            this.ucProxy1.Port = WowSetConfig.Instance.ProxyPort;
            this.ucProxy1.ProxyFile = WowSetConfig.Instance.ProxyFile;

            this.ClearLog();
            this.SetEnableForButton(true);
        }

        private DetectionParamsItem GetDetectionParamsItem()
        {
            DetectionParamsItem detectionItem = new DetectionParamsItem();
            StringBuilder sb = new StringBuilder();
            int i = 1;
            //If file / server changed
            bool isSettingChaned = false;

            detectionItem.IsDeleteCaptcha = chkDelCapatch.Checked;
            WowSetConfig.Instance.IsDeleteCaptcha = chkDelCapatch.Checked;

            #region GameServer

            GameServer gameServer = comboServer.SelectedItem as GameServer;
            if (null != gameServer)
            {
                detectionItem.CurrentGameServer = gameServer;
                if (WowSetConfig.Instance.GameServerNo != comboServer.SelectedIndex)
                {
                    isSettingChaned = true;
                }
                WowSetConfig.Instance.GameServerNo = comboServer.SelectedIndex;

                detectionItem.GameServerList = gameServerList;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请选择正确的游戏服务器网站！", i++));
            }

            #endregion GameServer

            detectionItem.HttpTimeout = WowSetConfig.Instance.HttpTimeout;

            #region Threads

            if (TextHelper.IsNumber(txtThreads.Text.Trim()))
            {
                int threads = TextHelper.StringToInt(txtThreads.Text.Trim());
                if (threads < 0 || threads > 50)
                {
                    sb.AppendLine(string.Format("{0}、请录入正确的线程整数值范围（1-50）！", i++));
                }

                detectionItem.Threads = threads;
                WowSetConfig.Instance.Threads = threads;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的线程整数值（如：5）！", i++));
            }

            if (TextHelper.IsNumber(this.txtRepeatCount.Text.Trim()))
            {
                int count = TextHelper.StringToInt(this.txtRepeatCount.Text.Trim());
                if (count >= 0)
                {
                    detectionItem.ErrorRepeatCount = count;
                    WowSetConfig.Instance.ErrorRepeatCount = count;
                }
                else
                {
                    sb.AppendLine(string.Format("{0}、请录入有效的出错重试次数！", i++));
                }
            }
            else
            {
                sb.AppendLine(string.Format("0}、请录入有效的出错重试次数！", i++));
            }

            if (TextHelper.IsNumber(this.txtCaptchCount.Text.Trim()))
            {
                int count = TextHelper.StringToInt(this.txtCaptchCount.Text.Trim());
                if (count > 0)
                {
                    detectionItem.CaptchaErrorCount = count;
                    WowSetConfig.Instance.CaptchCount = count;
                }
                else
                {
                    sb.AppendLine(string.Format("{0}、请录入有效的验证码出错重启数，默认是5！", i++));
                }
            }
            else
            {
                sb.AppendLine(string.Format("0}、请录入有效的验证码出错重启数，默认是5！", i++));
            }
            detectionItem.IsAutoSwitch = chkAutoSwitch.Checked;

            #endregion Threads

            #region Get characters

            detectionItem.IsGetUnbattleChar = this.chkUnbattleChar.Checked;
            detectionItem.IsGetBattleChar = this.chkBattleChar.Checked;
            detectionItem.IsGetDetail = this.chkDetail.Checked;
            detectionItem.IsGetGValue = this.chkGValue.Checked;

            if (this.chkBattleChar.Checked)
            {
                detectionItem.IsStopByLevel = this.chkStopByLevel.Checked;
                if (TextHelper.IsNumber(this.txtCharacterNum.Text.Trim()))
                {
                    int count = TextHelper.StringToInt(this.txtCharacterNum.Text.Trim());
                    if (count > 0)
                    {
                        detectionItem.AvailableCharacterLevel = count;
                    }
                }
                else
                {
                    sb.AppendLine(string.Format("{0}、请输入有效的等级，默认为70！", i++));
                }
            }

            #endregion

            #region FilePath

            if (!string.IsNullOrEmpty(txtFilePath.Text.Trim()) && File.Exists(txtFilePath.Text))
            {
                detectionItem.DataFilePath = txtFilePath.Text.Trim();
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的数据文件路径，或者文件是否存在！", i++));
            }

            detectionItem.DataFormat = (DataFormat)comboDataType.SelectedValue;
            WowSetConfig.Instance.DataFormat = detectionItem.DataFormat;

            #endregion FilePath

            #region reconnect mode

            detectionItem.IsSupportedReconnect = chkReconnect.Checked;
            WowSetConfig.Instance.IsSupportedReconnect = chkReconnect.Checked;

            if (chkReconnect.Checked)
            {
                if (radioADSL.Checked) detectionItem.ReconnectType = ReconnectType.ADSL;
                if (radioRouter.Checked) detectionItem.ReconnectType = ReconnectType.Router;
                if (radioVpn.Checked) detectionItem.ReconnectType = ReconnectType.VPN;

                WowSetConfig.Instance.ReconnectType = detectionItem.ReconnectType;

                if (radioADSL.Checked)
                {
                    ADSLItem adsl = this.ucAdsl.GetADSLItem();
                    if (null == adsl)
                    {
                        return null;
                    }
                    else
                    {
                        detectionItem.ADSL = adsl;
                        WowSetConfig.Instance.ADSLName = adsl.EntryName;
                        WowSetConfig.Instance.ADSLPwd = adsl.Password;
                        WowSetConfig.Instance.ADSLUser = adsl.User;
                    }
                }

                if (radioVpn.Checked)
                {
                    IList<VPNItem> vpnList = this.ucVpnList.GetVpnList();
                    // VPNItem item = this.ucVpn.GetVpnParams();
                    if (null != vpnList && vpnList.Count > 0)
                    {
                        detectionItem.VpnList = vpnList;
                        WowSetConfig.Instance.VpnEntryName = vpnList[0].EntryName;
                        WowSetConfig.Instance.VpnFile = this.ucVpnList.VpnFile;
                        WowSetConfig.Instance.VpnIP = vpnList[0].IP;
                        WowSetConfig.Instance.VpnUser = vpnList[0].User;
                        WowSetConfig.Instance.VpnPwd = vpnList[0].Password;
                    }
                    else
                    {
                        return null;
                    }
                }

                if (radioRouter.Checked)
                {
                    RouterItem router = this.ucRouter.GetRouterItem();
                    if (null == router)
                    {
                        return null;
                    }
                    else
                    {
                        detectionItem.Router = router;
                        WowSetConfig.Instance.RouterType = router.RouterType;
                        WowSetConfig.Instance.RouterIP = router.IP;
                        WowSetConfig.Instance.RouterPwd = router.Password;
                        WowSetConfig.Instance.RouterUser = router.User;
                    }
                }

                if (!radioRouter.Checked && !radioVpn.Checked && !radioADSL.Checked)
                {
                    sb.AppendLine(string.Format("{0}、当前选择了网络重连功能，但没有选择具体的重连方式！！", i++));
                }
            }

            #endregion reconnect mode

            #region Data query type

            if (radioCustomRange.Checked && !this.GetCustomRange(detectionItem))
            {
                return null;
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "（WOW魔兽）提醒");
                return null;
            }

            if (radioFromFirst.Checked)
            {
                detectionItem.QueryType = QueryType.FromFrist;
            }
            else if (radioCustomRange.Checked)
            {
                detectionItem.QueryType = QueryType.FromCustomRange;
            }
            else if (radioFromStopped.Checked)
            {
                detectionItem.QueryType = QueryType.FromStopped;
            }
            WowSetConfig.Instance.QuertyType = detectionItem.QueryType;

            #endregion Data query type

            #region warning Dialog

            i = 1;
            sb = new StringBuilder();
            sb.AppendLine(string.Format("{0}、请确认选择的游戏网站，数据格式是否录入正确！", i++));
            sb.AppendLine(string.Format("{0}、请确认选择扫瞄方式是不是正确，否则容易造成重复劳动！", i++));
            sb.AppendLine(string.Format("{0}、请确认录入线程数，网络连接超时，验证码出错重试数是否录入正确！", i++));

            if (this.radioCustomRange.Checked)
            {
                sb.AppendLine(string.Format("{0}、请确认是否真的要按自定义的数据范围来进行处理，并且上下行值是否正确！", i++));
            }

            if (chkReconnect.Checked)
            {
                sb.AppendLine(string.Format("{0}、请确认是否真的要支持网络重连，并且网络重连的方式是否正确！", i++));
            }

            if (this.radioADSL.Checked)
            {
                sb.AppendLine(string.Format("{0}、请确认ADSL名称是否是当前使用的PPPOE名称，用户名及密码是否正确！", i++));
            }

            if (this.radioVpn.Checked)
            {
                sb.AppendLine(string.Format("{0}、请确认VPN名称，IP址址，用户名及密码是否正确！", i++));
            }

            if (this.radioRouter.Checked)
            {
                sb.AppendLine(string.Format("{0}、请确认选择的路由器类型，IP址址，用户名及密码是否正确！", i++));
            }

            DialogResult dlgResult = MessageBox.Show(sb.ToString(), "（WOW）提醒 ---->  真的要按当前的配置执行吗？", MessageBoxButtons.OKCancel);
            if (dlgResult != DialogResult.OK)
            {
                return null;
            }

            #endregion warning Dialog


            detectionItem.IsSupportProxy = this.ucProxy1.IsSupportProxy;
            if (detectionItem.IsSupportProxy)
            {
                detectionItem.ProxyList = this.ucProxy1.ProxyList;
            }

            WowSetConfig.Instance.ProxyIP = this.ucProxy1.IP;
            WowSetConfig.Instance.ProxyPort = this.ucProxy1.Port;
            WowSetConfig.Instance.ProxyFile = this.ucProxy1.ProxyFile;

            detectionItem.IsIpAccessLimit = chkIPLimit.Checked;

            if (WowSetConfig.Instance.LastFile.Trim() != detectionItem.DataFilePath.Trim())
            {
                isSettingChaned = true;
            }
            if (radioFromFirst.Checked || isSettingChaned)
            {
                BattleDBHelper.ClearTable(WOWTableName.QueriedAccount, WOWTableName.HistoryLists);
            }

            WowSetConfig.Instance.Save();
            return detectionItem;
        }

        private bool GetCustomRange(DetectionParamsItem detectionItem)
        {
            int i = 0;
            int lower = -1, upper = -1;
            StringBuilder sb = new StringBuilder();

            string lowerTxt = this.txtLower.Text.Trim();
            string upperTxt = this.txtUpper.Text.Trim();

            if (!TextHelper.IsNumber(lowerTxt))
            {
                sb.AppendLine(string.Format("{0}、自定义范围的起始值非法！", ++i));
            }
            else
            {
                lower = TextHelper.StringToInt(lowerTxt);
            }

            if (!TextHelper.IsNumber(upperTxt))
            {
                sb.AppendLine(string.Format("{0}、自定义范围的结束值非法！", ++i));
            }
            else
            {
                upper = TextHelper.StringToInt(upperTxt);
            }

            if ((upper <= 0) || (lower <= 0) || (lower > upper))
            {
                sb.AppendLine(string.Format("{0}、自定义范围的起始值和结束值必须大于0,并且前值必须大于后值！", ++i));
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return false;
            }

            detectionItem.RangeLower = lower - 1;
            detectionItem.RangeUpper = upper - 1;

            WowSetConfig.Instance.RangeLower = lower;
            WowSetConfig.Instance.RangeUpper = upper;

            return true;
        }

        #endregion Load params / set params
    }
}