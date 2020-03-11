using System.Collections.Generic;
using System.Drawing;
using Sudoku.Model;

namespace Sudoku
{
    /// <summary>
    ///     A field renderer.
    /// </summary>
    class FieldRenderer
    {
        /// <summary>
        ///     Gets the red pen small.
        /// </summary>
        /// <value>
        ///     The red pen small.
        /// </value>
        protected readonly Pen _blackPenBig, _blackPenSmall, _yellowPenSmall, _orangePenSmall, _redPenSmall;
        /// <summary>
        ///     Gets the light green.
        /// </summary>
        /// <value>
        ///     The light green.
        /// </value>
        protected readonly SolidBrush _lightYelllow, _lightGreen;
        /// <summary>
        ///     The font.
        /// </summary>
        protected readonly Font _font = new Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Pixel);

        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldRenderer"/> class.
        /// </summary>
        public FieldRenderer()
        {
            _blackPenBig = new Pen(Color.Black, 6.0f);
            _blackPenSmall = new Pen(Color.Black, 1.0f);
            _yellowPenSmall = new Pen(Color.YellowGreen, 4.0f);
            _redPenSmall = new Pen(Color.Red, 4.0f);
            _orangePenSmall = new Pen(Color.Orange, 4.0f);
            _lightYelllow = new SolidBrush(Color.FromArgb(16, Color.Yellow));
            _lightGreen = new SolidBrush(Color.FromArgb(32, Color.Green));
        }

        /// <summary>
        ///     Paints the selection.
        /// </summary>
        /// <param name="g">          The Graphics to process. </param>
        /// <param name="w">          The width. </param>
        /// <param name="h">          The height. </param>
        /// <param name="m">          The Point to process. </param>
        /// <param name="mm">         The millimetres. </param>
        /// <param name="collisions"> The collisions. </param>
        public void PaintSelection(Graphics g, float w, float h, Point m, Point mm, IList<(int xx, int yy)> collisions)
        {
            w -= 4 * _blackPenBig.Width;
            h -= 4 * _blackPenBig.Width;

            float w3 = w / 3.0f;
            float h3 = h / 3.0f;

            float w33 = (w3 - 2 * _blackPenSmall.Width) / 3.0f;
            float h33 = (h3 - 2 * _blackPenSmall.Width) / 3.0f;

            g.DrawRectangle(
                _orangePenSmall,
                _blackPenBig.Width + m.X * (w3 + _blackPenBig.Width) + _orangePenSmall.Width * 0.5f,
                _blackPenBig.Width + m.Y * (h3 + _blackPenBig.Width) + _orangePenSmall.Width * 0.5f,
                w3 - _orangePenSmall.Width,
                h3 - _orangePenSmall.Width);

            g.FillRectangle(
               _lightGreen,
               _blackPenBig.Width,
               _blackPenBig.Width + m.Y * (h3 + _blackPenBig.Width) + mm.Y * (h33 + _blackPenSmall.Width),
                w + 4 * _blackPenBig.Width,
                h33);

            g.FillRectangle(
               _lightGreen,
               _blackPenBig.Width + m.X * (w3 + _blackPenBig.Width) + mm.X * (w33 + _blackPenSmall.Width),
               _blackPenBig.Width,
                w33,
                h + 4 * _blackPenBig.Width);

            var x = _blackPenBig.Width + _yellowPenSmall.Width * 0.5f;
            var y = _blackPenBig.Width + _yellowPenSmall.Width * 0.5f;

            g.DrawRectangle(_yellowPenSmall,
                x + m.X * (w3 + _blackPenBig.Width) + mm.X * (w33 + _blackPenSmall.Width),
                y + m.Y * (h3 + _blackPenBig.Width) + mm.Y * (h33 + _blackPenSmall.Width),
                w33 - _yellowPenSmall.Width,
                h33 - _yellowPenSmall.Width);
            foreach ((int xx, int yy) in collisions)
            {
                g.DrawRectangle(_redPenSmall,
               x + xx / 3 * (w3 + _blackPenBig.Width) + xx % 3 * (w33 + _blackPenSmall.Width),
               y + yy / 3 * (h3 + _blackPenBig.Width) + yy % 3 * (h33 + _blackPenSmall.Width),
               w33 - _yellowPenSmall.Width,
               h33 - _yellowPenSmall.Width);
            }

        }

        /// <summary>
        ///     Paints the board.
        /// </summary>
        /// <param name="g">     The Graphics to process. </param>
        /// <param name="w">     The width. </param>
        /// <param name="h">     The height. </param>
        /// <param name="field"> The field. </param>
        public void PaintBoard(Graphics g, float w, float h, Field field)
        {
            w -= _blackPenBig.Width;
            h -= _blackPenBig.Width;

            var x = _blackPenBig.Width * 0.5f;
            var y = _blackPenBig.Width * 0.5f;

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
                            var b = field[xm * 3 + xmm, ym * 3 + ymm];
                            if(b.Locked != 0)
                                g.FillRectangle(
                                    new SolidBrush(Color.FromArgb(32 + 16 * b.Locked, Color.DimGray)),
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
    }
}
