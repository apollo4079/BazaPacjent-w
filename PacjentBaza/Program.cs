using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacjentBaza
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

            LogonForm logon = new LogonForm();

            Application.Run(logon);

            if (logon.LogonSuccessful)
            {
                Application.Run(new Form1());
                logon.Dispose();
            }
        }
    }
}
