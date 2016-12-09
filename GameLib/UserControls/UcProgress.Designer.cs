namespace PublicUtilities
{
    partial class UcProgress
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
            this.txtCount = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCount
            // 
            this.txtCount.AutoSize = true;
            this.txtCount.ForeColor = System.Drawing.Color.Red;
            this.txtCount.Location = new System.Drawing.Point(159, 6);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(11, 12);
            this.txtCount.TabIndex = 63;
            this.txtCount.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(88, 5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 62;
            this.label26.Text = "当前数据：";
            // 
            // txtTime
            // 
            this.txtTime.AutoSize = true;
            this.txtTime.ForeColor = System.Drawing.Color.Red;
            this.txtTime.Location = new System.Drawing.Point(35, 6);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(11, 12);
            this.txtTime.TabIndex = 61;
            this.txtTime.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(0, 6);
            this.label24.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 60;
            this.label24.Text = "用时：";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 6);
            this.progressBar.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(191, 19);
            this.progressBar.TabIndex = 59;
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Controls.Add(this.txtTime);
            this.panelInfo.Controls.Add(this.txtCount);
            this.panelInfo.Controls.Add(this.label24);
            this.panelInfo.Controls.Add(this.label26);
            this.panelInfo.Location = new System.Drawing.Point(207, 3);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(202, 19);
            this.panelInfo.TabIndex = 64;
            // 
            // UcProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelInfo);
            this.Name = "UcProgress";
            this.Size = new System.Drawing.Size(409, 29);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelInfo;
    }
}
