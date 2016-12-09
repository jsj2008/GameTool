using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Apple.Http;
using PublicUtilities.Helpers;

namespace Apple
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            CookieCleaner.CleanCookies("apple");
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(new UcAppleLogin(GameServers.AppleServer, "lzcj4@163.com", "122603Lzc!@#") { Dock= DockStyle.Fill});
        }
    }
}
