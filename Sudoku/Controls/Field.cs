using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.Controls
{
    class Field : FieldBase
    {
        protected override void OnPaintFieldValues(Graphics g)
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
                foreach (Point collision in _collisionErrors)
                {
                    g.DrawRectangle(_redPenSmall,
                   x + collision.X / 3 * (w3 + _blackPenBig.Width) + collision.X % 3 * (w33 + _blackPenSmall.Width),
                   y + collision.Y / 3 * (h3 + _blackPenBig.Width) + collision.Y % 3 * (h33 + _blackPenSmall.Width),
                   w33 - _yellowPenSmall.Width,
                   h33 - _yellowPenSmall.Width);
                }
            }
        }
    }
}