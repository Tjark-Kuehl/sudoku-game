using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;
using Sudoku.View;

namespace Sudoku
{
    static class Program
    {
        private static readonly byte[] Possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
