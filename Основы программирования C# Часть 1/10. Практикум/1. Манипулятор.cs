using Avalonia;
using NUnit.Framework;
using System;
using System.Numerics;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
    public static double FindSecondX(float manipulatorPart, double angle, double firstX)
    {
        return manipulatorPart * Math.Cos(angle) + firstX;
    }

    public static double FindSecondY(float manipulatorPart, double angle, double firstY)
    {
        return manipulatorPart * Math.Sin(angle) + firstY;
    }

    public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
    {
        var elbowPos = new Point(FindSecondX(UpperArm, shoulder, 0), FindSecondY(UpperArm, shoulder, 0));
        var elbowH = shoulder + Math.PI + elbow;
        var wristPos = new Point(FindSecondX(Forearm, elbowH, elbowPos.X), FindSecondY(Forearm, elbowH, elbowPos.Y));
        var wristH = elbowH + Math.PI + wrist;
        var palmEndPos = new Point(FindSecondX(Palm, wristH, wristPos.X), FindSecondY(Palm, wristH, wristPos.Y));

        return new[]
        {
            elbowPos,
            wristPos,
            palmEndPos
        };
    }
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    public double GetLength(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point1.X - point2.X, 2)
            + Math.Pow(point1.Y - point2.Y, 2));
    }

    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
    [TestCase(Math.PI / 2, Math.PI, 3 * Math.PI, 0, Forearm + UpperArm + Palm)]
    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Forearm, UpperArm - Palm)]
    [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, -Forearm, UpperArm - Palm)]
    public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
        Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
        Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        Assert.AreEqual(GetLength(new Point(0, 0), joints[0]), UpperArm, 0.0001);
        Assert.AreEqual(GetLength(joints[0], joints[1]), Forearm, 0.0001);
        Assert.AreEqual(GetLength(joints[1], joints[2]), Palm, 0.0001);
    }
}