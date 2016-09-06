using System;
using System.Threading;
using System.Windows.Forms;

namespace DumpertNotifier
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Mutex mutex = new Mutex(false, "Global\\" + AppGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show(@"Instance already running");
                    return;
                }
                Application.Run(new DnForm());
            }
        }
        private const string AppGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
    }
}