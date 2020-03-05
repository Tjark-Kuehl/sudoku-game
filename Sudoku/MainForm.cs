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
        private IGameLoader _gameLoader;
        private IPlayerLoader _playerLoader;

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
            _gameLoader = new GameLoader();
            _playerLoader = new PlayerLoader();
            LoadMainMenu();
        }


        public void LoadMainMenu()
        {
            Controls.Clear();
            var mmc = new MainMenuControl
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

                PlayerSelectionMenuControl psm = new PlayerSelectionMenuControl(_playerLoader)
                {
                    Dock = DockStyle.Fill
                };

                psm.OnUserSelected += (sender, player) =>
                {

                    var cdf = new ChooseDifficultyForm(player, _gameLoader)
                    {
                        Text = $"player '{player.Name}'",
                        Title = "choose difficulty or load a save game",
                        StartPosition = FormStartPosition.CenterParent
                    };
                    var res = cdf.ShowDialog();
                    Game game;
                    switch (res)
                    {
                        case DialogResult.OK:
                            game = new Game(player);
                            game.Load(cdf.ChoosenDifficulty); 
                            break;
                        case DialogResult.Yes:
                            _gameLoader.Load(player, out game);
                            break;
                        default:
                            return;
                    }

                    Controls.Remove(psm);
                    var gmc = new GameMenuControl(game)
                    {
                        Dock = DockStyle.Fill
                    };
                    gmc.OnGameFinished += (ss, ee) =>
                    {
                        Controls.Remove(gmc);
                        player.GameCount += 1;
                        player.Playtime += game.Time;
                        player.Score += game.Score;
                        _playerLoader.SavePlayer(player);

                        var gec = new GameEndControl(game)
                        {
                            Dock = DockStyle.Fill
                        };
                        gec.OnBackToMainMenuClicked += (sss, eee) =>
                        {
                           LoadMainMenu();
                        };
                        Controls.Add(gec);

                    };
                   
                    void Close(object sender, EventArgs e)
                    {
                        _gameLoader.Save(game);
                    }
                    FormClosing -= Close;
                    FormClosing += Close;
                    Controls.Add(gmc);
                };
                Controls.Add(psm);
            };
            Controls.Add(mmc);
        }
    }
}
