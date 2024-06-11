using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//University Project for lesson
//"Object-Oriented development"
//2023-2024
//project and assets from Ioanna Andrianou
// AKA Ιωάννα Ανδριανού
//(Tarmac on GitHub)

namespace ergasia
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
