using System;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    public partial class MainMenuControl : UserControl
    {
        public event EventHandler<EventArgs> EndGame;
        public event EventHandler<EventArgs> StartGame;
        public MainMenuControl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EndGame?.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame?.Invoke(this, e);
        }
    }
}
