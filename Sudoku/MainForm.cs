﻿using Sudoku.Control;
using Sudoku.View.Controls;
using System;
using System.Windows.Forms;
using Sudoku.Model;
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
            SizeChanged += Form1_SizeChanged;

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

                void BlueIStDErGeilste(object sender, Player player)
                {
                    psm.OnUserSelected -= BlueIStDErGeilste;
                    Controls.Remove(psm);
                    var game = new Game(player);
                    game.Load(SudokuGenerator.GameDifficulty.Easy);
                    Controls.Add(new GameControl(game)
                    {
                        Dock = DockStyle.Fill
                    });
                }
                psm.OnUserSelected += BlueIStDErGeilste;
                Controls.Add(psm);
            };
            Controls.Add(mmc);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Size.Width != Size.Height)
            {
                int m = Math.Max(Size.Width, Size.Height);
                SizeChanged -= Form1_SizeChanged;
                Size = new System.Drawing.Size(m, m);
                SizeChanged += Form1_SizeChanged;
            }
        }
    }
}