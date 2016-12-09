using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    public partial class FrmTranslate : Form
    {
        public FrmTranslate()
        {
            InitializeComponent();
        }

        #region  UI Event

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GetParams();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            txtMode1Before.ReadOnly = !cbMode1AddBefore.Checked;
            txtMode1After.ReadOnly = !cbMode1AddAfter.Checked;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.ShowDialog();
            this.txtFile.Text = openDlg.FileName;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.SetButtonsEnabled(true);
            this.StopTranslate();
        }

        private void radioMode3Del_CheckedChanged(object sender, EventArgs e)
        {
            txtMode3AddChar.ReadOnly = radioMode3Del.Checked;
            this.txtMode3AddChar.Text = string.Empty;
        }

        private void SetButtonsEnabled(bool isStarted)
        {
            this.btnStart.Enabled = isStarted;
            this.btnStop.Enabled = !isStarted;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text == "")
            {
                MessageBox.Show("请先选择文件！");
            }
            else if (!this.radioMode1.Checked && !this.radioMode2.Checked && !this.radioMode3.Checked)
            {
                MessageBox.Show("请选择模式！");
            }
            else if (this.radioMode3.Checked && this.radioMode3Insert.Checked &&
                !this.CheckIsAllNumber(this.txtMode3Index.Text))
            {
                MessageBox.Show("位置必须为整数！");
            }
            else
            {
                this.SetButtonsEnabled(false);
                this.SaveParams();
                this.StartTranslate();
            }
        }

        #endregion

        #region Load / Save Params

        private void GetParams()
        {
            switch (WowToolConfig.Instance.SelectedMode)
            {
                case 1:
                    this.radioMode1.Checked = true; break;
                case 2:
                    this.radioMode2.Checked = true; break;
                case 3:
                    this.radioMode3.Checked = true; break;
                default:
                    this.radioMode1.Checked = true; break;
            }

            this.txtMode1AddChar.Text = WowToolConfig.Instance.Mode1AddChars;
            this.cbMode1AddBefore.Checked = WowToolConfig.Instance.Mode1IsAddBefore;
            this.txtMode1Before.Text = WowToolConfig.Instance.Mode1AddBeforeChars;
            this.cbMode1AddAfter.Checked = WowToolConfig.Instance.Mode1IsAddAfter;
            this.txtMode1After.Text = WowToolConfig.Instance.Mode1AddAfterChars;

            this.txtMode2AddChar.Text = WowToolConfig.Instance.Mode2AddChars;

            this.radioMode3Insert.Checked = WowToolConfig.Instance.Mode3IsAdd;
            this.radioMode3Del.Checked = WowToolConfig.Instance.Mode3IsDelete;
            this.txtMode3Index.Text = WowToolConfig.Instance.Mode3Index;
            this.txtMode3AddChar.Text = WowToolConfig.Instance.Mode3AddChars;

            this.txtMode1Before.ReadOnly = !this.cbMode1AddBefore.Checked;
            this.txtMode1After.ReadOnly = !this.cbMode1AddAfter.Checked;
        }

        private void SaveParams()
        {
            if (this.radioMode1.Checked)
            {
                WowToolConfig.Instance.SelectedMode = 1;
            }
            if (this.radioMode2.Checked)
            {
                WowToolConfig.Instance.SelectedMode = 2;
            }

            if (this.radioMode3.Checked)
            {
                WowToolConfig.Instance.SelectedMode = 3;
            }

            WowToolConfig.Instance.Mode1AddChars = this.txtMode1AddChar.Text;
            WowToolConfig.Instance.Mode1IsAddBefore = this.cbMode1AddBefore.Checked;
            WowToolConfig.Instance.Mode1IsAddAfter = this.cbMode1AddAfter.Checked;
            WowToolConfig.Instance.Mode1AddBeforeChars = this.txtMode1Before.Text;
            WowToolConfig.Instance.Mode1AddAfterChars = this.txtMode1After.Text;

            WowToolConfig.Instance.Mode2AddChars = this.txtMode2AddChar.Text;

            WowToolConfig.Instance.Mode3IsAdd = this.radioMode3Insert.Checked;
            WowToolConfig.Instance.Mode3IsDelete = this.radioMode3Del.Checked;

            WowToolConfig.Instance.Mode3AddChars = this.txtMode3AddChar.Text;
            WowToolConfig.Instance.Mode3Index = this.txtMode3Index.Text;

            WowToolConfig.Instance.Save();
        }

        #endregion

        #region  Business

        private bool CheckIsAllNumber(string lineStr)
        {
            bool b = Regex.IsMatch(lineStr, @"^\d+$");
            return b;
        }

        private bool IsNumber(char c)
        {
            if (c >= 0x30 && c <= 0x39)
            {
                return true;
            }
            return false;
        }

        TranslateFactory translateFactory = null;
        private void StartTranslate()
        {
            translateFactory = new TranslateFactory(this.GetTranslateParams());
            translateFactory.CurrentLineChanged += translateFactory_CurrentLineChanged;
            translateFactory.ProcessFinished += translateFactory_ProcessFinished;
            translateFactory.Start();
            this.StartTimer();
        }

        void translateFactory_ProcessFinished(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                this.StopTranslate();
                MessageBox.Show("数据处理完成");
            }));
        }

        void translateFactory_CurrentLineChanged(object sender, ProgressEventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                this.progressBar1.Value = e.CurrentPercent;
                this.txtCount.Text = string.Format("第{0}条", e.CurrentLine);
            }));
        }

        private TranslateParams GetTranslateParams()
        {
            TranslateParams translateParams = new TranslateParams();
            translateParams.FilePath = this.txtFile.Text;
            translateParams.ModeOneParams = new ModeOneParams()
            {
                IsAddBefore = this.cbMode1AddBefore.Checked,
                IsAddAfter = this.cbMode1AddAfter.Checked,
                AddBeforeChars = this.txtMode1Before.Text,
                AddAfterChars = this.txtMode1After.Text,
                AddChars = this.txtMode1AddChar.Text
            };

            translateParams.ModeTwoParams = new ModeTwoParams()
            {
                AddChars = this.txtMode2AddChar.Text
            };

            translateParams.ModeThreeParams = new ModeThreeParams()
            {
                IsInsert = this.radioMode3Insert.Checked,
                IsDel = this.radioMode3Del.Checked,
                Index = Convert.ToInt32(this.txtMode3Index.Text) - 1,
                AddChars = this.txtMode3AddChar.Text
            };

            if (this.radioMode1.Checked)
            {
                translateParams.CurrentMode = TranslateMode.One;
            }

            if (this.radioMode2.Checked)
            {
                translateParams.CurrentMode = TranslateMode.Two;
            }

            if (this.radioMode3.Checked)
            {
                translateParams.CurrentMode = TranslateMode.Three;
            }

            return translateParams;
        }

        private void StopTranslate()
        {
            this.StopTimer();
            this.SetButtonsEnabled(true);
            if (null != translateFactory)
            {
                translateFactory.Stop();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.StopTranslate();
            this.Close();
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
        
        #region old logical

        //private int getFileLines(string FileName)
        //{
        //    int num = 0;
        //    using (StreamReader reader = File.OpenText(FileName))
        //    {
        //        while (reader.Peek() > -1)
        //        {
        //            reader.ReadLine();
        //            num++;
        //        }
        //    }
        //    return num;
        //}



        //private void Mode1(string lineStr)
        //{
        //    if (this.CheckIsAllNumber(lineStr))
        //    {
        //        if (this.cbAddBefore.Checked)
        //        {
        //            lineStr = this.txtBefore.Text + lineStr;
        //        }
        //        if (this.cbAddAfter.Checked)
        //        {
        //            lineStr = lineStr + this.cbAddAfter.Text;
        //        }
        //        sb.Append(lineStr);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            if (lineStr.Length <= 0) return;

        //            char c = lineStr[lineStr.Length - 1];
        //            if (this.isNumber(c))
        //            {
        //                int num = Convert.ToInt32(c.ToString()) + 1;
        //                if (num == 10)
        //                {
        //                    num = 0;
        //                }
        //                string str = lineStr.Substring(0, lineStr.Length - 1) + num.ToString();
        //                sb.AppendLine(str);
        //            }
        //            else
        //            {
        //                sb.AppendLine(string.Format("{0}{1}", lineStr, this.txtMode1AddChar.Text));
        //            }
        //        }
        //        catch
        //        {
        //            sb.AppendLine("非ascii密码-->>>" + lineStr);
        //        }
        //    }
        //}

        //private void Mode2(string LineStr)
        //{
        //    sb.AppendLine(string.Format("{0}{1}", LineStr, this.txtMode2AddChar.Text));
        //}

        //StringBuilder sb;
        //private void StartReplace(string oldFile, string newFile)
        //{
        //    sb = new StringBuilder();
        //    this.progressBar1.Maximum = this.getFileLines(oldFile);
        //    this.progressBar1.Value = 0;
        //    using (StreamReader reader = File.OpenText(oldFile))
        //    {
        //        while (reader.Peek() > -1)
        //        {
        //            string lineStr = reader.ReadLine().Trim();
        //            if (this.radioMode1.Checked)
        //            {
        //                this.Mode1(lineStr);
        //            }
        //            else
        //            {
        //                this.Mode2(lineStr);
        //            }
        //            this.progressBar1.Value++;
        //            Application.DoEvents();
        //        }
        //        using (StreamWriter writer = File.CreateText(newFile))
        //        {
        //            writer.Write(sb.ToString());
        //            writer.Flush();
        //        }
        //    }
        //    MessageBox.Show("处理完毕！");
        //}

        #endregion
    }

    public class TranslateParams
    {

        public string FilePath
        {
            get;
            set;
        }
        public TranslateMode CurrentMode
        {
            get;
            set;
        }

        public ModeOneParams ModeOneParams
        {
            get;
            set;
        }
        public ModeTwoParams ModeTwoParams
        {
            get;
            set;
        }
        public ModeThreeParams ModeThreeParams
        {
            get;
            set;
        }
    }

    public class ModeOneParams
    {
        public bool IsAddBefore
        {
            get;
            set;
        }

        public string AddBeforeChars
        {
            get;
            set;
        }

        public bool IsAddAfter
        {
            get;
            set;
        }

        public string AddAfterChars
        {
            get;
            set;
        }

        public string AddChars
        {
            get;
            set;
        }
    }

    public class ModeTwoParams
    {
        public string AddChars
        {
            get;
            set;
        }
    }

    public class ModeThreeParams
    {
        public bool IsInsert
        {
            get;
            set;
        }

        public bool IsDel
        {
            get;
            set;
        }
        public int Index
        { get; set; }

        public string AddChars
        {
            get;
            set;
        }
    }
}
