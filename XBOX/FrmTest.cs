using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XBOX.Http;
using PublicUtilities;
using System.Threading;

namespace XBOX
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            DetectionParamsItem paramsItem = new DetectionParamsItem();
            paramsItem.CurrentGameServer = GameServers.XBOXServer;
            AccountItem account = new AccountItem();
            account.User = "testlzc@hotmail.com";
            account.Password = "122603lzc!@#";
            UCXBoxLogin uc = new UCXBoxLogin(paramsItem, XBOXLogManager.Instance) { Dock = DockStyle.Fill };
            panel1.Controls.Add(uc);
            using (uc)
            {
                uc.GetState(account, new AutoResetEvent(false));
            }
        }
    }
}
