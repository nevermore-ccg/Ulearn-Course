using System;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var random = new Random(seed);
            double x = 1.0;
            double y = 0;
            const double pi = Math.PI;

            for (int i = 0; i < iterationsCount; i++)
            {
                double x1;
                double y1;
                if (random.Next(2) == 0)
                {
                    x1 = (x * Math.Cos(45 * pi / 180) - y * Math.Sin(45 * pi / 180)) / Math.Sqrt(2);
                    y1 = (x * Math.Sin(45 * pi / 180) + y * Math.Cos(45 * pi / 180)) / Math.Sqrt(2);
                }
                else
                {
                    x1 = (x * Math.Cos(135 * pi / 180) - y * Math.Sin(135 * pi / 180)) / Math.Sqrt(2) + 1;
                    y1 = (x * Math.Sin(135 * pi / 180) + y * Math.Cos(135 * pi / 180)) / Math.Sqrt(2);
                }
                pixels.SetPixel(x1, y1);
                x = x1;
                y = y1;
            }
        }
    }
}