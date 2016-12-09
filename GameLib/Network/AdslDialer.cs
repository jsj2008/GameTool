using System;
using System.Net;
using System.Threading;
using DotRas;

namespace PublicUtilities
{
    public class AdslDialer : IDisposable
    {
        //Unit: second
        private const int ThreadSleepTime = 5;

        private RasPhoneBook rasPhoneBook = new RasPhoneBook();
        private RasDialer rasDialer = new RasDialer();
        private bool isDialing = false;

        private int Timeout
        { get; set; }

        private ADSLItem ADSLItem
        { get; set; }

        private LogManagerBase LogManager
        {
            get;
            set;
        }

        public AdslDialer(ADSLItem adslItem, LogManagerBase logManager)
        {
            this.LogManager = logManager;
            if (null == adslItem)
            {
                LogManager.Error("ADSL dialer parameters can't be null!");
            }

            this.ADSLItem = adslItem;
            this.Timeout = 5 * 60 * 1000;

            this.rasDialer.StateChanged += rasDialer_StateChanged;
            this.rasDialer.DialCompleted += rasDialer_DialCompleted;
        }

        void rasDialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            LogManager.Info(string.Format("-> ADSL 拨号当前状态:{0}", e.State));
        }

        void rasDialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                LogManager.InfoWithCallback(string.Format("-> ADSL 拨号：名称＝{0}，用户={1} 拨号被取消！", this.ADSLItem.EntryName, this.ADSLItem.User));
            }
            else if (e.TimedOut)
            {
                LogManager.InfoWithCallback(string.Format("-> ADSL 拨号：名称＝{0}，用户={1} 拨号超时！", this.ADSLItem.EntryName, this.ADSLItem.User));
            }
            else if (e.Error != null)
            {
                LogManager.InfoWithCallback(string.Format("-> ADSL 拨号：名称＝{0}，用户={1} 拨号出错：{2}！", this.ADSLItem.EntryName, this.ADSLItem.User, e.Error));
            }
            else if (e.Connected)
            {
                LogManager.InfoWithCallback(string.Format("-> 恭喜，ADSL 拨号：名称＝{0}，用户={1} 拨号成功！", this.ADSLItem.EntryName, this.ADSLItem.User));
            }

            this.isDialing = false;
        }

        private bool CreateADSL()
        {
            if (null == this.ADSLItem)
            {
                return false;
            }
            // This opens the phonebook so it can be used. Different overloads here will determine where the phonebook is opened/created.
            this.rasPhoneBook.Open();
            if (this.rasPhoneBook.Entries.Count > 0)
            {
                foreach (RasEntry rasEntry in this.rasPhoneBook.Entries)
                {
                    if (rasEntry.Name.Equals(this.ADSLItem.EntryName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //LogManager.InfoWithCallback(string.Format("-> ADSL 拨号新IP:{0}", this.ADSLItem.IP));
                        //rasEntry.PhoneNumber = this.ADSLItem.IP;
                        rasEntry.PhoneNumber = " ";
                        return false;
                    }
                }
            }

            // Create the entry that will be used by the dialer to dial the connection. Entries can be created manually, however the static methods on
            // the RasEntry class shown below contain default information matching that what is set by Windows for each platform.
            RasEntry entry = new RasEntry(this.ADSLItem.EntryName);
            entry.Device = RasDevice.GetDeviceByName("(PPPOE)", RasDeviceType.PPPoE, false);
            entry.FramingProtocol = RasFramingProtocol.Ppp;
            // entry.NetworkProtocols = RasNetworkProtocols;
            entry.RedialCount = 3;
            entry.RedialPause = 60;
            entry.PhoneNumber = string.Empty;
            //Must set to None
            entry.EncryptionType = RasEncryptionType.None;
            // entry.PrerequisiteEntryName
            // Add the new entry to the phone book.
            this.rasPhoneBook.Entries.Add(entry);
            return true;
        }

        public bool Dial()
        {
            this.HangUp();
            //this.CreateADSL();

            this.isDialing = true;
            // This button will be used to dial the connection.
            this.rasDialer.EntryName = this.ADSLItem.EntryName;
            this.rasDialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            //MS 5 Mins
            this.rasDialer.Timeout = this.Timeout;

            try
            {
                // Set the credentials the dialer should use.
                this.rasDialer.Credentials = new NetworkCredential(this.ADSLItem.User, this.ADSLItem.Password);
                this.rasDialer.AllowUseStoredCredentials = true;
                //this.rasDialer.
                // NOTE: The entry MUST be in the phone book before the connection can be dialed.
                // Begin dialing the connection; this will raise events from the dialer instance.
                RasHandle rasHandle = this.rasDialer.Dial();
                bool b = !rasHandle.IsInvalid;
                return b;
            }
            catch (Exception ex)
            {
                LogManager.ErrorWithCallback(string.Format("-> ADSL拨号异常：{0}", ex.ToString()));
                this.isDialing = false;
            }
            return false;
        }

        public void HangUp()
        {
            try
            {
                if (this.isDialing)
                {
                    // The connection attempt has not been completed, cancel the attempt.
                    LogManager.Info("-> ADSL拨号正在取消上一次拨号！");
                    this.rasDialer.DialAsyncCancel();
                }
                else
                {
                    // The connection attempt has completed, attempt to find the connection in the active connections.
                    foreach (RasConnection connection in RasConnection.GetActiveConnections())
                    {
                        if (connection.EntryName == this.ADSLItem.EntryName)
                        {
                            LogManager.InfoWithCallback(string.Format("-> ADSL拨号正在断开:'{0}' 的连接！", this.ADSLItem.EntryName));
                            // The connection has been found, disconnect it.
                            connection.HangUp();
                            Thread.Sleep(3 * 1000);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.ErrorWithCallback(string.Format("-> ADSL断开异常：{0}", ex.ToString()));
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
