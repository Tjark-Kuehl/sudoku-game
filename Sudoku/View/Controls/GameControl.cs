﻿using Sudoku.Control;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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
        public GameControl(Game game)
        {
            _game = game;
            InitializeComponent();

            _blackPenBig = new Pen(Color.Black, 6.0f);
            _blackPenSmall = new Pen(Color.Black, 1.0f);
            _yellowPenSmall = new Pen(Color.YellowGreen, 4.0f);
            _redPenSmall = new Pen(Color.Red, 4.0f);
            _orangePenSmall = new Pen(Color.Orange, 4.0f);
            _lightYelllow = new SolidBrush(Color.FromArgb(16, Color.Yellow));
            _lightGreen = new SolidBrush(Color.FromArgb(32, Color.Green));
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
                foreach ((int X, int Y) in _collisions)
                {
                    g.DrawRectangle(_redPenSmall,
                   x + X / 3 * (w3 + _blackPenBig.Width) + X % 3 * (w33 + _blackPenSmall.Width),
                   y + Y / 3 * (h3 + _blackPenBig.Width) + Y % 3 * (h33 + _blackPenSmall.Width),
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
            using (Graphics g = e.Graphics)
            using (var b = new Bitmap(Width, Height))
            using (var bg = Graphics.FromImage(b))
            {
                bg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                bg.PageScale = g.PageScale;
                bg.PageUnit = g.PageUnit;

                PaintBoard(bg);
                PaintSelection(bg);
                g.DrawImage(b, 0,0);
            }
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
            else { return; }

            if (!_game.TrySet(_m.X * 3 + _mm.X, _m.Y * 3 + _mm.Y, n, ref _collisions))
            {
                Invalidate();
                return;
            }

            Invalidate();

            if (_game.IsFinished())
            {
                MessageBox.Show("Congratz du kleiner wicht!");
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var w = ClientRectangle.Width - Padding.Right - Padding.Left;
            var h = ClientRectangle.Height - Padding.Bottom - Padding.Top;

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