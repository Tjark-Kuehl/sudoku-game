using System;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            new Form1().ShowDialog();
            this.Opacity = 1;
        }
    }
}
