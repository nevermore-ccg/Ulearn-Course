using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
    public Rocket GetNextMove(Rocket rocket)
    {
        var tasks = new Task<(Turn Turn, double Score)>[threadsCount];
        for (var i = 0; i < threadsCount; i++)
            tasks[i] = Task.Run(() => SearchBestMove(rocket,
                new Random(), iterationsCount / threadsCount));
        Task.WaitAll(tasks);
        var (turn, score) = tasks
            .Select(t => t.Result)
            .OrderBy(t => t.Score)
            .First();
        return rocket.Move(turn, level);
    }

    public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
    {
        return new() { Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount)) };
    }
}