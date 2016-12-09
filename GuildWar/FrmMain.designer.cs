namespace GuildWar
{
    partial class FrmMain
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
            this.btnStop = new System.Windows.Forms.Button();
            this.tabWebDetection = new System.Windows.Forms.TabControl();
            this.pageParams = new System.Windows.Forms.TabPage();
            this.txtCaptchCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelNet = new System.Windows.Forms.Panel();
            this.radioFromFirst = new System.Windows.Forms.RadioButton();
            this.txtLastInfo = new System.Windows.Forms.Label();
            this.radioCustomRange = new System.Windows.Forms.RadioButton();
            this.radioFromStopped = new System.Windows.Forms.RadioButton();
            this.label35 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtUpper = new System.Windows.Forms.TextBox();
            this.txtLower = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.chkReconnect = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRepeatCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboDataType = new System.Windows.Forms.ComboBox();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboServer = new System.Windows.Forms.ComboBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupRestartMode = new System.Windows.Forms.GroupBox();
            this.radioVpn = new System.Windows.Forms.RadioButton();
            this.radioRouter = new System.Windows.Forms.RadioButton();
            this.radioADSL = new System.Windows.Forms.RadioButton();
            this.pageDisplay = new System.Windows.Forms.TabPage();
            this.progressProcess = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tagTest = new System.Windows.Forms.TabPage();
            this.panelWeb = new System.Windows.Forms.Panel();
            this.groupControl = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtRemainCount = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txtUnknowCount = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtFailedCount = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtSuccessCount = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtProcessedCount = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTotalCount = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labSeconds = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSpend = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPaused = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnStopSound = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDialCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabWebDetection.SuspendLayout();
            this.pageParams.SuspendLayout();
            this.groupRestartMode.SuspendLayout();
            this.pageDisplay.SuspendLayout();
            this.tagTest.SuspendLayout();
            this.groupControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(6, 43);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.Black;
            this.btnStop.Location = new System.Drawing.Point(6, 124);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tabWebDetection
            // 
            this.tabWebDetection.Controls.Add(this.pageParams);
            this.tabWebDetection.Controls.Add(this.pageDisplay);
            this.tabWebDetection.Controls.Add(this.tagTest);
            this.tabWebDetection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWebDetection.Location = new System.Drawing.Point(0, 0);
            this.tabWebDetection.Name = "tabWebDetection";
            this.tabWebDetection.SelectedIndex = 0;
            this.tabWebDetection.Size = new System.Drawing.Size(817, 519);
            this.tabWebDetection.TabIndex = 18;
            // 
            // pageParams
            // 
            this.pageParams.BackColor = System.Drawing.SystemColors.Control;
            this.pageParams.Controls.Add(this.txtDialCount);
            this.pageParams.Controls.Add(this.label4);
            this.pageParams.Controls.Add(this.txtCaptchCount);
            this.pageParams.Controls.Add(this.label2);
            this.pageParams.Controls.Add(this.panelNet);
            this.pageParams.Controls.Add(this.radioFromFirst);
            this.pageParams.Controls.Add(this.txtLastInfo);
            this.pageParams.Controls.Add(this.radioCustomRange);
            this.pageParams.Controls.Add(this.radioFromStopped);
            this.pageParams.Controls.Add(this.label35);
            this.pageParams.Controls.Add(this.label32);
            this.pageParams.Controls.Add(this.txtUpper);
            this.pageParams.Controls.Add(this.txtLower);
            this.pageParams.Controls.Add(this.label40);
            this.pageParams.Controls.Add(this.chkReconnect);
            this.pageParams.Controls.Add(this.label9);
            this.pageParams.Controls.Add(this.txtRepeatCount);
            this.pageParams.Controls.Add(this.label1);
            this.pageParams.Controls.Add(this.comboDataType);
            this.pageParams.Controls.Add(this.txtThreads);
            this.pageParams.Controls.Add(this.label7);
            this.pageParams.Controls.Add(this.comboServer);
            this.pageParams.Controls.Add(this.btnFile);
            this.pageParams.Controls.Add(this.label3);
            this.pageParams.Controls.Add(this.txtFilePath);
            this.pageParams.Controls.Add(this.label6);
            this.pageParams.Controls.Add(this.groupRestartMode);
            this.pageParams.Location = new System.Drawing.Point(4, 22);
            this.pageParams.Name = "pageParams";
            this.pageParams.Padding = new System.Windows.Forms.Padding(3);
            this.pageParams.Size = new System.Drawing.Size(809, 493);
            this.pageParams.TabIndex = 0;
            this.pageParams.Text = "参数设置";
            // 
            // txtCaptchCount
            // 
            this.txtCaptchCount.Location = new System.Drawing.Point(679, 222);
            this.txtCaptchCount.Name = "txtCaptchCount";
            this.txtCaptchCount.Size = new System.Drawing.Size(61, 21);
            this.txtCaptchCount.TabIndex = 58;
            this.txtCaptchCount.Text = "5";
            this.txtCaptchCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCaptchCount.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(575, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "验证出错重启数：";
            this.label2.Visible = false;
            // 
            // panelNet
            // 
            this.panelNet.Location = new System.Drawing.Point(11, 240);
            this.panelNet.Name = "panelNet";
            this.panelNet.Size = new System.Drawing.Size(399, 211);
            this.panelNet.TabIndex = 54;
            // 
            // radioFromFirst
            // 
            this.radioFromFirst.AutoSize = true;
            this.radioFromFirst.Checked = true;
            this.radioFromFirst.ForeColor = System.Drawing.Color.Black;
            this.radioFromFirst.Location = new System.Drawing.Point(338, 104);
            this.radioFromFirst.Name = "radioFromFirst";
            this.radioFromFirst.Size = new System.Drawing.Size(71, 16);
            this.radioFromFirst.TabIndex = 53;
            this.radioFromFirst.TabStop = true;
            this.radioFromFirst.Text = "重头开始";
            this.radioFromFirst.UseVisualStyleBackColor = true;
            // 
            // txtLastInfo
            // 
            this.txtLastInfo.AutoSize = true;
            this.txtLastInfo.Location = new System.Drawing.Point(492, 139);
            this.txtLastInfo.Name = "txtLastInfo";
            this.txtLastInfo.Size = new System.Drawing.Size(0, 12);
            this.txtLastInfo.TabIndex = 52;
            this.txtLastInfo.Visible = false;
            // 
            // radioCustomRange
            // 
            this.radioCustomRange.AutoSize = true;
            this.radioCustomRange.ForeColor = System.Drawing.Color.Black;
            this.radioCustomRange.Location = new System.Drawing.Point(339, 154);
            this.radioCustomRange.Name = "radioCustomRange";
            this.radioCustomRange.Size = new System.Drawing.Size(107, 16);
            this.radioCustomRange.TabIndex = 51;
            this.radioCustomRange.TabStop = true;
            this.radioCustomRange.Text = "自定义行号范围";
            this.radioCustomRange.UseVisualStyleBackColor = true;
            this.radioCustomRange.CheckedChanged += new System.EventHandler(this.radioCustomRange_CheckedChanged);
            // 
            // radioFromStopped
            // 
            this.radioFromStopped.AutoSize = true;
            this.radioFromStopped.ForeColor = System.Drawing.Color.Black;
            this.radioFromStopped.Location = new System.Drawing.Point(338, 128);
            this.radioFromStopped.Name = "radioFromStopped";
            this.radioFromStopped.Size = new System.Drawing.Size(143, 16);
            this.radioFromStopped.TabIndex = 50;
            this.radioFromStopped.TabStop = true;
            this.radioFromStopped.Text = "从上次停止处开始扫瞄";
            this.radioFromStopped.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(11, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(377, 12);
            this.label35.TabIndex = 46;
            this.label35.Text = "本软件所有参数只有在按开始按钮时才生效，所以执行前请仔细核对！";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(550, 156);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(17, 12);
            this.label32.TabIndex = 45;
            this.label32.Text = "到";
            // 
            // txtUpper
            // 
            this.txtUpper.Location = new System.Drawing.Point(580, 152);
            this.txtUpper.Name = "txtUpper";
            this.txtUpper.Size = new System.Drawing.Size(61, 21);
            this.txtUpper.TabIndex = 42;
            this.txtUpper.Text = "0";
            this.txtUpper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLower
            // 
            this.txtLower.Location = new System.Drawing.Point(477, 152);
            this.txtLower.Name = "txtLower";
            this.txtLower.Size = new System.Drawing.Size(63, 21);
            this.txtLower.TabIndex = 40;
            this.txtLower.Text = "50";
            this.txtLower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ForeColor = System.Drawing.Color.Black;
            this.label40.Location = new System.Drawing.Point(454, 156);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(29, 12);
            this.label40.TabIndex = 43;
            this.label40.Text = "从：";
            // 
            // chkReconnect
            // 
            this.chkReconnect.AutoSize = true;
            this.chkReconnect.Checked = true;
            this.chkReconnect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReconnect.Location = new System.Drawing.Point(13, 184);
            this.chkReconnect.Name = "chkReconnect";
            this.chkReconnect.Size = new System.Drawing.Size(120, 16);
            this.chkReconnect.TabIndex = 13;
            this.chkReconnect.Text = "是否支持网络重连";
            this.chkReconnect.UseVisualStyleBackColor = true;
            this.chkReconnect.CheckedChanged += new System.EventHandler(this.chkRestart_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(577, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "出错重试数：";
            this.label9.Visible = false;
            // 
            // txtRepeatCount
            // 
            this.txtRepeatCount.Location = new System.Drawing.Point(679, 191);
            this.txtRepeatCount.Name = "txtRepeatCount";
            this.txtRepeatCount.Size = new System.Drawing.Size(61, 21);
            this.txtRepeatCount.TabIndex = 7;
            this.txtRepeatCount.Text = "0";
            this.txtRepeatCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRepeatCount.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "数据格式：";
            // 
            // comboDataType
            // 
            this.comboDataType.DisplayMember = "Text";
            this.comboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDataType.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboDataType.ForeColor = System.Drawing.Color.Red;
            this.comboDataType.FormattingEnabled = true;
            this.comboDataType.Location = new System.Drawing.Point(449, 68);
            this.comboDataType.Name = "comboDataType";
            this.comboDataType.Size = new System.Drawing.Size(191, 23);
            this.comboDataType.TabIndex = 3;
            this.comboDataType.ValueMember = "Type";
            // 
            // txtThreads
            // 
            this.txtThreads.Location = new System.Drawing.Point(116, 99);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.Size = new System.Drawing.Size(61, 21);
            this.txtThreads.TabIndex = 4;
            this.txtThreads.Text = "5";
            this.txtThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "线程数：";
            // 
            // comboServer
            // 
            this.comboServer.DisplayMember = "Header";
            this.comboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboServer.ForeColor = System.Drawing.Color.Red;
            this.comboServer.FormattingEnabled = true;
            this.comboServer.Location = new System.Drawing.Point(115, 65);
            this.comboServer.Name = "comboServer";
            this.comboServer.Size = new System.Drawing.Size(191, 26);
            this.comboServer.TabIndex = 0;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(628, 30);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(53, 23);
            this.btnFile.TabIndex = 1;
            this.btnFile.Text = "打开&F";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "服务器：";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(114, 33);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(490, 21);
            this.txtFilePath.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "帐户文件：";
            // 
            // groupRestartMode
            // 
            this.groupRestartMode.Controls.Add(this.radioVpn);
            this.groupRestartMode.Controls.Add(this.radioRouter);
            this.groupRestartMode.Controls.Add(this.radioADSL);
            this.groupRestartMode.Enabled = false;
            this.groupRestartMode.Location = new System.Drawing.Point(13, 192);
            this.groupRestartMode.Name = "groupRestartMode";
            this.groupRestartMode.Size = new System.Drawing.Size(242, 42);
            this.groupRestartMode.TabIndex = 38;
            this.groupRestartMode.TabStop = false;
            // 
            // radioVpn
            // 
            this.radioVpn.AutoSize = true;
            this.radioVpn.Location = new System.Drawing.Point(157, 14);
            this.radioVpn.Name = "radioVpn";
            this.radioVpn.Size = new System.Drawing.Size(41, 16);
            this.radioVpn.TabIndex = 2;
            this.radioVpn.TabStop = true;
            this.radioVpn.Text = "VPN";
            this.radioVpn.UseVisualStyleBackColor = true;
            this.radioVpn.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioRouter
            // 
            this.radioRouter.AutoSize = true;
            this.radioRouter.Location = new System.Drawing.Point(77, 14);
            this.radioRouter.Name = "radioRouter";
            this.radioRouter.Size = new System.Drawing.Size(59, 16);
            this.radioRouter.TabIndex = 1;
            this.radioRouter.TabStop = true;
            this.radioRouter.Text = "路由器";
            this.radioRouter.UseVisualStyleBackColor = true;
            this.radioRouter.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioADSL
            // 
            this.radioADSL.AutoSize = true;
            this.radioADSL.Location = new System.Drawing.Point(18, 14);
            this.radioADSL.Name = "radioADSL";
            this.radioADSL.Size = new System.Drawing.Size(47, 16);
            this.radioADSL.TabIndex = 0;
            this.radioADSL.TabStop = true;
            this.radioADSL.Text = "ADSL";
            this.radioADSL.UseVisualStyleBackColor = true;
            this.radioADSL.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // pageDisplay
            // 
            this.pageDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.pageDisplay.Controls.Add(this.progressProcess);
            this.pageDisplay.Controls.Add(this.txtLog);
            this.pageDisplay.Location = new System.Drawing.Point(4, 22);
            this.pageDisplay.Name = "pageDisplay";
            this.pageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.pageDisplay.Size = new System.Drawing.Size(809, 493);
            this.pageDisplay.TabIndex = 1;
            this.pageDisplay.Text = "查询进度";
            // 
            // progressProcess
            // 
            this.progressProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressProcess.Location = new System.Drawing.Point(3, 464);
            this.progressProcess.Name = "progressProcess";
            this.progressProcess.Size = new System.Drawing.Size(785, 23);
            this.progressProcess.TabIndex = 20;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtLog.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.ForeColor = System.Drawing.Color.LawnGreen;
            this.txtLog.Location = new System.Drawing.Point(3, 11);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(785, 446);
            this.txtLog.TabIndex = 19;
            this.txtLog.Text = "Test";
            // 
            // tagTest
            // 
            this.tagTest.Controls.Add(this.panelWeb);
            this.tagTest.Location = new System.Drawing.Point(4, 22);
            this.tagTest.Name = "tagTest";
            this.tagTest.Padding = new System.Windows.Forms.Padding(3);
            this.tagTest.Size = new System.Drawing.Size(809, 493);
            this.tagTest.TabIndex = 2;
            this.tagTest.Text = "预览";
            this.tagTest.UseVisualStyleBackColor = true;
            // 
            // panelWeb
            // 
            this.panelWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWeb.Location = new System.Drawing.Point(3, 3);
            this.panelWeb.Name = "panelWeb";
            this.panelWeb.Size = new System.Drawing.Size(803, 487);
            this.panelWeb.TabIndex = 60;
            // 
            // groupControl
            // 
            this.groupControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl.Controls.Add(this.label23);
            this.groupControl.Controls.Add(this.txtRemainCount);
            this.groupControl.Controls.Add(this.label29);
            this.groupControl.Controls.Add(this.label34);
            this.groupControl.Controls.Add(this.txtUnknowCount);
            this.groupControl.Controls.Add(this.label36);
            this.groupControl.Controls.Add(this.label31);
            this.groupControl.Controls.Add(this.txtFailedCount);
            this.groupControl.Controls.Add(this.label33);
            this.groupControl.Controls.Add(this.label28);
            this.groupControl.Controls.Add(this.txtSuccessCount);
            this.groupControl.Controls.Add(this.label30);
            this.groupControl.Controls.Add(this.label25);
            this.groupControl.Controls.Add(this.txtProcessedCount);
            this.groupControl.Controls.Add(this.label27);
            this.groupControl.Controls.Add(this.label22);
            this.groupControl.Controls.Add(this.txtTotalCount);
            this.groupControl.Controls.Add(this.label24);
            this.groupControl.Controls.Add(this.label13);
            this.groupControl.Controls.Add(this.labSeconds);
            this.groupControl.Controls.Add(this.label11);
            this.groupControl.Controls.Add(this.groupBox1);
            this.groupControl.Controls.Add(this.groupBox2);
            this.groupControl.Controls.Add(this.groupBox3);
            this.groupControl.Location = new System.Drawing.Point(124, 10);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(114, 505);
            this.groupControl.TabIndex = 19;
            this.groupControl.TabStop = false;
            this.groupControl.Text = "数据栏";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(73, 251);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 39;
            this.label23.Text = "条";
            // 
            // txtRemainCount
            // 
            this.txtRemainCount.AutoSize = true;
            this.txtRemainCount.ForeColor = System.Drawing.Color.Red;
            this.txtRemainCount.Location = new System.Drawing.Point(24, 251);
            this.txtRemainCount.Name = "txtRemainCount";
            this.txtRemainCount.Size = new System.Drawing.Size(11, 12);
            this.txtRemainCount.TabIndex = 38;
            this.txtRemainCount.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(17, 229);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.TabIndex = 37;
            this.label29.Text = "剩余数：";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(73, 429);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(17, 12);
            this.label34.TabIndex = 33;
            this.label34.Text = "条";
            // 
            // txtUnknowCount
            // 
            this.txtUnknowCount.AutoSize = true;
            this.txtUnknowCount.Location = new System.Drawing.Point(26, 429);
            this.txtUnknowCount.Name = "txtUnknowCount";
            this.txtUnknowCount.Size = new System.Drawing.Size(11, 12);
            this.txtUnknowCount.TabIndex = 32;
            this.txtUnknowCount.Text = "0";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(17, 410);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(53, 12);
            this.label36.TabIndex = 31;
            this.label36.Text = "未知数：";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(73, 392);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(17, 12);
            this.label31.TabIndex = 30;
            this.label31.Text = "条";
            // 
            // txtFailedCount
            // 
            this.txtFailedCount.AutoSize = true;
            this.txtFailedCount.Location = new System.Drawing.Point(24, 392);
            this.txtFailedCount.Name = "txtFailedCount";
            this.txtFailedCount.Size = new System.Drawing.Size(11, 12);
            this.txtFailedCount.TabIndex = 29;
            this.txtFailedCount.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(17, 373);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 12);
            this.label33.TabIndex = 28;
            this.label33.Text = "失败数：";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(73, 353);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 12);
            this.label28.TabIndex = 27;
            this.label28.Text = "条";
            // 
            // txtSuccessCount
            // 
            this.txtSuccessCount.AutoSize = true;
            this.txtSuccessCount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSuccessCount.ForeColor = System.Drawing.Color.Red;
            this.txtSuccessCount.Location = new System.Drawing.Point(24, 353);
            this.txtSuccessCount.Name = "txtSuccessCount";
            this.txtSuccessCount.Size = new System.Drawing.Size(12, 12);
            this.txtSuccessCount.TabIndex = 26;
            this.txtSuccessCount.Text = "0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(17, 334);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 12);
            this.label30.TabIndex = 25;
            this.label30.Text = "成功数：";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(73, 210);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 24;
            this.label25.Text = "条";
            // 
            // txtProcessedCount
            // 
            this.txtProcessedCount.AutoSize = true;
            this.txtProcessedCount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProcessedCount.ForeColor = System.Drawing.Color.Red;
            this.txtProcessedCount.Location = new System.Drawing.Point(24, 210);
            this.txtProcessedCount.Name = "txtProcessedCount";
            this.txtProcessedCount.Size = new System.Drawing.Size(12, 12);
            this.txtProcessedCount.TabIndex = 23;
            this.txtProcessedCount.Text = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(17, 188);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 22;
            this.label27.Text = "已处理数：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(72, 168);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 21;
            this.label22.Text = "条";
            // 
            // txtTotalCount
            // 
            this.txtTotalCount.AutoSize = true;
            this.txtTotalCount.ForeColor = System.Drawing.Color.Red;
            this.txtTotalCount.Location = new System.Drawing.Point(24, 168);
            this.txtTotalCount.Name = "txtTotalCount";
            this.txtTotalCount.Size = new System.Drawing.Size(11, 12);
            this.txtTotalCount.TabIndex = 20;
            this.txtTotalCount.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 146);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 19;
            this.label24.Text = "数据总数：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(71, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "秒";
            // 
            // labSeconds
            // 
            this.labSeconds.AutoSize = true;
            this.labSeconds.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSeconds.ForeColor = System.Drawing.Color.MediumBlue;
            this.labSeconds.Location = new System.Drawing.Point(18, 98);
            this.labSeconds.Name = "labSeconds";
            this.labSeconds.Size = new System.Drawing.Size(15, 14);
            this.labSeconds.TabIndex = 15;
            this.labSeconds.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "重连用时：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSpend);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 108);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // txtSpend
            // 
            this.txtSpend.AutoSize = true;
            this.txtSpend.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSpend.ForeColor = System.Drawing.Color.MediumBlue;
            this.txtSpend.Location = new System.Drawing.Point(7, 38);
            this.txtSpend.Name = "txtSpend";
            this.txtSpend.Size = new System.Drawing.Size(15, 14);
            this.txtSpend.TabIndex = 18;
            this.txtSpend.Text = "0";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 16);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.TabIndex = 17;
            this.label38.Text = "总用时：";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(11, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 187);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(11, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(93, 174);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            // 
            // btnPaused
            // 
            this.btnPaused.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaused.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPaused.ForeColor = System.Drawing.Color.Black;
            this.btnPaused.Location = new System.Drawing.Point(6, 84);
            this.btnPaused.Name = "btnPaused";
            this.btnPaused.Size = new System.Drawing.Size(75, 23);
            this.btnPaused.TabIndex = 18;
            this.btnPaused.Text = "暂停";
            this.btnPaused.UseVisualStyleBackColor = true;
            this.btnPaused.Click += new System.EventHandler(this.btnPaused_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnStopSound);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnStart);
            this.groupBox4.Controls.Add(this.btnStop);
            this.groupBox4.Controls.Add(this.btnPaused);
            this.groupBox4.Location = new System.Drawing.Point(15, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(92, 504);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "控制栏";
            // 
            // btnStopSound
            // 
            this.btnStopSound.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStopSound.Location = new System.Drawing.Point(6, 230);
            this.btnStopSound.Name = "btnStopSound";
            this.btnStopSound.Size = new System.Drawing.Size(75, 23);
            this.btnStopSound.TabIndex = 20;
            this.btnStopSound.Text = "静音";
            this.btnStopSound.UseVisualStyleBackColor = true;
            this.btnStopSound.Click += new System.EventHandler(this.btnStopSound_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(6, 191);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "清缓存";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupControl);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(817, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 519);
            this.panel1.TabIndex = 21;
            // 
            // txtDialCount
            // 
            this.txtDialCount.Location = new System.Drawing.Point(116, 134);
            this.txtDialCount.Name = "txtDialCount";
            this.txtDialCount.Size = new System.Drawing.Size(61, 21);
            this.txtDialCount.TabIndex = 59;
            this.txtDialCount.Text = "3";
            this.txtDialCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 60;
            this.label4.Text = "每IP扫号数：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 519);
            this.Controls.Add(this.tabWebDetection);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FrmMain";
            this.Text = "GuildWar 对号";
            this.tabWebDetection.ResumeLayout(false);
            this.pageParams.ResumeLayout(false);
            this.pageParams.PerformLayout();
            this.groupRestartMode.ResumeLayout(false);
            this.groupRestartMode.PerformLayout();
            this.pageDisplay.ResumeLayout(false);
            this.tagTest.ResumeLayout(false);
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabControl tabWebDetection;
        private System.Windows.Forms.TabPage pageDisplay;
        private System.Windows.Forms.ProgressBar progressProcess;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.GroupBox groupControl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labSeconds;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPaused;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label txtUnknowCount;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label txtFailedCount;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label txtSuccessCount;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label txtProcessedCount;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label txtTotalCount;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label txtRemainCount;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label txtSpend;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage pageParams;
        private System.Windows.Forms.Label txtLastInfo;
        private System.Windows.Forms.RadioButton radioCustomRange;
        private System.Windows.Forms.RadioButton radioFromStopped;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtUpper;
        private System.Windows.Forms.TextBox txtLower;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.CheckBox chkReconnect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRepeatCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboDataType;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboServer;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupRestartMode;
        private System.Windows.Forms.RadioButton radioVpn;
        private System.Windows.Forms.RadioButton radioRouter;
        private System.Windows.Forms.RadioButton radioADSL;
        private System.Windows.Forms.RadioButton radioFromFirst;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStopSound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaptchCount;
        private System.Windows.Forms.Panel panelNet;
        private System.Windows.Forms.TabPage tagTest;
        private System.Windows.Forms.Panel panelWeb;
        private System.Windows.Forms.TextBox txtDialCount;
        private System.Windows.Forms.Label label4;
    }
}

