using System;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class PlayerSelectionMenuControl : UserControl
    {
        public event EventHandler<Player> OnUserSelected;
        private readonly IPlayerLoader _playerLoader;

        public PlayerSelectionMenuControl(IPlayerLoader playerLoader)
        {
            _playerLoader = playerLoader;

            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            foreach (var player in _playerLoader.LoadPlayers())
            {
                AddPlayer(player);
            }
        }

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

        private void createNewPlayerButton_Click(object sender, EventArgs e)
        {
            var popup = new CreatePlayerPopupControl();
            if (popup.ShowDialog() != DialogResult.OK) return;
            _playerLoader.SavePlayer(AddPlayer(new Player(popup.NewPlayerName)));
        }
    }
}
