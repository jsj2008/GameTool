using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    public partial class FrmRanker : Form
    {
        public FrmRanker()
        {
            InitializeComponent();
            this.DiableAddAndDelRadios();
            this.FormClosing += FrmRanker_FormClosing;
            LoadParams();
        }

        private void DiableAddAndDelRadios()
        {
            this.radioAddBefore.Enabled = false;
            this.radioAddAfter.Enabled = false;
            this.radioDelBefore.Enabled = false;
            this.radioDelAfter.Enabled = false;
        }

        void FrmRanker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveParams();
        }

        private int GetStringToIndex(string s)
        {
            return Int32.Parse(s.Trim());
        }

        private bool IsInt(string s)
        {
            return Regex.IsMatch(s.Trim(), @"^\d+$");
        }

        #region Get/ Load / Save Params

        Dictionary<DataType, DataInsertParams> insertParamsDic = new Dictionary<DataType, DataInsertParams>();
        private void GetParams()
        {
            insertParamsDic.Clear();

            string filePath = txtRankFilePath.Text;
            DataFormat dataFormat = (DataFormat)comboDataType.SelectedValue;
            string targetSplitChars = txtTargetSplitChar.Text;
            string rawSplitChars = txtRawSplitChar.Text;
            bool isCanAdd = this.chkAdd.Checked;
            bool isAddBefore = this.radioAddBefore.Checked;
            bool isAddAfter = this.radioAddAfter.Checked;
            int addIndex = GetStringToIndex(this.txtAddIndex.Text) - 1;
            bool isCanDel = this.chkDel.Checked;
            bool isDelBefore = this.radioDelBefore.Checked;
            bool isDelAfter = this.radioDelAfter.Checked;
            int delIndex = GetStringToIndex(this.txtDelIndex.Text) - 1;

            #region DataType.Num

            DataInsertParams paramNum = new DataInsertParams();
            paramNum.Type = DataType.Num;
            paramNum.RawFilePath = filePath;
            paramNum.Format = dataFormat;
            paramNum.TargetSplitChars = targetSplitChars;
            paramNum.RawSplitChars = rawSplitChars;
            paramNum.IsCanAdd = isCanAdd;
            paramNum.IsCanDel = isCanDel;
            paramNum.IsAddBefore = isAddBefore;
            paramNum.IsAddAfter = isAddAfter;
            paramNum.AddIndex = addIndex;
            paramNum.IsDelBefore = isDelBefore;
            paramNum.IsDelAfter = isDelAfter;
            paramNum.DelIndex = delIndex;
            paramNum.Add3Chars = txtNum3.Text;
            paramNum.Add4Chars = txtNum4.Text;
            paramNum.Add5Chars = txtNum5.Text;
            paramNum.Add6Chars = txtNum6.Text;
            paramNum.Add7Chars = txtNum7.Text;
            paramNum.Add8Chars = txtNum8.Text;
            paramNum.Add9Chars = txtNum9.Text;

            insertParamsDic.Add(DataType.Num, paramNum);

            #endregion

            #region DataType.Char

            DataInsertParams paramChar = new DataInsertParams();
            paramChar.Type = DataType.Char;
            paramChar.RawFilePath = filePath;
            paramChar.Format = dataFormat;
            paramChar.TargetSplitChars = targetSplitChars;
            paramChar.RawSplitChars = rawSplitChars;
            paramChar.IsCanAdd = isCanAdd;
            paramChar.IsCanDel = isCanDel;
            paramChar.IsAddBefore = isAddBefore;
            paramChar.IsAddAfter = isAddAfter;
            paramChar.AddIndex = addIndex;
            paramChar.IsDelBefore = isDelBefore;
            paramChar.IsDelAfter = isDelAfter;
            paramChar.DelIndex = delIndex;
            paramChar.Add3Chars = txtChar3.Text;
            paramChar.Add4Chars = txtChar4.Text;
            paramChar.Add5Chars = txtChar5.Text;
            paramChar.Add6Chars = txtChar6.Text;
            paramChar.Add7Chars = txtChar7.Text;
            paramChar.Add8Chars = txtChar8.Text;
            paramChar.Add9Chars = txtChar9.Text;

            insertParamsDic.Add(DataType.Char, paramChar);

            #endregion

            #region DataType.Composite

            DataInsertParams paramComposite = new DataInsertParams();
            paramComposite.Type = DataType.Composit;
            paramComposite.RawFilePath = filePath;
            paramComposite.Format = dataFormat;
            paramComposite.TargetSplitChars = targetSplitChars;
            paramComposite.RawSplitChars = rawSplitChars;
            paramComposite.IsCanAdd = isCanAdd;
            paramComposite.IsCanDel = isCanDel;
            paramComposite.IsAddBefore = isAddBefore;
            paramComposite.IsAddAfter = isAddAfter;
            paramComposite.AddIndex = addIndex;
            paramComposite.IsDelBefore = isDelBefore;
            paramComposite.IsDelAfter = isDelAfter;
            paramComposite.DelIndex = delIndex;
            paramComposite.Add3Chars = txtComposite3.Text;
            paramComposite.Add4Chars = txtComposite4.Text;
            paramComposite.Add5Chars = txtComposite5.Text;
            paramComposite.Add6Chars = txtComposite6.Text;
            paramComposite.Add7Chars = txtComposite7.Text;
            paramComposite.Add8Chars = txtComposite8.Text;
            paramComposite.Add9Chars = txtComposite9.Text;

            insertParamsDic.Add(DataType.Composit, paramComposite);

            #endregion

            #region DataType.Special

            DataInsertParams paramSpecial = new DataInsertParams();
            paramSpecial.Type = DataType.Special;
            paramSpecial.RawFilePath = filePath;
            paramSpecial.Format = dataFormat;
            paramSpecial.TargetSplitChars = targetSplitChars;
            paramSpecial.RawSplitChars = rawSplitChars;
            paramSpecial.IsCanAdd = isCanAdd;
            paramSpecial.IsCanDel = isCanDel;
            paramSpecial.IsAddBefore = isAddBefore;
            paramSpecial.IsAddAfter = isAddAfter;
            paramSpecial.AddIndex = addIndex;
            paramSpecial.IsDelBefore = isDelBefore;
            paramSpecial.IsDelAfter = isDelAfter;
            paramSpecial.DelIndex = delIndex;
            paramSpecial.AddSpecialChars = txtSpecial.Text;
            insertParamsDic.Add(DataType.Special, paramSpecial);

            #endregion

        }

        private void LoadParams()
        {
            List<DataTypeItem> list = new List<DataTypeItem>();
            list.Add(new DataTypeItem() { Text = "帐号|密码|邮箱", Type = DataFormat.AccountPasswordMail });
            list.Add(new DataTypeItem() { Text = "帐号|邮箱|密码", Type = DataFormat.AccountMailPassword });
            list.Add(new DataTypeItem() { Text = "邮箱|密码", Type = DataFormat.MailPassword });
            comboDataType.DataSource = list;

            #region General

            comboDataType.SelectedValue = WowToolConfig.Instance.DataFormat;
            txtTargetSplitChar.Text = WowToolConfig.Instance.TargetSplitChars;
            txtRawSplitChar.Text = WowToolConfig.Instance.RawSplitChars;
            chkAdd.Checked = WowToolConfig.Instance.IsCanAdd;
            chkDel.Checked = WowToolConfig.Instance.IsCanDel;
            radioAddBefore.Checked = WowToolConfig.Instance.IsAddBefore;
            radioAddAfter.Checked = WowToolConfig.Instance.IsAddAfter;
            txtAddIndex.Text = WowToolConfig.Instance.AddIndex;
            radioDelBefore.Checked = WowToolConfig.Instance.IsDelBefore;
            radioDelAfter.Checked = WowToolConfig.Instance.IsDelAfter;
            txtDelIndex.Text = WowToolConfig.Instance.DelIndex;

            #endregion

            #region  Num
            txtNum3.Text = WowToolConfig.Instance.NumAdd3Chars;
            txtNum4.Text = WowToolConfig.Instance.NumAdd4Chars;
            txtNum5.Text = WowToolConfig.Instance.NumAdd5Chars;
            txtNum6.Text = WowToolConfig.Instance.NumAdd6Chars;
            txtNum7.Text = WowToolConfig.Instance.NumAdd7Chars;
            txtNum8.Text = WowToolConfig.Instance.NumAdd8Chars;
            txtNum9.Text = WowToolConfig.Instance.NumAdd9Chars;

            #endregion

            #region  Char

            txtChar3.Text = WowToolConfig.Instance.CharAdd3Chars;
            txtChar4.Text = WowToolConfig.Instance.CharAdd4Chars;
            txtChar5.Text = WowToolConfig.Instance.CharAdd5Chars;
            txtChar6.Text = WowToolConfig.Instance.CharAdd6Chars;
            txtChar7.Text = WowToolConfig.Instance.CharAdd7Chars;
            txtChar8.Text = WowToolConfig.Instance.CharAdd8Chars;
            txtChar9.Text = WowToolConfig.Instance.CharAdd9Chars;

            #endregion

            #region  Composite

            txtComposite3.Text = WowToolConfig.Instance.CompositeAdd3Chars;
            txtComposite4.Text = WowToolConfig.Instance.CompositeAdd4Chars;
            txtComposite5.Text = WowToolConfig.Instance.CompositeAdd5Chars;
            txtComposite6.Text = WowToolConfig.Instance.CompositeAdd6Chars;
            txtComposite7.Text = WowToolConfig.Instance.CompositeAdd7Chars;
            txtComposite8.Text = WowToolConfig.Instance.CompositeAdd8Chars;
            txtComposite9.Text = WowToolConfig.Instance.CompositeAdd9Chars;

            #endregion

            #region  Specialk

            txtSpecial.Text = WowToolConfig.Instance.SpecialChars;

            #endregion

            WowToolConfig.Instance.Save();
        }

        private void SaveParams()
        {
            #region General

            WowToolConfig.Instance.DataFormat = (DataFormat)comboDataType.SelectedValue;
            WowToolConfig.Instance.TargetSplitChars = txtTargetSplitChar.Text;
            WowToolConfig.Instance.RawSplitChars = txtRawSplitChar.Text;
            WowToolConfig.Instance.IsCanAdd = chkAdd.Checked;
            WowToolConfig.Instance.IsCanDel = chkDel.Checked;
            WowToolConfig.Instance.IsAddBefore = radioAddBefore.Checked;
            WowToolConfig.Instance.IsAddAfter = radioAddAfter.Checked;
            WowToolConfig.Instance.AddIndex = txtAddIndex.Text;

            WowToolConfig.Instance.IsDelBefore = radioDelBefore.Checked;
            WowToolConfig.Instance.IsDelAfter = radioDelAfter.Checked;
            WowToolConfig.Instance.DelIndex = txtDelIndex.Text;

            #endregion

            #region  Num

            WowToolConfig.Instance.NumAdd3Chars = txtNum3.Text;
            WowToolConfig.Instance.NumAdd4Chars = txtNum4.Text;
            WowToolConfig.Instance.NumAdd5Chars = txtNum5.Text;
            WowToolConfig.Instance.NumAdd6Chars = txtNum6.Text;
            WowToolConfig.Instance.NumAdd7Chars = txtNum7.Text;
            WowToolConfig.Instance.NumAdd8Chars = txtNum8.Text;
            WowToolConfig.Instance.NumAdd9Chars = txtNum9.Text;

            #endregion

            #region  Char

            WowToolConfig.Instance.CharAdd3Chars = txtChar3.Text;
            WowToolConfig.Instance.CharAdd4Chars = txtChar4.Text;
            WowToolConfig.Instance.CharAdd5Chars = txtChar5.Text;
            WowToolConfig.Instance.CharAdd6Chars = txtChar6.Text;
            WowToolConfig.Instance.CharAdd7Chars = txtChar7.Text;
            WowToolConfig.Instance.CharAdd8Chars = txtChar8.Text;
            WowToolConfig.Instance.CharAdd9Chars = txtChar9.Text;

            #endregion

            #region  Composite

            WowToolConfig.Instance.CompositeAdd3Chars = txtComposite3.Text;
            WowToolConfig.Instance.CompositeAdd4Chars = txtComposite4.Text;
            WowToolConfig.Instance.CompositeAdd5Chars = txtComposite5.Text;
            WowToolConfig.Instance.CompositeAdd6Chars = txtComposite6.Text;
            WowToolConfig.Instance.CompositeAdd7Chars = txtComposite7.Text;
            WowToolConfig.Instance.CompositeAdd8Chars = txtComposite8.Text;
            WowToolConfig.Instance.CompositeAdd9Chars = txtComposite9.Text;

            #endregion

            #region  Specialk

            WowToolConfig.Instance.SpecialChars = txtSpecial.Text;

            #endregion

            WowToolConfig.Instance.Save();
        }

        #endregion

        #region    UI event

        private void btnDataOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.ShowDialog();
            if (!string.IsNullOrEmpty(openDlg.FileName))
            {
                this.txtRankFilePath.Text = openDlg.FileName;
            }
        }

        private void btnRankStart_Click(object sender, EventArgs e)
        {
            this.StartDataRank();
        }

        /// <summary>
        /// If Button start is true, button stop is false
        /// </summary>
        /// <param name="isStarted"></param>
        private void SetRankButtonEabled(bool isStarted)
        {
            this.btnRankStart.Enabled = isStarted;
            this.btnRankStop.Enabled = !isStarted;
        }

        private void btnRankStop_Click(object sender, EventArgs e)
        {
            this.StopRank();
        }

        private void RadioIndex_CheckedChanged(object sender, EventArgs e)
        {
            txtAddIndex.ReadOnly = !(radioAddBefore.Checked || radioAddAfter.Checked);
            txtDelIndex.ReadOnly = !(radioDelBefore.Checked || radioDelAfter.Checked);
        }
        
        private void chkAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdd.Checked)
            {
                chkDel.Checked = !chkAdd.Checked;
            }

            radioAddBefore.Enabled = chkAdd.Checked;
            radioAddAfter.Enabled = chkAdd.Checked;

            if (!chkAdd.Checked)
            {
                radioAddBefore.Checked = false;
                radioAddAfter.Checked = false;
            }
        }

        private void chkDel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDel.Checked)
            {
                chkAdd.Checked = !chkDel.Checked;
            }

            radioDelBefore.Enabled = chkDel.Checked;
            radioDelAfter.Enabled = chkDel.Checked;

            if (!chkDel.Checked)
            {
                radioDelBefore.Checked = false;
                radioDelAfter.Checked = false;
            }

            groupNum.Enabled = !chkDel.Checked;
            groupChar.Enabled = !chkDel.Checked;
            groupComposite.Enabled = !chkDel.Checked;
            groupSpecial.Enabled = !chkDel.Checked;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.StopRank();
            this.Close();
        }            

        #endregion

        #region  CheckData

        private bool CheckRankData()
        {
            StringBuilder sb = new StringBuilder();
            #region Gernal

            sb.AppendLine("请检查选择的数据类型是当前您数据的存储形式。");

            string filePath = txtRankFilePath.Text.Trim();
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                sb.AppendLine("数据文件不能为空或者不存在！");
            }
            if (string.IsNullOrEmpty(txtRawSplitChar.Text.Trim()))
            {
                sb.AppendLine("源始文件分隔符真的为空");
            }

            if (string.IsNullOrEmpty(txtTargetSplitChar.Text.Trim()))
            {
                sb.AppendLine("目的文件分隔符真的为空");
            }

            if ((radioAddBefore.Checked || radioAddAfter.Checked) &&
                (string.IsNullOrEmpty(txtAddIndex.Text.Trim()) || !IsInt(txtAddIndex.Text) || (GetStringToIndex(txtAddIndex.Text) < 1)))
            {
                sb.AppendLine("添加数据位置非法,最小位置为1");
            }

            if ((radioDelBefore.Checked || radioDelAfter.Checked) &&
                (string.IsNullOrEmpty(txtDelIndex.Text.Trim()) || !IsInt(txtDelIndex.Text) || (GetStringToIndex(txtDelIndex.Text) < 1)))
            {
                sb.AppendLine("删除数据位置非法，最小位置为1");
            }

            #endregion

            #region  Num

            if (string.IsNullOrEmpty(txtNum3.Text))
            {
                sb.AppendLine("纯数字 -- 长度3及以下 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtNum4.Text))
            {
                sb.AppendLine("纯数字 -- 长度4 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtNum5.Text))
            {
                sb.AppendLine("纯数字 -- 长度5 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtNum6.Text))
            {
                sb.AppendLine("纯数字 -- 长度6 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtNum7.Text))
            {
                sb.AppendLine("纯数字 -- 长度7 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtNum8.Text))
            {
                sb.AppendLine("纯数字 -- 长度8 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtNum9.Text))
            {
                sb.AppendLine("纯数字 -- 长度8以上 添加字符真要为空");
            }

            #endregion

            #region  Char


            if (string.IsNullOrEmpty(txtChar3.Text))
            {
                sb.AppendLine("纯字母 -- 长度3及以下 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtChar4.Text))
            {
                sb.AppendLine("纯字母 -- 长度4 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtChar5.Text))
            {
                sb.AppendLine("纯字母 -- 长度5 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtChar6.Text))
            {
                sb.AppendLine("纯字母 -- 长度6 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtChar7.Text))
            {
                sb.AppendLine("纯字母 -- 长度7 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtChar8.Text))
            {
                sb.AppendLine("纯字母 -- 长度8 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtChar9.Text))
            {
                sb.AppendLine("纯字母 -- 长度8以上 添加字符真要为空");
            }

            #endregion

            #region  Composite

            if (string.IsNullOrEmpty(txtComposite3.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度3及以下 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtComposite4.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度4 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtComposite5.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度5 添加字符真要为空");
            }
            if (string.IsNullOrEmpty(txtComposite6.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度6 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtComposite7.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度7 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtComposite8.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度8 添加字符真要为空");
            }

            if (string.IsNullOrEmpty(txtComposite9.Text))
            {
                sb.AppendLine("字母+数字混合 -- 长度8以上 添加字符真要为空");
            }

            #endregion

            #region  Special char

            if (string.IsNullOrEmpty(txtSpecial.Text))
            {
                sb.AppendLine(" 特殊字符 -- 添加字符真要为空");
            }

            #endregion

            if (sb.Length > 0)
            {
                DialogResult dlgResult = MessageBox.Show(sb.ToString(), "数据分离警告", MessageBoxButtons.OKCancel);
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    return false;
                }
                return dlgResult == DialogResult.OK;
            }

            return true;
        }

        #endregion

        #region Timer

        System.Windows.Forms.Timer timer = null;
        int timerCount = 0;

        private void StartTimer()
        {
            if (null == timer)
            {
                timer = new System.Windows.Forms.Timer();
                //1s
                timer.Interval = 1000;
                timer.Tick += timer_Tick;
            }

            this.StopTimer();
            timerCount = 0;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            txtTime.Text = TimeSpan.FromSeconds(timerCount).ToString();
        }

        private void StopTimer()
        {
            if ((null != timer) && timer.Enabled)
            {
                timer.Stop();
            }
        }

        #endregion

        #region  Business

        RankerFactory rankerFactory = null;
        private void StartDataRank()
        {
            if (this.CheckRankData())
            {
                this.SaveParams();
                this.SetRankButtonEabled(false);
                rankerFactory = GetRankerFactory();
                rankerFactory.Start();
                this.StartTimer();
            }
        }

        private RankerFactory GetRankerFactory()
        {
            this.GetParams();
            RankerFactory rankerFactory = new RankerFactory(txtRankFilePath.Text.Trim(),
                                                            (DataFormat)comboDataType.SelectedValue, insertParamsDic);
            rankerFactory.CurrentLineChanged += dataRanker_CurrentLineChanged;
            rankerFactory.ProcessFinished += dataRanker_ProcessFinished;
            return rankerFactory;
        }

        void dataRanker_CurrentLineChanged(object sender, ProgressEventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
                          {
                              this.progressRank.Value = e.CurrentPercent;
                              this.txtCount.Text = string.Format("第{0}条", e.CurrentLine);
                          }));
        }

        void dataRanker_ProcessFinished(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
                          {
                              this.StopRank();
                          }));

            MessageBox.Show("数据归类完成");
        }

        private void StopRank()
        {
            this.SetRankButtonEabled(true);
            this.StopTimer();
            if (null != this.rankerFactory)
            {
                rankerFactory.Stop();
                rankerFactory.CurrentLineChanged -= dataRanker_CurrentLineChanged;
                rankerFactory.ProcessFinished -= dataRanker_ProcessFinished;
                rankerFactory = null;
            }
        }

        #endregion
    }

}
