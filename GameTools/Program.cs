using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new FrmMain());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (null != ex)
            {
                WowLogManager.Instance.Fatal(string.Format("CurrentDomain_UnhandledException:{0}", ex.StackTrace));
            }
            else
            {
                WowLogManager.Instance.Fatal(string.Format("CurrentDomain_UnhandledException:{0}", e.ExceptionObject));
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            WowLogManager.Instance.Fatal(string.Format("Application_ThreadException:{0}", e.Exception.StackTrace));
        }

        private static void TestRegex()
        {
            string s = string.Empty;
            bool result = false;
            s = "12345";
            s = s.Remove(2, 1);
            s = s.Substring(0, 2 - 1) + s.Substring(2);
            s = s.Substring(0, 2 + 1) + s.Substring(2 + 2);

            s = s.Substring(0, 2 + 1) + s.Substring(2 + 1);
            s = s.Insert(1, "222");
            result = Regex.IsMatch(s, @"^\d+$");
            result = Regex.IsMatch(s, @"^\d*$");

            s = "12cc";
            result = Regex.IsMatch(s, @"^\d+$");
            result = Regex.IsMatch(s, @"^\d*$");

            s = "12cc0";
            result = Regex.IsMatch(s, @"[^A-Za-z\d]+");
        }
    }
}
