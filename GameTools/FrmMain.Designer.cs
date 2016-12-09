namespace WowTools
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRanker = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGPU = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRift = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRS = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPwdReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHorzitional = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.wowBtn = new System.Windows.Forms.ToolStripButton();
            this.riftBtn = new System.Windows.Forms.ToolStripButton();
            this.rsBtn = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpen,
            this.toolStripMenuItem1,
            this.查询ToolStripMenuItem,
            this.menuDisplay});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1112, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuOpen
            // 
            this.menuOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInsert,
            this.menuRanker,
            this.menuFilter});
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(44, 21);
            this.menuOpen.Text = "数据";
            // 
            // menuInsert
            // 
            this.menuInsert.Name = "menuInsert";
            this.menuInsert.Size = new System.Drawing.Size(136, 22);
            this.menuInsert.Text = "数据插入";
            this.menuInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // menuRanker
            // 
            this.menuRanker.Name = "menuRanker";
            this.menuRanker.Size = new System.Drawing.Size(136, 22);
            this.menuRanker.Text = "数据格式化";
            this.menuRanker.Click += new System.EventHandler(this.btnRank_Click);
            // 
            // menuFilter
            // 
            this.menuFilter.Name = "menuFilter";
            this.menuFilter.Size = new System.Drawing.Size(136, 22);
            this.menuFilter.Text = "数据过滤";
            this.menuFilter.Click += new System.EventHandler(this.menuFilter_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGPU});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "破解";
            // 
            // menuGPU
            // 
            this.menuGPU.Name = "menuGPU";
            this.menuGPU.Size = new System.Drawing.Size(101, 22);
            this.menuGPU.Text = "GPU";
            this.menuGPU.Click += new System.EventHandler(this.menuGPU_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWow,
            this.menuRift,
            this.menuRS,
            this.menuPwdReset});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.查询ToolStripMenuItem.Text = "对号";
            // 
            // menuWow
            // 
            this.menuWow.Name = "menuWow";
            this.menuWow.Size = new System.Drawing.Size(124, 22);
            this.menuWow.Text = "战网对号";
            this.menuWow.Click += new System.EventHandler(this.menuWow_Click);
            // 
            // menuRift
            // 
            this.menuRift.Name = "menuRift";
            this.menuRift.Size = new System.Drawing.Size(124, 22);
            this.menuRift.Text = "裂隙对号";
            this.menuRift.Click += new System.EventHandler(this.menuRift_Click);
            // 
            // menuRS
            // 
            this.menuRS.Name = "menuRS";
            this.menuRS.Size = new System.Drawing.Size(124, 22);
            this.menuRS.Text = "江湖对号";
            this.menuRS.Click += new System.EventHandler(this.menuRS_Click);
            // 
            // menuPwdReset
            // 
            this.menuPwdReset.Enabled = false;
            this.menuPwdReset.Name = "menuPwdReset";
            this.menuPwdReset.Size = new System.Drawing.Size(124, 22);
            this.menuPwdReset.Text = "破宝对号";
            this.menuPwdReset.Click += new System.EventHandler(this.menuPwdReset_Click);
            // 
            // menuDisplay
            // 
            this.menuDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCascade,
            this.menuHorzitional,
            this.menuVertical});
            this.menuDisplay.Name = "menuDisplay";
            this.menuDisplay.Size = new System.Drawing.Size(44, 21);
            this.menuDisplay.Text = "显示";
            // 
            // menuCascade
            // 
            this.menuCascade.Name = "menuCascade";
            this.menuCascade.Size = new System.Drawing.Size(100, 22);
            this.menuCascade.Text = "层叠";
            this.menuCascade.Click += new System.EventHandler(this.menuCascade_Click);
            // 
            // menuHorzitional
            // 
            this.menuHorzitional.Name = "menuHorzitional";
            this.menuHorzitional.Size = new System.Drawing.Size(100, 22);
            this.menuHorzitional.Text = "水平";
            this.menuHorzitional.Click += new System.EventHandler(this.menuHorzitional_Click);
            // 
            // menuVertical
            // 
            this.menuVertical.Name = "menuVertical";
            this.menuVertical.Size = new System.Drawing.Size(100, 22);
            this.menuVertical.Text = "垂直";
            this.menuVertical.Click += new System.EventHandler(this.menuVertical_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wowBtn,
            this.riftBtn,
            this.rsBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1112, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // wowBtn
            // 
            this.wowBtn.Checked = true;
            this.wowBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wowBtn.Image = ((System.Drawing.Image)(resources.GetObject("wowBtn.Image")));
            this.wowBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wowBtn.Name = "wowBtn";
            this.wowBtn.Size = new System.Drawing.Size(52, 22);
            this.wowBtn.Text = "魔兽";
            this.wowBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.wowBtn.ToolTipText = "魔兽";
            this.wowBtn.Click += new System.EventHandler(this.wowBtn_Click);
            // 
            // riftBtn
            // 
            this.riftBtn.Image = ((System.Drawing.Image)(resources.GetObject("riftBtn.Image")));
            this.riftBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.riftBtn.Name = "riftBtn";
            this.riftBtn.Size = new System.Drawing.Size(52, 22);
            this.riftBtn.Text = "裂隙";
            this.riftBtn.Click += new System.EventHandler(this.riftBtn_Click);
            // 
            // rsBtn
            // 
            this.rsBtn.Image = ((System.Drawing.Image)(resources.GetObject("rsBtn.Image")));
            this.rsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rsBtn.Name = "rsBtn";
            this.rsBtn.Size = new System.Drawing.Size(52, 22);
            this.rsBtn.Text = "江湖";
            this.rsBtn.Click += new System.EventHandler(this.rsBtn_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 579);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "WOW工具";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuInsert;
        private System.Windows.Forms.ToolStripMenuItem menuRanker;
        private System.Windows.Forms.ToolStripMenuItem menuDisplay;
        private System.Windows.Forms.ToolStripMenuItem menuCascade;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuGPU;
        private System.Windows.Forms.ToolStripMenuItem menuFilter;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuWow;
        private System.Windows.Forms.ToolStripMenuItem menuHorzitional;
        private System.Windows.Forms.ToolStripMenuItem menuVertical;
        private System.Windows.Forms.ToolStripMenuItem menuPwdReset;
        private System.Windows.Forms.ToolStripMenuItem menuRift;
        private System.Windows.Forms.ToolStripMenuItem menuRS;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton wowBtn;
        private System.Windows.Forms.ToolStripButton riftBtn;
        private System.Windows.Forms.ToolStripButton rsBtn;

    }
}