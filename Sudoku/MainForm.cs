using Sudoku.Control;
using Sudoku.View.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using Sudoku.View;

namespace Sudoku
{
    public partial class MainForm : Form
    {
        private IGameLoader _loader;
        private PlayerManager _manager;

        public MainForm()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            Size s = Size;
            ResizeEnd += (sender, args) => { this.Refresh(); };
            ResizeBegin += (sender, args) => { s = Size; };
            Resize += (sender, args) =>
            {
                int m = Math.Max(Size.Width, Size.Height);
                if (Size.Width < s.Width || Size.Height < s.Height)
                {
                    m = Math.Min(Size.Width, Size.Height);
                }
                Size = new Size(m, m + 50);
            };
            _loader = new GameLoader();
            _manager = new PlayerManager();

            MainMenuControl mmc = new MainMenuControl
            {
                Dock = DockStyle.Fill
            };
            mmc.EndGame += (s, e) =>
            {
                Close();
            };
            mmc.StartGame += (s, e) =>
            {
                Controls.Remove(mmc);

                PlayerSelectionMenuControl psm = new PlayerSelectionMenuControl(_manager)
                {
                    Dock = DockStyle.Fill
                };

                psm.OnUserSelected += (sender, player) =>
                {
                    var game = new Game(player);

                    var cdf = new ChooseDifficultyForm(game, _loader)
                    {
                        Text = $"player '{player.Name}'",
                        Title = "choose difficulty or load a save game",
                        StartPosition = FormStartPosition.CenterParent
                    };
                    if (cdf.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    Controls.Remove(psm);
                    Controls.Add(new GameMenuControl(game)
                    {
                        Dock = DockStyle.Fill
                    });

                };
                Controls.Add(psm);
            };
            Controls.Add(mmc);
        }
    }
}
