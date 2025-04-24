using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var windowQueue = new Queue<double>();
        var maxValueList = new LinkedList<double>();
        foreach (var point in data)
        {
            windowQueue.Enqueue(point.OriginalY);
            while (maxValueList.Count > 0 && maxValueList.Last.Value < point.OriginalY)
                maxValueList.RemoveLast();
            maxValueList.AddLast(point.OriginalY);
            if (windowQueue.Count > windowWidth && maxValueList.First.Value == windowQueue.Dequeue())
                maxValueList.RemoveFirst();
            yield return point.WithMaxY(maxValueList.First.Value);
        }
    }
}