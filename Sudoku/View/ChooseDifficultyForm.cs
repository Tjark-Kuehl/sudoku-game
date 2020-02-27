using System;
using System.Windows.Forms;
using Sudoku.Control;

namespace Sudoku.View
{
    public partial class ChooseDifficultyForm : Form
    {
        private readonly Game _game;
        private readonly IGameLoader _loader;

        public string Title
        {
            set => label1.Text = value;
        }

        public ChooseDifficultyForm( Game game, IGameLoader loader)
        {
            _game = game;
            _loader = loader;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            _game.Load(SudokuGenerator.GameDifficulty.Easy);
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            _game.Load(SudokuGenerator.GameDifficulty.Medium);
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            _game.Load(SudokuGenerator.GameDifficulty.Hard);
            this.DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (_game.Load(_loader))
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
