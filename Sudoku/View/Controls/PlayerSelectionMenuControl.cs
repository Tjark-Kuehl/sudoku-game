using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class PlayerSelectionMenuControl : UserControl
    {
        public event EventHandler<Player> OnUserSelected;

        private PlayerManager _playerManager;

        public PlayerSelectionMenuControl(PlayerManager playerManager)
        {
            _playerManager = playerManager;

            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            tableLayoutPanel1.Controls.Clear();
            foreach (var player in _playerManager.Players)
            {
                var p = new PlayerSelectionMenuListElement(player, OnElementWasSelected)
                {
                    Dock = DockStyle.Fill
                };
                var i = tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                tableLayoutPanel1.Controls.Add(p, 0, i);
            }
        }

        private void OnElementWasSelected(Player player)
        {
            OnUserSelected?.Invoke(this, player);
        }

        private void createNewPlayerButton_Click(object sender, EventArgs e)
        {
            var popup = new CreatePlayerPopupControl();
            if (popup.ShowDialog() == DialogResult.OK)
            {
                _playerManager.RegisterPlayer(new Player(popup.NewPlayerName));
                UpdateList();
            }
        }
    }
}
