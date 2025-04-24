using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double _minLength = double.MaxValue;
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrder = MakeTrivialPermutation(checkpoints,
                            new int[checkpoints.Length], new int[checkpoints.Length], 1, 0);
            _minLength = double.MaxValue;
            return bestOrder;
        }

        private static int[] MakeTrivialPermutation(Point[] checkpoints,
                             int[] order, int[] bestOrder, int position, double path)
        {
            if (position == checkpoints.Length)
            {
                if (path < _minLength)
                    _minLength = path;
                return (int[])order.Clone();
            }
            for (int i = 1; i < order.Length; i++)
            {
                var index = Array.IndexOf(order, i, 0, position);
                if (index != -1)
                    continue;
                order[position] = i;

                var distance = PointExtensions.DistanceTo(checkpoints[order[position - 1]],
                                                          checkpoints[order[position]]);
                path += distance;
                if (path > _minLength)
                    continue;
                bestOrder = MakeTrivialPermutation(checkpoints, order, bestOrder, position + 1, path);
                path -= distance;
            }
            return bestOrder;
        }
    }
}