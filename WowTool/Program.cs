using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PublicUtilities;

namespace WebDetection
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
            Application.Run(new FrmWebDetection());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (null != ex)
            {
                WowLogManager.Instance.Fatal(string.Format("(Wow) CurrentDomain_UnhandledException:{0}", ex.StackTrace));
            }
            else
            {
                WowLogManager.Instance.Fatal(string.Format("(Wow) CurrentDomain_UnhandledException:{0}", e.ExceptionObject));
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            WowLogManager.Instance.Fatal(string.Format("(Wow) Application_ThreadException:{0}", e.Exception.StackTrace));
        }
    }

}
