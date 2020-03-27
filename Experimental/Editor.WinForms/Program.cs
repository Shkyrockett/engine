using System;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using Form1 mainForm = new Form1();
            Application.Run(mainForm);
        }
    }
}
