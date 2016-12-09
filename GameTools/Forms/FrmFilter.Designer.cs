namespace WowTools
{
    partial class FrmFilter
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
            this.chkChar = new System.Windows.Forms.CheckBox();
            this.chkNum = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.ucOpenFile = new PublicUtilities.UcOpenFile();
            this.ucProgress = new PublicUtilities.UcProgress();
            this.SuspendLayout();
            // 
            // chkChar
            // 
            this.chkChar.AutoSize = true;
            this.chkChar.Location = new System.Drawing.Point(16, 77);
            this.chkChar.Name = "chkChar";
            this.chkChar.Size = new System.Drawing.Size(72, 16);
            this.chkChar.TabIndex = 12;
            this.chkChar.Text = "过滤字母";
            this.chkChar.UseVisualStyleBackColor = true;
            // 
            // chkNum
            // 
            this.chkNum.AutoSize = true;
            this.chkNum.Location = new System.Drawing.Point(135, 77);
            this.chkNum.Name = "chkNum";
            this.chkNum.Size = new System.Drawing.Size(72, 16);
            this.chkNum.TabIndex = 13;
            this.chkNum.Text = "过滤数字";
            this.chkNum.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.Color.Red;
            this.btnStart.Location = new System.Drawing.Point(430, 73);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 21);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ucOpenFile
            // 
            this.ucOpenFile.Filter = "Text Files (*.txt)|*.txt|Dictionary Files (*.dic)|*.dic|All Files (*.*)|*.*";
            this.ucOpenFile.Location = new System.Drawing.Point(12, 19);
            this.ucOpenFile.Name = "ucOpenFile";
            this.ucOpenFile.Size = new System.Drawing.Size(499, 28);
            this.ucOpenFile.TabIndex = 60;
            // 
            // ucProgress
            // 
            this.ucProgress.Location = new System.Drawing.Point(4, 138);
            this.ucProgress.Name = "ucProgress";
            this.ucProgress.Size = new System.Drawing.Size(544, 29);
            this.ucProgress.TabIndex = 59;
            // 
            // FrmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 249);
            this.Controls.Add(this.ucOpenFile);
            this.Controls.Add(this.ucProgress);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chkNum);
            this.Controls.Add(this.chkChar);
            this.Name = "FrmFilter";
            this.Text = "FrmFilter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkChar;
        private System.Windows.Forms.CheckBox chkNum;
        private System.Windows.Forms.Button btnStart;
        private PublicUtilities.UcProgress ucProgress;
        private PublicUtilities.UcOpenFile ucOpenFile;
    }
}