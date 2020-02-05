using System;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

            SizeChanged += Form1_SizeChanged;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Size.Width != Size.Height)
            {
                int m = Math.Max(Size.Width, Size.Height);
                SizeChanged -= Form1_SizeChanged;
                Size = new System.Drawing.Size(m, m);
                SizeChanged += Form1_SizeChanged;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            field1.Invalidate();
        }
    }
}
