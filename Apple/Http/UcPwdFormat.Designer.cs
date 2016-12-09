namespace Apple.Http
{
    partial class UcPwdFormat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFirstCharReversal = new System.Windows.Forms.CheckBox();
            this.chkAddOneAfterEndWithNum = new System.Windows.Forms.CheckBox();
            this.chkAddAAfterEndWithChar = new System.Windows.Forms.CheckBox();
            this.chkAddAAllNum = new System.Windows.Forms.CheckBox();
            this.chkAddOneAllChar = new System.Windows.Forms.CheckBox();
            this.chkRawAddFirstCharReversal = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkRaw = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkFirstCharReversal);
            this.groupBox1.Controls.Add(this.chkAddOneAfterEndWithNum);
            this.groupBox1.Controls.Add(this.chkAddAAfterEndWithChar);
            this.groupBox1.Controls.Add(this.chkAddAAllNum);
            this.groupBox1.Controls.Add(this.chkAddOneAllChar);
            this.groupBox1.Controls.Add(this.chkRawAddFirstCharReversal);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.chkRaw);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "密码选项";
            // 
            // chkFirstCharReversal
            // 
            this.chkFirstCharReversal.AutoSize = true;
            this.chkFirstCharReversal.Location = new System.Drawing.Point(6, 48);
            this.chkFirstCharReversal.Name = "chkFirstCharReversal";
            this.chkFirstCharReversal.Size = new System.Drawing.Size(120, 16);
            this.chkFirstCharReversal.TabIndex = 19;
            this.chkFirstCharReversal.Text = "首字母大小写反转";
            this.chkFirstCharReversal.UseVisualStyleBackColor = true;
            this.chkFirstCharReversal.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAddOneAfterEndWithNum
            // 
            this.chkAddOneAfterEndWithNum.AutoSize = true;
            this.chkAddOneAfterEndWithNum.Location = new System.Drawing.Point(6, 69);
            this.chkAddOneAfterEndWithNum.Name = "chkAddOneAfterEndWithNum";
            this.chkAddOneAfterEndWithNum.Size = new System.Drawing.Size(108, 16);
            this.chkAddOneAfterEndWithNum.TabIndex = 18;
            this.chkAddOneAfterEndWithNum.Text = "密码结尾数字+1";
            this.chkAddOneAfterEndWithNum.UseVisualStyleBackColor = true;
            this.chkAddOneAfterEndWithNum.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAddAAfterEndWithChar
            // 
            this.chkAddAAfterEndWithChar.AutoSize = true;
            this.chkAddAAfterEndWithChar.Location = new System.Drawing.Point(6, 90);
            this.chkAddAAfterEndWithChar.Name = "chkAddAAfterEndWithChar";
            this.chkAddAAfterEndWithChar.Size = new System.Drawing.Size(108, 16);
            this.chkAddAAfterEndWithChar.TabIndex = 17;
            this.chkAddAAfterEndWithChar.Text = "密码结尾字母+a";
            this.chkAddAAfterEndWithChar.UseVisualStyleBackColor = true;
            this.chkAddAAfterEndWithChar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAddAAllNum
            // 
            this.chkAddAAllNum.AutoSize = true;
            this.chkAddAAllNum.Location = new System.Drawing.Point(6, 112);
            this.chkAddAAllNum.Name = "chkAddAAllNum";
            this.chkAddAAllNum.Size = new System.Drawing.Size(72, 16);
            this.chkAddAAllNum.TabIndex = 16;
            this.chkAddAAllNum.Text = "全数字+a";
            this.chkAddAAllNum.UseVisualStyleBackColor = true;
            this.chkAddAAllNum.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAddOneAllChar
            // 
            this.chkAddOneAllChar.AutoSize = true;
            this.chkAddOneAllChar.Location = new System.Drawing.Point(6, 133);
            this.chkAddOneAllChar.Name = "chkAddOneAllChar";
            this.chkAddOneAllChar.Size = new System.Drawing.Size(72, 16);
            this.chkAddOneAllChar.TabIndex = 15;
            this.chkAddOneAllChar.Text = "全字母+1";
            this.chkAddOneAllChar.UseVisualStyleBackColor = true;
            this.chkAddOneAllChar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkRawAddFirstCharReversal
            // 
            this.chkRawAddFirstCharReversal.AutoSize = true;
            this.chkRawAddFirstCharReversal.Location = new System.Drawing.Point(6, 154);
            this.chkRawAddFirstCharReversal.Name = "chkRawAddFirstCharReversal";
            this.chkRawAddFirstCharReversal.Size = new System.Drawing.Size(174, 16);
            this.chkRawAddFirstCharReversal.TabIndex = 14;
            this.chkRawAddFirstCharReversal.Text = "原始密码+首字母大小写反转";
            this.chkRawAddFirstCharReversal.UseVisualStyleBackColor = true;
            this.chkRawAddFirstCharReversal.CheckStateChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(118, 27);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(72, 16);
            this.chkAll.TabIndex = 13;
            this.chkAll.Text = "全部选项";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkRaw
            // 
            this.chkRaw.AutoSize = true;
            this.chkRaw.Checked = true;
            this.chkRaw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRaw.Location = new System.Drawing.Point(6, 27);
            this.chkRaw.Name = "chkRaw";
            this.chkRaw.Size = new System.Drawing.Size(72, 16);
            this.chkRaw.TabIndex = 12;
            this.chkRaw.Text = "原始密码";
            this.chkRaw.UseVisualStyleBackColor = true;
            this.chkRaw.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // UcPwdFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UcPwdFormat";
            this.Size = new System.Drawing.Size(208, 180);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFirstCharReversal;
        private System.Windows.Forms.CheckBox chkAddOneAfterEndWithNum;
        private System.Windows.Forms.CheckBox chkAddAAfterEndWithChar;
        private System.Windows.Forms.CheckBox chkAddAAllNum;
        private System.Windows.Forms.CheckBox chkAddOneAllChar;
        private System.Windows.Forms.CheckBox chkRawAddFirstCharReversal;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkRaw;
    }
}
