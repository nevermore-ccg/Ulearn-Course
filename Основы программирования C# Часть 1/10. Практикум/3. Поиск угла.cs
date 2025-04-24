using NUnit.Framework;
using System;

namespace Manipulation;

public class TriangleTask
{
    public static double GetABAngle(double a, double b, double c)
    {
        if ((a > 0 && b > 0) && c >= 0)
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        return double.NaN;
    }
}

[TestFixture]
public class TriangleTask_Tests
{
    [TestCase(3, 4, 5, Math.PI / 2)]
    [TestCase(1, 1, 1, Math.PI / 3)]
    [TestCase(1, 1, 4, double.NaN)]
    [TestCase(4, 2, 1, double.NaN)]
    [TestCase(1, 4, 1, double.NaN)]
    [TestCase(3.0, 3.0, 3.0, 1.0471975511965976)]
    public void TestGetABAngle(double a, double b, double c, double expectedAngle)
    {
        var actualAngle = TriangleTask.GetABAngle(a, b, c);

        Assert.AreEqual(expectedAngle, actualAngle, 0.0001);
    }
}