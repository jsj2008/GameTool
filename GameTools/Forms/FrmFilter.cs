using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    public partial class FrmFilter : Form
    {
        public FrmFilter()
        {
            InitializeComponent();
        }

        DataFilter dataFilter = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.CheckParams())
            {
                DataFilterParam filterParam = GetParams();
                dataFilter = new DataFilter(filterParam);
                dataFilter.CurrentLineChanged += dataFilter_CurrentLineChanged;
                dataFilter.ProcessFinished += dataFilter_ProcessFinished;
                dataFilter.Start();
                ucProgress.StartTimer();
            }
        }

        void dataFilter_ProcessFinished(object sender, EventArgs e)
        {
            dataFilter.Stop();
            dataFilter.CurrentLineChanged -= dataFilter_CurrentLineChanged;
            dataFilter.ProcessFinished -= dataFilter_ProcessFinished;
            dataFilter = null;

            ucProgress.StopTimer();
            MessageBox.Show("数据归类完成");
        }

        void dataFilter_CurrentLineChanged(object sender, ProgressEventArgs e)
        {
            ucProgress.SetCount(e);
        }

        private DataFilterParam GetParams()
        {
            DataFilterParam filterParam = new DataFilterParam();
            filterParam.FilePath = ucOpenFile.FilePath;
            filterParam.IsFilterChar = chkChar.Checked;
            filterParam.IsFilterNum = chkNum.Checked;

            return filterParam;
        }

        private bool CheckParams()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(ucOpenFile.FilePath) || !File.Exists(ucOpenFile.FilePath))
            {
                sb.AppendLine("请选择有效的数据文件！");
            }
            if (!chkChar.Checked && !chkNum.Checked)
            {
                sb.AppendLine("字母或者数字过滤至少选择一种！");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return false;
            }

            return true;
        }
    }
}
