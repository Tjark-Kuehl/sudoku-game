using System;
using Sudoku.Control;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    /// <summary>
    ///     A game control.
    /// </summary>
    partial class GameControl : UserControl
    {
        /// <summary>
        ///     The current location.
        /// </summary>
        protected Point? _currentLocation;
        /// <summary>
        ///     The collisions.
        /// </summary>
        protected IList<(int, int)> _collisions = new List<(int, int)>();

        /// <summary>
        ///     Gets the millimetres.
        /// </summary>
        /// <value>
        ///     The millimetres.
        /// </value>
        protected Point _m, _mm;
        
        /// <summary>
        ///     The game.
        /// </summary>
        private readonly Game _game;
        /// <summary>
        ///     The game menu.
        /// </summary>
        private readonly GameMenuControl _gameMenu;
        /// <summary>
        ///     The field renderer.
        /// </summary>
        private readonly FieldRenderer _fieldRenderer;


        /// <summary>
        ///     Occurs when On Game Finished.
        /// </summary>
        public event EventHandler<EventArgs> OnGameFinished; 

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        /// <param name="game">     The game. </param>
        /// <param name="gameMenu"> The game menu. </param>
        public GameControl(Game game, GameMenuControl gameMenu)
        {
            _game = game;
            _gameMenu = gameMenu;
            _fieldRenderer = new FieldRenderer();

            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var g = e.Graphics;
            using var b = new Bitmap(Width, Height);
            using var bg = Graphics.FromImage(b);

            bg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            bg.PageScale = g.PageScale;
            bg.PageUnit = g.PageUnit;

            _fieldRenderer.PaintBoard(bg, Width, Height, _game.Field);
            if(_currentLocation.HasValue)
                _fieldRenderer.PaintSelection(bg, Width, Height, _m, _mm, _collisions);
            g.DrawImage(b, 0, 0);

        }
        /// <inheritdoc/>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            byte n;
            if (_currentLocation.HasValue && (int)e.KeyCode >= '1' && (int)e.KeyCode <= '9')
            {
                n = (byte)((int)e.KeyCode - '1' + 1);
                Invalidate();
            }
            else if (_currentLocation.HasValue && (int)e.KeyCode >= (int)Keys.NumPad1 && (int)e.KeyCode <= (int)Keys.NumPad9)
            {
                n = (byte)((int)e.KeyCode - (int)Keys.NumPad1 + 1);
            }
            else
            {
                if (!_currentLocation.HasValue) { return; }

                bool SetY(int y)
                {
                    if (_game.IsLocked(_m.X * 3 + _mm.X, y)) return false;
                    _m.Y = y / 3;
                    _mm.Y = y - _m.Y * 3;
                    this.Invalidate();
                    return true;

                }
                bool SetX(int x)
                {
                    if (_game.IsLocked(x, _m.Y * 3 + _mm.Y)) return false;
                    _m.X = x / 3;
                    _mm.X = x - _m.X * 3;
                    this.Invalidate();
                    return true;

                }
                switch (e.KeyCode)
                {
                    case Keys.S:
                    case Keys.Down:
                        {
                            for (int y = _m.Y * 3 + _mm.Y + 1; y < 9; y++)
                            {
                                if (SetY(y)) { return; }
                            }
                            for (int y = 0; y < _m.Y * 3 + _mm.Y; y++)
                            {
                                if (SetY(y)) { return; }
                            }
                            break;
                        }
                    case Keys.W:
                    case Keys.Up:
                        {
                            for (int y = _m.Y * 3 + _mm.Y - 1; y >= 0; y--)
                            {
                                if (SetY(y)) { return; }
                            }
                            for (int y = 8; y > _m.Y * 3 + _mm.Y; y--)
                            {
                                if (SetY(y)) { return; }
                            }
                            break;
                        }

                    case Keys.D:
                    case Keys.Right:
                        {
                            for (int x = _m.X * 3 + _mm.X + 1; x < 9; x++)
                            {
                                if (SetX(x)) { return; }
                            }
                            for (int x = 0; x < _m.X * 3 + _mm.X; x++)
                            {
                                if (SetX(x)) { return; }
                            }
                            break;
                        }
                    case Keys.A:
                    case Keys.Left:
                        {
                            for (int x = _m.X * 3 + _mm.X - 1; x >= 0; x--)
                            {
                                if (SetX(x)) { return; }
                            }
                            for (int x = 8; x > _m.X * 3 + _mm.X; x--)
                            {
                                if (SetX(x)) { return; }
                            }
                            break;
                        }
                }
                return;
            }

            if (!_game.TrySet(_m.X * 3 + _mm.X, _m.Y * 3 + _mm.Y, n, ref _collisions))
            {
                Invalidate();
                return;
            }

            Invalidate();
            if (_game.IsFinished())
            {
                OnGameFinished?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <inheritdoc/>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int w = ClientRectangle.Width - Padding.Right - Padding.Left;
            int h = ClientRectangle.Height - Padding.Bottom - Padding.Top;

            float w3 = w / 3.0f;
            float h3 = h / 3.0f;

            float lx = (float)e.Location.X - Padding.Left - Padding.Right;
            float ly = (float)e.Location.Y - Padding.Top - Padding.Bottom;

            int xm = (int)((lx / w) * 3);
            int ym = (int)((ly / h) * 3);

            int xmm = (int)(((lx - (xm * w3)) / w3) * 3);
            int ymm = (int)(((ly - (ym * h3)) / h3) * 3);

            if (_game.IsLocked(xmm + xm * 3, ymm + ym * 3))
            {
                _currentLocation = null;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                _currentLocation = e.Location;
                _m = new Point(xm, ym);
                _mm = new Point(xmm, ymm);
            }
            else
            {
                _game.Set(xmm + xm * 3, ymm + ym * 3, 0);
            }

            _collisions.Clear();
            Invalidate();
        }
    }
}
