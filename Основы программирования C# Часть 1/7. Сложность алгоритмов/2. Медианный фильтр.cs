using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    public static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);
            if (lengthX == 1 && lengthY == 1)
                return original;
            var filteredPic = new double[lengthX, lengthY];
            for (int i = 0; i < lengthX; i++)
                for (int j = 0; j < lengthY; j++)
                    if (lengthX != 1 && lengthY != 1)
                        if ((i == 0 || i == lengthX - 1) && (j == 0 || j == lengthY - 1))
                            filteredPic[i, j] = FilterAngle(original, i, j, lengthX, lengthY);
                        else if (i - 1 < 0 || i + 1 == lengthX || j - 1 < 0 || j + 1 == lengthY)
                            filteredPic[i, j] = FilterBorder(original, i, j, lengthX);
                        else
                            filteredPic[i, j] = FindMedian(original[i - 1, j - 1], original[i - 1, j], original[i - 1, j + 1],
                                original[i, j - 1], original[i, j], original[i, j + 1],
                                original[i + 1, j - 1], original[i + 1, j], original[i + 1, j + 1]);
                    else
                        filteredPic[i, j] = FilterLength(original, i, j, lengthX, lengthY);
            return filteredPic;
        }

        public static double FilterLength(double[,] original, int x, int y, int lengthX, int lengthY)
        {
            if (lengthX == 1)
                if (y - 1 < 0)
                    return FindMedian(original[x, y], original[x, y + 1]);
                else if (y + 1 == lengthY)
                    return FindMedian(original[x, y], original[x, y - 1]);
                else
                    return FindMedian(original[x, y], original[x, y - 1], original[x, y + 1]);
            else
                if (x - 1 < 0)
                return FindMedian(original[x, y], original[x + 1, y]);
            else if (x + 1 == lengthY)
                return FindMedian(original[x, y], original[x - 1, y]);
            else
                return FindMedian(original[x, y], original[x - 1, y], original[x + 1, y]);
        }

        public static double FilterBorder(double[,] original, int x, int y, int lengthX)
        {
            if (x - 1 < 0)
                return FindMedian(original[x, y], original[x, y - 1], original[x, y + 1], original[x + 1, y], original[x + 1, y - 1], original[x + 1, y + 1]);
            else if (x + 1 == lengthX)
                return FindMedian(original[x, y], original[x, y - 1], original[x, y + 1], original[x - 1, y], original[x - 1, y - 1], original[x - 1, y + 1]);
            else if (y - 1 < 0)
                return FindMedian(original[x, y], original[x + 1, y], original[x - 1, y], original[x - 1, y + 1], original[x, y + 1], original[x + 1, y + 1]);
            else
                return FindMedian(original[x, y], original[x + 1, y], original[x - 1, y], original[x - 1, y - 1], original[x, y - 1], original[x + 1, y - 1]);
        }

        public static double FilterAngle(double[,] original, int x, int y, int lengthX, int lengthY)
        {
            if (x == 0 && y == 0)
                return FindMedian(original[x, y], original[x + 1, y], original[x + 1, y + 1], original[x, y + 1]);
            else if (x == 0 && y == lengthY - 1)
                return FindMedian(original[x, y], original[x, y - 1], original[x + 1, y], original[x + 1, y - 1]);
            else if (x == lengthX - 1 && y == 0)
                return FindMedian(original[x, y], original[x - 1, y], original[x - 1, y + 1], original[x, y + 1]);
            else
                return FindMedian(original[x, y], original[x, y - 1], original[x - 1, y], original[x - 1, y - 1]);
        }

        public static double FindMedian(params double[] values)
        {
            var list = new List<double>(values);
            list = list.OrderBy(x => x).ToList();
            if (list.Count % 2 == 0)
                return (list[list.Count / 2] + list[(list.Count - 1) / 2]) / 2;
            else
                return list[list.Count / 2];
        }
    }
}