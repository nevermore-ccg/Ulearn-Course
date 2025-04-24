using System;
using System.Linq;

namespace Dungeon;

public class DungeonTask
{
    public static MoveDirection[] FindShortestPath(Map map)
    {
        SinglyLinkedList<Point>? path = null;
        if (map.Chests.Length != 0)
        {
            path = GetShortestPathWithChest(map);
        }
        if (path == null)
        {
            path = BfsTask.FindPaths(map, map.InitialPosition, new[] { new EmptyChest(map.Exit) })
                .FirstOrDefault();
        }
        if (path == null) return new MoveDirection[0];
        return ConvertPointsToDirections(path);
    }

    private static SinglyLinkedList<Point>? GetShortestPathWithChest(Map map)
    {
        var startPaths = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        var endPaths = BfsTask.FindPaths(map, map.Exit, map.Chests);
        var chestsDictionary = map.Chests.ToDictionary(x => x.Location);
        var fullPaths = startPaths
            .Join(endPaths, listToChests =>
            listToChests.Value, listToExit => listToExit.Value, Tuple.Create)
            .Select(p => Tuple.Create(p.Item1, p.Item2, chestsDictionary[p.Item1.Value].Value))
            .ToList();
        var shortestPath = fullPaths
            .MinBy(p => (p.Item1.Length + p.Item2.Length - 1, -p.Item3));
        var result = shortestPath?.Item2
            .Skip(1)
            .Aggregate(shortestPath.Item1, (current, point) =>
                new SinglyLinkedList<Point>(point, current));
        return result;
    }

    private static MoveDirection[] ConvertPointsToDirections(SinglyLinkedList<Point> path)
    {
        var pathList = path
            .Reverse()
            .ToList();
        return pathList
            .Zip(pathList.Skip(1), (first, second) => Walker.ConvertOffsetToDirection(second - first))
            .ToArray();
    }
}