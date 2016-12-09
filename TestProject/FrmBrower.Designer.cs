namespace TestProject
{
    partial class FrmBrower
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
            this.webBrower = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrower
            // 
            this.webBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrower.Location = new System.Drawing.Point(0, 0);
            this.webBrower.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrower.Name = "webBrower";
            this.webBrower.Size = new System.Drawing.Size(819, 409);
            this.webBrower.TabIndex = 0;
            // 
            // FrmBrower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 409);
            this.Controls.Add(this.webBrower);
            this.Name = "FrmBrower";
            this.Text = "FrmBrower";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrower;
    }
}