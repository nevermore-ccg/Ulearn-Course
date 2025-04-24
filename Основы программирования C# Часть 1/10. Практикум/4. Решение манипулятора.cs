using Avalonia.Media;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        var wristX = x - Palm * Math.Cos(alpha);
        var wristY = y + Palm * Math.Sin(alpha);
        var distanceToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);
        var elbow = TriangleTask.GetABAngle(UpperArm, Forearm, distanceToWrist);
        var shoulder = TriangleTask.GetABAngle(UpperArm, distanceToWrist, Forearm)
            + Math.Atan2(wristY, wristX);
        var wrist = -alpha - shoulder - elbow;
        var result = new double[] { shoulder, elbow, wrist };
        if (result.Any(double.IsNaN))
            return new[] { double.NaN, double.NaN, double.NaN };
        return result;
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    [Test]
    public void TestMoveManipulatorTo()
    {
        Random rand = new Random();
        for (int i = 0; i < 100; i++)
        {
            double x = rand.NextDouble();
            double y = rand.NextDouble();
            double alpha = rand.NextDouble() * 2 * Math.PI;

            var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);

            if (!angles.Any(double.IsNaN))
            {
                var effectorCoordinates =
                    AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                Assert.AreEqual(x, effectorCoordinates[2].X, 0.0001);
                Assert.AreEqual(y, effectorCoordinates[2].Y, 0.0001);
            }
            else
                Assert.AreEqual(angles[0], angles[1], 0.0001);
        }
    }
}