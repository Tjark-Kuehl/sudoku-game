using System;
using System.Windows.Forms;
using Sudoku.Control;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A game menu control.
    /// </summary>
    public partial class GameMenuControl : UserControl
    {
        /// <summary>
        ///     The game.
        /// </summary>
        private readonly Game _game;
        /// <summary>
        ///     Occurs when On Game Finished.
        /// </summary>
        public event EventHandler<EventArgs> OnGameFinished;
        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        private int Score
        {
            get { return int.Parse(score.Text); }
            set { score.Text = value.ToString(); }
        }
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        private int Time
        {
            get { return int.Parse(time.Text); }
            set { time.Text = value.ToString(); }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameMenuControl"/> class.
        /// </summary>
        /// <param name="game"> The game. </param>
        public GameMenuControl(Game game)
        {
            _game = game;
            InitializeComponent();
            Score = game.Score;
            Time = game.Time;
            var gameControl = new GameControl(game, this)
            {
                Dock = DockStyle.Fill
            };
            this.tableLayoutPanel1.Controls.Add(gameControl, 0, 1);

            game.OnTimeChanged += (sender, t) =>
            {
                if (time.InvokeRequired)
                {
                    time.Invoke((MethodInvoker) delegate { Time = t; });
                    return;
                }

                Time = t;
            };

            game.OnScoreChanged += (sender, s) =>
            {
                if (score.InvokeRequired)
                {
                    score.Invoke((MethodInvoker)delegate { Score = s; });
                    return;
                }

                Score = s;
            };

            gameControl.OnGameFinished += (sender, args) =>
            {
                game.End();
                OnGameFinished?.Invoke(this, args);
            };
            game.Start();
        }
    }
}
