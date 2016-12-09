namespace QQTest
{
    partial class FrmQQOnline
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQQ = new System.Windows.Forms.RichTextBox();
            this.qqView = new System.Windows.Forms.DataGridView();
            this.colQQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnChat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.qqView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(26, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "检测";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "检测QQ号；号相隔";
            // 
            // txtQQ
            // 
            this.txtQQ.Location = new System.Drawing.Point(35, 132);
            this.txtQQ.Name = "txtQQ";
            this.txtQQ.Size = new System.Drawing.Size(244, 96);
            this.txtQQ.TabIndex = 3;
            this.txtQQ.Text = "31636209";
            // 
            // qqView
            // 
            this.qqView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qqView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colQQ,
            this.colState});
            this.qqView.Location = new System.Drawing.Point(304, 55);
            this.qqView.Name = "qqView";
            this.qqView.RowTemplate.Height = 23;
            this.qqView.Size = new System.Drawing.Size(462, 333);
            this.qqView.TabIndex = 4;
            // 
            // colQQ
            // 
            this.colQQ.DataPropertyName = "QQ";
            this.colQQ.HeaderText = "QQ号";
            this.colQQ.Name = "colQQ";
            // 
            // colState
            // 
            this.colState.DataPropertyName = "State";
            this.colState.HeaderText = "状态";
            this.colState.Name = "colState";
            // 
            // btnChat
            // 
            this.btnChat.Location = new System.Drawing.Point(142, 34);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(75, 23);
            this.btnChat.TabIndex = 5;
            this.btnChat.Text = "Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // FrmQQOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 420);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.qqView);
            this.Controls.Add(this.txtQQ);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "FrmQQOnline";
            this.Text = "QQ在线检测";
            ((System.ComponentModel.ISupportInitialize)(this.qqView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtQQ;
        private System.Windows.Forms.DataGridView qqView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.Button btnChat;
    }
}

