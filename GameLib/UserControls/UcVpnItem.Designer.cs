namespace PublicUtilities
{
    partial class UcVpnItem
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
            this.btnTestVpn = new System.Windows.Forms.Button();
            this.groupVpn = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtVpnIP = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtVpnPwd = new System.Windows.Forms.TextBox();
            this.txtVpnUser = new System.Windows.Forms.TextBox();
            this.txtVpnName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupVpn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestVpn
            // 
            this.btnTestVpn.ForeColor = System.Drawing.Color.Black;
            this.btnTestVpn.Location = new System.Drawing.Point(289, 80);
            this.btnTestVpn.Name = "btnTestVpn";
            this.btnTestVpn.Size = new System.Drawing.Size(43, 25);
            this.btnTestVpn.TabIndex = 50;
            this.btnTestVpn.Text = "测试";
            this.btnTestVpn.UseVisualStyleBackColor = true;
            this.btnTestVpn.Click += new System.EventHandler(this.btnTestVpn_Click);
            // 
            // groupVpn
            // 
            this.groupVpn.Controls.Add(this.btnTestVpn);
            this.groupVpn.Controls.Add(this.label41);
            this.groupVpn.Controls.Add(this.txtVpnIP);
            this.groupVpn.Controls.Add(this.label26);
            this.groupVpn.Controls.Add(this.txtVpnPwd);
            this.groupVpn.Controls.Add(this.txtVpnUser);
            this.groupVpn.Controls.Add(this.txtVpnName);
            this.groupVpn.Controls.Add(this.label19);
            this.groupVpn.Controls.Add(this.label20);
            this.groupVpn.Controls.Add(this.label21);
            this.groupVpn.Location = new System.Drawing.Point(3, 3);
            this.groupVpn.Name = "groupVpn";
            this.groupVpn.Size = new System.Drawing.Size(353, 160);
            this.groupVpn.TabIndex = 49;
            this.groupVpn.TabStop = false;
            this.groupVpn.Text = "VPN参数";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.Red;
            this.label41.Location = new System.Drawing.Point(59, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(255, 13);
            this.label41.TabIndex = 47;
            this.label41.Text = "请先自建VPN连接测试，最好保证VPN支持Ping!";
            this.label41.Visible = false;
            // 
            // txtVpnIP
            // 
            this.txtVpnIP.Location = new System.Drawing.Point(99, 59);
            this.txtVpnIP.Name = "txtVpnIP";
            this.txtVpnIP.Size = new System.Drawing.Size(184, 20);
            this.txtVpnIP.TabIndex = 35;
            this.txtVpnIP.Text = "192.168.1.1";
            this.txtVpnIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(7, 62);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 13);
            this.label26.TabIndex = 36;
            this.label26.Text = "IP地址：";
            // 
            // txtVpnPwd
            // 
            this.txtVpnPwd.Location = new System.Drawing.Point(98, 126);
            this.txtVpnPwd.Name = "txtVpnPwd";
            this.txtVpnPwd.PasswordChar = '*';
            this.txtVpnPwd.Size = new System.Drawing.Size(185, 20);
            this.txtVpnPwd.TabIndex = 10;
            this.txtVpnPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVpnUser
            // 
            this.txtVpnUser.Location = new System.Drawing.Point(99, 92);
            this.txtVpnUser.Name = "txtVpnUser";
            this.txtVpnUser.Size = new System.Drawing.Size(184, 20);
            this.txtVpnUser.TabIndex = 9;
            this.txtVpnUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVpnName
            // 
            this.txtVpnName.Location = new System.Drawing.Point(99, 23);
            this.txtVpnName.Name = "txtVpnName";
            this.txtVpnName.Size = new System.Drawing.Size(184, 20);
            this.txtVpnName.TabIndex = 8;
            this.txtVpnName.Text = "VPN";
            this.txtVpnName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 126);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 13);
            this.label19.TabIndex = 34;
            this.label19.Text = "密码：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 33;
            this.label20.Text = "登录名：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 13);
            this.label21.TabIndex = 32;
            this.label21.Text = "VPN名称：";
            // 
            // UcVpnItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupVpn);
            this.Name = "UcVpnItem";
            this.Size = new System.Drawing.Size(364, 166);
            this.groupVpn.ResumeLayout(false);
            this.groupVpn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestVpn;
        private System.Windows.Forms.GroupBox groupVpn;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtVpnIP;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtVpnPwd;
        private System.Windows.Forms.TextBox txtVpnUser;
        private System.Windows.Forms.TextBox txtVpnName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
    }
}
