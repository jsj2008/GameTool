using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace HashFormat
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();             
            this.Load += new EventHandler(FrmMain_Load);
        }

        void FrmMain_Load(object sender, EventArgs e)
        {
            this.ta.TabPages.Remove(tabAdd);
            this.ta.TabPages.Remove(tabColumn);
            this.ta.TabPages.Remove(tabMerge);
        }

        private string GetFolder()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            return folderDlg.SelectedPath;
        }

        #region Add hash for content 

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            string selectFolder = GetFolder();
            if (!string.IsNullOrEmpty(selectFolder))
            {
                this.txtFolder.Text = selectFolder;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedFolder = txtFolder.Text;
            string hashValue = txtHash.Text;

            if (string.IsNullOrEmpty(selectedFolder))
            {
                MessageBox.Show("请选择有效目录");
                return;
            }

            if (string.IsNullOrEmpty(hashValue))
            {
                MessageBox.Show("当前Hash值为空");
                return;
            }

            Thread hashThread = new Thread(StartHashAdd);
            hashThread.Start();
        }

        HashAdder hashAdder = null;
        private void StartHashAdd()
        {
            string selectedFolder = txtFolder.Text;
            string hashValue = txtHash.Text;
            hashAdder = new HashAdder(selectedFolder, hashValue);
            hashAdder.ProcessStateChanged += hashAdder_ProcessStateChanged;
            hashAdder.Run();
        }

        void hashAdder_ProcessStateChanged(object sender, ProcessStateEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            switch (e.CurrentState)
            {
                case ProcessState.Started:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        btnAdd.Enabled = false;
                        progressAdd.Style = ProgressBarStyle.Marquee;
                    }));
                    break;
                case ProcessState.Processing:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        txtCurrentFile.Text = e.CurrentFile;
                    }));
                    break;
                case ProcessState.Completed:
                    this.BeginInvoke(new ThreadStart(delegate()
                                      {
                                          progressAdd.Style = ProgressBarStyle.Blocks;
                                          MessageBox.Show(this, "Hash值添加完成");
                                          btnAdd.Enabled = true;
                                          hashAdder.ProcessStateChanged -= hashAdder_ProcessStateChanged;                                          
                                      }));
                    break;
                default:
                    break;
            }
        }

        #endregion       

        #region Get colume from content

        private void btnColumnFolder_Click(object sender, EventArgs e)
        {
            string selectFolder = GetFolder();
            if (!string.IsNullOrEmpty(selectFolder))
            {
                this.txtColumnFolder.Text = selectFolder;
            }
        }

        private void btnColumnExtract_Click(object sender, EventArgs e)
        {
            string selectedFolder = txtColumnFolder.Text;
            string columns = txtColumn.Text;

            if (string.IsNullOrEmpty(selectedFolder))
            {
                MessageBox.Show("请选择有效目录");
                return;
            }

            if (string.IsNullOrEmpty(columns))
            {
                MessageBox.Show("当前选取列名为空");
                return;
            }

            Thread columnThread = new Thread(StartColumesExtract);
            columnThread.Start();
        }

        ColumnExtractor columnExtractor = null;
        private void StartColumesExtract()
        {
            string selectedFolder = txtColumnFolder.Text;
            string columns =txtColumn.Text;
            columnExtractor = new ColumnExtractor(selectedFolder, columns);
            columnExtractor.ProcessStateChanged += columnExtractor_ProcessStateChanged;
            columnExtractor.Run();
        }

        void columnExtractor_ProcessStateChanged(object sender, ProcessStateEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            switch (e.CurrentState)
            {
                case ProcessState.Started:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        btnColumnExtract.Enabled = false;
                        pbColumn.Style = ProgressBarStyle.Marquee;
                    }));
                    break;
                case ProcessState.Processing:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        txtColumeCurrentFile.Text = e.CurrentFile;
                    }));
                    break;
                case ProcessState.Completed:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        pbColumn.Style = ProgressBarStyle.Blocks;
                        MessageBox.Show(this, "列提取完成");
                        btnColumnExtract.Enabled = true;
                        columnExtractor.ProcessStateChanged -= columnExtractor_ProcessStateChanged;
                    }));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Merge

        private void btnMergeFolder_Click(object sender, EventArgs e)
        {
            string selectFolder = GetFolder();
            if (!string.IsNullOrEmpty(selectFolder))
            {
                this.txtMergeFolder.Text = selectFolder;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            string selectedFolder = txtMergeFolder.Text;

            if (string.IsNullOrEmpty(selectedFolder))
            {
                MessageBox.Show("请选择有效目录");
                return;
            }

            Thread columnThread = new Thread(StartHashMerge);
            columnThread.Start();
        }

        HashMerge hashMerge = null;
        private void StartHashMerge()
        {
            string selectedFolder = txtMergeFolder.Text;
            hashMerge = new HashMerge(selectedFolder);
            hashMerge.ProcessStateChanged += hashMerge_ProcessStateChanged;
            hashMerge.Run();
        }

        void hashMerge_ProcessStateChanged(object sender, ProcessStateEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            switch (e.CurrentState)
            {
                case ProcessState.Started:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        btnMerge.Enabled = false;
                        pbMerge.Style = ProgressBarStyle.Marquee;
                    }));
                    break;
                case ProcessState.Processing:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        txtMergeFile.Text = e.CurrentFile;
                    }));
                    break;
                case ProcessState.Completed:
                    this.BeginInvoke(new ThreadStart(delegate()
                    {
                        pbMerge.Style = ProgressBarStyle.Blocks;
                        MessageBox.Show(this, "合并去重完成");
                        btnMerge.Enabled = true;
                        hashMerge.ProcessStateChanged -= columnExtractor_ProcessStateChanged;
                    }));
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Merge By 6

        private void btnOpenMergeBy6_Click(object sender, EventArgs e)
        {
            string selectFolder = GetFolder();
            if (!string.IsNullOrEmpty(selectFolder))
            {
                this.txtFolder.Text = selectFolder;
            }
        }

        private void btnMergeBy6_Click(object sender, EventArgs e)
        {
            string selectedFolder = txtFolerMerge6.Text;
            string strInterval = txtInterval.Text;

            if (string.IsNullOrEmpty(selectedFolder))
            {
                MessageBox.Show("请选择有效目录");
                return;
            }
            int intervalVal = 0;
            bool isInt=int.TryParse(strInterval, out intervalVal);
            if (!isInt)
            {
                MessageBox.Show("当前数据间隔不是有效值");
                return;
            }

            Thread hashThread = new Thread(StartHashAdd);
            hashThread.Start();
        }

        #endregion

       

    }
}
