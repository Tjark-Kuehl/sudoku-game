using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class GameEndControl : UserControl
    {
        public event EventHandler<EventArgs> OnBackToMainMenuClicked;

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

        private void toMainMenu_button_Click(object sender, EventArgs e)
        {
            OnBackToMainMenuClicked?.Invoke(this, e);
        }
    }
}
