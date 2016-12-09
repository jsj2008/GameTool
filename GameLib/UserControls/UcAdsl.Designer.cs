namespace PublicUtilities
{
    partial class UcAdsl
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
            this.btnTestAdsl = new System.Windows.Forms.Button();
            this.groupADSL = new System.Windows.Forms.GroupBox();
            this.txtADSLPwd = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtADSLUser = new System.Windows.Forms.TextBox();
            this.txtADSLName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupADSL.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestAdsl
            // 
            this.btnTestAdsl.ForeColor = System.Drawing.Color.Black;
            this.btnTestAdsl.Location = new System.Drawing.Point(306, 50);
            this.btnTestAdsl.Name = "btnTestAdsl";
            this.btnTestAdsl.Size = new System.Drawing.Size(43, 23);
            this.btnTestAdsl.TabIndex = 49;
            this.btnTestAdsl.Text = "测试";
            this.btnTestAdsl.UseVisualStyleBackColor = true;
            this.btnTestAdsl.Click += new System.EventHandler(this.btnTestAdsl_Click);
            // 
            // groupADSL
            // 
            this.groupADSL.Controls.Add(this.txtADSLPwd);
            this.groupADSL.Controls.Add(this.btnTestAdsl);
            this.groupADSL.Controls.Add(this.label15);
            this.groupADSL.Controls.Add(this.txtADSLUser);
            this.groupADSL.Controls.Add(this.txtADSLName);
            this.groupADSL.Controls.Add(this.label12);
            this.groupADSL.Controls.Add(this.label14);
            this.groupADSL.Location = new System.Drawing.Point(3, 3);
            this.groupADSL.Name = "groupADSL";
            this.groupADSL.Size = new System.Drawing.Size(366, 117);
            this.groupADSL.TabIndex = 50;
            this.groupADSL.TabStop = false;
            this.groupADSL.Text = "ADSL参数";
            // 
            // txtADSLPwd
            // 
            this.txtADSLPwd.Location = new System.Drawing.Point(99, 79);
            this.txtADSLPwd.Name = "txtADSLPwd";
            this.txtADSLPwd.PasswordChar = '*';
            this.txtADSLPwd.Size = new System.Drawing.Size(184, 21);
            this.txtADSLPwd.TabIndex = 35;
            this.txtADSLPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 36;
            this.label15.Text = "密码：";
            // 
            // txtADSLUser
            // 
            this.txtADSLUser.Location = new System.Drawing.Point(99, 52);
            this.txtADSLUser.Name = "txtADSLUser";
            this.txtADSLUser.Size = new System.Drawing.Size(184, 21);
            this.txtADSLUser.TabIndex = 9;
            this.txtADSLUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtADSLName
            // 
            this.txtADSLName.Location = new System.Drawing.Point(99, 21);
            this.txtADSLName.Name = "txtADSLName";
            this.txtADSLName.Size = new System.Drawing.Size(184, 21);
            this.txtADSLName.TabIndex = 8;
            this.txtADSLName.Text = "PPPOE";
            this.txtADSLName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "登录名：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "PPPOE名称：";
            // 
            // UcAdsl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupADSL);
            this.Name = "UcAdsl";
            this.Size = new System.Drawing.Size(374, 123);
            this.groupADSL.ResumeLayout(false);
            this.groupADSL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestAdsl;
        private System.Windows.Forms.GroupBox groupADSL;
        private System.Windows.Forms.TextBox txtADSLPwd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtADSLUser;
        private System.Windows.Forms.TextBox txtADSLName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
    }
}
