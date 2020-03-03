using System;
using System.Windows.Forms;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class GameEndControl : UserControl
    {
        public event EventHandler<EventArgs> OnBackToMainMenuClicked;
        public GameEndControl(Player player, int time, int score)
        {
            InitializeComponent();
            score_label.Text += score;
            time_label.Text += time + " Sekunden";
        }

        private void toMainMenu_button_Click(object sender, EventArgs e)
        {
            OnBackToMainMenuClicked?.Invoke(this, e);
        }
    }
}
