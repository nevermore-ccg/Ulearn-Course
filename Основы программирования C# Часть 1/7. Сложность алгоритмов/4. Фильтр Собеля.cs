using System;

namespace Recognizer
{
    public static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var midPixel = (int)(sx.GetLength(0) / 2);
            var sy = TransposeMatrix(sx);
            for (int x = midPixel; x < width - midPixel; x++)
                for (int y = midPixel; y < height - midPixel; y++)
                {
                    var gx = Convolute(g, sx, x - midPixel, y - midPixel);
                    var gy = Convolute(g, sy, x - midPixel, y - midPixel);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        static double Convolute(double[,] g, double[,] matrix, int indexX, int indexY)
        {
            var side = matrix.GetLength(0);
            double result = 0;
            for (int x = 0; x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    result += matrix[x, y] * g[x + indexX, y + indexY];
                }
            }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var result = new double[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result[i, j] = matrix[j, i];
            return result;
        }
    }
}