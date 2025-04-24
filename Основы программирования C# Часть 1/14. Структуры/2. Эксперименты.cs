using System.Collections.Generic;

namespace StructBenchmarking;

public class Experiments
{
    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();
        var arrayCreationTask = new ArrayCreationTask();

        foreach (var fieldsCount in Constants.FieldCounts)
        {
            classesTimes.Add(new ExperimentResult(fieldsCount,
                benchmark.MeasureDurationInMs(arrayCreationTask.CreateClassTask(fieldsCount), repetitionsCount)));
            structuresTimes.Add(new ExperimentResult(fieldsCount,
                benchmark.MeasureDurationInMs(arrayCreationTask.CreateStructTask(fieldsCount), repetitionsCount)));
        }

        return new ChartData
        {
            Title = "Create array",
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();
        var methodCallTask = new MethodCallTask();

        foreach (var fieldsCount in Constants.FieldCounts)
        {
            classesTimes.Add(new ExperimentResult(fieldsCount,
                benchmark.MeasureDurationInMs(methodCallTask.CreateClassTask(fieldsCount), repetitionsCount)));
            structuresTimes.Add(new ExperimentResult(fieldsCount,
                benchmark.MeasureDurationInMs(methodCallTask.CreateStructTask(fieldsCount), repetitionsCount)));
        }

        return new ChartData
        {
            Title = "Call method with argument",
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }
}
public interface IBenchmarkTasks
{
    public ITask CreateStructTask(int size);
    public ITask CreateClassTask(int size);
}

public class ArrayCreationTask : IBenchmarkTasks
{
    public ITask CreateClassTask(int size)
    {
        return new ClassArrayCreationTask(size);
    }

    public ITask CreateStructTask(int size)
    {
        return new StructArrayCreationTask(size);
    }
}

public class MethodCallTask : IBenchmarkTasks
{
    public ITask CreateClassTask(int size)
    {
        return new MethodCallWithClassArgumentTask(size);
    }

    public ITask CreateStructTask(int size)
    {
        return new MethodCallWithStructArgumentTask(size);
    }
}