using System;
using System.Net;
using System.Threading;
using DotRas;

namespace PublicUtilities
{
    public class VPNDialer : IDisposable
    {
        //Unit: second
        private const int ThreadSleepTime = 5;

        private RasPhoneBook rasPhoneBook = new RasPhoneBook();
        private RasDialer rasDialer = new RasDialer();
        private ManualResetEvent vpnManualReset = null;
        private bool isDialing = false;

        private int Timeout
        { get; set; }

        private VPNItem VPNItem
        { get; set; }

        private LogManagerBase LogManager
        { get; set; }

        public bool IsDialSucceed
        { get; private set; }

        public VPNDialer(VPNItem vpnItem, ManualResetEvent manualReset, LogManagerBase logManager)
        {
            this.LogManager = logManager;
            if ((null == vpnItem) || (null == manualReset))
            {
                LogManager.Error("Vpn dialer parameters can't be null!");
            }

            this.VPNItem = vpnItem;
            this.vpnManualReset = manualReset;
            this.Timeout = 5 * 60 * 1000;

            this.rasDialer.StateChanged += rasDialer_StateChanged;
            this.rasDialer.DialCompleted += rasDialer_DialCompleted;
        }

        void rasDialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            LogManager.Info(string.Format("-> VPN 拨号当前状态:{0}", e.State));
        }

        void rasDialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                LogManager.InfoWithCallback(string.Format("-> VPN 拨号：名称＝{0}，IP={1} 拨号被取消！", this.VPNItem.EntryName, this.VPNItem.IP));
            }
            else if (e.TimedOut)
            {
                LogManager.InfoWithCallback(string.Format("-> VPN 拨号：名称＝{0}，IP={1} 拨号超时！", this.VPNItem.EntryName, this.VPNItem.IP));
            }
            else if (e.Error != null)
            {
                LogManager.InfoWithCallback(string.Format("-> VPN 拨号：名称＝{0}，IP={1} 拨号出错：{2}！", this.VPNItem.EntryName, this.VPNItem.IP, e.Error));
            }
            else if (e.Connected)
            {
                this.IsDialSucceed = true;
                LogManager.InfoWithCallback(string.Format("-> 恭喜，VPN 拨号：名称＝{0}，IP={1} 拨号成功！", this.VPNItem.EntryName, this.VPNItem.IP));
            }

            this.isDialing = false;
            this.vpnManualReset.Set();
        }

        private bool CreateVpn()
        {
            if (null == this.VPNItem)
            {
                return false;
            }
            // This opens the phonebook so it can be used. Different overloads here will determine where the phonebook is opened/created.
            this.rasPhoneBook.Open();
            if (this.rasPhoneBook.Entries.Count > 0)
            {
                foreach (RasEntry rasEntry in this.rasPhoneBook.Entries)
                {
                    if (rasEntry.Name.Equals(this.VPNItem.EntryName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        LogManager.InfoWithCallback(string.Format("-> VPN 拨号新IP:{0}", this.VPNItem.IP));
                        rasEntry.PhoneNumber = this.VPNItem.IP;
                        return false;
                    }
                }
            }

            // Create the entry that will be used by the dialer to dial the connection. Entries can be created manually, however the static methods on
            // the RasEntry class shown below contain default information matching that what is set by Windows for each platform.
            RasEntry entry = RasEntry.CreateVpnEntry(this.VPNItem.EntryName, this.VPNItem.IP, RasVpnStrategy.Default,
                                                     RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));

            //Must set to None
            entry.EncryptionType = RasEncryptionType.None;
            // entry.PrerequisiteEntryName
            // Add the new entry to the phone book.
            this.rasPhoneBook.Entries.Add(entry);
            return true;
        }

        public void Dial()
        {
            this.Disconnect();
            this.CreateVpn();

            this.isDialing = true;
            // This button will be used to dial the connection.
            this.rasDialer.EntryName = this.VPNItem.EntryName;
            this.rasDialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            //MS 5M
            this.rasDialer.Timeout = this.Timeout;

            try
            {
                // Set the credentials the dialer should use.
                this.rasDialer.Credentials = new NetworkCredential(this.VPNItem.User, this.VPNItem.Password);
                this.rasDialer.AllowUseStoredCredentials = true;
                this.rasDialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

                //this.rasDialer.
                // NOTE: The entry MUST be in the phone book before the connection can be dialed.
                // Begin dialing the connection; this will raise events from the dialer instance.
                this.rasDialer.DialAsync();
            }
            catch (Exception ex)
            {
                LogManager.ErrorWithCallback(string.Format("-> VPN拨号异常：{0}", ex.ToString()));
                this.vpnManualReset.Set();
                this.isDialing = false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (this.isDialing)
                {
                    // The connection attempt has not been completed, cancel the attempt.
                    LogManager.Info("-> VPN拨号正在取消上一次拨号！");
                    this.rasDialer.DialAsyncCancel();
                }
                else
                {
                    // The connection attempt has completed, attempt to find the connection in the active connections.
                    foreach (RasConnection connection in RasConnection.GetActiveConnections())
                    {
                        if (connection.EntryName == this.VPNItem.EntryName)
                        {
                            LogManager.Info(string.Format("-> VPN拨号正在断开:{0}  的连接！", connection.EntryName));
                            // The connection has been found, disconnect it.
                            connection.HangUp();
                            Thread.Sleep(ThreadSleepTime * 1000);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.ErrorWithCallback(string.Format("-> VPN断开异常：{0}", ex.ToString()));
            }
        }

        public void Dispose()
        {
            using (this.rasDialer)
            {
                this.rasDialer.StateChanged -= rasDialer_StateChanged;
                this.rasDialer.DialCompleted -= rasDialer_DialCompleted;
            }
        }
    }
}
