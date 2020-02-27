using System;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class PlayerSelectionMenuControl : UserControl
    {
        public event EventHandler<Player> OnUserSelected;
        private readonly PlayerManager _playerManager;

        public PlayerSelectionMenuControl(PlayerManager playerManager)
        {
            _playerManager = playerManager;

            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            tableLayoutPanel1.Controls.Clear(); 
            tableLayoutPanel1.RowStyles.Clear();
            foreach (var player in _playerManager.Players)
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
            }
        }

        private void createNewPlayerButton_Click(object sender, EventArgs e)
        {
            var popup = new CreatePlayerPopupControl();
            if (popup.ShowDialog() != DialogResult.OK) return;
            _playerManager.RegisterPlayer(new Player(popup.NewPlayerName));
            UpdateList();
        }
    }
}
