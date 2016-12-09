using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace Apple
{
    public partial class FrmMain
    {
        #region Load params / set params

        private void LoadParmas()
        {
            #region game server

            gameServerList.Clear();

            gameServerList.Add(GameServers.AppleServer);

            comboServer.DataSource = gameServerList;
            comboServer.SelectedIndex = SetConfig.Instance.GameServerNo;

            if (!string.IsNullOrEmpty(SetConfig.Instance.LastFile) && !SetConfig.Instance.IsCompleted)
                this.txtFilePath.Text = SetConfig.Instance.LastFile;

            #endregion game server

            #region raw data type

            List<DataTypeItem> list = new List<DataTypeItem>();
            list.Add(new DataTypeItem() { Text = "帐号|密码|邮箱", Type = DataFormat.AccountPasswordMail });
            list.Add(new DataTypeItem() { Text = "帐号|邮箱|密码", Type = DataFormat.AccountMailPassword });
            list.Add(new DataTypeItem() { Text = "邮箱|密码", Type = DataFormat.MailPassword });
            list.Add(new DataTypeItem() { Text = "帐号|密码", Type = DataFormat.AccountPassword });
            comboDataType.DataSource = list;
            this.comboDataType.SelectedValue = SetConfig.Instance.DataFormat;

            #endregion raw data type

            #region Query Type

            QueryType qType = SetConfig.Instance.QuertyType;
            radioFromFirst.Checked = qType == QueryType.FromFrist;
            radioCustomRange.Checked = qType == QueryType.FromCustomRange;
            radioFromStopped.Checked = qType == QueryType.FromStopped;

            if (!SetConfig.Instance.IsCompleted)
            {
                radioFromStopped.Checked = true;
            }

            #endregion Query Type

            #region Restart Mode

            this.chkReconnect.Checked = SetConfig.Instance.IsSupportedReconnect;
            if (this.chkReconnect.Checked)
            {
                switch (SetConfig.Instance.ReconnectType)
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

            this.txtThreads.Text = SetConfig.Instance.Threads.ToString();

            this.txtLower.Text = SetConfig.Instance.RangeLower.ToString();
            this.txtUpper.Text = SetConfig.Instance.RangeUpper.ToString();

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

            #region GameServer

            GameServer gameServer = comboServer.SelectedItem as GameServer;
            if (null != gameServer)
            {
                detectionItem.CurrentGameServer = gameServer;
                if (SetConfig.Instance.GameServerNo != comboServer.SelectedIndex)
                {
                    isSettingChaned = true;
                }
                SetConfig.Instance.GameServerNo = comboServer.SelectedIndex;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请选择正确的游戏服务器网站！", i++));
            }

            #endregion GameServer

            detectionItem.PwdFormatOptions = ucPwdFormat1.PwdOptions;
            detectionItem.HttpTimeout = SetConfig.Instance.HttpTimeout;

            #region Threads

            if (TextHelper.IsNumber(txtThreads.Text.Trim()))
            {
                int threads = TextHelper.StringToInt(txtThreads.Text.Trim());
                if (threads < 0 || threads > 50)
                {
                    sb.AppendLine(string.Format("{0}、请录入正确的线程整数值范围（1-50）！", i++));
                }

                detectionItem.Threads = threads;
                SetConfig.Instance.Threads = threads;
            }
            else
            {
                sb.AppendLine(string.Format("{0}、请录入正确的线程整数值（如：5）！", i++));
            }
            #endregion Threads

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
            SetConfig.Instance.DataFormat = detectionItem.DataFormat;

            #endregion FilePath

            #region reconnect mode

            detectionItem.IsSupportedReconnect = chkReconnect.Checked;
            SetConfig.Instance.IsSupportedReconnect = chkReconnect.Checked;

            if (chkReconnect.Checked)
            {
                if (radioADSL.Checked) detectionItem.ReconnectType = ReconnectType.ADSL;
                if (radioRouter.Checked) detectionItem.ReconnectType = ReconnectType.Router;
                if (radioVpn.Checked) detectionItem.ReconnectType = ReconnectType.VPN;

                SetConfig.Instance.ReconnectType = detectionItem.ReconnectType;

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
                        SetConfig.Instance.ADSLName = adsl.EntryName;
                        SetConfig.Instance.ADSLPwd = adsl.Password;
                        SetConfig.Instance.ADSLUser = adsl.User;
                    }
                }

                if (radioVpn.Checked)
                {
                    IList<VPNItem> vpnList = this.ucVpnList.GetVpnList();
                    if (null != vpnList && vpnList.Count > 0)
                    {
                        detectionItem.VpnList = vpnList;
                        SetConfig.Instance.VpnEntryName = vpnList[0].EntryName;
                        SetConfig.Instance.VpnIP = vpnList[0].IP;
                        SetConfig.Instance.VpnUser = vpnList[0].User;
                        SetConfig.Instance.VpnPwd = vpnList[0].Password;
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
                        SetConfig.Instance.RouterType = router.RouterType;
                        SetConfig.Instance.RouterIP = router.IP;
                        SetConfig.Instance.RouterPwd = router.Password;
                        SetConfig.Instance.RouterUser = router.User;
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
                MessageBox.Show(sb.ToString());
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
            SetConfig.Instance.QuertyType = detectionItem.QueryType;

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

            DialogResult dlgResult = MessageBox.Show(sb.ToString(), "提醒 ---->  真的要按当前的配置执行吗？", MessageBoxButtons.OKCancel);
            if (dlgResult != DialogResult.OK)
            {
                return null;
            }

            #endregion warning Dialog

            if (SetConfig.Instance.LastFile.Trim() != detectionItem.DataFilePath.Trim())
            {
                isSettingChaned = true;
            }
            if (radioFromFirst.Checked || isSettingChaned)
            {
                DBHelper.ClearTable(AppleTableName.QueriedApple, AppleTableName.AppleResult);
            }

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
                MessageBox.Show(sb.ToString(), "（苹果）提醒");
                return false;
            }

            detectionItem.RangeLower = lower - 1;
            detectionItem.RangeUpper = upper - 1;

            SetConfig.Instance.RangeLower = lower;
            SetConfig.Instance.RangeUpper = upper;

            return true;
        }

        #endregion Load params / set params
    }
}