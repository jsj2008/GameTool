namespace HashFormat
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
            this.ta = new System.Windows.Forms.TabControl();
            this.tabAdd = new System.Windows.Forms.TabPage();
            this.progressAdd = new System.Windows.Forms.ProgressBar();
            this.txtCurrentFile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabColumn = new System.Windows.Forms.TabPage();
            this.pbColumn = new System.Windows.Forms.ProgressBar();
            this.txtColumeCurrentFile = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnColumnExtract = new System.Windows.Forms.Button();
            this.txtColumn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnColumnFolder = new System.Windows.Forms.Button();
            this.txtColumnFolder = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabMerge = new System.Windows.Forms.TabPage();
            this.pbMerge = new System.Windows.Forms.ProgressBar();
            this.txtMergeFile = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnMergeFolder = new System.Windows.Forms.Button();
            this.txtMergeFolder = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabMergeBy6 = new System.Windows.Forms.TabPage();
            this.progressMergeBy6 = new System.Windows.Forms.ProgressBar();
            this.labMergeBy6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnOpenMergeBy6 = new System.Windows.Forms.Button();
            this.txtFolerMerge6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnMergeBy6 = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ta.SuspendLayout();
            this.tabAdd.SuspendLayout();
            this.tabColumn.SuspendLayout();
            this.tabMerge.SuspendLayout();
            this.tabMergeBy6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ta
            // 
            this.ta.Controls.Add(this.tabAdd);
            this.ta.Controls.Add(this.tabColumn);
            this.ta.Controls.Add(this.tabMerge);
            this.ta.Controls.Add(this.tabMergeBy6);
            this.ta.Location = new System.Drawing.Point(2, 0);
            this.ta.Name = "ta";
            this.ta.SelectedIndex = 0;
            this.ta.Size = new System.Drawing.Size(768, 383);
            this.ta.TabIndex = 16;
            // 
            // tabAdd
            // 
            this.tabAdd.Controls.Add(this.progressAdd);
            this.tabAdd.Controls.Add(this.txtCurrentFile);
            this.tabAdd.Controls.Add(this.label3);
            this.tabAdd.Controls.Add(this.btnAdd);
            this.tabAdd.Controls.Add(this.txtHash);
            this.tabAdd.Controls.Add(this.label1);
            this.tabAdd.Controls.Add(this.btnSelectFolder);
            this.tabAdd.Controls.Add(this.txtFolder);
            this.tabAdd.Controls.Add(this.label2);
            this.tabAdd.Location = new System.Drawing.Point(4, 22);
            this.tabAdd.Name = "tabAdd";
            this.tabAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdd.Size = new System.Drawing.Size(760, 357);
            this.tabAdd.TabIndex = 0;
            this.tabAdd.Text = "添加Hash值";
            this.tabAdd.UseVisualStyleBackColor = true;
            // 
            // progressAdd
            // 
            this.progressAdd.Location = new System.Drawing.Point(79, 144);
            this.progressAdd.Name = "progressAdd";
            this.progressAdd.Size = new System.Drawing.Size(430, 23);
            this.progressAdd.TabIndex = 24;
            // 
            // txtCurrentFile
            // 
            this.txtCurrentFile.AutoSize = true;
            this.txtCurrentFile.Location = new System.Drawing.Point(77, 114);
            this.txtCurrentFile.Name = "txtCurrentFile";
            this.txtCurrentFile.Size = new System.Drawing.Size(23, 12);
            this.txtCurrentFile.TabIndex = 23;
            this.txtCurrentFile.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "当前文件 ：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(544, 67);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "转换";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(79, 67);
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(430, 21);
            this.txtHash.TabIndex = 20;
            this.txtHash.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "Hash值：";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(544, 23);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolder.TabIndex = 18;
            this.btnSelectFolder.Text = "选择";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(79, 25);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(430, 21);
            this.txtFolder.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "文件夹：";
            // 
            // tabColumn
            // 
            this.tabColumn.Controls.Add(this.pbColumn);
            this.tabColumn.Controls.Add(this.txtColumeCurrentFile);
            this.tabColumn.Controls.Add(this.label5);
            this.tabColumn.Controls.Add(this.btnColumnExtract);
            this.tabColumn.Controls.Add(this.txtColumn);
            this.tabColumn.Controls.Add(this.label6);
            this.tabColumn.Controls.Add(this.btnColumnFolder);
            this.tabColumn.Controls.Add(this.txtColumnFolder);
            this.tabColumn.Controls.Add(this.label7);
            this.tabColumn.Location = new System.Drawing.Point(4, 22);
            this.tabColumn.Name = "tabColumn";
            this.tabColumn.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumn.Size = new System.Drawing.Size(760, 357);
            this.tabColumn.TabIndex = 1;
            this.tabColumn.Text = "列提取";
            this.tabColumn.UseVisualStyleBackColor = true;
            // 
            // pbColumn
            // 
            this.pbColumn.Location = new System.Drawing.Point(87, 149);
            this.pbColumn.Name = "pbColumn";
            this.pbColumn.Size = new System.Drawing.Size(430, 23);
            this.pbColumn.TabIndex = 33;
            // 
            // txtColumeCurrentFile
            // 
            this.txtColumeCurrentFile.AutoSize = true;
            this.txtColumeCurrentFile.Location = new System.Drawing.Point(85, 119);
            this.txtColumeCurrentFile.Name = "txtColumeCurrentFile";
            this.txtColumeCurrentFile.Size = new System.Drawing.Size(23, 12);
            this.txtColumeCurrentFile.TabIndex = 32;
            this.txtColumeCurrentFile.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "当前文件 ：";
            // 
            // btnColumnExtract
            // 
            this.btnColumnExtract.Location = new System.Drawing.Point(552, 72);
            this.btnColumnExtract.Name = "btnColumnExtract";
            this.btnColumnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnColumnExtract.TabIndex = 30;
            this.btnColumnExtract.Text = "提取";
            this.btnColumnExtract.UseVisualStyleBackColor = true;
            this.btnColumnExtract.Click += new System.EventHandler(this.btnColumnExtract_Click);
            // 
            // txtColumn
            // 
            this.txtColumn.Location = new System.Drawing.Point(87, 72);
            this.txtColumn.Name = "txtColumn";
            this.txtColumn.Size = new System.Drawing.Size(430, 21);
            this.txtColumn.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 28;
            this.label6.Text = "列设置：";
            // 
            // btnColumnFolder
            // 
            this.btnColumnFolder.Location = new System.Drawing.Point(552, 28);
            this.btnColumnFolder.Name = "btnColumnFolder";
            this.btnColumnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnColumnFolder.TabIndex = 27;
            this.btnColumnFolder.Text = "选择";
            this.btnColumnFolder.UseVisualStyleBackColor = true;
            this.btnColumnFolder.Click += new System.EventHandler(this.btnColumnFolder_Click);
            // 
            // txtColumnFolder
            // 
            this.txtColumnFolder.Location = new System.Drawing.Point(87, 30);
            this.txtColumnFolder.Name = "txtColumnFolder";
            this.txtColumnFolder.Size = new System.Drawing.Size(430, 21);
            this.txtColumnFolder.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "文件夹：";
            // 
            // tabMerge
            // 
            this.tabMerge.Controls.Add(this.pbMerge);
            this.tabMerge.Controls.Add(this.txtMergeFile);
            this.tabMerge.Controls.Add(this.label8);
            this.tabMerge.Controls.Add(this.btnMerge);
            this.tabMerge.Controls.Add(this.btnMergeFolder);
            this.tabMerge.Controls.Add(this.txtMergeFolder);
            this.tabMerge.Controls.Add(this.label10);
            this.tabMerge.Location = new System.Drawing.Point(4, 22);
            this.tabMerge.Name = "tabMerge";
            this.tabMerge.Padding = new System.Windows.Forms.Padding(3);
            this.tabMerge.Size = new System.Drawing.Size(760, 357);
            this.tabMerge.TabIndex = 2;
            this.tabMerge.Text = "合并去重";
            this.tabMerge.UseVisualStyleBackColor = true;
            // 
            // pbMerge
            // 
            this.pbMerge.Location = new System.Drawing.Point(82, 89);
            this.pbMerge.Name = "pbMerge";
            this.pbMerge.Size = new System.Drawing.Size(430, 23);
            this.pbMerge.TabIndex = 33;
            // 
            // txtMergeFile
            // 
            this.txtMergeFile.AutoSize = true;
            this.txtMergeFile.Location = new System.Drawing.Point(80, 59);
            this.txtMergeFile.Name = "txtMergeFile";
            this.txtMergeFile.Size = new System.Drawing.Size(23, 12);
            this.txtMergeFile.TabIndex = 32;
            this.txtMergeFile.Text = "...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "当前文件 ：";
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(547, 64);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 30;
            this.btnMerge.Text = "合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnMergeFolder
            // 
            this.btnMergeFolder.Location = new System.Drawing.Point(547, 20);
            this.btnMergeFolder.Name = "btnMergeFolder";
            this.btnMergeFolder.Size = new System.Drawing.Size(75, 23);
            this.btnMergeFolder.TabIndex = 27;
            this.btnMergeFolder.Text = "选择";
            this.btnMergeFolder.UseVisualStyleBackColor = true;
            this.btnMergeFolder.Click += new System.EventHandler(this.btnMergeFolder_Click);
            // 
            // txtMergeFolder
            // 
            this.txtMergeFolder.Location = new System.Drawing.Point(82, 22);
            this.txtMergeFolder.Name = "txtMergeFolder";
            this.txtMergeFolder.Size = new System.Drawing.Size(430, 21);
            this.txtMergeFolder.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "文件夹：";
            // 
            // tabMergeBy6
            // 
            this.tabMergeBy6.Controls.Add(this.btnMergeBy6);
            this.tabMergeBy6.Controls.Add(this.txtInterval);
            this.tabMergeBy6.Controls.Add(this.label4);
            this.tabMergeBy6.Controls.Add(this.progressMergeBy6);
            this.tabMergeBy6.Controls.Add(this.labMergeBy6);
            this.tabMergeBy6.Controls.Add(this.label9);
            this.tabMergeBy6.Controls.Add(this.btnOpenMergeBy6);
            this.tabMergeBy6.Controls.Add(this.txtFolerMerge6);
            this.tabMergeBy6.Controls.Add(this.label11);
            this.tabMergeBy6.Location = new System.Drawing.Point(4, 22);
            this.tabMergeBy6.Name = "tabMergeBy6";
            this.tabMergeBy6.Padding = new System.Windows.Forms.Padding(3);
            this.tabMergeBy6.Size = new System.Drawing.Size(760, 357);
            this.tabMergeBy6.TabIndex = 3;
            this.tabMergeBy6.Text = "间隔数据归类";
            this.tabMergeBy6.UseVisualStyleBackColor = true;
            // 
            // progressMergeBy6
            // 
            this.progressMergeBy6.Location = new System.Drawing.Point(62, 191);
            this.progressMergeBy6.Name = "progressMergeBy6";
            this.progressMergeBy6.Size = new System.Drawing.Size(430, 23);
            this.progressMergeBy6.TabIndex = 30;
            // 
            // labMergeBy6
            // 
            this.labMergeBy6.AutoSize = true;
            this.labMergeBy6.Location = new System.Drawing.Point(91, 142);
            this.labMergeBy6.Name = "labMergeBy6";
            this.labMergeBy6.Size = new System.Drawing.Size(23, 12);
            this.labMergeBy6.TabIndex = 29;
            this.labMergeBy6.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "当前文件 ：";
            // 
            // btnOpenMergeBy6
            // 
            this.btnOpenMergeBy6.Location = new System.Drawing.Point(548, 31);
            this.btnOpenMergeBy6.Name = "btnOpenMergeBy6";
            this.btnOpenMergeBy6.Size = new System.Drawing.Size(75, 23);
            this.btnOpenMergeBy6.TabIndex = 27;
            this.btnOpenMergeBy6.Text = "选择";
            this.btnOpenMergeBy6.UseVisualStyleBackColor = true;
            this.btnOpenMergeBy6.Click += new System.EventHandler(this.btnOpenMergeBy6_Click);
            // 
            // txtFolerMerge6
            // 
            this.txtFolerMerge6.Location = new System.Drawing.Point(83, 33);
            this.txtFolerMerge6.Name = "txtFolerMerge6";
            this.txtFolerMerge6.Size = new System.Drawing.Size(430, 21);
            this.txtFolerMerge6.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "文件夹：";
            // 
            // btnMergeBy6
            // 
            this.btnMergeBy6.Location = new System.Drawing.Point(548, 71);
            this.btnMergeBy6.Name = "btnMergeBy6";
            this.btnMergeBy6.Size = new System.Drawing.Size(75, 23);
            this.btnMergeBy6.TabIndex = 33;
            this.btnMergeBy6.Text = "合并";
            this.btnMergeBy6.UseVisualStyleBackColor = true;
            this.btnMergeBy6.Click += new System.EventHandler(this.btnMergeBy6_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(83, 76);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(430, 21);
            this.txtInterval.TabIndex = 32;
            this.txtInterval.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "间隔：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 381);
            this.Controls.Add(this.ta);
            this.Name = "FrmMain";
            this.Text = "数据格式化";
            this.ta.ResumeLayout(false);
            this.tabAdd.ResumeLayout(false);
            this.tabAdd.PerformLayout();
            this.tabColumn.ResumeLayout(false);
            this.tabColumn.PerformLayout();
            this.tabMerge.ResumeLayout(false);
            this.tabMerge.PerformLayout();
            this.tabMergeBy6.ResumeLayout(false);
            this.tabMergeBy6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ta;
        private System.Windows.Forms.TabPage tabAdd;
        private System.Windows.Forms.ProgressBar progressAdd;
        private System.Windows.Forms.Label txtCurrentFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabColumn;
        private System.Windows.Forms.ProgressBar pbColumn;
        private System.Windows.Forms.Label txtColumeCurrentFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnColumnExtract;
        private System.Windows.Forms.TextBox txtColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnColumnFolder;
        private System.Windows.Forms.TextBox txtColumnFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabMerge;
        private System.Windows.Forms.ProgressBar pbMerge;
        private System.Windows.Forms.Label txtMergeFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnMergeFolder;
        private System.Windows.Forms.TextBox txtMergeFolder;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabMergeBy6;
        private System.Windows.Forms.ProgressBar progressMergeBy6;
        private System.Windows.Forms.Label labMergeBy6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOpenMergeBy6;
        private System.Windows.Forms.TextBox txtFolerMerge6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnMergeBy6;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label4;
    }
}

