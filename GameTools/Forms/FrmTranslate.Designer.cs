namespace WowTools
{
    partial class FrmTranslate
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioMode3Del = new System.Windows.Forms.RadioButton();
            this.radioMode3Insert = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMode3AddChar = new System.Windows.Forms.TextBox();
            this.txtMode3Index = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.radioMode3 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMode2AddChar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMode1AddChar = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMode1After = new System.Windows.Forms.TextBox();
            this.txtMode1Before = new System.Windows.Forms.TextBox();
            this.cbMode1AddBefore = new System.Windows.Forms.CheckBox();
            this.cbMode1AddAfter = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioMode2 = new System.Windows.Forms.RadioButton();
            this.radioMode1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btnExit);
            this.tabPage1.Controls.Add(this.txtCount);
            this.tabPage1.Controls.Add(this.txtTime);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnOpenFile);
            this.tabPage1.Controls.Add(this.txtFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(729, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据插入工具";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(648, 179);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtCount
            // 
            this.txtCount.AutoSize = true;
            this.txtCount.ForeColor = System.Drawing.Color.Red;
            this.txtCount.Location = new System.Drawing.Point(646, 326);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(35, 12);
            this.txtCount.TabIndex = 21;
            this.txtCount.Text = "第0条";
            // 
            // txtTime
            // 
            this.txtTime.AutoSize = true;
            this.txtTime.ForeColor = System.Drawing.Color.Red;
            this.txtTime.Location = new System.Drawing.Point(646, 284);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(65, 12);
            this.txtTime.TabIndex = 20;
            this.txtTime.Text = "00：00：00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(646, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 19;
            this.label12.Text = "当前:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(646, 262);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "用时:";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(646, 125);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(646, 78);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 21);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(34, 410);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(595, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.radioMode3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMode2AddChar);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMode1AddChar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioMode2);
            this.groupBox1.Controls.Add(this.radioMode1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(34, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 367);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioMode3Del);
            this.groupBox3.Controls.Add(this.radioMode3Insert);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtMode3AddChar);
            this.groupBox3.Controls.Add(this.txtMode3Index);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(24, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(511, 74);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // radioMode3Del
            // 
            this.radioMode3Del.AutoSize = true;
            this.radioMode3Del.Location = new System.Drawing.Point(100, 15);
            this.radioMode3Del.Name = "radioMode3Del";
            this.radioMode3Del.Size = new System.Drawing.Size(47, 16);
            this.radioMode3Del.TabIndex = 22;
            this.radioMode3Del.TabStop = true;
            this.radioMode3Del.Text = "删除";
            this.radioMode3Del.UseVisualStyleBackColor = true;
            this.radioMode3Del.Click += new System.EventHandler(this.radioMode3Del_CheckedChanged);
            // 
            // radioMode3Insert
            // 
            this.radioMode3Insert.AutoSize = true;
            this.radioMode3Insert.Location = new System.Drawing.Point(28, 15);
            this.radioMode3Insert.Name = "radioMode3Insert";
            this.radioMode3Insert.Size = new System.Drawing.Size(47, 16);
            this.radioMode3Insert.TabIndex = 21;
            this.radioMode3Insert.TabStop = true;
            this.radioMode3Insert.Text = "插入";
            this.radioMode3Insert.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(172, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(287, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "位置从1开始，插入为指定位置之前，删除为当前位置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "位置:";
            // 
            // txtMode3AddChar
            // 
            this.txtMode3AddChar.Location = new System.Drawing.Point(241, 40);
            this.txtMode3AddChar.Name = "txtMode3AddChar";
            this.txtMode3AddChar.Size = new System.Drawing.Size(104, 21);
            this.txtMode3AddChar.TabIndex = 18;
            // 
            // txtMode3Index
            // 
            this.txtMode3Index.Location = new System.Drawing.Point(67, 37);
            this.txtMode3Index.Name = "txtMode3Index";
            this.txtMode3Index.Size = new System.Drawing.Size(104, 21);
            this.txtMode3Index.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "字符:";
            // 
            // radioMode3
            // 
            this.radioMode3.AutoSize = true;
            this.radioMode3.Location = new System.Drawing.Point(15, 265);
            this.radioMode3.Name = "radioMode3";
            this.radioMode3.Size = new System.Drawing.Size(59, 16);
            this.radioMode3.TabIndex = 13;
            this.radioMode3.TabStop = true;
            this.radioMode3.Text = "模式三";
            this.radioMode3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(245, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "匹配方式:如果满足第3条,则跳过1,2两种情况";
            // 
            // txtMode2AddChar
            // 
            this.txtMode2AddChar.Location = new System.Drawing.Point(332, 236);
            this.txtMode2AddChar.Name = "txtMode2AddChar";
            this.txtMode2AddChar.Size = new System.Drawing.Size(104, 21);
            this.txtMode2AddChar.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "请输入你想要的字符:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "1,密码最后面加1位或者几位字符,";
            // 
            // txtMode1AddChar
            // 
            this.txtMode1AddChar.Location = new System.Drawing.Point(455, 66);
            this.txtMode1AddChar.Name = "txtMode1AddChar";
            this.txtMode1AddChar.Size = new System.Drawing.Size(104, 21);
            this.txtMode1AddChar.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMode1After);
            this.groupBox2.Controls.Add(this.txtMode1Before);
            this.groupBox2.Controls.Add(this.cbMode1AddBefore);
            this.groupBox2.Controls.Add(this.cbMode1AddAfter);
            this.groupBox2.Location = new System.Drawing.Point(39, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 42);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // txtMode1After
            // 
            this.txtMode1After.Location = new System.Drawing.Point(279, 15);
            this.txtMode1After.Name = "txtMode1After";
            this.txtMode1After.ReadOnly = true;
            this.txtMode1After.Size = new System.Drawing.Size(83, 21);
            this.txtMode1After.TabIndex = 6;
            // 
            // txtMode1Before
            // 
            this.txtMode1Before.Location = new System.Drawing.Point(85, 15);
            this.txtMode1Before.Name = "txtMode1Before";
            this.txtMode1Before.ReadOnly = true;
            this.txtMode1Before.Size = new System.Drawing.Size(83, 21);
            this.txtMode1Before.TabIndex = 5;
            // 
            // cbMode1AddBefore
            // 
            this.cbMode1AddBefore.AutoSize = true;
            this.cbMode1AddBefore.Location = new System.Drawing.Point(19, 17);
            this.cbMode1AddBefore.Name = "cbMode1AddBefore";
            this.cbMode1AddBefore.Size = new System.Drawing.Size(60, 16);
            this.cbMode1AddBefore.TabIndex = 0;
            this.cbMode1AddBefore.Text = "加前面";
            this.cbMode1AddBefore.UseVisualStyleBackColor = true;
            this.cbMode1AddBefore.Click += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // cbMode1AddAfter
            // 
            this.cbMode1AddAfter.AutoSize = true;
            this.cbMode1AddAfter.Location = new System.Drawing.Point(213, 17);
            this.cbMode1AddAfter.Name = "cbMode1AddAfter";
            this.cbMode1AddAfter.Size = new System.Drawing.Size(60, 16);
            this.cbMode1AddAfter.TabIndex = 1;
            this.cbMode1AddAfter.Text = "加后面";
            this.cbMode1AddAfter.UseVisualStyleBackColor = true;
            this.cbMode1AddAfter.Click += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "请输入你想要的字符:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(413, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "3,如果密码都是数字,那么在数字前面加英文字母,或者在数字后面加英文字母";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "2,判断如果密码最后一个是英文则在密码后面加字符,";
            // 
            // radioMode2
            // 
            this.radioMode2.AutoSize = true;
            this.radioMode2.Location = new System.Drawing.Point(15, 204);
            this.radioMode2.Name = "radioMode2";
            this.radioMode2.Size = new System.Drawing.Size(59, 16);
            this.radioMode2.TabIndex = 4;
            this.radioMode2.TabStop = true;
            this.radioMode2.Text = "模式二";
            this.radioMode2.UseVisualStyleBackColor = true;
            // 
            // radioMode1
            // 
            this.radioMode1.AutoSize = true;
            this.radioMode1.Location = new System.Drawing.Point(15, 20);
            this.radioMode1.Name = "radioMode1";
            this.radioMode1.Size = new System.Drawing.Size(59, 16);
            this.radioMode1.TabIndex = 2;
            this.radioMode1.TabStop = true;
            this.radioMode1.Text = "模式一";
            this.radioMode1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "1,行的最后一个如果是数字就递增";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(465, 11);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 21);
            this.btnOpenFile.TabIndex = 9;
            this.btnOpenFile.Text = "选择文件";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(34, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(401, 21);
            this.txtFile.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(737, 476);
            this.tabControl1.TabIndex = 0;
            // 
            // FrmTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 476);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmTranslate";
            this.Text = "数据插入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioMode3Del;
        private System.Windows.Forms.RadioButton radioMode3Insert;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMode3AddChar;
        private System.Windows.Forms.TextBox txtMode3Index;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioMode3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMode2AddChar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMode1AddChar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMode1After;
        private System.Windows.Forms.TextBox txtMode1Before;
        private System.Windows.Forms.CheckBox cbMode1AddBefore;
        private System.Windows.Forms.CheckBox cbMode1AddAfter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioMode2;
        private System.Windows.Forms.RadioButton radioMode1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnExit;


    }
}

