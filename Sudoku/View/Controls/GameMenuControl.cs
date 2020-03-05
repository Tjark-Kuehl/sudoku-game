using System;
using System.Windows.Forms;
using Sudoku.Control;

namespace Sudoku.View.Controls
{
    public partial class GameMenuControl : UserControl
    {
        private readonly Game _game;
        public event EventHandler<EventArgs> OnGameFinished;
        private int Score
        {
            get { return int.Parse(score.Text); }
            set { score.Text = value.ToString(); }
        }
        private int Time
        {
            get { return int.Parse(time.Text); }
            set { time.Text = value.ToString(); }
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            _game.End();
            OnGameFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
