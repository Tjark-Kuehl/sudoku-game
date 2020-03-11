using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;
using Sudoku.View;

namespace Sudoku
{
    /// <summary>
    ///     A program.
    /// </summary>
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
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
