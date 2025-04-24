using Greedy.Architecture;
using System.Collections.Generic;
using System.Linq;

namespace Greedy;

public class GreedyPathFinder : IPathFinder
{
    public List<Point> FindPathToCompleteGoal(State state)
    {
        if (state.Chests.Count == 1) return new List<Point>();
        var pathFinder = new DijkstraPathFinder();
        var start = state.Position;
        var foundChests = new HashSet<Point>();
        var path = new List<Point>();
        var energy = state.InitialEnergy;
        for (var i = 0; i < state.Goal; i++)
        {
            var bestPath = pathFinder.GetPathsByDijkstra(state, start, state.Chests
                .Where(chest => !foundChests.Contains(chest)))
                .First();
            if (bestPath == null) return new List<Point>();
            energy -= bestPath.Cost;
            if (energy < 0) break;
            foundChests.Add(bestPath.End);
            start = bestPath.End;
            path.AddRange(bestPath.Path.Skip(1));
        }
        return path;
    }
}