using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class BfsTask
{
    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Chest[] chests)
    {
        var queue = new Queue<SinglyLinkedList<Point>>();
        queue.Enqueue(new SinglyLinkedList<Point>(start));
        var visited = new HashSet<Point> { start };
        var chestLocations = chests
            .Select(chest => chest.Location)
            .ToHashSet();
        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            if (chestLocations.Contains(point.Value)) yield return point;
            foreach (var dPoint in Walker.PossibleDirections)
            {
                var nextPoint = point.Value + dPoint;
                if (!map.InBounds(nextPoint) ||
                    visited.Contains(nextPoint) ||
                    map.Dungeon[nextPoint.X, nextPoint.Y] != MapCell.Empty)
                    continue;
                queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, point));
                visited.Add(nextPoint);
            }
        }
    }
}