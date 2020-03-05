using System;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View
{
    public partial class ChooseDifficultyForm : Form
    {
        private readonly IGameLoader _loader;

        public SudokuGenerator.GameDifficulty ChoosenDifficulty = null;

        public string Title
        {
            set => label1.Text = value;
        }

        public ChooseDifficultyForm(Player player, IGameLoader loader)
        {
            _loader = loader;
            InitializeComponent();
            button4.Enabled = loader.HasSaveFile(player, out _);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Easy.Default;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Medium.Default;
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Hard.Default;
            this.DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.DialogResult = DialogResult.Yes;
        }
    }
}
