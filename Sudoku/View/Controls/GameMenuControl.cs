using System;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Sudoku.Control;
using Timer = System.Timers.Timer;

namespace Sudoku.View.Controls
{
    public partial class GameMenuControl : UserControl
    {
        public int Score
        {
            get { return int.Parse(label2.Text); }
            set { label2.Text = value.ToString(); }
        }
        public int Time
        {
            get { return int.Parse(label3.Text); }
            set{label3.Text = value.ToString();}
        }

        public GameMenuControl(Game game)
        {
            InitializeComponent();
            this.tableLayoutPanel1.Controls.Add(new GameControl(game, this)
            {
                Dock = DockStyle.Fill
            }, 0, 1);
            Timer t = new Timer(1000);
            t.Elapsed += (sender, args) =>
            {
                if (label3.InvokeRequired)
                {
                    label3.Invoke(new Action(() => { Time += 1; }));
                    return;
                }
                Time += 1;
            };
            t.Start();
        }
    
    }
}
