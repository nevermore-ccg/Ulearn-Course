using NUnit.Framework;
using System.Collections.Generic;

namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            int lengthX = original.GetLength(0);
            int lengthY = original.GetLength(1);
            var grayVersion = new double[lengthX, lengthY];
            for (int i = 0; i < lengthX; i++)
            {
                for (int j = 0; j < lengthY; j++)
                {
                    grayVersion[i, j] = (0.299 * original[i, j].R + 0.587
                                         * original[i, j].G + 0.114 * original[i, j].B) / 255;
                }
            }
            return grayVersion;
        }
    }
}