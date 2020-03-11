using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A game end control.
    /// </summary>
    public partial class GameEndControl : UserControl
    {
        /// <summary>
        ///     Occurs when On Back To Main Menu Clicked.
        /// </summary>
        public event EventHandler<EventArgs> OnBackToMainMenuClicked;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameEndControl"/> class.
        /// </summary>
        /// <param name="game"> The game. </param>
        public GameEndControl(Game game)
        {
            InitializeComponent();
            score_label.Text += game.Score;
            time_label.Text += game.Time + " Sekunden";
            FieldRenderer renderer = new FieldRenderer();
            int index = 0;

            IList<Field> solutions = new List<Field>();
            foreach (Field f in SudokuGenerator.Solutions(game.Field))
            {
                solutions.Add(f.Copy());
            }
            label2.Text = $"{index + 1}/{solutions.Count}";
            panel2.Paint += (sender, args) =>
            {
                renderer.PaintBoard(args.Graphics, panel2.Width, panel2.Height, solutions[index]);
            };
            button1.Click += (sender, args) =>
            {
                if (index - 1 >= 0)
                {
                    index--;
                    Refresh();
                    label2.Text = $"{index + 1}/{solutions.Count}";
                }
            };
            button2.Click += (sender, args) =>
            {
                if (index + 1 < solutions.Count)
                {
                    index++;
                    Refresh();
                    label2.Text = $"{index + 1}/{solutions.Count}";
                }
            };
        }

        /// <summary>
        ///     Event handler. Called by toMainMenu_button for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void toMainMenu_button_Click(object sender, EventArgs e)
        {
            OnBackToMainMenuClicked?.Invoke(this, e);
        }
    }
}
