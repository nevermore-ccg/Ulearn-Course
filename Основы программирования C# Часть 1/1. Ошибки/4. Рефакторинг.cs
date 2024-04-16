using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    public class Painter
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawTrajectory(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
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

            DrawFirstSide(size);
            DrawSecondSide(size);
            DrawThirdSide(size);
            DrawFourthSide(size);
        }

        private static void DrawFourthSide(int size)
        {
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, Math.PI / 2);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, Math.PI / 2 + Math.PI);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f - size * 0.04f, Math.PI / 2 + Math.PI / 2);
            Painter.Change(size * 0.04f, Math.PI / 2 - Math.PI);
            Painter.Change(size * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawThirdSide(int size)
        {
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, Math.PI);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, Math.PI + Math.PI);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f - size * 0.04f, Math.PI + Math.PI / 2);
            Painter.Change(size * 0.04f, Math.PI - Math.PI);
            Painter.Change(size * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        private static void DrawSecondSide(int size)
        {
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, -Math.PI / 2);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, -Math.PI / 2 + Math.PI);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f - size * 0.04f, -Math.PI / 2 + Math.PI / 2);
            Painter.Change(size * 0.04f, -Math.PI / 2 - Math.PI);
            Painter.Change(size * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawFirstSide(int size)
        {
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, 0);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.04f * Math.Sqrt(2), Math.PI / 4);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f, Math.PI);
            Painter.DrawTrajectory(new Pen(Brushes.Yellow), size * 0.375f - size * 0.04f, Math.PI / 2);
            Painter.Change(size * 0.04f, -Math.PI);
            Painter.Change(size * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }
    }
}