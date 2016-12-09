namespace GameGift
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.ucOpenFile1 = new PublicUtilities.UcOpenFile();
            this.ucProgress1 = new PublicUtilities.UcProgress();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "帐号文件：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(707, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 28;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(88, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 249);
            this.panel1.TabIndex = 31;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtLog.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.ForeColor = System.Drawing.Color.LawnGreen;
            this.txtLog.Location = new System.Drawing.Point(567, 128);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(666, 339);
            this.txtLog.TabIndex = 32;
            this.txtLog.Text = "Test";
            // 
            // ucOpenFile1
            // 
            this.ucOpenFile1.Filter = "Text Files (*.txt)|*.txt|Dictionary Files (*.dic)|*.dic|All Files (*.*)|*.*";
            this.ucOpenFile1.Location = new System.Drawing.Point(88, 32);
            this.ucOpenFile1.Name = "ucOpenFile1";
            this.ucOpenFile1.Size = new System.Drawing.Size(590, 28);
            this.ucOpenFile1.TabIndex = 30;
            // 
            // ucProgress1
            // 
            this.ucProgress1.Location = new System.Drawing.Point(84, 83);
            this.ucProgress1.Name = "ucProgress1";
            this.ucProgress1.Size = new System.Drawing.Size(594, 29);
            this.ucProgress1.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 544);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucOpenFile1);
            this.Controls.Add(this.ucProgress1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private PublicUtilities.UcProgress ucProgress1;
        private PublicUtilities.UcOpenFile ucOpenFile1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}

