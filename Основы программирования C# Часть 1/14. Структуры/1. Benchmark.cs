using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StructBenchmarking;
public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        task.Run();
        var timer = new Stopwatch();
        timer.Start();
        for (int i = 0; i < repetitionCount; i++)
        {
            task.Run();
        }
        timer.Stop();
        return timer.Elapsed.TotalMilliseconds / repetitionCount;
    }
}
public class StringBuilderTask : ITask
{
    public void Run()
    {
        var strBuilder = new StringBuilder();
        for (int i = 0; i < 10000; i++)
        {
            strBuilder.Append('a');
        }
        var result = strBuilder.ToString();
    }
}
public class StringConstructorTask : ITask
{
    public void Run()
    {
        var result = new string('a', 10000);
    }
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var strBuilderTask = new StringBuilderTask();
        var strConstructorTask = new StringConstructorTask();
        var benchmark = new Benchmark();

        var strConstructorTime = benchmark.MeasureDurationInMs(strConstructorTask, 100);
        var strBuilderTime = benchmark.MeasureDurationInMs(strBuilderTask, 100);

        Assert.Less(strConstructorTime, strBuilderTime);
    }
}