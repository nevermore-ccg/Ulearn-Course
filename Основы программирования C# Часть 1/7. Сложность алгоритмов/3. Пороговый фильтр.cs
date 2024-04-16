using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);
            var threshold = (int)(whitePixelsFraction * original.Length);
            var filteredPic = new double[lengthX, lengthY];
            var list = new List<double>(original.Length);
            foreach (var value in original)
                list.Add(value);
            list = list.OrderByDescending(x => x).ToList();
            list.RemoveRange(threshold, list.Count - threshold);
            for (var i = 0; i < lengthX; i++)
            {
                for (var j = 0; j < lengthY; j++)
                {
                    if (list.Contains(original[i, j]))
                        filteredPic[i, j] = 1.0;
                    else
                        filteredPic[i, j] = 0.0;
                }
            }
            return filteredPic;
        }
    }
}