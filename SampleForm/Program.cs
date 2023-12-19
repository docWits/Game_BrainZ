using System;
using System.Windows.Forms;

namespace SampleForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения. 
        /// </summary>
        [STAThread()]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}
