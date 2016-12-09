namespace TestProject
{
    partial class Frmcaptcha
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.imagecaptcha = new System.Windows.Forms.PictureBox();
            this.lbcaptcha = new System.Windows.Forms.Label();
            this.lbFilePath = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imagecaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(79, 26);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnopen_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(199, 26);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // imagecaptcha
            // 
            this.imagecaptcha.Location = new System.Drawing.Point(79, 144);
            this.imagecaptcha.Name = "imagecaptcha";
            this.imagecaptcha.Size = new System.Drawing.Size(306, 169);
            this.imagecaptcha.TabIndex = 2;
            this.imagecaptcha.TabStop = false;
            // 
            // lbcaptcha
            // 
            this.lbcaptcha.Location = new System.Drawing.Point(135, 110);
            this.lbcaptcha.Name = "lbcaptcha";
            this.lbcaptcha.Size = new System.Drawing.Size(132, 24);
            this.lbcaptcha.TabIndex = 3;
            this.lbcaptcha.Text = "label1";
            // 
            // lbFilePath
            // 
            this.lbFilePath.AutoSize = true;
            this.lbFilePath.Location = new System.Drawing.Point(135, 82);
            this.lbFilePath.Name = "lbFilePath";
            this.lbFilePath.Size = new System.Drawing.Size(41, 12);
            this.lbFilePath.TabIndex = 4;
            this.lbFilePath.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(331, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "GetOption";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "结果：";
            // 
            // Frmcaptcha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 375);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbFilePath);
            this.Controls.Add(this.lbcaptcha);
            this.Controls.Add(this.imagecaptcha);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnOpen);
            this.Name = "Frmcaptcha";
            this.Text = "Frmcaptcha";
            ((System.ComponentModel.ISupportInitialize)(this.imagecaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.PictureBox imagecaptcha;
        private System.Windows.Forms.Label lbcaptcha;
        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}