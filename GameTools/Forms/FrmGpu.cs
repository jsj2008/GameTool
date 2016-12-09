using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    public partial class FrmGpu : Form
    {
        public FrmGpu()
        {
            InitializeComponent();
            this.Load += FrmGpu_Load;
            this.FormClosing += FrmGpu_FormClosing;
        }

        void FrmGpu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Load -= FrmGpu_Load;
            this.FormClosing -= FrmGpu_FormClosing;
            this.SaveParams();
        }

        void FrmGpu_Load(object sender, EventArgs e)
        {
            this.LoadParams();
        }

        #region Load/Save params

        private void LoadParams()
        {
            IList<GPUAppItem> gpuAppList = new List<GPUAppItem>();
            gpuAppList.Add(new GPUAppItem() { Title = "Ighashgpu", GpuApp = GPUAppType.Ighashgpu });
            gpuAppList.Add(new GPUAppItem() { Title = "Egb", GpuApp = GPUAppType.Egb });
            comboApp.DataSource = gpuAppList;

            comboApp.SelectedValue = WowToolConfig.Instance.Gpu_AppType;
            this.chkSalt.Checked = WowToolConfig.Instance.GPU_IsSalt;
            this.txtTimeOut.Text = WowToolConfig.Instance.GPU_Timeout;
            this.txtCount.Text = WowToolConfig.Instance.GPU_BatchCount;
        }

        private void SaveParams()
        {
            WowToolConfig.Instance.Gpu_AppType = (GPUAppType)comboApp.SelectedValue;
            WowToolConfig.Instance.GPU_IsSalt = this.chkSalt.Checked;
            WowToolConfig.Instance.GPU_Timeout = this.txtTimeOut.Text;
            WowToolConfig.Instance.GPU_BatchCount = this.txtCount.Text;
            WowToolConfig.Instance.Save();
        }

        private GPUParams GetParams()
        {
            GPUParams gpuParam = new GPUParams();
            gpuParam.AppPath = txtApp.Text;
            gpuParam.DataPath = txtData.Text;
            gpuParam.IsSalt = chkSalt.Checked;
            gpuParam.GpuApp = (GPUAppType)this.comboApp.SelectedValue;
            gpuParam.Timeout = Convert.ToInt32(txtTimeOut.Text);
            gpuParam.BatchCount = Convert.ToInt32(txtCount.Text);
            return gpuParam;
        }

        private bool CheckParams()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(txtApp.Text) || !File.Exists(txtApp.Text))
            {
                sb.AppendLine("GPU破解程序无效或者不存在！");
            }

            if (string.IsNullOrEmpty(txtData.Text) || !File.Exists(txtData.Text))
            {
                sb.AppendLine("Hash数据文件无效或者不存在");
            }

            if (string.IsNullOrEmpty(txtCount.Text) || !TextHelper.IsNumber(txtCount.Text))
            {
                sb.AppendLine("批量处理个数非法！");
            }

            if ((chkSalt.Checked) && (Convert.ToInt32(txtCount.Text) <= 0 || Convert.ToInt32(txtCount.Text) > 512))
            {
                sb.AppendLine("批量处理个数只能在 1 - 512 范围内！");
            }

            if (string.IsNullOrEmpty(txtTimeOut.Text) || !TextHelper.IsNumber(txtTimeOut.Text))
            {
                sb.AppendLine("超时时间非法！");
            }

            if ((chkSalt.Checked) && (Convert.ToInt32(txtTimeOut.Text) <= 0))
            {
                sb.AppendLine("超时时间必须大于0！");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return false;
            }
            return true;
        }

        #endregion

        #region UI Event

        private void btnOpenApp_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择正确的GPU破解程序所在的路径！");
                return;
            }

            txtApp.Text = filePath;
        }

        private string GetFilePath()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;
            openDlg.ShowDialog();
            return openDlg.FileName;
        }

        private void btnOpenData_Click(object sender, EventArgs e)
        {
            string filePath = GetFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择正确的要破解Hash文件所在的路径！");
                return;
            }

            txtData.Text = filePath;
        }

        private void chkSalt_CheckedChanged(object sender, EventArgs e)
        {
            txtCount.Enabled = this.chkSalt.Checked;
            txtTimeOut.Enabled = this.chkSalt.Checked;
        }

        #endregion

        private GPUCrackHelper cracker = null;
        private void btnCrack_Click(object sender, EventArgs e)
        {
            if (this.CheckParams())
            {
                this.SetButtonState(false);
                GPUParams gpuParams = this.GetParams();
                //Md5MD5PassSaltHepler md5Helper = new Md5MD5PassSaltHepler();
                //md5Helper.Start(gpuParams.DataPath);
                cracker = new GPUCrackHelper(gpuParams);
                cracker.Start();
            }
        }

        private void SetButtonState(bool isStarted)
        {
            this.btnCrack.Enabled = isStarted;
            this.btnStop.Enabled = !isStarted;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(null != this.cracker)
            {
                this.cracker.Stop();
            }
            this.SetButtonState(true);
        }
    }

    public class GPUParams
    {
        public string AppPath
        {
            get;
            set;
        }

        public string DataPath
        {
            get;
            set;
        }

        public bool IsSalt
        {
            get;
            set;
        }

        public int BatchCount
        {
            get;
            set;
        }

        public int Timeout
        {
            get;
            set;
        }

        public GPUAppType GpuApp
        {
            get;
            set;
        }
    }

}
