using System;
using System.IO;
using System.Windows.Forms;
using Sudoku.View;

namespace Sudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            for (int i = 0; i < 1000; i+=1)
            {
                Console.WriteLine(i + ": " + ScoreFactor(i * 1000));
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static float ScoreFactor(int value)
        {
            return 1f + (3f / ((value / 2000f) + 1f));
        }
    }
}
