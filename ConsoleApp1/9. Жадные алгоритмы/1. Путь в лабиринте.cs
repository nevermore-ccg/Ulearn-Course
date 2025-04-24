using Greedy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greedy;
internal class DijkstraData
{
    public Point Previous { get; set; }
    public int Price { get; set; }
}
public class DijkstraPathFinder
{
    public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
        IEnumerable<Point> targets)
    {
        var track = new Dictionary<Point, DijkstraData>();
        track[start] = new DijkstraData { Previous = start, Price = 0 };
        var visited = new HashSet<Point> { start };
        while (visited.Count != 0)
        {
            Point toOpen = start;
            var bestPrice = double.PositiveInfinity;
            foreach (var point in visited)
                if (track.ContainsKey(point) && track[point].Price < bestPrice)
                {
                    toOpen = point;
                    bestPrice = track[point].Price;
                }
            visited.Remove(toOpen);
            if (targets.Contains(toOpen))
            {
                var path = CreatePath(track, toOpen);
                if (path != null) yield return path;
            }
            TrackIncidentPoints(state, track, visited, toOpen);
        }
    }

    private static void TrackIncidentPoints(State state, Dictionary<Point, DijkstraData> track,
        HashSet<Point> visited, Point toOpen)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                var nextPoint = toOpen + new Point(dx, dy);
                if (Math.Abs(dx) + Math.Abs(dy) != 1 ||
                    !state.InsideMap(nextPoint) ||
                    state.IsWallAt(nextPoint)) continue;
                var currentPrice = track[toOpen].Price + state.CellCost[nextPoint.X, nextPoint.Y];
                if (!track.ContainsKey(nextPoint) || track[nextPoint].Price > currentPrice)
                {
                    track[nextPoint] = new DijkstraData { Previous = toOpen, Price = currentPrice };
                    visited.Add(nextPoint);
                }
            }
    }

    private static PathWithCost CreatePath(Dictionary<Point, DijkstraData> track, Point end)
    {
        var path = new List<Point>();
        var price = track[end].Price;
        while (end != track[end].Previous)
        {
            path.Add(end);
            end = track[end].Previous;
        }
        path.Add(end);
        path.Reverse();
        return new PathWithCost(price, path.ToArray());
    }
}