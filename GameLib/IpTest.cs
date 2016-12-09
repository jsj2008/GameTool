using System;
using System.Collections.Generic;
using System.Text;
using DotRas;
using System.Threading;

namespace PublicUtilities
{
    public class IpTest
    {
        public static void GetIp()
        {
            // The connection attempt has completed, attempt to find the connection in the active connections.
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                LogManager.Info(string.Format("-> VPN拨号正在断开:{0}  的连接！", connection.EntryName));
                // The connection has been found, disconnect it.
                connection.HangUp();
            }
        }
    }
}
