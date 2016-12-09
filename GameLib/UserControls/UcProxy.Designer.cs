namespace PublicUtilities.UserControls
{
    partial class UcProxy
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.txtProxyIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStopProxy = new System.Windows.Forms.Button();
            this.radioMannualProxy = new System.Windows.Forms.RadioButton();
            this.btnProxyFile = new System.Windows.Forms.Button();
            this.btnProxySpeed = new System.Windows.Forms.Button();
            this.radioAutoProxy = new System.Windows.Forms.RadioButton();
            this.txtProxyFile = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtProxyPort);
            this.groupBox6.Controls.Add(this.txtProxyIP);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.btnStopProxy);
            this.groupBox6.Controls.Add(this.radioMannualProxy);
            this.groupBox6.Controls.Add(this.btnProxyFile);
            this.groupBox6.Controls.Add(this.btnProxySpeed);
            this.groupBox6.Controls.Add(this.radioAutoProxy);
            this.groupBox6.Controls.Add(this.txtProxyFile);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(335, 167);
            this.groupBox6.TabIndex = 81;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "代理选择";
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.Enabled = false;
            this.txtProxyPort.Location = new System.Drawing.Point(60, 131);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.Size = new System.Drawing.Size(61, 21);
            this.txtProxyPort.TabIndex = 67;
            this.txtProxyPort.Text = "80";
            this.txtProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProxyIP
            // 
            this.txtProxyIP.Enabled = false;
            this.txtProxyIP.Location = new System.Drawing.Point(60, 101);
            this.txtProxyIP.Name = "txtProxyIP";
            this.txtProxyIP.Size = new System.Drawing.Size(138, 21);
            this.txtProxyIP.TabIndex = 66;
            this.txtProxyIP.Text = "127.0.0.1";
            this.txtProxyIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 69;
            this.label4.Text = "端口:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 68;
            this.label5.Text = "IP：";
            // 
            // btnStopProxy
            // 
            this.btnStopProxy.Location = new System.Drawing.Point(221, 13);
            this.btnStopProxy.Name = "btnStopProxy";
            this.btnStopProxy.Size = new System.Drawing.Size(53, 23);
            this.btnStopProxy.TabIndex = 78;
            this.btnStopProxy.Text = "停止";
            this.btnStopProxy.UseVisualStyleBackColor = true;
            this.btnStopProxy.Visible = false;
            this.btnStopProxy.Click += new System.EventHandler(this.btnStopProxy_Click);
            // 
            // radioMannualProxy
            // 
            this.radioMannualProxy.AutoSize = true;
            this.radioMannualProxy.Location = new System.Drawing.Point(8, 78);
            this.radioMannualProxy.Name = "radioMannualProxy";
            this.radioMannualProxy.Size = new System.Drawing.Size(71, 16);
            this.radioMannualProxy.TabIndex = 76;
            this.radioMannualProxy.TabStop = true;
            this.radioMannualProxy.Text = "手动代理";
            this.radioMannualProxy.UseVisualStyleBackColor = true;
            this.radioMannualProxy.CheckedChanged += new System.EventHandler(this.radioMannualProxy_CheckedChanged);
            // 
            // btnProxyFile
            // 
            this.btnProxyFile.Enabled = false;
            this.btnProxyFile.Location = new System.Drawing.Point(93, 13);
            this.btnProxyFile.Name = "btnProxyFile";
            this.btnProxyFile.Size = new System.Drawing.Size(53, 23);
            this.btnProxyFile.TabIndex = 73;
            this.btnProxyFile.Text = "打开&P";
            this.btnProxyFile.UseVisualStyleBackColor = true;
            this.btnProxyFile.Click += new System.EventHandler(this.btnProxyFile_Click);
            // 
            // btnProxySpeed
            // 
            this.btnProxySpeed.Location = new System.Drawing.Point(158, 13);
            this.btnProxySpeed.Name = "btnProxySpeed";
            this.btnProxySpeed.Size = new System.Drawing.Size(53, 23);
            this.btnProxySpeed.TabIndex = 77;
            this.btnProxySpeed.Text = "测速";
            this.btnProxySpeed.UseVisualStyleBackColor = true;
            this.btnProxySpeed.Visible = false;
            this.btnProxySpeed.Click += new System.EventHandler(this.btnProxySpeed_Click);
            // 
            // radioAutoProxy
            // 
            this.radioAutoProxy.AutoSize = true;
            this.radioAutoProxy.Location = new System.Drawing.Point(8, 18);
            this.radioAutoProxy.Name = "radioAutoProxy";
            this.radioAutoProxy.Size = new System.Drawing.Size(71, 16);
            this.radioAutoProxy.TabIndex = 75;
            this.radioAutoProxy.TabStop = true;
            this.radioAutoProxy.Text = "自动代理";
            this.radioAutoProxy.UseVisualStyleBackColor = true;
            this.radioAutoProxy.CheckedChanged += new System.EventHandler(this.radioAutoProxy_CheckedChanged);
            // 
            // txtProxyFile
            // 
            this.txtProxyFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyFile.Enabled = false;
            this.txtProxyFile.Location = new System.Drawing.Point(8, 42);
            this.txtProxyFile.Name = "txtProxyFile";
            this.txtProxyFile.ReadOnly = true;
            this.txtProxyFile.Size = new System.Drawing.Size(321, 21);
            this.txtProxyFile.TabIndex = 74;
            // 
            // UcProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Name = "UcProxy";
            this.Size = new System.Drawing.Size(335, 167);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtProxyPort;
        private System.Windows.Forms.TextBox txtProxyIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStopProxy;
        private System.Windows.Forms.RadioButton radioMannualProxy;
        private System.Windows.Forms.Button btnProxyFile;
        private System.Windows.Forms.Button btnProxySpeed;
        private System.Windows.Forms.RadioButton radioAutoProxy;
        private System.Windows.Forms.TextBox txtProxyFile;
    }
}
