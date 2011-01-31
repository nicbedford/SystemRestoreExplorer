using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SystemRestoreExplorer
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    MessageBox.Show("This software only supports Windows Vista or later", "Error");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
    }
}
