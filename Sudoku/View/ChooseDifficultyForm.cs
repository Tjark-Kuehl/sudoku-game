using System;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View
{
    /// <summary>
    ///     Form for viewing the choose difficulty.
    /// </summary>
    public partial class ChooseDifficultyForm : Form
    {
        /// <summary>
        ///     The loader.
        /// </summary>
        private readonly IGameLoader _loader;

        /// <summary>
        ///     The choosen difficulty.
        /// </summary>
        public SudokuGenerator.GameDifficulty ChoosenDifficulty = null;

        /// <summary>
        ///     Sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        public string Title
        {
            set => label1.Text = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChooseDifficultyForm"/> class.
        /// </summary>
        /// <param name="player"> The player. </param>
        /// <param name="loader"> The loader. </param>
        public ChooseDifficultyForm(Player player, IGameLoader loader)
        {
            _loader = loader;
            InitializeComponent();
            button4.Enabled = loader.HasSaveFile(player, out _);
        }

        /// <summary>
        ///     Event handler. Called by button1 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Easy.Default;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     Event handler. Called by button2 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Medium.Default;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     Event handler. Called by button3 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ChoosenDifficulty = SudokuGenerator.GameDifficulty.Hard.Default;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     Event handler. Called by button4 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.DialogResult = DialogResult.Yes;
        }
    }
}
