using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PublicUtilities;

namespace PublicUtilities
{
    public partial class UcProgress : UserControl
    {
        public UcProgress()
        {
            InitializeComponent();
        }

        public void SetCount(ProgressEventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                this.progressBar.Value = e.CurrentPercent;
                this.txtCount.Text = string.Format("第{0}条", e.CurrentLine);
            }));
        }

        #region Timer

        System.Windows.Forms.Timer timer = null;
        int timerCount = 0;

        public void StartTimer()
        {
            //this.BeginInvoke(new ThreadStart(delegate()
            // {
            if (null == timer)
            {
                timer = new System.Windows.Forms.Timer();
                //1s
                timer.Interval = 1000;
                timer.Tick += timer_Tick;
            }

            this.StopTimer();
            timerCount = 0;
            timer.Start();
            //}));
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            txtTime.Text = TimeSpan.FromSeconds(timerCount).ToString();
        }

        public void StopTimer()
        {
            //this.BeginInvoke(new ThreadStart(delegate()
            // {
            if ((null != timer) && timer.Enabled)
            {
                timer.Stop();
            }
            //}));
        }

        #endregion

    }
}
