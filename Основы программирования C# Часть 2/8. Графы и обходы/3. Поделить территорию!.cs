using System.Collections.Generic;
using System.Linq;

namespace Rivals;

public class RivalsTask
{
    public static IEnumerable<OwnedLocation> AssignOwners(Map map)
    {
        var queue = new Queue<OwnedLocation>();
        var visited = new HashSet<Point>();
        var chests = map.Chests.ToHashSet();
        for (var i = 0; i < map.Players.Length; i++)
        {
            visited.Add(map.Players[i]);
            queue.Enqueue(new OwnedLocation(i, map.Players[i], 0));
        }
        while (queue.Count != 0)
        {
            var ownedLocation = queue.Dequeue();
            yield return ownedLocation;
            if (chests.Contains(ownedLocation.Location)) continue;
            EnqueueIncidentLocations(map, queue, visited, ownedLocation);
        }
    }

    private static void EnqueueIncidentLocations(Map map, Queue<OwnedLocation> queue,
        HashSet<Point> visited, OwnedLocation ownedLocation)
    {
        for (int dy = -1; dy <= 1; dy++)
            for (int dx = -1; dx <= 1; dx++)
            {
                var nextLocation = new Point(ownedLocation.Location.X + dx, ownedLocation.Location.Y + dy);
                if ((dx != 0 && dy != 0) ||
                    visited.Contains(nextLocation) ||
                    !map.InBounds(nextLocation) ||
                    map.Maze[nextLocation.X, nextLocation.Y] != MapCell.Empty)
                    continue;
                visited.Add(nextLocation);
                queue.Enqueue(new OwnedLocation(ownedLocation.Owner, nextLocation, ownedLocation.Distance + 1));
            }
    }
}