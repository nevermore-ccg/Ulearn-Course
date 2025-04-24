using Avalonia.Media;
using RefactorMe.Common;
using System;

namespace RefactorMe
{
    public class Painter
    {
        private static float _x, _y;
        private static IGraphics _graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            _graphics = newGraphics;
            _graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            _x = x0;
            _y = y0;
        }

        public static void DrawTrajectory(Pen pen, double length, double angle)
        {
            var x1 = (float)(_x + length * Math.Cos(angle));
            var y1 = (float)(_y + length * Math.Sin(angle));
            _graphics.DrawLine(pen, _x, _y, x1, y1);
            _x = x1;
            _y = y1;
        }

        public static void Change(double length, double angle)
        {
            _x = (float)(_x + length * Math.Cos(angle));
            _y = (float)(_y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleRotation, IGraphics graphics)
        {
            // angleRotation пока не используется, но будет использоваться в будущем
            Painter.Initialize(graphics);
            var size = Math.Min(width, height);
            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
            Painter.SetPosition(x0, y0);
            var pen = new Pen(Brushes.Yellow);
            DrawSide(size, 0, pen);
            DrawSide(size, -Math.PI / 2, pen);
            DrawSide(size, Math.PI, pen);
            DrawSide(size, Math.PI / 2, pen);
        }

        private static void DrawSide(int size, double angle, Pen pen)
        {
            Painter.DrawTrajectory(pen, size * 0.375f, angle);
            Painter.DrawTrajectory(pen, size * 0.04f * Math.Sqrt(2), angle + Math.PI / 4);
            Painter.DrawTrajectory(pen, size * 0.375f, angle + Math.PI);
            Painter.DrawTrajectory(pen, size * 0.375f - size * 0.04f, angle + Math.PI / 2);
            Painter.Change(size * 0.04f, angle - -Math.PI);
            Painter.Change(size * 0.04f * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }
    }
}