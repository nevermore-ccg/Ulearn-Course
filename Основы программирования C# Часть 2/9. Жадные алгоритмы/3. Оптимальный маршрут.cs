using Greedy.Architecture;
using System.Collections.Generic;
using System.Linq;

namespace Greedy;

public class NotGreedyPathFinder : IPathFinder
{
    public List<Point> FindPathToCompleteGoal(State state)
    {
        var pathFinder = new DijkstraPathFinder();
        var stack = new Stack<(PathWithCost CurrentPath, HashSet<Point> FoundChests)>();
        stack.Push((new PathWithCost(0, new Point[0]), new HashSet<Point>()));
        var start = state.Position;
        var fullPath = new List<(List<Point> Path, int chestsCount)>();
        while (stack.Count != 0)
        {
            var path = stack.Pop();
            if (path.FoundChests.Count == state.Chests.Count)
                return path.CurrentPath.Path;
            if (path.CurrentPath.Path.Count != 0)
                start = path.CurrentPath.End;
            var paths = pathFinder.GetPathsByDijkstra(state, start, state.Chests
                .Where(chest => !path.FoundChests.Contains(chest)))
                .Reverse()
                .ToList();
            if (paths.All(p => path.CurrentPath.Cost + p.Cost > state.InitialEnergy))
                fullPath.Add((path.CurrentPath.Path, path.FoundChests.Count));
            else
                PushNewPaths(state, state.InitialEnergy, stack, path, paths);
        }
        if (fullPath.Count != 0)
            return fullPath.MaxBy(fp => fp.chestsCount).Path;
        return new List<Point>();
    }

    private static void PushNewPaths(State state, int energy,
        Stack<(PathWithCost CurrentPath, HashSet<Point> FoundChests)> stack,
        (PathWithCost CurrentPath, HashSet<Point> FoundChests) path, List<PathWithCost> paths)
    {
        foreach (var p in paths)
        {
            var cost = path.CurrentPath.Cost + p.Cost;
            if (cost > energy) continue;
            if (p.Path
                .Where(point => state.Chests.Contains(point) && point != p.End && point != p.Start)
                .Count() != 0) continue;
            var newPath = new List<Point>();
            newPath.AddRange(path.CurrentPath.Path);
            newPath.AddRange(p.Path.Skip(1));
            var foundChests = new List<Point>() { p.End };
            foundChests.AddRange(path.FoundChests);
            stack.Push((new PathWithCost(cost, newPath.ToArray()), foundChests.ToHashSet()));
        }
    }
}