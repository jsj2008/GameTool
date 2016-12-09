namespace PublicUtilities
{
    partial class UcRouter
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
            this.btnTestRouter = new System.Windows.Forms.Button();
            this.groupRouter = new System.Windows.Forms.GroupBox();
            this.txtRouterPwd = new System.Windows.Forms.TextBox();
            this.txtRouterUser = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboRouter = new System.Windows.Forms.ComboBox();
            this.txtRounterIp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupRouter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestRouter
            // 
            this.btnTestRouter.ForeColor = System.Drawing.Color.Black;
            this.btnTestRouter.Location = new System.Drawing.Point(300, 78);
            this.btnTestRouter.Name = "btnTestRouter";
            this.btnTestRouter.Size = new System.Drawing.Size(43, 23);
            this.btnTestRouter.TabIndex = 50;
            this.btnTestRouter.Text = "测试";
            this.btnTestRouter.UseVisualStyleBackColor = true;
            this.btnTestRouter.Click += new System.EventHandler(this.btnTestRouter_Click);
            // 
            // groupRouter
            // 
            this.groupRouter.Controls.Add(this.btnTestRouter);
            this.groupRouter.Controls.Add(this.txtRouterPwd);
            this.groupRouter.Controls.Add(this.txtRouterUser);
            this.groupRouter.Controls.Add(this.label10);
            this.groupRouter.Controls.Add(this.label18);
            this.groupRouter.Controls.Add(this.comboRouter);
            this.groupRouter.Controls.Add(this.txtRounterIp);
            this.groupRouter.Controls.Add(this.label8);
            this.groupRouter.Controls.Add(this.label2);
            this.groupRouter.Location = new System.Drawing.Point(3, 3);
            this.groupRouter.Name = "groupRouter";
            this.groupRouter.Size = new System.Drawing.Size(354, 147);
            this.groupRouter.TabIndex = 49;
            this.groupRouter.TabStop = false;
            this.groupRouter.Text = "路由器参数";
            // 
            // txtRouterPwd
            // 
            this.txtRouterPwd.Location = new System.Drawing.Point(99, 111);
            this.txtRouterPwd.Name = "txtRouterPwd";
            this.txtRouterPwd.PasswordChar = '*';
            this.txtRouterPwd.Size = new System.Drawing.Size(184, 21);
            this.txtRouterPwd.TabIndex = 36;
            this.txtRouterPwd.Text = "admin";
            this.txtRouterPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRouterUser
            // 
            this.txtRouterUser.Location = new System.Drawing.Point(99, 80);
            this.txtRouterUser.Name = "txtRouterUser";
            this.txtRouterUser.Size = new System.Drawing.Size(184, 21);
            this.txtRouterUser.TabIndex = 35;
            this.txtRouterUser.Text = "admin";
            this.txtRouterUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "密码：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 83);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 37;
            this.label18.Text = "登录名：";
            // 
            // comboRouter
            // 
            this.comboRouter.DisplayMember = "Header";
            this.comboRouter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRouter.ForeColor = System.Drawing.Color.Red;
            this.comboRouter.FormattingEnabled = true;
            this.comboRouter.Location = new System.Drawing.Point(99, 19);
            this.comboRouter.Name = "comboRouter";
            this.comboRouter.Size = new System.Drawing.Size(184, 20);
            this.comboRouter.TabIndex = 11;
            this.comboRouter.ValueMember = "RouterType";
            // 
            // txtRounterIp
            // 
            this.txtRounterIp.Location = new System.Drawing.Point(99, 53);
            this.txtRounterIp.Name = "txtRounterIp";
            this.txtRounterIp.Size = new System.Drawing.Size(184, 21);
            this.txtRounterIp.TabIndex = 12;
            this.txtRounterIp.Text = "192.168.1.1";
            this.txtRounterIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "路由器类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "路由器IP：";
            // 
            // UcRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupRouter);
            this.Name = "UcRouter";
            this.Size = new System.Drawing.Size(362, 150);
            this.groupRouter.ResumeLayout(false);
            this.groupRouter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestRouter;
        private System.Windows.Forms.GroupBox groupRouter;
        private System.Windows.Forms.TextBox txtRouterPwd;
        private System.Windows.Forms.TextBox txtRouterUser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboRouter;
        private System.Windows.Forms.TextBox txtRounterIp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
    }
}
