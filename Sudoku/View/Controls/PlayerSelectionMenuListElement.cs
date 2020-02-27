using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku.Model;

namespace Sudoku.View.Controls
{
    public partial class PlayerSelectionMenuListElement : UserControl
    {
        private readonly Player _player;
        private readonly Action<Player> _onSelect;

        public PlayerSelectionMenuListElement(Player player, Action<Player> select)
        { 
            _player = player;
            _onSelect = select;
            InitializeComponent();
            button1.Text = $"{player.Name} (Score: {player.Score})";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _onSelect?.Invoke(_player);
        }
    }
}
