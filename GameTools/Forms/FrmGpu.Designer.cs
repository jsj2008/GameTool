namespace WowTools
{
    partial class FrmGpu
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
            this.btnOpenApp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApp = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenData = new System.Windows.Forms.Button();
            this.groupSalt = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSalt = new System.Windows.Forms.CheckBox();
            this.btnCrack = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboApp = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupSalt.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenApp
            // 
            this.btnOpenApp.Location = new System.Drawing.Point(558, 22);
            this.btnOpenApp.Name = "btnOpenApp";
            this.btnOpenApp.Size = new System.Drawing.Size(75, 25);
            this.btnOpenApp.TabIndex = 5;
            this.btnOpenApp.Text = "打开";
            this.btnOpenApp.UseVisualStyleBackColor = true;
            this.btnOpenApp.Click += new System.EventHandler(this.btnOpenApp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "程序路径：";
            // 
            // txtApp
            // 
            this.txtApp.Location = new System.Drawing.Point(93, 22);
            this.txtApp.Name = "txtApp";
            this.txtApp.Size = new System.Drawing.Size(430, 20);
            this.txtApp.TabIndex = 0;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(93, 75);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(430, 20);
            this.txtData.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据路径：";
            // 
            // btnOpenData
            // 
            this.btnOpenData.Location = new System.Drawing.Point(558, 73);
            this.btnOpenData.Name = "btnOpenData";
            this.btnOpenData.Size = new System.Drawing.Size(75, 25);
            this.btnOpenData.TabIndex = 6;
            this.btnOpenData.Text = "打开";
            this.btnOpenData.UseVisualStyleBackColor = true;
            this.btnOpenData.Click += new System.EventHandler(this.btnOpenData_Click);
            // 
            // groupSalt
            // 
            this.groupSalt.Controls.Add(this.label5);
            this.groupSalt.Controls.Add(this.txtTimeOut);
            this.groupSalt.Controls.Add(this.label4);
            this.groupSalt.Controls.Add(this.txtCount);
            this.groupSalt.Controls.Add(this.label3);
            this.groupSalt.Controls.Add(this.chkSalt);
            this.groupSalt.Location = new System.Drawing.Point(26, 145);
            this.groupSalt.Name = "groupSalt";
            this.groupSalt.Size = new System.Drawing.Size(497, 146);
            this.groupSalt.TabIndex = 8;
            this.groupSalt.TabStop = false;
            this.groupSalt.Text = "groupBox1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(219, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "(秒)";
            this.label5.Visible = false;
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(91, 92);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(122, 20);
            this.txtTimeOut.TabIndex = 4;
            this.txtTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimeOut.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "超时：";
            this.label4.Visible = false;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(91, 48);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(122, 20);
            this.txtCount.TabIndex = 3;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "单批个数：";
            // 
            // chkSalt
            // 
            this.chkSalt.AutoSize = true;
            this.chkSalt.ForeColor = System.Drawing.Color.Red;
            this.chkSalt.Location = new System.Drawing.Point(6, 0);
            this.chkSalt.Name = "chkSalt";
            this.chkSalt.Size = new System.Drawing.Size(116, 18);
            this.chkSalt.TabIndex = 2;
            this.chkSalt.Text = "是否含有Salt数据";
            this.chkSalt.UseVisualStyleBackColor = true;
            this.chkSalt.CheckedChanged += new System.EventHandler(this.chkSalt_CheckedChanged);
            // 
            // btnCrack
            // 
            this.btnCrack.Location = new System.Drawing.Point(558, 129);
            this.btnCrack.Name = "btnCrack";
            this.btnCrack.Size = new System.Drawing.Size(75, 25);
            this.btnCrack.TabIndex = 9;
            this.btnCrack.Text = "开始破解";
            this.btnCrack.UseVisualStyleBackColor = true;
            this.btnCrack.Click += new System.EventHandler(this.btnCrack_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "破解程序：";
            this.label6.Visible = false;
            // 
            // comboApp
            // 
            this.comboApp.DisplayMember = "Title";
            this.comboApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboApp.ForeColor = System.Drawing.Color.Red;
            this.comboApp.FormattingEnabled = true;
            this.comboApp.Location = new System.Drawing.Point(93, 126);
            this.comboApp.Name = "comboApp";
            this.comboApp.Size = new System.Drawing.Size(155, 21);
            this.comboApp.TabIndex = 11;
            this.comboApp.ValueMember = "GpuApp";
            this.comboApp.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(558, 173);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 25);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "停止破解";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // FrmGpu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 391);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.comboApp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCrack);
            this.Controls.Add(this.groupSalt);
            this.Controls.Add(this.btnOpenData);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtApp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenApp);
            this.Name = "FrmGpu";
            this.Text = "FrmGpu";
            this.groupSalt.ResumeLayout(false);
            this.groupSalt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApp;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenData;
        private System.Windows.Forms.GroupBox groupSalt;
        private System.Windows.Forms.CheckBox chkSalt;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCrack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboApp;
        private System.Windows.Forms.Button btnStop;
    }
}