using System;
using Sudoku.Control;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.View.Controls
{
    partial class GameControl : UserControl
    {
        protected readonly Pen _blackPenBig, _blackPenSmall, _yellowPenSmall, _orangePenSmall, _redPenSmall;
        protected readonly SolidBrush _lightYelllow, _lightGreen;

        protected Point? _currentLocation;
        protected IList<(int, int)> _collisions = new List<(int, int)>();

        protected Point _m, _mm;
        protected readonly Font _font = new Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Pixel);
        private readonly Game _game;
        private readonly GameMenuControl _gameMenu;

        private DateTime _scoreTime;

        public event EventHandler<EventArgs> OnGameFinished; 

        public GameControl(Game game, GameMenuControl gameMenu)
        {
            _game = game;
            _gameMenu = gameMenu;
            InitializeComponent();

            _blackPenBig = new Pen(Color.Black, 6.0f);
            _blackPenSmall = new Pen(Color.Black, 1.0f);
            _yellowPenSmall = new Pen(Color.YellowGreen, 4.0f);
            _redPenSmall = new Pen(Color.Red, 4.0f);
            _orangePenSmall = new Pen(Color.Orange, 4.0f);
            _lightYelllow = new SolidBrush(Color.FromArgb(16, Color.Yellow));
            _lightGreen = new SolidBrush(Color.FromArgb(32, Color.Green));

            _scoreTime = DateTime.Now;
        }

        void PaintSelection(Graphics g)
        {
            if (_currentLocation.HasValue)
            {
                var w = ClientRectangle.Width - Padding.Right - Padding.Left - 4 * _blackPenBig.Width;
                var h = ClientRectangle.Height - Padding.Bottom - Padding.Top - 4 * _blackPenBig.Width;

                float w3 = w / 3.0f;
                float h3 = h / 3.0f;

                float w33 = (w3 - 2 * _blackPenSmall.Width) / 3.0f;
                float h33 = (h3 - 2 * _blackPenSmall.Width) / 3.0f;

                g.DrawRectangle(
                    _orangePenSmall,
                    Padding.Left + _blackPenBig.Width + _m.X * (w3 + _blackPenBig.Width) + _orangePenSmall.Width * 0.5f,
                    Padding.Top + _blackPenBig.Width + _m.Y * (h3 + _blackPenBig.Width) + _orangePenSmall.Width * 0.5f,
                    w3 - _orangePenSmall.Width,
                    h3 - _orangePenSmall.Width);

                g.FillRectangle(
                   _lightGreen,
                    Padding.Left + _blackPenBig.Width,
                    Padding.Top + _blackPenBig.Width + _m.Y * (h3 + _blackPenBig.Width) + _mm.Y * (h33 + _blackPenSmall.Width),
                    w + 4 * _blackPenBig.Width,
                    h33);

                g.FillRectangle(
                   _lightGreen,
                    Padding.Left + _blackPenBig.Width + _m.X * (w3 + _blackPenBig.Width) + _mm.X * (w33 + _blackPenSmall.Width),
                    Padding.Top + _blackPenBig.Width,
                    w33,
                    h + 4 * _blackPenBig.Width);

                var x = Padding.Left + _blackPenBig.Width + _yellowPenSmall.Width * 0.5f;
                var y = Padding.Top + _blackPenBig.Width + _yellowPenSmall.Width * 0.5f;

                g.DrawRectangle(_yellowPenSmall,
                    x + _m.X * (w3 + _blackPenBig.Width) + _mm.X * (w33 + _blackPenSmall.Width),
                    y + _m.Y * (h3 + _blackPenBig.Width) + _mm.Y * (h33 + _blackPenSmall.Width),
                    w33 - _yellowPenSmall.Width,
                    h33 - _yellowPenSmall.Width);
                foreach ((int xx, int yy) in _collisions)
                {
                    g.DrawRectangle(_redPenSmall,
                   x + xx / 3 * (w3 + _blackPenBig.Width) + xx % 3 * (w33 + _blackPenSmall.Width),
                   y + yy / 3 * (h3 + _blackPenBig.Width) + yy % 3 * (h33 + _blackPenSmall.Width),
                   w33 - _yellowPenSmall.Width,
                   h33 - _yellowPenSmall.Width);
                }
            }
        }

        void PaintBoard(Graphics g)
        {
            var x = Padding.Left + _blackPenBig.Width * 0.5f;
            var y = Padding.Top + _blackPenBig.Width * 0.5f;
            var w = ClientRectangle.Width - Padding.Right - Padding.Left - _blackPenBig.Width;
            var h = ClientRectangle.Height - Padding.Bottom - Padding.Top - _blackPenBig.Width;

            float w3 = w / 3.0f;
            float h3 = h / 3.0f;

            float w33 = w3 / 3.0f;
            float h33 = h3 / 3.0f;

            g.DrawRectangle(_blackPenBig, x, y, w, h);

            for (int i = 1; i < 3; i++)
            {
                var ow = x + w3 * i;
                g.DrawLine(_blackPenBig,
                        ow,
                        y,
                        ow,
                        y + h);

                var oh = y + h3 * i;
                g.DrawLine(_blackPenBig,
                            x,
                            oh,
                            x + w,
                            oh);

            }
            for (int i = 0; i < 3; i++)
            {
                var ow = x + w3 * i;
                var oh = y + h3 * i;
                for (int i2 = 1; i2 < 3; i2++)
                {
                    g.DrawLine(_blackPenSmall,
                            ow + w33 * i2,
                            y,
                            ow + w33 * i2,
                            y + h);
                    g.DrawLine(_blackPenSmall,
                           x,
                           oh + h33 * i2,
                           x + w,
                           oh + h33 * i2);
                }
            }

            for (int ym = 0; ym < 3; ym++)
                for (int xm = 0; xm < 3; xm++)
                {
                    var ow = x + w3 * xm;
                    var oh = y + h3 * ym;
                    for (int ymm = 0; ymm < 3; ymm++)
                        for (int xmm = 0; xmm < 3; xmm++)
                        {
                            var b = _game.Get(xm * 3 + xmm, ym * 3 + ymm);
                            if (b.Locked != 0)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(16 + 8 * b.Locked, Color.DimGray)),
                                    ow + xmm * w33,
                                    oh + ymm * h33,
                                    w33,
                                    h33);

                            if (b.Value == 0) continue;
                            string k = b.Value.ToString();
                            var s = g.MeasureString(k, _font);
                            g.DrawString(k,
                                _font,
                                new SolidBrush(Color.Black),
                                ow + w33 * xmm + w33 * 0.5f - s.Width * 0.5f,
                                oh + h33 * ymm + h33 * 0.5f - s.Height * 0.5f);
                        }
                }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var g = e.Graphics;
            using var b = new Bitmap(Width, Height);
            using var bg = Graphics.FromImage(b);

            bg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            bg.PageScale = g.PageScale;
            bg.PageUnit = g.PageUnit;

            PaintBoard(bg);
            PaintSelection(bg);
            g.DrawImage(b, 0, 0);

        }

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
                    if (_game.Islocked(_m.X * 3 + _mm.X, y)) return false;
                    _m.Y = y / 3;
                    _mm.Y = y - _m.Y * 3;
                    this.Invalidate();
                    return true;

                }
                bool SetX(int x)
                {
                    if (_game.Islocked(x, _m.Y * 3 + _mm.Y)) return false;
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
            

            var scoreTime = DateTime.Now;
            var tb = scoreTime - _scoreTime;
            var factor = ScoreFactor(tb.Milliseconds);
            _scoreTime = DateTime.Now;

            if (!_game.TrySet(_m.X * 3 + _mm.X, _m.Y * 3 + _mm.Y, n, ref _collisions))
            {
                this._gameMenu.Score += (int)(_game.CalculateScore(_m.X * 3 + _mm.X, _m.Y * 3 + _mm.Y, true) * factor);
                Invalidate();
                return;
            }

            Invalidate();
            this._gameMenu.Score += (int)(_game.CalculateScore(_m.X * 3 + _mm.X, _m.Y * 3 + _mm.Y, false) * factor);

            if (_game.IsFinished())
            {
                MessageBox.Show("Congratz du kleiner wicht!");
            }
        }

        private static float ScoreFactor(int value)
        {
           return 1f + ( 4f / ( (value / 2000f) + 1f ));
        }
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

            if (_game.Islocked(xmm + xm * 3, ymm + ym * 3))
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
