using System;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A main menu control.
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        /// <summary>
        ///     Occurs when End Game.
        /// </summary>
        public event EventHandler<EventArgs> EndGame;
        /// <summary>
        ///     Occurs when Start Game.
        /// </summary>
        public event EventHandler<EventArgs> StartGame;
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainMenuControl"/> class.
        /// </summary>
        public MainMenuControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Event handler. Called by button2 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button2_Click(object sender, EventArgs e)
        {
            EndGame?.Invoke(this, e);
        }

        /// <summary>
        ///     Event handler. Called by button1 for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void button1_Click(object sender, EventArgs e)
        {
            StartGame?.Invoke(this, e);
        }
    }
}
