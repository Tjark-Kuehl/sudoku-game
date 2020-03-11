using System;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A player selection menu control.
    /// </summary>
    public partial class PlayerSelectionMenuControl : UserControl
    {
        /// <summary>
        ///     Occurs when On User Selected.
        /// </summary>
        public event EventHandler<Player> OnUserSelected;
        /// <summary>
        ///     The player loader.
        /// </summary>
        private readonly IPlayerLoader _playerLoader;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerSelectionMenuControl"/> class.
        /// </summary>
        /// <param name="playerLoader"> The player loader. </param>
        public PlayerSelectionMenuControl(IPlayerLoader playerLoader)
        {
            _playerLoader = playerLoader;

            InitializeComponent();
            UpdateList();
        }

        /// <summary>
        ///     Updates the list.
        /// </summary>
        private void UpdateList()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            foreach (var player in _playerLoader.LoadPlayers())
            {
                AddPlayer(player);
            }
        }

        /// <summary>
        ///     Adds a player.
        /// </summary>
        /// <param name="player"> The player. </param>
        /// <returns>
        ///     A Player.
        /// </returns>
        Player AddPlayer(Player player)
        {
            var p = new Button
            {
                Dock = DockStyle.Fill,
                Text = $"{player.Name} ({player.Score})"
            };
            p.Click += (sender, args) =>
            {
                OnUserSelected?.Invoke(this, player);
            };
            var i = tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tableLayoutPanel1.Controls.Add(p, 0, i);
            return player;
        }

        /// <summary>
        ///     Event handler. Called by createNewPlayerButton for click events.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        private void createNewPlayerButton_Click(object sender, EventArgs e)
        {
            var popup = new CreatePlayerPopupControl();
            if (popup.ShowDialog() != DialogResult.OK) return;
            _playerLoader.SavePlayer(AddPlayer(new Player(popup.NewPlayerName)));
        }
    }
}
