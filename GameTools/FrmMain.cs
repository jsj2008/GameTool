using System;
using System.Windows.Forms;
using WebDetection;
//using PwdReset;
using RSTool;
using RiftTool;
using System.Threading;

namespace WowTools
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadDefaultForm();
        }

        #region Methods

        private void LoadDefaultForm()
        {
            this.ShowForm<FrmWebDetection>(ref frmWebDetection);
        }

        private void ShowForm<T>(ref T frm) where T : Form, new()
        {
            if ((null == frm) || frm.IsDisposed)
            {
                frm = new T();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Width = this.Width - 10;
                frm.Height = this.Height - 20;
                frm.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        #endregion Methods

        #region Menu Events

        FrmTranslate frmTranslate = null;

        private void btnInsert_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmTranslate>(ref frmTranslate);
        }

        private void btnRank_Click(object sender, EventArgs e)
        {
            LoadDefaultForm();
        }

        FrmGpu frmGpu = null;

        private void menuGPU_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmGpu>(ref frmGpu);
        }

        FrmFilter frmFilter = null;

        private void menuFilter_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmFilter>(ref frmFilter);
        }

        FrmWebDetection frmWebDetection = null;

        private void menuWow_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmWebDetection>(ref frmWebDetection);
        }

        FrmRS frmRs = null;
        private void menuRS_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmRS>(ref frmRs);
        }

        FrmRift frmRift = null;
        private void menuRift_Click(object sender, EventArgs e)
        {
            this.ShowForm<FrmRift>(ref frmRift);
        }

        //FrmPwdReset frmPwdReset = null;
        private void menuPwdReset_Click(object sender, EventArgs e)
        {
            // this.ShowForm<FrmPwdReset>(ref frmPwdReset);
        }

        private void menuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void menuHorzitional_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        #endregion Menu Events

        private void wowBtn_Click(object sender, EventArgs e)
        {
            this.menuWow_Click(null, null);
        }

        private void riftBtn_Click(object sender, EventArgs e)
        {
            this.menuRift_Click(null, null);
        }

        private void rsBtn_Click(object sender, EventArgs e)
        {
            this.menuRS_Click(null, null);
        }

    }
}